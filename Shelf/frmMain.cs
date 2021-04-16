using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shelf.Anilist;

namespace Shelf
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Task<string> result;
            string userId;

            txtLog.AppendText($"Trying to get User ID with username '{txtUsername.Text}'\r\n");
            result = AnilistRequest.RequestUserID(txtUsername.Text);
            userId = result.Result;
            txtLog.AppendText(String.IsNullOrWhiteSpace(userId) ? "No user Id!\n\r" : $"User ID found! [{userId}]\r\n");
        }
    }
}
