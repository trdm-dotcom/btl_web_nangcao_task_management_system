using btl_web_nangcao_task_management_system.model;
using btl_web_nangcao_task_management_system.model.db;
using btl_web_nangcao_task_management_system.Repositories;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system
{
    public partial class UserRemove : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connDBTaskManagementSystem"].ConnectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            loadEmployee();
        }

        private void loadEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                EmployeeRepository employeeRepository = new EmployeeRepository();
                employeeRepository.findAll(command).ForEach(employee =>
                {
                    employeeDropDownList.Items.Add(new ListItem(employee.name, employee.id.ToString()));
                });
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

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string getInfo(long employee)
        {
            bool error = false;
            string message = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"id", employee }
                };
                EmployeeRepository employeeRepository = new EmployeeRepository();
                Employee employeeEntity = employeeRepository.findByConditionAnd(command, parameters)[0];
                if (employeeEntity == null)
                {
                    error = true;
                    message = "Not found employee";
                }
                else
                {
                    message = Enum.GetName(typeof(EmployeeRole), employeeEntity.role);
                }
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                error = true;
                message = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string removeEmployee(long employee)
        {
            bool error = false;
            string message = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                EmployeeRepository employeeRepository = new EmployeeRepository();
                try
                {
                    if (employeeRepository.findByConditionAnd(command, new Dictionary<string, object> { { "id", employee } }).Count > 0)
                    {
                        employeeRepository.delete(command, employee);
                        message = "Remove success";
                    }
                    else
                    {
                        error = true;
                        message = "Not found employee";
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.Error("error trying to do something", ex);
                error = true;
                message = "Internal error server";
            }
            finally
            {
                connection.Close();
            }
            return JsonConvert.SerializeObject(new
            {
                error = error,
                message = message
            });
        }
    }
}