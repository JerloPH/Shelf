using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Shelf.Anilist;
using Shelf.Json;

namespace Shelf
{
    public partial class frmMain : Form
    {
        private bool IsRefreshing = false;
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
                string accessToken = "";

                IsRefreshing = true;
                btnRefresh.Enabled = false;

                // Get media, and write to json file
                anilistMedia = await AnilistRequest.RequestMediaList(accessToken);
                if (anilistMedia != null)
                {
                    using (StreamWriter sw = new StreamWriter("AnilistMedia.json"))
                    {
                        sw.Write(JsonConvert.SerializeObject(anilistMedia));
                    }
                }

                txtLog.AppendText(anilistMedia == null ? "No media!\n\r" : "Media files written!\r\n");
                // Re-enable button
                btnRefresh.Enabled = true;
                IsRefreshing = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Get Access Token
            var form = new frmGetAccessTkn();
            form.ShowDialog(this);
            AccessToken = form.accessToken;
            form.Dispose();
        }
    }
}
