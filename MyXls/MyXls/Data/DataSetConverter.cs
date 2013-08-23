using System.Collections.Generic;
using System.Data;

namespace org.in2bits.MyXls.Data
{
	/// <summary>Converts a dataset into an xls document.</summary>
	public class DataSetConverter
	{
		private IList<DataSourceConverter<DataRow>> _tableAdapters;

		/// <summary>DataSourceAdapters associated with this DataSetConverter</summary>
		public IList<DataSourceConverter<DataRow>> TableAdapters
		{
			get { return _tableAdapters = _tableAdapters ?? new List<DataSourceConverter<DataRow>>(); }
			set { _tableAdapters = new List<DataSourceConverter<DataRow>>(value); }
		}

		/// <summary>
		/// Create an xls document from a <see cref="DataSet"/>
		/// </summary>
		/// <param name="dataset">Source <see cref="DataSet"/> </param>
		/// <returns>New <see cref="XlsDocument"/> containg the data from the <see cref="DataSet"/>.</returns>
		public XlsDocument CreateDocument(DataSet dataset)
		{
			return CreateDocument(null, dataset);
		}

		/// <summary>
		/// Populates an xls document from a <see cref="DataSet"/>
		/// </summary>
		/// <param name="dataset">Source <see cref="DataSet"/> </param>
		/// <param name="document">Document to populate with the data.</param>
		/// <returns><see cref="XlsDocument"/> containg the data from the <see cref="DataSet"/>.</returns>
		public XlsDocument CreateDocument(XlsDocument document, DataSet dataset)
		{
			document = document ?? new XlsDocument();
			if (_tableAdapters == null || _tableAdapters.Count == 0)
			{
				_tableAdapters = CreateDefaultAdapters(dataset);
			}

			for (int i = 0; i < _tableAdapters.Count; i++)
			{
				_tableAdapters[i].PopulateWorksheet(document, null, dataset.Tables[i], dataset.Tables[i].TableName);
			}

			return document;
		}

		/// <summary>
		/// Creates <see cref="DataSourceConverter{DataRow}"/>s based off the <see cref="DataTable"/>.  Defaults to all 
		/// columns defined in each <see cref="DataTable"/>
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>DataSource Converter</returns>
		public static DataSourceConverter<DataRow> CreateDefaultDataTableConverter(DataTable table)
		{
			DataSourceConverter<DataRow> converter = new DataSourceConverter<DataRow>();
			foreach (DataColumn column in table.Columns)
			{
				converter.Fields.Add(new AdapterBoundField<DataRow>(column.ColumnName));
			}
			return converter;
		}

		/// <summary>
		/// Creates <see cref="DataSourceConverter{DataRow}"/>s based off the <see cref="DataTable"/>s in the <see cref="DataSet"/>.  Defaults to all 
		/// columns defined in each <see cref="DataTable"/>
		/// </summary>
		/// <param name="dataset"></param>
		/// <returns></returns>
		public static IList<DataSourceConverter<DataRow>> CreateDefaultAdapters(DataSet dataset)
		{
			var result = new List<DataSourceConverter<DataRow>>();
			foreach (DataTable table in dataset.Tables)
			{
				result.Add(CreateDefaultDataTableConverter(table));
			}
			return result;
		}
	}
}
