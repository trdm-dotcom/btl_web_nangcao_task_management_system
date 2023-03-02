using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.Repositories;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.UI
{
    public partial class ProjectCreate : System.Web.UI.Page
    {
        String connectionString = ConfigurationManager.ConnectionStrings["connTaskManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (CheckInputValues())
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try 
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                Project project = new Project();
                project.title = titleTextBox.Text;
                project.description = descriptionTextBox.Text;
                project.startDate = Convert.ToDateTime(startDateTextBox.Text);
                project.estimateTime = Convert.ToDateTime(estimateDateTextBox.Text);
                project.status = (ProjectStatus) Enum.Parse(typeof(ProjectStatus), statusDropDownList.SelectedItem.Value);
                
                try {
                    ProjectRepository projectRepository = new ProjectRepository();
                    int projectID = projectRepository.save(command, project);
                    
                    EmployeeRepository employeeRepository = new EmployeeRepository();
                    foreach (ListItem item in selectedEmployeeListBox.Items)
                    {
                        EmployeeProject employeeProject = new EmployeeProject();
                        employeeProject.employeeId = Convert.ToInt32(item.Text);
                        employeeProject.employeeName = item.Text;
                        employeeProject.projectId = projectID;
                        employeeProject.projectName = project.title;
                        employeeRepository.InsertEmployeeProject(command, employeeProjects);
                    }
                    transaction.Commit();
                }
                catch (Exception exp) {
                    transaction.Rollback();
                    throw exp;
                }
            }
            catch (Exception exp)
            {
                errorMessage.Text = "Internal error server";
            }
            finally {
                connection.close();
            }
        }

        protected void singleAddButton_Click(object sender, EventArgs e)
        {
            if (allEmployeeListBox.SelectedItem == null)
            {
                return;
            }

            ListItem listItemObject = new ListItem();
            listItemObject.Text = allEmployeeListBox.SelectedItem.Text;
            listItemObject.Value = allEmployeeListBox.SelectedItem.Value;
            selectedEmployeeListBox.Items.Insert(0, listItemObject);

            allEmployeeListBox.Items.RemoveAt(allEmployeeListBox.SelectedIndex);
        }

        protected void allAddButton_Click(object sender, EventArgs e)
        {
            if (allEmployeeListBox.Items.Count == 0)
            {
                return;
            }
            foreach (ListItem item in allEmployeeListBox.Items)
            {
                ListItem listItemObject = new ListItem();
                listItemObject.Text = item.Text;
                listItemObject.Value = item.Value;
                selectedEmployeeListBox.Items.Insert(0, listItemObject);
            }
            allEmployeeListBox.Items.Clear();
        }

        protected void singleRemoveButton_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeListBox.SelectedItem == null)
            {
                return;
            }

            ListItem listItemObject = new ListItem();
            listItemObject.Text = selectedEmployeeListBox.SelectedItem.Text;
            listItemObject.Value = selectedEmployeeListBox.SelectedItem.Value;
            allEmployeeListBox.Items.Insert(0, listItemObject);

            selectedEmployeeListBox.Items.RemoveAt(selectedEmployeeListBox.SelectedIndex);
        }

        protected void allRemoveButton_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeListBox.Items.Count == 0)
            {
                return;
            }
            foreach (ListItem item in selectedEmployeeListBox.Items)
            {
                ListItem listItemObject = new ListItem();
                listItemObject.Text = item.Text;
                listItemObject.Value = item.Value;
                allEmployeeListBox.Items.Insert(0, listItemObject);
            }
            selectedEmployeeListBox.Items.Clear();
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