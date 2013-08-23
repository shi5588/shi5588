using System;
using System.IO;
using NUnit.Framework;
using org.in2bits.MyXls.ByteUtil;
using org.in2bits.MyXls.Tests;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class CellTests : MyXlsTestFixture
    {
		private static readonly string _numbersFilename = TestsConfig.ReferenceFileFolder + "ReadNumbers.xls";
		private delegate T Parse<T>(string val);
		private Parse<Double> _parseDouble = delegate(string val) { return Double.Parse(val); };
		private Parse<Int32> _parseInt = delegate(string val) { return Int32.Parse(val); };

		[Test]
        public void WriteDateTime()
        {
            DateTime dateTime = DateTime.Now;
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");

                  Cell cell = sheet.Cells.Add(1, 1, dateTime);
                  Assert.AreEqual(dateTime.ToOADate(), cell.Value, "Cell value");
              };
            string fileName = WriteDocument(docDelegate);
            double dateTimeDouble = Math.Round(dateTime.ToOADate(), 2);
            string dateTimeString = dateTimeDouble.ToString();
            AssertPropertyViaExcelOle(fileName, CellProperties.Text, dateTimeString, "Date value (text)");
        }

        [Test]
        public void WriteTextWithoutSharedStrings()
        {
            string text = "abc";
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    Assert.IsFalse(doc.Workbook.ShareStrings, "ShareSettings value");
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1).Value = text;
                    Assert.AreEqual(text, sheet.Rows[1].CellAtCol(1).Value, "Cell value from XlsDocument");
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(fileName, CellProperties.Text, text, "Cell value from Excel via OLE");
        }

        [Test]
        public void WriteTextWithSharedStrings()
        {
            string text = "abc";
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    doc.Workbook.ShareStrings = true;
                    Assert.IsTrue(doc.Workbook.ShareStrings, "ShareSettings value");
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1).Value = text;
                    Assert.AreEqual(text, sheet.Rows[1].CellAtCol(1).Value, "Cell value from XlsDocument");
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(fileName, CellProperties.Text, text, "Cell value from Excel via OLE");
        }

        [Test]
        public void Write1()
        {
            string fileName = Write1Cell((double)1);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, 1.ToString(), "Cell Value");
        }

        [Test]
        public void Write1_1()
        {
            string fileName = Write1Cell(1.1);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, (1.1).ToString(), "Cell Value");
        }

        [Test]
        public void Write1_01()
        {
            string fileName = Write1Cell(1.01);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, (1.01).ToString(), "Cell Value");
        }

        [Test]
        public void Write1_001()
        {
            string fileName = Write1Cell(1.001);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, (1.001).ToString(), "Cell Value");
        }

        [Test]
        public void WriteN2825_48()
        {
            string fileName = Write1Cell(-2825.48);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, (-2825.48).ToString(), "Cell Value");
        }

        [Test]
        public void DoubleIsLittleEndian()
        {
            double dbl = 123.4567;
            Bytes bytes = new Bytes(BitConverter.GetBytes(dbl));
            Bytes.Bits bits = bytes.GetBits();
            Bytes.Bits mostSigBits = bits.Get(2, 62);
            double newDbl = mostSigBits.ToDouble();
            Assert.AreEqual(dbl, newDbl, 0.000001, "Values are different");
        }

        [Test]
        public void ChoppedDoubleIsValidDecimalRK()
        {
            double dbl = 123.4567;
            Bytes bytes = new Bytes(BitConverter.GetBytes(dbl));
            Bytes.Bits bits = bytes.GetBits();
            Bytes.Bits mostSigBits = bits.Get(34, 30);
            double newDbl = mostSigBits.ToDouble();
            Assert.AreEqual(dbl, newDbl, 0.001, "Values are too different");
        }

        private static Worksheet GetBaseMergeAreaOverlapTestSheet()
        {
            XlsDocument doc = new XlsDocument();
            Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
            sheet.Cells.Merge(2, 4, 10, 15);
            return sheet;
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingColumnsThrowsA()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 4, 8, 11);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingColumnsThrowsB()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 4, 10, 12);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingColumnsThrowsC()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 4, 12, 15);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingColumnsThrowsD()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 4, 12, 18);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingRowsThrowsA()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(1, 2, 11, 12);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingRowsThrowsB()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(2, 3, 11, 12);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingRowsThrowsC()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 4, 11, 12);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void MergingOverlappingRowsThrowsD()
        {
            GetBaseMergeAreaOverlapTestSheet().Cells.Merge(3, 8, 11, 12);
        }

        [Test]
        public void MergeA1toC1()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1, "ValA1");
                    sheet.Cells.Add(1, 2, "ValB1");
                    sheet.Cells.Add(1, 3, "ValC1");
                    sheet.Cells.Add(2, 1, "ValA2");
                    sheet.Cells.Add(2, 2, "ValB2");
                    sheet.Cells.Add(2, 3, "ValC2");
                    sheet.Cells.Merge(1, 1, 1, 3);
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "A1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "B1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 1, 3, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "C1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 2, 1, fileName, CellProperties.MergeAreaCount, (1).ToString(),
                                      "A2 should have MergeArea with 1 cells");
            AssertPropertyViaExcelOle(1, 2, 2, fileName, CellProperties.MergeAreaCount, (1).ToString(),
                                      "B2 should have MergeArea with 1 cells");
            AssertPropertyViaExcelOle(1, 2, 3, fileName, CellProperties.MergeAreaCount, (1).ToString(),
                                      "C2 should have MergeArea with 1 cells");
        }

        [Test]
        public void MergeA1toC1andB2toC2()
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1, "ValA1");
                    sheet.Cells.Add(1, 2, "ValB1");
                    sheet.Cells.Add(1, 3, "ValC1");
                    sheet.Cells.Add(2, 1, "ValA2");
                    sheet.Cells.Add(2, 2, "ValB2");
                    sheet.Cells.Add(2, 3, "ValC2");
                    sheet.Cells.Merge(1, 1, 1, 3);
                    sheet.Cells.Merge(2, 2, 2, 3);
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(1, 1, 1, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "A1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "B1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 1, 3, fileName, CellProperties.MergeAreaCount, (3).ToString(),
                                      "C1 should have MergeArea with 3 cells");
            AssertPropertyViaExcelOle(1, 2, 1, fileName, CellProperties.MergeAreaCount, (1).ToString(),
                                      "A2 should have MergeArea with 1 cells");
            AssertPropertyViaExcelOle(1, 2, 2, fileName, CellProperties.MergeAreaCount, (2).ToString(),
                                      "B2 should have MergeArea with 2 cells");
            AssertPropertyViaExcelOle(1, 2, 3, fileName, CellProperties.MergeAreaCount, (2).ToString(),
                                      "C2 should have MergeArea with 2 cells");
        }

		private static Cell AssertReadNumberCell<T>(Parse<T> parse, ushort rowIndex, ushort columnIndex)
		{
			Assert.IsTrue(File.Exists(_numbersFilename), String.Format("{0} not found", _numbersFilename));
			XlsDocument xls = new XlsDocument(_numbersFilename);
			Worksheet ws = xls.Workbook.Worksheets[0];

			Cell cell = ws.Rows[rowIndex].CellAtCol(columnIndex);
			string value = GetCellPropertyViaExcelOle(1, rowIndex, columnIndex, _numbersFilename, CellProperties.Value);
			Assert.AreEqual(value, cell.Value.ToString());
			Assert.AreEqual(parse(value), cell.Value);
			return cell;
		}

		[Test]
		public void ReadN76543_21()
		{
			// Floating point stored as NUMBER
			Cell cell = AssertReadNumberCell(_parseDouble, 1, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(76543.21d, cell.Value);
		}

		[Test]
		public void ReadN76543_22()
		{
			// Floating point stored as RKInt: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 2, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(76543.22d, cell.Value);
		}

		[Test]
		public void ReadN76543_23()
		{
			// Floating point stored as RKInt: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 3, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(76543.23d, cell.Value);
		}

		[Test]
		public void ReadN123_1111()
		{
			// Floating point stored as NUMBER
			Cell cell = AssertReadNumberCell(_parseDouble, 4, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(123.1111d, cell.Value);
		}

		[Test]
		public void ReadN123_1()
		{
			// Floating point stored as RKFloat: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 5, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(123.1d, cell.Value);
		}

		[Test]
		public void ReadN123_01()
		{
			// Floating point stored as RKFloat: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 6, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(123.01d, cell.Value);
		}

		[Test]
		public void ReadN123_02()
		{
			// Floating point stored as RKFloat: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 7, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(123.02d, cell.Value);
		}

		[Test]
		public void ReadN123_03()
		{
			// Floating point stored as RKFloat: div100 = 1
			Cell cell = AssertReadNumberCell(_parseDouble, 8, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(123.03d, cell.Value);
		}

		[Test]
		public void ReadN123454321()
		{
			// Integer stored as RKInt: div100 = 0
			Cell cell = AssertReadNumberCell(_parseInt, 9, 1);
			Assert.AreEqual(CellTypes.Integer, cell.Type);
			Assert.AreEqual(123454321, cell.Value);
		}

		[Test]
		public void ReadN2001()
		{
			// Floating point stored as RKFloat: div100 = 0
			Cell cell = AssertReadNumberCell(_parseDouble, 10, 1);
			Assert.AreEqual(CellTypes.Float, cell.Type);
			Assert.AreEqual(2001d, cell.Value);
		}

		[Test]
		public void Write_Int32TooBig()
		{
			// Integer larger than +/- 536870911 will be written out correctly instead of throwing an exception
			int i = 536870912;
			string fileName = Write1Cell(i);
			AssertPropertyViaExcelOle(fileName, CellProperties.Value, (i).ToString(), "Cell Value differs");
		}

		[Test]
		public void Write_Int64TooBig()
		{
			// Integer larger than +/- 536870911 will be written out correctly instead of throwing an exception
			long i = 536870912;
			string fileName = Write1Cell(i);
			AssertPropertyViaExcelOle(fileName, CellProperties.Value, (i).ToString(), "Cell Value differs");
		}
    }
}
