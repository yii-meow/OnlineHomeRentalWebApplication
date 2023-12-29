using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            if (!IsPostBack)
            {
                BindCanvasData();
            }

            DateTime lastMonthDate = DateTime.Now.AddMonths(-1);
            DateTime lastLastMonthDate = DateTime.Now.AddMonths(-2);

            string formattedDate = lastMonthDate.ToString("MMM yyyy");
            lblMonthRecord.Text = formattedDate;

            string formattedDate2 = lastLastMonthDate.ToString("MMM yyyy");
            lblLastMonthRecord.Text = formattedDate2;

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strTotalRevenue = @"
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

                using (SqlCommand cmdRevenue = new SqlCommand(strTotalRevenue, con))
                {
                    cmdRevenue.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdRevenue.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdRevenue.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal lastMonthSales = 0;
                    decimal lastLastMonthSales = 0;

                    using (SqlDataReader reader = cmdRevenue.ExecuteReader())
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

                string strTotalBooking = @"
                        SELECT 
                        COUNT(CASE WHEN MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth) THEN 1 END) AS LastMonthBookingCount,
                        COUNT(CASE WHEN MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth) THEN 1 END) AS LastLastMonthBookingCount
                    FROM 
                        Booking
                    WHERE 
                        LandlordId = @LandlordId 
                        AND (
                            (MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth))
                            OR
                            (MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth))
                        )
                ";

                using (SqlCommand cmdTotalBooking = new SqlCommand(strTotalBooking, con))
                {
                    cmdTotalBooking.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdTotalBooking.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdTotalBooking.Parameters.AddWithValue("@LandlordId", Session["LandlordId"]); // Replace with the actual LandlordId

                    int lastMonthBookingCount = 0;
                    int lastLastMonthBookingCount = 0;

                    using (SqlDataReader reader = cmdTotalBooking.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastMonthBookingCount = Convert.ToInt32(reader["LastMonthBookingCount"]);
                            lastLastMonthBookingCount = Convert.ToInt32(reader["LastLastMonthBookingCount"]);
                        }
                    }

                    lblTotalBooking.Text = lastMonthBookingCount.ToString();
                    lblTotalBookingDif.Text = "";

                    if (lastMonthBookingCount == lastLastMonthBookingCount)
                    {
                        lblTotalBookingDif.Text = "=";
                    }
                    else
                    {
                        if (lastMonthBookingCount > lastLastMonthBookingCount)
                        {
                            lblTotalBookingDif.ForeColor = System.Drawing.Color.Green;
                            lblTotalBookingDif.Text = "+";
                        }
                        else
                        {
                            lblTotalBookingDif.ForeColor = System.Drawing.Color.Red;
                        }
                        lblTotalBookingDif.Text += (lastMonthBookingCount - lastLastMonthBookingCount).ToString();
                    }
                }

                string strNetProfit = @"
                    SELECT 
                        SUM(CASE WHEN MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth) THEN PaymentAmount * 0.94 ELSE 0 END) AS LastMonthNetProfit,
                        SUM(CASE WHEN MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth) THEN PaymentAmount * 0.94 ELSE 0 END) AS LastLastMonthNetProfit
                    FROM 
                        Booking
                        INNER JOIN Payment ON Booking.BookingId = Payment.BookingId
                    WHERE 
                        Booking.LandlordId = @LandlordId 
                        AND Payment.PaymentStatus = 'Successful'
                        AND (
                            (MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth))
                            OR
                            (MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth))
                        )
                ";

                using (SqlCommand cmdNetProfit = new SqlCommand(strNetProfit, con))
                {
                    cmdNetProfit.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdNetProfit.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdNetProfit.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal lastMonthNetProfit = 0;
                    decimal lastLastMonthNetProfit = 0;

                    using (SqlDataReader reader = cmdNetProfit.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastMonthNetProfit = ((decimal)reader["LastMonthNetProfit"]);
                            lastLastMonthNetProfit = ((decimal)reader["LastLastMonthNetProfit"]);
                        }
                    }

                    lblNetProfit.Text = lastMonthNetProfit.ToString("C");
                    lblNetProfitDif.Text = "";

                    if (lastMonthNetProfit == lastLastMonthNetProfit)
                    {
                        lblNetProfitDif.Text = "=";
                    }
                    else
                    {
                        if (lastMonthNetProfit > lastLastMonthNetProfit)
                        {
                            lblNetProfitDif.ForeColor = System.Drawing.Color.Green;
                            lblNetProfitDif.Text = "+";
                        }
                        else
                        {
                            lblNetProfitDif.ForeColor = System.Drawing.Color.Red;
                        }
                        lblNetProfitDif.Text += (lastMonthNetProfit - lastLastMonthNetProfit).ToString("C");
                    }
                }

                string strCancelledBooking = @"
                    SELECT 
                        COUNT(CASE WHEN MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth) THEN 1 ELSE NULL END) AS LastMonthCancelledBookings,
                        COUNT(CASE WHEN MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth) THEN 1 ELSE NULL END) AS LastLastMonthCancelledBookings
                    FROM 
                        Booking
                    WHERE 
                        Booking.LandlordId = @LandlordId 
                        AND Booking.BookingStatus = 'Cancelled'
                        AND (
                            (MONTH(BookingTime) = MONTH(@LastMonth) AND YEAR(BookingTime) = YEAR(@LastMonth))
                            OR
                            (MONTH(BookingTime) = MONTH(@LastLastMonth) AND YEAR(BookingTime) = YEAR(@LastLastMonth))
                        )
                ";

                using (SqlCommand cmdCancelledBooking = new SqlCommand(strCancelledBooking, con))
                {
                    cmdCancelledBooking.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdCancelledBooking.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdCancelledBooking.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    int lastMonthCancelledBooking = 0;
                    int lastLastMonthCancelledBooking = 0;

                    using (SqlDataReader reader = cmdCancelledBooking.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastMonthCancelledBooking = ((int)reader["LastMonthCancelledBookings"]);
                            lastLastMonthCancelledBooking = ((int)reader["LastLastMonthCancelledBookings"]);
                        }
                    }

                    lblCancelledBooking.Text = lastMonthCancelledBooking.ToString();
                    lblCancelledBookingDif.Text = "";

                    if (lastMonthCancelledBooking == lastLastMonthCancelledBooking)
                    {
                        lblCancelledBookingDif.Text = "=";
                    }
                    else
                    {
                        if (lastMonthCancelledBooking > lastLastMonthCancelledBooking)
                        {
                            lblCancelledBookingDif.ForeColor = System.Drawing.Color.Green;
                            lblCancelledBookingDif.Text = "+";
                        }
                        else
                        {
                            lblNetProfitDif.ForeColor = System.Drawing.Color.Red;
                        }
                        lblCancelledBookingDif.Text += (lastMonthCancelledBooking - lastLastMonthCancelledBooking).ToString();
                    }
                }

                // Last month's unused property and last last month

                string strUnusedPropertyDurationLastMonth = "WITH CTE AS (" +
                    "SELECT PropertyId, " +
                    "SUM(Duration) AS UsedDays " +
                    "FROM dbo.Booking " +
                    "WHERE MONTH(CheckInDate) = MONTH(@LastMonth) AND YEAR(CheckInDate) = YEAR(@LastMonth) " +
                    "GROUP BY PropertyId " +
                    ") " +
                    "SELECT SUM(DAY(EOMONTH(@LastMonth)) - COALESCE(CTE.UsedDays, 0)) AS TotalUnusedDays " +
                    "FROM dbo.Property P " +
                    "LEFT JOIN CTE ON P.PropertyId = CTE.PropertyId " +
                    "WHERE P.LandlordId = @LandlordId";

                string strUnusedPropertyDurationLastLastMonth = "WITH CTE AS (" +
                    "SELECT PropertyId, " +
                    "SUM(Duration) AS UsedDays " +
                    "FROM dbo.Booking " +
                    "WHERE MONTH(CheckInDate) = MONTH(@LastLastMonth) AND YEAR(CheckInDate) = YEAR(@LastLastMonth) " +
                    "GROUP BY PropertyId " +
                    ") " +
                    "SELECT SUM(DAY(EOMONTH(@LastLastMonth)) - COALESCE(CTE.UsedDays, 0)) AS TotalUnusedDays " +
                    "FROM dbo.Property P " +
                    "LEFT JOIN CTE ON P.PropertyId = CTE.PropertyId " +
                    "WHERE P.LandlordId = @LandlordId";

                int lastMonthUnusedPropertyDuration = 0;
                int lastLastMonthunusedPropertyDuration = 0;

                using (SqlCommand cmdUnusedPropertyDurationLastMonth = new SqlCommand(strUnusedPropertyDurationLastMonth, con))
                {
                    cmdUnusedPropertyDurationLastMonth.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdUnusedPropertyDurationLastMonth.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdUnusedPropertyDurationLastMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    object result = cmdUnusedPropertyDurationLastMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        lastMonthUnusedPropertyDuration = (int)result;
                    }
                }

                lblUnusedDuration.Text = lastMonthUnusedPropertyDuration.ToString();

                using (SqlCommand cmdUnusedPropertyDurationLastLastMonth = new SqlCommand(strUnusedPropertyDurationLastLastMonth, con))
                {
                    cmdUnusedPropertyDurationLastLastMonth.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdUnusedPropertyDurationLastLastMonth.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdUnusedPropertyDurationLastLastMonth.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    object result = cmdUnusedPropertyDurationLastLastMonth.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        lastLastMonthunusedPropertyDuration = (int)result;
                    }
                }

                lblUnusedDurationDif.Text = "";

                if (lastMonthUnusedPropertyDuration == lastLastMonthunusedPropertyDuration)
                {
                    lblUnusedDurationDif.Text = "=";
                }
                else
                {
                    if (lastMonthUnusedPropertyDuration > lastLastMonthunusedPropertyDuration)
                    {
                        lblUnusedDurationDif.ForeColor = System.Drawing.Color.Red;
                        lblUnusedDurationDif.Text = "+";
                    }
                    else
                    {
                        lblUnusedDurationDif.ForeColor = System.Drawing.Color.Green;
                    }
                    lblUnusedDurationDif.Text += (lastMonthUnusedPropertyDuration - lastLastMonthunusedPropertyDuration).ToString();
                }

                string strAverageBookingPrice = @"
                    SELECT 
                    ROUND(
                        AVG(
                            CASE 
                                WHEN MONTH(B.BookingTime) = MONTH(@LastMonth) AND YEAR(B.BookingTime) = YEAR(@LastMonth) THEN P.PaymentAmount 
                                ELSE 0
                            END
                        ), 2
                    ) AS LastMonthAverageBookingPrice,
                    ROUND(
                        AVG(
                            CASE 
                                WHEN MONTH(B.BookingTime) = MONTH(@LastLastMonth) AND YEAR(B.BookingTime) = YEAR(@LastLastMonth) THEN P.PaymentAmount 
                                ELSE 0 
                            END
                        ), 2
                    ) AS LastLastMonthAverageBookingPrice
                FROM 
                    Booking B 
                    INNER JOIN Payment P ON B.BookingId = P.BookingId 
                WHERE 
                    B.LandlordId = @LandlordId 
                    AND B.BookingStatus = 'Completed'
                    AND (
                        (MONTH(B.BookingTime) = MONTH(@LastMonth) AND YEAR(B.BookingTime) = YEAR(@LastMonth))
                        OR
                        (MONTH(B.BookingTime) = MONTH(@LastLastMonth) AND YEAR(B.BookingTime) = YEAR(@LastLastMonth))
                    )
                ";

                using (SqlCommand cmdAverageBookingPrice = new SqlCommand(strAverageBookingPrice, con))
                {
                    cmdAverageBookingPrice.Parameters.AddWithValue("@LastMonth", lastMonthDate);
                    cmdAverageBookingPrice.Parameters.AddWithValue("@LastLastMonth", lastLastMonthDate);
                    cmdAverageBookingPrice.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    decimal lastMonthAverageBookingPrice = 0;
                    decimal lastLastMonthAverageBookingPrice = 0;

                    using (SqlDataReader reader = cmdAverageBookingPrice.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastMonthAverageBookingPrice = ((decimal)reader["LastMonthAverageBookingPrice"]);
                            lastLastMonthAverageBookingPrice = ((decimal)reader["LastLastMonthAverageBookingPrice"]);
                        }
                    }

                    lblAvgBooking.Text = lastMonthAverageBookingPrice.ToString("C");
                    lblAvgBookingDif.Text = "";

                    if (lastMonthAverageBookingPrice == lastLastMonthAverageBookingPrice)
                    {
                        lblAvgBookingDif.Text = "=";
                    }
                    else
                    {
                        if (lastMonthAverageBookingPrice > lastLastMonthAverageBookingPrice)
                        {
                            lblAvgBookingDif.ForeColor = System.Drawing.Color.Green;
                            lblAvgBookingDif.Text = "+";
                        }
                        else
                        {
                            lblAvgBookingDif.ForeColor = System.Drawing.Color.Red;
                        }
                        lblAvgBookingDif.Text += (lastMonthAverageBookingPrice - lastLastMonthAverageBookingPrice).ToString("C");
                    }
                }
            }
        }
        protected string GetPaymentStatusStyle(string paymentStatus)
        {
            if (paymentStatus.Equals("Successful", StringComparison.OrdinalIgnoreCase))
            {
                return "color: green;";
            }
            else
            {
                return "color: red;";
            }
        }

        private void BindCanvasData()
        {
            // Replace this connection string with your actual connection string
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                string strRevenue = @"
                    WITH Last6Months AS (
                        SELECT DISTINCT
                            MONTH(DATEADD(MONTH, -Number - 1, GETDATE())) AS MonthNumber
                        FROM master.dbo.spt_values
                        WHERE Type = 'P' AND Number BETWEEN 0 AND 5
                    )

                    SELECT 
                        l.MonthNumber AS BookingMonth,
                        COALESCE(YEAR(b.BookingTime), YEAR(GETDATE())) AS BookingYear,
                        COALESCE(SUM(p.PaymentAmount), 0) AS TotalRevenue
                    FROM Last6Months l
                    LEFT JOIN Booking b ON l.MonthNumber = MONTH(b.BookingTime) AND b.LandlordId = 1
                    LEFT JOIN Payment p ON b.BookingId = p.BookingId
                    GROUP BY l.MonthNumber, COALESCE(YEAR(b.BookingTime), YEAR(GETDATE()))
                    ORDER BY COALESCE(YEAR(b.BookingTime), YEAR(GETDATE())), l.MonthNumber;
                ";

                using (SqlCommand command = new SqlCommand(strRevenue, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert DataTable to JSON for use in JavaScript
                    string jsonBookingData = ConvertDataTableToJson(dataTable);

                    // Inject the JSON data into the JavaScript block
                    ScriptManager.RegisterStartupScript(this, GetType(), "DrawCanvas", $"DrawCanvas({jsonBookingData});", true);
                }
            }
        }
        private string ConvertDataTableToJson(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> rows = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            System.Collections.Generic.Dictionary<string, object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }
    }
}