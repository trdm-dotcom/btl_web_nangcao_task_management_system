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
        private int id { set; get; }
        private int commentEmployeeId { set; get; }
        private string commentEmployeeName { set; get; }
        private int taskId { set; get; }
        private string comments { set; get; }
        private string commentAttachment { set; get; }
        private DateTime commentDate { set; get; }
    }
}