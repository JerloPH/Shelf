
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
            this.btnRefreshItems = new System.Windows.Forms.Button();
            this.btnChangeTachi = new System.Windows.Forms.Button();
            this.lvAnime = new System.Windows.Forms.ListView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpgBasic = new System.Windows.Forms.TabPage();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.tpgTachi = new System.Windows.Forms.TabPage();
            this.tabMedia = new System.Windows.Forms.TabControl();
            this.tpMediaAnime = new System.Windows.Forms.TabPage();
            this.tpMediaManga = new System.Windows.Forms.TabPage();
            this.cbMediaRefresh = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.tpgBasic.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.tpgTachi.SuspendLayout();
            this.tabMedia.SuspendLayout();
            this.tpMediaAnime.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRefresh.Location = new System.Drawing.Point(21, 95);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(242, 52);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh Token";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 293);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(425, 228);
            this.txtLog.TabIndex = 4;
            // 
            // btnFetchMedia
            // 
            this.btnFetchMedia.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFetchMedia.Location = new System.Drawing.Point(143, 102);
            this.btnFetchMedia.Name = "btnFetchMedia";
            this.btnFetchMedia.Size = new System.Drawing.Size(249, 52);
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
            this.cbMedia.Location = new System.Drawing.Point(143, 9);
            this.cbMedia.Name = "cbMedia";
            this.cbMedia.Size = new System.Drawing.Size(249, 39);
            this.cbMedia.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Media:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtUsername.Location = new System.Drawing.Point(143, 56);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(249, 38);
            this.txtUsername.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Username:";
            // 
            // btnChangeConfig
            // 
            this.btnChangeConfig.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangeConfig.Location = new System.Drawing.Point(21, 17);
            this.btnChangeConfig.Name = "btnChangeConfig";
            this.btnChangeConfig.Size = new System.Drawing.Size(242, 52);
            this.btnChangeConfig.TabIndex = 10;
            this.btnChangeConfig.Text = "Update Config";
            this.btnChangeConfig.UseVisualStyleBackColor = true;
            this.btnChangeConfig.Click += new System.EventHandler(this.btnChangeConfig_Click);
            // 
            // btnMALExport
            // 
            this.btnMALExport.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMALExport.Location = new System.Drawing.Point(143, 167);
            this.btnMALExport.Name = "btnMALExport";
            this.btnMALExport.Size = new System.Drawing.Size(249, 52);
            this.btnMALExport.TabIndex = 11;
            this.btnMALExport.Text = "To MAL Export";
            this.btnMALExport.UseVisualStyleBackColor = true;
            this.btnMALExport.Click += new System.EventHandler(this.btnMALExport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tachiyomi Backup File:";
            // 
            // txtTachi
            // 
            this.txtTachi.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTachi.Location = new System.Drawing.Point(10, 54);
            this.txtTachi.Name = "txtTachi";
            this.txtTachi.Size = new System.Drawing.Size(390, 38);
            this.txtTachi.TabIndex = 12;
            // 
            // btnGenTachi
            // 
            this.btnGenTachi.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenTachi.Location = new System.Drawing.Point(10, 175);
            this.btnGenTachi.Name = "btnGenTachi";
            this.btnGenTachi.Size = new System.Drawing.Size(249, 52);
            this.btnGenTachi.TabIndex = 14;
            this.btnGenTachi.Text = "To Tachi Backup";
            this.btnGenTachi.UseVisualStyleBackColor = true;
            this.btnGenTachi.Click += new System.EventHandler(this.btnGenTachi_Click);
            // 
            // btnRefreshItems
            // 
            this.btnRefreshItems.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRefreshItems.Location = new System.Drawing.Point(728, 12);
            this.btnRefreshItems.Name = "btnRefreshItems";
            this.btnRefreshItems.Size = new System.Drawing.Size(170, 51);
            this.btnRefreshItems.TabIndex = 16;
            this.btnRefreshItems.Text = "Refresh";
            this.btnRefreshItems.UseVisualStyleBackColor = true;
            this.btnRefreshItems.Click += new System.EventHandler(this.btnRefreshItems_Click);
            // 
            // btnChangeTachi
            // 
            this.btnChangeTachi.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangeTachi.Location = new System.Drawing.Point(10, 107);
            this.btnChangeTachi.Name = "btnChangeTachi";
            this.btnChangeTachi.Size = new System.Drawing.Size(249, 52);
            this.btnChangeTachi.TabIndex = 17;
            this.btnChangeTachi.Text = "Change Tachi File";
            this.btnChangeTachi.UseVisualStyleBackColor = true;
            this.btnChangeTachi.Click += new System.EventHandler(this.btnChangeTachi_Click);
            // 
            // lvAnime
            // 
            this.lvAnime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAnime.HideSelection = false;
            this.lvAnime.Location = new System.Drawing.Point(3, 3);
            this.lvAnime.Name = "lvAnime";
            this.lvAnime.Size = new System.Drawing.Size(482, 381);
            this.lvAnime.TabIndex = 18;
            this.lvAnime.UseCompatibleStateImageBehavior = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(461, 60);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 31);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Status";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpgBasic);
            this.tabControl.Controls.Add(this.tpgConfig);
            this.tabControl.Controls.Add(this.tpgTachi);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(425, 273);
            this.tabControl.TabIndex = 20;
            // 
            // tpgBasic
            // 
            this.tpgBasic.Controls.Add(this.txtUsername);
            this.tpgBasic.Controls.Add(this.cbMedia);
            this.tpgBasic.Controls.Add(this.label1);
            this.tpgBasic.Controls.Add(this.label2);
            this.tpgBasic.Controls.Add(this.btnFetchMedia);
            this.tpgBasic.Controls.Add(this.btnMALExport);
            this.tpgBasic.Location = new System.Drawing.Point(4, 29);
            this.tpgBasic.Name = "tpgBasic";
            this.tpgBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBasic.Size = new System.Drawing.Size(417, 240);
            this.tpgBasic.TabIndex = 0;
            this.tpgBasic.Text = "Basic";
            this.tpgBasic.UseVisualStyleBackColor = true;
            // 
            // tpgConfig
            // 
            this.tpgConfig.Controls.Add(this.btnChangeConfig);
            this.tpgConfig.Controls.Add(this.btnRefresh);
            this.tpgConfig.Location = new System.Drawing.Point(4, 29);
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpgConfig.Size = new System.Drawing.Size(417, 240);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "Config";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // tpgTachi
            // 
            this.tpgTachi.Controls.Add(this.label3);
            this.tpgTachi.Controls.Add(this.txtTachi);
            this.tpgTachi.Controls.Add(this.btnChangeTachi);
            this.tpgTachi.Controls.Add(this.btnGenTachi);
            this.tpgTachi.Location = new System.Drawing.Point(4, 29);
            this.tpgTachi.Name = "tpgTachi";
            this.tpgTachi.Size = new System.Drawing.Size(417, 240);
            this.tpgTachi.TabIndex = 2;
            this.tpgTachi.Text = "Tachiyomi";
            this.tpgTachi.UseVisualStyleBackColor = true;
            // 
            // tabMedia
            // 
            this.tabMedia.Controls.Add(this.tpMediaAnime);
            this.tabMedia.Controls.Add(this.tpMediaManga);
            this.tabMedia.Location = new System.Drawing.Point(454, 101);
            this.tabMedia.Name = "tabMedia";
            this.tabMedia.SelectedIndex = 0;
            this.tabMedia.Size = new System.Drawing.Size(496, 420);
            this.tabMedia.TabIndex = 21;
            // 
            // tpMediaAnime
            // 
            this.tpMediaAnime.Controls.Add(this.lvAnime);
            this.tpMediaAnime.Location = new System.Drawing.Point(4, 29);
            this.tpMediaAnime.Name = "tpMediaAnime";
            this.tpMediaAnime.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaAnime.Size = new System.Drawing.Size(488, 387);
            this.tpMediaAnime.TabIndex = 0;
            this.tpMediaAnime.Text = "Anime";
            this.tpMediaAnime.UseVisualStyleBackColor = true;
            // 
            // tpMediaManga
            // 
            this.tpMediaManga.Location = new System.Drawing.Point(4, 29);
            this.tpMediaManga.Name = "tpMediaManga";
            this.tpMediaManga.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaManga.Size = new System.Drawing.Size(488, 403);
            this.tpMediaManga.TabIndex = 1;
            this.tpMediaManga.Text = "Manga";
            this.tpMediaManga.UseVisualStyleBackColor = true;
            // 
            // cbMediaRefresh
            // 
            this.cbMediaRefresh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMediaRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbMediaRefresh.FormattingEnabled = true;
            this.cbMediaRefresh.Location = new System.Drawing.Point(458, 12);
            this.cbMediaRefresh.Name = "cbMediaRefresh";
            this.cbMediaRefresh.Size = new System.Drawing.Size(251, 39);
            this.cbMediaRefresh.TabIndex = 12;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 533);
            this.Controls.Add(this.cbMediaRefresh);
            this.Controls.Add(this.tabMedia);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRefreshItems);
            this.Controls.Add(this.txtLog);
            this.MinimumSize = new System.Drawing.Size(980, 580);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.tabControl.ResumeLayout(false);
            this.tpgBasic.ResumeLayout(false);
            this.tpgBasic.PerformLayout();
            this.tpgConfig.ResumeLayout(false);
            this.tpgTachi.ResumeLayout(false);
            this.tpgTachi.PerformLayout();
            this.tabMedia.ResumeLayout(false);
            this.tpMediaAnime.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnRefreshItems;
        private System.Windows.Forms.Button btnChangeTachi;
        private System.Windows.Forms.ListView lvAnime;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpgBasic;
        private System.Windows.Forms.TabPage tpgConfig;
        private System.Windows.Forms.TabPage tpgTachi;
        private System.Windows.Forms.TabControl tabMedia;
        private System.Windows.Forms.TabPage tpMediaAnime;
        private System.Windows.Forms.TabPage tpMediaManga;
        private System.Windows.Forms.ComboBox cbMediaRefresh;
    }
}

