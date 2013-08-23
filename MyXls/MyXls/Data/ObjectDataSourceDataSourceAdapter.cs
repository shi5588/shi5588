using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace org.in2bits.MyXls.Data
{
	/// <summary>Adapter to enumerate an object data source </summary>
	/// <typeparam name="TItem">Type of the item enumerated.</typeparam>
	public class ObjectDataSourceDataSourceAdapter<TItem> : EnumerableDataSourceAdapter<TItem>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectDataSourceDataSourceAdapter{TItem}"/> class.
		/// </summary>
		/// <param name="dataSource">data source.</param>
		public ObjectDataSourceDataSourceAdapter(ObjectDataSource dataSource) : base(dataSource) {}


		/// <summary>
		/// Gets the enumerator of the data source.
		/// </summary>
		/// <returns>IEnumerable source.</returns>
		public override IEnumerable GetEnumerator()
		{
			return ((ObjectDataSource)_dataSource).Select();
		}

		/// <summary>Gets the value of the data item from the data source. </summary>
		/// <param name="dataItem">The data item.</param>
		/// <param name="field">Adapter field bound to this item.</param>
		/// <returns>Value or null.</returns>
		public override object GetValue(TItem dataItem, IAdapterBoundField field)
		{
			return String.IsNullOrEmpty(field.DataField) ? null : DataBinder.Eval(dataItem, field.DataField);
		}
	}
}
