using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class DataSetConverterTest
	{
		private DataSet _data;

		[SetUp]
		public void Setup()
		{
			_data = new DataSet("Test");
			_data.Tables.Add(DataSourceConverterTest.GetDataTable());
			_data.Tables.Add(GetSecondDataTable());
		}

		public static DataTable GetSecondDataTable()
		{
			DataTable result = new DataTable("Pixar Movies");
			result.Columns.Add("Name", typeof(string));
			result.Columns.Add("Year", typeof(int));

			result.Rows.Add("Toy Story", 1995);
			result.Rows.Add("A Bug's Life", 1998);
			result.Rows.Add("Toy Story 2", 1999);
			result.Rows.Add("Monsters Inc.", 2001);
			result.Rows.Add("Finding Nemo", 2003);
			result.Rows.Add("The Incredibles", 2004);
			result.Rows.Add("Cars", 2006);
			result.Rows.Add("Ratatouille", 2007);
			result.Rows.Add("Wall-E", 2008);
			result.Rows.Add("Up", 2009);

			return result;
		}

		[Test]
		public void WithDefaultConverters()
		{
			DataSetConverter converter = new DataSetConverter();
			XlsDocument doc = new XlsDocument();

			converter.CreateDocument(doc, _data);

			Assert.AreEqual(_data.Tables.Count, doc.Workbook.Worksheets.Count);

			for (int i = 0; i < doc.Workbook.Worksheets.Count; i++)
			{
				Worksheet worksheet = doc.Workbook.Worksheets[i];
				Assert.AreEqual(_data.Tables[i].TableName, worksheet.Name);

				// validte header row
				ValidateSheetFromDataTable(worksheet, _data.Tables[i]);
			}
		}

		private void ValidateSheetFromDataTable(Worksheet worksheet, DataTable table) 
		{
			for (ushort col = 1; col < table.Columns.Count; col++)
			{
				Assert.AreEqual(table.Columns[col - 1].ColumnName, worksheet.Rows[1].CellAtCol(col).Value);
			}

			for (ushort row = 2; row < table.Rows.Count; row++)
			{
				for (ushort col = 1; col < table.Columns.Count; col++)
				{
					Assert.AreEqual(table.Rows[row - 2][col - 1], worksheet.Rows[row].CellAtCol(col).Value);
				}
			}
		}

		[Test]
		public void WithCustomConverters()
		{
			DataSetConverter converter = new DataSetConverter();
			DataSourceConverter<DataRow> table1Converter = new DataSourceConverter<DataRow>();
			table1Converter.Fields.Add(new AdapterBoundField<DataRow>("Name", "Character Name"));
			table1Converter.Fields.Add(new AdapterBoundField<DataRow>("Age", "Age", "{0:###}"));
			converter.TableAdapters.Add(table1Converter);
			converter.TableAdapters.Add(DataSetConverter.CreateDefaultDataTableConverter(_data.Tables[1]));
			XlsDocument doc = new XlsDocument();

			converter.CreateDocument(doc, _data);

			Assert.AreEqual(_data.Tables.Count, doc.Workbook.Worksheets.Count);
			DataSourceConverterTest.ValidateWorksheetFromDataTable(doc.Workbook.Worksheets[0], _data.Tables[0]);
			ValidateSheetFromDataTable(doc.Workbook.Worksheets[1], _data.Tables[1]);
		}
		
	}
}
