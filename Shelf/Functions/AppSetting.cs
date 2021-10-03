using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Shelf.Views;

namespace Shelf.Functions
{
    public static class AppSettings
    {
        public static AppSettingsEntity AppConfig { get; set; } = null;
        public static List<AppSettingsListEntity> AppConfigList { get; set; } = new List<AppSettingsListEntity>();
        public static List<String> RequiredRestart { get; set; } = new List<string>();

        public static void Save()
        {
            string jsonfile = GlobalFunc.FILE_APPCONFIG;
            try
            {
                GlobalFunc.JsonEncode(AppConfig, jsonfile);
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public static void Load()
        {
            string jsonfile = GlobalFunc.FILE_APPCONFIG;
            try
            {
                AppConfig = GlobalFunc.JsonDecode<AppSettingsEntity>(jsonfile);
                // TODO: Load from Resources files: 'requiredRestart.txt'
                // and add them to 'RequiredRestart' list.
            }
            catch (Exception ex) { Logs.Err(ex); }
            if (AppConfig == null)
            {
                AppConfig = new AppSettingsEntity();
                AppConfig.DefaultValues();
                GlobalFunc.JsonEncode(AppConfig, jsonfile);
            }

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
                        item.Restart = RequiredRestart.Contains(prop.Name);
                        AppConfigList.Add(item); // Add to List for Data Binding.

                        // Log to debug
                        //var content = JsonConvert.SerializeObject(item, Formatting.Indented);
                        //Logs.Debug($"Item added:\n{content}");
                    }
                }
                // TODO: Add button on main form to show settings
                // and remove below code.
                var form = new frmSettings();
                form.Show();
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
        public static bool setValueLoop(string name, object defValue)
        {
            bool success = false;
            string debugTime = "ffffff";
            Logs.Debug($"ForEach Loop {DateTime.Now.ToString(debugTime)}");
            try
            {
                PropertyInfo[] props = typeof(AppSettingsEntity).GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop != null)
                    {
                        if (prop.Name.Equals(name))
                        {
                            prop.SetValue(AppConfig, defValue);
                            success = true;
                            break;
                        }
                    }
                    //var attr = (JsonPropertyAttribute)prop.GetCustomAttribute(typeof(JsonPropertyAttribute));
                    //if (attr != null)
                    //{
                    //    Logs.Debug($"Property: {prop.Name}");
                    //    Logs.Debug($"Value: {attr.PropertyName}");
                    //    if (attr.PropertyName.Equals(name))
                    //    {
                    //        prop.SetValue(AppConfig, defValue);
                    //        return true;
                    //    }
                    //}
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
            Logs.Debug($"ForEach Loop done {DateTime.Now.ToString(debugTime)}");
            return success;
        }
    }
    public class AppSettingsListEntity
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public bool Restart { get; set; }
    }
    public class AppSettingsEntity
    {
        public void DefaultValues()
        {
            tachibackup = "";
            isAlwaysDownloadCover = false;
        }
        
        #region Properties
        [DisplayName("Tachiyomi Backups Location"), Description("Folder Location of Tachiyomi backups")]
        public string tachibackup { get; set; }

        [DisplayName("Always Download Cover"), Description("Always download cover from Anilist")]
        public bool isAlwaysDownloadCover { get; set; }
        #endregion
    }
}
