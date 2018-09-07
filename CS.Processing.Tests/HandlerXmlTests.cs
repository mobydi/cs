using System.Linq;
using System.Threading;
using CS.Plugin.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS.Plugin.XML;

namespace CS.Processing.Tests
{
    [TestClass]
    public class HandlerXmlTests
    {
        [TestMethod]
        public void should_xml_parser_work()
        {
            IHandler handler = new HandlerXml();

            var result = handler.Parse(@"samples\data.xml", CancellationToken.None).ToList();

            Assert.AreEqual(14, result.Count());

            var first = result.First();
            Assert.AreEqual("2013-5-20", first.Date);

            var last = result.Last();
            Assert.AreEqual("1006900", last.Volume);
        }
    }
}
