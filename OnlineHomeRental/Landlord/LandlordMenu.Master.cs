using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class LandlordMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["LandlordId"] != null)
            {
                LandlordUsername.Text = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("/LandlordLoginPage.aspx");
            }

            // Highlight navigation bar on the current page
            if (Request.Url.AbsolutePath.EndsWith("Homepage.aspx"))
            {
                homeLinkLi.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Property.aspx") || Request.Url.AbsolutePath.EndsWith("PropertyDetails.aspx"))
            {
                propertyLinkLi.Attributes["class"] = "submenu nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Bookings.aspx"))
            {
                bookingLinkLi.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Notification.aspx"))
            {
                notificationLinkLi.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("LandlordChat.aspx"))
            {
                chatLinkLi.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Finance.aspx"))
            {
                financeLinkLi.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("AccountSetting.aspx"))
            {
                accountLinkLi.Attributes["class"] = "nav-item active";
            }
        }
        protected void ExitLink_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null; // Clear the session
            Response.Redirect("/LandlordLoginPage.aspx");
        }
    }
}