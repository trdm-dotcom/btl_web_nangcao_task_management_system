using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectRemoveEmployee : System.Web.UI.Page
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillProjectDropDownList();
            }
            if (Request.QueryString["project"] != null)
            {
                FillEmployeeListBox(long.Parse(Request.QueryString["project"]));
            }
            if (Request.QueryString["action"] != null)
            {
                string response = string.Empty;
                switch (Request.QueryString["action"])
                {
                    case "loadEmployeeInProject":
                        response = getEmployeeInProject(long.Parse(Request.QueryString["projectId"]));
                        break;
                }
                Response.Clear();
                Response.Write(response);
                Response.ContentType = "application/json";
                Response.Flush();
                Response.Close();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string removeEmployee(List<long> employeeIds, long projectId)
        {
            string message = string.Empty;
            bool error = false;
            if (employeeIds.Count < 1)
            {
                message = "Please select employee";
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    try
                    {
                        ProjectRepository projectRepository = new ProjectRepository();
                        Dictionary<string, object> parametes = new Dictionary<string, object>
                    {
                        {"id",  projectId}
                    };
                        List<Project> projects = projectRepository.findByConditionAnd(command, parametes);
                        if (projects.Count > 0)
                        {
                            if (projects[0].status.Equals(ProjectStatus.OPEN))
                            {
                                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                                employeeIds.ToList().ForEach(employeeId =>
                                {
                                    EmployeeProject employeeProject = new EmployeeProject();
                                    employeeProject.employeeId = employeeId;
                                    employeeProject.projectId = projectId;
                                    employeeProjectRepository.delete(command, employeeProject);
                                });
                                message = "Remove success";
                            }
                            else
                            {
                                message = "Project was closed";
                                error = true;
                            }
                        }
                        else
                        {
                            message = "Project not found";
                            error = true;
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        log.Error("error trying to delete", ex);
                        transaction.Rollback();
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("error trying to do something", ex);
                    message = "Internal error server";
                    error = true;
                }
                finally
                {
                    connection.Close();
                }
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }

        private void FillProjectDropDownList()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                ProjectRepository projectRepository = new ProjectRepository();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"status", Enum.GetName(typeof(ProjectStatus), ProjectStatus.OPEN)}
                };
                projectRepository.findByConditionAnd(command, parameters).ForEach(p =>
                {
                    projectDropDownList.Items.Add(new ListItem(p.title, p.id.ToString()));
                });
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        private string getEmployeeInProject(long projectId)
        {
            string response = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "projectId", projectId}
                };
                response = JsonConvert.SerializeObject(employeeProjectRepository.findByConditionAnd(command, parameters));
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        private void FillEmployeeListBox(long projectId)
        {
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "projectId", projectId}
                };
                removeEmployeeListBox.DataSource = employeeProjectRepository.findByConditionAnd(command, parameters);
                removeEmployeeListBox.DataTextField = "employeeName";
                removeEmployeeListBox.DataValueField = "employeeId";
                removeEmployeeListBox.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                ListItem listItem = projectDropDownList.Items.FindByValue(projectId.ToString());
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
                connection.Close();
            }
        }
    }
}