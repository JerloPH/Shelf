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
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!IsRefreshing)
            {
                AnilistAnimeManga anilistMedia = null;
                string userId;

                IsRefreshing = true;
                btnRefresh.Enabled = false;
                txtLog.AppendText($"Trying to get User ID with username '{txtUsername.Text}'\r\n");
                userId = await AnilistRequest.RequestUserID(txtUsername.Text);
                txtLog.AppendText(String.IsNullOrWhiteSpace(userId) ? "No user Id!\n\r" : $"User ID found! [{userId}]\r\n");

                // Get media, and write to json file
                anilistMedia = await AnilistRequest.RequestMediaList(userId);
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
    }
}
