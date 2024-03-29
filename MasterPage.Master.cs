﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl_web_nangcao_task_management_system.UI
{
    public partial class MasterPage1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Clear();
                Response.Redirect("Login.aspx");
                Response.Close();
            }
        }

        protected void logoutHyperLink_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Clear();
            Response.Redirect("Login.aspx");
            Response.Close();
        }
    }
}