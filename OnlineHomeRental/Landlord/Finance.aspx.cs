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
    public partial class Finance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now.AddMonths(-1);
            string formattedDate = currentDate.ToString("MMM yyyy");
            lblMonthRecord.Text = formattedDate;

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strTotalRevenue = @"DECLARE @LastMonth DATE = DATEADD(MONTH, -1, GETDATE());
                                 DECLARE @LastLastMonth DATE = DATEADD(MONTH, -1, @LastMonth);

                                 SELECT 
                                    SUM(CASE WHEN MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth) THEN P.PaymentAmount ELSE 0 END) AS LastMonthSales,
                                    SUM(CASE WHEN MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth) THEN P.PaymentAmount ELSE 0 END) AS LastLastMonthSales
                                 FROM 
                                    Booking B 
                                    INNER JOIN Payment P ON B.BookingId = P.BookingId 
                                 WHERE 
                                    LandlordId = @LandlordId 
                                    AND (
                                        (MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth))
                                        OR
                                        (MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth))
                                    );";

                using (SqlCommand cmdRevenueThisMonth = new SqlCommand(strTotalRevenue, con))
                {
                    cmdRevenueThisMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal lastMonthSales = 0;
                    decimal lastLastMonthSales = 0;

                    using (SqlDataReader reader = cmdRevenueThisMonth.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastMonthSales = ((decimal)reader["LastMonthSales"]);
                            lastLastMonthSales = ((decimal)reader["LastLastMonthSales"]);
                        }
                    }

                    lblTotalSales.Text = lastMonthSales.ToString("C");
                    lblTotalSalesDif.Text = "";

                    if (lastMonthSales == lastLastMonthSales)
                    {
                        lblTotalSalesDif.Text = "=";
                    }
                    else
                    {
                        if (lastMonthSales > lastLastMonthSales)
                        {
                            lblTotalSalesDif.ForeColor = System.Drawing.Color.Green;
                            lblTotalSalesDif.Text = "+";
                        }
                        else
                        {
                            lblTotalSalesDif.ForeColor = System.Drawing.Color.Red;
                        }
                        lblTotalSalesDif.Text += (lastMonthSales - lastLastMonthSales).ToString("C");
                    }
                }
            }
        }
    }
}