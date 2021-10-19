
using System.ComponentModel;

namespace Shelf.CustomEnums
{
    public enum MediaEntryMode
    {
        [Description("All")]
        All = 0,
        [Description("MAL Only")]
        MAL = 1,
        [Description("Non-MAL Only")]
        NonMAL = 2
    }
}
