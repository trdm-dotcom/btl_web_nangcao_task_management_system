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
using System.Diagnostics;

namespace btl_web_nangcao_task_management_system.page
{
    public partial class ProjectEdit : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != null && (string)Session["role"] == Enum.GetName(typeof(EmployeeRole), EmployeeRole.ADMIN))
            {
                if (string.IsNullOrEmpty(Request.QueryString["project"]))
                {
                    Response.Clear();
                    Response.Redirect("ProjectPage.aspx");
                    Response.Close();
                }
                if (!Page.IsPostBack)
                {
                    LoadProjectData(long.Parse(Request.QueryString["project"]));
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

        protected void updateButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            successMessage.Text = string.Empty;
            if (ViewState["projectId"] == null)
            {
                errorMessage.Text = "Invalid project";
                return;
            }
            long projectId = (long)ViewState["projectId"];
            if (!CheckInputValues())
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
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"id",  projectId}
                };
                List<Project> projects = projectRepository.findByConditionAnd(command, parameters);
                try
                {
                    if (projects.Count > 0)
                    {
                        if (projects[0].status.Equals(ProjectStatus.OPEN))
                        {
                            Project project = projects[0];
                            parameters = new Dictionary<string, object>()
                            {
                                {"title",  titleTextBox.Text},
                                {"description", descriptionTextBox.Text},
                                {"startDate", Convert.ToDateTime(startDateTextBox.Text)},
                                {"estimateDate", Convert.ToDateTime(estimateDateTextBox.Text)},
                                {"lead", long.Parse(leadDropDownList.SelectedItem.Value)}
                            };
                            projectRepository.update(command, parameters, project.id);
                            successMessage.Text = "Update success";
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
                    Debug.WriteLine(ex.ToString());
                    log.Error("error trying to update", ex);
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

        private void LoadProjectData(long projectId)
        {
            errorMessage.Text = string.Empty;
            successMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
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
                    if (result[0].status.Equals(ProjectStatus.OPEN))
                    {
                        parameters = new Dictionary<string, object>
                        {
                            {"projectId", projectId},
                        };
                        EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                        employeeProjectRepository.findByConditionAnd(command, parameters).ForEach(employeeProject =>
                        {
                            leadDropDownList.Items.Add(new ListItem(employeeProject.employeeName, employeeProject.employeeId.ToString()));
                        });
                        Project project = result[0];
                        titleTextBox.Text = project.title;
                        descriptionTextBox.Text = project.description;
                        startDateTextBox.Text = project.startDate.ToString("yyyy-MM-dd");
                        estimateDateTextBox.Text = project.estimateDate.ToString("yyyy-MM-dd");
                        ListItem listItem = leadDropDownList.Items.FindByValue(project.lead.ToString());
                        if (listItem != null)
                        {
                            listItem.Selected = true;
                        }
                        ViewState["projectId"] = project.id;
                    }
                    else
                    {
                        errorMessage.Text = "Project was close";
                        updateButton.Enabled = false;
                    }
                }
                else
                {
                    errorMessage.Text = "Project not found";
                    updateButton.Enabled = false;
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

        private bool CheckInputValues()
        {
            bool isPassed = validDropDownList(leadDropDownList, feedbackLead, "Please select project's lead");
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                titleTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackTitle.Text = "Please enter project title";
                isPassed = false;
            }
            else
            {
                feedbackTitle.Text = string.Empty;
                titleTextBox.CssClass = titleTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                descriptionTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackDescription.Text = "Please enter project description";
                isPassed = false;
            }
            else
            {
                feedbackDescription.Text = string.Empty;
                descriptionTextBox.CssClass = descriptionTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(startDateTextBox.Text))
            {
                startDateTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackStartDate.Text = "Please enter project start date";
                isPassed = false;
            }
            else
            {
                feedbackStartDate.Text = string.Empty;
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(estimateDateTextBox.Text))
            {
                estimateDateTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackestimateDate.Text = "Please enter estimate date";
                isPassed = false;
            }
            else
            {
                feedbackestimateDate.Text = string.Empty;
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (!DateTime.TryParse(estimateDateTextBox.Text, out _)
                || Convert.ToDateTime(estimateDateTextBox.Text) < DateTime.Now.Date)
            {
                estimateDateTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackestimateDate.Text = "Please enter valid date";
                isPassed = false;
            }
            else
            {
                feedbackestimateDate.Text = string.Empty;
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            return isPassed;
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
    }
}