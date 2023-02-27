using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class task
    {
        public task()
        {
        }

        private int id { get; set; }
        private string name { get; set; }
        private string description { get; set; }
        private int projectId { get; set; }
        private DateTime startDate { get; set; }
        private DateTime estimatedTime { get; set; }
        private int employee_AssignTo { get; set; }
        private int employee_AssigenBy { get; set; }
        private TaskStatus status { get; set; }
    }
}