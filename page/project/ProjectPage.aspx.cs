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
using System.Web.WebSockets;

namespace btl_web_nangcao_task_management_system.page.project
{
    public partial class ProjectPage : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillProjectGridView();
            }
        }

        public void fillProjectGridView()
        {
            ProjectGridView.DataSource = getProjects();
            ProjectGridView.DataBind();
        }


        private List<Project> getProjects()
        {
            List<Project> projects = new List<Project>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                ProjectRepository projectRepository = new ProjectRepository();
                projects = projectRepository.findAll(command);
                projects.Sort();
                return projects;
            }   
            catch (Exception ex)  
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return projects;
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
    }
}