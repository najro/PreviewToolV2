using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private DisplayItemFactory displayItemFactory;
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
            displayItemFactory = new DisplayItemFactory();
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

        private void PreviewToolForm_Load(object sender, EventArgs e)
        {

        }

        private void listBoxDisplayItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDisplayItems.SelectedItem != null)
            {
                var displayItem = (DisplayItem)listBoxDisplayItems.SelectedItem;
                var viewPath = htmlViewFactory.CreateHtmlViewForFile(displayItem.Name);
            }
           
        }
    }
}