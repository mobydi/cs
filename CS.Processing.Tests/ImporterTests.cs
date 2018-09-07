using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.Processing.Tests
{
    [TestClass]
    public class ImporterTests
    {
        [TestMethod]
        public void should_connect_basic_plugin()
        {
            Importer importer = new Importer(Path.GetTempPath());
            importer.DoImport();
            var result = importer.GetAllHandlers();

            Assert.AreEqual(3, result.Count());
        }
    }
}
