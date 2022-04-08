
namespace FigurePreview
{
    partial class PreviewToolForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewToolForm));
            this.lblFiguresRootInfo = new System.Windows.Forms.Label();
            this.listBoxDisplayItems = new System.Windows.Forms.ListBox();
            this.webView2FigureView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSelectFiguresFolder = new System.Windows.Forms.Button();
            this.buttonRefreshHtml = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView2FigureView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFiguresRootInfo
            // 
            this.lblFiguresRootInfo.AutoSize = true;
            this.lblFiguresRootInfo.Location = new System.Drawing.Point(167, 17);
            this.lblFiguresRootInfo.Name = "lblFiguresRootInfo";
            this.lblFiguresRootInfo.Size = new System.Drawing.Size(92, 13);
            this.lblFiguresRootInfo.TabIndex = 0;
            this.lblFiguresRootInfo.Text = "lblFiguresRootInfo";
            // 
            // listBoxDisplayItems
            // 
            this.listBoxDisplayItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDisplayItems.FormattingEnabled = true;
            this.listBoxDisplayItems.Location = new System.Drawing.Point(12, 54);
            this.listBoxDisplayItems.Name = "listBoxDisplayItems";
            this.listBoxDisplayItems.Size = new System.Drawing.Size(152, 524);
            this.listBoxDisplayItems.TabIndex = 1;
            this.listBoxDisplayItems.SelectedIndexChanged += new System.EventHandler(this.listBoxDisplayItems_SelectedIndexChanged);
            // 
            // webView2FigureView
            // 
            this.webView2FigureView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView2FigureView.CreationProperties = null;
            this.webView2FigureView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2FigureView.Location = new System.Drawing.Point(170, 54);
            this.webView2FigureView.Name = "webView2FigureView";
            this.webView2FigureView.Size = new System.Drawing.Size(949, 524);
            this.webView2FigureView.TabIndex = 2;
            this.webView2FigureView.ZoomFactor = 1D;
            this.webView2FigureView.Click += new System.EventHandler(this.webView2FigureView_Click);
            // 
            // buttonSelectFiguresFolder
            // 
            this.buttonSelectFiguresFolder.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectFiguresFolder.Name = "buttonSelectFiguresFolder";
            this.buttonSelectFiguresFolder.Size = new System.Drawing.Size(147, 23);
            this.buttonSelectFiguresFolder.TabIndex = 3;
            this.buttonSelectFiguresFolder.Text = "Velg rotmappe for figurer";
            this.buttonSelectFiguresFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFiguresFolder.Click += new System.EventHandler(this.buttonSelectFiguresFolder_Click);
            // 
            // buttonRefreshHtml
            // 
            this.buttonRefreshHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshHtml.Location = new System.Drawing.Point(1044, 12);
            this.buttonRefreshHtml.Name = "buttonRefreshHtml";
            this.buttonRefreshHtml.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshHtml.TabIndex = 4;
            this.buttonRefreshHtml.Text = "Refresh";
            this.buttonRefreshHtml.UseVisualStyleBackColor = true;
            this.buttonRefreshHtml.Click += new System.EventHandler(this.buttonRefreshHtml_Click);
            // 
            // PreviewToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 589);
            this.Controls.Add(this.buttonRefreshHtml);
            this.Controls.Add(this.buttonSelectFiguresFolder);
            this.Controls.Add(this.webView2FigureView);
            this.Controls.Add(this.listBoxDisplayItems);
            this.Controls.Add(this.lblFiguresRootInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreviewToolForm";
            this.Text = "Forhåndsvisningsverktøy ";
            this.Load += new System.EventHandler(this.PreviewToolForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView2FigureView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFiguresRootInfo;
        private System.Windows.Forms.ListBox listBoxDisplayItems;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2FigureView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonSelectFiguresFolder;
        private System.Windows.Forms.Button buttonRefreshHtml;
    }
}

