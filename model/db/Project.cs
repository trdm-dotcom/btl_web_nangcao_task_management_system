﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl_web_nangcao_task_management_system.model.db
{
    [Serializable]
    public class Project
    {
        public Project()
        {
        }

        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public long lead { get; set; }
        public DateTime startDate { get; set; }
        public DateTime estimateDate { get; set; }
        public ProjectStatus status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}