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
            string formattedDate = currentDate.ToString("MMM yyyy");
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

                    decimal totalRevenue = 0;

                    object result = cmdSelectTotalRevenue.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        totalRevenue = (Decimal)result;
                    }

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
                    string topBookedProperty = null;
                    object result = cmdTopProperty.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        topBookedProperty = (string)result;
                    }

                    else
                    {
                        topBookedProperty = "None";
                    }

                    lblTopBookedProperty.Text = topBookedProperty;
                }

                string strBookingCountThisMonth = "SELECT COUNT(*) FROM Booking " +
                                                  "WHERE MONTH(BookingTime) = MONTH(GETDATE()) " +
                                                  "AND YEAR(BookingTime) = YEAR(GETDATE()) " +
                                                  "AND LandlordId = @LandlordId";
                using (SqlCommand cmdBookingCountThisMonth = new SqlCommand(strBookingCountThisMonth, con))
                {
                    cmdBookingCountThisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    int bookingCountThisMonth = 0;

                    object result = cmdBookingCountThisMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        bookingCountThisMonth = (int)result;
                    }

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
                    decimal revenueThisMonth = 0;

                    object result = cmdRevenueThisMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        revenueThisMonth = (decimal)result;
                    }

                    lblRevenueThisMonth.Text = revenueThisMonth.ToString("C");
                    lblProfitThisMonth.Text = (revenueThisMonth * 0.94M).ToString("C");
                }

                string strAvgBookingThisMonth = "SELECT ROUND(AVG(P.PaymentAmount),2) as 'Booking Size' " +
                    "FROM Booking B " +
                    "INNER JOIN Payment P ON B.BookingId = P.BookingId " +
                    "WHERE MONTH(B.BookingTime) = MONTH(GETDATE()) AND YEAR(B.BookingTime) = YEAR(GETDATE()) " +
                    "AND B.LandlordId = @LandlordId";
                using (SqlCommand cmdAvgBookinghisMonth = new SqlCommand(strAvgBookingThisMonth, con))
                {
                    cmdAvgBookinghisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    decimal avgBookingThisMonth = 0;

                    object result = cmdAvgBookinghisMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        avgBookingThisMonth = (decimal)result;
                    }

                    lblAvgBooking.Text = avgBookingThisMonth.ToString("C");
                }

                string strCompletionRateThisMonth = "SELECT " +
                    "CASE " +
                    "WHEN COUNT(*) > 0 THEN (SUM(CASE WHEN BookingStatus = 'Completed' THEN 1 ELSE 0 END) * 100.0) / COUNT(*) " +
                    "ELSE 0.0 " +
                    "END AS CompletionRate " +
                    "FROM Booking " +
                    "WHERE LandlordId = @LandlordId AND " +
                    "MONTH(BookingTime) = MONTH(GETDATE()) AND YEAR(BookingTime) = YEAR(GETDATE())";

                using (SqlCommand cmdCompletionRate = new SqlCommand(strCompletionRateThisMonth, con))
                {
                    cmdCompletionRate.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    decimal completeionRateThisMonth = 0;

                    object result = cmdCompletionRate.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        completeionRateThisMonth = (decimal)result;
                    }

                    lblCompletionRate.Text = completeionRateThisMonth.ToString("N2");
                }
            }
        }

    }
}