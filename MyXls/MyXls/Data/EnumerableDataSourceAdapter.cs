using System;
using System.Collections;
using System.Web.UI;

namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// Adapter to enumerate and extract data from an IEnumerable data source.
	/// </summary>
	/// <typeparam name="TItem">The type of the item in the enumerated list.</typeparam>
	public class EnumerableDataSourceAdapter<TItem> : DataSourceAdapter<TItem>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnumerableDataSourceAdapter&lt;TItem&gt;"/> class.
		/// </summary>
		/// <param name="dataSource">The data source to enumerate.</param>
		public EnumerableDataSourceAdapter(object dataSource) : base(dataSource) {}


		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>IEnumerable</returns>
		public override IEnumerable GetEnumerator()
		{
			return (IEnumerable)_dataSource;
		}

		/// <summary>
		/// Gets the value of the data item.
		/// </summary>
		/// <param name="dataItem">The data item.</param>
		/// <param name="field">Adapter field associated with this data item.</param>
		/// <returns>Value or null</returns>
		public override object GetValue(TItem dataItem, IAdapterBoundField field)
		{
			return String.IsNullOrEmpty(field.DataField) ? null : DataBinder.Eval(dataItem, field.DataField);
		}
	}
}
