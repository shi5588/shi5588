using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class AdapterBoundFieldTest
	{
		private AdapterFieldEventArgs<int> _storedArgs;
		private object _storedSender;

		[SetUp]
		public void Setup()
		{
			_storedArgs = null;
			_storedSender = null;
		}

		[Test]
		public void HeaderTextEmptyOrNull()
		{
			string dataField = "DataField";
			AdapterBoundField<int> field = new AdapterBoundField<int>(dataField);

			Assert.AreEqual(dataField, field.HeaderText, "HeaderText property not assigned should return DataField");
		}

		[Test]
		public void OnBoundField_CellDataBoundCalled()
		{
			XlsDocument doc = new XlsDocument();
			Worksheet sheet = doc.Workbook.Worksheets.Add("Test");
			Row row = sheet.Rows.AddRow(1);
			Cell cell = sheet.Cells.Add(1, 1);

			AdapterBoundField<int> field = new AdapterBoundField<int>("test");
			field.CellDataBound = TestCellDataBound;
			field.OnBoundField(this, cell, row, 50);
			

			Assert.IsNotNull(_storedArgs, "event not called");
			Assert.IsNotNull(_storedSender, "event not called");
			Assert.AreEqual(this, _storedSender);
			Assert.AreEqual(50, _storedArgs.DataItem);
			Assert.AreEqual(cell, _storedArgs.Cell);
			Assert.AreEqual(row, _storedArgs.Row);
		}

		private void TestCellDataBound(object sender, AdapterFieldEventArgs<int> args)
		{
			_storedArgs = args;
			_storedSender = sender;
		}
	}
}
