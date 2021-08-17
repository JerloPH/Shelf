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

namespace Shelf
{
    public partial class frmMain : Form
    {
        private bool IsRefreshing = false;
        private bool IsFetchingMedia = false;
        private string AuthCode = "";
        private string PublicTkn = "";

        // Public Properties
        public Form ConfigForm { get; set; } = null; // Config form

        public frmMain()
        {
            InitializeComponent();
            AnilistRequest.Initialize(); // Initialize config
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbMedia.Items.AddRange(new string[] { "ALL", "ANIME", "MANGA" });
            cbMedia.SelectedIndex = 0;
            Log("Click on 'Refresh Token' to start!");
            //btnRefresh.PerformClick(); // Fetch access code and token
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
                        anilistMedia = await AnilistRequest.RequestMediaList(PublicTkn, txtUsername.Text, media);
                        GlobalFunc.WriteMediaJsonToFile(media, anilistMedia);
                        Log(anilistMedia == null ? $"No {media} found!" : $"{media} files written!");
                        media = "MANGA";
                        anilistMedia = await AnilistRequest.RequestMediaList(PublicTkn, txtUsername.Text, media);
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
    }
}
