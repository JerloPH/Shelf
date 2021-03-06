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
using Shelf.CustomEnums;
using Shelf.Views;
using Shelf.Attributes;
using JerloPH_CSharp;

namespace Shelf.Functions
{
    public static class AppSettings
    {
        public static AppSettingsEntity AppConfig { get; set; } = null;
        public static List<AppSettingsListEntity> AppConfigList { get; set; } = new List<AppSettingsListEntity>();

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
                if (File.Exists(jsonfile))
                    AppConfig = GlobalFunc.JsonDecode<AppSettingsEntity>(jsonfile); // Load settings
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
                        item.settingType = SettingType.Default;
                        try
                        {
                            object[] attribute = prop.GetCustomAttributes(typeof(CustomSettingAttrib), true);
                            if (attribute.Length > 0)
                            {
                                var attribCustom = (CustomSettingAttrib)attribute[0];
                                item.Restart = attribCustom.IsRequiredRestart;
                                item.settingType = attribCustom.settingType;
                            }
                        }
                        catch (Exception ex) { Logs.Err(ex); }

                        AppConfigList.Add(item); // Add to List for Data Binding.

                        // Log to debug
                        //var content = JsonConvert.SerializeObject(item, Formatting.Indented);
                        //Logs.Debug($"Item added:\n{content}");
                    }
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
            isAutoFetchToken = false;
            isSaveToken = false;
            isAutoRefreshmedia = false;
            isAlwaysUseRomaji = false;
            isAutoSkipTachi = false;
            tachiBackupMode = TachiBackupMode.Default;
        }

        #region Properties
        [DisplayName("Always Download Cover"), Description("Always download cover from Anilist")]
        public bool isAlwaysDownloadCover { get; set; }

        [DisplayName("Automatically Fetch Token"), Description("Fetch token upon loading")]
        public bool isAutoFetchToken { get; set; }

        [DisplayName("Automatically Refresh Media"), Description("Refresh Media List after fetching Token")]
        public bool isAutoRefreshmedia { get; set; }

        [DisplayName("Load and Save Token"), Description("Load/Save Token after fetching"),
            CustomSettingAttrib(IsRequiredRestart = true)]
        public bool isSaveToken { get; set; }

        [DisplayName("Always Display Romaji"), Description("Only use Romaji title of Entry")]
        public bool isAlwaysUseRomaji { get; set; }

        [DisplayName("Tachiyomi Backups Location"), Description("Folder Location of Tachiyomi backups"),
            CustomSettingAttrib(settingType = SettingType.Directory, IsRequiredRestart = true)]
        public string tachibackup { get; set; }

        [DisplayName("Tachiyomi Backup files"), Description("Select which files will be generated")]
        public TachiBackupMode tachiBackupMode { get; set; }

        [DisplayName("Automatically Uncheck status for Tachi Backup"), Description("Uncheck Completed/Dropped on Tachiyomi backup")]
        public bool isAutoSkipTachi { get; set; }
        #endregion
    }
}
