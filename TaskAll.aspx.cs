using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.model.dto;
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

namespace btl_web_nangcao_task_management_system
{
    public partial class TaskAll : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["task"] = loadTask();
                TaskGridView.DataSource = (List<TaskDto>)ViewState["task"];
                TaskGridView.DataBind();
            }
        }


        protected void TaskGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TaskGridView.PageIndex = e.NewPageIndex;
            TaskGridView.DataSource = (List<TaskDto>)ViewState["task"];
            TaskGridView.DataBind();
        }

        private List<TaskDto> loadTask()
        {
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                EmployeeRepository employeeRepository = new EmployeeRepository();
                Dictionary<long, string> keyValues = employeeRepository.findAll(command).ToDictionary(keySelector: it => it.id, elementSelector: it => it.name);
                TaskRepository taskRepository = new TaskRepository();
                return (List<TaskDto>)taskRepository.findAllTaskDto(command).Select(it =>
                {
                    it.nameEmployeeAssignee = keyValues[it.employeeAssignee] != null ? keyValues[it.employeeAssignee] : string.Empty;
                    it.nameEmployeeReporter = keyValues[it.employeeReporter] != null ? keyValues[it.employeeReporter] : string.Empty;
                    it.nameEmployeeQA = keyValues[it.employeeQA] != null ? keyValues[it.employeeQA] : string.Empty;
                    return it;
                });
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        protected string showStatus(object obj)
        {
            return Enum.GetName(typeof(TaskStatus), obj);
        }

        protected string showPriority(object obj)
        {
            return Enum.GetName(typeof(TaskPriority), obj);
        }
    }
}