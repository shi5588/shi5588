using System;
using org.in2bits.MyXls;

namespace org.in2bits.MyXls
{
    public partial class DemoMyXLS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            org.in2bits.MyXls.XlsDocument doc = new XlsDocument();
            doc.FileName = "TestingAgain.xls";

            //doc.Workbook.ProtectContents = true;

            for (int s = 1; s <= 5; s++)
            {
                string sheetName = Request.Form["txtSheet" + s].Replace(",", string.Empty);
                
                if (sheetName.Trim() == string.Empty)
                    continue;

                int rowMin, rowCount, colMin, colCount;

                try
                {
                    rowMin = int.Parse(Request.Form["txtRowMin" + s]);
                    rowCount = int.Parse(Request.Form["txtRows" + s]);
                    colMin = int.Parse(Request.Form["txtColMin" + s]);
                    colCount = int.Parse(Request.Form["txtCols" + s]);
                }
                catch
                {
                    continue;
                }

                if (rowCount > 65535) rowCount = 65535;
                if (rowCount < 0) rowCount = 0;
                if (rowMin < 1) rowMin = 1;
                if (rowMin > 32767) rowMin = 32767;

                if (colCount > 255) colCount = 255;
                if (colCount < 1) colCount = 1;
                if (colMin < 1) colMin = 1;
                if (colMin > 100) colMin = 100;

                if (sheetName.Length > 35) sheetName = sheetName.Substring(0, 35);

                Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
                Cells cells = sheet.Cells;

                for (int row = 0; row <= rowCount; row++)
                {
                    if (row == 0)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            Cell cell = cells.Add(rowMin + row, colMin + col - 1, "Fld" + col);

                            cell.TopLineStyle = 2;
                            cell.TopLineColor = Colors.Black;
                            cell.BottomLineStyle = 2;
                            cell.BottomLineColor = Colors.Black;
                            if (col == 1)
                            {
                                cell.LeftLineStyle = 2;
                                cell.LeftLineColor = Colors.Black;
                            }
                            cell.RightLineStyle = 2;
                            cell.RightLineColor = Colors.Black;

                            cell.Font.Weight = FontWeight.Bold;
                            cell.Pattern = FillPattern.Solid;
                            cell.PatternColor = Colors.Silver;
                        }
                    }
                    else
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            Cell cell = cells.Add(rowMin + row, colMin + col - 1, /*row + col*/1.001);
                        }
                    }
                }
            }

            doc.Send();
            Response.Flush();
            Response.End();
        }
    }
}
