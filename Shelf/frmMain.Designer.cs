﻿
namespace Shelf
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnFetchMedia = new System.Windows.Forms.Button();
            this.cbMedia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeConfig = new System.Windows.Forms.Button();
            this.btnMALExport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTachi = new System.Windows.Forms.TextBox();
            this.btnGenTachi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(254, 52);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh Token";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 78);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(459, 441);
            this.txtLog.TabIndex = 4;
            // 
            // btnFetchMedia
            // 
            this.btnFetchMedia.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFetchMedia.Location = new System.Drawing.Point(287, 12);
            this.btnFetchMedia.Name = "btnFetchMedia";
            this.btnFetchMedia.Size = new System.Drawing.Size(254, 52);
            this.btnFetchMedia.TabIndex = 5;
            this.btnFetchMedia.Text = "Fetch Media";
            this.btnFetchMedia.UseVisualStyleBackColor = true;
            this.btnFetchMedia.Click += new System.EventHandler(this.btnFetchMedia_Click);
            // 
            // cbMedia
            // 
            this.cbMedia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMedia.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbMedia.FormattingEnabled = true;
            this.cbMedia.Location = new System.Drawing.Point(610, 93);
            this.cbMedia.Name = "cbMedia";
            this.cbMedia.Size = new System.Drawing.Size(249, 39);
            this.cbMedia.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(477, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Media:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtUsername.Location = new System.Drawing.Point(610, 140);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(249, 38);
            this.txtUsername.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(477, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Username:";
            // 
            // btnChangeConfig
            // 
            this.btnChangeConfig.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangeConfig.Location = new System.Drawing.Point(571, 12);
            this.btnChangeConfig.Name = "btnChangeConfig";
            this.btnChangeConfig.Size = new System.Drawing.Size(254, 52);
            this.btnChangeConfig.TabIndex = 10;
            this.btnChangeConfig.Text = "Update Config";
            this.btnChangeConfig.UseVisualStyleBackColor = true;
            this.btnChangeConfig.Click += new System.EventHandler(this.btnChangeConfig_Click);
            // 
            // btnMALExport
            // 
            this.btnMALExport.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMALExport.Location = new System.Drawing.Point(477, 219);
            this.btnMALExport.Name = "btnMALExport";
            this.btnMALExport.Size = new System.Drawing.Size(254, 52);
            this.btnMALExport.TabIndex = 11;
            this.btnMALExport.Text = "To MAL Export";
            this.btnMALExport.UseVisualStyleBackColor = true;
            this.btnMALExport.Click += new System.EventHandler(this.btnMALExport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(477, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tachi File:";
            // 
            // txtTachi
            // 
            this.txtTachi.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTachi.Location = new System.Drawing.Point(610, 312);
            this.txtTachi.Name = "txtTachi";
            this.txtTachi.Size = new System.Drawing.Size(249, 38);
            this.txtTachi.TabIndex = 12;
            // 
            // btnGenTachi
            // 
            this.btnGenTachi.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenTachi.Location = new System.Drawing.Point(477, 378);
            this.btnGenTachi.Name = "btnGenTachi";
            this.btnGenTachi.Size = new System.Drawing.Size(254, 52);
            this.btnGenTachi.TabIndex = 14;
            this.btnGenTachi.Text = "To Tachi Backup";
            this.btnGenTachi.UseVisualStyleBackColor = true;
            this.btnGenTachi.Click += new System.EventHandler(this.btnGenTachi_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 531);
            this.Controls.Add(this.btnGenTachi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTachi);
            this.Controls.Add(this.btnMALExport);
            this.Controls.Add(this.btnChangeConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMedia);
            this.Controls.Add(this.btnFetchMedia);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnRefresh);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnFetchMedia;
        private System.Windows.Forms.ComboBox cbMedia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangeConfig;
        private System.Windows.Forms.Button btnMALExport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTachi;
        private System.Windows.Forms.Button btnGenTachi;
    }
}

