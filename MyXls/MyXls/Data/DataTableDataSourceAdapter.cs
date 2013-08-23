using System;
using System.Collections;
using System.Data;

namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// Adapter for a DataSource
	/// </summary>
	/// <typeparam name="TItem">DataTable item type.  Typically of type DataRow</typeparam>
	public class DataTableDataSourceAdapter<TItem> : DataSourceAdapter<TItem>
	{
		/// <summary>
		/// Creates a new adapter.
		/// </summary>
		/// <param name="dataSource">Source DataTable</param>
		public DataTableDataSourceAdapter(DataTable dataSource) : base(dataSource) {}

		/// <summary>
		/// Gets the value of the data item specific to each datasource.
		/// </summary>
		/// <param name="dataItem">The data item.</param>
		/// <param name="field">The field bound to this data item.</param>
		/// <returns>Value or null</returns>
		public override object GetValue(TItem dataItem, IAdapterBoundField field)
		{
			object result = null;
			if (!String.IsNullOrEmpty(field.DataField))
			{
				DataRow row = dataItem as DataRow;
				result = row.IsNull(field.DataField) ? null : row[field.DataField];
			}
			return result;
		}

		/// <summary>
		/// Returns an enumerator for the data source.
		/// </summary>
		/// <returns>Instance implementing IEnumerable</returns>
		public override IEnumerable GetEnumerator()
		{
			return ((DataTable)_dataSource).Rows;
		}
	}
}
