Imports org.in2bits.MyXls

Partial Class vb_Simplest
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim xls As New XlsDocument
        xls.Send()
    End Sub
End Class
