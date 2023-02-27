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

        private int Id { get; set; }
        private string title { get; set; }
        private string description { get; set; }
        private DateTime startDate { get; set; }
        private DateTime estimateTime { get; set; }
        private ProjectStatus status { get; set; }
    }
}