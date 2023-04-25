using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btl_web_nangcao_task_management_system.model.db;

namespace btl_web_nangcao_task_management_system.model.dto
{
    [Serializable]
    public class ProjectDto : Project
    {
        public ProjectDto() { }
        public string leadName { get; set; }
    }
}