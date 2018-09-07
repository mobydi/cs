using System.Configuration;

namespace CS.Processing
{
    public class ServiceConfigurationSection : ConfigurationSection
    {
        public static ServiceConfigurationSection GetSection()
        {
            return ConfigurationManager.GetSection("service") as ServiceConfigurationSection;
        }

        [ConfigurationProperty("Processing", IsDefaultCollection = false)]
        public ProcessingConfig Processing
        {
            get
            {
                return base["Processing"] as ProcessingConfig;
            }
        }

        [ConfigurationProperty("Plugins", IsDefaultCollection = false)]
        public PluginsConfig Plugins
        {
            get
            {
                return base["Plugins"] as PluginsConfig;
            }
        }
    }

    public class ProcessingConfig : ConfigurationElement
    {
        public ProcessingConfig() { }

        public ProcessingConfig(int seconds)
        {
            Seconds = seconds;
        }

        [ConfigurationProperty("Folder", DefaultValue = "data", IsRequired = true, IsKey = false)]
        public string Folder
        {
            get { return (string)this["Folder"]; }
            set { this["Folder"] = value; }
        }

        [ConfigurationProperty("Seconds", DefaultValue = 5, IsRequired = true, IsKey = true)]
        public int Seconds
        {
            get { return (int)this["Seconds"]; }
            set { this["Seconds"] = value; }
        }
    }

    public class PluginsConfig : ConfigurationElement
    {
        public PluginsConfig() { }

        public PluginsConfig(string folder)
        {
            Folder = folder;
        }

        [ConfigurationProperty("Folder", DefaultValue = "plugins", IsRequired = true, IsKey = false)]
        public string Folder
        {
            get { return (string)this["Folder"]; }
            set { this["Folder"] = value; }
        }
    }
}
