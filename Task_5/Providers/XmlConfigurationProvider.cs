using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Task_5.Attributes;

namespace Task_5.Providers
{
    public class XmlConfigurationProvider
    {
        private readonly string _path;

        public XmlConfigurationProvider()
        {
            _path = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Resources\file.xml");
        }

        public void SaveProperties(IEnumerable<PropertyInfo> properties, Configuration configuration)
        {
            var xmlWriter = XmlWriter.Create(_path);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(nameof(Configuration));

            foreach (var property in properties)
            {
                xmlWriter.WriteStartElement(nameof(Configuration));
                xmlWriter.WriteAttributeString(property.Name, property.GetValue(configuration).ToString());
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public void LoadProperties(Configuration configuration)
        {
            var properties = GetPropertiesForReadingFromXml();

            var xmlReader = XmlReader.Create(_path);

            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == nameof(Configuration)))
                {
                    if (xmlReader.HasAttributes)
                    {
                        var property = xmlReader.GetAttribute("Name");

                        var propertyFromAttribute = properties.FirstOrDefault(property => xmlReader.GetAttribute(property.Name) != null);

                        if (propertyFromAttribute.PropertyType.Equals(typeof(string)))
                        {
                            propertyFromAttribute?.SetValue(configuration, xmlReader.GetAttribute(propertyFromAttribute.Name));
                        }
                        
                        if (propertyFromAttribute.PropertyType.Equals(typeof(int)))
                        {
                            propertyFromAttribute?.SetValue(configuration, int.Parse(xmlReader.GetAttribute(propertyFromAttribute.Name)));
                        }

                        if (propertyFromAttribute.PropertyType.Equals(typeof(float)))
                        {
                            propertyFromAttribute?.SetValue(configuration, float.Parse(xmlReader.GetAttribute(propertyFromAttribute.Name)));
                        }

                        if (propertyFromAttribute.PropertyType.Equals(typeof(TimeSpan)))
                        {
                            propertyFromAttribute?.SetValue(configuration, TimeSpan.Parse(xmlReader.GetAttribute(propertyFromAttribute.Name)));
                        }
                    }
                }
            }
        }

        private List<PropertyInfo> GetPropertiesForReadingFromXml()
        {
            var type = typeof(Configuration);
            var properties = type.GetProperties().Where(property => Attribute.IsDefined(property, typeof(ConfigurationItemAttribute)));

            var propertiesForXmlFile = new List<PropertyInfo>();

            foreach (var property in properties)
            {
                var configurationItemAttribute = (ConfigurationItemAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute));

                if (configurationItemAttribute.ProviderType.Equals(typeof(XmlConfigurationProvider)))
                {
                    propertiesForXmlFile.Add(property);
                }
            }

            return propertiesForXmlFile;
        }
    }
}
