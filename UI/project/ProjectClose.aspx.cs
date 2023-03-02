using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.Repositories;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.UI.project
{
    public partial class ProjectClose : System.Web.UI.Page
    {
        String connectionString = ConfigurationManager.ConnectionStrings["connTaskManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            if(!CheckInputValues())
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                Project project = new Project();
                project.Id = int.Parse(projectDropDownList.SelectedItem.Value);
                project.status = ProjectStatus.CLOSE;
            }
            catch(Exception ex)
            {
                errorMessage.Text = "Internal error server";
            }
            finally {
                connection.close();
            }
        }

        protected void projectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!CheckInputValues())
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                Project project = new Project();
                project.Id = int.Parse(projectDropDownList.SelectedItem.Value);
            }
            catch(Exception ex)
            {
                errorMessage.Text = "Internal error server";
            }
            finally {
                connection.close();
            }
        }

        private bool CheckInputValues()
        {
            bool isPassed = true;
            if (projectDropDownList.SelectedIndex.Equals(0))
            {
                projectDropDownList.CssClass = string.Format("{0} is-invalid", projectDropDownList.CssClass);
                feedbackProject.Text = "Please select a project";
                isPassed = false;
            }
            else
            {
                feedbackProject.Text = string.Empty;
                projectDropDownList.CssClass = projectDropDownList.CssClass.Replace("is-invalid", "");
            }
            return isPassed;
        }
    }
}