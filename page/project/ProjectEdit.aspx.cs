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

namespace btl_web_nangcao_task_management_system.page
{
    public partial class ProjectEdit : System.Web.UI.Page
    {
        String connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillProjectDropDownList();
                if (!string.IsNullOrEmpty(Request.QueryString["project"]))
                {
                    LoadProjectData(Request.QueryString["project"]);
                }
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
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
                Dictionary<string, object> parametes = new Dictionary<string, object>
                {
                    {"id",  projectDropDownList.SelectedItem.Value}
                };
                List<Project> projects = projectRepository.findByConditionAnd(command, parametes);
                try {
                    if (projects.Count > 0) 
                    {
                        if (projects[0].status.Equals(ProjectStatus.OPEN))
                        {
                            Project project = projects[0];
                            Dictionary<string, object> parameters = new Dictionary<string, object>()
                            {
                                {"title",  titleTextBox.Text},
                                {"description", descriptionTextBox.Text},
                                {"startDate", Convert.ToDateTime(startDateTextBox.Text)},
                                {"estimateTime", Convert.ToDateTime(estimateDateTextBox.Text)}
                            };
                            projectRepository.update(command, parameters, project.id);
                            transaction.Commit();
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
                catch (Exception exp) {
                    transaction.Rollback();
                    throw exp;
                }
            }
            catch (Exception ex)
            {
                errorMessage.Text = "Internal error server";
            }
            finally {
                connection.Close();
            }
        }

        private void LoadProjectData(int projectId) {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "id", int.Parse(projectDropDownList.SelectedItem.Value) }
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> result = projectRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    if (result[0].status.Equals(ProjectStatus.OPEN))
                    {
                        Project project = result[0];
                        titleTextBox.Text = project.title;
                        descriptionTextBox.Text = project.description;
                        startDateTextBox.Text = project.startDate.ToShortDateString();
                        estimateDateTextBox.Text = project.estimateTime.ToShortDateString();
                    }
                    else
                    {
                        errorMessage.Text = "Projec was close"; 
                        updateButton.Enabled = false;
                    }
                }
                else
                {
                    errorMessage.Text = "Project not found";
                    updateButton.Enabled = false;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadProjectData(string projectKey)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "key", projectKey }
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> result = projectRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    if (result[0].status.Equals(ProjectStatus.OPEN))
                    {
                        Project project = result[0];
                        titleTextBox.Text = project.title;
                        descriptionTextBox.Text = project.description;
                        startDateTextBox.Text = project.startDate.ToShortDateString();
                        estimateDateTextBox.Text = project.estimateTime.ToShortDateString();
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
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        private bool CheckInputValues()
        {
            bool isPassed = validDropDownList(projectDropDownList, feedbackProject, "Please select a project");
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                titleTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackTitle.Text = "Please enter project title";
                isPassed = false;
            }
            else
            {
                feedbackTitle.Text = string.Empty;
                titleTextBox.CssClass = titleTextBox.CssClass.Replace("is-invalid", "");
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
                descriptionTextBox.CssClass = descriptionTextBox.CssClass.Replace("is-invalid", "");
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
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", "");
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", "");
            }

            if (!DateTime.TryParse(startDateTextBox.Text, out _)
                || Convert.ToDateTime(startDateTextBox.Text) < DateTime.Now.Date)
            {
                startDateTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackStartDate.Text = "Please enter valid date";
                isPassed = false;
            }
            else
            {
                feedbackStartDate.Text = string.Empty;
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", "");
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", "");
            }
            return isPassed;
        }

        protected void projectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validDropDownList(projectDropDownList, feedbackProject, "Please select a project"))
            {
                titleTextBox.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;
                startDateTextBox.Text = string.Empty;
                estimateDateTextBox.Text = string.Empty;
                return;
            }
            LoadProjectData(int.Parse(projectDropDownList.SelectedItem.Value));
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
                dropDownList.CssClass = dropDownList.CssClass.Replace("is-invalid", "");
            }
            return isPassed;
        }
    }
}