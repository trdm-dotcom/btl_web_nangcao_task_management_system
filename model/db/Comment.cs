using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class Comment
    {
        public Comment()
        {
        }
        public int id { set; get; }
        public int employeeId { set; get; }
        public string employeeName { set; get; }
        public int taskId { set; get; }
        public string content { set; get; }
        public DateTime createdAt { get; set; }
    }
}