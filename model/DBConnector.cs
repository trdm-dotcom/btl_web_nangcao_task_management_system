using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model
{
    public class DBConnector
    {
        private string sqlConnectionString = null;
        private SqlConnection sqlConn = null;

        public DBConnector()
        {
            sqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            sqlConn = new SqlConnection(sqlConnectionString);
        }

        public SqlConnection GetConnection
        {
            get { return sqlConn; }
        }
    }
}