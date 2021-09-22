using Shelf.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static void BindLocalMediaToDataGrid(DataGridView grid, List<LocalMediaPaths> data)
        {
            try
            {
                BindingList<LocalMediaPaths> localMangaBindList = null;
                if (data?.Count > 0)
                {
                    localMangaBindList = new BindingList<LocalMediaPaths>(data);
                }
                else
                    localMangaBindList = new BindingList<LocalMediaPaths>(new List<LocalMediaPaths>());

                var source = new BindingSource(localMangaBindList, null);
                grid.DataSource = source;
                grid.Columns[0].HeaderText = "Path";
                grid.Columns[1].HeaderText = "Separate source";
            }
            catch { throw; }
        }
    }
}
