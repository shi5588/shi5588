using NUnit.Framework;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class ColumnInfoTests : MyXlsTestFixture
    {
        [Test]
        public void ColumnWidth5120()
        {
            ushort colWidth = 5120;
            MyXlsTestFixture.XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                  ColumnInfo columnInfo = new ColumnInfo(doc, sheet);
                  sheet.AddColumnInfo(columnInfo);
                  columnInfo.Width = colWidth;
                  Assert.AreEqual(colWidth, columnInfo.Width, "Column Width setting");
              };
            string fileName = WriteDocument(docDelegate); //48.762
            string actualString = GetCellPropertyViaExcelOle(fileName, CellProperties.Width);
            double actual = double.NaN;
            Assert.IsTrue(double.TryParse(actualString, out actual), "Column width didn't parse");
            Assert.AreEqual(colWidth / 48.762, actual, 0.01, "Column width"); //NOTE: This factor (48.762) depends on the default (first) font in the file
        }
    }
}
