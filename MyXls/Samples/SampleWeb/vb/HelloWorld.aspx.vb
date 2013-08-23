Imports org.in2bits.MyXls

Partial Class vb_HelloWorld
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim xls As New XlsDocument
        xls.FileName = "MyXLS Hello World.xls"
        Dim sheet As Worksheet = xls.Workbook.Worksheets.AddNamed("Hello, World")
        sheet.Cells.Add(1, 1, "Hello, World!")
        xls.Send()
    End Sub
End Class
