using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class Finance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now.AddMonths(-1);
            string formattedDate = currentDate.ToString("MMM yyyy");
            lblMonthRecord.Text = formattedDate;
        }
    }
}