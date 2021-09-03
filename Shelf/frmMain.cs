using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Shelf.Anilist;
using Shelf.Json;
using Shelf.Functions;
using Shelf.Entity;

namespace Shelf
{
    public partial class frmMain : Form
    {
        private bool IsRefreshing = false;
        private bool IsFetchingMedia = false;
        private string AuthCode = "";
        private string PublicTkn = "";

        private DataTable DTAnime = new DataTable();

        // Public Properties
        public Form ConfigForm { get; set; } = null; // Config form

        public frmMain()
        {
            InitializeComponent();
            AnilistRequest.Initialize(); // Initialize config
            GlobalFunc.InitializedApp();

            // Initialize objects
            InitializeObjects();
        }
        private void InitializeObjects()
        {
            var colId = new DataColumn()
            {
                ColumnName = "colId",
                Caption = "Id",
                DataType = typeof(long)
            };

            var colTitle = new DataColumn()
            {
                ColumnName = "colTitle",
                Caption = "Romaji Title",
                DataType = typeof(String)
            };

            DTAnime.Columns.AddRange(new DataColumn[] { colId, colTitle });

            gridAnime.DataSource = DTAnime;
        }
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
        #region Custom Events
        #endregion
        private void frmMain_Load(object sender, EventArgs e)
        {
            cbMedia.Items.AddRange(new string[] { "ALL", "ANIME", "MANGA" });
            cbMedia.SelectedIndex = 0;
            Log("Click on 'Refresh Token' to start!");
            //btnRefresh.PerformClick(); // Fetch access code and token
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            gridAnime.Width = this.ClientRectangle.Width - gridAnime.Left - 16;
            gridAnime.Height = this.ClientRectangle.Height - gridAnime.Top - 16;
            gridAnime.Columns[0].Width = (int)(gridAnime.Width * 0.2); // Id
            gridAnime.Columns[1].Width = (int)(gridAnime.Width * 0.65); // Romaji Title
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!IsRefreshing)
            {
                // Get Public Token
                var form = new frmGetAuthCode();
                form.ShowDialog(this);
                AuthCode = form.AuthCode;
                form.Dispose();

                IsRefreshing = true;
                btnRefresh.Enabled = false;

                Log("Requesting token..");
                // Request Access Token, using Auth code
                PublicTkn = await AnilistRequest.RequestPublicToken(AuthCode);
                //Log($"Validating Access Code: [{AuthCode}]");
                Log((!String.IsNullOrWhiteSpace(PublicTkn) ? "Token refreshed!" : "No Public Token!"));

                // Re-enable button
                btnRefresh.Enabled = true;
                IsRefreshing = false;
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
                AnilistAnimeManga anilistMedia = null;
                string media = "";
                if (!String.IsNullOrWhiteSpace(PublicTkn))
                {
                    // What type of media?
                    if (cbMedia.SelectedIndex > 0)
                    {
                        media = cbMedia.Text;
                        // Get media, and write to json file
                        anilistMedia = await AnilistRequest.RequestMediaList(PublicTkn, txtUsername.Text, media);
                        GlobalFunc.WriteMediaJsonToFile(media, anilistMedia);
                        Log(anilistMedia == null ? $"No {media} found!" : $"{media} files written!");
                    }
                    else
                    {
                        // Get media, and write to json file
                        media = "ANIME";
                        Log($"Requesting {media}...");
                        anilistMedia = await AnilistRequest.RequestMediaList(PublicTkn, txtUsername.Text, media);
                        Log($"{media} data fetched!");
                        GlobalFunc.WriteMediaJsonToFile(media, anilistMedia);
                        Log(anilistMedia == null ? $"No {media} found!" : $"{media} files written!");
                        media = "MANGA";
                        Log($"Requesting {media}...");
                        anilistMedia = await AnilistRequest.RequestMediaList(PublicTkn, txtUsername.Text, media);
                        Log($"{media} data fetched!");
                        GlobalFunc.WriteMediaJsonToFile(media, anilistMedia);
                        Log(anilistMedia == null ? $"No {media} found!" : $"{media} files written!");
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

        private void btnMALExport_Click(object sender, EventArgs e)
        {
            btnMALExport.Enabled = false;
            string username = txtUsername.Text;
            string media = "";
            string outputAnime = "";
            string outputManga = "";
            string outputAnimeNonMal = "";
            string outputMangaNonMal = "";
            bool processAnime = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 1;
            bool processManga = cbMedia.SelectedIndex == 0 || cbMedia.SelectedIndex == 2;

            if (processAnime)
            {
                try
                {
                    outputAnime = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_{DateTime.Now.ToString("yyyy-MM-dd")}.xml");
                    outputAnimeNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"anime_NonMal_{DateTime.Now.ToString("yyyy-MM-dd")}.json");
                    GlobalFunc.WriteFile(outputAnime, "");
                }
                catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot create Anime output!"); return; }
            }
            if (processManga)
            {
                try
                {
                    outputManga = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_{DateTime.Now.ToString("yyyy-MM-dd")}.xml");
                    outputMangaNonMal = Path.Combine(GlobalFunc.DIR_OUTPUT, $"manga_NonMal_{DateTime.Now.ToString("yyyy-MM-dd")}.json");
                    GlobalFunc.WriteFile(outputManga, "");
                }
                catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot create Manga output!"); return; }
            }

            var form = new frmLoading("Creating MAL export..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                // ANIME
                if (processAnime)
                {
                    media = "ANIME";
                    form.Message = $"Processing {media}..";
                    var anime = MediaTasks.GetAnimeList().Result;
                    if (anime != null)
                    {
                        MediaTasks.ProcessMedia(anime, "anime", outputAnime, username, outputAnimeNonMal).Wait();
                        anime.Clear();
                    }
                    form.Message = $"Done {media} entries!";
                    // End of Anime entries
                }
                // MANGA
                if (processManga)
                {
                    media = "MANGA";
                    form.Message = $"Processing {media}..";
                    var manga = MediaTasks.GetMangaList().Result;
                    if (manga != null)
                    {
                        MediaTasks.ProcessMedia(manga, "manga", outputManga, username, outputMangaNonMal).Wait();
                        manga.Clear();
                    }
                    form.Message = $"Done {media} entries!";
                    // End of Manga entries
                }
            };
            form.ShowDialog(this);
            GlobalFunc.Alert("Done!");
            btnMALExport.Enabled = true;
        }

        private void btnGenTachi_Click(object sender, EventArgs e)
        {
            List<string> ext = new List<string>();
            ext.AddRange(new string[]{ "proto", "gz" });
            string file = txtTachi.Text.Trim();
            if (File.Exists(file))
            {
                try
                {
                    if (ext.Contains(Path.GetExtension(file).Trim('.')))
                    {
                        var form = new frmLoading("Generating Tachiyomi backup..", "Loading");
                        form.BackgroundWorker.DoWork += (sender1, e1) =>
                        {
                            MediaTasks.GenerateMissingTachiEntries(file).Wait();
                        };
                        form.ShowDialog(this);
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
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            DTAnime.Clear();
            var form = new frmLoading("Refreshing Anime list..", "Loading");
            form.BackgroundWorker.DoWork += async (sender1, e1) =>
            {
                var anime = await MediaTasks.GetAnimeList();
                foreach (var item in anime)
                {
                    this.BeginInvoke((Action)delegate
                    {
                        DTAnime.Rows.Add(item.Media.Id, item.Media.Title.Romaji);
                    });
                }
            };
            form.ShowDialog(this);
            gridAnime.Sort(gridAnime.Columns[1], ListSortDirection.Ascending);
            gridAnime.Refresh();
        }
    }
}
