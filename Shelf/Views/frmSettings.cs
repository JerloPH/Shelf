using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shelf.Functions;

namespace Shelf.Views
{
    public partial class frmSettings : Form
    {
        private Type selectedType = null;
        private string selectedName = "";
        private bool requireRestart = false;
        public frmSettings()
        {
            InitializeComponent();
            UIHelper.BindLocalMediaToDataGrid(gridSetting, AppSettings.AppConfigList, null);
            gridSetting.RowHeadersVisible = false;
            panelValueSet.Controls.Add(panelValue);
            panelValueSet.Controls.Add(btnApply);
            panelAction.Controls.Add(btnSave);
            panelAction.Controls.Add(btnReset);
        }

        private void gridSetting_SelectionChanged(object sender, EventArgs e)
        {
            if (gridSetting.SelectedRows.Count > 0)
            {
                var row = gridSetting.SelectedRows[0];
                if (row != null)
                {
                    panelValue.Controls.Clear(); // Clear previous controls
                    selectedName = row.Cells["colName"].Value.ToString();
                    selectedType = AppSettings.getType(selectedName);
                    requireRestart = (bool)row.Cells["colRestart"].Value;
                    lblDesc.Text = row.Cells["colDesc"].Value.ToString();
                    if (selectedType != null)
                    {
                        switch (Type.GetTypeCode(selectedType))
                        {
                            case TypeCode.String:
                                var ctrl = new TextBox();
                                ctrl.Dock = DockStyle.Fill;
                                ctrl.Text = row.Cells["colValue"].Value.ToString();
                                panelValue.Controls.Add(ctrl);
                                break;
                            case TypeCode.Boolean:
                                var cb = new ComboBox();
                                bool choice = (bool)row.Cells["colValue"].Value;
                                cb.Items.AddRange(new string[] { "Yes", "No"});
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                cb.Dock = DockStyle.Fill;
                                cb.SelectedIndex = choice ? 0 : 1;
                                panelValue.Controls.Add(cb);
                                break;
                        }
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            if (panelValue.Controls.Count > 0)
            {
                var ctrl = panelValue.Controls[0];
                if (ctrl != null)
                {
                    switch (Type.GetTypeCode(selectedType))
                    {
                        case TypeCode.String:
                            var txt = ctrl as TextBox;
                            Logs.Debug($"Name: {selectedName}, Value: {txt.Text}");
                            isSuccess = AppSettings.setValue(selectedName, txt.Text);
                            break;
                        case TypeCode.Boolean:
                            var cb = ctrl as ComboBox;
                            bool choice = cb.SelectedIndex == 0;
                            isSuccess = AppSettings.setValue(selectedName, choice);
                            break;
                    }
                }
            }
            if (isSuccess)
            {
                AppSettings.Save();
                gridSetting.Refresh();
                Logs.Debug($"New value: {AppSettings.AppConfig.tachibackup}");
                if (requireRestart)
                {
                    GlobalFunc.Alert("Required App Restart for\nsome Settings to take effect.");
                }
                else
                    GlobalFunc.Alert("Changes applied!");
            }
        }
    }
}
