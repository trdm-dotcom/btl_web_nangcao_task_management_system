using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class Task
    {
        public Task()
        {
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int projectId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime estimatedTime { get; set; }
        public int employeeAssignee { get; set; }
        public int employeeReporter { get; set; }
        public int employeeQA { get; set; }
        public TaskStatus status { get; set; }
        public TaskPriority priority { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}