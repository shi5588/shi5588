using System;

using org.in2bits.MyXls;

namespace MyXls.Examles.CS
{
    /// <summary>
    /// Create and send a blank Workbook.  This is the simplest possible use of
    /// MyXls.  By default, if you haven't added any Worksheets to the Workbook
    /// when you call Send on the XlsDocument, one is added and named "Sheet1".
    /// </summary>
    public class BlankWorksheet
    {
        public void Run()
        {
            XlsDocument xls = new XlsDocument(); //Create a new XlsDocument
            xls.Send(); //Send it to the client
        }
    }
}
