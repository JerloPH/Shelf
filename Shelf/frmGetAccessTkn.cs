﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Shelf.Functions;
using Shelf.Json;

namespace Shelf
{
    public partial class frmGetAccessTkn : Form
    {
        public string accessToken { get; set; } = "";
        private string ANICLIENT = "";
        private string ANISECRET = "";
        private string redirect_uri = @"https://anilist.co/api/v2/oauth/pin";
        private string AnilistUrl = "";
        public frmGetAccessTkn()
        {
            InitializeComponent();
            // Initialize vars
            btnOK.DialogResult = DialogResult.OK;
            AnilistUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={ANICLIENT}&redirect_uri={redirect_uri}&response_type=code";
            try
            {
                string content = GlobalFunc.ReadFromFile("");
                var jsonConfig = JsonConvert.DeserializeObject<AnilistConfig>(content);
                ANICLIENT = jsonConfig.clientId;
                ANISECRET = jsonConfig.clientSecret;
            }
            catch { }
            // MessageBox.Show(AnilistUrl);
            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
        }
        private async Task NavigateTo(string url)
        {
            //await webView.EnsureCoreWebView2Async();
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.Source = new Uri(url);
                //webView.CoreWebView2.Navigate(AnilistUrl);
            }
        }
        private  async void webView_Click(object sender, EventArgs e)
        {
            await NavigateTo(AnilistUrl);
        }
    }
}
