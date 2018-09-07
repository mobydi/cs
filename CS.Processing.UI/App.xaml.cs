using System.Configuration;
using System.Windows;

namespace CS.Processing.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Importer _importer;

        public App()
        {
            ServiceConfigurationSection serviceConfigSection = ServiceConfigurationSection.GetSection();
            if (serviceConfigSection == null)
                throw new ConfigurationErrorsException("serviceConfigSection");

            PluginsConfig serviceConfig = serviceConfigSection.Plugins;

            _importer = new Importer(serviceConfig.Folder);
            _importer.DoImport();
        }

        public Importer Importer { get { return _importer; } }
    }
}
