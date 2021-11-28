using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Shelf.CustomEnums;
using Shelf.Anilist;
using Shelf.Json;
using Shelf.Functions;
using System.ComponentModel;
using Shelf.Entity;
using Newtonsoft.Json;
using JerloPH_CSharp;
using Shelf.Views;

namespace Shelf
{
    public partial class frmMain : Form
    {
        private bool IsRefreshing = false;
        private bool IsFetchingMedia = false;
        private string AnilistUserId = "";
        private string AuthCode = "";
        private string PublicTkn = "";
        private DateTime? TokenDate = null;
        private ImageList animeCoverList = new ImageList();
        private ImageList mangaCoverList = new ImageList();
        private ImageList localanimeCoverList = new ImageList();
        private ImageList localmangaCoverList = new ImageList();
        private LocalMedia localMedia = null;
        private List<TachiBackupFile> TachiBackups = null;

        public AppSettingsEntity Setting = null;

        // Public Properties
        public Form ConfigForm { get; set; } = null; // Config form

        public frmMain()
        {
            InitializeComponent();
            this.Text = $"Shelf v{GlobalFunc.GetAppVersion()}";
            localMedia = new LocalMedia();
            TachiBackups = new List<TachiBackupFile>();
            Msg.SetDefaultParent(this);
        }
        private async Task<bool> InitializeObjects()
        {
            // Initialize all objects
            return await Task.Run(delegate
            {
                try
                {
                    this.Invoke((Action)delegate
                    {
                        // Initialize controls
                        UIHelper.SetupListViewAndImgList(lvAnime, animeCoverList); // Anime media listview
                        UIHelper.SetupListViewAndImgList(lvManga, mangaCoverList); // Manga media listview
                        UIHelper.SetupListViewAndImgList(lvTachi, mangaCoverList); // Tachiyomi library
                        UIHelper.SetupListViewAndImgList(lvLocalAnime, localanimeCoverList); // Local Anime
                        UIHelper.SetupListViewAndImgList(lvLocalManga, localmangaCoverList); // Local Manga
                                                                                             // Other Controls
                        cbMediaRefresh.Items.AddRange(new string[] { "All", "Anime", "Manga", "Tachiyomi", "Local Anime", "Local Manga" });
                            cbMediaRefresh.SelectedIndex = 0;
                            cbMedia.Items.AddRange(new string[] { "ALL", "ANIME", "MANGA" });
                            cbMedia.SelectedIndex = 0;

                            UIHelper.PopulateCombobox<MediaEntryMode>(cbEntryMode);

                        // Populate panel with checkboxes
                        string[] status = new string[] { "CURRENT", "PAUSED", "PLANNING", "REPEATING", "COMPLETED", "DROPPED" };
                        foreach (string item in status)
                        {
                            int index = cblistTachiSkip.Items.Add(item);
                            bool check = true;
                            if (Setting.isAutoSkipTachi)
                            {
                                if (item.Equals("COMPLETED") || item.Equals("DROPPED"))
                                    check = false;
                            }
                            cblistTachiSkip.SetItemChecked(index, check);
                            if (check)
                                GlobalFunc.INCLUDED_STATUS.Add(item);
                        }
                        cblistTachiSkip.ItemCheck += CblistTachiSkip_ItemCheck;
                        cblistTachiSkip.MultiColumn = true;
                        Log("Initialized UI Controls!");

                        // Initialize Local Media Paths
                        if (File.Exists(GlobalFunc.FILE_LOCAL_MEDIA))
                        {
                            localMedia = GlobalFunc.JsonDecode<LocalMedia>(GlobalFunc.FILE_LOCAL_MEDIA);
                        }
                        else
                        {
                            localMedia.paths = new List<LocalMediaPaths>();
                            if (GlobalFunc.DEBUG)
                            {
                                string path1 = FileHelper.CreateNewFolder(GlobalFunc.DIR_TEMP, "mangaFolder1");
                                string path2 = FileHelper.CreateNewFolder(GlobalFunc.DIR_TEMP, "mangaFolder2");
                                localMedia.paths.Add(new LocalMediaPaths() { folder = path1, isSeparateSources = false, mediaType = MediaAniManga.MANGA });
                                localMedia.paths.Add(new LocalMediaPaths() { folder = path2, isSeparateSources = false, mediaType = MediaAniManga.MANGA });
                                GlobalFunc.JsonEncode(localMedia, GlobalFunc.FILE_LOCAL_MEDIA);
                            }
                        }
                        UIHelper.BindLocalMediaToDataGrid(gridPathLocal, localMedia.paths, new string[] { "Path", "Separate Source", "Media" });
                        (gridPathLocal.Columns[2] as DataGridViewComboBoxColumn).DataSource = System.Enum.GetValues(typeof(MediaAniManga));
                        Log("Initialized Local Media Paths!");
                    });
                    return true;
                }
                catch (Exception ex) { GlobalFunc.ShowErr("Some UI are not initialized!", ex , this); }
                return false;
            });            
        }

