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
        }
    }
}