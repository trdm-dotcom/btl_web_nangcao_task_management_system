using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.UI
{
    public partial class ProjectCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in selectedEmployeeListBox.Items)
            {
                string employeeIDs = item.Value + ",";
            }
        }

        protected void singleAddButton_Click(object sender, EventArgs e)
        {

        }

        protected void allAddButton_Click(object sender, EventArgs e)
        {

        }

        protected void singleRemoveButton_Click(object sender, EventArgs e)
        {

        }

        protected void allRemoveButton_Click(object sender, EventArgs e)
        {

        }
    }
}