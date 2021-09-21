using System;
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
    }
}
