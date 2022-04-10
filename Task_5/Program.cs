using System;
using System.Collections.Generic;
using System.Reflection;
using Task_5.Attributes;
using Task_5.Providers;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration
            {
                Name = "name",
                Type = "type",
                Count = 10,
                Age = 20,
                Cost = 12.94f,
                Price = 45.78f,
                StartDate = new TimeSpan(43297582362),
                EndDate = new TimeSpan(23, 56, 34),
            };

            SaveSettings(configuration);

            Console.WriteLine(configuration);
            Console.WriteLine();

            var conf = LoadSettings();

            Console.WriteLine(conf);
        }

        public static void SaveSettings(Configuration configuration)
        {
            var type = typeof(Configuration);

            var appSettingProvider = new AppSettingConfigurationProvider();
            var xmlProvider = new XmlConfigurationProvider();

            var propertiesForAppSetting = new List<PropertyInfo>();
            var propertiesForXmlFile = new List<PropertyInfo>();

            foreach (var property in type.GetProperties())
            {
                if (Attribute.IsDefined(property, typeof(ConfigurationItemAttribute)))
                {
                    var configurationItemAttribute = (ConfigurationItemAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute));

                    if (configurationItemAttribute.ProviderType.Equals(typeof(AppSettingConfigurationProvider)))
                    {
                        propertiesForAppSetting.Add(property);
                    }

                    if (configurationItemAttribute.ProviderType.Equals(typeof(XmlConfigurationProvider)))
                    {
                        propertiesForXmlFile.Add(property);
                    }
                }
            }

            appSettingProvider.SaveProperties(propertiesForAppSetting, configuration);
            xmlProvider.SaveProperties(propertiesForXmlFile, configuration);
        }

        public static Configuration LoadSettings()
        {
            var conf = new Configuration();

            var appSettingProvider = new AppSettingConfigurationProvider();
            var xmlProvider = new XmlConfigurationProvider();

            appSettingProvider.LoadProperties(conf);
            xmlProvider.LoadProperties(conf);

            return conf;
        }
    }
}
