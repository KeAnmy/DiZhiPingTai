using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ZYSK.DZPT.Base.Utility.Tools
{
  public static  class ConfigUtil
    {
        private static NameValueCollection a = ConfigurationManager.AppSettings;

        private static ConnectionStringSettingsCollection b = ConfigurationManager.ConnectionStrings;

        /// <summary>
        /// 根据key的值从config设置文件中得到相对应的值.
        /// <para>
        /// 如果没有找到,返回null;
        /// </para>
        /// <para>
        /// 如果在value中发现有|DataDirectory|值,则使用网站根目录的App_Data目录地址来替换.
        /// </para>
        /// </summary>
        /// <param name="key">要查找的AppSettings键值对中的键</param>
        /// <returns>要查找的AppSettings键值对中的值</returns>
        public static string GetAppSettings(string key)
        {
            int num = 5;
            while (true)
            {
                switch (num)
                {
                    case 0:
                        if (ConfigUtil.a[key] == null)
                        {
                            num = 2;
                            continue;
                        }
                        num = 7;
                        continue;
                    case 1:
                        goto IL_7F;
                    case 2:
                        goto IL_77;
                    case 3:
                        goto IL_52;
                    case 4:
                        goto IL_F9;
                    case 6:
                        num = 0;
                        continue;
                    case 7:
                        if (ConfigUtil.a[key].IndexOf("|DataDirectory|") < 0)
                        {
                            num = 4;
                            continue;
                        }
                        if (true)
                        {
                        }
                        num = 3;
                        continue;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    num = 6;
                    continue;
                }
            IL_77:
                num = 1;
            }
        IL_52:
            return ConfigUtil.a[key].Replace("|DataDirectory|", ((HttpContext.Current != null) ? HttpContext.Current.Server.MapPath("~") : AppDomain.CurrentDomain.BaseDirectory) + "\\App_Data");
        IL_7F:
            return null;
        IL_F9:
            return ConfigUtil.a[key];
        }

        /// <summary>
        /// 根据key的值从config设置文件中得到相对应的值,如果没有找到,返回null;
        /// </summary>
        /// <param name="key">要查找的ConnectionString键值对中的键</param>
        /// <returns>要查找的ConnectionString键值对中的值</returns>
        public static ConnectionStringSettings GetConnectionSettings(string key)
        {
            int num = 4;
            while (true)
            {
                switch (num)
                {
                    case 0:
                        goto IL_67;
                    case 1:
                        num = 2;
                        continue;
                    case 2:
                        if (ConfigUtil.b[key] == null)
                        {
                            num = 3;
                            continue;
                        }
                        num = 0;
                        continue;
                    case 3:
                        goto IL_5B;
                }
                if (string.IsNullOrEmpty(key))
                {
                    break;
                }
                if (true)
                {
                }
                num = 1;
            }
        IL_5B:
            return null;
        IL_67:
            string connectionString = ConfigUtil.b[key].ConnectionString.Replace("|DataDirectory|", ((HttpContext.Current != null) ? HttpContext.Current.Server.MapPath("~") : AppDomain.CurrentDomain.BaseDirectory) + "\\App_Data");
            return new ConnectionStringSettings(key, connectionString, ConfigUtil.b[key].ProviderName);
        }

        /// <summary>
        /// 更新(或添加)配置文件app.config的appSettings节的信息
        /// </summary>
        /// <param name="key">要更新的配置文件的key</param>
        /// <param name="value">要更新的配置文件的key的value值</param>
        public static void SaveAppSettings(string key, string value)
        {
            Configuration configuration;
            while (true)
            {
                configuration = null;
                int num = 7;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (configuration.AppSettings.Settings[key] == null)
                            {
                                num = 5;
                                continue;
                            }
                            configuration.AppSettings.Settings[key].Value = value;
                            num = 3;
                            continue;
                        case 1:
                            goto IL_65;
                        case 2:
                            configuration = WebConfigurationManager.OpenWebConfiguration("~");
                            num = 1;
                            continue;
                        case 3:
                            goto IL_C7;
                        case 4:
                            goto IL_60;
                        case 5:
                            configuration.AppSettings.Settings.Add(key, value);
                            num = 4;
                            continue;
                        case 6:
                            goto IL_65;
                        case 7:
                            if (HttpContext.Current != null)
                            {
                                num = 2;
                                continue;
                            }
                            configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            num = 6;
                            continue;
                    }
                    break;
                IL_65:
                    num = 0;
                }
            }
        IL_60:
            goto IL_E5;
        IL_C7:
            if (true)
            {
            }
        IL_E5:
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
            ConfigUtil.a = ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// 更新(或添加)配置文件app.config的connectionSettings节的信息
        /// </summary>
        /// <param name="connectionName">数据库连接名称</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerName">数据库连接提供程序</param>
        public static void SaveConnectionSettings(string connectionName, string connectionString, string providerName)
        {
            Configuration configuration;
            while (true)
            {
                configuration = null;
                int num = 6;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            goto IL_77;
                        case 1:
                            {
                                ConnectionStringSettings connectionStringSettings=new ConnectionStringSettings ();
                                configuration.ConnectionStrings.ConnectionStrings.Add(connectionStringSettings);
                                num = 0;
                                continue;
                            }
                        case 2:
                            {
                                ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings();
                                configuration.ConnectionStrings.ConnectionStrings[connectionName].ProviderName = connectionStringSettings.ProviderName;
                                num = 11;
                                continue;
                            }
                        case 3:
                            {
                                ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings(connectionName, connectionString, providerName);
                                if (true)
                                {
                                }
                                num = 5;
                                continue;
                            }
                        case 4:
                            if (providerName != null)
                            {
                                num = 2;
                                continue;
                            }
                            goto IL_18D;
                        case 5:
                            goto IL_FA;
                        case 6:
                            if (HttpContext.Current != null)
                            {
                                num = 10;
                                continue;
                            }
                            configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            num = 8;
                            continue;
                        case 7:
                            goto IL_FA;
                        case 8:
                            goto IL_16F;
                        case 9:
                            {
                                if (configuration.ConnectionStrings.ConnectionStrings[connectionName] == null)
                                {
                                    num = 1;
                                    continue;
                                }
                                ConnectionStringSettings connectionStringSettings= new ConnectionStringSettings();
                                configuration.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString = connectionStringSettings.ConnectionString;
                                num = 4;
                                continue;
                            }
                        case 10:
                            configuration = WebConfigurationManager.OpenWebConfiguration("~");
                            num = 12;
                            continue;
                        case 11:
                            goto IL_14F;
                        case 12:
                            goto IL_16F;
                        case 13:
                            {
                                if (providerName != null)
                                {
                                    num = 3;
                                    continue;
                                }
                                ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings(connectionName, connectionString);
                                num = 7;
                                continue;
                            }
                    }
                    break;
                IL_FA:
                    num = 9;
                    continue;
                IL_16F:
                    num = 13;
                }
            }
        IL_77:
        IL_14F:
        IL_18D:
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.ConnectionStrings.SectionInformation.Name);
            ConfigUtil.b = ConfigurationManager.ConnectionStrings;
        }

        public static bool GetConfigValueBoolean(string name, bool defaultValue = false)
        {
            string value = ConfigurationManager.AppSettings.Get(name);
            bool result = false;
            if (bool.TryParse(value, out result))
            {
                if (true)
                {
                }
                return result;
            }
            return defaultValue;
        }

        public static string GetConfigValue(string name, string defaultValue = "")
        {
            string text = ConfigurationManager.AppSettings.Get(name);
            if (string.IsNullOrEmpty(text))
            {
                if (true)
                {
                }
                return defaultValue;
            }
            return text;
        }

        public static int GetConfigValueInt32(string name, int defaultValue = 0)
        {
            string s = ConfigurationManager.AppSettings.Get(name);
            int result = -1;
            if (int.TryParse(s, out result))
            {
                if (true)
                {
                }
                return result;
            }
            return defaultValue;
        }

        public static double GetConfigValueDouble(string name, double defaultValue = 0.0)
        {
            string s = ConfigurationManager.AppSettings.Get(name);
            double result = -1.0;
            if (double.TryParse(s, out result))
            {
                if (true)
                {
                }
                return result;
            }
            return defaultValue;
        }
    }
}
