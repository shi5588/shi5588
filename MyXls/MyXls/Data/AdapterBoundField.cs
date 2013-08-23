using System;
using System.Data;
using System.Web.UI;

namespace org.in2bits.MyXls.Data
{
	/// <summary>Field defined in a <see cref="DataSourceAdapter{TItem}"/> to generate a column in the output xls document.  
	/// This field is bound to a column in the source <see cref="DataSet"/></summary>
	public class AdapterBoundField<T> :  IAdapterBoundField
	{
		private string _dataField;
		private string _headerText;
		private string _dataFormatString;

		/// <summary>Creates the AdapterBoundField.</summary>
		public AdapterBoundField() : this(null) {}

		/// <summary>Creates the AdapterBoundField.</summary>
		/// <param name="dataField">Required datafield, which is the column name in the source <see cref="DataSet"/></param>
		public AdapterBoundField(string dataField) : this(dataField, null) {}

		/// <summary>Creates the AdapterBoundField. </summary>
		/// <param name="dataField">Required datafield, which is the column name in the source <see cref="DataSet"/></param>
		/// <param name="headerText">Optional header text, which is used as the text display of the header column.</param>
		public AdapterBoundField(string dataField, string headerText) : this(dataField, headerText, null) {}

		/// <summary>Creates the AdapterBoundField. </summary>
		/// <param name="dataField">Required datafield, which is the column name in the source <see cref="DataSet"/></param>
		/// <param name="headerText">Optional header text, which is used as the text display of the header column.</param>
		/// <param name="dataFormat">Option data format string used to format the data output.</param>
		public AdapterBoundField(string dataField, string headerText, string dataFormat)
		{
			_dataField = dataField;
			_headerText = headerText;
			_dataFormatString = dataFormat;
		}

		/// <summary>Callback on cell data binding</summary>
		public CellDataBoundEventHandler<T> CellDataBound { get; set; }

		/// <summary>True if nulls should be converted to <see cref="string.Empty"/> </summary>
		public bool ConvertNullToEmptyString { get; set; }

		/// <summary>Column name in the source <see cref="DataSet"/> that the AdapterField is bound to.</summary>
		public string DataField
		{
			get { return _dataField; }
		}

		/// <summary>Format string used when data binding.  See <see cref="DataBinder.Eval(object,string,string)"/></summary>
		public string DataFormatString { get { return _dataFormatString; } set { _dataFormatString = value; } }

		/// <summary>Optional text used for the column header.  If null or empty the DataField will be used.</summary>
		public string HeaderText
		{
			get { return String.IsNullOrEmpty(_headerText) ? DataField : _headerText; }
			set { _headerText = value; }
		}

		/// <summary>Calls the CellDataBound callback.</summary>
		/// <param name="sender">Event orginiator</param>
		/// <param name="cell">Cell being populated</param>
		/// <param name="row">Row being populated</param>
		/// <param name="dataItem">The data item.</param>
		public void OnBoundField(object sender, Cell cell, Row row, T dataItem)
		{
			if (CellDataBound != null)
			{
				AdapterFieldEventArgs<T> args = new AdapterFieldEventArgs<T>(cell, row, dataItem);
				CellDataBound(sender, args);
			}
		}
	}


	/// <summary>Delegate for cell databound events.</summary>
	public delegate void CellDataBoundEventHandler<T>(object sender, AdapterFieldEventArgs<T> args);
}