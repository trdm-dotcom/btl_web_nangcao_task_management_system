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
    public partial class ProjectAddEmployee : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FillProjectDropDownList();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            if (selectedEmployeeListBox.SelectedItem == null)
            {
                errorMessage.Text = "Please select employee";
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
                            foreach (ListItem item in selectedEmployeeListBox.Items)
                            {
                                EmployeeProject employeeProject = new EmployeeProject();
                                employeeProject.employeeId = int.Parse(item.Text);
                                employeeProject.employeeName = item.Text;
                                employeeProject.projectId = projects[0].id;
                                employeeProject.projectName = projectDropDownList.SelectedItem.Text;
                                employeeProjectRepository.save(command, employeeProject);
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
                catch(Exception ex) {
                    transaction.Rollback();
                    log.error("error trying insert record to db", ex);
                    throw ex;
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
            allEmployeeListBox.Items.Clear();
            selectedEmployeeListBox.Items.Clear();
            if (!validDropDownList(projectDropDownList, feedbackProject, "Please select a project"))
            {
                return;
            }
            FillEmployeeListBox(int.Parse(projectDropDownList.SelectedItem.Value));
        }

        protected void singleAddButton_Click(object sender, EventArgs e)
        {
            if (allEmployeeListBox.SelectedItem == null)
            {
                return;
            }
            ListItem listItemObject = new ListItem();
            listItemObject.Text = allEmployeeListBox.SelectedItem.Text;
            listItemObject.Value = allEmployeeListBox.SelectedItem.Value;
            selectedEmployeeListBox.Items.Insert(0, listItemObject);
            allEmployeeListBox.Items.RemoveAt(allEmployeeListBox.SelectedIndex);
        }

        protected void allAddButton_Click(object sender, EventArgs e)
        {
            if (allEmployeeListBox.Items.Count == 0)
            {
                return;
            }
            foreach (ListItem item in allEmployeeListBox.Items)
            {
                ListItem listItemObject = new ListItem();
                listItemObject.Text = item.Text;
                listItemObject.Value = item.Value;
                selectedEmployeeListBox.Items.Insert(0, listItemObject);
            }
            allEmployeeListBox.Items.Clear();
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
                log.error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        private void FillEmployeeListBox(int projectId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                List<EmployeeProject> employeeProjects = employeeProjectRepository.getByProjectId(command, projectId);
                allEmployeeListBox.DataSource = employeeProjects;
                allEmployeeListBox.DataTextField = "employeeName";
                allEmployeeListBox.DataValueField = "employeeId";
                allEmployeeListBox.DataBind();

                selectedEmployeeListBox.DataSource = employeeProjects;
                selectedEmployeeListBox.DataTextField = "employeeName";
                selectedEmployeeListBox.DataValueField = "employeeId";
                selectedEmployeeListBox.DataBind();
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