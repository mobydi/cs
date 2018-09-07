using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CS.Plugin.Interfaces;

namespace CS.Processing
{
    public class DirectoryWatcher
    {
        private const string ProcessingExt = "._processing";
        private readonly string _rootPath;
        private readonly string _processedPath;
        private readonly int _timespan;
        private readonly Dictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>();

        public DirectoryWatcher(string rootFolder, IEnumerable<IHandler> handlers, int timespan)
        {
            _rootPath = rootFolder;
            _timespan = timespan;

            _processedPath = Path.Combine(_rootPath, "Processed"); //TODO: move to config?
            if (!Directory.Exists(_processedPath))
            {
                Directory.CreateDirectory(_processedPath);
            }
            handlers.ForEach(handler => _handlers.Add(handler.Extension.ToUpper(), handler));
        }

        public IObservable<IEnumerable<HandlerData>> Start()
        {
            return Observable.Create<IEnumerable<HandlerData>>(
                observer =>
                {
                    var cts = new CancellationTokenSource();
                    var ctoken = cts.Token;
                    var scheduler = Scheduler.CurrentThread;
                    new Thread(() =>
                    {
                        while (!ctoken.IsCancellationRequested)
                        {
                            //TODO: errors & logging?
                            Directory.EnumerateFiles(_rootPath, "*", SearchOption.TopDirectoryOnly)
                                .Where(fileName => !fileName.EndsWith(ProcessingExt))
                                .ForEach(originalFile =>
                                {
                                    //rename to avoid double processing
                                    //TODO: smart rename
                                    var fileName = Path.GetFileName(originalFile);
                                    var processingFile = originalFile + ProcessingExt;
                                    File.Move(originalFile, processingFile);

                                    ThreadPool.QueueUserWorkItem(_ =>
                                    {
                                        try
                                        {
                                            var handler = GetHandlerFromFileName(fileName);
                                            handler.Parse(processingFile, ctoken)
                                                .Batch(100)
                                                .ForEach(
                                                    filesBatch =>
                                                        scheduler.Schedule(() =>
                                                        {
                                                            observer.OnNext(filesBatch);
                                                        }));

                                            //move out processed file
                                            //TODO: smart rename
                                            File.Move(processingFile, Path.Combine(_processedPath, fileName + Guid.NewGuid() ));
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine(ex);
                                            //TODO:logging?
                                        }
                                    });
                                });


                            Task.Delay(TimeSpan.FromSeconds(_timespan), ctoken);
                        }
                    }).Start();

                    return () => cts.Cancel() ;
                });
        }

        private IHandler GetHandlerFromFileName(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext))
                return new EmptyHandler();

            return _handlers[ext.TrimStart('.').ToUpper()];
        }
    }
}