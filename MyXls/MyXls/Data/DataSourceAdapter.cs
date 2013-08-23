using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// Abstract base class for DataSource adapters.  The descendents of this class are responsible for adapting different
	/// untyped datasource objects into a common form usable by the <see cref="DataSourceConverter{TItem}"/>
	/// </summary>
	/// <typeparam name="TItem">The type of the individual data items in the data source.</typeparam>
	public abstract class DataSourceAdapter<TItem>
	{
		/// <summary>DataSource</summary>
		protected object _dataSource;

		/// <summary>
		/// Creates a new DataSourceAdapter
		/// </summary>
		/// <param name="dataSource">Source of the data.</param>
		protected DataSourceAdapter(object dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			_dataSource = dataSource;
		}

		/// <summary>
		/// Gets the value of the data item specific to each datasource.
		/// </summary>
		/// <param name="dataItem">The data item.</param>
		/// <param name="field">The field bound to this data item.</param>
		/// <returns>Value or null</returns>
        public virtual object GetValue(TItem dataItem, IAdapterBoundField field)
        {
        	return dataItem;
        }

		/// <summary>
		/// Gets the enumerator of the datasource.
		/// </summary>
		/// <returns>IEnumearble instance.</returns>
		public abstract IEnumerable GetEnumerator();

		/// <summary>
		/// Factory method to create the appropriate adapter based on a data source.
		/// </summary>
		/// <param name="dataSource">The data source to create an adapter for.</param>
		/// <returns>Specific data source adapter instance.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the dataSource paramter is null.</exception>
		/// <exception cref="NotSupportedException">Thrown if the dataSource is not of a supported type.</exception>"
        internal static DataSourceAdapter<TItem> CreateAdapter(object dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentException("dataSource cannot be null", "dataSource");
			}

			DataSourceAdapter<TItem> adapter;
			Type dataSourceType = dataSource.GetType();
			if (dataSourceType == typeof(DataTable))
			{
				adapter = new DataTableDataSourceAdapter<TItem>((DataTable)dataSource);
			}
			else if (typeof(IDataReader).IsAssignableFrom(dataSourceType))
			{
				adapter = new DataReaderDataSourceAdapter<TItem>((IDataReader)dataSource);
			}
			else if (typeof(IEnumerable).IsAssignableFrom(dataSourceType))
			{
				adapter = new EnumerableDataSourceAdapter<TItem>(dataSource);
			}
			else if (dataSourceType == typeof(ObjectDataSource))
			{
				adapter = new ObjectDataSourceDataSourceAdapter<TItem>((ObjectDataSource)dataSource);
			}
			else
			{
				throw new NotSupportedException(String.Format("{0} type not supported", dataSourceType.FullName));
			}
			return adapter;	
		}
	}
}
