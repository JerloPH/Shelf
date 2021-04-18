
using System.Windows.Forms;

namespace Shelf
{
    partial class frmGetAuthCode
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
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.CreationProperties = null;
            this.webView.Location = new System.Drawing.Point(12, 44);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(928, 444);
            this.webView.TabIndex = 0;
            this.webView.Text = "webView21";
            this.webView.ZoomFactor = 1D;
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAuthCode.Location = new System.Drawing.Point(12, 494);
            this.txtAuthCode.Multiline = true;
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(775, 99);
            this.txtAuthCode.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(794, 494);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(146, 99);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Log in your Anilist, authorize, and copy code to the textbox below";
            // 
            // frmGetAccessTkn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 596);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAuthCode);
            this.Controls.Add(this.webView);
            this.MinimizeBox = false;
            this.Name = "frmGetAccessTkn";
            this.Text = "frmGetAccessTkn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGetAccessTkn_FormClosing);
            this.Resize += new System.EventHandler(this.frmGetAccessTkn_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
    }
}