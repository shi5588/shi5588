namespace org.in2bits.MyXls
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGoMyXls = new System.Windows.Forms.Button();
            this.buttonGoExcel = new System.Windows.Forms.Button();
            this.textBoxRows = new System.Windows.Forms.TextBox();
            this.labelRows = new System.Windows.Forms.Label();
            this.labelColumns = new System.Windows.Forms.Label();
            this.textBoxColumns = new System.Windows.Forms.TextBox();
            this.radioButtonSimple = new System.Windows.Forms.RadioButton();
            this.radioButtonComlex = new System.Windows.Forms.RadioButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageWriteFile = new System.Windows.Forms.TabPage();
            this.buttonGoCompare = new System.Windows.Forms.Button();
            this.tabPageReadFile = new System.Windows.Forms.TabPage();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxReadFile = new System.Windows.Forms.TextBox();
            this.richTextBoxRecordList = new System.Windows.Forms.RichTextBox();
            this.buttonReadFile = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageWriteFile.SuspendLayout();
            this.tabPageReadFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGoMyXls
            // 
            this.buttonGoMyXls.Location = new System.Drawing.Point(6, 197);
            this.buttonGoMyXls.Name = "buttonGoMyXls";
            this.buttonGoMyXls.Size = new System.Drawing.Size(114, 23);
            this.buttonGoMyXls.TabIndex = 0;
            this.buttonGoMyXls.Text = "GO - MyXls!";
            this.buttonGoMyXls.UseVisualStyleBackColor = true;
            this.buttonGoMyXls.Click += new System.EventHandler(this.buttonGoMyXls_Click);
            // 
            // buttonGoExcel
            // 
            this.buttonGoExcel.Enabled = false;
            this.buttonGoExcel.Location = new System.Drawing.Point(126, 197);
            this.buttonGoExcel.Name = "buttonGoExcel";
            this.buttonGoExcel.Size = new System.Drawing.Size(98, 23);
            this.buttonGoExcel.TabIndex = 1;
            this.buttonGoExcel.Text = "GO - Excel!";
            this.buttonGoExcel.UseVisualStyleBackColor = true;
            // 
            // textBoxRows
            // 
            this.textBoxRows.Location = new System.Drawing.Point(6, 29);
            this.textBoxRows.Name = "textBoxRows";
            this.textBoxRows.Size = new System.Drawing.Size(100, 22);
            this.textBoxRows.TabIndex = 2;
            this.textBoxRows.Text = "100";
            // 
            // labelRows
            // 
            this.labelRows.AutoSize = true;
            this.labelRows.Location = new System.Drawing.Point(6, 9);
            this.labelRows.Name = "labelRows";
            this.labelRows.Size = new System.Drawing.Size(42, 17);
            this.labelRows.TabIndex = 3;
            this.labelRows.Text = "Rows";
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(6, 66);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(62, 17);
            this.labelColumns.TabIndex = 4;
            this.labelColumns.Text = "Columns";
            // 
            // textBoxColumns
            // 
            this.textBoxColumns.Location = new System.Drawing.Point(6, 86);
            this.textBoxColumns.Name = "textBoxColumns";
            this.textBoxColumns.Size = new System.Drawing.Size(100, 22);
            this.textBoxColumns.TabIndex = 5;
            this.textBoxColumns.Text = "10";
            // 
            // radioButtonSimple
            // 
            this.radioButtonSimple.AutoSize = true;
            this.radioButtonSimple.Checked = true;
            this.radioButtonSimple.Location = new System.Drawing.Point(6, 126);
            this.radioButtonSimple.Name = "radioButtonSimple";
            this.radioButtonSimple.Size = new System.Drawing.Size(71, 21);
            this.radioButtonSimple.TabIndex = 6;
            this.radioButtonSimple.TabStop = true;
            this.radioButtonSimple.Text = "Simple";
            this.radioButtonSimple.UseVisualStyleBackColor = true;
            // 
            // radioButtonComlex
            // 
            this.radioButtonComlex.AutoSize = true;
            this.radioButtonComlex.Location = new System.Drawing.Point(6, 153);
            this.radioButtonComlex.Name = "radioButtonComlex";
            this.radioButtonComlex.Size = new System.Drawing.Size(82, 21);
            this.radioButtonComlex.TabIndex = 7;
            this.radioButtonComlex.Text = "Complex";
            this.radioButtonComlex.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageWriteFile);
            this.tabControl.Controls.Add(this.tabPageReadFile);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(643, 862);
            this.tabControl.TabIndex = 9;
            // 
            // tabPageWriteFile
            // 
            this.tabPageWriteFile.Controls.Add(this.buttonGoCompare);
            this.tabPageWriteFile.Controls.Add(this.textBoxRows);
            this.tabPageWriteFile.Controls.Add(this.radioButtonComlex);
            this.tabPageWriteFile.Controls.Add(this.buttonGoMyXls);
            this.tabPageWriteFile.Controls.Add(this.radioButtonSimple);
            this.tabPageWriteFile.Controls.Add(this.buttonGoExcel);
            this.tabPageWriteFile.Controls.Add(this.textBoxColumns);
            this.tabPageWriteFile.Controls.Add(this.labelRows);
            this.tabPageWriteFile.Controls.Add(this.labelColumns);
            this.tabPageWriteFile.Location = new System.Drawing.Point(4, 25);
            this.tabPageWriteFile.Name = "tabPageWriteFile";
            this.tabPageWriteFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWriteFile.Size = new System.Drawing.Size(635, 833);
            this.tabPageWriteFile.TabIndex = 0;
            this.tabPageWriteFile.Text = "Write File";
            this.tabPageWriteFile.UseVisualStyleBackColor = true;
            // 
            // buttonGoCompare
            // 
            this.buttonGoCompare.Location = new System.Drawing.Point(6, 226);
            this.buttonGoCompare.Name = "buttonGoCompare";
            this.buttonGoCompare.Size = new System.Drawing.Size(218, 23);
            this.buttonGoCompare.TabIndex = 8;
            this.buttonGoCompare.Text = "GO - Compare!";
            this.buttonGoCompare.UseVisualStyleBackColor = true;
            // 
            // tabPageReadFile
            // 
            this.tabPageReadFile.Controls.Add(this.buttonBrowse);
            this.tabPageReadFile.Controls.Add(this.textBoxReadFile);
            this.tabPageReadFile.Controls.Add(this.richTextBoxRecordList);
            this.tabPageReadFile.Controls.Add(this.buttonReadFile);
            this.tabPageReadFile.Location = new System.Drawing.Point(4, 25);
            this.tabPageReadFile.Name = "tabPageReadFile";
            this.tabPageReadFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReadFile.Size = new System.Drawing.Size(635, 833);
            this.tabPageReadFile.TabIndex = 1;
            this.tabPageReadFile.Text = "Read File";
            this.tabPageReadFile.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(514, 6);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(29, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxReadFile
            // 
            this.textBoxReadFile.Location = new System.Drawing.Point(6, 6);
            this.textBoxReadFile.Name = "textBoxReadFile";
            this.textBoxReadFile.Size = new System.Drawing.Size(502, 22);
            this.textBoxReadFile.TabIndex = 2;
            this.textBoxReadFile.Text = "..\\..\\..\\Docs\\Baseline2003.xls";
            // 
            // richTextBoxRecordList
            // 
            this.richTextBoxRecordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxRecordList.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxRecordList.Location = new System.Drawing.Point(6, 35);
            this.richTextBoxRecordList.Name = "richTextBoxRecordList";
            this.richTextBoxRecordList.ReadOnly = true;
            this.richTextBoxRecordList.Size = new System.Drawing.Size(623, 792);
            this.richTextBoxRecordList.TabIndex = 1;
            this.richTextBoxRecordList.Text = "";
            // 
            // buttonReadFile
            // 
            this.buttonReadFile.Location = new System.Drawing.Point(549, 6);
            this.buttonReadFile.Name = "buttonReadFile";
            this.buttonReadFile.Size = new System.Drawing.Size(80, 23);
            this.buttonReadFile.TabIndex = 0;
            this.buttonReadFile.Text = "Read File";
            this.buttonReadFile.UseVisualStyleBackColor = true;
            this.buttonReadFile.Click += new System.EventHandler(this.buttonReadFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 886);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tabPageWriteFile.ResumeLayout(false);
            this.tabPageWriteFile.PerformLayout();
            this.tabPageReadFile.ResumeLayout(false);
            this.tabPageReadFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGoMyXls;
        private System.Windows.Forms.Button buttonGoExcel;
        private System.Windows.Forms.TextBox textBoxRows;
        private System.Windows.Forms.Label labelRows;
        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.TextBox textBoxColumns;
        private System.Windows.Forms.RadioButton radioButtonSimple;
        private System.Windows.Forms.RadioButton radioButtonComlex;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageWriteFile;
        private System.Windows.Forms.TabPage tabPageReadFile;
        private System.Windows.Forms.Button buttonReadFile;
        private System.Windows.Forms.TextBox textBoxReadFile;
        private System.Windows.Forms.RichTextBox richTextBoxRecordList;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonGoCompare;
    }
}

