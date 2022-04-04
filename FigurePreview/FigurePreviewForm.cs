using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using FigurePreview.Configuration;
using FigurePreview.Factories;
using FigurePreview.Models;

namespace FigurePreview
{
    public partial class PreviewToolForm : Form
    {
        private FigureItemFactory figureItemFactory;
        private HtmlViewFactory htmlViewFactory;
        private string selectedPathFiguresRootFolder = FigureConfiguration.Instance.FigurePreview.StartPath;

        public PreviewToolForm()
        {
            InitializeComponent();
            InitializeFactories();
            VerifyConfiguration();
            DisplayFigures();
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
            var list = figureItemFactory.GetDisplayItems(selectedPathFiguresRootFolder);

            listBoxDisplayItems.Items.Clear();
            listBoxDisplayItems.DisplayMember = "Name";

            foreach (var displayItem in list)
            {
                listBoxDisplayItems.Items.Add(displayItem);
            }

            lblFiguresRootInfo.Text = $"Figurer hentes fra mappe : {selectedPathFiguresRootFolder}";
        }

        private async void PreviewToolForm_Load(object sender, EventArgs e)
        {
            await InitializeAsync();
            
            if ((webView2FigureView == null) || (webView2FigureView.CoreWebView2 == null))
            {
                Debug.WriteLine("webview not ready");
            }

            webView2FigureView.Source = new Uri(Directory.GetCurrentDirectory() + $"\\htmlview\\view\\default.htm");

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