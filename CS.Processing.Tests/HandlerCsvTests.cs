using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS.Plugin.CSV;
using CS.Plugin.Interfaces;

namespace CS.Processing.Tests
{
    [TestClass]
    public class HandlerCsvTests
    {
        [TestMethod]
        public void should_csv_parser_work()
        {
            IHandler handler = new HandlerCsv();

            var result = handler.Parse(@"samples\data.csv", CancellationToken.None).ToList();

            Assert.AreEqual(14, result.Count());

            var first = result.First();
            Assert.AreEqual("2013-5-20", first.Date);

            var last = result.Last();
            Assert.AreEqual("1006900", last.Volume);
        }
    }
}
