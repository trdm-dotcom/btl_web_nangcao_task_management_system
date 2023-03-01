using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class EmployeeProject
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public int projectInd { get; set; }
        public string projectName { get; set; }
    }
}