using System;
using System.Configuration;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace CS.Processing.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainWindow()
        {
            ServiceConfigurationSection serviceConfigSection = ServiceConfigurationSection.GetSection();
            if (serviceConfigSection == null)
                throw new ConfigurationErrorsException("serviceConfigSection");

            ProcessingConfig processingConfig = serviceConfigSection.Processing;

            InitializeComponent();

            _disposables.Add(new DirectoryWatcher(processingConfig.Folder, ((App)Application.Current).Importer.GetAllHandlers(), processingConfig.Seconds)
                .Start()
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(elements =>
                {
                    foreach (var element in elements)
                    {
                        DataView.Items.Add(element);
                    }
                }));

            Closed += (sender, args) => _disposables.Dispose();
        }
    }
}
