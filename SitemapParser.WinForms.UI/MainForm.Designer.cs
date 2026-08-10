
namespace SitemapParser.WInForms.UI {
    partial class MainForm
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
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbUrls = new System.Windows.Forms.ComboBox();
            this.lblHttpStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(177, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbUrls
            // 
            this.cmbUrls.FormattingEnabled = true;
            this.cmbUrls.Location = new System.Drawing.Point(258, 12);
            this.cmbUrls.Name = "cmbUrls";
            this.cmbUrls.Size = new System.Drawing.Size(530, 21);
            this.cmbUrls.TabIndex = 3;
            // 
            // lblHttpStatus
            // 
            this.lblHttpStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHttpStatus.Location = new System.Drawing.Point(19, 36);
            this.lblHttpStatus.Name = "lblHttpStatus";
            this.lblHttpStatus.Size = new System.Drawing.Size(769, 21);
            this.lblHttpStatus.TabIndex = 4;
            this.lblHttpStatus.Text = "httpStatus";
            this.lblHttpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblHttpStatus);
            this.Controls.Add(this.cmbUrls);
            this.Controls.Add(this.btnGo);
            this.Name = "MainForm";
            this.Text = "Sitemap Parser";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cmbUrls;
        private System.Windows.Forms.Label lblHttpStatus;
    }
}

