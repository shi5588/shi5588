using NUnit.Framework;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class XfTests : MyXlsTestFixture
    {
        [Test]
        public void FontBold()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    Assert.AreEqual(17, doc.Workbook.XFs.Count, "XF Count before setting Bold");
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1, "test").Font.Bold = true;
                    Assert.AreEqual(18, doc.Workbook.XFs.Count, "XF Count after setting Bold");
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(fileName, CellProperties.Font_Bold, true.ToString(), "Font is bold");
        }

        [Test]
        public void FontItalic()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Assert.AreEqual(17, doc.Workbook.XFs.Count, "XF count before setting italic");
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                  sheet.Cells.Add(1, 1, "test").Font.Italic = true;
                  Assert.AreEqual(18, doc.Workbook.XFs.Count, "XF count after setting italic");
              };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(fileName, CellProperties.Font_Italic, true.ToString(), "Font is italic");
        }

        [Test]
        public void FontUnderlineStyles()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");

                  Assert.AreEqual(17, doc.Workbook.XFs.Count, "XF count before setting UnderlineSingle");
                  sheet.Cells.Add(1, 1, "test").Font.Underline = UnderlineTypes.Single;
                  Assert.AreEqual(18, doc.Workbook.XFs.Count, "XF count after setting UnderlineSingle");

                  sheet.Cells.Add(1, 2, "test").Font.Underline = UnderlineTypes.SingleAccounting;
                  Assert.AreEqual(19, doc.Workbook.XFs.Count, "XF count after setting UnderlineSingle");

                  sheet.Cells.Add(1, 3, "test").Font.Underline = UnderlineTypes.Double;
                  Assert.AreEqual(20, doc.Workbook.XFs.Count, "XF count after setting UnderlineDouble");

                  sheet.Cells.Add(1, 4, "test").Font.Underline = UnderlineTypes.DoubleAccounting;
                  Assert.AreEqual(21, doc.Workbook.XFs.Count, "XF count after setting UnderlineDoubleAccounting");

                  sheet.Cells.Add(1, 5, "test").Font.Underline = UnderlineTypes.None;
                  Assert.AreEqual(21, doc.Workbook.XFs.Count, "XF count after setting UnderlineNone");

                  sheet.Cells.Add(1, 6, "test");
                  Assert.AreEqual(21, doc.Workbook.XFs.Count, "XF count after setting no formatting");
              };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.Single.ToString(), "Font is UnderlineSingle");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.SingleAccounting.ToString(), "Font is UnderlineSingleAccounting");
            AssertPropertyViaExcelOle(1, 1, 3, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.Double.ToString(), "Font is UnderlineDouble");
            AssertPropertyViaExcelOle(1, 1, 4, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.DoubleAccounting.ToString(), "Font is UnderlineDoubleAccounting");
            AssertPropertyViaExcelOle(1, 1, 5, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.None.ToString(), "Font is UnderlineNone");
            AssertPropertyViaExcelOle(1, 1, 6, fileName, CellProperties.Font_Underline, ExcelUnderlineStyles.None.ToString(), "Font is UnderlineNone");
        }

        [Test]
        public void FontName()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");

                  sheet.Cells.Add(1, 1, "test");
                  Assert.AreEqual(17, doc.Workbook.XFs.Count, "XF count before setting FontName");

                  Cell cell = sheet.Cells.Add(1, 2, "test");
                  string newFont = "Times New Roman";
                  cell.Font.FontName = newFont;
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
                  Assert.AreEqual(18, doc.Workbook.XFs.Count, "XF count after setting FontName");
              };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Name, "Arial", "Font Name");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.Font_Name, "Times New Roman", "Font Name");
        }

        [Test]
        public void FontUnderlineNameRotation()
        {
            string newFont = "Times New Roman";
            short newRotation = 45;
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");

                  Cell cell = sheet.Cells.Add(1, 1, "test");
                  Assert.AreEqual(UnderlineTypes.Default, cell.Font.Underline,
                                  "Old underline type");
                  Assert.AreEqual("Arial", cell.Font.FontName, "Old font name");
                  Assert.AreEqual(0, cell.Rotation, "Old cell rotation");
                  cell.Font.FontName = newFont;
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
                  cell.Font.Underline = UnderlineTypes.Double;
                  Assert.AreEqual(UnderlineTypes.Double, cell.Font.Underline,
                                  "New underline type");
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
                  cell.Rotation = newRotation;
                  Assert.AreEqual(45, cell.Rotation, "New cell rotation");
                  Assert.AreEqual(UnderlineTypes.Double, cell.Font.Underline,
                                  "New underline type");
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
              };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Name, newFont, "Font name");
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Underline, ((int)ExcelUnderlineStyles.Double).ToString(), "Font underline type");
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Rotation, newRotation.ToString(),
                                      "Cell Rotation");
        }

        [Test]
        public void FontUnderlineNameOnTwoConsecutiveCells()
        {
            string newFont = "Times New Roman";
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");

                  Cell cell = sheet.Cells.Add(1, 1, "test");
                  Assert.AreEqual(UnderlineTypes.Default, cell.Font.Underline,
                                  "Old underline type");
                  Assert.AreEqual("Arial", cell.Font.FontName, "Old font name");

                  cell.Font.FontName = newFont;
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
                  Assert.AreEqual(UnderlineTypes.Default, cell.Font.Underline,
                                  "Old underline type");

                  cell.Font.Underline = UnderlineTypes.Double;
                  Assert.AreEqual(UnderlineTypes.Double, cell.Font.Underline,
                                  "New underline type");
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");

                  cell = sheet.Cells.Add(1, 2, "test");
                  Assert.AreEqual(UnderlineTypes.Default, cell.Font.Underline,
                                  "Old underline type");
                  Assert.AreEqual("Arial", cell.Font.FontName, "Old font name");

                  cell.Font.FontName = newFont;
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
                  Assert.AreEqual(UnderlineTypes.Default, cell.Font.Underline,
                                  "Old underline type");

                  cell.Font.Underline = UnderlineTypes.Double;
                  Assert.AreEqual(UnderlineTypes.Double, cell.Font.Underline,
                                  "New underline type");
                  Assert.AreEqual(newFont, cell.Font.FontName, "New font name");
              };
            string fileName = WriteDocument(docDelegate);

            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Name, newFont, "Font name");
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.Font_Underline, ((int)ExcelUnderlineStyles.Double).ToString(), "Font underline type");

            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.Font_Name, newFont, "Font name");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.Font_Underline, ((int)ExcelUnderlineStyles.Double).ToString(), "Font underline type");
        }

            [Test]
        public void SetPatternBackgroundColor()
        {
            //http://www.mvps.org/dmcritchie/excel/colors.htm
            XlsDocument theDoc = null;
            Color colorA1 = Colors.Red;
            Color colorA2 = Colors.Green;
            Color colorA3 = Colors.Blue;
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    theDoc = doc;
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    Cell cell;
                    cell = sheet.Cells.Add(1, 1, "red");
                	cell.Pattern = FillPattern.Percent50;
                    cell.PatternBackgroundColor = colorA1;
                    cell = sheet.Cells.Add(1, 2, "green");
					cell.Pattern = FillPattern.Percent50;
                    cell.PatternBackgroundColor = colorA2;
                    cell = sheet.Cells.Add(1, 3, "blue");
					cell.Pattern = FillPattern.Percent50;
                    cell.PatternBackgroundColor = colorA3;
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.PatternBackgroundColor, GetExcelVbaColorIndex(theDoc, colorA1).ToString(),
                                      "Cell A1 should be red");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.PatternBackgroundColor, GetExcelVbaColorIndex(theDoc, colorA2).ToString(),
                                      "Cell A2 should be green");
            AssertPropertyViaExcelOle(1, 1, 3, fileName, CellProperties.PatternBackgroundColor, GetExcelVbaColorIndex(theDoc, colorA3).ToString(),
                                      "Cell A3 should be Blue");
        }

        private int GetExcelVbaColorIndex(XlsDocument doc, Color color)
        {
            return doc.Workbook.Palette.GetIndex(color) - 7;
        }

        [Test]
        public void DefaultPatternColors()
        {
            XlsDocument theDoc = null;
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    theDoc = doc;
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    Cell cell = sheet.Cells.Add(1, 1, "black background");
                	cell.Pattern = FillPattern.Percent75;
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.PatternBackgroundColor, "-4105", "Default Pattern Background Color Index");
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.PatternColor, "-4105", "Default Pattern Color Index");
        }
    }
}
