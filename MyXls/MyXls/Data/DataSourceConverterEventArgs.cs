using System;

namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// Event argument data for the <see cref="DataSourceAdapter{TItem}" />
	/// </summary>
	public class DataSourceConverterEventArgs : EventArgs
	{
		/// <summary>XlsDocument row of the event.</summary>
		protected readonly Row _row;
		/// <summary>DataItem for the event.</summary>
		protected readonly object _dataItem;

		/// <summary>Creates a new DataTableEventArgs instance.</summary>
		/// <param name="row">Current xls <see cref="Row"/>.</param>
		public DataSourceConverterEventArgs(Row row) : this(row, null) {}

		/// <summary>Creates a new DataTableEventArgs instance.</summary>
		/// <param name="row">Current xls <see cref="Row"/>.</param>
		/// <param name="dataItem">Current bound data item or null.</param>
		public DataSourceConverterEventArgs(Row row, object dataItem)
		{
			_row = row;
			_dataItem = dataItem;
		}

		/// <summary>Current xls <see cref="Row"/>.</summary>
		public Row Row
		{
			get { return _row; }
		}

		/// <summary>Current bound data item or null if not assigned.</summary>
		public object DataItem
		{
			get { return _dataItem; }
		}
	}
}