using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class DataSourceConverterTest
	{
		private DataTable _data;
		private List<DisneyCharacter> _dataList;

		[SetUp]
		public void Setup()
		{
			_data = GetDataTable();
			_dataList = GetData();
		}

		public static DataTable GetDataTable()
		{
			DataTable result = new DataTable("Disney");
			result.Columns.Add("Name", typeof(string));
			result.Columns.Add("Age", typeof(int));

			result.Rows.Add("Mickey Mouse", 52);
			result.Rows.Add("Minny Mouse", 48);
			result.Rows.Add("Goofy", 51);
			result.Rows.Add("Donald Duck", 49);

			return result;
		}

		[Test]
		public void CreateWorksheetWithDataTable()
		{
			DataSourceConverter<DataRow> converter = new DataSourceConverter<DataRow>();
			converter.Fields.Add(new AdapterBoundField<DataRow>("Name", "Character Name"));
			converter.Fields.Add(new AdapterBoundField<DataRow>("Age", "Age", "{0:###}"));
			XlsDocument doc = new XlsDocument();

			converter.CreateWorksheet(doc, _data, _data.TableName);

			Assert.AreEqual(1, doc.Workbook.Worksheets.Count);
			ValidateWorksheetFromDataTable(doc.Workbook.Worksheets[0], _data);
		}

		public static void ValidateWorksheetFromDataTable(Worksheet sheet, DataTable data) 
		{
			Assert.AreEqual(data.Rows.Count + 1, sheet.Rows.Count);

			// validate header row
			Assert.AreEqual("Character Name", sheet.Rows[1].CellAtCol(1).Value);
			Assert.AreEqual("Age", sheet.Rows[1].CellAtCol(2).Value);

			ushort i = 2;
			foreach (DataRow row in data.Rows)
			{
				Assert.AreEqual(row["Name"], sheet.Rows[i].CellAtCol(1).Value);
				Assert.AreEqual(String.Format("{0:###}", row["Age"]), sheet.Rows[i].CellAtCol(2).Value);
				i++;
			}
		}

		[Test]
		public void CreateSpreadsheetWithBoldHeadersAndCallback()
		{
			DataSourceConverter<DataRow> converter = new DataSourceConverter<DataRow>();
			converter.Fields.Add(new AdapterBoundField<DataRow>("Name", "Character Name"));
			AdapterBoundField<DataRow> ageField = new AdapterBoundField<DataRow>("Age");

			// Create a delegate on the field that handles populating each row
			// this delegate is called for each row for the field in the data
			ageField.CellDataBound += delegate(object sender, AdapterFieldEventArgs<DataRow> args)
			                               {
			                               		args.Cell.Value = String.Format("{0} is {1} Years Old", args.DataItem["Name"], args.DataItem["Age"]);
			                               };

			converter.Fields.Add(ageField);
			converter.HeaderDataBound += DataSourceConverter<DataRow>.BoldAllCells;
			XlsDocument doc = new XlsDocument();
			
			converter.CreateWorksheet(doc, _data, "Testing");

			// validate worksheet is created
			Assert.AreEqual(1, doc.Workbook.Worksheets.Count);
			Assert.AreEqual(_data.Rows.Count + 1, doc.Workbook.Worksheets[0].Rows.Count);

			// validate header row
			Assert.AreEqual("Character Name", doc.Workbook.Worksheets[0].Rows[1].CellAtCol(1).Value);
			Assert.IsTrue(doc.Workbook.Worksheets[0].Rows[1].CellAtCol(1).Font.Bold);
			Assert.AreEqual("Age", doc.Workbook.Worksheets[0].Rows[1].CellAtCol(2).Value);
			Assert.IsTrue(doc.Workbook.Worksheets[0].Rows[1].CellAtCol(2).Font.Bold);

			ushort i = 2;
			foreach (DataRow row in _data.Rows)
			{
				Assert.AreEqual(row["Name"], doc.Workbook.Worksheets[0].Rows[i].CellAtCol(1).Value);
				Assert.AreEqual(String.Format("{0} is {1} Years Old", row["Name"], row["Age"]), 
					doc.Workbook.Worksheets[0].Rows[i].CellAtCol(2).Value);
				i++;
			}
		}

		public class DisneyCharacter
		{
			private string _name;
			private int _age;

			public string Name { get { return _name; } set { _name = value; } }
			public int Age { get { return _age; } set { _age = value; } }
		}

		[Test]
		public void CreateSpreadsheetFromIEnumerable()
		{
			DataSourceConverter<DisneyCharacter> converter = new DataSourceConverter<DisneyCharacter>();
			converter.Fields.Add(new AdapterBoundField<DisneyCharacter>("Name", "Character Name"));
			converter.Fields.Add(new AdapterBoundField<DisneyCharacter>("Age", null, "{0:###}"));
			XlsDocument doc = new XlsDocument();

			converter.CreateWorksheet(doc, _dataList, "testing");

			ValidateXlsToIList(doc);
		}

		private void ValidateXlsToIList(XlsDocument doc) 
		{
			Assert.AreEqual(1, doc.Workbook.Worksheets.Count);
			Assert.AreEqual(_dataList.Count + 1, doc.Workbook.Worksheets[0].Rows.Count);

			// validate worksheet is created
			Assert.AreEqual(1, doc.Workbook.Worksheets.Count);
			Assert.AreEqual(_dataList.Count + 1, doc.Workbook.Worksheets[0].Rows.Count);

			// validate header row
			Assert.AreEqual("Character Name", doc.Workbook.Worksheets[0].Rows[1].CellAtCol(1).Value);
			Assert.AreEqual("Age", doc.Workbook.Worksheets[0].Rows[1].CellAtCol(2).Value);

			ushort i = 2;
			foreach (DisneyCharacter dc in _dataList)
			{
				Assert.AreEqual(dc.Name, doc.Workbook.Worksheets[0].Rows[i].CellAtCol(1).Value);
				Assert.AreEqual(String.Format("{0:###}", dc.Age), doc.Workbook.Worksheets[0].Rows[i].CellAtCol(2).Value);
				i++;
			}
		}

		public static List<DisneyCharacter> GetData()
		{
			List<DisneyCharacter> items = new List<DisneyCharacter>();

			DisneyCharacter dc = new DisneyCharacter();
			dc.Name = "Mickey Mouse";
			dc.Age = 52;
			items.Add(dc);

			dc = new DisneyCharacter();
			dc.Name = "Minny Mouse";
			dc.Age = 48;
			items.Add(dc);

			dc = new DisneyCharacter();
			dc.Name = "Goofy";
			dc.Age = 51;
			items.Add(dc);

			dc = new DisneyCharacter();
			dc.Name = "Donald Duck";
			dc.Age = 49;
			items.Add(dc);

			return items;
		}

		[Test]
		public void CreateSpreadSheetFromObjectDataSource()
		{
			DataSourceConverter<DisneyCharacter> converter = new DataSourceConverter<DisneyCharacter>();
			converter.Fields.Add(new AdapterBoundField<DisneyCharacter>("Name", "Character Name"));
			converter.Fields.Add(new AdapterBoundField<DisneyCharacter>("Age", null, "{0:###}"));
			XlsDocument doc = new XlsDocument();
			ObjectDataSource ods = new ObjectDataSource("org.in2bits.MyXls.Data.DataSourceConverterTest, org.in2bits.MyXlsTests", "GetData");

			converter.CreateWorksheet(doc, ods, "testing");

			ValidateXlsToIList(doc);
		}
	}
}
