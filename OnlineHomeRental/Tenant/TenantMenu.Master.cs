using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class TenantMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["TenantId"] == null)
            {
                Response.Redirect("/TenantLoginPage.aspx");
            }
        }
    }
}