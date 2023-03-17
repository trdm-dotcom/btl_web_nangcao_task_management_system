using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    public class Project
    {
        public Project()
        {
        }

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int lead { get; set; }
        public DateTime startDate { get; set; }
        public DateTime estimateDate { get; set; }
        public ProjectStatus status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}