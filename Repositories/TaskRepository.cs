using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.Repositories
{
    class TaskRepository {
        public List<Task> findAll(SqlCommand command) {
            List<Task> taskList = new List<Task>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_task WHERE 1=1";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Task task = new Task();
                task.id = int.Parse(dataRow["id"].ToString());
                task.name = dataRow["name"].ToString();
                task.description = dataRow["description"].ToString();
                task.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                task.estimatedTime = Convert.ToDateTime(dataRow["estimatedTime"].ToString());
                task.employeeReporter = int.Parse(dataRow["employeeReporter"].ToString());
                task.employeeAssignee = int.Parse(dataRow["employeeAssignee"].ToString());
                task.employeeQA = int.Parse(dataRow["employeeQA"].ToString());
                task.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                task.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                taskList.Add(task);
            }
            return taskList;
        }

        public List<Task> findByConditionAnd(SqlCommand command, Dictionary<string, object> parameters) {
            List<Task> taskList = new List<Task>();
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
            command.CommandText = string.Format("SELECT * FROM t_task WHERE {0}", sb.ToString());
            if (parameters.Count > 0 && parameters != null)
            {
                foreach (KeyValuePair<string, object> element in parameters)
                {
                    command.Parameters.AddWithValue(string.Format("@{0}", element.Key), element.Value);
                }
            }
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Task item = new Task();
                item.id = int.Parse(dataRow["id"].ToString());
                item.name = dataRow["name"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimatedTime = Convert.ToDateTime(dataRow["estimatedTime"].ToString());
                item.employeeReporter = int.Parse(dataRow["employeeReporter"].ToString());
                item.employeeAssignee = int.Parse(dataRow["employeeAssignee"].ToString());
                item.employeeQA = int.Parse(dataRow["employeeQA"].ToString());
                item.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                item.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                taskList.Add(item);
            }
            return taskList;
        }

        public int save(SqlCommand command, Task task)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_task(name, description, projectId, startDate, estimateDate, employeeReporter, employeeAssignee, employeeQA, status, priority)" +
                    " VALUES(@name, @description, @projectId, @startDate, @estimateDate, @employeeReporter, @employeeAssignee, @employeeQA, @status, @priority);";
            command.Parameters.AddWithValue("@name", task.name);
            command.Parameters.AddWithValue("@description", task.description);
            command.Parameters.AddWithValue("@projectId", task.projectId);
            command.Parameters.AddWithValue("@startDate", task.startDate);
            command.Parameters.AddWithValue("@estimateDate", task.estimatedTime);
            command.Parameters.AddWithValue("@employeeReporter", task.employeeReporter);
            command.Parameters.AddWithValue("@employeeAssignee", task.employeeAssignee);
            command.Parameters.AddWithValue("@employeeQA", task.employeeQA);
            command.Parameters.AddWithValue("@status", Enum.GetName(typeof(TaskStatus), task.status));
            command.Parameters.AddWithValue("@priority", Enum.GetName(typeof(TaskPriority), task.priority));
            return (int)command.ExecuteScalar();
        }

        public void update(SqlCommand command, Dictionary<string, object> parameters, int id)
        {
            StringBuilder sb = new StringBuilder("UPDATE t_task SET");
            foreach (string key in parameters.Keys)
            {
                sb.Append(string.Format(" {0} = @{1}", key, key));
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("WHERE id = @id");
            command.CommandType = CommandType.Text;
            command.CommandText = sb.ToString();
            command.Parameters.AddWithValue("@id", id);
            foreach (KeyValuePair<string, object> element in parameters)
            {
                command.Parameters.AddWithValue(string.Format("@{0}", element.Key), element.Value);
            }
            command.ExecuteNonQuery();
        }
    }
}