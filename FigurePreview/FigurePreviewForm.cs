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


        //TODO
        // Check config and add good excpetion message
        // diable folder selecin if dynamic i true, otherwise keep it
        // se till att lasta om sida och lägg till listener när dropdown ändras

        private FigureItemFactory figureItemFactory;
        private HtmlViewFactory htmlViewFactory;
        private string selectedPathFiguresRootFolder = FigureConfiguration.Instance.FigurePreview.StartPath;
        private FileSystemWatcher watcherDynamicPathFile;
        private FileSystemWatcher watcherFiguresFolders;

        public PreviewToolForm()
        {
            InitializeComponent();
            LoadConfigurationAndDisplayFigures();
            WatchDynamicPathFileModifications();
            WatchFiguresFoldersModifications();

        }


        public void LoadConfigurationAndDisplayFigures()
        {

            InitializeFactories();

            VerifyConfiguration();

            if (FigureConfiguration.Instance.FigurePreview.DynamicPathFile.Enable)
            {

                var startPath = FigureConfiguration.Instance.FigurePreview.StartPath;
                var dynamicPublicationPath = FigureConfiguration.Instance.FigurePreview.PublicationDynamicPath?.Text;
                buttonSelectFiguresFolder.Enabled = false;

                selectedPathFiguresRootFolder = $"{startPath}\\{dynamicPublicationPath}";
            }
            else
            {
                buttonSelectFiguresFolder.Enabled = true;
                selectedPathFiguresRootFolder = FigureConfiguration.Instance.FigurePreview.StartPath;
            }

            DisplayFigures();
            
        }

        public void WatchDynamicPathFileModifications()
        {
            watcherDynamicPathFile = new FileSystemWatcher($"{FigureConfiguration.Instance.FigurePreview.DynamicPathDirectory}");
            watcherDynamicPathFile.NotifyFilter = NotifyFilters.LastWrite;
            watcherDynamicPathFile.Filter = $"{FigureConfiguration.Instance.FigurePreview.DynamicPathFileName}";
            watcherDynamicPathFile.EnableRaisingEvents = true;
            watcherDynamicPathFile.Changed += Watcher_DynamicPathFile;
        }

        public void WatchFiguresFoldersModifications()
        {
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
            //watcher.Dispose();
            Invoke(new UpdateUI(UpdateUI2), false);
            WatchFiguresFoldersModifications();
        }

        private void Watcher_DynamicPathFile(object sender, FileSystemEventArgs e)
        {
            //watcher.Dispose();
            Invoke(new UpdateUI(UpdateUI2), true);
            WatchFiguresFoldersModifications();
        }

        public delegate void UpdateUI(bool update = false);

        public void UpdateUI2(bool setDynamicPath)
        {
            if (setDynamicPath) {
                FigureConfiguration.Instance.SetPublicationDynamicPath();
            }
            
            LoadConfigurationAndDisplayFigures();
            //WatchDynamicPathFileModifications();
        }

        private void SetDefaultView()
        {
            string defaultView = htmlViewFactory.GetDefaultView();
            webView2FigureView.Source = new Uri(defaultView);
        }

      
        private void InitializeFactories()
        {
            figureItemFactory = new FigureItemFactory();
            htmlViewFactory = new HtmlViewFactory();
        }

        private void VerifyConfiguration()
        {
            var errorMessage = "";
            try
            {
                if (!FigureConfiguration.Instance.IsConfigurationValid(out errorMessage))
                {
                    MessageBox.Show(errorMessage, "Feil på konfigurasjonsfil");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Feil på konfigurasjonsfil");
            }
        }

        private void DisplayFigures()
        {
            htmlViewFactory.CleanUpViewFiles();

            var list = figureItemFactory.GetDisplayItems(selectedPathFiguresRootFolder);

            listBoxDisplayItems.Items.Clear();
            listBoxDisplayItems.DisplayMember = "Name";

            foreach (var displayItem in list)
            {
                listBoxDisplayItems.Items.Add(displayItem);
            }

            lblFiguresRootInfo.Text = $"Figurer hentes fra mappe : {selectedPathFiguresRootFolder}";

            SetDefaultView();

        }

        private async void PreviewToolForm_Load(object sender, EventArgs e)
        {
            await InitializeAsync();

            if ((webView2FigureView == null) || (webView2FigureView.CoreWebView2 == null))
            {
                Debug.WriteLine("webview not ready");
            }

            //webView2FigureView.Source = new Uri(Directory.GetCurrentDirectory() + $"\\htmlview\\view\\default.htm");
        }

        private async Task InitializeAsync()
        {
            await webView2FigureView.EnsureCoreWebView2Async(null);
        }


        private void listBoxDisplayItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDisplayItems.SelectedItem != null)
            {
                var displayItem = (FigureItem)listBoxDisplayItems.SelectedItem;
                var viewPath = htmlViewFactory.CreateHtmlViewForFile(displayItem);
                webView2FigureView.Source = new Uri(viewPath);
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
            }
        }

        private void buttonRefreshHtml_Click(object sender, EventArgs e)
        {
            webView2FigureView.Reload();
        }
    }
}