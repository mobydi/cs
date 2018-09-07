using System.Collections.Generic;
using System.Threading;

namespace CS.Plugin.Interfaces
{
    public interface IHandler 
    {
        IEnumerable<HandlerData> Parse(string filePath, CancellationToken ct);
        string Extension { get; }
    }

    public class HandlerData
    {
        public string Date { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }
    }
}
