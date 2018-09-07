using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using CS.Plugin.Interfaces;

namespace CS.Plugin.XML
{
    [Export(typeof(IHandler))]
    public class HandlerXml : IHandler
    {
        IEnumerable<HandlerData> IHandler.Parse(string filePath, CancellationToken ct)
        {
            using (var reader = new XmlTextReader(filePath))
            {
                while (reader.Read() && !ct.IsCancellationRequested)
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "value")
                        {
                            yield return new HandlerData
                            {
                                Date = reader.GetAttribute("date"),
                                Open = reader.GetAttribute("open"),
                                High = reader.GetAttribute("high"),
                                Low = reader.GetAttribute("low"),
                                Close = reader.GetAttribute("close"),
                                Volume = reader.GetAttribute("volume")
                            };
                        }
                    }
                    
                }
            }
        }

        public string Extension => "XML";
    }
}
