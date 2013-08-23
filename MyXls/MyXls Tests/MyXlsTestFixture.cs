using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using org.in2bits.MyXls.Tests;

namespace org.in2bits.MyXls
{
    public class MyXlsTestFixture
    {
        public delegate void XlsDocumentDelegate(XlsDocument doc);

        public static void AssertPropertyViaExcelOle(string fileName, CellProperties property, string expected, string message)
        {
            AssertPropertyViaExcelOle(1, 1, 1, fileName, property, expected, message);
        }

        public static void AssertPropertyViaExcelOle(int sheetIndex, int rowIndex, int columnIndex, string fileName, CellProperties property, string expected, string message)
        {
            string actual = GetCellPropertyViaExcelOle(sheetIndex, rowIndex, columnIndex, fileName, property);
            Assert.AreEqual(expected, actual, message);
        }

        public static string GetCellPropertyViaExcelOle(string fileName, CellProperties property)
        {
            return GetCellPropertyViaExcelOle(1, 1, 1, fileName, property);
        }

        public static string GetCellPropertyViaExcelOle(int sheetIndex, int rowIndex, int columnIndex, string fileName, CellProperties property)
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cscript.exe";
            string sourceScript = Path.Combine(TestsConfig.ScriptsFolder, "ReadXlsCellProperty.vbs");
            string propertyScript = string.Format("ReadXlsCell{0}.vbs", property);
            if (File.Exists(propertyScript))
                File.Delete(propertyScript);
            string script = File.ReadAllText(sourceScript);
            script = script.Replace(">>SheetIndex<<", sheetIndex.ToString());
            script = script.Replace(">>RowIndex<<", rowIndex.ToString());
            script = script.Replace(">>ColumnIndex<<", columnIndex.ToString());
            script = script.Replace(">>CellProperty<<", GetCellPropertyString(property));
            File.WriteAllText(propertyScript, script);
            startInfo.Arguments = string.Format("{0} //B //NoLogo \"{1}\"", propertyScript, fileName);
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            string output = proc.StandardOutput.ReadToEnd();
            if (string.IsNullOrEmpty(output))
                output = proc.StandardError.ReadToEnd();
            File.Delete(propertyScript);
            return output;
        }

        public enum CellProperties
        {
            Value,
            Text,
            Font_Bold,
            Font_Italic,
            Font_Underline,
            Font_Name,
            Rotation,
            Width,
            MergeAreaCount,
            PatternColor,
            PatternBackgroundColor,
            Comment
        }

        public static string GetCellPropertyString(CellProperties property)
        {
            switch (property)
            {
                case CellProperties.Value:
                case CellProperties.Text:
                case CellProperties.Width:
                    return property.ToString();
                case CellProperties.Rotation:
                    return "Orientation";
                case CellProperties.Font_Bold:
                    return "Font.Bold";
                case CellProperties.Font_Italic:
                    return "Font.Italic";
                case CellProperties.Font_Underline:
                    return "Font.Underline";
                case CellProperties.Font_Name:
                    return "Font.Name";
                case CellProperties.MergeAreaCount:
                    return "MergeArea.Count";
                case CellProperties.PatternColor:
                    return "Interior.PatternColorIndex";
                case CellProperties.PatternBackgroundColor:
                    return "Interior.ColorIndex";
                case CellProperties.Comment:
                    return "Comment";
                default:
                    throw new NotSupportedException(property.ToString());
            }
        }

        public abstract class ExcelUnderlineStyles
        {
            public static readonly int Double = -4119;
            public static readonly int DoubleAccounting = 5;
            public static readonly int None = -4142;
            public static readonly int Single = 2;
            public static readonly int SingleAccounting = 4;
        }

        public static string WriteDocument(XlsDocumentDelegate docDelegate)
        {
            string path = Environment.CurrentDirectory;
            if (!path.EndsWith("\\"))
                path += "\\";

            string fileName = "writedocument";

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;
            if (docDelegate != null)
                docDelegate(xls);
            xls.Save(path, true);
            return string.Format("{0}{1}.xls", path, fileName);
        }

        public static string Write1Cell(object value)
        {
            XlsDocumentDelegate docDelegate = delegate(XlsDocument doc)
              {
                  Worksheet sheet = doc.Workbook.Worksheets.Add("Sheet1");
                  sheet.Cells.Add(1, 1, value);
              };
            return WriteDocument(docDelegate);
        }
    }
}
