
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxDisplayItems = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hent filer fra";
            // 
            // listBoxDisplayItems
            // 
            this.listBoxDisplayItems.FormattingEnabled = true;
            this.listBoxDisplayItems.Location = new System.Drawing.Point(32, 49);
            this.listBoxDisplayItems.Name = "listBoxDisplayItems";
            this.listBoxDisplayItems.Size = new System.Drawing.Size(120, 433);
            this.listBoxDisplayItems.TabIndex = 1;
            this.listBoxDisplayItems.SelectedIndexChanged += new System.EventHandler(this.listBoxDisplayItems_SelectedIndexChanged);
            // 
            // PreviewToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 589);
            this.Controls.Add(this.listBoxDisplayItems);
            this.Controls.Add(this.label1);
            this.Name = "PreviewToolForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PreviewToolForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxDisplayItems;
    }
}

