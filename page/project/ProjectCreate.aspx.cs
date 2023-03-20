﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.Repositories;
using btl_web_nangcao_task_management_system.model.db;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace btl_web_nangcao_task_management_system.page
{
    public partial class ProjectCreate : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            FillEmployeeListBox();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (!CheckInputValues())
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try 
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.Transaction = transaction;

                Project project = new Project();
                project.title = titleTextBox.Text;
                project.description = descriptionTextBox.Text;
                project.startDate = Convert.ToDateTime(startDateTextBox.Text);
                project.estimateDate = Convert.ToDateTime(estimateDateTextBox.Text);
                project.status = ProjectStatus.OPEN;
                project.lead = int.Parse(leadDropDownList.SelectedItem.Value);
                try {
                    ProjectRepository projectRepository = new ProjectRepository();
                    int projectID = projectRepository.save(command, project);

                    EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                    foreach (ListItem item in selectedEmployeeListBox.Items)
                    {
                        EmployeeProject employeeProject = new EmployeeProject();
                        employeeProject.employeeId = Convert.ToInt32(item.Text);
                        employeeProject.employeeName = item.Text;
                        employeeProject.projectId = projectID;
                        employeeProject.projectName = project.title;
                        employeeProjectRepository.save(command, employeeProject);
                    }
                    transaction.Commit();
                }
                catch (Exception ex) {
                    log.error("error trying to insert", ex);
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.error("error trying to something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally {
                connection.Close();
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
            leadDropDownList.Items.Insert(1, listItemObject);
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
                leadDropDownList.Items.Insert(1, listItemObject);
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
            leadDropDownList.Items.RemoveAt(selectedEmployeeListBox.SelectedIndex);
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
            leadDropDownList.Items.Clear(); 
        }

        private bool CheckInputValues()
        {
            bool isPassed = validDropDownList(leadDropDownList, feedbackLead, "Please select project's lead");
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                titleTextBox.CssClass = string.Format("{0} is-invalid", titleTextBox.CssClass);
                feedbackTitle.Text = "Please enter project title";
                isPassed = false;
            }
            else
            {
                feedbackTitle.Text = string.Empty;
                titleTextBox.CssClass = titleTextBox.CssClass.Replace("is-invalid", string.Empty);
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
                descriptionTextBox.CssClass = descriptionTextBox.CssClass.Replace("is-invalid", string.Empty);
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
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", string.Empty);
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", string.Empty);
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
                startDateTextBox.CssClass = startDateTextBox.CssClass.Replace("is-invalid", string.Empty);
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
                estimateDateTextBox.CssClass = estimateDateTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if(selectedEmployeeListBox.Items.Count < 1)
            {
                selectedEmployeeListBox.CssClass = string.Format("{0} is-invalid", selectedEmployeeListBox.CssClass);
                feedbackEmployee.Text = "Please select employee";
                isPassed = false;
            }
            else
            {
                feedbackEmployee.Text = string.Empty;
                selectedEmployeeListBox.CssClass = selectedEmployeeListBox.CssClass.Replace("is-invalid", string.Empty);
            }
            return isPassed;
        }

        private void FillEmployeeListBox()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                EmployeeRepository employeeRepository = new EmployeeRepository();
                allEmployeeListBox.DataSource = employeeRepository.findAll(command);
                allEmployeeListBox.DataTextField = "name";
                allEmployeeListBox.DataValueField = "id";
                allEmployeeListBox.DataBind();
            }
            catch (Exception ex)
            {
                log.error("error trying to something", ex);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        private bool validDropDownList(DropDownList dropDownList, Label feedbackLabel, string errorMessage)
        {
            bool isPassed = true;
            if (dropDownList.SelectedIndex <= 0
                || dropDownList.SelectedItem.Value == null)
            {
                dropDownList.CssClass = string.Format("{0} is-invalid", dropDownList.CssClass);
                feedbackLabel.Text = errorMessage;
                isPassed = false;
            }
            else
            {
                feedbackLabel.Text = string.Empty;
                dropDownList.CssClass = dropDownList.CssClass.Replace("is-invalid", string.Empty);
            }
            return isPassed;
        }

        protected void leadDropDownList_DataBound(object sender, EventArgs e)
        {
            leadDropDownList.Items.Insert(0, new ListItem("-Select-"));
        }
    }
}