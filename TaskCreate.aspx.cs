using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.task
{
    public partial class TaskCreate : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != null && (string)Session["role"] != Enum.GetName(typeof(EmployeeRole), EmployeeRole.INIT))
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

        protected void saveButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            bool success = false;
            long taskId = -1;
            if (!CheckInputValues())
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
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        {"id", long.Parse(projectDropDownList.SelectedItem.Value)},
                        {"status", Enum.GetName(typeof(ProjectStatus), ProjectStatus.OPEN)}
                    };
                    ProjectRepository projectRepository = new ProjectRepository();
                    if(projectRepository.findByConditionAnd(command, parameters).Count() > 0)
                    {
                        Task task = new Task();
                        task.name = titleTextBox.Text;
                        task.description = descriptionTextBox.Text;
                        task.projectId = long.Parse(projectDropDownList.SelectedItem.Value);
                        task.startDate = Convert.ToDateTime(startDateTextBox.Text);
                        task.estimateDate = Convert.ToDateTime(estimateDateTextBox.Text);
                        if (reporterDropDownList.SelectedIndex > 0)
                        {
                            task.employeeReporter = long.Parse(reporterDropDownList.SelectedItem.Value);
                        }
                        if (assigneeDropDownList.SelectedIndex > 0)
                        {
                            task.employeeAssignee = long.Parse(assigneeDropDownList.SelectedItem.Value);
                        }
                        if (QADropDownList.SelectedIndex > 0)
                        {
                            task.employeeQA = long.Parse(QADropDownList.SelectedItem.Value);
                        }
                        task.priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), priorityDropDownList.SelectedItem.Value);
                        TaskRepository taskRepository = new TaskRepository();
                        taskId = taskRepository.save(command, task);
                    }
                    else
                    {
                        errorMessage.Text = "Invalid project";
                        success = false;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("error trying to insert", ex);
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
                Response.Redirect(string.Format("TaskEdit.aspx?task={0}", taskId));
                Response.Close();
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
            GetEmployees(long.Parse(projectDropDownList.SelectedItem.Value)).ForEach(it =>
            {
                assigneeDropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
                reporterDropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
                QADropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
            });
        }

        private List<Employee> GetEmployees(long projectId)
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
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
            return employees;
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

        private bool CheckInputValues()
        {
            bool isPassed = validDropDownList(projectDropDownList, feedbackProject, "Please select a project") && validDropDownList(priorityDropDownList, feedbackPriority, "Please select priority for task");
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
                feedbackDescription.Text = "Please enter project description";
                isPassed = false;
            }
            else
            {
                feedbackDescription.Text = string.Empty;
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
    }
}