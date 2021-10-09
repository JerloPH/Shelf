using System;
using Shelf.Enum;

namespace Shelf.Attributes
{
    public class CustomSettingAttrib : Attribute
    {
        public bool IsRequiredRestart { get; set; } = false;

        public SettingType settingType { get; set; } = SettingType.Default;
    }
}
