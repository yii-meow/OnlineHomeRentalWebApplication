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
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string queryTotalRevenue = "SELECT SUM(P.PaymentAmount) FROM Booking B " +
                                           "INNER JOIN Payment P ON B.BookingId = P.BookingId " +
                                           "WHERE LandlordId = @LandlordId";

                using (SqlCommand cmdSelectTotalRevenue = new SqlCommand(queryTotalRevenue, con))
                {
                    cmdSelectTotalRevenue.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal totalRevenue = Convert.ToDecimal(cmdSelectTotalRevenue.ExecuteScalar());

                    lblTotalRevenue.Text = totalRevenue.ToString("C");
                }

                string strTop3Properties = "SELECT TOP 1 COUNT(*) as BookingCount, B.PropertyId, P.PropertyName " +
                                   "FROM Booking B " +
                                   "INNER JOIN Property P ON B.PropertyId = P.PropertyId " +
                                   "WHERE B.LandlordId = @LandlordId " +
                                   "GROUP BY B.PropertyId, P.PropertyName " +
                                   "ORDER BY BookingCount DESC";
                using (SqlCommand cmdTop3Properties = new SqlCommand(strTop3Properties, con))
                {
                    cmdTop3Properties.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    string topBookedProperty = cmdTop3Properties.ToString();
                    lblTopBookedProperty.Text = topBookedProperty;
                }

                // Query to retrieve the count of bookings for the current month
                string strBookingCountThisMonth = "SELECT COUNT(*) FROM Booking " +
                                                  "WHERE MONTH(BookingTime) = MONTH(GETDATE()) " +
                                                  "AND YEAR(BookingTime) = YEAR(GETDATE()) " +
                                                  "AND LandlordId = @LandlordId";
                using (SqlCommand cmdBookingCountThisMonth = new SqlCommand(strBookingCountThisMonth, con))
                {
                    cmdBookingCountThisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    int bookingCountThisMonth = (int)cmdBookingCountThisMonth.ExecuteScalar();
                    lblBookingThisMonth.Text = bookingCountThisMonth.ToString();
                }
            }
        }

    }
}