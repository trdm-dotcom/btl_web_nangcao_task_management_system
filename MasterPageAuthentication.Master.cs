using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.page.authentication
{
    public partial class MasterPageAuthentication : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["user"] != null)
            {
                Response.Clear();
                Response.Redirect("~/page/Home.aspx");
                Response.Close();
            }
        }

        public Label LabelHeaderMasterPageAuthentication
        {
            get
            {
                return this.labelHeader;
            }
        }
    }
}