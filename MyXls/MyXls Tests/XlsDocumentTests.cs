using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using org.in2bits.MyOle2;
using org.in2bits.MyXls.ByteUtil;
using org.in2bits.MyXls.Tests;
using Directory=org.in2bits.MyOle2.Directory;
using System.Data.OleDb;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class XlsDocumentTests : MyXlsTestFixture
    {
        [Test]
        public void DotNetUnicodeString()
        {
            string myString = "hello";
            byte[] myBytes = Encoding.Unicode.GetBytes(myString); //BitConverter.GetBytes(myString);
            Debug.WriteLine(myString.ToCharArray().Length);
            Assert.AreEqual(10, myBytes.Length);
            Assert.AreEqual(104, (byte)('h'));
            Assert.AreEqual(104, myBytes[0]);
            Assert.AreEqual(0, myBytes[1]);
            Assert.AreEqual(101, myBytes[2]);
            Assert.AreEqual(0, myBytes[3]);
            Assert.AreEqual(108, myBytes[4]);
            Assert.AreEqual(0, myBytes[5]);
            Assert.AreEqual(108, myBytes[6]);
            Assert.AreEqual(0, myBytes[7]);
            Assert.AreEqual(111, myBytes[8]);
            Assert.AreEqual(0, myBytes[9]);
        }

        [Test]
        public void EmptyWorkbook()
        {
            XlsDocument doc = new XlsDocument();
            SetTestMetadata(doc);
            string expected = TestsConfig.ReferenceFileFolder + "empty.xls";
            string actual = "empty.xls";
            WriteBytesToFile(doc.Bytes, actual);
            AssertFilesBinaryEqual(expected, actual);
        }

        [Test]
        public void GetBytesTwice()
        {
            XlsDocument doc = new XlsDocument();
            int expectedLength = doc.Bytes.ByteArray.Length;
            Assert.AreEqual(expectedLength, doc.Bytes.ByteArray.Length);
        }

        [Test]
        public void Rows1Cols1Row1Col1()
        {
            string expected = TestsConfig.ReferenceFileFolder + "Rows1Cols1Row1Col1.xls";

            string actual = "Rows1Cols1Row1Col1.xls";

            XlsDocument doc = GetSimple("BIFF8Sheet_", 1, 1, 1, 1, 1);
            doc.FileName = actual;

            WriteBytesToFile(doc.Bytes, actual);
            AssertFilesBinaryEqual(expected, actual);
        }

        [Test]
        public void Rows2Cols1Row1Col1()
        {
            string expected = TestsConfig.ReferenceFileFolder + "Rows2Cols1Row1Col1.xls";

            string actual = "Rows2Cols1Row1Col1.xls";

            XlsDocument doc = GetSimple("BIFF8Sheet_", 1, 2, 1, 1, 1);
            doc.FileName = actual;

            WriteBytesToFile(doc.Bytes, actual);
            AssertFilesBinaryEqual(expected, actual);
        }

        [Test]
        public void Save()
        {
            string fileName = "TestSave.xls";
            XlsDocument doc = new XlsDocument();
            doc.FileName = fileName;
        	string path = Path.Combine(Environment.CurrentDirectory, fileName);
            if (File.Exists(path))
        	    File.Delete(path);
            Assert.IsFalse(File.Exists(path));
            doc.Save();
            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }


		[Test]
		[ExpectedException(typeof(IOException))]
		public void SaveNoOverwrite()
		{
			string fileName = "TestSave.xls";
			XlsDocument doc = new XlsDocument();
			doc.FileName = fileName;
			string path = Path.Combine(Environment.CurrentDirectory, fileName);
			File.WriteAllText(path, "test");
			Assert.IsTrue(File.Exists(path));
			try
			{
				doc.Save();
			}
			catch(IOException ex)
			{
				StringAssert.Contains("already exists", ex.Message);
				throw;
			}
		}

        [Test]
        public void GetStreamsFromBaseline2003Xls()
        {
            string fileName = TestsConfig.ReferenceFileFolder + "Baseline2003.xls";
            Assert.IsTrue(File.Exists(fileName), "Baseline2003.xls not found");
            XlsDocument xls = new XlsDocument(fileName);
            Assert.AreEqual(3, xls.OLEDoc.Streams.Count, "# streams found in Ole Document");
            Bytes workbookBytes = xls.OLEDoc.Streams[xls.OLEDoc.Streams.GetIndex(Directory.Biff8Workbook)].Bytes;
            Assert.IsNotNull(workbookBytes);
            Assert.IsFalse(0 == workbookBytes.Length);
            Assert.IsNotNull(xls.Workbook, "Workbook object");
            Assert.IsNotNull(xls.Workbook.Worksheets, "Worksheets collection");
            Assert.AreEqual(3, xls.Workbook.Worksheets.Count);
            Assert.AreEqual("Sheet1", xls.Workbook.Worksheets[0].Name, "Sheet 1 Name");
            Assert.AreEqual(WorksheetTypes.Worksheet, xls.Workbook.Worksheets[0].SheetType, "Sheet1 Worksheet Type");
            Assert.AreEqual(WorksheetVisibilities.Visible, xls.Workbook.Worksheets[0].Visibility,
                            "Sheet1 Worksheet Visibility");
            Assert.AreEqual("Sheet2", xls.Workbook.Worksheets[1].Name, "Sheet 2 Name");
            Assert.AreEqual("Sheet3", xls.Workbook.Worksheets[2].Name, "Sheet 3 Name");
        }

        [Test]
        public void ReadBaseline2003_A1_1()
        {
            string fileName = TestsConfig.ReferenceFileFolder + "Baseline2003.A1.1.xls";
            Assert.IsTrue(File.Exists(fileName), "Baseline2003.A1.1.xls not found");
            XlsDocument xls = new XlsDocument(fileName);
            Assert.AreEqual(3, xls.Workbook.Worksheets.Count, "Worksheet count");
            Worksheet sheet1 = xls.Workbook.Worksheets[1];
            Assert.IsNotNull(sheet1, "Sheet1 reference by index");
            sheet1 = xls.Workbook.Worksheets["Sheet1"];
            Assert.IsNotNull(sheet1, "Sheet1 reference by name");
            Assert.AreEqual(1, sheet1.Cells.Count, "Sheet1 Cells Count");
            Cell cellA1 = sheet1.Rows[1].CellAtCol(1);
            Assert.IsNotNull(cellA1, "CellA1 reference");
            Assert.AreEqual(1, cellA1.Value, "Cell A1 value");
        }

        private static void SetTestMetadata(XlsDocument doc)
        {
        	doc.SummaryInformation.CreateTimeDate = DateTime.Parse("1/1/2007 00:00:00 -8");
            doc.SummaryInformation.LastSavedBy = "Tester";
        }

        private static XlsDocument GetSimple(string sbase, int sct, int rct, int cct, int rmin, int cmin)
        {
            XlsDocument doc = new XlsDocument();
            SetTestMetadata(doc);

            for (int s = 1; s <= sct; s++)
            {
                string sName = string.Format("{0}{1}", sbase, s);
                if (sName.Length > 35)
                    sName = sName.Substring(0, 35);
                Worksheet sht = doc.Workbook.Worksheets.Add(sName); //Add sheet (and name it)
                Cells cells = sht.Cells; //Grab Sheet.Cells Collection object

                for (int r = 0; r <= rct; r++)
                {
                    if (r == 0) //Add Header row
                    {
                        for (int c = 1; c <= cct; c++) //Iterate columns
                        {
                            cells.Add((ushort)(rmin + r), (ushort)(cmin + c - 1), "Fld" + c); //Add Header cells
                        }
                    }
                    else //Write data row
                    {
                        for (int c = 1; c <= cct; c++) //Iterate columns
                        {
                            cells.Add((ushort)(rmin + r), (ushort)(cmin + c - 1), -1 * (r + c)); //Add Value cells
                        }
                    }
                }
            }

            return doc;
        }

        private void AssertFilesBinaryEqual(string expectedFileName, string actualFileName)
        {
            int row = 0;
            FileStream expectedStream = new FileInfo(expectedFileName).Open(FileMode.Open, FileAccess.Read);
            FileStream actualStream = new FileInfo(actualFileName).Open(FileMode.Open, FileAccess.Read);

            string failMessage = string.Empty;

            try
            {
                while (!IsEOF(expectedStream) && !IsEOF(actualStream))
                {
                    byte[] bufferExpected = new byte[16];
                    byte[] bufferActual = new byte[16];

                    expectedStream.Read(bufferExpected, 0, 16);
                    actualStream.Read(bufferActual, 0, 16);

                    int i = 0;
                    for (; i < 16; i++)
                        if (bufferExpected[i] != bufferActual[i]) break;

                    if (i < 16)
                    {
                        int diffPosition = row * 16 + i;
                        failMessage = string.Format("files differ at byte {0}", diffPosition);
                        break;
                    }

                    row++;
                }

                if (failMessage == string.Empty)
                {
                    if (!IsEOF(expectedStream))
                        Assert.Fail("expected is longer");
                    else if (!IsEOF(actualStream))
                        Assert.Fail("actual is longer");
                }
            }
            finally
            {
                expectedStream.Close();
                expectedStream.Dispose();
                actualStream.Close();
                actualStream.Dispose();
            }

			if (failMessage != string.Empty)
			{
				Console.WriteLine("AssertBinaryFilesEqual failed expected file {0} actual file {1}: {2}", 
					expectedFileName, actualFileName, failMessage);
				AssertFailMyXls(failMessage, expectedFileName, actualFileName);
			}
        }

        private void AssertFailMyXls(string message, string expectedFile, string actualFile)
        {
            Ole2Document n2doc = new Ole2Document();
            FileStream fs = new FileInfo(actualFile).Open(FileMode.Open, FileAccess.Read);
            n2doc.Load(fs);
            fs.Close();
            Bytes actualWorksheet = n2doc.Streams[n2doc.Streams.GetIndex(Directory.Biff8Workbook)].Bytes;
            n2doc = new Ole2Document();
            fs = new FileInfo(expectedFile).Open(FileMode.Open, FileAccess.Read);
            n2doc.Load(fs);
            fs.Close();
            Bytes expectedWorksheet = n2doc.Streams[n2doc.Streams.GetIndex(Directory.Biff8Workbook)].Bytes;
            string expectedWorksheetFile = "ExpectedWorksheet.records";
            string actualWorksheetFile = "ActualWorksheet.records";
            WriteRecordsToFile(actualWorksheet, actualWorksheetFile);
            WriteRecordsToFile(expectedWorksheet, expectedWorksheetFile);

            Process ueProc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "uedit32";
            startInfo.Arguments =
                string.Format("/a \"{0}\" \"{1}\" \"{2}\" \"{3}\"", expectedFile, actualFile, expectedWorksheetFile,
                              actualWorksheetFile);
            ueProc.StartInfo = startInfo;
            ueProc.Start();
            Assert.Fail(message);
        }

        private static void WriteRecordsToFile(Bytes bytes, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            FileStream fs = fi.Open(FileMode.Create, FileAccess.Write);
            int pos = 0;
            StringBuilder sb = new StringBuilder();
            while (pos < bytes.Length)
            {
                byte[] ridArray = bytes.Get(pos, 2).ByteArray;
                sb.Append(RID.Name(ridArray));
                sb.Append(" ");
                pos += 2;
                ushort len = BitConverter.ToUInt16(bytes.Get(pos, 2).ByteArray, 0);
                pos += 2;
                byte[] byteArray = bytes.Get(pos, len).ByteArray;
                foreach (byte b in byteArray)
                {
                    sb.Append(GetByteHex(b));
                    sb.Append(" ");
                }
                sb.Append(Environment.NewLine);
                pos += byteArray.Length;
            }
            string records = sb.ToString();
            fs.Write(Encoding.ASCII.GetBytes(records), 0, records.Length);
            fs.Flush();
            fs.Close();
        }

        private static string GetByteHex(byte b)
        {
            int int1 = (int)Math.Floor(b * 1.0 / 16);
            int int2 = (int)(b % 16);

            return new string(new char[] { GetHexChar(int1), GetHexChar(int2) });
        }

        private static char GetHexChar(int i)
        {
            if (i <= 9)
                i += 48;
            else
                i += 55;
            return (char)i;
        }

        private static bool IsEOF(FileStream stream)
        {
            return (stream.Position >= stream.Length);
        }

        private static void WriteBytesToFile(byte[] bytes, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (File.Exists(fi.Name))
                File.Delete(fi.Name);
            FileStream fileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.Read);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
            fileStream.Dispose();
        }

        private static void WriteBytesToFile(byte[] bytes)
        {
            WriteBytesToFile(bytes, "test.bin");
        }

        private static void WriteBytesToFile(Bytes bytes, string fileName)
        {
            WriteBytesToFile(bytes.ByteArray, fileName);
        }

        private static readonly string[] NORTHWIND_TABLE_NAMES = new string[]
            {
                "Categories",
                "Customers",
                "Employees",
                "Order Details",
                "Orders",
                "Products",
                "Shippers",
                "Suppliers"
            };

        private DataSet GetNorthwindDataSet()
        {
            DataSet dataSet = new DataSet("Northwind");

            string northwindMdb = "Northwind.mdb";

            //NOTE: If you're gonna use a .mdb, store it in SVN with a .ref appended,
            //then copy it to a .mdb, use it, and delete the .mdb after the test.
            //JET insists on updating the file every time it's accessed (not good for source control).
            File.Copy(Path.Combine(TestsConfig.ReferenceFileFolder, northwindMdb + ".ref"), northwindMdb, true);

            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + northwindMdb + ";";
                conn.Open();
                foreach (string tableName in NORTHWIND_TABLE_NAMES)
                {
                    string sql = string.Format("select * from [{0}]", tableName);
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable table = dataSet.Tables.Add(tableName);
                            table.Load(reader);
                        }
                    }
                }
            }

            File.Delete("Northwind.mdb");

            return dataSet;
        }

