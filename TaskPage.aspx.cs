using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.model.dto;
using btl_web_nangcao_task_management_system.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections;
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
    public partial class TaskAll : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["project"] != null) {
                loadTask(long.Parse(Request.QueryString["project"]));
            }
            else
            {
                Response.Clear();
                Response.Redirect("ProjectPage.aspx");
                Response.Close();
            }
        }

        private void loadTask(long projectId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                Dictionary<long, string> employeeProjects = new Dictionary<long, string>();
                TaskRepository taskRepository = new TaskRepository();
                List<TaskDto> todoTasks = new List<TaskDto>();
                List<TaskDto> progessTasks = new List<TaskDto>();
                List<TaskDto> doneTasks = new List<TaskDto>();
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"projectId",  projectId}
                };
                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                employeeProjectRepository.findByConditionAnd(command, parameters)
                    .ForEach(it =>
                    {
                        employeeProjects.Add(it.employeeId, it.employeeName);
                    });
                string value = string.Empty;
                taskRepository.findByConditionAnd(command, parameters)
                    .ForEach(task =>
                    {
                        TaskDto taskDto = new TaskDto
                        {
                            id = task.id,
                            name = task.name,
                            startDate = task.startDate,
                            nameEmployeeAssignee = employeeProjects.TryGetValue(task.employeeAssignee, out value) ? value : "none",
                            priority = task.priority
                        };
                        switch (task.status) {
                            case TaskStatus.TODO:
                                todoTasks.Add(taskDto);
                                break;
                            case TaskStatus.PROGESS: 
                                progessTasks.Add(taskDto);
                                break;
                            case TaskStatus.DONE:
                                doneTasks.Add(taskDto);
                                break;
                        }
                    });

                quantityTaskTodo.Text = todoTasks.Count.ToString();
                toDoListView.DataSource = todoTasks;
                toDoListView.DataBind();

                quantityTaskProgess.Text = progessTasks.Count.ToString();
                progessListView.DataSource = progessTasks;
                progessListView.DataBind();

                quantityTaskDone.Text = doneTasks.Count.ToString();
                doneListView.DataSource = doneTasks;
                doneListView.DataBind();
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

        protected string showPriority(object obj)
        {
            return Enum.GetName(typeof(TaskPriority), obj);
        }
    }
}