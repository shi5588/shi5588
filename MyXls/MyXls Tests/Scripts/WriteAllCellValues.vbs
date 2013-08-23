Dim file
Dim xls, wbk, sht, rng

If WScript.Arguments.Length <> 1 Then
	WScript.Quit 1
End If

file = WScript.Arguments(0)

Set xls = CreateObject("Excel.Application")
xls.Visible = False

Set wbk = xls.Workbooks.Open(file, , True)

Dim sheetIndex
sheetIndex = 1

For sheetIndex = 1 To wbk.Sheets.Count
	Set sht = wbk.Sheets(sheetIndex)

	Dim rowIndex, columnIndex, columnCount
	rowIndex = 1
	columnIndex = 1
	columnCount = 0

	Set rng = sht.Cells(rowIndex, columnIndex)
	Do While Not(IsEmpty(rng.Value))
		columnCount = columnCount + 1
		columnIndex = columnCount + 1
		Set rng = sht.Cells(rowIndex, columnIndex)
		WScript.StdOut.WriteLine sheetIndex & vbTab & rowIndex & vbTab & columnIndex & vbTab & rng.Value
	Loop

	Dim allColumnsEmpty
	allColumnsEmpty = False
	Dim row
	Do While Not(allColumnsEmpty)
		row = ""
		columnIndex = 1
		allColumnsEmpty = True
		For columnIndex = 1 To columnCount
			Set rng = sht.Cells(rowIndex, columnIndex)
			If Not(IsEmpty(rng.Value)) Then allColumnsEmpty = False
				Dim theValue
				theValue = rng.Value
				If TypeName(theValue) = "String" Then
					theValue = Replace(theValue, vbCrLf, " ")
					'theValue = Replace(theValue, vbCr, " ")
					'theValue = Replace(theValue, vbLf, " ")
				End If
			row = row & sheetIndex & vbTab & rowIndex & vbTab & columnIndex & vbTab & theValue & vbCrLf
		Next
		If Not(allColumnsEmpty) Then WScript.StdOut.Write row
		rowIndex = rowIndex + 1
	Loop
Next


Set rng = Nothing

Set sht = Nothing

wbk.Close
Set wbk = Nothing

xls.Quit
Set xls = Nothing
