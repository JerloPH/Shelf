using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Shelf.Enum;
using Shelf.Views;

namespace Shelf.Functions
{
    public static class AppSettings
    {
        public static AppSettingsEntity AppConfig { get; set; } = null;
        public static List<AppSettingsListEntity> AppConfigList { get; set; } = new List<AppSettingsListEntity>();
        public static List<String> AppConfigRequiredRestart { get; set; } = new List<string>();

        public static void SaveAppConfig()
        {
            string jsonfile = GlobalFunc.FILE_APPCONFIG;
            try
            {
                GlobalFunc.JsonEncode(AppConfig, jsonfile);
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public static void LoadAppConfig(bool isLoadForm = false)
        {
            string jsonfile = GlobalFunc.FILE_APPCONFIG;
            try
            {
                AppConfig = GlobalFunc.JsonDecode<AppSettingsEntity>(jsonfile); // Load settings
                // Add settings properties name that requires restart on change.
                AppConfigRequiredRestart.Clear();
                string file = Path.Combine(GlobalFunc.DIR_RES, "requiredRestart.txt");
                string content = GlobalFunc.ReadFromFile(file);
                if (!String.IsNullOrWhiteSpace(content))
                {
                    string[] items = content.Split('*');
                    AppConfigRequiredRestart.AddRange(items);
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
            if (AppConfig == null)
            {
                AppConfig = new AppSettingsEntity();
                AppConfig.DefaultValues();
                GlobalFunc.JsonEncode(AppConfig, jsonfile);
            }
            // Bind settings properties to 'AppConfigList', for use in 'DataGridView' in 'frmSettingss'
            try
            {
                AppConfigList.Clear();
                foreach (PropertyInfo prop in typeof(AppSettingsEntity).GetProperties())
                {
                    if (prop != null)
                    {
                        var item = new AppSettingsListEntity();
                        item.Name = prop.Name;
                        item.Value = prop.GetValue(AppConfig, null);
                        // Get Display Name and Description values
                        MemberInfo mInfo = typeof(AppSettingsEntity).GetProperty(prop.Name);
                        var displayNameAttr = mInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                            .Cast<DisplayNameAttribute>().Single();
                        var descAttr = mInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)
                            .Cast<DescriptionAttribute>().Single();
                        item.Caption = displayNameAttr.DisplayName;
                        item.Description = descAttr.Description;
                        item.Restart = AppConfigRequiredRestart.Contains(prop.Name);
                        item.settingType = SettingType.Default;
                        // Set type special cases
                        if (item.Name.Equals("tachibackup"))
                            item.settingType = SettingType.Directory;

                        AppConfigList.Add(item); // Add to List for Data Binding.

                        // Log to debug
                        //var content = JsonConvert.SerializeObject(item, Formatting.Indented);
                        //Logs.Debug($"Item added:\n{content}");
                    }
                }
                // TODO: Add button on main form to show settings
                // and remove below code.
                if (isLoadForm)
                {
                    var form = new frmSettings();
                    form.Show();
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public static bool setValue(string name, object defValue)
        {
            bool success = false;
            string debugTime = "ffffff";
            Logs.Debug($"LINQ {DateTime.Now.ToString(debugTime)}");
            try
            {
                PropertyInfo propA = typeof(AppSettingsEntity).GetProperties().Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (propA != null)
                {
                    propA.SetValue(AppConfig, defValue);
                    success = true;
                }
                // Also update binded list
                AppSettingsListEntity item = AppConfigList.Where(x => x.Name.Equals(name)).First();
                if (item != null)
                    item.Value = defValue;
            }
            catch (Exception ex) { Logs.Err(ex); }
            Logs.Debug($"LINQ done {DateTime.Now.ToString(debugTime)}");
            return success;
        }
        public static Type getType(string name)
        {
            try
            {
                PropertyInfo propA = typeof(AppSettingsEntity).GetProperties().Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (propA != null)
                {
                    return propA.PropertyType;
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
            return null;
        }
    }
    public class AppSettingsListEntity
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public bool Restart { get; set; }
        public SettingType settingType { get; set; }
    }
    public class AppSettingsEntity
    {
        public void DefaultValues()
        {
            tachibackup = "";
            isAlwaysDownloadCover = false;
            isAutoFetchTkn = false;
            isAutoRefreshmedia = false;
            isAlwaysUseRomaji = false;
            isAutoSkipTachi = false;
        }
        
        #region Properties
        [DisplayName("Tachiyomi Backups Location"), Description("Folder Location of Tachiyomi backups")]
        public string tachibackup { get; set; }

        [DisplayName("Always Download Cover"), Description("Always download cover from Anilist")]
        public bool isAlwaysDownloadCover { get; set; }

        [DisplayName("Automatically Fetch Token"), Description("Fetch token upon Load")]
        public bool isAutoFetchTkn { get; set; }

        [DisplayName("Automatically Refresh Media"), Description("Refresh Media List upon Load")]
        public bool isAutoRefreshmedia { get; set; } // TODO: Apply setting

        [DisplayName("Always Display Romaji"), Description("Only use Romaji title of Entry")]
        public bool isAlwaysUseRomaji { get; set; }

        [DisplayName("Automatically Uncheck status for Tachi Backup"), Description("Uncheck Completed/Dropped on Tachiyomi backup")]
        public bool isAutoSkipTachi { get; set; }
        #endregion
    }
}
