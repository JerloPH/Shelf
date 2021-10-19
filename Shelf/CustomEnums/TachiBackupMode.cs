
using System.ComponentModel;

namespace Shelf.CustomEnums
{
    public enum TachiBackupMode
    {
        [Description("Default")]
        Default = 0,
        [Description("Include JSON format")]
        WithJson = 1,
        [Description("Include Extra files")]
        WithExtra = 2
    }
}
