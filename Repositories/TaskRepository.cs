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
using btl_web_nangcao_task_management_system.model.dto;

namespace btl_web_nangcao_task_management_system.Repositories
{
    class TaskRepository {
        public List<Task> findAll(SqlCommand command) {
            List<Task> taskList = new List<Task>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_task WHERE 1=1";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Task task = new Task();
                task.id = (long)dataRow["id"];
                task.projectId = (long)dataRow["projectId"];
                task.name = dataRow["name"].ToString();
                task.description = dataRow["description"].ToString();
                task.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                task.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                task.employeeReporter = (long)dataRow["employeeReporter"];
                task.employeeAssignee = (long)dataRow["employeeAssignee"];
                task.employeeQA = (long)dataRow["employeeQA"];
                task.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                task.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                task.createdAt = Convert.ToDateTime(dataRow["createAt"].ToString());
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
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Task item = new Task();
                item.id = (long)dataRow["id"];
                item.projectId = (long)dataRow["projectId"];
                item.name = dataRow["name"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                item.employeeReporter = (long)dataRow["employeeReporter"];
                item.employeeAssignee = (long)dataRow["employeeAssignee"];
                item.employeeQA = (long)dataRow["employeeQA"];
                item.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                item.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                item.createdAt = Convert.ToDateTime(dataRow["createAt"].ToString());
                taskList.Add(item);
            }
            return taskList;
        }

        public long save(SqlCommand command, Task task)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_task(name, description, projectId, startDate, estimateDate, employeeReporter, employeeAssignee, employeeQA, status, priority) output INSERTED.ID" +
                    " VALUES(@name, @description, @projectId, @startDate, @estimateDate, @employeeReporter, @employeeAssignee, @employeeQA, @status, @priority);";
            command.Parameters.AddWithValue("@name", task.name);
            command.Parameters.AddWithValue("@description", task.description);
            command.Parameters.AddWithValue("@projectId", task.projectId);
            command.Parameters.AddWithValue("@startDate", task.startDate);
            command.Parameters.AddWithValue("@estimateDate", task.estimateDate);
            command.Parameters.AddWithValue("@employeeReporter", task.employeeReporter);
            command.Parameters.AddWithValue("@employeeAssignee", task.employeeAssignee);
            command.Parameters.AddWithValue("@employeeQA", task.employeeQA);
            command.Parameters.AddWithValue("@status", Enum.GetName(typeof(TaskStatus), task.status));
            command.Parameters.AddWithValue("@priority", Enum.GetName(typeof(TaskPriority), task.priority));
            long id = (long)command.ExecuteScalar();
            command.Parameters.Clear();
            return id;
        }

        public void update(SqlCommand command, Dictionary<string, object> parameters, long id)
        {
            StringBuilder sb = new StringBuilder("UPDATE t_task SET");
            foreach (string key in parameters.Keys)
            {
                sb.Append(string.Format(" {0} = @{1}", key, key));
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE id = @id");
            command.CommandType = CommandType.Text;
            command.CommandText = sb.ToString();
            command.Parameters.AddWithValue("@id", id);
            foreach (KeyValuePair<string, object> element in parameters)
            {
                command.Parameters.AddWithValue(string.Format("@{0}", element.Key), element.Value);
            }
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public List<TaskDto> findAllTaskAssigneeToEmployee(SqlCommand command, long employee)
        {
            List<TaskDto> taskList = new List<TaskDto>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT t.*, p.title FROM t_task AS t INNER JOIN t_project AS p ON t.projectId = p.id WHERE employeeAssignee = @employeeAssignee ORDER BY createAt DESC";
            command.Parameters.AddWithValue("@employeeAssignee", employee);
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TaskDto item = new TaskDto();
                item.id = (long)dataRow["id"];
                item.projectId = (long)dataRow["projectId"];
                item.name = dataRow["name"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                item.employeeReporter = (long)dataRow["employeeReporter"];
                item.employeeAssignee = (long)dataRow["employeeAssignee"];
                item.employeeQA = (long)dataRow["employeeQA"];
                item.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                item.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                item.projectTitle = dataRow["title"].ToString();
                item.createdAt = Convert.ToDateTime(dataRow["createAt"].ToString());
                taskList.Add(item);
            }
            return taskList;
        }

        public List<TaskDto> findAllTaskDto(SqlCommand command)
        {
            List<TaskDto> taskList = new List<TaskDto>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT t.*, p.title FROM t_task AS t INNER JOIN t_project AS p ON t.projectId = p.id WHERE 1=1";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TaskDto task = new TaskDto();
                task.id = (long)dataRow["id"];
                task.projectId = (long)dataRow["projectId"];
                task.name = dataRow["name"].ToString();
                task.description = dataRow["description"].ToString();
                task.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                task.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                task.employeeReporter = (long)dataRow["employeeReporter"];
                task.employeeAssignee = (long)dataRow["employeeAssignee"];
                task.employeeQA = (long)dataRow["employeeQA"];
                task.status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dataRow["status"].ToString());
                task.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), dataRow["priority"].ToString());
                task.projectTitle = dataRow["title"].ToString();
                task.createdAt = Convert.ToDateTime(dataRow["createAt"].ToString());
                taskList.Add(task);
            }
            return taskList;
        }
    }
}