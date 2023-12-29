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
                string strTotalRevenue = "SELECT SUM(P.PaymentAmount) FROM Booking B " +
                                           "INNER JOIN Payment P ON B.BookingId = P.BookingId " +
                                           "WHERE LandlordId = @LandlordId";

                using (SqlCommand cmdTotalRevenue = new SqlCommand(strTotalRevenue, con))
                {
                    cmdTotalRevenue.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal totalRevenue = 0;

                    object result = cmdTotalRevenue.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        totalRevenue = (Decimal)result;
                    }

                    lblTotalRevenue.Text = totalRevenue.ToString("C");
                }

                string strTopPropery = "SELECT TOP 1 P.PropertyName " +
                                   "FROM Booking B " +
                                   "INNER JOIN Property P ON B.PropertyId = P.PropertyId " +
                                   "WHERE B.LandlordId = @LandlordId " +
                                   "GROUP BY B.PropertyId, P.PropertyName " +
                                   "ORDER BY COUNT(*) DESC";
                using (SqlCommand cmdTopProperty = new SqlCommand(strTopPropery, con))
                {
                    cmdTopProperty.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    string topBookedProperty = "None";
                    object result = cmdTopProperty.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    { 
                        topBookedProperty = (string)result;
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
                    "AND Booking.LandlordId = @LandlordId AND Payment.PaymentStatus='Successful'";
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
                    "AND B.LandlordId = @LandlordId AND B.BookingStatus = 'Completed'";
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

                string strUnusedPropertyDurationThisMonth = "WITH CTE AS (" +
                    "SELECT PropertyId, " +
                    "SUM(Duration) AS UsedDays " +
                    "FROM dbo.Booking " +
                    "WHERE MONTH(CheckInDate) = MONTH(GETDATE()) AND YEAR(CheckInDate) = YEAR(GETDATE()) " +
                    "GROUP BY PropertyId " +
                    ") " +
                    "SELECT SUM(DAY(EOMONTH(GETDATE())) - COALESCE(CTE.UsedDays, 0)) AS TotalUnusedDays " +
                    "FROM dbo.Property P " +
                    "LEFT JOIN CTE ON P.PropertyId = CTE.PropertyId " +
                    "WHERE P.LandlordId = @LandlordId";

                using (SqlCommand cmdUnusedPropertyDurationThisMonth = new SqlCommand(strUnusedPropertyDurationThisMonth, con))
                {
                    cmdUnusedPropertyDurationThisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    int UnusedPropertyDurationThisMonth = 0;

                    object result = cmdUnusedPropertyDurationThisMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        UnusedPropertyDurationThisMonth = (int)result;
                    }

                    lblUnusedDuration.Text = UnusedPropertyDurationThisMonth.ToString();
                }

                string strPotentialLoss = "WITH CTE AS ( " +
                    "SELECT PropertyId, " +
                    "SUM(Duration) AS UsedDays " +
                    "FROM dbo.Booking " +
                    "WHERE MONTH(CheckInDate) = MONTH(GETDATE()) AND YEAR(CheckInDate) = YEAR(GETDATE()) " +
                    "GROUP BY PropertyId " +
                    ") " +
                    "SELECT SUM(P.PropertyPrice * (DAY(EOMONTH(GETDATE())) - COALESCE(CTE.UsedDays, 0))) AS TotalPotentialLoss " +
                    "FROM dbo.Property P " +
                    "LEFT JOIN CTE ON P.PropertyId = CTE.PropertyId " +
                    "WHERE P.LandlordId = @LandlordId;";

                using (SqlCommand cmdPotentialLoss = new SqlCommand(strPotentialLoss, con))
                {
                    cmdPotentialLoss.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    double potentialLoss = 0;

                    object result = cmdPotentialLoss.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        // Use Convert.ToDouble instead of double.Parse
                        potentialLoss = Convert.ToDouble(result);
                    }

                    lblTotalPotentialLoss.Text = potentialLoss.ToString("C");
                }

                string strTop3Properties = "SELECT TOP 3 P.PropertyName " +
                                   "FROM Booking B " +
                                   "INNER JOIN Property P ON B.PropertyId = P.PropertyId " +
                                   "WHERE B.LandlordId = @LandlordId " +
                                   "GROUP BY B.PropertyId, P.PropertyName " +
                                   "ORDER BY COUNT(*) DESC";

                string top3Propeties = "";

                using (SqlCommand cmdTop3Properties = new SqlCommand(strTop3Properties, con))
                {
                    cmdTop3Properties.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    using (SqlDataReader reader = cmdTop3Properties.ExecuteReader())
                    {
                        int ranking = 1;
                        // Check if there are rows returned
                        if (reader.HasRows)
                        {
                            // Loop through the result set
                            while (reader.Read())
                            {
                                // Access the PropertyName column from the result set
                                string propertyName = reader["PropertyName"].ToString();

                                // Concatenate property names to form the top 3 list
                                top3Propeties += $"{ranking}. {propertyName}<br/><br/>";
                                ranking++;
                            }

                            // Display the top 3 properties in the label
                            lblTop3BookedProperties.Text = top3Propeties;
                        }
                        else
                        {
                            // Handle the case when there are no rows
                            lblTop3BookedProperties.Text = "No booked properties found.";
                        }
                    }
                }
            }
        }
    }
}