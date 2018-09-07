using System.ComponentModel.Composition;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using CS.Plugin.Interfaces;

namespace CS.Plugin.CSV
{
    [Export(typeof(IHandler))]
    public class HandlerCsv : IHandler
    {
        IEnumerable<HandlerData> IHandler.Parse(string filePath, CancellationToken ct)
        {
            using (var source = new StreamReader(filePath))
            {
                while (!source.EndOfStream && !ct.IsCancellationRequested)
                {
                    var line = source.ReadLine();
                    var data = Parse(line);
                    yield return (data);
                }
            }
        }

        public string Extension => "CSV";

        HandlerData Parse(string line)
        {
            var data = line.Split(',');
            if (data.Length != 6)
                throw new Exception(string.Format("Columns parce error with line [{0}]", line));

            return new HandlerData
            {
                Date = data[0],
                Open = data[1],
                High = data[2],
                Low = data[3],
                Close = data[4],
                Volume = data[5]
            };
        }
    }
}
