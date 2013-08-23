namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// Event argument for an <see cref="AdapterBoundField{T}"/>
	/// </summary>
	public class AdapterFieldEventArgs<T> : DataSourceConverterEventArgs
	{
		private readonly Cell _cell;
		
		/// <summary>Creates a new AdapterFieldEventArgs instance.</summary>
		/// <param name="cell">Current xls <see cref="Cell"/></param>
		/// <param name="row">Current xls <see cref="Row"/>.</param>
		/// <param name="dataItem">Current data item</param>
		public AdapterFieldEventArgs(Cell cell, Row row, T dataItem) : base(row, dataItem)
		{
			_cell = cell;
		}

		/// <summary>Current xls <see cref="Cell"/></summary>
		public Cell Cell
		{
			get { return _cell; }
		}

		/// <summary>Current bound data item or null if not assigned.</summary>
		/// <value></value>
		public new T DataItem
		{
			get { return (T) _dataItem; }
		}
	}
}
