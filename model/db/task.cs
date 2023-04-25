using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    [Serializable]
    public class Task
    {
        public Task()
        {
        }

        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long projectId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime estimateDate { get; set; }
        public long employeeAssignee { get; set; }
        public long employeeReporter { get; set; }
        public long employeeQA { get; set; }
        public TaskStatus status { get; set; }
        public TaskPriority priority { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}