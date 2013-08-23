Option Explicit
Dim bits
bits = NUMBERVal(1.001)
Dim bitIdx, byteIdx, byteBit, bitString, revBitString
bitString = ""
revBitString = ""
bitIdx = 0
byteIdx = 0
byteBit = 0
For bitIdx = 0 To UBound(bits)
	byteIdx = Int(bitIdx / 8)
	byteBit = bitIdx Mod 8
	If byteIdx > 0 And byteBit = 0 Then
		bitString = bitString & " "
		revBitString = revBitString & " "
	End If
	bitString = bitString & bits(bitIdx)
	revBitString = revBitString & bits((8 * byteIdx) + (7 - byteBit))
Next
WScript.Echo "0-7: " & bitString
WScript.Echo "7-0: " & revBitString

		Private Function NUMBERVal(ByVal val)
			Dim i, j, bits(63), digs, pint, pdec, ptmp, issig, dig, pwr

			For i = 0 To 63
				bits(i) = 0
			Next 'i

'			If div100 = True Then
'				bits(0) = 1
'			End If
'			bits(1) = 0

			If val < 0 Then
				bits(63) = 1
				val = -1 * val
			End If

			digs = 0
			pwr = 0

			pint = Int(val)
			pdec = val - pint

			Do While pint > 0 And pwr < 1024
				ptmp = pint / 2
				If Int(ptmp) < ptmp Then
					dig = 1
				Else
					dig = 0
				End If
				If digs < 53 Then
					bits(digs) = dig
					digs = digs + 1
				End If
				If digs > 1 Then
					pwr = pwr + 1
				End If
				pint = Int(ptmp)
			Loop

			j = 53 - digs
			For i = 1 To digs
				If i > 1 Then
					bits(digs + j - i) = bits(digs - i)
			End If
				bits(digs - i) = 0
			Next 'i

			If digs > 0 Then
				issig = True
			Else
				issig = False
			End If

			Do While pdec > 0 And digs < 53 And pwr > -1023
				If Not issig Then
					pwr = pwr - 1
				End If
				ptmp = pdec * 2
				If ptmp >= 1 Then
					issig = True
					dig = 1
				Else
					dig = 0
				End If
				If issig Then
					If digs > 0 Then
						bits(52 - digs) = dig
					End If
					digs = digs + 1
				End If
				pdec = ptmp - Int(ptmp)
			Loop

			If val <> 0 Then
				pwr = pwr + 1023 'Add bias
			End If

			digs = 0
			Do While pwr > 0 And digs < 11
				ptmp = pwr / 2
				If Int(ptmp) < ptmp Then
					dig = 1
				Else
					dig = 0
				End If
				digs = digs + 1
				bits(51 + digs) = dig
				pwr = Int(ptmp)
			Loop

			NUMBERVal = bits

'		    DisplayBits bits
		End Function

		Private Function Bits2HexStrLE(bits)
			If p_biff8.Debug Then Response.Write(Now() & " - Cell.Bits2HexStrLE<br />")
			Dim i, j, k, str

			j = Int(UBound(bits) / 8) + 1

			For i = 1 To j
				k = (8 * (i - 1)) + 3
				str = str & Hex(bits(k + 1) + 2 * bits(k + 2) + 4 * bits(k + 3) + 8 * bits(k + 4))
				k = 8 * (i - 1) - 1
				str = str & Hex(bits(k + 1) + 2 * bits(k + 2) + 4 * bits(k + 3) + 8 * bits(k + 4))
			Next 'i

			Bits2HexStrLE = str
		End Function
