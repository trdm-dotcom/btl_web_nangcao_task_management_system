using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.Repositories
{
    class ProjectRepository {
        
        public List<Project> findAll(SqlCommand command) {
            try {
                List<Project> projectList = new List<Project>();
                command.CommandText = "";
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                foreach(DataRow dataRow in dataTable.Rows)
                {
                    Project project = new Project();
                    project.id = dataRow["id"].ToString();
                    project.tittle = dataRow["tittle"].ToString();
                    projectList.Add(project);
                }
                return projectList;
            }
            catch (Exception exp) {
                throw exp
            }
        }

        public List<Project> findBy(SqlCommand command) {
            try {
                List<Project> projectList = new List<Project>();
                command.CommandText = "";
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                foreach(DataRow dataRow in dataTable.Rows)
                {
                    Project project = new Project();
                    project.id = dataRow["id"].ToString();
                    project.tittle = dataRow["tittle"].ToString();
                    projectList.Add(project);
                }
                return projectList;
            }
            catch (Exception exp) {
                throw exp
            }
        }

        public int save(SqlCommand command, Project project) {
            try {
                command.CommandText = "";
                return  (int)command.ExecuteScalar();
            }
            catch (Exception exp) {
                throw exp
            }
        }
    }
}