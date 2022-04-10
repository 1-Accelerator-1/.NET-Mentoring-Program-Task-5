using System;
using Task_5.Attributes;
using Task_5.Providers;

namespace Task_5
{
    public class Configuration
    {
        [ConfigurationItem(typeof(AppSettingConfigurationProvider))]
        public string Name { get; set; }

        [ConfigurationItem(typeof(XmlConfigurationProvider))]
        public string Type { get; set; }

        [ConfigurationItem(typeof(AppSettingConfigurationProvider))]
        public int Count { get; set; }

        [ConfigurationItem(typeof(XmlConfigurationProvider))]
        public int Age { get; set; }

        [ConfigurationItem(typeof(AppSettingConfigurationProvider))]
        public float Price { get; set; }

        [ConfigurationItem(typeof(XmlConfigurationProvider))]
        public float Cost { get; set; }

        [ConfigurationItem(typeof(AppSettingConfigurationProvider))]
        public TimeSpan StartDate { get; set; }

        [ConfigurationItem(typeof(XmlConfigurationProvider))]
        public TimeSpan EndDate { get; set; }

        public override string ToString()
        {
            return $"Name: {Name},\n" +
                $"Type: {Type},\n" +
                $"Count: {Count},\n" +
                $"Age: {Age},\n" +
                $"Price: {Price},\n" +
                $"Cost: {Cost},\n" +
                $"StartDate: {StartDate},\n" +
                $"EndDate: {EndDate}";
        }
    }
}
