using System;

using org.in2bits.MyXls;

public partial class Simplest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument(); //Create a new MyXls Document
        
        xls.Send(); //MyXls adds a single empty sheet named Sheet1 when Send 
                    //is called, if you didn't add one yourself.
    }
}
