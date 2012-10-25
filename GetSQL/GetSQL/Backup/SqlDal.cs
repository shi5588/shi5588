using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GetSQL
{
    public static class SqlDAL
    {
        private static string getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ConfigurationManager.AppSettings["servername"].Trim();
            builder.InitialCatalog = FrmMain.CurrentDbName;
            builder.UserID = ConfigurationManager.AppSettings["userid"].Trim();
            builder.Password = ConfigurationManager.AppSettings["password"].Trim();
            builder.PersistSecurityInfo = true;
            builder.ConnectTimeout = 360;
            builder.ApplicationName = ConfigurationManager.AppSettings["appname"].Trim();
            return builder.ConnectionString;        
        }


        public static DataSet ExecuteQuery(string strSQL)
        { 
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                connection.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(strSQL, connection);
                adapter.Fill(ds);
                return ds;
            }            
        }

        public static void ExecuteCmd(string strSQL)
        {
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
