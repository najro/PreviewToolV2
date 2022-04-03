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
        private FigureItemFactory displayItemFactory;
        private HtmlViewFactory htmlViewFactory;

        public PreviewToolForm()
        {
            InitializeComponent();
            InitializeFactories();
            VerifyConfiguration();
            LoadDisplayItems();
        }

        private void InitializeFactories()
        {
            displayItemFactory = new FigureItemFactory();
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
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Feil på konfigurasjonsfil");
            }
        }

        private void LoadDisplayItems()
        {
            var list = displayItemFactory.GetDisplayItems();


            listBoxDisplayItems.DisplayMember = "Name";

            foreach (var displayItem in list)
            {
                listBoxDisplayItems.Items.Add(displayItem);
            }
        }

        private async void PreviewToolForm_Load(object sender, EventArgs e)
        {
            webView2FigureView.CoreWebView2InitializationCompleted += WebView2FigureView_CoreWebView2InitializationCompleted;

            Debug.WriteLine("before InitializeAsync");
            await InitializeAsync();
            Debug.WriteLine("after InitializeAsync");

            if ((webView2FigureView == null) || (webView2FigureView.CoreWebView2 == null))
            {
                Debug.WriteLine("webview not ready");
            }

            webView2FigureView.Source = new Uri(Directory.GetCurrentDirectory() + $"\\htmlview\\view\\default.htm");

        }

        private async Task InitializeAsync()
        {
            Debug.WriteLine("InitializeAsync");
            await webView2FigureView.EnsureCoreWebView2Async(null);
            Debug.WriteLine("WebView2 Runtime version: " + webView2FigureView.CoreWebView2.Environment.BrowserVersionString);
        }

        private void WebView2FigureView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)

        {
            Debug.WriteLine("WebView_CoreWebView2InitializationCompleted");
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
    }
}