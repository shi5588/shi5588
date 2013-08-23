using System.Collections;
using System.Data;

namespace org.in2bits.MyXls.Data
{

	/// <summary>Adapter to enumerate and extract data from an IDataReader data source.</summary>
	/// <typeparam name="TItem">The type of the data item.</typeparam>
	public class DataReaderDataSourceAdapter<TItem> : DataSourceAdapter<TItem>
	{
		/// <summary>Initializes a new instance of the <see cref="DataReaderDataSourceAdapter&lt;TItem&gt;"/> class.</summary>
		/// <param name="dataSource">The data source to convert.</param>
		public DataReaderDataSourceAdapter(IDataReader dataSource) : base(dataSource) {}

		/// <summary>
		/// Returns the value of a data item, give the adapter field.
		/// </summary>
		/// <param name="dataItem">data item</param>
		/// <param name="field">adapter field interface</param>
		/// <returns>Value of the data item</returns>
		public override object GetValue(TItem dataItem, IAdapterBoundField field)
		{
			IDataReader reader = dataItem as IDataReader;
			return reader.IsDBNull(reader.GetOrdinal(field.DataField)) ? null : reader[field.DataField];
		}

		/// <summary>
		/// Returns an enumerator for the data source.
		/// </summary>
		/// <returns>Instance implementing IEnumerable</returns>
		public override IEnumerable GetEnumerator()
		{
			return new DataReaderEnumerable((IDataReader) _dataSource);
		}

		class DataReaderEnumerable : IEnumerable
		{
			private readonly IDataReader _datareader;

			public DataReaderEnumerable(IDataReader datareader)
			{
				_datareader = datareader;
			}
			
			public IEnumerator GetEnumerator()
			{
				return new DataReaderEnumerator(_datareader);
			}
		}

		class DataReaderEnumerator : IEnumerator
		{
			private IDataReader _reader;
			
			public DataReaderEnumerator(IDataReader reader)
			{
				_reader = reader;
			}

			public bool MoveNext()
			{
				return _reader.Read();
			}

			public void Reset()
			{
				throw new System.NotImplementedException();
			}

			public object Current
			{
				get { return _reader; }
			}
		}
	}
}
