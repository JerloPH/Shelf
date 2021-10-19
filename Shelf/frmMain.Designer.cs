
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
            this.lblTachiBackupFile = new System.Windows.Forms.Label();
            this.btnGenTachi = new System.Windows.Forms.Button();
            this.btnRefreshItems = new System.Windows.Forms.Button();
            this.btnAddTachiBackup = new System.Windows.Forms.Button();
            this.lvAnime = new System.Windows.Forms.ListView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpgBasic = new System.Windows.Forms.TabPage();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.tpgTachi = new System.Windows.Forms.TabPage();
            this.panelTachiSkip = new System.Windows.Forms.Panel();
            this.cblistTachiSkip = new System.Windows.Forms.CheckedListBox();
            this.panelTachi = new System.Windows.Forms.Panel();
            this.cmbTachiBackup = new System.Windows.Forms.ComboBox();
            this.cbReplaceTachiLib = new System.Windows.Forms.CheckBox();
            this.tpgLocal = new System.Windows.Forms.TabPage();
            this.btnLocalMangaAdd = new System.Windows.Forms.Button();
            this.gridPathLocal = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.tabMedia = new System.Windows.Forms.TabControl();
            this.tpMediaAnime = new System.Windows.Forms.TabPage();
            this.tpMediaManga = new System.Windows.Forms.TabPage();
            this.lvManga = new System.Windows.Forms.ListView();
            this.tpMediaTachi = new System.Windows.Forms.TabPage();
            this.lvTachi = new System.Windows.Forms.ListView();
            this.tpMediaLocalAnime = new System.Windows.Forms.TabPage();
            this.lvLocalAnime = new System.Windows.Forms.ListView();
            this.tpMediaLocalManga = new System.Windows.Forms.TabPage();
            this.lvLocalManga = new System.Windows.Forms.ListView();
            this.cbMediaRefresh = new System.Windows.Forms.ComboBox();
            this.cbEntryMode = new System.Windows.Forms.ComboBox();
            this.btnTachiRefresh = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tpgBasic.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.tpgTachi.SuspendLayout();
            this.panelTachiSkip.SuspendLayout();
            this.panelTachi.SuspendLayout();
            this.tpgLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPathLocal)).BeginInit();
            this.tabMedia.SuspendLayout();
            this.tpMediaAnime.SuspendLayout();
            this.tpMediaManga.SuspendLayout();
            this.tpMediaTachi.SuspendLayout();
            this.tpMediaLocalAnime.SuspendLayout();
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
            this.txtLog.Location = new System.Drawing.Point(12, 357);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(425, 164);
            this.txtLog.TabIndex = 4;
            // 
            // btnFetchMedia
            // 
            this.btnFetchMedia.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            this.btnMALExport.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMALExport.Location = new System.Drawing.Point(143, 160);
            this.btnMALExport.Name = "btnMALExport";
            this.btnMALExport.Size = new System.Drawing.Size(249, 52);
            this.btnMALExport.TabIndex = 11;
            this.btnMALExport.Text = "Export MAL";
            this.btnMALExport.UseVisualStyleBackColor = true;
            this.btnMALExport.Click += new System.EventHandler(this.btnMALExport_Click);
            // 
            // lblTachiBackupFile
            // 
            this.lblTachiBackupFile.AutoSize = true;
            this.lblTachiBackupFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTachiBackupFile.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTachiBackupFile.Location = new System.Drawing.Point(0, 0);
            this.lblTachiBackupFile.Name = "lblTachiBackupFile";
            this.lblTachiBackupFile.Size = new System.Drawing.Size(249, 31);
            this.lblTachiBackupFile.TabIndex = 13;
            this.lblTachiBackupFile.Text = "Tachiyomi Backup File:";
            // 
            // btnGenTachi
            // 
            this.btnGenTachi.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenTachi.Location = new System.Drawing.Point(3, 136);
            this.btnGenTachi.Name = "btnGenTachi";
            this.btnGenTachi.Size = new System.Drawing.Size(218, 52);
            this.btnGenTachi.TabIndex = 14;
            this.btnGenTachi.Text = "Export Backup";
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
            // btnAddTachiBackup
            // 
            this.btnAddTachiBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddTachiBackup.Location = new System.Drawing.Point(0, 78);
            this.btnAddTachiBackup.Name = "btnAddTachiBackup";
            this.btnAddTachiBackup.Size = new System.Drawing.Size(221, 52);
            this.btnAddTachiBackup.TabIndex = 17;
            this.btnAddTachiBackup.Text = "Add backup";
            this.btnAddTachiBackup.UseVisualStyleBackColor = true;
            this.btnAddTachiBackup.Click += new System.EventHandler(this.btnChangeTachi_Click);
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
            this.tabControl.Size = new System.Drawing.Size(425, 343);
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
            this.tpgBasic.Size = new System.Drawing.Size(417, 310);
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
            this.tpgConfig.Size = new System.Drawing.Size(417, 310);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "Config";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // tpgTachi
            // 
            this.tpgTachi.AllowDrop = true;
            this.tpgTachi.Controls.Add(this.panelTachiSkip);
            this.tpgTachi.Controls.Add(this.panelTachi);
            this.tpgTachi.Location = new System.Drawing.Point(4, 29);
            this.tpgTachi.Name = "tpgTachi";
            this.tpgTachi.Size = new System.Drawing.Size(417, 310);
            this.tpgTachi.TabIndex = 2;
            this.tpgTachi.Text = "Tachiyomi";
            this.tpgTachi.UseVisualStyleBackColor = true;
            this.tpgTachi.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpgTachi_DragDrop);
            this.tpgTachi.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpgTachi_DragEnter);
            // 
            // panelTachiSkip
            // 
            this.panelTachiSkip.Controls.Add(this.cblistTachiSkip);
            this.panelTachiSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTachiSkip.Location = new System.Drawing.Point(0, 232);
            this.panelTachiSkip.Name = "panelTachiSkip";
            this.panelTachiSkip.Size = new System.Drawing.Size(417, 78);
            this.panelTachiSkip.TabIndex = 1;
            // 
            // cblistTachiSkip
            // 
            this.cblistTachiSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cblistTachiSkip.FormattingEnabled = true;
            this.cblistTachiSkip.Location = new System.Drawing.Point(0, 0);
            this.cblistTachiSkip.Name = "cblistTachiSkip";
            this.cblistTachiSkip.Size = new System.Drawing.Size(417, 78);
            this.cblistTachiSkip.TabIndex = 0;
            // 
            // panelTachi
            // 
            this.panelTachi.Controls.Add(this.btnTachiRefresh);
            this.panelTachi.Controls.Add(this.cmbTachiBackup);
            this.panelTachi.Controls.Add(this.cbReplaceTachiLib);
            this.panelTachi.Controls.Add(this.lblTachiBackupFile);
            this.panelTachi.Controls.Add(this.btnAddTachiBackup);
            this.panelTachi.Controls.Add(this.btnGenTachi);
            this.panelTachi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTachi.Location = new System.Drawing.Point(0, 0);
            this.panelTachi.Name = "panelTachi";
            this.panelTachi.Size = new System.Drawing.Size(417, 232);
            this.panelTachi.TabIndex = 0;
            // 
            // cmbTachiBackup
            // 
            this.cmbTachiBackup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTachiBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbTachiBackup.FormattingEnabled = true;
            this.cmbTachiBackup.Location = new System.Drawing.Point(3, 34);
            this.cmbTachiBackup.Name = "cmbTachiBackup";
            this.cmbTachiBackup.Size = new System.Drawing.Size(398, 36);
            this.cmbTachiBackup.TabIndex = 19;
            // 
            // cbReplaceTachiLib
            // 
            this.cbReplaceTachiLib.AutoSize = true;
            this.cbReplaceTachiLib.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbReplaceTachiLib.Location = new System.Drawing.Point(3, 194);
            this.cbReplaceTachiLib.Name = "cbReplaceTachiLib";
            this.cbReplaceTachiLib.Size = new System.Drawing.Size(372, 32);
            this.cbReplaceTachiLib.TabIndex = 18;
            this.cbReplaceTachiLib.Text = "Replace Library with Generated Backup";
            this.cbReplaceTachiLib.UseVisualStyleBackColor = true;
            // 
            // tpgLocal
            // 
            this.tpgLocal.Controls.Add(this.btnLocalMangaAdd);
            this.tpgLocal.Controls.Add(this.gridPathLocal);
            this.tpgLocal.Controls.Add(this.label4);
            this.tpgLocal.Location = new System.Drawing.Point(4, 29);
            this.tpgLocal.Name = "tpgLocal";
            this.tpgLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLocal.Size = new System.Drawing.Size(417, 310);
            this.tpgLocal.TabIndex = 3;
            this.tpgLocal.Text = "Local Media";
            this.tpgLocal.UseVisualStyleBackColor = true;
            // 
            // btnLocalMangaAdd
            // 
            this.btnLocalMangaAdd.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLocalMangaAdd.Location = new System.Drawing.Point(241, 8);
            this.btnLocalMangaAdd.Name = "btnLocalMangaAdd";
            this.btnLocalMangaAdd.Size = new System.Drawing.Size(170, 48);
            this.btnLocalMangaAdd.TabIndex = 23;
            this.btnLocalMangaAdd.Text = "Add";
            this.btnLocalMangaAdd.UseVisualStyleBackColor = true;
            this.btnLocalMangaAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gridPathLocal
            // 
            this.gridPathLocal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPathLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPathLocal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.gridPathLocal.Location = new System.Drawing.Point(6, 60);
            this.gridPathLocal.Name = "gridPathLocal";
            this.gridPathLocal.RowHeadersWidth = 51;
            this.gridPathLocal.RowTemplate.Height = 29;
            this.gridPathLocal.Size = new System.Drawing.Size(405, 208);
            this.gridPathLocal.TabIndex = 15;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "folder";
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "isSeparateSources";
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "mediaType";
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(6, 3);
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
            this.tpMediaLocalAnime.Controls.Add(this.lvLocalAnime);
            this.tpMediaLocalAnime.Location = new System.Drawing.Point(4, 29);
            this.tpMediaLocalAnime.Name = "tpMediaLocalAnime";
            this.tpMediaLocalAnime.Size = new System.Drawing.Size(569, 387);
            this.tpMediaLocalAnime.TabIndex = 3;
            this.tpMediaLocalAnime.Text = "Local Anime";
            this.tpMediaLocalAnime.UseVisualStyleBackColor = true;
            // 
            // lvLocalAnime
            // 
            this.lvLocalAnime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLocalAnime.HideSelection = false;
            this.lvLocalAnime.Location = new System.Drawing.Point(0, 0);
            this.lvLocalAnime.Name = "lvLocalAnime";
            this.lvLocalAnime.Size = new System.Drawing.Size(569, 387);
            this.lvLocalAnime.TabIndex = 22;
            this.lvLocalAnime.UseCompatibleStateImageBehavior = false;
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
            // btnTachiRefresh
            // 
            this.btnTachiRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTachiRefresh.Location = new System.Drawing.Point(227, 78);
            this.btnTachiRefresh.Name = "btnTachiRefresh";
            this.btnTachiRefresh.Size = new System.Drawing.Size(174, 52);
            this.btnTachiRefresh.TabIndex = 20;
            this.btnTachiRefresh.Text = "Refresh";
            this.btnTachiRefresh.UseVisualStyleBackColor = true;
            this.btnTachiRefresh.Click += new System.EventHandler(this.btnTachiRefresh_Click);
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
            this.panelTachiSkip.ResumeLayout(false);
            this.panelTachi.ResumeLayout(false);
            this.panelTachi.PerformLayout();
            this.tpgLocal.ResumeLayout(false);
            this.tpgLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPathLocal)).EndInit();
            this.tabMedia.ResumeLayout(false);
            this.tpMediaAnime.ResumeLayout(false);
            this.tpMediaManga.ResumeLayout(false);
            this.tpMediaTachi.ResumeLayout(false);
            this.tpMediaLocalAnime.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblTachiBackupFile;
        private System.Windows.Forms.Button btnGenTachi;
        private System.Windows.Forms.Button btnRefreshItems;
        private System.Windows.Forms.Button btnAddTachiBackup;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridPathLocal;
        private System.Windows.Forms.Button btnLocalMangaAdd;
        private System.Windows.Forms.ListView lvLocalManga;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.ListView lvLocalAnime;
        private System.Windows.Forms.Panel panelTachi;
        private System.Windows.Forms.Panel panelTachiSkip;
        private System.Windows.Forms.CheckedListBox cblistTachiSkip;
        private System.Windows.Forms.ComboBox cmbTachiBackup;
        private System.Windows.Forms.Button btnTachiRefresh;
    }
}

