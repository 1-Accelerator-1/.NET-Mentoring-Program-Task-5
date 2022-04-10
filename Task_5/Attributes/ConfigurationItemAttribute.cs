using System;

namespace Task_5.Attributes
{
    public class ConfigurationItemAttribute : Attribute
    {
        private Type _providerType;

        public ConfigurationItemAttribute(Type providerType)
        {
            _providerType = providerType;
        }

        public Type ProviderType => _providerType;
    }
}