#if !SILVERLIGHT
        [Test]
        public void WriteNorthwindDataSetToXls()
        {
            string northwindXls = "Northwind.xls";

            using (DataSet dataSet = GetNorthwindDataSet())
            {
                XlsDocument xls = new XlsDocument(dataSet);
                SetTestMetadata(xls); //so metadata doesn't differ on different test machines and kill the compare
                Assert.AreEqual(NORTHWIND_TABLE_NAMES.Length, xls.Workbook.Worksheets.Count, "Worksheet count");
                xls.Save(true);
                AssertFilesBinaryEqual(Path.Combine(TestsConfig.ReferenceFileFolder, northwindXls), northwindXls);
            }
        }
#endif

        [Test]
        public void MyXlsShouldReadSameValuesFromNorthwindXlsAsAdoFromMdb()
        {
            using (DataSet dataSet = GetNorthwindDataSet())
            {
                XlsDocument newDoc = new XlsDocument(Path.Combine(TestsConfig.ReferenceFileFolder, "Northwind.xls")); //read it back in to test reading
                foreach (string tableName in NORTHWIND_TABLE_NAMES)
                {
                    DataTable table = dataSet.Tables[tableName];
                    Worksheet sheet = newDoc.Workbook.Worksheets[tableName];
                    ushort colIndex = 1;
                    ushort rowIndex = 1;
                    foreach (DataColumn tableColumn in table.Columns)
                    {
                        Assert.AreEqual(tableColumn.ColumnName, sheet.Rows[1].CellAtCol(colIndex++).Value);
                    }
                    foreach (DataRow tableRow in table.Rows)
                    {
                        rowIndex++;
                        colIndex = 0;
                        Row row = sheet.Rows[rowIndex];
                        foreach (DataColumn tableColumn in table.Columns)
                        {
                            colIndex++;
                            object actualObject = null;
                            if (row.CellExists(colIndex))
                                actualObject = row.CellAtCol(colIndex).Value;
                            object expectedObject = tableRow[tableColumn];
                            string expectedValue = expectedObject.ToString();
                            Type dataType = tableColumn.DataType;
                            string failMessage = string.Format(
                                "Actual = value read by MyXls from .XLS{0}Expected = value read by ADO.NET from .MDB{0}Sheet {1} Row {2} Column {3}",
                                Environment.NewLine, tableName, rowIndex - 1, tableColumn.ColumnName);
                            if (dataType == typeof(byte[]))
                            {
                                expectedValue = string.Format("[ByteArray({0})]", ((byte[])expectedObject).Length.ToString());
                                Assert.AreEqual(expectedValue, actualObject, failMessage);
                            }
                            else if (dataType == typeof(DateTime))
                            {
                                if (expectedObject == DBNull.Value)
                                {
                                    Assert.IsNull(actualObject, failMessage);
                                }
                                else
                                {
                                    expectedValue = Convert.ToDouble(((DateTime)expectedObject).ToOADate()).ToString();
                                    Assert.AreEqual(expectedValue, actualObject.ToString(), failMessage);
                                }
                            }
                            else if (dataType == typeof(bool))
                            {
                                expectedObject = (bool)expectedObject ? 1 : 0;
                                Assert.AreEqual(expectedObject, actualObject, failMessage);
                            }
                            else if (dataType == typeof(string))
                            {
                                if (expectedObject == DBNull.Value)
                                    expectedObject = null;
                                Assert.AreEqual(expectedObject, actualObject, failMessage);
                            }
                            else if (dataType == typeof(Single) || dataType == typeof(double))
                            {
                                double expectedDouble = Convert.ToDouble(expectedObject);
                                double actualDouble = Convert.ToDouble(actualObject);
                                Assert.AreEqual(expectedDouble, actualDouble, 0.000001, failMessage);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void ExcelOleShouldReadSameValuesFromNorthwindXlsAsAdoFromMdb()
        {
            using (DataSet dataSet = GetNorthwindDataSet())
            {
                string[] lines = new string[0];

                //Uncomment the below to recreate Northwind.csv (I can't imagine what would change
                //that would require this, though...
                /*            string fileName = "cscript.exe";
                            string script = Path.Combine(TestsConfig.ScriptsFolder, "WriteAllCellValues.vbs");
                            string xlsFile = Path.Combine(TestsConfig.ReferenceFileFolder, "Northwind.xls");
                            Assert.IsTrue(File.Exists(script), "Script exists");
                            Assert.IsTrue(File.Exists(xlsFile), "XLS File exists");

                            string arguments = string.Format("\"{0}\" //B //NoLogo \"{1}\"", script, xlsFile);

                            string errors = string.Empty;
                            string output = CommandLineHelper.Run(fileName, arguments, out errors);

                            Assert.IsTrue(string.IsNullOrEmpty(errors), "Errors should be null or Empty");
                            lines = output.Split(Environment.NewLine.ToCharArray());
                            File.WriteAllLines("Northwind.csv", lines);*/

                lines = new List<string>(File.ReadAllLines(Path.Combine(TestsConfig.ReferenceFileFolder, "Northwind.csv"))).ToArray();

                Assert.Greater(lines.Length, 0, "Number of lines read");

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new char[] { '\t' }, 4);
                    int rowIndex = int.Parse(parts[1]);
                    if (rowIndex == 1)
                        continue; //skip header row
                    int sheetIndex = int.Parse(parts[0]);
                    int colIndex = int.Parse(parts[2]);
                    string actualValue = parts[3];
                    DataTable table = dataSet.Tables[NORTHWIND_TABLE_NAMES[sheetIndex - 1]];
                    DataColumn column = table.Columns[colIndex - 1];
                    object expectedObject = table.Rows[rowIndex - 2][column];
                    string expectedValue = expectedObject.ToString();
                    Type dataType = column.DataType;
                    if (dataType == typeof(byte[]))
                    {
                        expectedValue = string.Format("[ByteArray({0})]", ((byte[])expectedObject).Length.ToString());
                        Assert.AreEqual(expectedValue, actualValue,
                                        string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                      colIndex));
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        if (expectedObject == DBNull.Value)
                        {
                            Assert.IsTrue(string.IsNullOrEmpty(actualValue), string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                                                           colIndex));
                        }
                        else
                        {
                            expectedValue = Convert.ToDouble(((DateTime)expectedObject).ToOADate()).ToString();
                            Assert.AreEqual(expectedValue, actualValue,
                                            string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                          colIndex));
                        }
                    }
                    else if (dataType == typeof(bool))
                    {
                        expectedValue = (bool)expectedObject ? "1" : "0";
                        Assert.AreEqual(expectedValue, actualValue,
                                        string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                      colIndex));
                    }
                    else if (dataType == typeof(string))
                    {
                        expectedValue = expectedValue.Substring(0, Math.Min(expectedValue.Length, 255)); //there is apparently a 255-char limit
                        //in Excel VBA reading text from a cell
                        expectedValue = expectedValue.Replace("\r\n", " ");
                        Assert.AreEqual(expectedValue, actualValue,
                                        string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                      colIndex));
                    }
                    else if (dataType == typeof(Single) || dataType == typeof(double))
                    {
                        double expectedDouble = Convert.ToDouble(expectedObject);
                        double actualDouble = double.Parse(actualValue);
                        Assert.AreEqual(expectedDouble, actualDouble, 0.000001,
                                        string.Format("Value Sheet {0} Row {1} Col {2}", sheetIndex, rowIndex,
                                                      colIndex));
                    }
                }
            }
        }

        [Test]
        public void Write33725_0()
        {
            string fileName = Write1Cell((double) 33725.0);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, "33725", "Write 33725.0");
        }

        [Test]
        public void SharedStringToggle()
        {
            XlsDocument shareOff = new XlsDocument();
            Assert.IsFalse(shareOff.Workbook.ShareStrings, "Default ShareStrings setting");

            XlsDocument shareOn = new XlsDocument();
            shareOn.Workbook.ShareStrings = true;

            Worksheet sheetOff = shareOff.Workbook.Worksheets.Add("Sheet1");
            Worksheet sheetOn = shareOn.Workbook.Worksheets.Add("Sheet1");

            string text = "abc";
            sheetOff.Cells.Add(1, 1, text);
            sheetOff.Cells.Add(1, 2, text);

            sheetOn.Cells.Add(1, 1, text);
            sheetOn.Cells.Add(1, 2, text);

            Assert.AreEqual(0, shareOff.Workbook.SharedStringTable.CountUnique,
                            "ShareStrings false should have 0 SharedStringTable entries");
            Assert.AreEqual(1, shareOn.Workbook.SharedStringTable.CountUnique,
                            "ShareStrings true should have 1 SharedStringTable entry");
        }

        [Test]
        public void StoreAndRetrieveSharedString()
        {
            string text = "abc";
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    doc.Workbook.ShareStrings = true;
                    Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells.Add(1, 1, text);
                    Assert.AreEqual(text, sheet.Rows[1].CellAtCol(1).Value, "Shared String Value retrieved from XlsDocument");
                };
            string fileName = WriteDocument(docDelegate);
            AssertPropertyViaExcelOle(fileName, CellProperties.Value, text,
                                      "Shared String Value retrieved via Excel OLE");
        }

		[Test]
		public void RetrieveDuplicateSharedString()
		{
			string text1 = "NUMBER";
			string text2 = "RKInt div100 = 1";
			string fileName = TestsConfig.ReferenceFileFolder + "ReadNumbers.xls";
			AssertPropertyViaExcelOle(1, 1, 2, fileName, CellProperties.Value, text1,
									  "Shared String Value retrieved via Excel OLE");
			AssertPropertyViaExcelOle(1, 2, 2, fileName, CellProperties.Value, text2,
									  "Shared String Value retrieved via Excel OLE");
			AssertPropertyViaExcelOle(1, 3, 2, fileName, CellProperties.Value, text2,
									  "Shared String Value retrieved via Excel OLE");
		}

        [Test]
        public void SstContinueRecord()
        {
            //8,216 bytes available for data in SST Record
            //(before CONTINUE record is necessary)
            //A 7-char single-byte (compressed unicode) text string, plus
            //the 3 bytes at the beginning of the Unicode
            //string data, gives 10 bytes per 7-char string.
            //This gives us 821 7-char strings, the 822nd
            //spilling across to a CONTINUE record.

            XlsDocumentDelegate noContinueDocDelegate = delegate(XlsDocument doc)
                {
                    doc.Workbook.ShareStrings = true;
                    Cells cells = doc.Workbook.Worksheets.Add("Sheet1").Cells;
                    for (int i = 0; i < 821; i++)
                        cells.Add(i + 1, 1, (i + 1000001).ToString());
                    Assert.AreEqual(821, doc.Workbook.SharedStringTable.CountUnique, "Unique values in SST");
                };
            string noContinueFile = WriteDocument(noContinueDocDelegate);
            AssertPropertyViaExcelOle(1, 821, 1, noContinueFile, CellProperties.Value, "1000821", "Read last string before CONTINUE");

            List<Record> noContinueWorkbookRecords = null;
            Workbook.BytesReadCallback bytesReadCallback = delegate(List<Record> records)
                {
                    noContinueWorkbookRecords = records;
                };
            new XlsDocument(noContinueFile, bytesReadCallback);
            Assert.IsNotNull(noContinueWorkbookRecords, "Workbook records list");
            Record sst = GetSstRecord(noContinueWorkbookRecords);
            Assert.IsNotNull(sst, "SST Record");
            Assert.AreEqual(0, sst.Continues.Count, "SST Continues");

            XlsDocumentDelegate continueDoc = delegate(XlsDocument doc)
                {
                    doc.Workbook.ShareStrings = true;
                    Cells cells = doc.Workbook.Worksheets.Add("Sheet1").Cells;
                    for (int i = 0; i < 822; i++)
                        cells.Add(i + 1, 1, (i + 1000001).ToString());
                    Assert.AreEqual(822, doc.Workbook.SharedStringTable.CountUnique, "Unique values in SST");
                };
            string continueFile = WriteDocument(continueDoc);
            AssertPropertyViaExcelOle(1, 822, 1, continueFile, CellProperties.Value, "1000822", "Read string split over to CONTINUE");

            List<Record> continueWorkbookRecords = null;
            bytesReadCallback = delegate(List<Record> records)
                {
                    continueWorkbookRecords = records;
                };
            new XlsDocument(continueFile, bytesReadCallback);
            Assert.IsNotNull(continueWorkbookRecords, "Workbook records list");
            sst = GetSstRecord(continueWorkbookRecords);
            Assert.IsNotNull(sst, "SST Record");
            Assert.AreEqual(1, sst.Continues.Count, "SST CONTINUE Records");
        }

        private Record GetSstRecord(List<Record> records)
        {
            foreach (Record record in records)
            {
                if (record.RID == RID.SST)
                    return record;
            }
            return null;
        }

        [Test]
        public void SstTwoContinueRecords()
        {
            //8,216 bytes available for string records in SST Record
            //(before CONTINUE record is necessary)
            //8,223 bytes available for string records in CONTINUE Record
            //(before next CONTINUE record is necessary)
            //A 7-char single-byte (compressed unicode) text string, plus
            //the 3 bytes at the beginning of the Unicode
            //string data, gives 10 bytes per 7-char string record.
            //This gives us 821 7-char strings, the 822nd
            //spilling 4 bytes across to the first CONTINUE record.
            //Then another 821 (1,643 total), the 1,644th spilling 1 byte
            //over to the second CONTINUE Record.

            int stringsToWrite = 821;

            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
                {
                    doc.Workbook.ShareStrings = true;
                    Cells cells = doc.Workbook.Worksheets.Add("Sheet1").Cells;
                    for (int i = 0; i < stringsToWrite; i++)
                        cells.Add(i + 1, 1, (i + 1000001).ToString());
                    Assert.AreEqual(stringsToWrite, doc.Workbook.SharedStringTable.CountUnique, "Unique values in SST");
                };
            string fileName = WriteDocument(docDelegate);

            List<Record> workbookRecords = null;
            int continueRecordCount = 0;
            Workbook.BytesReadCallback bytesReadCallback = delegate(List<Record> records)
                {
                    workbookRecords = records;
                };
            new XlsDocument(fileName, bytesReadCallback);
            Assert.IsNotNull(workbookRecords, "Workbook records list");
            Record sst = GetSstRecord(workbookRecords);
            Assert.AreEqual(0, sst.Continues.Count, "SST CONTINUE Records");

            //reset
            workbookRecords = null;
            continueRecordCount = 0;

            stringsToWrite = 822;
            fileName = WriteDocument(docDelegate);
            new XlsDocument(fileName, bytesReadCallback);
            Assert.IsNotNull(workbookRecords, "Workbook records list");
            sst = GetSstRecord(workbookRecords);
            Assert.AreEqual(1, sst.Continues.Count, "SST CONTINUE Records");

            //reset
            workbookRecords = null;
            continueRecordCount = 0;

            stringsToWrite = 1643;
            fileName = WriteDocument(docDelegate);
            new XlsDocument(fileName, bytesReadCallback);
            Assert.IsNotNull(workbookRecords, "Workbook records list");
            sst = GetSstRecord(workbookRecords);
            Assert.AreEqual(1, sst.Continues.Count, "SST CONTINUE Records");

            //reset
            workbookRecords = null;
            continueRecordCount = 0;

            stringsToWrite = 1644;
            fileName = WriteDocument(docDelegate);
            new XlsDocument(fileName, bytesReadCallback);
            Assert.IsNotNull(workbookRecords, "Workbook records list");
            sst = GetSstRecord(workbookRecords);
            Assert.AreEqual(2, sst.Continues.Count, "SST CONTINUE Records");
            AssertPropertyViaExcelOle(1, 1644, 1, fileName, CellProperties.Value, (stringsToWrite + 1000000).ToString(),
                                      "Last String value via Excel OLE");
        }

		[Test]
		public void SstContinueRecordNoSplit()
		{
			// This is an example of the CONTINUE record where the spillover
			// string is not actually split across two SST records.  Once the 
			// first record is filled up with 632 strings, the 633th string 
			// is placed in the CONTINUE record.
			//
			// This means that there are 632 whole 10-char strings (13 bytes 
			// per 10-char string * 632 = 8216) in the first record with 1 
			// whole 10-char string in the CONTINUE record.

			XlsDocumentDelegate continueDocDelegate = delegate(XlsDocument doc)
			{
				doc.Workbook.ShareStrings = true;
				Cells cells = doc.Workbook.Worksheets.Add("Sheet1").Cells;
				for (int i = 0; i < 633; i++)
					cells.Add(i + 1, 1, (i + 1000000001).ToString());
				Assert.AreEqual(633, doc.Workbook.SharedStringTable.CountUnique, "Unique values in SST");
			};
			string continueFile = WriteDocument(continueDocDelegate);
			AssertPropertyViaExcelOle(1, 633, 1, continueFile, CellProperties.Value, "1000000633", "Read last string after CONTINUE");

			List<Record> continueWorkbookRecords = null;
			Workbook.BytesReadCallback bytesReadCallback = delegate(List<Record> records)
			{
				continueWorkbookRecords = records;
			};
			new XlsDocument(continueFile, bytesReadCallback);
			Assert.IsNotNull(continueWorkbookRecords, "Workbook records list");
			Record sst = GetSstRecord(continueWorkbookRecords);
			Assert.IsNotNull(sst, "SST Record");
			Assert.AreEqual(1, sst.Continues.Count, "SST Continues");
		}

		[Test]
		public void SstContinueRecordNoSplitFile()
		{
			// This is basically the same as the test above except it uses a 
			// file created by Excel and has two CONTINUE records 
			// (632 + 632 + 1 = 1265).

			string fileName = TestsConfig.ReferenceFileFolder + "SstContinue.xls";
			List<Record> continueWorkbookRecords = null;
			Workbook.BytesReadCallback bytesReadCallback = delegate(List<Record> records)
			{
				continueWorkbookRecords = records;
			};
			XlsDocument xls = new XlsDocument(fileName, bytesReadCallback);
			Assert.IsNotNull(continueWorkbookRecords, "Workbook records list");
			Record sst = GetSstRecord(continueWorkbookRecords);
			Assert.IsNotNull(sst, "SST Record");
			Assert.AreEqual(2, sst.Continues.Count, "SST Continues"); 
			
			Assert.AreEqual(1, xls.Workbook.Worksheets.Count);
			Worksheet ws = xls.Workbook.Worksheets[0];
			Assert.AreEqual(1266, ws.Rows.Count);
			Assert.AreEqual(1265, ws.Rows.MaxRow);
			Assert.AreEqual("Test000632", ws.Rows[632].CellAtCol(1).Value);
			Assert.AreEqual("Test001264", ws.Rows[1264].CellAtCol(1).Value);
			Assert.AreEqual("Test001265", ws.Rows[1265].CellAtCol(1).Value);
		}

        [Test]
        public void Read2007BlankBudgetWorksheet()
        {
            string fileName = Path.Combine(TestsConfig.ReferenceFileFolder, "BlankBudgetWorksheet.xls");
            List<Record> workbookRecords;
            Workbook.BytesReadCallback workbookBytesReadCallback = delegate(List<Record> records)
               {
                   workbookRecords = records;
               };
            XlsDocument xls = new XlsDocument(fileName, workbookBytesReadCallback);
            Assert.AreEqual(3, xls.Workbook.Worksheets.Count, "Number of worksheets");
            Assert.AreEqual("Budget", xls.Workbook.Worksheets[0].Name, "Worksheet 1 name");
            Assert.AreEqual("Income", xls.Workbook.Worksheets[1].Name, "Worksheet 2 name");
            Assert.AreEqual("Expenses", xls.Workbook.Worksheets[2].Name, "Worksheet 3 name");
            Worksheet sheet = xls.Workbook.Worksheets[0];
            Assert.AreEqual("See reverse for instructions and guidelines", sheet.Rows[6].CellAtCol(8).Value,
                            "Cell H6 hyperlink text");
            Assert.AreEqual("Budget Plan", sheet.Rows[7].CellAtCol(10).Value, "Cell J7 value");
            Assert.AreEqual("Administrative Support (12% of Revenue)", sheet.Rows[28].CellAtCol(7).Value,
                            "Cell G28 value");
            Assert.AreEqual(6801, sheet.Rows[10].CellAtCol(3).Value, "Cell C10 value");
            Assert.AreEqual("- 20", sheet.Rows[10].CellAtCol(6).Value, "Cell F10 Formula result value");
            Assert.AreEqual(0, sheet.Rows[10].CellAtCol(10).Value, "Cell J10 Multi-RK Value");
            Assert.AreEqual(0, sheet.Rows[10].CellAtCol(11).Value, "Cell J11 Multi-RK Value");
        }

        [Test]
        public void ReadYuanWorksheet()
        {
            string fileName = Path.Combine(TestsConfig.ReferenceFileFolder, "Yuan697.xls");
            List<Record> workbookRecords;
            Workbook.BytesReadCallback workbookBytesReadCallback = delegate(List<Record> records)
               {
                   workbookRecords = records;
               };
            XlsDocument xls = new XlsDocument(fileName, workbookBytesReadCallback);
            Assert.AreEqual(1, xls.Workbook.Worksheets.Count, "Number of worksheets");
            Assert.AreEqual("Sheet1", xls.Workbook.Worksheets[0].Name, "Worksheet 1 name");
            Worksheet sheet = xls.Workbook.Worksheets[0];
            Assert.AreEqual("num", sheet.Rows[1].CellAtCol(1).Value, "Cell A1 text");
            Assert.AreEqual("name", sheet.Rows[1].CellAtCol(2).Value, "Cell B1 text");
            Assert.AreEqual("sex", sheet.Rows[1].CellAtCol(3).Value, "Cell C1 text");
            Assert.AreEqual(2005151001, sheet.Rows[2].CellAtCol(1).Value, "Cell A2 value");
            Assert.AreEqual(2005151002, sheet.Rows[3].CellAtCol(1).Value, "Cell A3 value");
            Assert.AreEqual(2005151024, sheet.Rows[4].CellAtCol(1).Value, "Cell A4 value");
        }

		[Test]
		public void CellFontAndColorAreWrittenAndRead()
		{
			string filename = WriteDocument(delegate(XlsDocument doc)
                	{
                		Worksheet sheet = doc.Workbook.Worksheets.Add("Font");
                		Cell yellowCell = sheet.Cells.Add(1, 1, "Yellow Background");
                		yellowCell.PatternColor = Colors.Yellow;
                		yellowCell.Pattern = FillPattern.Solid;
                		yellowCell.Font.Bold = true;
                		yellowCell.Font.Color = Colors.Blue;
                	});


			XlsDocument read = new XlsDocument(filename);
			Cell c = read.Workbook.Worksheets[0].Rows[1].CellAtCol(1);
			//Assert.AreEqual(Colors.Yellow, c.PatternColor);  still not working  written, not read correctly
			//Assert.AreEqual(FillPattern.Solid, c.Pattern);
			Assert.IsTrue(c.Font.Bold);
			Assert.AreEqual(Colors.Blue, c.Font.Color);

		}
    }
}
