using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.task
{
    public partial class TaskEdit : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != null && (string)Session["role"] != Enum.GetName(typeof(EmployeeRole), EmployeeRole.INIT))
            {
                if (string.IsNullOrEmpty(Request.QueryString["task"]))
                {
                    Response.Clear();
                    Response.Redirect("TaskCreate.aspx");
                    Response.Close();
                }
                if (!Page.IsPostBack)
                {
                    LoadTaskData(long.Parse(Request.QueryString["task"]));
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
        
        private void LoadTaskData(long taskId)
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
                    { "id", taskId }
                };
                TaskRepository taskRepository = new TaskRepository();
                List<Task> result = taskRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    if (!result[0].status.Equals(TaskStatus.DONE))
                    {
                        Task task = result[0];
                        titleTextBox.Text = task.name;
                        descriptionTextBox.Text = task.description;
                        startDateTextBox.Text = task.startDate.ToString("yyyy-MM-dd");
                        estimateDateTextBox.Text = task.estimateDate.ToString("yyyy-MM-dd");
                        ViewState["taskId"] = task.id;
                        GetEmployees(command, task.projectId).ForEach(it =>
                        {
                            if (it.role.Equals(EmployeeRole.INIT))
                            {
                                return;
                            }
                            assigneeDropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
                            reporterDropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
                            QADropDownList.Items.Add(new ListItem(it.name, it.id.ToString()));
                        });

                        Thread t1 = new Thread(() =>
                        {
                            ListItem listItem = priorityDropDownList.Items.FindByValue(task.priority.ToString());
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }
                        });
                        t1.Start();

                        Thread t2 = new Thread(() =>
                        {
                            ListItem listItem = reporterDropDownList.Items.FindByValue(task.employeeReporter.ToString());
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }
                        });
                        t2.Start();

                        Thread t3 = new Thread(() =>
                        {
                            ListItem listItem = assigneeDropDownList.Items.FindByValue(task.employeeAssignee.ToString());
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }
                        });
                        t3.Start();

                        Thread t4 = new Thread(() =>
                        {
                            ListItem listItem = QADropDownList.Items.FindByValue(task.employeeQA.ToString());
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }
                        });
                        t4.Start();

                        t1.Join();
                        t2.Join();
                        t3.Join();
                        t4.Join();
                    }
                    else
                    {
                        errorMessage.Text = "Task was done";
                        updateButton.Enabled = false;
                    }
                }
                else
                {
                    errorMessage.Text = "Task not found";
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

        protected void updateButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
            successMessage.Text = string.Empty;
            if (ViewState["taskId"] == null)
            {
                errorMessage.Text = "Invalid task";
                return;
            }
            long taskId = (long)ViewState["taskId"];
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
                TaskRepository taskRepository = new TaskRepository();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"id",  taskId}
                };
                List<Task> result = taskRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    if (!result[0].status.Equals(TaskStatus.DONE))
                    {
                        Task task = result[0];
                        parameters = new Dictionary<string, object>
                        {
                            {"name", titleTextBox.Text },
                            {"description", descriptionTextBox.Text},
                            {"startDate", Convert.ToDateTime(startDateTextBox.Text)},
                            {"estimateDate", Convert.ToDateTime(estimateDateTextBox.Text)},
                            {"priority",  priorityDropDownList.SelectedItem.Value },
                        };
                        if (reporterDropDownList.SelectedIndex > 0)
                        {
                            parameters.Add("employeeReporter", long.Parse(reporterDropDownList.SelectedItem.Value));
                        }
                        if (assigneeDropDownList.SelectedIndex > 0)
                        {
                            parameters.Add("employeeAssignee", long.Parse(assigneeDropDownList.SelectedItem.Value));
                        }
                        if (QADropDownList.SelectedIndex > 0)
                        {
                            parameters.Add("employeeQA", long.Parse(QADropDownList.SelectedItem.Value));
                        }
                        taskRepository.update(command, parameters, task.id);
                        successMessage.Text = "Update success";
                    }
                    else
                    {
                        errorMessage.Text = "Task was done";
                    }
                }
                else
                {
                    errorMessage.Text = "Task not found";
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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
            bool isPassed = validDropDownList(priorityDropDownList, feedbackPriority, "Please select priority for task");
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

        private List<Employee> GetEmployees(SqlCommand command, long projectId)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.findByEmployeeProjectProjectId(command, projectId);
        }
    }
}