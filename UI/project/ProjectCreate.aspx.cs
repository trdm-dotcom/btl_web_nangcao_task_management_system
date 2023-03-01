using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.UI
{
    public partial class ProjectCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (CheckInputValues())
            {
                return;
            }
            try 
            {
                Project project = new Project();
                project.title = titleTextBox.Text;
                project.description = descriptionTextBox.Text;
                project.startDate = Convert.ToDateTime(startDateTextBox.Text);
                project.estimateTime = Convert.ToDateTime(estimateDateTextBox.Text);
                project.status = (ProjectStatus) Enum.Parse(typeof(ProjectStatus), statusDropDownList.SelectedItem.Value);
                List<EmployeeProject> employeeProjects = new List<EmployeeProject>();
                foreach (ListItem item in selectedEmployeeListBox.Items)
                {
                    EmployeeProject employeeProject = new EmployeeProject();
                    employeeProject.employeeId = Convert.ToInt32(item.Text);
                    employeeProject.employeeName = item.Text;
                    employeeProject.projectName = project.title;
                    employeeProjects.Add(employeeProject);  
                }
            }
            catch (Exception exp)
            {
                errorMessage.Text = "Internal error server";
            }
        }

        protected void singleAddButton_Click(object sender, EventArgs e)
        {

        }

        protected void allAddButton_Click(object sender, EventArgs e)
        {

        }

        protected void singleRemoveButton_Click(object sender, EventArgs e)
        {

        }

        protected void allRemoveButton_Click(object sender, EventArgs e)
        {

        }

        private bool CheckInputValues()
        {
            bool isPassed = true;
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                titleTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackTitle.Text = "Please enter project title";
                isPassed = false;
            }
            else
            {
                feedbackTitle.Text = string.Empty;
                titleTextBox.CssClass = titleTextBox.CssClass.Replace("is-invalid", "");
            }
            if (string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                descriptionTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackDescription.Text = "Please enter project description";
                isPassed = false;
            }
            else
            {
                feedbackDescription.Text = string.Empty;
                descriptionTextBox.CssClass = descriptionTextBox.CssClass.Replace("is-invalid", "");
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
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", "");
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", "");
            }

            if (!DateTime.TryParse(startDateTextBox.Text, out _) 
                || Convert.ToDateTime(startDateTextBox.Text) < DateTime.Now.Date)
            {
                startDateTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackStartDate.Text = "Please enter valid date";
                isPassed = false;
            }
            else
            {
                feedbackStartDate.Text = string.Empty;
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", "");
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", "");
            }
            return isPassed;
        }
    }
}