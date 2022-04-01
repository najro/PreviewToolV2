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

namespace FigurePreview
{
    public partial class PreviewToolForm : Form
    {
        public PreviewToolForm()
        {
            InitializeComponent();
            VerifyConfiguration();
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

        private void PreviewToolForm_Load(object sender, EventArgs e)
        {

        }
    }
}