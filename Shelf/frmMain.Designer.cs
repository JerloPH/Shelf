
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
            this.cbReplaceTachiLib = new System.Windows.Forms.CheckBox();
            this.tpgLocal = new System.Windows.Forms.TabPage();
            this.tabLocalMedia = new System.Windows.Forms.TabControl();
            this.tpgLocalAnime = new System.Windows.Forms.TabPage();
            this.tpgLocalManga = new System.Windows.Forms.TabPage();
            this.btnLocalMangaAdd = new System.Windows.Forms.Button();
            this.gridPathLocalManga = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.tabMedia = new System.Windows.Forms.TabControl();
            this.tpMediaAnime = new System.Windows.Forms.TabPage();
            this.tpMediaManga = new System.Windows.Forms.TabPage();
            this.lvManga = new System.Windows.Forms.ListView();
            this.tpMediaTachi = new System.Windows.Forms.TabPage();
            this.lvTachi = new System.Windows.Forms.ListView();
            this.tpMediaLocalAnime = new System.Windows.Forms.TabPage();
            this.tpMediaLocalManga = new System.Windows.Forms.TabPage();
            this.cbMediaRefresh = new System.Windows.Forms.ComboBox();
            this.cbEntryMode = new System.Windows.Forms.ComboBox();
            this.lvLocalManga = new System.Windows.Forms.ListView();
            this.tabControl.SuspendLayout();
            this.tpgBasic.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.tpgTachi.SuspendLayout();
            this.tpgLocal.SuspendLayout();
            this.tabLocalMedia.SuspendLayout();
            this.tpgLocalManga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPathLocalManga)).BeginInit();
            this.tabMedia.SuspendLayout();
            this.tpMediaAnime.SuspendLayout();
            this.tpMediaManga.SuspendLayout();
            this.tpMediaTachi.SuspendLayout();
            this.tpMediaLocalManga.SuspendLayout();
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
            this.txtLog.Location = new System.Drawing.Point(12, 325);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(425, 196);
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
            this.btnMALExport.Location = new System.Drawing.Point(143, 160);
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
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tachiyomi Backup File:";
            // 
            // txtTachi
            // 
            this.txtTachi.AllowDrop = true;
            this.txtTachi.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTachi.Location = new System.Drawing.Point(10, 34);
            this.txtTachi.Name = "txtTachi";
            this.txtTachi.Size = new System.Drawing.Size(390, 38);
            this.txtTachi.TabIndex = 12;
            this.txtTachi.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtTachi_DragDrop);
            this.txtTachi.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtTachi_DragEnter);
            // 
            // btnGenTachi
            // 
            this.btnGenTachi.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenTachi.Location = new System.Drawing.Point(10, 136);
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
            this.btnRefreshItems.Location = new System.Drawing.Point(861, 12);
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
            this.btnChangeTachi.Location = new System.Drawing.Point(10, 78);
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
            this.lvAnime.Size = new System.Drawing.Size(563, 381);
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
            this.tabControl.AllowDrop = true;
            this.tabControl.Controls.Add(this.tpgBasic);
            this.tabControl.Controls.Add(this.tpgConfig);
            this.tabControl.Controls.Add(this.tpgTachi);
            this.tabControl.Controls.Add(this.tpgLocal);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(425, 307);
            this.tabControl.TabIndex = 20;
            this.tabControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabControl_DragEnter);
            // 
            // tpgBasic
            // 
            this.tpgBasic.AllowDrop = true;
            this.tpgBasic.Controls.Add(this.txtUsername);
            this.tpgBasic.Controls.Add(this.cbMedia);
            this.tpgBasic.Controls.Add(this.label1);
            this.tpgBasic.Controls.Add(this.label2);
            this.tpgBasic.Controls.Add(this.btnFetchMedia);
            this.tpgBasic.Controls.Add(this.btnMALExport);
            this.tpgBasic.Location = new System.Drawing.Point(4, 29);
            this.tpgBasic.Name = "tpgBasic";
            this.tpgBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBasic.Size = new System.Drawing.Size(417, 274);
            this.tpgBasic.TabIndex = 0;
            this.tpgBasic.Text = "Basic";
            this.tpgBasic.UseVisualStyleBackColor = true;
            // 
            // tpgConfig
            // 
            this.tpgConfig.AllowDrop = true;
            this.tpgConfig.Controls.Add(this.btnChangeConfig);
            this.tpgConfig.Controls.Add(this.btnRefresh);
            this.tpgConfig.Location = new System.Drawing.Point(4, 29);
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpgConfig.Size = new System.Drawing.Size(417, 274);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "Config";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // tpgTachi
            // 
            this.tpgTachi.AllowDrop = true;
            this.tpgTachi.Controls.Add(this.cbReplaceTachiLib);
            this.tpgTachi.Controls.Add(this.label3);
            this.tpgTachi.Controls.Add(this.txtTachi);
            this.tpgTachi.Controls.Add(this.btnChangeTachi);
            this.tpgTachi.Controls.Add(this.btnGenTachi);
            this.tpgTachi.Location = new System.Drawing.Point(4, 29);
            this.tpgTachi.Name = "tpgTachi";
            this.tpgTachi.Size = new System.Drawing.Size(417, 274);
            this.tpgTachi.TabIndex = 2;
            this.tpgTachi.Text = "Tachiyomi";
            this.tpgTachi.UseVisualStyleBackColor = true;
            this.tpgTachi.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpgTachi_DragDrop);
            this.tpgTachi.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpgTachi_DragEnter);
            // 
            // cbReplaceTachiLib
            // 
            this.cbReplaceTachiLib.AutoSize = true;
            this.cbReplaceTachiLib.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbReplaceTachiLib.Location = new System.Drawing.Point(10, 194);
            this.cbReplaceTachiLib.Name = "cbReplaceTachiLib";
            this.cbReplaceTachiLib.Size = new System.Drawing.Size(372, 32);
            this.cbReplaceTachiLib.TabIndex = 18;
            this.cbReplaceTachiLib.Text = "Replace Library with Generated Backup";
            this.cbReplaceTachiLib.UseVisualStyleBackColor = true;
            // 
            // tpgLocal
            // 
            this.tpgLocal.Controls.Add(this.tabLocalMedia);
            this.tpgLocal.Location = new System.Drawing.Point(4, 29);
            this.tpgLocal.Name = "tpgLocal";
            this.tpgLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLocal.Size = new System.Drawing.Size(417, 274);
            this.tpgLocal.TabIndex = 3;
            this.tpgLocal.Text = "Local Media";
            this.tpgLocal.UseVisualStyleBackColor = true;
            // 
            // tabLocalMedia
            // 
            this.tabLocalMedia.Controls.Add(this.tpgLocalAnime);
            this.tabLocalMedia.Controls.Add(this.tpgLocalManga);
            this.tabLocalMedia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLocalMedia.Location = new System.Drawing.Point(3, 3);
            this.tabLocalMedia.Name = "tabLocalMedia";
            this.tabLocalMedia.SelectedIndex = 0;
            this.tabLocalMedia.Size = new System.Drawing.Size(411, 268);
            this.tabLocalMedia.TabIndex = 0;
            // 
            // tpgLocalAnime
            // 
            this.tpgLocalAnime.Location = new System.Drawing.Point(4, 29);
            this.tpgLocalAnime.Name = "tpgLocalAnime";
            this.tpgLocalAnime.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLocalAnime.Size = new System.Drawing.Size(403, 235);
            this.tpgLocalAnime.TabIndex = 0;
            this.tpgLocalAnime.Text = "Anime";
            this.tpgLocalAnime.UseVisualStyleBackColor = true;
            // 
            // tpgLocalManga
            // 
            this.tpgLocalManga.Controls.Add(this.btnLocalMangaAdd);
            this.tpgLocalManga.Controls.Add(this.gridPathLocalManga);
            this.tpgLocalManga.Controls.Add(this.label4);
            this.tpgLocalManga.Location = new System.Drawing.Point(4, 29);
            this.tpgLocalManga.Name = "tpgLocalManga";
            this.tpgLocalManga.Size = new System.Drawing.Size(403, 235);
            this.tpgLocalManga.TabIndex = 1;
            this.tpgLocalManga.Text = "Manga";
            this.tpgLocalManga.UseVisualStyleBackColor = true;
            // 
            // btnLocalMangaAdd
            // 
            this.btnLocalMangaAdd.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLocalMangaAdd.Location = new System.Drawing.Point(216, 3);
            this.btnLocalMangaAdd.Name = "btnLocalMangaAdd";
            this.btnLocalMangaAdd.Size = new System.Drawing.Size(170, 48);
            this.btnLocalMangaAdd.TabIndex = 23;
            this.btnLocalMangaAdd.Text = "Add";
            this.btnLocalMangaAdd.UseVisualStyleBackColor = true;
            this.btnLocalMangaAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gridPathLocalManga
            // 
            this.gridPathLocalManga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPathLocalManga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPathLocalManga.Location = new System.Drawing.Point(3, 57);
            this.gridPathLocalManga.Name = "gridPathLocalManga";
            this.gridPathLocalManga.RowHeadersWidth = 51;
            this.gridPathLocalManga.RowTemplate.Height = 29;
            this.gridPathLocalManga.Size = new System.Drawing.Size(383, 175);
            this.gridPathLocalManga.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 31);
            this.label4.TabIndex = 14;
            this.label4.Text = "Paths: ";
            // 
            // tabMedia
            // 
            this.tabMedia.Controls.Add(this.tpMediaAnime);
            this.tabMedia.Controls.Add(this.tpMediaManga);
            this.tabMedia.Controls.Add(this.tpMediaTachi);
            this.tabMedia.Controls.Add(this.tpMediaLocalAnime);
            this.tabMedia.Controls.Add(this.tpMediaLocalManga);
            this.tabMedia.Location = new System.Drawing.Point(454, 101);
            this.tabMedia.Name = "tabMedia";
            this.tabMedia.SelectedIndex = 0;
            this.tabMedia.Size = new System.Drawing.Size(577, 420);
            this.tabMedia.TabIndex = 21;
            // 
            // tpMediaAnime
            // 
            this.tpMediaAnime.Controls.Add(this.lvAnime);
            this.tpMediaAnime.Location = new System.Drawing.Point(4, 29);
            this.tpMediaAnime.Name = "tpMediaAnime";
            this.tpMediaAnime.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaAnime.Size = new System.Drawing.Size(569, 387);
            this.tpMediaAnime.TabIndex = 0;
            this.tpMediaAnime.Text = "Anime";
            this.tpMediaAnime.UseVisualStyleBackColor = true;
            // 
            // tpMediaManga
            // 
            this.tpMediaManga.Controls.Add(this.lvManga);
            this.tpMediaManga.Location = new System.Drawing.Point(4, 29);
            this.tpMediaManga.Name = "tpMediaManga";
            this.tpMediaManga.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaManga.Size = new System.Drawing.Size(569, 387);
            this.tpMediaManga.TabIndex = 1;
            this.tpMediaManga.Text = "Manga";
            this.tpMediaManga.UseVisualStyleBackColor = true;
            // 
            // lvManga
            // 
            this.lvManga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvManga.HideSelection = false;
            this.lvManga.Location = new System.Drawing.Point(3, 3);
            this.lvManga.Name = "lvManga";
            this.lvManga.Size = new System.Drawing.Size(563, 381);
            this.lvManga.TabIndex = 19;
            this.lvManga.UseCompatibleStateImageBehavior = false;
            // 
            // tpMediaTachi
            // 
            this.tpMediaTachi.Controls.Add(this.lvTachi);
            this.tpMediaTachi.Location = new System.Drawing.Point(4, 29);
            this.tpMediaTachi.Name = "tpMediaTachi";
            this.tpMediaTachi.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaTachi.Size = new System.Drawing.Size(569, 387);
            this.tpMediaTachi.TabIndex = 2;
            this.tpMediaTachi.Text = "Tachiyomi";
            this.tpMediaTachi.UseVisualStyleBackColor = true;
            // 
            // lvTachi
            // 
            this.lvTachi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTachi.HideSelection = false;
            this.lvTachi.Location = new System.Drawing.Point(3, 3);
            this.lvTachi.Name = "lvTachi";
            this.lvTachi.Size = new System.Drawing.Size(563, 381);
            this.lvTachi.TabIndex = 20;
            this.lvTachi.UseCompatibleStateImageBehavior = false;
            // 
            // tpMediaLocalAnime
            // 
            this.tpMediaLocalAnime.Location = new System.Drawing.Point(4, 29);
            this.tpMediaLocalAnime.Name = "tpMediaLocalAnime";
            this.tpMediaLocalAnime.Size = new System.Drawing.Size(569, 387);
            this.tpMediaLocalAnime.TabIndex = 3;
            this.tpMediaLocalAnime.Text = "Local Anime";
            this.tpMediaLocalAnime.UseVisualStyleBackColor = true;
            // 
            // tpMediaLocalManga
            // 
            this.tpMediaLocalManga.Controls.Add(this.lvLocalManga);
            this.tpMediaLocalManga.Location = new System.Drawing.Point(4, 29);
            this.tpMediaLocalManga.Name = "tpMediaLocalManga";
            this.tpMediaLocalManga.Size = new System.Drawing.Size(569, 387);
            this.tpMediaLocalManga.TabIndex = 4;
            this.tpMediaLocalManga.Text = "Local Manga";
            this.tpMediaLocalManga.UseVisualStyleBackColor = true;
            // 
            // cbMediaRefresh
            // 
            this.cbMediaRefresh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMediaRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbMediaRefresh.FormattingEnabled = true;
            this.cbMediaRefresh.Location = new System.Drawing.Point(458, 12);
            this.cbMediaRefresh.Name = "cbMediaRefresh";
            this.cbMediaRefresh.Size = new System.Drawing.Size(202, 39);
            this.cbMediaRefresh.TabIndex = 12;
            // 
            // cbEntryMode
            // 
            this.cbEntryMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntryMode.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbEntryMode.FormattingEnabled = true;
            this.cbEntryMode.Location = new System.Drawing.Point(666, 12);
            this.cbEntryMode.Name = "cbEntryMode";
            this.cbEntryMode.Size = new System.Drawing.Size(179, 39);
            this.cbEntryMode.TabIndex = 22;
            // 
            // lvLocalManga
            // 
            this.lvLocalManga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLocalManga.HideSelection = false;
            this.lvLocalManga.Location = new System.Drawing.Point(0, 0);
            this.lvLocalManga.Name = "lvLocalManga";
            this.lvLocalManga.Size = new System.Drawing.Size(569, 387);
            this.lvLocalManga.TabIndex = 21;
            this.lvLocalManga.UseCompatibleStateImageBehavior = false;
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 533);
            this.Controls.Add(this.cbEntryMode);
            this.Controls.Add(this.cbMediaRefresh);
            this.Controls.Add(this.tabMedia);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRefreshItems);
            this.Controls.Add(this.txtLog);
            this.MinimumSize = new System.Drawing.Size(1061, 580);
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
            this.tpgLocal.ResumeLayout(false);
            this.tabLocalMedia.ResumeLayout(false);
            this.tpgLocalManga.ResumeLayout(false);
            this.tpgLocalManga.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPathLocalManga)).EndInit();
            this.tabMedia.ResumeLayout(false);
            this.tpMediaAnime.ResumeLayout(false);
            this.tpMediaManga.ResumeLayout(false);
            this.tpMediaTachi.ResumeLayout(false);
            this.tpMediaLocalManga.ResumeLayout(false);
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
        private System.Windows.Forms.ListView lvManga;
        private System.Windows.Forms.TabPage tpMediaTachi;
        private System.Windows.Forms.TabPage tpMediaLocalAnime;
        private System.Windows.Forms.TabPage tpMediaLocalManga;
        private System.Windows.Forms.ListView lvTachi;
        private System.Windows.Forms.CheckBox cbReplaceTachiLib;
        private System.Windows.Forms.ComboBox cbEntryMode;
        private System.Windows.Forms.TabPage tpgLocal;
        private System.Windows.Forms.TabControl tabLocalMedia;
        private System.Windows.Forms.TabPage tpgLocalAnime;
        private System.Windows.Forms.TabPage tpgLocalManga;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridPathLocalManga;
        private System.Windows.Forms.Button btnLocalMangaAdd;
        private System.Windows.Forms.ListView lvLocalManga;
    }
}

