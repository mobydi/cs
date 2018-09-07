using System.Linq;
using System.Threading;
using CS.Plugin.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS.Plugin.TXT;

namespace CS.Processing.Tests
{
    [TestClass]
    public class HandlerTxtTests
    {
        [TestMethod]
        public void should_txt_parser_work()
        {
            IHandler handler = new HandlerTxt();

            var result = handler.Parse(@"samples\data.txt", CancellationToken.None).ToList();

            Assert.AreEqual(14, result.Count());

            var first = result.First();
            Assert.AreEqual("2013-5-20", first.Date);

            var last = result.Skip(13).First();
            Assert.AreEqual("1006900", last.Volume);
        }
    }
}
