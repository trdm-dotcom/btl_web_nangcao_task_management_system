using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.Repositories
{
    class EmployeeRepository {

        public long save(SqlCommand command, Employee employee) {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO t_employee (password, email, role, name) output INSERTED.ID " +
                " VALUES(@password, @email, @role, @name);";
            command.Parameters.AddWithValue("@name", employee.name);
            command.Parameters.AddWithValue("@password", employee.password);
            command.Parameters.AddWithValue("@email", employee.email);
            command.Parameters.AddWithValue("role", Enum.GetName(typeof(EmployeeRole), employee.role));
            long id = (long)command.ExecuteScalar();
            command.Parameters.Clear();
            return id;
        }

        public List<Employee> findAll(SqlCommand command)
        {
            List<Employee> employeeList = new List<Employee>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM t_employee WHERE 1=1";
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Employee employee = new Employee();
                employee.id = (long)dataRow["id"];
                employee.name = dataRow["name"].ToString();
                employee.email = dataRow["email"].ToString();
                employee.role = (EmployeeRole)Enum.Parse(typeof(EmployeeRole), dataRow["role"].ToString());
                employeeList.Add(employee);
            }
            return employeeList;
        }

        public List<Employee> findByConditionAnd(SqlCommand command, Dictionary<string, object> parameters)
        {
            List<Employee> employeeList = new List<Employee>();
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
            command.CommandText = string.Format("SELECT * FROM t_employee WHERE {0}", sb.ToString());
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
                Employee item = new Employee();
                item.id = (long)dataRow["id"];
                item.name = dataRow["name"].ToString();
                item.email = dataRow["email"].ToString();
                item.password = dataRow["password"].ToString();
                item.role = (EmployeeRole)Enum.Parse(typeof(EmployeeRole), dataRow["role"].ToString());
                employeeList.Add(item);
            }
            return employeeList;
        }

        public List<Employee> findByEmployeeProjectProjectId(SqlCommand command, long projectId)
        {
            List<Employee> employeeList = new List<Employee>();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT e.* FROM t_employee AS e INNER JOIN t_employeeProject AS ep " +
                "ON e.id = ep.employeeId WHERE ep.projectId = @projectId";
            command.Parameters.AddWithValue("@projectId", projectId);
            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Employee item = new Employee();
                item.id = (long)dataRow["id"];
                item.name = dataRow["name"].ToString();
                item.email = dataRow["email"].ToString();
                item.role = (EmployeeRole)Enum.Parse(typeof(EmployeeRole), dataRow["role"].ToString());
                employeeList.Add(item);
            }
            return employeeList;
        }

        public void update(SqlCommand command, Dictionary<string, object> parameters, int id)
        {
            StringBuilder sb = new StringBuilder("UPDATE t_employee SET");
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
            command.Parameters.Clear();
        }
    }
}