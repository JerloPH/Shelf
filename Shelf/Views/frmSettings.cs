using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shelf.CustomEnums;
using Shelf.Functions;

namespace Shelf.Views
{
    public partial class frmSettings : Form
    {
        private Type selectedType = null;
        private string selectedName = "";
        private string selectedCaption = "";
        private bool requireRestart = false;
        private SettingType selectedSetType = SettingType.Default;
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
                    selectedCaption = row.Cells["colCaption"].Value.ToString();
                    selectedType = AppSettings.getType(selectedName);
                    requireRestart = (bool)row.Cells["colRestart"].Value;
                    selectedSetType = (SettingType)row.Cells["colSetType"].Value;
                    lblDesc.Text = row.Cells["colDesc"].Value.ToString();
                    if (selectedType != null)
                    {
                        if (selectedType.IsEnum)
                        {
  
                            var cb = new ComboBox();
                            int choice = (int)row.Cells["colValue"].Value;
                            cb.DataSource = Enum.GetValues(selectedType)
                                .Cast<Enum>()
                                .Select(value => new
                                {
                                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                                    value
                                })
                                .OrderBy(item => item.value)
                                .ToList();
                            cb.DisplayMember = "Description";
                            cb.ValueMember = "Value";
                            cb.DropDownStyle = ComboBoxStyle.DropDownList;
                            cb.Dock = DockStyle.Fill;
                            panelValue.Controls.Add(cb);
                            cb.SelectedIndex = choice;
                        }
                        else
                        {
                            switch (Type.GetTypeCode(selectedType))
                            {
                                case TypeCode.String:
                                    var ctrl = new TextBox();
                                    ctrl.Dock = DockStyle.Fill;
                                    ctrl.Text = row.Cells["colValue"].Value.ToString();
                                    panelValue.Controls.Add(ctrl);
                                    if (selectedSetType == SettingType.Directory || selectedSetType == SettingType.File)
                                    {
                                        var browse = new Button();
                                        browse.Text = "Browse";
                                        browse.Size = new Size(120, ctrl.Height);
                                        browse.Dock = DockStyle.Right;
                                        browse.Tag = ctrl;
                                        browse.MouseClick += Browse_MouseClick;
                                        panelValue.Controls.Add(browse);
                                    }
                                    break;
                                case TypeCode.Boolean:
                                    var cb = new ComboBox();
                                    bool choice = (bool)row.Cells["colValue"].Value;
                                    cb.Items.AddRange(new string[] { "Yes", "No" });
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
        }

        private void Browse_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                var ctrl = sender as Button;
                if (ctrl?.Tag != null)
                {
                    TextBox txt = ctrl.Tag as TextBox;
                    if (txt != null)
                    {
                        string initialVal = txt.Text;
                        try
                        {
                            txt.Text = GlobalFunc.BrowseForDirectory(selectedCaption, GlobalFunc.DIR_OUTPUT, initialVal, selectedSetType);
                        }
                        catch (Exception ex) { Logs.Err(ex); txt.Text = initialVal; }
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
                    if (selectedType.IsEnum)
                    {
                        var cb = ctrl as ComboBox;
                        int val = cb.SelectedIndex;
                        isSuccess = AppSettings.setValue(selectedName, val);
                    }
                    else
                    {
                        switch (Type.GetTypeCode(selectedType))
                        {
                            case TypeCode.String:
                                var txt = ctrl as TextBox;
                                string value = txt.Text;
                                Logs.Debug($"Name: {selectedName}, Value: {value}");
                                if (selectedSetType != SettingType.Default)
                                    value = value.TrimEnd('\\').TrimEnd('/');

                                isSuccess = AppSettings.setValue(selectedName, value);
                                break;
                            case TypeCode.Boolean:
                                var cb = ctrl as ComboBox;
                                bool choice = cb.SelectedIndex == 0;
                                isSuccess = AppSettings.setValue(selectedName, choice);
                                break;
                        }
                    }
                }
            }
            if (isSuccess)
            {
                gridSetting.Refresh();
                GlobalFunc.Alert("Changes applied!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppSettings.SaveAppConfig();
            if (requireRestart)
            {
                GlobalFunc.Alert("Required App Restart for\nsome Settings to take effect.");
            }
            else
                GlobalFunc.Alert("Changes Saved!");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            AppSettings.LoadAppConfig();
            gridSetting.Refresh();
        }

        private void gridSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int index = gridSetting.Columns["colValue"].Index;
            string name = selectedName;
            if (e.ColumnIndex == index)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Yes" : "No";
                    e.FormattingApplied = true;
                }
                else if (e.Value is Enum)
                {
                    e.Value = ((int)e.Value).ToString();
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
