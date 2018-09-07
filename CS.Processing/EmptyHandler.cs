using System.Collections.Generic;
using System.Threading;
using CS.Plugin.Interfaces;

namespace CS.Processing
{
    class EmptyHandler : IHandler
    {
        public IEnumerable<HandlerData> Parse(string filePath, CancellationToken ct)
        {
            //TODO: logging?
            yield break;
        }

        public string Extension => string.Empty;
    }
}
