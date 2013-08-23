using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class DataSourceAdapterTest
	{
		public static Dictionary<Type, Type> SupportedTypes = new Dictionary<Type, Type>();

		static DataSourceAdapterTest()
		{
			SupportedTypes.Add(typeof(IEnumerable), typeof(EnumerableDataSourceAdapter<DataSourceAdapterTest>));
			SupportedTypes.Add(typeof(IDataReader), typeof(DataReaderDataSourceAdapter<DataSourceAdapterTest>));
			SupportedTypes.Add(typeof(DataTable), typeof(DataTableDataSourceAdapter<DataSourceAdapterTest>));
			SupportedTypes.Add(typeof(ObjectDataSource), typeof(ObjectDataSourceDataSourceAdapter<DataSourceAdapterTest>));
		}

		[Test]
		public void CreateSupported()
		{
			KeyValuePair<object, Type>[] dataSources = new KeyValuePair<object, Type>[]
				{
               		new KeyValuePair<object, Type>(new List<DataSourceAdapterTest>(), typeof(IEnumerable)),
					new KeyValuePair<object, Type>(new TestDataReader(), typeof(IDataReader)),
					new KeyValuePair<object, Type>(new DataTable(), typeof(DataTable)),
               		new KeyValuePair<object, Type>(new ObjectDataSource(), typeof(ObjectDataSource))
				};

			foreach (KeyValuePair<object, Type> pair in dataSources)
			{
				DataSourceAdapter<DataSourceAdapterTest> adapter = DataSourceAdapter<DataSourceAdapterTest>.CreateAdapter(pair.Key);
				Console.WriteLine("DataSource Type: {0}, Adapter Type: {1}", pair.Key.GetType().Name, adapter.GetType().Name);
				Assert.IsNotNull(adapter);
				Assert.AreEqual(SupportedTypes[pair.Value], adapter.GetType());
			}
		}

		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void CreateNotSupported()
		{
			DataSourceAdapter<object> adapter = DataSourceAdapter<object>.CreateAdapter(new object());
		}
	}
}
