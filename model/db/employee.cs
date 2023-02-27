using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class employee
    {
        public employee() { }
        private int id { get; set; }
        private string pasword { get; set; }
        private string name { get; set; }
        private string email { get; set; }
        private DateTime joiningDate { get; set; }
        private EmployeeRole role { get; set; }
    }
}