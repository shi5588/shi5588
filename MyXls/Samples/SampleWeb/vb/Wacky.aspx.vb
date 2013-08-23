Imports org.in2bits.MyXls

Partial Class vb_Wacky
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim xls As New XlsDocument
        xls.FileName = "Wacky.xls"

        'Add some metadata (visible from Excel under File -> Properties)
        xls.SummaryInformation.Author = "Tim Erickson" 'let them know who's responsible!
        xls.SummaryInformation.Subject = "A wacky display of Excel file generation"
        xls.DocumentSummaryInformation.Company = "in2bits.org"

        Dim sheetNumber As Integer
        For sheetNumber = 1 To 5
            Dim sheetName As String = "Sheet " & sheetNumber
            Dim rowMin As Integer = sheetNumber
            Dim rowCount As Integer = sheetNumber + 10
            Dim colMin As Integer = sheetNumber
            Dim colCount As Integer = sheetNumber + 10
            Dim sheet As Worksheet = xls.Workbook.Worksheets.AddNamed(sheetName)
            Dim cells As Cells = sheet.Cells
            Dim r As Integer
            For r = 0 To rowCount
                If (r = 0) Then
                    Dim c As Integer
                    For c = 0 To colCount
                        cells.Add(rowMin + r, colMin + c, "Fld" & (c + 1)).Font.Bold = True
                    Next
                Else
                    Dim c As Integer
                    For c = 0 To colCount
                        Dim val As Integer = r + c
                        Dim cell As Cell = cells.Add(rowMin + r, colMin + c, val)
                        If Not ((val Mod 2) = 0) Then
                            cell.Font.FontName = "Times New Roman"
                            cell.Font.Underline = UnderlineTypes.Double
                            cell.Rotation = 45
                        End If
                    Next
                End If
            Next
        Next

        xls.Send()
    End Sub
End Class
