using System;
using System.Collections.Generic;
using System.Text;

namespace org.in2bits.MyXls
{
	/// <summary>
	/// Fill pattern of a cell
	/// </summary>
	public enum FillPattern : ushort
	{
		/// <summary>No fill.</summary>
		None = 0,

		/// <summary>Solid fill.</summary>
		Solid,

		/// <summary>50% fill.</summary>
		Percent50,

		/// <summary>75% fill.</summary>
		Percent75,

		/// <summary>55% fill.</summary>
		Percent25,

		/// <summary>Horizontal stripe fill.</summary>
		HorizontalStripe,

		/// <summary>Vertical stripe fill.</summary>
		VerticalStripe,

		/// <summary>Reverse diagonal stripe fill.</summary>
		ReverseDiagonalStripe,

		/// <summary>Diagonal stripe fill.</summary>
		DiagonalStripe,

		/// <summary>Diagonal crosshatch fill.</summary>
		DiagonalCrosshatch,

		/// <summary>Thick diagonal crosshatch fill.</summary>
		ThickDiagonalCrosshatch,

		/// <summary>Thin horizontal stripe fill.</summary>
		ThinHorizontalStripe,

		/// <summary>Thin vertical stripe fill.</summary>
		ThinVerticalStripe,

		/// <summary>Thin reverse diagonal stripe fill.</summary>
		ThinReverseDiagonalStripe,

		/// <summary>Thin diagonal stripe fill.</summary>
		ThinDiagonalStripe,

		/// <summary>Thin horizonta crosshatch fill.</summary>
		ThinHorizontalCrosshatch,

		/// <summary>Thin diagonal crosshatch fill.</summary>
		ThinDiagonalCrosshatch,
        
		/// <summary>12.5% fill.</summary>
		Percent12,

		/// <summary>6% fill.</summary>
		Percent6
	}
}
