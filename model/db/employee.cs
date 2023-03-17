using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class Employee
    {
        public Employee() { }
        public int id { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public EmployeeRole role { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}