using NUnit.Framework;
using NUnit.Framework.Legacy;
using OfxSharpLib;
using System.IO;
using System.Linq;

namespace OFXSharp.Tests
{
    [TestFixture]
    public class MemoryStreamParseTest
    {
        [Test]
        public void CanParseMemoryStream()
        {
            var parser = new OfxDocumentParser();
            var bytes = File.ReadAllBytes(@"bb.ofx");
            var stream = new MemoryStream(bytes);

            var ofxDocument = parser.Import(stream);
            ClassicAssert.AreEqual(3, ofxDocument.Transactions.Count());
        }
    }
}
