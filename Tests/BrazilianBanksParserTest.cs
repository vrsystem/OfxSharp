using NUnit.Framework;
using NUnit.Framework.Legacy;
using OfxSharpLib;
using System.IO;
using System.Linq;
using System.Text;

namespace OFXSharp.Tests
{
    public class BrazilianBanksParserTest
    {
        [Test]
        public void CanParseItau()
        {
            var parser = new OfxDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"itau.ofx", FileMode.Open));

            ClassicAssert.AreEqual(ofxDocument.Account.AccountId, "9999 99999-9");
            ClassicAssert.AreEqual(ofxDocument.Account.BankId, "0341");

            ClassicAssert.AreEqual(3, ofxDocument.Transactions.Count());
            CollectionAssert.AreEqual(ofxDocument.Transactions.Select(x => x.Memo.Trim()).ToList(), new[] { "RSHOP", "REND PAGO APLIC AUT MAIS", "SISDEB" });
        }

        [Test]
        public void CanParseSantander()
        {
            var parser = new OfxDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"santander.ofx", FileMode.Open));

            ClassicAssert.IsNotNull(ofxDocument);
        }

        [Test]
        public void CanParseBancoDoBrasil()
        {
            var parser = new OfxDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"bb.ofx", FileMode.Open), Encoding.GetEncoding("ISO-8859-1"));

            ClassicAssert.AreEqual(ofxDocument.Account.AccountId, "99999-9");
            ClassicAssert.AreEqual(ofxDocument.Account.BranchId, "9999-9");
            ClassicAssert.AreEqual(ofxDocument.Account.BankId, "1");

            ClassicAssert.AreEqual(3, ofxDocument.Transactions.Count());
            CollectionAssert.AreEqual(ofxDocument.Transactions.Select(x => x.Memo.Trim()).ToList(), new[] { "Transferência Agendada", "Compra com Cartão", "Saque" });

            ClassicAssert.IsNotNull(ofxDocument);
        }

        [Test]
        public void CanParseNuBank()
        {
            var parser = new OfxDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"nu.ofx", FileMode.Open), Encoding.GetEncoding("UTF-8"));

            ClassicAssert.AreEqual(ofxDocument.Account.AccountId, "61726153-2");
            ClassicAssert.AreEqual(ofxDocument.Account.BranchId, "1");
            ClassicAssert.AreEqual(ofxDocument.Account.BankId, "0260");

            ClassicAssert.AreEqual(12, ofxDocument.Transactions.Count());
            CollectionAssert.AreEqual(ofxDocument.Transactions.Select(x => x.Memo.Trim()).FirstOrDefault(), "Depósito Recebido por Boleto");

            ClassicAssert.IsNotNull(ofxDocument);
        }
    }
}
