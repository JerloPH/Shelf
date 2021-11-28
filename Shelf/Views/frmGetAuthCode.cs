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
using JerloPH_CSharp;

namespace Shelf
{
    public partial class frmGetAuthCode : Form
    {
        public string AuthCode { get; set; } = "";
        private string ANICLIENT = "";
        private string ANISECRET = "";
        private string redirect_uri = "";
        private string AnilistUrl = "";
        public frmGetAuthCode()
        {
            InitializeComponent();
            this.Icon = Shelf.Properties.Resources.MainAppIcon;
            // Initialize vars
            ANICLIENT = AnilistRequest.GetConfig(0);
            ANISECRET = AnilistRequest.GetConfig(1);
            redirect_uri = AnilistRequest.RedirectUrl;
            AnilistUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={ANICLIENT}&redirect_uri={redirect_uri}&response_type=code";
            //AnilistUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={ANICLIENT}&response_type=token"; // Implicit grant, public list

            btnOK.DialogResult = DialogResult.OK;
            InitializeAsync();
        }
        #region Custom Events and Functions
        async void InitializeAsync()
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);
                webView.Source = new Uri(AnilistUrl);
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        #endregion
        private void frmGetAuthCode_FormClosing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                if (String.IsNullOrWhiteSpace(txtAuthCode.Text))
                {
                    if (!Msg.ShowYesNo("Access Token textbox is empty!\nProceed?", this))
                    {
                        txtAuthCode.Focus();
                        e.Cancel = true;
                        return;
                    }
                }
                AuthCode = txtAuthCode.Text;
            }
            //webView?.Dispose();
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
