namespace org.in2bits.MyXls.Data
{
	/// <summary>
	/// AdapterField interface used by the <see cref="DataSourceConverter{TItem}"/>.
	/// </summary>
	public interface IAdapterBoundField
	{
		/// <summary>If true nulls will be converted to an empty string in the output. </summary>
		bool ConvertNullToEmptyString { get; set; }

		/// <summary>Data field name or null if this field is not bound.</summary>
		string DataField { get; }

		/// <summary>Format string for data binding.</summary>
		string DataFormatString { get; set; }
	}
}
