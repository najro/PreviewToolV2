using FigurePreview.Configuration;
using FigurePreview.Factories;
using FigurePreview.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FigurePreview
{
    public partial class PreviewToolForm : Form
    {
        private FigureItemFactory figureItemFactory;
        private HtmlViewFactory htmlViewFactory;
        
        private FileSystemWatcher watcherDynamicPathFile;
        private FileSystemWatcher watcherFiguresFolders;
        
        private string selectedDisplayName;
        private string selectedPathFiguresRootFolder;

        public PreviewToolForm()
        {
            InitializeComponent();
            ResetErrorMessage();
            VerifyConfigurationAndDisplayFigures(true);           
            WatchFiguresFoldersModifications();

            if (FigureConfiguration.Instance.FigurePreview.DynamicPathFile.Enabled)
            {
                WatchDynamicPathFileModifications();
            }
        }
        
        public void VerifyConfigurationAndDisplayFigures(bool loadForm = false)
        {
            if (!FigureConfiguration.Instance.IsConfigurationValid(out string errorMessage))
            {
                DisplayError(errorMessage);
                return;
            }

            InitializeFactories(loadForm);
            ResetErrorMessage();
            EnableViewComponents();
            SetInitalSelectedPathFigureRootFolder();
            DisplayFigures();
        }
          
        private void DisplayFigures()
        {
            // verify that selectedPathFiguresRootFolder exists
            if (selectedPathFiguresRootFolder == null || string.IsNullOrWhiteSpace(selectedPathFiguresRootFolder) || !Directory.Exists(selectedPathFiguresRootFolder))
            {
                return;
            }

            // Delete all html files
            htmlViewFactory.CleanUpViewFiles();

            // get unique list of figures to display
            var list = figureItemFactory.GetDisplayItems(selectedPathFiguresRootFolder);

            // clear figure displaylist  and add unique list of figures
            listBoxDisplayItems.Items.Clear();
            listBoxDisplayItems.DisplayMember = "Name";

            foreach (var displayItem in list)
            {
                listBoxDisplayItems.Items.Add(displayItem);
            }

            // set and display information from where figures are collected
            lblFiguresRootInfo.Text = $"Figurer hentes fra mappe:\n{selectedPathFiguresRootFolder}";

            FigureItem selectedFigureItem = null;

            if (!string.IsNullOrWhiteSpace(selectedDisplayName))
            {
                foreach (FigureItem figureItem in listBoxDisplayItems.Items)
                {                    
                    if (figureItem.Name.Equals(selectedDisplayName))
                    {
                        selectedFigureItem = figureItem;
                        break;                                            
                    }                    
                }
            }

            // check if selected figure is already set, then try to pre selected this view
            // this might occur when any changes of files in figure folder
            if (selectedFigureItem != null)
            {
                listBoxDisplayItems.SelectedItem = selectedFigureItem;
                ReloadHtmlView();
            }
            else
            {
                SetDefaultView();
            }                        
        }


        #region watch dynamic xml file and figures folder for any modifications
        public void WatchDynamicPathFileModifications()
        {
            if (!FigureConfiguration.Instance.IsConfigurationValid(out var errorMessage))
            {
                return;
            }

            var dynamicPathDirectory = FigureConfiguration.Instance.FigurePreview.DynamicPathDirectory;

            if (dynamicPathDirectory == null || string.IsNullOrWhiteSpace(dynamicPathDirectory) || !Directory.Exists(dynamicPathDirectory))
            {
                return;
            }

            watcherDynamicPathFile = new FileSystemWatcher($"{dynamicPathDirectory}");
            watcherDynamicPathFile.NotifyFilter = NotifyFilters.LastWrite;
            watcherDynamicPathFile.Filter = $"{FigureConfiguration.Instance.FigurePreview.DynamicPathFileName}";
            watcherDynamicPathFile.EnableRaisingEvents = true;
            watcherDynamicPathFile.Changed += Watcher_DynamicPathFile;
        }

        public void WatchFiguresFoldersModifications()
        {
            if (!FigureConfiguration.Instance.IsConfigurationValid(out var errorMessage))
            {
                return;
            }

            if (selectedPathFiguresRootFolder == null || string.IsNullOrWhiteSpace(selectedPathFiguresRootFolder) || !Directory.Exists(selectedPathFiguresRootFolder))
            {
                return;
            }

            watcherFiguresFolders?.Dispose();
            watcherFiguresFolders = new FileSystemWatcher($"{selectedPathFiguresRootFolder}");
            watcherFiguresFolders.IncludeSubdirectories = true;
            watcherFiguresFolders.EnableRaisingEvents = true;
            watcherFiguresFolders.Changed += Watcher_WatchFiguresFolderModifications;
            watcherFiguresFolders.Renamed += Watcher_WatchFiguresFolderModifications;
            watcherFiguresFolders.Deleted += Watcher_WatchFiguresFolderModifications;
            watcherFiguresFolders.Created += Watcher_WatchFiguresFolderModifications;
        }

        private void Watcher_WatchFiguresFolderModifications(object sender, FileSystemEventArgs e)
        {
            watcherFiguresFolders.EnableRaisingEvents = false;
            Invoke(new UpdateUI(UpdateUIView), false);
            watcherFiguresFolders.EnableRaisingEvents = true;
        }

        private void Watcher_DynamicPathFile(object sender, FileSystemEventArgs e)
        {
            watcherDynamicPathFile.EnableRaisingEvents = false;
            Invoke(new UpdateUI(UpdateUIView), true);
            WatchFiguresFoldersModifications();
            watcherDynamicPathFile.EnableRaisingEvents = true;
        }

        public delegate void UpdateUI(bool update = false);

        public void UpdateUIView(bool setDynamicPath)
        {
            if (setDynamicPath)
            {
                selectedDisplayName = string.Empty;
                FigureConfiguration.Instance.SetPublicationDynamicPath();
            }

            VerifyConfigurationAndDisplayFigures();
        }
        #endregion

        #region button and selected figures operations
        private void listBoxDisplayItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDisplayItems.SelectedItem != null)
            {
                var displayItem = (FigureItem)listBoxDisplayItems.SelectedItem;
                CreateAndDisplayHtmlFigure(displayItem);
            }
        }



        private void buttonSelectFiguresFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = false;

            folderDlg.SelectedPath = selectedPathFiguresRootFolder;

            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedPathFiguresRootFolder = folderDlg.SelectedPath;
                DisplayFigures();
                WatchFiguresFoldersModifications();
            }
        }
        private void buttonRefreshHtml_Click(object sender, EventArgs e)
        {
            ReloadHtmlView();
        }
        #endregion

        #region html view operations
        private void CreateAndDisplayHtmlFigure(FigureItem displayItem)
        {
            selectedDisplayName = displayItem.Name;
            var viewPath = htmlViewFactory.CreateHtmlViewForFile(displayItem);
            webView2FigureView.Source = new Uri(viewPath);
        }
      
        private void ReloadHtmlView()
        {
            webView2FigureView.Reload();
        }

        private void SetDefaultView()
        {
            string defaultView = htmlViewFactory.GetDefaultView();
            webView2FigureView.Source = new Uri(defaultView);
        }
        #endregion

        #region ui helper functions
        public void ResetErrorMessage()
        {
            lblError.Text = "";
            lblError.Visible = false;
        }

        public void DisplayError(string errorMessage)
        {
            DisableViewComponents();
            var displayError = $"Det har skjedd en feil! {errorMessage}";
            lblError.Visible = true;
            lblError.Text = displayError;
        }

        public void DisableViewComponents()
        {
            lblFiguresRootInfo.Enabled = false;
            listBoxDisplayItems.Enabled = false;
            buttonRefreshHtml.Enabled = false;
            webView2FigureView.Enabled = false;
        }

        public void EnableViewComponents()
        {
            lblFiguresRootInfo.Enabled = true;
            listBoxDisplayItems.Enabled = true;
            buttonRefreshHtml.Enabled = true;
            webView2FigureView.Enabled = true;
            lblError.Visible = false;
        }

        #endregion

        #region init webview,  selectedPathFiguresRootFolder and factories
        private async void PreviewToolForm_Load(object sender, EventArgs e)
        {
            await InitializeAsync();
            if ((webView2FigureView == null) || (webView2FigureView.CoreWebView2 == null))
            {
                Debug.WriteLine("webview not ready");
            }
        }

        private async Task InitializeAsync()
        {
            await webView2FigureView.EnsureCoreWebView2Async(null);
        }
        private void InitializeFactories(bool loadForm = false)
        {
            figureItemFactory = new FigureItemFactory();
            htmlViewFactory = new HtmlViewFactory(loadForm);
        }
        private void SetInitalSelectedPathFigureRootFolder()
        {
            // caluclate selected path to figures
            if (FigureConfiguration.Instance.FigurePreview.DynamicPathFile.Enabled)
            {

                var startPath = FigureConfiguration.Instance.FigurePreview.StartPath;
                var dynamicPublicationPath = FigureConfiguration.Instance.FigurePreview.PublicationDynamicPath?.Text;
                buttonSelectFiguresFolder.Enabled = false;
                selectedPathFiguresRootFolder = $"{startPath}\\{dynamicPublicationPath}";

                if (!Directory.Exists(selectedPathFiguresRootFolder))
                {
                    DisplayError($"Root mappe {selectedPathFiguresRootFolder} finnes ikke");
                    return;
                }
            }
            else
            {
                buttonSelectFiguresFolder.Enabled = true;

                if (string.IsNullOrEmpty(selectedPathFiguresRootFolder))
                {
                    selectedPathFiguresRootFolder = FigureConfiguration.Instance.FigurePreview.StartPath;
                }
            }
        }
        #endregion
    }
}