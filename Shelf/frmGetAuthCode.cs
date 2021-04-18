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
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Shelf.Functions;
using Shelf.Json;
using Shelf.Anilist;

namespace Shelf
{
    public partial class frmGetAuthCode : Form
    {
        public string publicToken { get; set; } = "";
        private string ANICLIENT = "";
        private string ANISECRET = "";
        private string redirect_uri = "";
        private string AnilistUrl = "";
        public frmGetAuthCode()
        {
            InitializeComponent();
            // Initialize vars
            ANICLIENT = AnilistRequest.GetConfig();
            ANISECRET = AnilistRequest.GetConfig(1);
            redirect_uri = AnilistRequest.RedirectUrl;
            //MessageBox.Show(ANICLIENT + "\n[" + ANISECRET + "]");
            AnilistUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={ANICLIENT}&redirect_uri={redirect_uri}&response_type=code";

            btnOK.DialogResult = DialogResult.OK;
            InitializeAsync();
            this.FormClosing += frmGetAccessTkn_FormClosing;
        }
        #region Custom Events and Functions
        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri(AnilistUrl);
        }
        #endregion
        private void frmGetAccessTkn_FormClosing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                if (String.IsNullOrWhiteSpace(txtAuthCode.Text))
                {
                    GlobalFunc.Alert("Access Token textbox is empty!");
                    txtAuthCode.Focus();
                    e.Cancel = true;
                    return;
                }
                publicToken = txtAuthCode.Text;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmGetAccessTkn_Resize(object sender, EventArgs e)
        {
            txtAuthCode.Width = btnOK.Left - 16;
            webView.Width = this.Width - 16;
            webView.Height = txtAuthCode.Top - 48;
        }
    }
}
