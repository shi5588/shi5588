using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class WorksheetTests : MyXlsTestFixture
    {
        [Test]
        public void RenameWorksheet()
        {
            XlsDocument doc = new XlsDocument();
            Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
            Worksheet foundSheet = doc.Workbook.Worksheets["Sheet1"];
            Assert.IsNotNull(foundSheet, "Should find 'Sheet1' before renaming");
            sheet.Name = "MySheet";
            foundSheet = doc.Workbook.Worksheets["MySheet"];
            Assert.IsNotNull(foundSheet, "Should find 'MySheet' after renaming");
        }
    }
}
