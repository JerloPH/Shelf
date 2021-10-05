using Shelf.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Shelf.Functions
{
    public static class UIHelper
    {
        public static void PopulateCombobox<T>(ComboBox cb)
        {
            // Populate ComboBox from Enum with Description
            try
            {
                cb.DisplayMember = "Description";
                cb.ValueMember = "Value";
                cb.DataSource = System.Enum.GetValues(typeof(T))
                    .Cast<T>()
                    .Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                        value
                    })
                    .OrderBy(item => item.value)
                    .ToList();
                cb.SelectedIndex = 0;
            }
            catch { throw; }
        }
        public static void SetupListViewAndImgList(ListView grid, ImageList list)
        {
            list.ImageSize = new Size(120, 180);
            list.ColorDepth = ColorDepth.Depth32Bit;
            grid.LargeImageList = list;
            grid.View = View.LargeIcon;
            grid.Sorting = SortOrder.Ascending;
        }
        public static void BindComboBoxToDataSource<T>(ComboBox ctrl, List<T> data)
        {
            try
            {
                BindingList<T> localBindList = null;
                if (data?.Count > 0)
                {
                    localBindList = new BindingList<T>(data);
                }
                else
                    localBindList = new BindingList<T>(new List<T>());

                var source = new BindingSource(data, null);
                ctrl.DataSource = source;
            }
            catch { throw; }
        }
        public static void BindLocalMediaToDataGrid<T>(DataGridView grid, List<T> data, string[] headers)
        {
            try
            {
                BindingList<T> localBindList = null;
                if (data?.Count > 0)
                {
                    localBindList = new BindingList<T>(data);
                }
                else
                    localBindList = new BindingList<T>(new List<T>());

                var source = new BindingSource(data, null);
                grid.DataSource = source;
                if (headers != null)
                {
                    if (headers.Length > 0)
                    {
                        grid.Columns[0].HeaderText = headers[0];
                        if (headers.Length > 1)
                        {
                            for (int count = 1; count < headers.Length; count++)
                            {
                                grid.Columns[count].HeaderText = headers[count];
                            }
                        }
                    }
                }
            }
            catch { throw; }
        }
    }
}
