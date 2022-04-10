using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Task_5.Providers
{
    public class AppSettingConfigurationProvider
    {
        private readonly string _path;

        public AppSettingConfigurationProvider()
        {
            _path = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Resources\appsetting.json");
        }

        public void SaveProperties(IEnumerable<PropertyInfo> properties, Configuration configuration)
        {
            string text = "{\n";
            foreach (var property in properties)
            {
                text += $"\t\"{property.Name}\": \"{property.GetValue(configuration)}\",\n";
            }
            text += "}";
            File.WriteAllText(_path, text);
        }

        public void LoadProperties(Configuration configuration)
        {
            var textFromAppSettingFile = File.ReadAllText(_path);

            var readedConfiguration = JsonConvert.DeserializeObject<Configuration>(textFromAppSettingFile);

            configuration.Name = readedConfiguration.Name;
            configuration.Type = readedConfiguration.Type;
            configuration.Count = readedConfiguration.Count;
            configuration.Age = readedConfiguration.Age;
            configuration.Cost = readedConfiguration.Cost;
            configuration.Price = readedConfiguration.Price;
            configuration.StartDate = readedConfiguration.StartDate;
            configuration.EndDate = readedConfiguration.EndDate;
        }
    }
}
