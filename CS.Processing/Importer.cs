using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using CS.Plugin.Interfaces;

namespace CS.Processing
{
    public class Importer
    {
        private readonly string _pluginFolder;

        [ImportMany(typeof (IHandler))]
        private IEnumerable<IHandler> _handlers;

        public Importer(string pluginFolder)
        {
            this._pluginFolder = pluginFolder;
        }

        public void DoImport()
        {
            var local = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var catalog = new AggregateCatalog();
            if (Directory.Exists(local))
                catalog.Catalogs.Add(new DirectoryCatalog(local));
            if (Directory.Exists(_pluginFolder))
                catalog.Catalogs.Add(new DirectoryCatalog(_pluginFolder));

            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public IEnumerable<IHandler> GetAllHandlers()
        {
            return _handlers;
        }
    }
}
