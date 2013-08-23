using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using org.in2bits.MyOle2;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class FormatTests : MyXlsTestFixture
    {

        [Test]
        public void ApplyCurrencyFormat()
        {
            XlsDocument doc = new XlsDocument();
            Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
            Cell cell = sheet.Cells.Add(1, 1, 1.13);
            Assert.AreEqual(1, doc.Workbook.Formats.Count, "Format count before applying new format");
            cell.Format = StandardFormats.Currency_3;//"\"$\"#,##0.00_);(\"$\"#,##0.00)";
            Assert.AreEqual(1, doc.Workbook.Formats.Count, "Format count after applying new format");
            doc.FileName = "ApplyCurrencyFormat";
            doc.Save(true);
            string file = Environment.CurrentDirectory;
            if (!file.EndsWith("\\"))
                file += "\\";
            file += doc.FileName;
            AssertPropertyViaExcelOle(file, CellProperties.Text, "$1.13 ", "Cell Text");
        }

        [Test]
        public void ApplyCustomFormat()
        {
            XlsDocument doc = new XlsDocument();
            Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
            Cell cell = sheet.Cells.Add(1, 1, 1.13);
            Assert.AreEqual(1, doc.Workbook.Formats.Count, "Format count before applying new format");
            cell.Format = "\"x\"#,##0.00_);(\"x\"#,##0.00)";
            Assert.AreEqual(2, doc.Workbook.Formats.Count, "Format count after applying new format");
            doc.FileName = "ApplyCustomFormat";
            doc.Save(true);
            string file = Environment.CurrentDirectory;
            if (!file.EndsWith("\\"))
                file += "\\";
            file += doc.FileName;
            AssertPropertyViaExcelOle(file, CellProperties.Text, "x1.13 ", "Cell Text");
        }
    }
}
