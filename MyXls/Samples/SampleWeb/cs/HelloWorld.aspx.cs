using System;

using org.in2bits.MyXls;

public partial class HelloWorld : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = "MyXLS Hello World.xls";
        Worksheet sheet = xls.Workbook.Worksheets.AddNamed("Hello, World!");
        sheet.Cells.AddValueCell(1, 1, "Hello, World!");
        xls.Send();
    }
}
