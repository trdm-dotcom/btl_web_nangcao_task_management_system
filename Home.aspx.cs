﻿using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.model.dto;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page
{
    public partial class Home : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            long userId = (long)Session["user"];
            Thread t1 = new Thread(() => loadProject(userId));
            Thread t2 = new Thread(() => loadTask(userId));
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();
        }

        private void loadProject(long userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                ProjectRepository projectRepository = new ProjectRepository();
                recentProjectListView.DataSource = projectRepository.findAllProjectEmployeeJoinIn(command, userId);
                recentProjectListView.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        private void loadTask(long userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                TaskRepository taskRepository = new TaskRepository();
                taskAssignedListView.DataSource = taskRepository.findAllTaskAssigneeToEmployee(command, userId);
                taskAssignedListView.DataBind();
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