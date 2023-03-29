using btl_web_nangcao_task_management_system.model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.dto
{
    public class TaskDto : Task
    {
        public string nameEmployeeAssignee { get; set; }
        public string nameEmployeeReporter { get; set; }
        public string nameEmployeeQA { get; set; }
    }
}