        private async Task LoadTachiyomiBackupFiles()
        {
            // Load Tachiyomi backups
            await Task.Run(delegate {
                Log("Initializing Tachiyomi backups..");
                try
                {
                    this.Invoke((Action)delegate
                    {
                        // Clear prev
                        TachiBackups.Clear();
                        cmbTachiBackup.Items.Clear();

                        string tachibackupfolder = Setting.tachibackup;
                        if (Directory.Exists(tachibackupfolder))
                        {
                            foreach (string item in Directory.GetFiles(tachibackupfolder))
                            {
                                AddTachiBackupFile(item);
                            }
                        }
                    
                        cmbTachiBackup.DisplayMember = "Name";
                        cmbTachiBackup.ValueMember = "File";
                        cmbTachiBackup.SelectedIndex = cmbTachiBackup.Items.Count - 1;
                    });
                    Log("Loaded Tachiyomi backup files.");
                }
                catch (Exception ex) { Logs.Err(ex); Log("Error on loading tachiyomi backups folder!"); }
            });
        }
        public void AddTachiBackupFile(string item)
        {
            if (File.Exists(item) && (item.EndsWith("gz") || item.EndsWith("proto")))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var entry = new TachiBackupFile();
                    entry.Name = fileInfo.Name;
                    entry.File = item;
                    var matches = TachiBackups.Where(p => p.File == entry.File).ToList();
                    if (matches == null || matches.Count < 1)
                    {
                        TachiBackups.Add(entry);
                        cmbTachiBackup.Items.Add(entry);
                    }
                }
                catch (Exception ex) { Logs.Err(ex); }
            }
        }
        #region Form-specific functions
        public void Log(string log)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((Action) delegate
                {
                    txtLog.AppendText($"[{DateTime.Now.ToString("HH:mm:ss")}]: {log}\r\n");
                });
            }
            else
                txtLog.AppendText($"[{DateTime.Now.ToString("HH:mm:ss")}]: {log}\r\n");
        }
        public void SetText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke((Action)delegate { ctrl.Text = text; });
            }
            else
                ctrl.Text = text;
        }
        public void SetStatus(string text)
        {
            SetText(lblStatus, text);
        }
        public string GetCoverFilePath(long Id, MediaType type)
        {
            string root = (type == MediaType.MANGA) ? GlobalFunc.DIR_TEMP_MANGACOVER : GlobalFunc.DIR_TEMP_ANIMECOVER;
            string file = Path.Combine(root, $"{Id}.jpg");
            return file;
        }
        #endregion
        #region Tasks
        public async Task<string> RequestAuthCode()
        {
            return await Task.Run(delegate
            {
                string code = "";
                Invoke((Action)delegate
                {
                    var form = new frmGetAuthCode();
                    form.ShowDialog(this);
                    code = form.AuthCode;
                    form.Dispose();
                    FileHelper.WriteFile(GlobalFunc.FILE_AUTH_CODE, code);
                });
                return code;
            });
        }
        public async Task<string> RequestPublicTkn()
        {
            return await Task.Run(async delegate
            {
                string token = "";
                // Get Authorization Code
                if (String.IsNullOrWhiteSpace(AuthCode))
                {
                    Log("Loading Authorization Code..");
                    if (File.Exists(GlobalFunc.FILE_AUTH_CODE) && Setting.isSaveToken)
                        AuthCode = FileHelper.ReadFromFile(GlobalFunc.FILE_AUTH_CODE);

                    if (String.IsNullOrWhiteSpace(AuthCode))
                        AuthCode = await RequestAuthCode();
                }
                // Request Access Token, using Auth code
                try
                {
                    Log("Requesting token..");
                    token = await AnilistRequest.RequestPublicToken(AuthCode, Setting.isSaveToken);
                }
                catch (Exception ex) { Logs.Err(ex); token = ""; }
                if (String.IsNullOrWhiteSpace(token))
                {
                    this.Invoke((Action) delegate
                    {
                        AuthCode = ""; // reset authorization code if token is invalidated.
                    });
                    if (FileHelper.WriteFile(GlobalFunc.FILE_AUTH_CODE, ""))
                        Log("No Public Token acquired! Need to Authorize again.");
                    else
                        Log("Old Auth code detected! Manually remove them from data folder.");

                    return "";
                }
                else
                {
                    this.Invoke((Action)delegate
                    {
                        TokenDate = DateTime.Now;
                    });
                    Log($"Token refreshed! Date acquired: {TokenDate}");
                }
                return token;
            });
        }
        public async Task<bool> RequestMedia(string media, string userId, string token)
        {
            // Get media, and write to json file
            Log($"Requesting {media}...");
            try
            {
                var anilistMedia = await AnilistRequest.RequestMediaList(token, userId, media);
                if (anilistMedia != null)
                {
                    Log($"{media} data fetched!");
                    bool result = await Task.Run(delegate { return GlobalFunc.WriteMediaJsonToFile(media, anilistMedia); });
                    Log(result ? $"{media} files written!" : $"{media} is not saved!");
                    return result;
                }
                else
                    Log($"No {media} found!");
            }
            catch (Exception ex) { Logs.Err(ex); Msg.ShowWarning($"Error occured on fetching {media}!\nTry again."); }
            return false;
        }
        public async Task<bool> RefreshMedia(MediaType type, List<Entry> medias, ListView lv, ImageList imglist, bool IsClearCover)
        {
            return await Task.Run(async delegate
            {
                if (medias == null)
                    return false;
                
                long count = 0;
                int max = 0;
                string typeCaption = type.ToString().Replace("_", " ");
                Log($"Refreshing {typeCaption} list..");
                SetStatus($"Refreshing {typeCaption} list..");
                lv.Invoke((Action)delegate { lv.Items.Clear(); });
                if (IsClearCover)
                {
                    this.Invoke((Action)delegate
                    {
                        imglist.Images.Clear();
                    });
                }
                max = medias.Count;
                if (max > 0)
                {
                    foreach (var item in medias)
                    {
                        if (item != null)
                        {
                            count += 1;
                            SetStatus($"Adding item..{count}/{max}");
                            //Log($"Adding item with ID ->  {item.Media.Id}..");
                            await AddItemToListView(lv, imglist, item, type, count);
                            Thread.Sleep(10);
                            if (count >= 20 && GlobalFunc.DEBUG)
                                break;
                        }
                    }
                }
                Log($"{typeCaption} items loaded!");
                lv.Invoke((Action)delegate { lv.Sort(); });
                return true;
            });
        }
        public async Task<bool> AddItemToListView(ListView lv, ImageList imglist, Entry item, MediaType type, long count)
        {
            // Declare variables
            var lvitem = new ListViewItem();
            Image img = null;
            bool isLocal = (type == MediaType.LOCAL_ANIME || type == MediaType.LOCAL_MANGA);
            // Get Image
            if (isLocal)
            {
                img = await LoadImageLocal(item.Path); // Load Local
            }
            else // Load Image for Anilist entry
                img = await LoadImageAnilist(item.Media.Id, item.Media.CoverImage.Medium, type);

            if (img == null)
                img = Properties.Resources.nocover;
            // Run Task
            return await Task.Run(delegate
            {
                if (img != null)
                {
                    string key = (isLocal ? count.ToString() : item.Media.Id.ToString());
                    this.Invoke((Action) delegate
                    {
                        if (!imglist.Images.ContainsKey(key))
                        {
                            imglist.Images.Add(key, img);
                        }
                        else
                        {
                            if (key != "0")
                                img.Dispose();
                        }
                        lvitem.ImageKey = key;
                    });
                }
                else
                {
                    lvitem.ImageKey = "0";
                    Logs.App($"No Image! Item: {item.Media.Id}");
                }
                // Add properties values to ListView item
                lvitem.Tag = item.Media.Id;
                string title = "";
                if (Setting.isAlwaysUseRomaji)
                {
                    title = item.Media.Title.Romaji;
                }
                else
                {
                    title = (!String.IsNullOrWhiteSpace(item.Media.Title.English) ? item.Media.Title.English : item.Media.Title.Romaji);
                }
                lvitem.Text = title;
                lv.BeginInvoke((Action)delegate
                {
                    lv.Items.Add(lvitem);
                });
                return true;
            });
        }
        public async Task<Image> LoadImageLocal(string path)
        {
            try
            {
                return await Task.Run(delegate
                {
                    Image img = null;
                    string file = $"{path}\\cover.jpg";
                    if (File.Exists(file))
                        img = Image.FromFile(file);

                    return img;
                });
            }
            catch (Exception ex) { Logs.Err(ex); }
            return null;
        }
        public async Task<Image> LoadImageAnilist(long Id, string link, MediaType type)
        {
            try
            {
                if (Id > 0)
                {
                    Image img = null;
                    if (!Setting.isAlwaysDownloadCover)
                        img = await LoadImageFromTemp(Id, type);

                    if (img == null)
                    {
                        string file = GetCoverFilePath(Id, type);
                        img = await DownloadImage(link, file);
                    }
                    return img;
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
            return null;
        }
        public async Task<Image> LoadImageFromTemp(long Id, MediaType type)
        {
            try
            {
                string file = GetCoverFilePath(Id, type);
                if (File.Exists(file))
                {
                    return await Task.Run(delegate
                    {
                        return Image.FromFile(file);
                    });
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
            return null;
        }
        public async Task<Image> DownloadImage(string link, string saveTo)
        {
            try
            {
                return await Task.Run(delegate
                {
                    using (var client = new WebClient())
                    {
                        Logs.App($"Downloading image.. => {link}");
                        client.DownloadFile(link, saveTo);
                        Logs.App($"Saved to => {saveTo.Replace(GlobalFunc.DIR_TEMP, "<temp>")}");
                        return Image.FromFile(saveTo);
                    }
                });
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
            }
            return null;
        }
        private async void RefreshToken()
        {
            if (!IsRefreshing)
            {
                Log("Refreshing token..");
                btnFetchMedia.Enabled = false; // Disable fetching media files
                // Start fetching
                IsRefreshing = true;
                btnRefresh.Enabled = false;
                // Check if token is valid
                if (TokenDate.Value.AddMinutes(59) <= DateTime.Now || (String.IsNullOrWhiteSpace(PublicTkn)))
                {
                    PublicTkn = await RequestPublicTkn();
                    if (String.IsNullOrWhiteSpace(PublicTkn))
                        PublicTkn = await RequestPublicTkn();

                    if (String.IsNullOrWhiteSpace(AnilistUserId))
                    {
                        try
                        {
                            Log("Requesting user info..");
                            var reqUserResult = await AnilistRequest.RequestUser(PublicTkn);
                            AnilistUserId = reqUserResult.Item1;
                            Log($"User found! User Id: {AnilistUserId}, Username: {reqUserResult.Item2}");
                            if (String.IsNullOrWhiteSpace(txtUsername.Text))
                                txtUsername.Text = reqUserResult.Item2;
                        }
                        catch (Exception ex) { Logs.Err(ex); Log("User info not fetched!"); };
                    }
                }
                else
                    Log("Token is still valid.");

                // Re-enable button
                btnRefresh.Enabled = true;
                IsRefreshing = false;
                btnFetchMedia.Enabled = true; // Enable fetching media files

                if (Setting.isAutoRefreshmedia)
                    btnFetchMedia_Click(btnFetchMedia, new EventArgs());
            }
            else
                Log("Wait for previous refresh to finish!");
        }
        #endregion
        // ####################################################################### Custom Events
        private void CblistTachiSkip_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedIndices = this.cblistTachiSkip.CheckedIndices.Cast<int>().ToList();
            if (e.NewValue == CheckState.Checked)
                checkedIndices.Add(e.Index);
            else
                if (checkedIndices.Contains(e.Index))
                checkedIndices.Remove(e.Index);

            GlobalFunc.INCLUDED_STATUS.Clear();
            foreach (int index in checkedIndices)
            {
                string item = cblistTachiSkip.Items[index].ToString();
                if (!GlobalFunc.INCLUDED_STATUS.Contains(item))
                    GlobalFunc.INCLUDED_STATUS.Add(item);
            }
            //Log($"Status => {string.Join(',', GlobalFunc.INCLUDED_STATUS)}");
        }
        // ####################################################################### Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            var form = Msg.Load("Loading", "Form Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                this.Invoke(new Action(() =>
                {
                    GlobalFunc.InitializedApp(); // Initialize directory and files
                    AnilistRequest.Initialize(); // Initialize config
                    Setting = AppSettings.AppConfig;
                }));
                var success = InitializeObjects().Result;
                LoadTachiyomiBackupFiles().Wait();
            };
            form.SetIcon(Msg.LoadIcons.Default);
            form.ShowDialog(this);
            Log("Click on 'Refresh Token' to start!");
            TokenDate = DateTime.Now.AddMinutes(-61);
            IsRefreshing = false;
            if (Setting.isAutoFetchToken)
                RefreshToken(); // Fetch access code and token
            
            CenterToScreen();
            Focus();
            Select();
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            tabMedia.Width = this.ClientRectangle.Width - tabMedia.Left - 16;
            tabMedia.Height = this.ClientRectangle.Height - tabMedia.Top - 16;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshToken();
        }

        private async void btnFetchMedia_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PublicTkn))
            {
                Log("No Token! Will fetch..");
                RefreshToken();
            }
            else
            {
                if (!IsFetchingMedia)
                {
                    IsFetchingMedia = true;
                    btnFetchMedia.Enabled = false;

                    if (!String.IsNullOrWhiteSpace(PublicTkn))
                    {
                        // Get specified media, and write to json file
                        if (cbMedia.SelectedIndex > 0)
                        {
                            await RequestMedia(cbMedia.Text, AnilistUserId, PublicTkn);
                        }
                        else // Get all media
                        {
                            await RequestMedia("ANIME", AnilistUserId, PublicTkn);
                            await RequestMedia("MANGA", AnilistUserId, PublicTkn);
                        }
                    }

                    IsFetchingMedia = false;
                    btnFetchMedia.Enabled = true;
                }
                else
                    Log("Wait for current media to be fetched.");
            }
        }

        private void btnChangeConfig_Click(object sender, EventArgs e)
        {
            if (ConfigForm == null)
            {
                ConfigForm = new frmChangeAnilistConfig(this);
                ConfigForm.Show(this);
            }
            if (ConfigForm.WindowState == FormWindowState.Minimized)
                ConfigForm.WindowState = FormWindowState.Normal;

            ConfigForm.Focus();
        }

        private async void btnMALExport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Log("Username is empty!");
                txtUsername.Focus();
                return;
            }
            string username = txtUsername.Text;
            string outputAnime = "";
            string outputManga = "";
            string outputAnimeNonMal = "";
            string outputMangaNonMal = "";
            bool processAnime = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 1;
            bool processManga = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 2;
            btnMALExport.Enabled = false;
            // Process Anime
            if (processAnime)
            {
                try
                {
                    outputAnime = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_{GlobalFunc.DATE_TODAY}.xml");
                    outputAnimeNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_NonMal_{GlobalFunc.DATE_TODAY}.json");
                    FileHelper.WriteFile(outputAnime, "");
                }
                catch (Exception ex) { Logs.Err(ex); Msg.ShowWarning("Cannot create Anime output!"); return; }
            }
            if (processManga)
            {
                try
                {
                    outputManga = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_{GlobalFunc.DATE_TODAY}.xml");
                    outputMangaNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_NonMal_{GlobalFunc.DATE_TODAY}.json");
                    FileHelper.WriteFile(outputManga, "");
                }
                catch (Exception ex) { Logs.Err(ex); Msg.ShowWarning("Cannot create Manga output!"); return; }
            }

            await Task.Run((Action)async delegate
            {
                // ANIME
                if (processAnime)
                {
                    var anime = await MediaTasks.GetAnimeList(MediaEntryMode.All);
                    if (anime != null)
                    {
                        await MediaTasks.ProcessMedia(anime, "anime", outputAnime, username, outputAnimeNonMal);
                        anime.Clear();
                    }
                    // End of Anime entries
                }
                // MANGA
                if (processManga)
                {
                    var manga = MediaTasks.GetMangaList(MediaEntryMode.All).Result;
                    if (manga != null)
                    {
                        MediaTasks.ProcessMedia(manga, "manga", outputManga, username, outputMangaNonMal).Wait();
                        manga.Clear();
                    }
                    // End of Manga entries
                }
            });
            Msg.ShowInfo("Done!");
            btnMALExport.Enabled = true;
        }

        private async void btnGenTachi_Click(object sender, EventArgs e)
        {
            btnGenTachi.Enabled = false;
            bool IsReplaceTachiLib = cbReplaceTachiLib.Checked;
            List<string> ext = new List<string>();
            ext.AddRange(new string[]{ "proto", "gz" });
            string file = ((TachiBackupFile)cmbTachiBackup.SelectedItem).File;
            if (File.Exists(file))
            {
                try
                {
                    if (ext.Contains(Path.GetExtension(file).Trim('.')))
                    {
                        var entries = await MediaTasks.GenerateMissingTachiEntries(file, Setting.tachiBackupMode);
                        if (IsReplaceTachiLib)
                        {
                            await RefreshMedia(MediaType.MANGA, entries, lvTachi, mangaCoverList, false);
                        }
                        SetStatus("Idle");
                        Msg.ShowInfo((entries?.Count > 0) ? "Done generating Tachiyomi backup file!" : "All Anilist entries are\non your Tachiyomi library!");
                    }
                    else
                        Msg.ShowWarning($"Tachiyomi file isn't supported!\nOnly '{String.Join('/', ext)}' files are accepted.");
                }
                catch (Exception ex)
                {
                    Logs.Err(ex);
                    Msg.ShowWarning("Error occured on loading Tachiyomi!");
                }
            }
            else
                Msg.ShowWarning("Tachiyomi backup file does not exists!");

            btnGenTachi.Enabled = true;
        }

        private async void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnRefreshItems.Enabled = false;
            // vars
            MediaEntryMode mode = MediaEntryMode.All;
            var manga = new List<Entry>();
            string tachibackup = ((TachiBackupFile)cmbTachiBackup.SelectedItem).File;
            // Declare which media to refresh
            bool loadAnime = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("anime", StringComparison.OrdinalIgnoreCase);
            bool loadManga = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("manga", StringComparison.OrdinalIgnoreCase);
            bool loadTachi = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("tachiyomi", StringComparison.OrdinalIgnoreCase);
            bool loadLocalAnime = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("local anime", StringComparison.OrdinalIgnoreCase);
            bool loadLocalManga = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("local manga", StringComparison.OrdinalIgnoreCase);
            // Switch Entry Mode
            if (cbEntryMode.SelectedIndex > 0)
                mode = (MediaEntryMode)cbEntryMode.SelectedIndex;

            // Refresh Anime
            if (loadAnime)
            {
                var anime = await MediaTasks.GetAnimeList(mode);
                await RefreshMedia(MediaType.ANIME, anime, lvAnime, animeCoverList, true);
            }
            // Refresh Manga
            if (loadManga)
            {
                manga = await MediaTasks.GetMangaList(mode);
                await RefreshMedia(MediaType.MANGA, manga, lvManga, mangaCoverList, false);
            }
            // Refresh Tachiyomi Manga
            if (loadTachi)
            {
                Log("Loading Tachiyomi library..");
                var tachilib = await MediaTasks.LoadTachiyomiBackup(tachibackup);
                if (tachilib != null)
                {
                    var entries = await MediaTasks.GetTachiWithAnilist(manga, tachilib.Mangas, mode);
                    await RefreshMedia(MediaType.MANGA, entries, lvTachi, mangaCoverList, false);
                }
                else
                    Log("Tachiyomi backup file is missing!");
            }
            // Refresh Local Anime
            if (loadLocalAnime)
            {
                var localAnime = await MediaTasks.GetLocalMedia(localMedia.paths, MediaAniManga.ANIME);
                await RefreshMedia(MediaType.LOCAL_ANIME, localAnime, lvLocalAnime, localanimeCoverList, true);
            }
            // Refresh Local Manga
            if (loadLocalManga)
            {
                var localManga = await MediaTasks.GetLocalMedia(localMedia.paths, MediaAniManga.MANGA);
                await RefreshMedia(MediaType.LOCAL_MANGA, localManga, lvLocalManga, localmangaCoverList, true);
            }
            SetStatus("Idle");
            btnRefreshItems.Enabled = true;
        }

        private void btnChangeTachi_Click(object sender, EventArgs e)
        {
            string file = GlobalFunc.BrowseForFile("Browse for Tachiyomi backup file", "Tachiyomi backups|*.proto;*.gz", Setting.tachibackup);
            AddTachiBackupFile(file);
        }

        private void btnTachiGoto_Click(object sender, EventArgs e)
        {
            string file = ((TachiBackupFile)cmbTachiBackup.SelectedItem).File;
            if (File.Exists(file))
                GlobalFunc.FileOpeninExplorer(file, true);
        }

        private void tpgTachi_DragDrop(object sender, DragEventArgs e)
        {
            if (e?.Data != null)
            {
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string item in data)
                {
                    AddTachiBackupFile(item);
                }
            }
        }

        private void tabControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tpgTachi_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // TODO: Change to a form that will add Path
            GlobalFunc.JsonEncode(localMedia, GlobalFunc.FILE_LOCAL_MEDIA);
        }

        private async void btnTachiRefresh_Click(object sender, EventArgs e)
        {
            // Refresh backup list
            btnTachiRefresh.Enabled = false;
            await LoadTachiyomiBackupFiles();
            btnTachiRefresh.Enabled = true;
        }

        private void bnConfigSetting_Click(object sender, EventArgs e)
        {
            var form = new frmSettings();
            form.Show();
        }

        private void btnGotoOutput_Click(object sender, EventArgs e)
        {
            string dir = GlobalFunc.DIR_OUTPUT;
            if (Directory.Exists(dir))
                GlobalFunc.FileOpeninExplorer(dir, false);
        }
    }
}
