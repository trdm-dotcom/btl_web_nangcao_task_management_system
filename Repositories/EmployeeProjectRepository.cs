using btl_web_nangcao_task_management_system.model.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace btl_web_nangcao_task_management_system.Repositories
{
    public class EmployeeProjectRepository
    {
        public void save(SqlCommand command, EmployeeProject employeeProject)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_employeeProject (employeeId, projectId, employeeName, projectName)" +
                " VALUES(@employeeId, @projectId, @employeeName, @projectName);";
            command.Parameters.AddWithValue("@employeeId", employeeProject.employeeId);
            command.Parameters.AddWithValue("@projectId", employeeProject.projectId);
            command.Parameters.AddWithValue("@employeeName", employeeProject.employeeName);
            command.Parameters.AddWithValue("@projectName", employeeProject.projectName);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public void delete(SqlCommand command, EmployeeProject employee)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM t_employeeProject WHERE employeeId = @employeeId AND projectId = @projectId;";
            command.Parameters.AddWithValue("@employeeId", employee.employeeId);
            command.Parameters.AddWithValue("@projectId", employee.projectId);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public List<EmployeeProject> findByConditionAnd(SqlCommand command, Dictionary<string, object> parameters)
        {
            List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();
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
            command.CommandText = string.Format("SELECT * FROM t_employeeProject WHERE {0}", sb.ToString());
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
                EmployeeProject employeeProject = new EmployeeProject();
                employeeProject.employeeId = (long)dataRow["employeeId"];
                employeeProject.employeeName = dataRow["employeeName"].ToString();
                employeeProject.projectName = dataRow["projectName"].ToString();
                employeeProject.projectId = (long)dataRow["projectId"];
                employeeProjectList.Add(employeeProject);
            }
            return employeeProjectList;
        }

        public List<EmployeeProject> getAllInProject(SqlCommand command, long id)
        {
            List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_employeeProject WHERE projectId = @projectId";
            command.Parameters.AddWithValue("@projectId", id);
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                EmployeeProject employeeProject = new EmployeeProject();
                employeeProject.employeeId = (long)dataRow["employeeId"];
                employeeProject.employeeName = dataRow["employeeName"].ToString();
                employeeProjectList.Add(employeeProject);
            }
            return employeeProjectList;
        }

        public List<EmployeeProject> getAllNotInProject(SqlCommand command, long id)
        {
            List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_employee AS e WHERE NOT EXISTS (" +
                " SELECT projectId FROM t_employeeProject AS ep WHERE ep.employeeId = e.id AND projectId = @projectId) ";
            command.Parameters.AddWithValue("@projectId", id);
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                EmployeeProject employeeProject = new EmployeeProject();
                employeeProject.employeeId = (long)dataRow["id"];
                employeeProject.employeeName = dataRow["name"].ToString();
                employeeProjectList.Add(employeeProject);
            }
            return employeeProjectList;
        }
    }
}