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
using JerloPH_CSharp;

namespace Shelf.Views
{
    public partial class frmSettings : Form
    {
        private Type selectedType = null;
        private string selectedName = "";
        private string selectedCaption = "";
        private bool requireRestart = false;
        private SettingType selectedSetType = SettingType.Default;
        private string[] booleanChoice = new string[] { "Yes", "No" };
        private Font fontValueSet;

        public frmSettings()
        {
            InitializeComponent();
            this.Icon = Shelf.Properties.Resources.MainAppIcon;
            UIHelper.BindLocalMediaToDataGrid(gridSetting, AppSettings.AppConfigList, null);
            gridSetting.RowHeadersVisible = false;
            // Set font
            fontValueSet = btnApply.Font;
            // Set margins
            panelValue.Padding = new Padding(20, 0, 20, 0);
            btnApply.Padding = new Padding(20, 0, 20, 0);

            panelAction.Padding = new Padding(0, 6, 0, 0);
            // Set dock style
            btnApply.Dock = DockStyle.Right;
            // Add controls to their respective panels
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
                            cb.Font = fontValueSet;
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
                                        browse.Size = new Size(150, ctrl.Height);
                                        browse.Dock = DockStyle.Right;
                                        browse.Tag = ctrl;
                                        browse.MouseClick += Browse_MouseClick;
                                        browse.Padding = new Padding(10, 0, 10, 0);
                                        browse.Font = new Font(fontValueSet.Name, fontValueSet.Size - 2);
                                        panelValue.Controls.Add(browse);
                                    }
                                    break;
                                case TypeCode.Boolean:
                                    var cb = new ComboBox();
                                    bool choice = (bool)row.Cells["colValue"].Value;
                                    cb.Items.AddRange(booleanChoice);
                                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                    cb.Dock = DockStyle.Fill;
                                    cb.SelectedIndex = choice ? 0 : 1;
                                    cb.Font = fontValueSet;
                                    panelValue.Controls.Add(cb);
                                    break;
                            }
                        }
                    }
                    // Resize Apply button to match controls on 'value' panel
                    btnApply.Height = panelAction.Controls[0].Height;
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
                Msg.ShowInfo("Changes applied!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppSettings.SaveAppConfig();
            Msg.ShowInfo(requireRestart ? "Required App Restart for\nsome Settings to take effect." : "Changes Saved!");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            AppSettings.LoadAppConfig();
            gridSetting.Refresh();
        }

        private void gridSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                int index = gridSetting.Columns["colValue"].Index;
                if (e.ColumnIndex == index)
                {
                    if (e.Value is bool)
                    {
                        bool value = (bool)e.Value;
                        e.Value = (value) ? booleanChoice[0] : booleanChoice[1];
                        e.FormattingApplied = true;
                    }
                    else if (e.Value is Enum)
                    {
                        e.Value = GlobalFunc.GetEnumDesc((Enum)e.Value);
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
        }

        private void gridSetting_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = gridSetting.Columns["colValue"].Index;
                if (e.ColumnIndex == index && e.RowIndex > -1)
                {
                    DataGridViewCell cell = this.gridSetting.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    object val = cell.Value;
                    if (val is bool)
                    {
                        bool value = (bool)val;
                        cell.ToolTipText = (value) ? booleanChoice[0] : booleanChoice[1];
                    }
                    else if (val is Enum)
                    {
                        cell.ToolTipText = GlobalFunc.GetEnumDesc((Enum)val);
                    }
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
        }

    }
}
