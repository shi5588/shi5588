using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using org.in2bits.MyXls;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGoMyXls_Click(object sender, EventArgs e)
        {
            int rows = 0;
            if (!int.TryParse(textBoxRows.Text, out rows))
            {
                MessageBox.Show("Rows must be an int value!");
                return;
            }
            int cols = 0;
            if (!int.TryParse(textBoxColumns.Text, out cols))
            {
                MessageBox.Show("Columns must be an int value!");
                return;
            }
            if (cols < 1 || cols > 255)
            {
                MessageBox.Show("Columns must be between 1 and 255!");
                return;
            }
            if (rows < 1 || rows > 65535)
            {
                MessageBox.Show("Rows must be between 1 and 65,535!");
                return;
            }

            XlsDocument doc = new XlsDocument();
            Workbook wbk = doc.Workbook;
            Worksheet sht = wbk.Worksheets.Add("Test Sheet");
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (row == 0)
                    {
                        sht.Cells.Add(row + 1, col + 1, string.Format("Fld{0}", col + 1));
                    }
                    else
                    {
                        sht.Cells.Add(row + 1, col + 1, 1);
                    }
                }
            }
            byte[] bytes = doc.Bytes.ByteArray;
        }

        private void buttonReadFile_Click(object sender, EventArgs e)
        {
            string fileName = textBoxReadFile.Text;
            if (!File.Exists(fileName))
            {
                MessageBox.Show(string.Format("{0} not found!", fileName));
                return;
            }
            XlsDocument xls = new XlsDocument(fileName);
            Bytes stream = xls.OLEDoc.Streams[xls.OLEDoc.Streams.GetIndex(org.in2bits.MyOle2.Directory.Biff8Workbook)].Bytes;
            List<Record> records = Record.GetAll(stream);
            StringBuilder sb = new StringBuilder();
            foreach (Record record in records)
            {
                string name = RID.Name(record.RID);
                sb.Append(name);
                sb.Append(new string(' ', RID.NAME_MAX_LENGTH - name.Length) + ": ");
                byte[] recordData = record.Data.ByteArray;
                for (int i = 0; i < recordData.Length; i++)
                {
                    if (i > 0)
                        sb.Append(" ");
                    sb.Append(string.Format("{0:x2}", recordData[i]));
                }
                sb.Append(Environment.NewLine);
            }
            richTextBoxRecordList.Text = sb.ToString();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxReadFile.Text = ofd.FileName;
                buttonReadFile_Click(null, null);
            }
        }
    }
}