using System;
using System.Collections.Generic;

namespace org.in2bits.MyXls.Data
{
	/// <summary>Convert a datasource to a Worksheet</summary>
	public class DataSourceConverter<TItem>
	{
		/// <summary>Converter Fields</summary>
		protected readonly List<AdapterBoundField<TItem>> _fields;
		/// <summary>Name.  Used for the worksheet name if set.</summary>
		protected string _name;
		/// <summary>Datasource.</summary>
		protected object _dataSource;
		/// <summary>Adapter used for conversion.</summary>
		protected DataSourceAdapter<TItem> _adapter;
		
		/// <summary>
		/// Constructor to create a DataSource adapter.
		/// </summary>
		public DataSourceConverter()
		{
			_fields = new List<AdapterBoundField<TItem>>();
			ShowHeaderRow = true;
		}

		/// <summary>Current <see cref="Worksheet"/></summary>
		public Worksheet CurrentWorksheet { get; set; }

		/// <summary>If true a header row is created in the worksheet for the data rows.</summary>
		public bool ShowHeaderRow { get; set; }

		/// <summary>
		/// Definied fields (columns) to include in the worksheet.  
		/// Only fields that are defined are generated from the DataTable to the worksheet.
		/// </summary>
		public List<AdapterBoundField<TItem>> Fields
		{
			get { return _fields; }
		}

		/// <summary>Name of the data source.</summary>
		public virtual string Name
		{
			get { return _name; }
		}

		/// <summary>Event called when a data item is being bound to an Xls row</summary>
		public event EventHandler<DataSourceConverterEventArgs> RowDataBound;

		/// <summary>Evemt called when creating the header row.</summary>
		public event EventHandler<DataSourceConverterEventArgs> HeaderDataBound;

		/// <summary>
		/// Create a worksheet based on a DataTable
		/// </summary>
		/// <param name="document">Document to add the worksheet to and populate.</param>
		/// <param name="dataSource">Data to populate the worksheet from.</param>
		/// <param name="name">Data Source Name</param>
		/// <returns>New worksheet.</returns>
		public Worksheet CreateWorksheet(XlsDocument document, object dataSource, string name)
		{
			return PopulateWorksheet(document, null, dataSource, name);
		}

		/// <summary>
		/// Populate a worksheet based on a DataTable.
		/// </summary>
		/// <param name="document">XlsDocument.  Required if the passed worksheet is null.</param>
		/// <param name="worksheet">Worksheet to populated.  Will be created if null.</param>
		/// <param name="dataSource">Data to populate the worksheet from.</param>
		/// <param name="name">Data Source Name</param>
		/// <returns>Worksheet populated with DataTable data.</returns>
		public Worksheet PopulateWorksheet(XlsDocument document, Worksheet worksheet, object dataSource, string name)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			_dataSource = dataSource;

			if (worksheet == null && document == null)
			{
				throw new ArgumentNullException("document");
			}
			_name = String.IsNullOrEmpty(name) ? "Worksheet1" : name;
		
			CurrentWorksheet = worksheet ?? document.Workbook.Worksheets.Add(Name);
			if (ShowHeaderRow)
			{
				CreateHeaders();
			}
			CreateDataRows();
			return CurrentWorksheet;
		}

		/// <summary>
		/// Creates the header row and fires the OnHeaderRow event.
		/// </summary>
		protected virtual void CreateHeaders()
		{
			CheckCurrentWorksheetAssigned();
			Row headerRow = CurrentWorksheet.Rows.AddRow((ushort)(CurrentWorksheet.Rows.Count + 1));

			ushort column = 0;
			foreach (var field in _fields)
			{
				CurrentWorksheet.Cells.Add(headerRow.RowIndex, ++column, field.HeaderText);
			}
			OnHeaderRow(headerRow);
		}

		private void OnHeaderRow(Row row)
		{
			if (HeaderDataBound != null)
			{
				DataSourceConverterEventArgs args = new DataSourceConverterEventArgs(row);
				HeaderDataBound(this, args);
			}
		}

		private void CheckCurrentWorksheetAssigned()
		{
			if (CurrentWorksheet == null)
			{
				throw new ArgumentException("CurrentWorksheet has not be assigned.", "CurrentWorksheet");
			}
		}

		/// <summary>
		/// Creates the rows from the data.
		/// </summary>
		protected virtual void CreateDataRows()
		{
			_adapter = DataSourceAdapter<TItem>.CreateAdapter(_dataSource);
			foreach (TItem item in _adapter.GetEnumerator())
			{
				ushort column = 0;
				Row row = CurrentWorksheet.Rows.AddRow((ushort)(CurrentWorksheet.Rows.Count + 1));
				foreach (var field in _fields)
				{
					Cell cell = CurrentWorksheet.Cells.Add(row.RowIndex, ++column, GetRowValue(item, field));
					field.OnBoundField(this, cell, row, item);
				}
				OnRowDataBound(row, item);
			}
		}

		private void OnRowDataBound(Row row, TItem dataItem)
		{
			if (RowDataBound != null)
			{
				DataSourceConverterEventArgs args = new DataSourceConverterEventArgs(row, dataItem);
				RowDataBound(this, args);
			}
		}

		private object GetRowValue(TItem item, IAdapterBoundField boundField)
		{
			object result = _adapter.GetValue(item, boundField);
			if (result == null)
			{
				if (boundField.ConvertNullToEmptyString)
				{
					result = String.Empty;
				}
			}
			else 
			{
				if (!String.IsNullOrEmpty(boundField.DataFormatString))
				{
					result = String.Format(boundField.DataFormatString, result);
				}
			}
			return result;
		}

		/// <summary>
		/// Event implementation to bold all the columns in a row. 
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="args">Event arguments.</param>
		/// <example>
		/// In the following example this event is used to bold all the columns in the header row.
		/// <code>
		/// adapter.HeaderDataBound += new EventHandler&gt;DataSourceConverterEventArgs&lt;(DataTableAdapter.BoldAllCells);
		/// </code>
		/// </example>
		public static void BoldAllCells(object sender, DataSourceConverterEventArgs args)
		{
			for (ushort i = args.Row.MinCellCol; i <= args.Row.MaxCellCol; i++)
			{
				args.Row.CellAtCol(i).Font.Bold = true;
			}
		}
	}
}
