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

namespace btl_web_nangcao_task_management_system.page.task
{
    public partial class TaskCreate : System.Web.UI.Page
    {
        String connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            FillProjectDropDownList();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if(!CheckInputValues())
            {
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
                    Task task = new Task();
                    task.name = titleTextBox.Text; 
                    task.description = descriptionTextBox.Text;
                    task.projectId = int.Parse(projectDropDownList.SelectedItem.Value);
                    task.startDate = Convert.ToDateTime(startDateTextBox.Text);
                    task.estimatedTime = Convert.ToDateTime(estimateDateTextBox.Text);
                    if(reporterDropDownList.SelectedItem.Value != null 
                        && !reporterDropDownList.SelectedIndex.Equals(0))
                    {
                        task.employeeReporter = int.Parse(reporterDropDownList.SelectedItem.Value);
                    }
                    if (assigneeDropDownList.SelectedItem.Value != null
                        && !assigneeDropDownList.SelectedIndex.Equals(0))
                    {
                        task.employeeAssignee = int.Parse(assigneeDropDownList.SelectedItem.Value);
                    }
                    if (QADropDownList.SelectedItem.Value != null
                        && !QADropDownList.SelectedIndex.Equals(0))
                    {
                        task.employeeQA = int.Parse(QADropDownList.SelectedItem.Value);
                    }
                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.save(command, task);
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
            assigneeDropDownList.Items.Clear();
            reporterDropDownList.Items.Clear();
            QADropDownList.Items.Clear();
            if (!validDropDownList(projectDropDownList, feedbackProject, "Please select a project"))
            {
                return;
            }
            int projectId = int.Parse(projectDropDownList.SelectedItem.Value);
            List<Employee> employees = GetEmployees(projectId);
            assigneeDropDownList.DataSource = employees;
            assigneeDropDownList.DataTextField = "name";
            assigneeDropDownList.DataValueField = "id";
            assigneeDropDownList.DataBind();

            

            reporterDropDownList.DataSource = employees;
            reporterDropDownList.DataTextField = "name";
            reporterDropDownList.DataValueField = "id";
            reporterDropDownList.DataBind();

            QADropDownList.DataSource = employees;
            QADropDownList.DataTextField = "name";
            QADropDownList.DataValueField = "id";
            QADropDownList.DataBind();
        }

        private List<Employee> GetEmployees(int projectId)
        {
            List<Employee> employees= new List<Employee>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                EmployeeRepository employeeRepository = new EmployeeRepository();
                employees = employeeRepository.findByEmployeeProjectProjectId(command, projectId);
            }
            catch (Exception ex)
            {
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
            return employees;
        }

        protected void priorityDropDownList_DataBound(object sender, EventArgs e)
        {
            priorityDropDownList.Items.Insert(0, new ListItem("-Select-"));
            priorityDropDownList.Items.Insert(1, new ListItem(TaskPriority.LOW.ToString(), TaskPriority.LOW.ToString()));
            priorityDropDownList.Items.Insert(2, new ListItem(TaskPriority.MEDIUM.ToString(), TaskPriority.MEDIUM.ToString()));
            priorityDropDownList.Items.Insert(3, new ListItem(TaskPriority.HIGH.ToString(), TaskPriority.HIGH.ToString()));
        }

        private bool validDropDownList(DropDownList dropDownList, Label feedbackLabel, string errorMessage)
        {
            bool isPassed = true;
            if (dropDownList.SelectedIndex.Equals(0)
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

        protected void reporterDropDownList_DataBound(object sender, EventArgs e)
        {
            reporterDropDownList.Items.Insert(0, new ListItem("-Select-"));
        }

        protected void assigneeDropDownList_DataBound(object sender, EventArgs e)
        {
            assigneeDropDownList.Items.Insert(0, new ListItem("-Select-"));
        }

        protected void QADropDownList_DataBound(object sender, EventArgs e)
        {
            QADropDownList.Items.Insert(0, new ListItem("-Select-"));
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
                projectDropDownList.DataSource = projectRepository.findAll(command);
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
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", string.Empty);
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

        protected void projectDropDownList_DataBound(object sender, EventArgs e)
        {
            projectDropDownList.Items.Insert(0, new ListItem("-Select-"));
        }
    }
}