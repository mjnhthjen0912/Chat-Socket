using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer
{
    class DataProvider
    {
        public string cnnString = "Data Source=.;Initial Catalog=ServerChatApp;Integrated Security=True";
        protected SqlConnection cnn;
        protected SqlDataAdapter dt;
        protected SqlCommand cm;
        public void connect()
        {
            cnn = new SqlConnection(cnnString);
            cnn.Open();
        }

        public void disconnect()
        {
            cnn.Close();
        }

        public bool ExecuteNonQuery(string sql)
        {
            try
            {
                cm = new SqlCommand(sql, cnn);
                cm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return true;
            }
        }

        public int ExecuteScalar(string sql)
        {
            cm = new SqlCommand(sql, cnn);
            return (int)cm.ExecuteScalar();
        }

        public bool ExecuteUpdateQuery(string sql)
        {
            try
            {
                connect();
                ExecuteNonQuery(sql);
                disconnect();
                return true;
            }
            catch {
                return false;
            }
        }

        public DataSet ExecuteQuery(string strSelect)
        {
            DataSet dataset = new DataSet();
            cm = new SqlCommand();
            cm.Connection = this.cnn;
            dt = new SqlDataAdapter(strSelect, cnn);
            try { dt.Fill(dataset); }
            catch (SqlException ex)
            { }
            return dataset;
        }

        public DataTable ExecuteQuery_DataTable(string strSelect)
        {
            return ExecuteQuery(strSelect).Tables[0];
        }
        
    }
}
