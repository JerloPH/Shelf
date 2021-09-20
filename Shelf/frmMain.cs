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
using Shelf.Enum;
using Shelf.Anilist;
using Shelf.Json;
using Shelf.Functions;

namespace Shelf
{
    public partial class frmMain : Form
    {
        private bool IsRefreshing = false;
        private bool IsFetchingMedia = false;
        private string AuthCode = "";
        private string PublicTkn = "";
        private DateTime? TokenDate = null;
        private ImageList animeCoverList = new ImageList();
        private ImageList mangaCoverList = new ImageList();

        // Public Properties
        public Form ConfigForm { get; set; } = null; // Config form

        public frmMain()
        {
            InitializeComponent();
            GlobalFunc.InitializedApp(); // Initialize directory and files
            AnilistRequest.Initialize(); // Initialize config
            InitializeItems(); // Initialize controls
        }
        public void InitializeItems()
        {
            // Anime media listview
            animeCoverList.ImageSize = new Size(120, 180);
            animeCoverList.ColorDepth = ColorDepth.Depth32Bit;
            lvAnime.LargeImageList = animeCoverList;
            lvAnime.View = View.LargeIcon;
            lvAnime.Sorting = SortOrder.Ascending;
            // Manga media listview, copying some Anime properties
            mangaCoverList.ImageSize = animeCoverList.ImageSize;
            mangaCoverList.ColorDepth = animeCoverList.ColorDepth;
            lvManga.LargeImageList = mangaCoverList;
            lvManga.View = lvAnime.View;
            lvManga.Sorting = lvAnime.Sorting;
            // Tachiyomi library
            lvTachi.LargeImageList = mangaCoverList;
            lvTachi.View = lvAnime.View;
            lvTachi.Sorting = lvAnime.Sorting;
            // Other Controls
            cbMediaRefresh.Items.AddRange(new string[] { "All", "Anime", "Manga", "Tachiyomi", "Local Anime", "Local Manga" });
            cbMediaRefresh.SelectedIndex = 0;
        }
        public static bool SetDragFileOnTextBox(DragEventArgs e, TextBox tbox)
        {
            if (e?.Data != null)
            {
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (var item in data)
                {
                    if (File.Exists(item) && (item.EndsWith("gz") || item.EndsWith("proto")))
                    {
                        tbox.Text = item;
                        break;
                    }
                }
                return true;
            }
            return false;
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
        public async Task<bool> RequestMedia(string media, string username, string token)
        {
            // Get media, and write to json file
            Log($"Requesting {media}...");
            var anilistMedia = await AnilistRequest.RequestMediaList(token, username, media);
            if (anilistMedia != null)
            {
                Log($"{media} data fetched!");
                bool result = await Task.Run(delegate { return GlobalFunc.WriteMediaJsonToFile(media, anilistMedia); });
                if (result)
                {
                    Log($"{media} files written!");
                    return true;
                }
                else
                    Log($"{media} is not saved!");
            }
            else
                Log($"No {media} found!");

            return false;
        }
        public async Task<bool> RefreshMedia(MediaType type, List<Entry> medias, ListView lv, ImageList imglist, bool IsClearCover)
        {
            return await Task.Run(async delegate
            {
                int count = 0;
                int max = 0;
                Log($"Refreshing {type} list..");
                SetStatus($"Refreshing {type} list..");
                lv.Invoke((Action)delegate { lv.Items.Clear(); });
                if (IsClearCover)
                {
                    this.Invoke((Action)delegate
                    {
                        imglist.Images.Clear();
                    });
                }
                max = medias.Count;
                foreach (var item in medias)
                {
                    count += 1;
                    SetStatus($"Adding item..{count}/{max}");
                    await AddItemToListView(lv, imglist, item, type);
                    Thread.Sleep(10);
                    if (count >= 10 && GlobalFunc.DEBUG)
                        break;
                }
                Log($"{type} items loaded!");
                lv.Invoke((Action)delegate { lv.Sort(); });
                return true;
            });
        }
        public async Task<bool> AddItemToListView(ListView lv, ImageList imglist, Entry item, MediaType type)
        {
            // Declare variables
            var lvitem = new ListViewItem();
            Image img = null;
            // Download Image, if not existing
            if (item.Media.Id > 0)
            {
                img = await LoadImageFromTemp(item.Media.Id, type);
                if (img == null)
                {
                    string file = GetCoverFilePath(item.Media.Id, type);
                    img = await DownloadImage(item.Media.CoverImage.Medium, file);
                }
                else
                    Logs.App($"{type}: Loaded local image for: {item.Media.Id}.");
            }
            else
            {
                img = Properties.Resources.nocover;
            }

            // Run Task
            return await Task.Run(delegate
            {
                if (img != null)
                {
                    string key = item.Media.Id.ToString();
                    this.Invoke((Action) delegate
                    {
                        if (!imglist.Images.ContainsKey(key))
                        {
                            imglist.Images.Add(key, img);
                        }
                        else
                        {
                            if (item.Media.Id > 0)
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
                lvitem.Text = (!String.IsNullOrWhiteSpace(item.Media.Title.English) ? item.Media.Title.English : item.Media.Title.Romaji);
                lv.BeginInvoke((Action)delegate
                {
                    lv.Items.Add(lvitem);
                });
                return true;
            });
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
            catch (Exception ex)
            {
                Logs.Err(ex);
            }
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
        #endregion
        // ####################################################################### Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            cbMedia.Items.AddRange(new string[] { "ALL", "ANIME", "MANGA" });
            cbMedia.SelectedIndex = 0;
            Log("Click on 'Refresh Token' to start!");
            TokenDate = DateTime.Now.AddMinutes(-61);
            Log($"Date of Token: {TokenDate}");
            //btnRefresh.PerformClick(); // Fetch access code and token
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            tabMedia.Width = this.ClientRectangle.Width - tabMedia.Left - 16;
            tabMedia.Height = this.ClientRectangle.Height - tabMedia.Top - 16;
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!IsRefreshing)
            {
                btnFetchMedia.Enabled = false; // Disable fetching media files
                // Get Authorization Code
                if (File.Exists(GlobalFunc.FILE_AUTH_CODE))
                {
                    AuthCode = GlobalFunc.ReadFromFile(GlobalFunc.FILE_AUTH_CODE);
                }
                if (String.IsNullOrWhiteSpace(AuthCode))
                {
                    var form = new frmGetAuthCode();
                    form.ShowDialog(this);
                    AuthCode = form.AuthCode;
                    form.Dispose();
                    GlobalFunc.WriteFile(GlobalFunc.FILE_AUTH_CODE, AuthCode);
                }
                // Start fetching
                IsRefreshing = true;
                btnRefresh.Enabled = false;
                // Check if token is valid
                if (TokenDate.Value.AddMinutes(59) <= DateTime.Now)
                {
                    Log("Requesting token..");
                    // Request Access Token, using Auth code
                    PublicTkn = await AnilistRequest.RequestPublicToken(AuthCode);
                    if (String.IsNullOrWhiteSpace(PublicTkn))
                    {
                        AuthCode = ""; // reset authorization code if token is invalidated.
                        GlobalFunc.WriteFile(GlobalFunc.FILE_AUTH_CODE, "");
                        Log("No Public Token acquired! Try again.");
                    }
                    else
                    {
                        TokenDate = DateTime.Now;
                        Log($"Token refreshed! Date acquired: {TokenDate}");
                    }
                }
                else
                    Log("Token is still valid.");

                // Re-enable button
                btnRefresh.Enabled = true;
                IsRefreshing = false;
                btnFetchMedia.Enabled = true; // Enable fetching media files
            }
        }

        private async void btnFetchMedia_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Log("Username is empty!");
                txtUsername.Focus();
                return;
            }
            if (!IsFetchingMedia)
            {
                IsFetchingMedia = true;
                btnFetchMedia.Enabled = false;
                if (!String.IsNullOrWhiteSpace(PublicTkn))
                {
                    // What type of media?
                    if (cbMedia.SelectedIndex > 0)
                    {
                        await RequestMedia(cbMedia.Text, txtUsername.Text, PublicTkn);
                    }
                    else
                    {
                        // Get media, and write to json file
                        await RequestMedia("ANIME", txtUsername.Text, PublicTkn);
                        await RequestMedia("MANGA", txtUsername.Text, PublicTkn);
                    }
                }
                else
                    Log("No Token!");

                IsFetchingMedia = false;
                btnFetchMedia.Enabled = true;
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
            string username = txtUsername.Text;
            string outputAnime = "";
            string outputManga = "";
            string outputAnimeNonMal = "";
            string outputMangaNonMal = "";
            bool processAnime = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 1;
            bool processManga = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 2;
            if (String.IsNullOrWhiteSpace(username))
            {
                GlobalFunc.Alert("Username is empty!");
                txtUsername.Focus();
                return;
            }
            btnMALExport.Enabled = false;
            // Process Anime
            if (processAnime)
            {
                try
                {
                    outputAnime = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_{GlobalFunc.DATE_TODAY}.xml");
                    outputAnimeNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_NonMal_{GlobalFunc.DATE_TODAY}.json");
                    GlobalFunc.WriteFile(outputAnime, "");
                }
                catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot create Anime output!"); return; }
            }
            if (processManga)
            {
                try
                {
                    outputManga = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_{GlobalFunc.DATE_TODAY}.xml");
                    outputMangaNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_NonMal_{GlobalFunc.DATE_TODAY}.json");
                    GlobalFunc.WriteFile(outputManga, "");
                }
                catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot create Manga output!"); return; }
            }

            await Task.Run((Action)async delegate
            {
                // ANIME
                if (processAnime)
                {
                    var anime = await MediaTasks.GetAnimeList();
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
                    var manga = MediaTasks.GetMangaList().Result;
                    if (manga != null)
                    {
                        MediaTasks.ProcessMedia(manga, "manga", outputManga, username, outputMangaNonMal).Wait();
                        manga.Clear();
                    }
                    // End of Manga entries
                }
            });
            GlobalFunc.Alert("Done!");
            btnMALExport.Enabled = true;
        }

        private async void btnGenTachi_Click(object sender, EventArgs e)
        {
            btnGenTachi.Enabled = false;
            List<string> ext = new List<string>();
            ext.AddRange(new string[]{ "proto", "gz" });
            string file = txtTachi.Text.Trim();
            if (File.Exists(file))
            {
                try
                {
                    if (ext.Contains(Path.GetExtension(file).Trim('.')))
                    {
                        await MediaTasks.GenerateMissingTachiEntries(file);
                        GlobalFunc.Alert("Done generating Tachiyomi backup file!");
                    }
                    else
                        GlobalFunc.Alert($"Tachiyomi file isn't supported!\nOnly '{String.Join('/', ext)}' files are accepted.");
                }
                catch (Exception ex)
                {
                    Logs.Err(ex);
                    GlobalFunc.Alert("Invalid Tachiyomi Filepath!");
                }
            }
            else
                GlobalFunc.Alert("Tachiyomi backup file does not exists!");

            btnGenTachi.Enabled = true;
        }

        private async void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnRefreshItems.Enabled = false;
            // vars
            var manga = new List<Entry>();
            string tachibackup = txtTachi.Text;
            // Declare which media to refresh
            bool loadAnime = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("anime", StringComparison.OrdinalIgnoreCase);
            bool loadManga = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("manga", StringComparison.OrdinalIgnoreCase);
            bool loadTachi = cbMediaRefresh.SelectedIndex == 0 || cbMediaRefresh.Text.Equals("tachiyomi", StringComparison.OrdinalIgnoreCase);
            // Refresh Anime?
            if (loadAnime)
            {
                var anime = await MediaTasks.GetAnimeList();
                await RefreshMedia(MediaType.ANIME, anime, lvAnime, animeCoverList, true);
            }
            // Refresh Manga?
            if (loadManga)
            {
                manga = await MediaTasks.GetMangaList();
                await RefreshMedia(MediaType.MANGA, manga, lvManga, mangaCoverList, false);
            }
            // Refresh Manga?
            if (loadTachi)
            {
                Log("Loading Tachiyomi library..");
                var tachilib = await MediaTasks.LoadTachiyomiBackup(tachibackup);
                if (tachilib != null)
                {
                    var entries = new List<Entry>();
                    if (manga.Count < 1)
                        manga = await MediaTasks.GetMangaList();

                    await Task.Run(delegate
                    {
                        foreach (var item in tachilib.Mangas)
                        {
                            if (item.Tracking?.Count > 0)
                            {
                                foreach (var track in item.Tracking)
                                {
                                    if (track.TrackingUrl.Contains("anilist", StringComparison.OrdinalIgnoreCase))
                                    {
                                        var mangaExisting = entries.Select(x => x).Where(x => x.Media.Id == track.MediaId);
                                        if (!mangaExisting.Any())
                                        {
                                            var mangaQuery = manga.Select(x => x).Where(x => x.Media.Id == track.MediaId);
                                            if (mangaQuery.Count() == 1)
                                                entries.Add(mangaQuery.First());
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (item != null)
                                {
                                    try
                                    {
                                        var newEntry = new Entry();
                                        newEntry.Media = new Media();
                                        newEntry.Media.Title = new Title();
                                        if (!String.IsNullOrWhiteSpace(item.Title))
                                            newEntry.Media.Title.Romaji = item.Title;

                                        newEntry.Media.Id = 0;
                                        entries.Add(newEntry);
                                    }
                                    catch (Exception ex) { Logs.Err(ex); }
                                }
                            }
                        }
                    });
                    await RefreshMedia(MediaType.MANGA, entries, lvTachi, mangaCoverList, false);
                }
                else
                    Log("Tachiyomi backup file is missing!");
            }
            SetStatus("Idle");
            btnRefreshItems.Enabled = true;
        }

        private void btnChangeTachi_Click(object sender, EventArgs e)
        {
            string file = GlobalFunc.BrowseForFile("Browse for Tachiyomi backup file", "Tachiyomi backups|*.proto;*.gz", GlobalFunc.DIR_START);
            if (File.Exists(file))
                txtTachi.Text = file;
        }

        private void btnTachiGoto_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtTachi.Text))
                GlobalFunc.FileOpeninExplorer(txtTachi.Text);
        }

        private void tpgTachi_DragDrop(object sender, DragEventArgs e)
        {
            SetDragFileOnTextBox(e, txtTachi);
        }

        private void tabControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tpgTachi_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void txtTachi_DragDrop(object sender, DragEventArgs e)
        {
            SetDragFileOnTextBox(e, txtTachi);
        }

        private void txtTachi_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
