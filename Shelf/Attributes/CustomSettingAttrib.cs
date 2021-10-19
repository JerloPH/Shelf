using System;
using Shelf.CustomEnums;

namespace Shelf.Attributes
{
    public class CustomSettingAttrib : Attribute
    {
        public bool IsRequiredRestart { get; set; } = false;

        public SettingType settingType { get; set; } = SettingType.Default;
    }
}
