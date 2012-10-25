using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GetSQL
{
	public partial class FrmMain : Form
	{
		private BindingSource bindsrc;
		private static string currentDbName;

		public static string CurrentDbName
		{
			get { return currentDbName; }
			set { currentDbName = value; }
		}

		public FrmMain()
		{
			InitializeComponent();
		}

		private void FrmMain_Shown(object sender, EventArgs e)
		{
			this.bindsrc = new BindingSource();

			currentDbName = ConfigurationManager.AppSettings["dbname"].Trim();
			this.fillDbList();
			this.rbtnSelect.Checked = true;
		}

		private void cmbDb_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentDbName = this.cmbDb.Items[this.cmbDb.SelectedIndex].ToString();
			this.fillTablesList();
		}

		private void fillDbList()
		{
			string strSQL = @"
SELECT
	dtb.name AS [Database_Name],
	dtb.name AS [Database_DatabaseName2]
FROM master.sys.databases AS dtb
	LEFT OUTER JOIN sys.database_mirroring AS dmi ON dmi.database_id = dtb.database_id
WHERE
	(    CAST(case when dtb.name in ('master','model','msdb','tempdb') then 1 else dtb.is_distributor end AS bit)=0 
	 AND CAST(isnull(dtb.source_database_id, 0) AS bit)=0)
ORDER BY [Database_Name] ASC";
			DataSet ds = SqlDAL.ExecuteQuery(strSQL);
			this.cmbDb.Items.Clear();
			this.cmbDb.SelectedIndex = -1;
			foreach (DataRow r in ds.Tables[0].Rows) 
			{
				this.cmbDb.Items.Add(r[0]);
			}
		}

		private void fillTablesList()
		{ 
			string strSQL = @"
SELECT tbl.name AS [TblName]--,SCHEMA_NAME(tbl.schema_id) AS [Schema], tbl.create_date AS [CreateDate]
FROM sys.tables AS tbl
WHERE
	(CAST(
		case 
		when tbl.is_ms_shipped = 1 then 1
		when ( select major_id from sys.extended_properties  
			   where major_id = tbl.object_id and minor_id = 0 and class = 1 and name = N'microsoft_database_tools_support') 
			is not null then 1 else 0
		end 
	AS bit)=0)
ORDER BY [TblName] ASC";
			DataSet ds = SqlDAL.ExecuteQuery(strSQL);
			this.bindsrc.DataSource = ds.Tables[0];
			this.gridTables.DataSource = this.bindsrc;
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			DataRowView row = this.bindsrc.Current as DataRowView;
			if(row==null)
			{
				MessageBox.Show("当前没有选择任何的表，无法生成。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string tblName = row["TblName"].ToString();
			string strSQL = @"
	SELECT clmns.name AS [ColName]
	FROM sys.tables AS tbl
		INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
	WHERE tbl.name = '" + tblName + @"' AND SCHEMA_NAME(tbl.schema_id)=N'dbo'
	ORDER BY clmns.column_id ASC";
			DataSet ds = SqlDAL.ExecuteQuery(strSQL);
			if (this.rbtnSelect.Checked)
			{
				this.txtResult.Text = this.getSelectSQL(ds.Tables[0], tblName);
			}
			else if (this.rbtnInsert.Checked)
			{
				this.txtResult.Text = this.getInsertSQL(ds.Tables[0], tblName);
			}
			else
			{
				this.txtResult.Text = this.getUpdateSQL(ds.Tables[0], tblName);
			}
		}

		private string getSelectSQL(DataTable tblColumns, string strTable)
		{
			string strResult = "";
			foreach (DataRow r in tblColumns.Rows)
			{
				strResult += this.txtPrefix.Text.Trim() + "." + r[0].ToString() + ", ";
			}
			strResult = "SELECT " + strResult.Remove(strResult.Length - 2) + Environment.NewLine
                + "FROM dbo." + strTable + " " + this.txtPrefix.Text.Trim() + Environment.NewLine
				+ "WHERE 1 = 1 ";
			return strResult;
		}

		private string getInsertSQL(DataTable tblColumns, string strTable)
		{
			string strResult = "INSERT INTO dbo." + strTable + "(";
			foreach (DataRow r in tblColumns.Rows)
			{
				strResult += r[0].ToString() + ", ";
			}
			strResult = strResult.Remove(strResult.Length - 2) + ")" + Environment.NewLine + "VALUES(";
			foreach (DataRow r in tblColumns.Rows)
			{
				strResult += "@" + r[0].ToString() + ", ";
			}

			return strResult.Remove(strResult.Length - 2) + ")";
		}

		private string getUpdateSQL(DataTable tblColumns, string strTable)
		{
			// 1 取得主键
			string strKey = @"
SELECT ind.name, dsp.name as space_name, col.name as column_nName, ind_col.key_ordinal, ind_col.column_id
FROM sys.indexes ind 
LEFT OUTER JOIN sys.stats sta ON sta.object_id = ind.object_id and sta.stats_id = ind.index_id 
LEFT OUTER JOIN 
	(
		sys.index_columns ind_col INNER JOIN sys.columns col 
		ON col.object_id = ind_col.object_id and col.column_id = ind_col.column_id
	)ON ind_col.object_id = ind.object_id and ind_col.index_id = ind.index_id 
	LEFT OUTER JOIN sys.data_spaces dsp ON dsp.data_space_id = ind.data_space_id  
WHERE ind.object_id = object_id(N'" + strTable + @"')  AND ind.index_id >= 0 AND ind.type <> 3 AND ind.is_hypothetical = 0   
ORDER BY ind.index_id, ind_col.key_ordinal";
			DataTable keyTable = SqlDAL.ExecuteQuery(strKey).Tables[0];
			List<string> lstKey = new List<string>();
			
			foreach (DataRow r in keyTable.Rows)
			{
				lstKey.Add(r["column_nName"].ToString());
			}

			string strResult = "UPDATE dbo." + strTable + " SET ";
			foreach (DataRow r in tblColumns.Rows)
			{
				if(!lstKey.Contains(r[0].ToString()))
				{
					strResult += r[0].ToString() + " = @" + r[0].ToString() + ", ";
				}
			}
			strResult = strResult.Remove(strResult.Length - 2) + Environment.NewLine + "WHERE ";
			foreach (string r in lstKey)
			{
				strResult += r + " = @" + r + " AND ";
			}
			return strResult.Remove(strResult.Length - 4);
		}

		private void btBackupDB_Click(object sender, EventArgs e)
		{
			string strSQL = "BACKUP DATABASE " + currentDbName + " TO DISK= '"
		  + getBakFileName() + "' WITH INIT";
			SqlDAL.ExecuteCmd(strSQL);
			MessageBox.Show("Backup database successed!", "information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private string getBakFileName()
		{
			string strSQL = "SELECT TOP 1 [filename] FROM [sysfiles]";
			DataTable tbl = SqlDAL.ExecuteQuery(strSQL).Tables[0];
			if(tbl==null||tbl.Rows.Count <= 0)
			{
				MessageBox.Show("Unkowns database path name. Backup failed!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return "";
			}
			return tbl.Rows[0][0].ToString().Replace(".mdf", DateTime.Now.ToString("yyyyMMdd_HHmmss")+ ".bak");
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private FrmConnection _FrmConnection;
		private void btnConnection_Click(object sender, EventArgs e)
		{
			if (this._FrmConnection == null || this._FrmConnection.IsDisposed)
			{
				this._FrmConnection = new FrmConnection();
			}
			this._FrmConnection.ShowDialog(this);
		}
	}
}
