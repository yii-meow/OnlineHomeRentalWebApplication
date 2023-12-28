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
            DateTime currentDate = DateTime.Now;

            // Format the date as "MMM yyyy" (e.g., "Dec 2023")
            string formattedDate = currentDate.ToString("MMM yyyy");

            // Set the formatted date to the Literal control
            lblMonth.Text = formattedDate;

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

                string strTop3Properties = "SELECT TOP 1 P.PropertyName " +
                                   "FROM Booking B " +
                                   "INNER JOIN Property P ON B.PropertyId = P.PropertyId " +
                                   "WHERE B.LandlordId = @LandlordId " +
                                   "GROUP BY B.PropertyId, P.PropertyName " +
                                   "ORDER BY COUNT(*) DESC";
                using (SqlCommand cmdTopProperty = new SqlCommand(strTop3Properties, con))
                {
                    cmdTopProperty.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    string topBookedProperty = cmdTopProperty.ExecuteScalar().ToString();
                    lblTopBookedProperty.Text = topBookedProperty;
                }

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

                string strRevenueThisMonth = "SELECT SUM(Payment.PaymentAmount) " +
                    "FROM BOOKING " +
                    "INNER JOIN Payment ON Booking.BookingId = Payment.BookingId " +
                    "WHERE MONTH(BookingTime) = MONTH(GETDATE()) AND YEAR(BookingTime) = YEAR(GETDATE()) " +
                    "AND Booking.LandlordId = @LandlordId";
                using (SqlCommand cmdRevenueThisMonth = new SqlCommand(strRevenueThisMonth, con))
                {
                    cmdRevenueThisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    decimal revenueThisMonth = (decimal)cmdRevenueThisMonth.ExecuteScalar();
                    lblRevenueThisMonth.Text = revenueThisMonth.ToString("C");
                    lblProfitThisMonth.Text = (revenueThisMonth * 0.94M).ToString("C");
                }

                string strAvgBookingThisMonth = "SELECT ROUND(AVG(P.PaymentAmount),2) as 'Booking Size' " +
                    "FROM Booking B " +
                    "INNER JOIN Payment P ON B.BookingId = P.BookingId " +
                    "WHERE MONTH(B.BookingTime) = MONTH(GETDATE()) AND YEAR(B.BookingTime) = YEAR(GETDATE()) " +
                    "AND B.LandlordId = 1";
                using (SqlCommand cmdAvgBookinghisMonth = new SqlCommand(strAvgBookingThisMonth, con))
                {
                    cmdAvgBookinghisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    decimal avgBookingThisMonth = (decimal)cmdAvgBookinghisMonth.ExecuteScalar();
                    lblAvgBooking.Text = avgBookingThisMonth.ToString("C");
                }
            }
        }

    }
}