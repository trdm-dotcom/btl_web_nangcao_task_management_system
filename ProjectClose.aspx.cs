using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.Repositories;
using btl_web_nangcao_task_management_system.model.db;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Security.Cryptography;
using static log4net.Appender.ColoredConsoleAppender;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Web.Services;

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectClose : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != null && (string)Session["role"] == Enum.GetName(typeof(EmployeeRole), EmployeeRole.ADMIN))
            {
                if (!Page.IsPostBack)
                {
                    FillProjectDropDownList();
                }
            }
            else
            {
                Response.Clear();
                Response.Status = "403 Forbidden";
                Response.StatusCode = 403;
                Response.Clear();
            }
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            successMessage.Text = string.Empty;
            bool success = false;
            if (!validDropDownList(projectDropDownList, feedbackProject, "Please select a project"))
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                ProjectRepository projectRepository = new ProjectRepository();
                Dictionary<string, object> parametes = new Dictionary<string, object>
                {
                    {"id",  projectDropDownList.SelectedItem.Value}
                };
                List<Project> projects = projectRepository.findByConditionAnd(command, parametes);
                try
                {
                    if (projects.Count > 0)
                    {
                        if (projects[0].status.Equals(ProjectStatus.OPEN))
                        {
                            Dictionary<string, object> paramters = new Dictionary<string, object>()
                            {
                                {"status", Enum.GetName(typeof(ProjectStatus), ProjectStatus.CLOSE)}
                            };
                            projectRepository.update(command, paramters, projects[0].id);
                            successMessage.Text = "Close project success";
                        }
                        else
                        {
                            errorMessage.Text = "Project was closed";
                            success = false;
                        }
                    }
                    else
                    {
                        errorMessage.Text = "Project not found";
                        success = false;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("error trying to update record", ex);
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
                success = false;
            }
            finally
            {
                connection.Close();
            }
            if(success)
            {
                Response.Clear();
                Response.Redirect("ProjectPage.aspx");
                Response.Close();
            }
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
                projectRepository.findByConditionAnd(command, parameters).ForEach(project =>
                {
                    projectDropDownList.Items.Add(new ListItem(project.title, project.id.ToString()));
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

        private bool validDropDownList(DropDownList dropDownList, Label feedbackLabel, string errorMessage)
        {
            bool isPassed = true;
            if (dropDownList.SelectedIndex < 1
                || dropDownList.SelectedItem.Value == null)
            {
                dropDownList.CssClass = string.Format("{0} is-invalid", dropDownList.CssClass);
                feedbackLabel.Text = errorMessage;
                isPassed = false;
            }
            else
            {
                feedbackLabel.Text = string.Empty;
                dropDownList.CssClass = dropDownList.CssClass.Replace("is-invalid", string.Empty);
            }
            return isPassed;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        private string loadProjectData(long project)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string response = string.Empty;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "id", project }
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> result = projectRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    Project projectEntity = result[0];
                    response = JsonConvert.SerializeObject(new
                    {
                        title = projectEntity.title,
                        description = projectEntity.description,
                        status = Enum.GetName(typeof(ProjectStatus), projectEntity.status)
                    });
                }
            }
            catch(Exception ex)
            {
                log.Error("error trying to do something", ex);
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
    }
}