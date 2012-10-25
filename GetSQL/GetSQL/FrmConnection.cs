using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GetSQL
{
    public partial class FrmConnection : Form
    {
        private SqlConnectionStringBuilder _ConnStrBuild;

        public FrmConnection()
        {
            InitializeComponent();
        }

        private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            this.txtResult.Text = this._ConnStrBuild.ToString();

        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {
            this._ConnStrBuild = new SqlConnectionStringBuilder();
            this.propertyGrid.SelectedObject = this._ConnStrBuild;
        }
    }
}
