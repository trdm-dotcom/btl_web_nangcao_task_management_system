using btl_web_nangcao_task_management_system.model.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using btl_web_nangcao_task_management_system.model;
using System.Text;
using System.Diagnostics;

namespace btl_web_nangcao_task_management_system.repositories
{
    public class CommentRepository
    {
        private static int FETCH_COUNT = int.Parse(System.Configuration.ConfigurationManager.AppSettings["fetchCount"].ToString());
        public void save(SqlCommand command, Comment comment)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_comment (content, employeeName, employeeId, taskId)" +
                " VALUES(@content, @employeeName, @employeeId, @taskId);";
            command.Parameters.AddWithValue("@content", comment.content);
            command.Parameters.AddWithValue("@employeeName", comment.employeeName);
            command.Parameters.AddWithValue("@employeeId", comment.employeeId);
            command.Parameters.AddWithValue("@taskId", comment.taskId);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public List<Comment> findByConditionAnd(SqlCommand command, Dictionary<string, object> parameters)
        {
            List<Comment> commentList = new List<Comment>();
            StringBuilder sb = new StringBuilder();
            if (parameters.Count < 1 || parameters == null)
            {
                sb.Append(" 1 = 1 ");
            }
            else
            {
                foreach (string key in parameters.Keys)
                {
                    if (sb.Length > 1)
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(string.Format(" {0} = @{1} ", key, key));
                }
            }
            command.CommandType = CommandType.Text;
            command.CommandText = string.Format("SELECT * FROM t_comment WHERE {0}", sb.ToString());
            if (parameters.Count > 0 && parameters != null)
            {
                foreach (KeyValuePair<string, object> element in parameters)
                {
                    command.Parameters.AddWithValue(string.Format("@{0}", element.Key), element.Value);
                }
            }
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Comment item = new Comment();
                item.id = (long)dataRow["id"];
                item.content = dataRow["content"].ToString();
                item.employeeName = dataRow["employeeName"].ToString();
                item.employeeId = (long)dataRow["employeeId"];
                item.taskId = (long)dataRow["taskId"];
                item.createAt = (DateTime)dataRow["createdAt"];
                commentList.Add(item);
            }
            return commentList;
        }

        public List<Comment> findByConditionAndWithPaging(SqlCommand command, Dictionary<string, object> parameters, long offset, string sort)
        {
            List<Comment> commentList = new List<Comment>();
            StringBuilder sb = new StringBuilder();
            if (parameters.Count < 1 || parameters == null)
            {
                sb.Append(" 1 = 1 ");
            }
            else
            {
                foreach (string key in parameters.Keys)
                {
                    if (sb.Length > 1)
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(string.Format(" {0} = @{1} ", key, key));
                }
            }
            command.CommandType = CommandType.Text;
            command.CommandText = string.Format("SELECT * FROM t_comment WHERE {0} ORDER BY createAt {1} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY", 
                sb.ToString(), sort, offset, FETCH_COUNT);
            if (parameters.Count > 0 && parameters != null)
            {
                foreach (KeyValuePair<string, object> element in parameters)
                {
                    command.Parameters.AddWithValue(string.Format("@{0}", element.Key), element.Value);
                }
            }
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Comment item = new Comment();
                item.id = (long)dataRow["id"];
                item.content = dataRow["content"].ToString();
                item.employeeName = dataRow["employeeName"].ToString();
                item.employeeId = (long)dataRow["employeeId"];
                item.taskId = (long)dataRow["taskId"];
                item.createAt = (DateTime)dataRow["createAt"];
                commentList.Add(item);
            }
            return commentList;
        }
    }
}