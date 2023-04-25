using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.model.dto;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private static string ASCENDING = " ASC";
        private static string DESCENDING = " DESC";
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
            List<TaskDto> list = new List<TaskDto>();
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                EmployeeRepository employeeRepository = new EmployeeRepository();
                Dictionary<long, string> keyValues = employeeRepository.findAll(command)
                    .ToDictionary(keySelector: it => it.id, elementSelector: it => it.name);
                TaskRepository taskRepository = new TaskRepository();
                list = taskRepository.findAllTaskDto(command).Select(it =>
                {
                    string assignee = string.Empty;
                    string reporter = string.Empty;
                    string qa = string.Empty;
                    keyValues.TryGetValue(it.employeeAssignee, out assignee);
                    it.nameEmployeeAssignee = assignee;
                    keyValues.TryGetValue(it.employeeReporter, out reporter);
                    it.nameEmployeeReporter = reporter;
                    keyValues.TryGetValue(it.employeeQA, out qa);
                    it.nameEmployeeQA = qa;
                    log.Info(it);
                    return it;
                }).ToList();
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        protected string showStatus(object obj)
        {
            return Enum.GetName(typeof(TaskStatus), obj);
        }

        protected string showPriority(object obj)
        {
            return Enum.GetName(typeof(TaskPriority), obj);
        }

        protected void TaskGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            List<TaskDto> tasks = new List<TaskDto>();
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                tasks = SortGridView((List<TaskDto>)ViewState["task"], sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                tasks = SortGridView((List<TaskDto>)ViewState["task"], sortExpression, ASCENDING);
            }
            TaskGridView.DataSource = tasks;
            TaskGridView.DataBind();
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        private List<TaskDto> SortGridView(List<TaskDto> list, string sortExpression, string direction)
        {
            Comparison<TaskDto> comparison = GetTaskComparison(sortExpression + direction);
            list.Sort(comparison);
            return list;
        }

        private static Comparison<TaskDto> GetTaskComparison(string sortExpression)
        {
            string[] sortExpressionParts = sortExpression.Split(' ');

            string sortField = sortExpressionParts[0];
            bool descending = sortExpressionParts.Length > 1 && sortExpressionParts[1].ToUpper() == "DESC";

            Comparison<TaskDto> comparison;

            if (descending)
            {
                comparison = (p1, p2) => Comparer<string>.Default.Compare(p2.GetType().GetProperty(sortField).GetValue(p2).ToString(),
                                                                              p1.GetType().GetProperty(sortField).GetValue(p1).ToString());
            }
            else
            {
                comparison = (p1, p2) => Comparer<string>.Default.Compare(p1.GetType().GetProperty(sortField).GetValue(p1).ToString(),
                                                                              p2.GetType().GetProperty(sortField).GetValue(p2).ToString());
            }

            return comparison;
        }
    }
}