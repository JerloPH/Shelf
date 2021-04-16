using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace Shelf
{
    public partial class frmGetAccessTkn : Form
    {
        public string accessToken { get; set; } = "";
        private string redirect_uri = @"https://anilist.co/api/v2/oauth/pin";
        private string AnilistUrl = "";
        public frmGetAccessTkn()
        {
            InitializeComponent();
            btnOK.DialogResult = DialogResult.OK;
            AnilistUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={ANICLIENT}&redirect_uri={redirect_uri}&response_type=code";
            MessageBox.Show(AnilistUrl);
        }
        private async Task NavigateTo(string url)
        {
            await webView.EnsureCoreWebView2Async();
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
