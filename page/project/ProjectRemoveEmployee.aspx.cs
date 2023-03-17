using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectRemoveEmployee : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            FillProjectDropDownList();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            if (removeEmployeeListBox.SelectedItem == null)
            {
                errorMessage.Text = "Please select employee";
                return;
            }
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
                        {"id",  projectDropDownList.SelectedItem.Value}
                    };
                    List<Project> projects = projectRepository.findByConditionAnd(command, parametes);
                    if (projects.Count > 0)
                    {
                        if (projects[0].status.Equals(ProjectStatus.OPEN))
                        {
                            EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                            foreach (ListItem item in removeEmployeeListBox.Items)
                            {
                                EmployeeProject employeeProject = new EmployeeProject();
                                employeeProject.employeeId = int.Parse(item.Text);
                                employeeProject.projectId = int.Parse(projectDropDownList.SelectedItem.Value);
                                employeeProjectRepository.delete(command, employeeProject);
                            }
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
                    transaction.Rollback();
                    throw ex;
                }
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

        protected void projectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeEmployeeListBox.Items.Clear();
            if (!validDropDownList(projectDropDownList, feedbackProject, "Please select a project"))
            {
                return;
            }
            FillEmployeeListBox(int.Parse(projectDropDownList.SelectedItem.Value));
        }

        protected void projectDropDownList_DataBound(object sender, EventArgs e)
        {
            projectDropDownList.Items.Insert(0, new ListItem("-Select-"));
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

        private void FillEmployeeListBox(int projectId)
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
                    { "projectId", int.Parse(projectDropDownList.SelectedItem.Value) }
                };
                removeEmployeeListBox.DataSource = employeeProjectRepository.findByConditionAnd(command, parameters);
                removeEmployeeListBox.DataTextField = "employeeName";
                removeEmployeeListBox.DataValueField = "employeeId";
                removeEmployeeListBox.DataBind();
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