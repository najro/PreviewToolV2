
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
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.webView2FigureView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFiguresRootInfo
            // 
            this.lblFiguresRootInfo.AutoSize = true;
            this.lblFiguresRootInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiguresRootInfo.Location = new System.Drawing.Point(296, 18);
            this.lblFiguresRootInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiguresRootInfo.Name = "lblFiguresRootInfo";
            this.lblFiguresRootInfo.Size = new System.Drawing.Size(173, 22);
            this.lblFiguresRootInfo.TabIndex = 0;
            this.lblFiguresRootInfo.Text = "lblFiguresRootInfo";
            // 
            // listBoxDisplayItems
            // 
            this.listBoxDisplayItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDisplayItems.FormattingEnabled = true;
            this.listBoxDisplayItems.ItemHeight = 20;
            this.listBoxDisplayItems.Location = new System.Drawing.Point(18, 97);
            this.listBoxDisplayItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxDisplayItems.Name = "listBoxDisplayItems";
            this.listBoxDisplayItems.Size = new System.Drawing.Size(274, 804);
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
            this.webView2FigureView.Location = new System.Drawing.Point(300, 97);
            this.webView2FigureView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.webView2FigureView.Name = "webView2FigureView";
            this.webView2FigureView.Size = new System.Drawing.Size(1379, 804);
            this.webView2FigureView.TabIndex = 2;
            this.webView2FigureView.ZoomFactor = 1D;
            // 
            // buttonSelectFiguresFolder
            // 
            this.buttonSelectFiguresFolder.Enabled = false;
            this.buttonSelectFiguresFolder.Location = new System.Drawing.Point(18, 18);
            this.buttonSelectFiguresFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSelectFiguresFolder.Name = "buttonSelectFiguresFolder";
            this.buttonSelectFiguresFolder.Size = new System.Drawing.Size(200, 35);
            this.buttonSelectFiguresFolder.TabIndex = 3;
            this.buttonSelectFiguresFolder.Text = "Velg rotmappe for figurer";
            this.buttonSelectFiguresFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFiguresFolder.Click += new System.EventHandler(this.buttonSelectFiguresFolder_Click);
            // 
            // buttonRefreshHtml
            // 
            this.buttonRefreshHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshHtml.Location = new System.Drawing.Point(1566, 18);
            this.buttonRefreshHtml.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRefreshHtml.Name = "buttonRefreshHtml";
            this.buttonRefreshHtml.Size = new System.Drawing.Size(112, 35);
            this.buttonRefreshHtml.TabIndex = 4;
            this.buttonRefreshHtml.Text = "Refresh";
            this.buttonRefreshHtml.UseVisualStyleBackColor = true;
            this.buttonRefreshHtml.Click += new System.EventHandler(this.buttonRefreshHtml_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Red;
            this.lblError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(18, 18);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblError.Name = "lblError";
            this.lblError.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.lblError.Size = new System.Drawing.Size(30, 50);
            this.lblError.TabIndex = 5;
            // 
            // PreviewToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 906);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.buttonRefreshHtml);
            this.Controls.Add(this.buttonSelectFiguresFolder);
            this.Controls.Add(this.webView2FigureView);
            this.Controls.Add(this.listBoxDisplayItems);
            this.Controls.Add(this.lblFiguresRootInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.Label lblError;
    }
}

