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
        private string Token = "";
        private string AccessToken = "";
        public frmMain()
        {
            InitializeComponent();
            AnilistRequest.Initialize(); // Initialize config
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!IsRefreshing)
            {
                AnilistAnimeManga anilistMedia = null;

                IsRefreshing = true;
                btnRefresh.Enabled = false;

                txtLog.AppendText("Requesting token..\r\n");
                // Request Access Token, using Public Token
                AccessToken = await AnilistRequest.RequestAccessToken(Token);
                txtLog.AppendText($"Validating token: [{AccessToken}]\r\n");

                if (!String.IsNullOrWhiteSpace(AccessToken))
                {
                    txtLog.AppendText("Token granted!\r\n");
                    // Get media, and write to json file
                    anilistMedia = await AnilistRequest.RequestMediaList(AccessToken);
                    if (anilistMedia != null)
                    {
                        GlobalFunc.WriteFile("AnilistMedia.json", JsonConvert.SerializeObject(anilistMedia));
                    }

                    txtLog.AppendText(anilistMedia == null ? "No media!\n\r" : "Media files written!\r\n");
                }
                else
                {
                    txtLog.AppendText("Invalid Token!\r\n");
                }

                // Re-enable button
                btnRefresh.Enabled = true;
                IsRefreshing = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Get Public Token
            var form = new frmGetAccessTkn();
            form.ShowDialog(this);
            Token = form.publicToken;
            form.Dispose();
            if (Debugger.IsAttached)
            {
                //GlobalFunc.Alert($"Public Token: {Token}");
            }
        }
    }
}
