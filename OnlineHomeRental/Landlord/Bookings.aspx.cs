using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class BookingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming that BookingStatus is in the first cell (index 3), adjust accordingly
                string bookingStatus = DataBinder.Eval(e.Row.DataItem, "BookingStatus").ToString();

                if (bookingStatus == "Completed")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                }
                else if (bookingStatus == "Cancelled")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}