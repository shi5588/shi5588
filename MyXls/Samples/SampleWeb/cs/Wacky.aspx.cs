using System;

using org.in2bits.MyXls;

public partial class Wacky : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();
        xls.FileName = "Wacky.xls";
        
        //Add some metadata (visible from Excel under File -> Properties)
        xls.SummaryInformation.Author = "Tim Erickson"; //let them know who's responsible!
        xls.SummaryInformation.Subject = "A wacky display of Excel file generation";
        xls.DocumentSummaryInformation.Company = "in2bits.org";
        
        for (int sheetNumber = 1; sheetNumber <= 5; sheetNumber++)
        {
            string sheetName = "Sheet " + sheetNumber;
            int rowMin = sheetNumber;
            int rowCount = sheetNumber + 10;
            int colMin = sheetNumber;
            int colCount = sheetNumber + 10;
            Worksheet sheet = xls.Workbook.Worksheets.AddNamed(sheetName);
            Cells cells = sheet.Cells;
            for (int r = 0; r < rowCount; r++)
            {
                if (r == 0)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        cells.Add(rowMin + r, colMin + c, "Fld" + (c + 1)).Font.Bold = true;
                    }
                }
                else
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        int val = r + c;
                        Cell cell = cells.Add(rowMin + r, colMin + c, val);
                        if (val % 2 != 0)
                        {
                            cell.Font.FontName = "Times New Roman";
                            cell.Font.Underline = UnderlineTypes.Double;
                            cell.Rotation = 45;
                        }
                    }
                }
            }
        }

        xls.Send();
    }
}
