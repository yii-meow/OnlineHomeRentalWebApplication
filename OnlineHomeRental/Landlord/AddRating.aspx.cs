using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class AddRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addRating_Click(object sender, EventArgs e)
        {
            string propertyId = lblPropertyId.Text;
            string bookingId = lblBookingId.Text;
            int rating = int.Parse(hdnRating.Value);
            string reviewMessage = tbReviewMessage.Text;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strInsertReview = @"
                    INSERT INTO Review(PropertyId,BookingId,RatingScore,ReviewMessage,ReviewDate) 
                    VALUES (@PropertyId, @BookingId, @RatingScore, @ReviewMessage, @ReviewDate)
                ";

                using (SqlCommand cmdInsertReview = new SqlCommand(strInsertReview, connection))
                {
                    cmdInsertReview.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmdInsertReview.Parameters.AddWithValue("@BookingId", bookingId);
                    cmdInsertReview.Parameters.AddWithValue("@RatingScore", rating);
                    cmdInsertReview.Parameters.AddWithValue("@ReviewMessage", reviewMessage);
                    cmdInsertReview.Parameters.AddWithValue("@ReviewDate", DateTime.Now);

                    cmdInsertReview.ExecuteNonQuery();
                }
            }
        }
    }
}