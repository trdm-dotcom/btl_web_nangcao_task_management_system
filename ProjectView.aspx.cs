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

namespace btl_web_nangcao_task_management_system
{
    public partial class ProjectView : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["project"]))
            {
                loadProject(long.Parse(Request.QueryString["project"]));
            }
            else
            {
                Response.Clear();
                Response.Redirect("ProjectPage.aspx");
                Response.Close();
            }
        }

        private void loadProject(long id)
        {
            errorMessage.Text = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "id", id }
                };
                ProjectRepository projectRepository = new ProjectRepository();
                List<Project> result = projectRepository.findByConditionAnd(command, parameters);
                if (result.Count > 0)
                {
                    Project project = result[0];
                    projectTitleLabel.Text = project.title;
                    projectDescriptionLabel.Text= project.description;
                    projectStartDateLabel.Text = project.startDate.ToString("dd/M/yyyy");
                    projectEstimateDateLabel.Text = project.estimateDate.ToString("dd/M/yyyy");
                    projectLeadLabel.Text = project.lead.ToString();
                    projectStatusLabel.Text = Enum.GetName(typeof(ProjectStatus), project.status);
                }
                else
                {
                    errorMessage.Text = "Project not found";
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
    }
}