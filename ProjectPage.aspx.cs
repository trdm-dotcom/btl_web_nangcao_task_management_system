﻿using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.model.dto;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectPage : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            fillProjectGridView();
        }

        public void fillProjectGridView()
        {
            ProjectGridView.DataSource = getProjects();
            ProjectGridView.DataBind();
        }


        private List<ProjectDto> getProjects()
        {
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                ProjectRepository projectRepository = new ProjectRepository();
                return projectRepository.findAllProjectJoinEmployee(command);
            }   
            catch (Exception ex)  
            {
                log.Error("error trying to do something", ex);
                errorMessage.Text = "Internal error server";
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void ProjectGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProjectGridView.PageIndex = e.NewPageIndex;
            ProjectGridView.DataSource = getProjects();
            ProjectGridView.DataBind();
        }

        protected string showStatus(Object obj)
        {
            return Enum.GetName(typeof(ProjectStatus), obj);
        }

        protected string showDate(Object obj)
        {
            return obj.ToString();
        }
    }
}