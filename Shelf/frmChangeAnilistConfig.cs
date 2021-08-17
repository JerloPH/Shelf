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
using Shelf.Functions;

namespace Shelf
{
    public partial class frmChangeAnilistConfig : Form
    {
        private Form Caller = null;
        public frmChangeAnilistConfig(Form parent)
        {
            InitializeComponent();
            Caller = parent;
        }

        private void frmChangeAnilistConfig_Load(object sender, EventArgs e)
        {
            txtClientId.Text = AnilistRequest.GetConfig(0);
            txtClientSecret.Text = AnilistRequest.GetConfig(1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtClientId.Text))
            {
                GlobalFunc.Alert("No Client Id!", this);
                txtClientId.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtClientSecret.Text))
            {
                GlobalFunc.Alert("No Client Secret!", this);
                txtClientSecret.Focus();
                return;
            }
            if (AnilistRequest.UpdateConfig(txtClientId.Text, txtClientSecret.Text))
            {
                GlobalFunc.Alert("Successfully saved!", this);
                Close();
            }
            else
                GlobalFunc.Alert("Invalid Config!\nCheck your credentials.", this);
        }

        private void frmChangeAnilistConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Caller != null && Caller is frmMain)
            {
                var form = Caller as frmMain;
                form.ConfigForm = null;
            }
        }
    }
}
