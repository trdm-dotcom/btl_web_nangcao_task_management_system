using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC = BCrypt.Net.BCrypt;

namespace btl_web_nangcao_task_management_system.page.authentication
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        HttpCookie cookie = null;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPageAuthentication)Master).LabelHeaderMasterPageAuthentication.Text = "Login";
            HttpCookie cookie = Request.Cookies["userLogin"];
            if(cookie != null)
            {
                emailTextBox.Text = cookie["email"].ToString();
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
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
                Dictionary<String, Object> parameters = new Dictionary<string, object>()
                {
                    {"email", emailTextBox.Text}
                };
                List<Employee> employees = employeeRepository.findByConditionAnd(command, parameters);
                if(employees.Count > 0)
                {
                    if (BC.Verify(passwordTextBox.Text, employees[0].password))
                    {
                        Session["userID"] = employees[0].id.ToString();
                        if(cookie == null)
                        {
                            cookie = new HttpCookie("userLogin");
                        }
                        cookie["email"] = employees[0].email;
                        Response.Clear();
                        Response.Cookies.Add(cookie);
                        Response.Redirect("~/page/Home.aspx");
                        Response.Close();
                    }
                    else
                    {
                        errorMessage.Text = "Wrong email or password";
                    }
                }
                else
                {
                    errorMessage.Text = "Wrong email or password";
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

        private bool CheckInputValues()
        {
            bool isPassed = true;
            if (string.IsNullOrEmpty(emailTextBox.Text))
            {
                isPassed = false;
                emailTextBox.CssClass = string.Format("{0} is-invalid", emailTextBox.CssClass);
                feedbackEmail.Text = "Please enter your email";
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
            return isPassed;
        }
    }
}