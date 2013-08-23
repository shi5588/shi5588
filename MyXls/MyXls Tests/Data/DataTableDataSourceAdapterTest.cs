using System;
using System.Collections;
using System.Data;
using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class DataTableDataSourceAdapterTest
	{
		private DataTable _data;
		private DataTableDataSourceAdapter<DataRow> _adapter;

		[SetUp]
		public void Setup()
		{
			_data = new DataTable("Test");
			_data.Columns.Add("Id", typeof(Guid));
			_data.Columns.Add("Name", typeof(string));
			_data.Columns.Add("Age", typeof(int));

			_data.Rows.Add(Guid.NewGuid(), "Mickey Mouse", 52);
			_data.Rows.Add(Guid.NewGuid(), null, 48);
			_data.Rows.Add(Guid.NewGuid(), "Goofy", 51);
			_data.Rows.Add(Guid.NewGuid(), "Donald Duck", 49);		

			_adapter = new DataTableDataSourceAdapter<DataRow>(_data);
		}

		[Test]
		public void GetEnumerator()
		{
			IEnumerable enumerable = _adapter.GetEnumerator();
			Assert.IsNotNull(enumerable);
			Assert.AreEqual(typeof(DataRowCollection), enumerable.GetType());
		}

		[Test]
		public void GetValueNull()
		{
			IEnumerator enumerator = _adapter.GetEnumerator().GetEnumerator();
			for(int i = 0; i < 2; i++)
			{
				enumerator.MoveNext();
			}
			DataRow row = enumerator.Current as DataRow;
			Assert.IsNotNull(row);
			object value = _adapter.GetValue(row, new AdapterBoundField<DataRow>("Name"));
			Assert.IsNull(value);
		}

		[Test]
		public void GetValueNotNull()
		{
			IEnumerator enumerator = _adapter.GetEnumerator().GetEnumerator();
			enumerator.MoveNext();
			DataRow row = enumerator.Current as DataRow;
			
			Assert.IsNotNull(row);
			object value = _adapter.GetValue(row, new AdapterBoundField<DataRow>("Name"));
			Assert.IsNotNull(value);
			Assert.AreEqual("Mickey Mouse", value);
		}
	}
}
