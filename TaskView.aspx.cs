using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.repositories;
using btl_web_nangcao_task_management_system.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.task
{
    public partial class TaskView : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["task"]))
            {
                loadTask(long.Parse(Request.QueryString["task"]));
            }
            else
            {
                Response.Clear();
                Response.Redirect("ProjectPage.aspx");
                Response.Close();
            }
        }

        private void loadTask(long taskId)
        {
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                TaskRepository taskRepository = new TaskRepository();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"id", taskId},
                };
                List<Task> tasks = taskRepository.findByConditionAnd(command, parameters);
                if (taskId > 0)
                {
                    Task task = tasks[0];
                    nameTaskLabel.Text = task.name;
                    descriptionPanel.Controls.Add(new LiteralControl(task.description));
                    priorityLabel.Text = Enum.GetName(typeof(TaskPriority), task.priority);
                    priorityLabel.CssClass = task.priority.Equals(TaskPriority.HIGH) ? "text-danger" : (task.priority.Equals(TaskPriority.MEDIUM) ? "text-warning" : "text-primary");
                    startDateLabel.Text = task.startDate.ToString("dd/MM/yyyy");
                    estimateDateLabel.Text = task.estimateDate.ToString("dd/MM/yyyy");
                    ViewState["task"] = task.id;
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
                        ListItem listItem = reporterDropDownList.Items.FindByValue(task.employeeReporter.ToString());
                        if (listItem != null)
                        {
                            listItem.Selected = true;
                        }
                    });
                    t1.Start();

                    Thread t2 = new Thread(() =>
                    {
                        ListItem listItem = assigneeDropDownList.Items.FindByValue(task.employeeAssignee.ToString());
                        if (listItem != null)
                        {
                            listItem.Selected = true;
                        }
                    });
                    t2.Start();

                    Thread t3 = new Thread(() =>
                    {
                        ListItem listItem = QADropDownList.Items.FindByValue(task.employeeQA.ToString());
                        if (listItem != null)
                        {
                            listItem.Selected = true;
                        }
                    });
                    t3.Start();

                    Thread t4 = new Thread(() =>
                    {
                        ListItem listItem = statusDropDownList.Items.FindByValue(Enum.GetName(typeof(TaskStatus), task.status));
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
                    errorMessage.Text = "Task not found";
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

        private List<Employee> GetEmployees(SqlCommand command, long projectId)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.findByEmployeeProjectProjectId(command, projectId);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string updateStatus(string status, long task)
        {
            string message = string.Empty;
            bool error = false;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    TaskRepository taskRepository = new TaskRepository();
                    Dictionary<string, Object> parameters = new Dictionary<string, object>
                    {
                        { "status", status },
                    };
                    taskRepository.update(command, parameters, task);
                    transaction.Commit();
                    message = "Update success";
                }
                catch (Exception ex)
                {
                    log.Error("error trying to update", ex);
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                log.Error("error trying to do something", ex);
                message = "Internal error server";
                error = true;
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string updateEmployee(String role, long employee, long task)
        {
            string message = string.Empty;
            bool error = false;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    TaskRepository taskRepository = new TaskRepository();
                    Dictionary<string, Object> parameters = new Dictionary<string, object>
                    {
                        { role, employee },
                    };
                    taskRepository.update(command, parameters, task);
                    transaction.Commit();
                    message = "Update success";
                }
                catch (Exception ex)
                {
                    log.Error("error trying to update", ex);
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                message = "Internal error server";
                error = true;
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string postComments(String content, long user, long task, string name)
        {
            string message = string.Empty;
            bool error = false;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    Comment comment = new Comment()
                    {
                        content = content,
                        taskId = task,
                        employeeId = user,
                        employeeName = name
                    };
                    CommentRepository commentRepository = new CommentRepository();
                    commentRepository.save(command, comment);
                    transaction.Commit();
                    message = "Post comment success";
                }
                catch (Exception ex)
                {
                    log.Error("error trying to update", ex);
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                message = "Internal error server";
                error = true;
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string getComments(long task, long offset, long order)
        {
            List<Comment> comments = new List<Comment>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                Dictionary<string, Object> parameters = new Dictionary<string, object>
                {
                    { "taskId", task },
                };
                CommentRepository commentRepository = new CommentRepository();
                comments = commentRepository.findByConditionAndWithPaging(command, parameters, offset, order == 1 ? "DESC" : "ASC");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                log.Error("error trying to do something", ex);
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(comments);
        }
    }
}