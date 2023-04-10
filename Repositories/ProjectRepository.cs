using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using System.Diagnostics;
using System.Text;
using btl_web_nangcao_task_management_system.model.dto;

namespace btl_web_nangcao_task_management_system.Repositories
{
    class ProjectRepository {
        public List<Project> findAll(SqlCommand command) {
            List<Project> projectList = new List<Project>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_project WHERE 1=1";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Project project = new Project();
                project.id = (long)dataRow["id"];
                project.title = dataRow["title"].ToString();
                project.description = dataRow["description"].ToString();
                project.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                project.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                project.status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), dataRow["status"].ToString());
                project.lead = (long)dataRow["lead"];
                projectList.Add(project);
            }
            return projectList;
        }

        public List<Project> findByConditionAnd(SqlCommand command, Dictionary<string, object> parameters) {
            List<Project> projectList = new List<Project>();
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
            command.CommandText = string.Format("SELECT * FROM t_project WHERE {0}", sb.ToString());
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
                Project item = new Project();
                item.id = (long)dataRow["id"];
                item.title = dataRow["title"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                item.status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), dataRow["status"].ToString());
                item.lead = (long)dataRow["lead"];
                projectList.Add(item);
            }
            return projectList;
        }

        public long save(SqlCommand command, Project project) {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_project (title, description, startDate, estimateDate, status, lead) output INSERTED.ID" +
                    " VALUES(@title, @description, @startDate, @estimateDate, @status, @lead);";
            command.Parameters.AddWithValue("@title", project.title);
            command.Parameters.AddWithValue("@description", project.description);
            command.Parameters.AddWithValue("@startDate", project.startDate);
            command.Parameters.AddWithValue("@estimateDate", project.estimateDate);
            command.Parameters.AddWithValue("@status", Enum.GetName(typeof(ProjectStatus), project.status));
            command.Parameters.AddWithValue("@lead", project.lead);
            long id = (long)command.ExecuteScalar();
            command.Parameters.Clear();
            return id;
        }

        public void update(SqlCommand command, Dictionary<string, object> parameters, long id)
        {
            StringBuilder sb = new StringBuilder("UPDATE t_project SET");
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

        public List<ProjectDto> findAllProjectJoinEmployee(SqlCommand command) {
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT p.*, e.name FROM t_project AS p LEFT JOIN t_employee AS e ON p.lead = e.id;";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            List<ProjectDto> projectList = new List<ProjectDto>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProjectDto item = new ProjectDto();
                item.id = (long)dataRow["id"];
                item.title = dataRow["title"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                item.status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), dataRow["status"].ToString());
                item.lead = (long)dataRow["lead"];
                item.leadName = dataRow["name"].ToString();
                projectList.Add(item);
            }
            return projectList;
        }

        public List<ProjectDto> findAllProjectEmployeeJoinIn(SqlCommand command, long employee)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT p.*, e.name FROM t_project AS p INNER JOIN t_employeeProject AS ep ON p.id = ep.projectId LEFT JOIN t_employee AS e ON p.lead = e.id WHERE ep.employeeId = @employee";
            command.Parameters.AddWithValue("@employee", employee);
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            List<ProjectDto> projectList = new List<ProjectDto>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProjectDto item = new ProjectDto();
                item.id = (long)dataRow["id"];
                item.title = dataRow["title"].ToString();
                item.description = dataRow["description"].ToString();
                item.startDate = Convert.ToDateTime(dataRow["startDate"].ToString());
                item.estimateDate = Convert.ToDateTime(dataRow["estimateDate"].ToString());
                item.status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), dataRow["status"].ToString());
                item.lead = (long)dataRow["lead"];
                item.leadName = dataRow["name"].ToString();
                projectList.Add(item);
            }
            return projectList;
        }
    }
}