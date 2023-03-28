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

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectClose : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillProjectDropDownList();
            }
            if (Request.QueryString["action"] != null)
            {
                string response = string.Empty;
                switch (Request.QueryString["action"])
                {
                    case "loadProject":
                        response = LoadProjectData(long.Parse(Request.QueryString["project"]));
                        break;
                }
                Response.Clear();
                Response.Write(response);
                Response.ContentType = "application/json";
                Response.Flush();
                Response.Close();
            }
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
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
                            transaction.Commit();
                            Response.Clear();
                            Response.Redirect("ProjectPage.aspx");
                            Response.Close();
                        }
                        else
                        {
                            errorMessage.Text = "Project was closed";
                        }
                    }
                    else
                    {
                        errorMessage.Text = "Project not found";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    log.Error("error trying to update record", ex);
                    transaction.Rollback();
                    throw ex;
                }
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

        private string LoadProjectData(long projectId)
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
                    { "id", projectId }
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> result = projectRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    Project project = result[0];

                    response = JsonConvert.SerializeObject(new
                    {
                        title = project.title,
                        description = project.description,
                        status = Enum.GetName(typeof(ProjectStatus), project.status)
                    });
                }
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
    }
}