﻿using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC = BCrypt.Net.BCrypt;

namespace btl_web_nangcao_task_management_system.page.authentication
{
    public partial class SignUp : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        const string EMAIL_REGEX = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        const string NAME_REGEX = "^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s\\W|_]+$";

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPageAuthentication)Master).LabelHeaderMasterPageAuthentication.Text = "Signup";
        }

        protected void signupButton_Click(object sender, EventArgs e)
        {
            if (!CheckInputValues())
            {
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                EmployeeRepository employeeRepository = new EmployeeRepository();
                Employee employee = new Employee
                {
                    email = emailTextBox.Text,
                    name = nameTextBox.Text,
                    password = BC.HashPassword(passwordTextBox.Text),
                    role = EmployeeRole.INIT
                };
                employeeRepository.save(command, employee);
                HttpCookie cookie = new HttpCookie("userLogin");
                cookie["email"] = emailTextBox.Text;
                Response.Clear();
                Response.Cookies.Add(cookie);
                Response.Redirect("Login.aspx");
                Response.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                errorMessage.Text = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
        }

        private bool CheckInputValues()
        {
            bool isPassed = true;
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                isPassed = false;
                nameTextBox.CssClass = string.Format("{0} is-invalid", nameTextBox.CssClass);
                feedbackName.Text = "Please enter your name";
            }
            else if (!Regex.IsMatch(nameTextBox.Text, NAME_REGEX))
            {
                isPassed = false;
                nameTextBox.CssClass = string.Format("{0} is-invalid", nameTextBox.CssClass);
                feedbackName.Text = "Invalid your name";
            }
            else
            {
                feedbackName.Text = string.Empty;
                nameTextBox.CssClass = nameTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(emailTextBox.Text))
            {
                isPassed = false;
                emailTextBox.CssClass = string.Format("{0} is-invalid", emailTextBox.CssClass);
                feedbackEmail.Text = "Please enter your email";
            }
            else if (!Regex.IsMatch(emailTextBox.Text, EMAIL_REGEX))
            {
                isPassed = false;
                emailTextBox.CssClass = string.Format("{0} is-invalid", emailTextBox.CssClass);
                feedbackEmail.Text = "Invalid email";
            }
            else
            {
                feedbackEmail.Text = string.Empty;
                emailTextBox.CssClass = emailTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                isPassed = false;
                passwordTextBox.CssClass = string.Format("{0} is-invalid", passwordTextBox.CssClass);
                feedbackPassword.Text = "Please enter your password";
            }
            else
            {
                feedbackPassword.Text = string.Empty;
                passwordTextBox.CssClass = passwordTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            if (string.IsNullOrEmpty(confirmPasswordTextBox.Text)
                || !confirmPasswordTextBox.Text.Equals(passwordTextBox.Text))
            {
                isPassed = false;
                confirmPasswordTextBox.CssClass = string.Format("{0} is-invalid", confirmPasswordTextBox.CssClass);
                feedbackConfirmPassword.Text = "Invalid confirm password";
            }
            else
            {
                feedbackConfirmPassword.Text = string.Empty;
                confirmPasswordTextBox.CssClass = confirmPasswordTextBox.CssClass.Replace("is-invalid", string.Empty);
            }
            return isPassed;
        }
    }
}