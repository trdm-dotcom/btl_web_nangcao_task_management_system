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
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    log.error("error trying to update record", ex);
                    transaction.Rollback();
                    throw exp;
                }
            }
            catch (Exception ex)
            {
                log.error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        protected void projectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> paramters = new Dictionary<string, object>()
                {
                    {"id", projectDropDownList.SelectedItem.Value}
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> projectList = projectRepository.findByConditionAnd(command, paramters);
                if (projectList.Count > 0)
                {
                    Project project = projectList[0];
                    closeButton.Enabled = !project.status.Equals(ProjectStatus.CLOSE);
                    errorMessage.Text = string.Empty;
                }
                else
                {
                    errorMessage.Text = "Project not found";
                }
            }
            catch (Exception ex)
            {
                log.error("error trying to do something", ex);
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
                projectDropDownList.DataSource = projectRepository.findByConditionAnd(command, parameters);
                projectDropDownList.DataTextField = "title";
                projectDropDownList.DataValueField = "id";
                projectDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                log.error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        protected void projectDropDownList_DataBound(object sender, EventArgs e)
        {
            projectDropDownList.Items.Insert(0, new ListItem("-Select-"));
        }

        private bool validDropDownList(DropDownList dropDownList, Label feedbackLabel, string errorMessage)
        {
            bool isPassed = true;
            if (dropDownList.SelectedIndex <= 0
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
    }
}