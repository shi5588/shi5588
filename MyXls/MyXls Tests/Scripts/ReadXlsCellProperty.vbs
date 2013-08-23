Dim file
Dim xls, wbk, sht, rng

If WScript.Arguments.Length <> 1 Then
	WScript.Quit 1
End If

file = WScript.Arguments(0)

Set xls = CreateObject("Excel.Application")
xls.Visible = False

Set wbk = xls.Workbooks.Open(file, , True)

Set sht = wbk.Sheets(>>SheetIndex<<)

Set rng = sht.Cells(>>RowIndex<<, >>ColumnIndex<<)
WScript.StdOut.Write rng.>>CellProperty<<

Set rng = Nothing

Set sht = Nothing

wbk.Close
Set wbk = Nothing

xls.Quit
Set xls = Nothing
