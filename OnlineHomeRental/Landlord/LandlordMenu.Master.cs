using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class LandlordMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                LandlordUsername.Text = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("/LandlordLoginPage.aspx");
            }
        }
        protected void ExitLink_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null; // Clear the session
            Response.Redirect("/LandlordLoginPage.aspx");
        }
    }
}