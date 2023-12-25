using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class AddProperty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddProperty_Click(object sender, EventArgs e)
        {
            // Get user input
            string propertyName = txtPropertyName.Text;
            string propertyType = ddlPropertyType.SelectedValue;
            string propertyAddress = txtPropertyAddress.Text;
            string listingDescription = txtListingDescription.Text;
            string propertyPrice = txtPropertyPrice.Text;

            // Assuming you have a connection string in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL query to insert a record
            string insertQuery = "INSERT INTO [PropertyListing] (LandlordId, PropertyName, PropertyType, PropertyAddress, ListingDescription, PropertyPrice) " +
                                "VALUES (@LandlordId, @PropertyName, @PropertyType, @PropertyAddress, @ListingDescription, @PropertyPrice)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@LandlordId", 1);
                    command.Parameters.AddWithValue("@PropertyName", propertyName);
                    command.Parameters.AddWithValue("@PropertyType", propertyType);
                    command.Parameters.AddWithValue("@PropertyAddress", propertyAddress);
                    command.Parameters.AddWithValue("@ListingDescription", listingDescription);
                    command.Parameters.AddWithValue("@PropertyPrice", Convert.ToDecimal(propertyPrice));

                    connection.Open();
                    // command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

            // Set the alert message
            alertDiv.InnerText = "Success";

            // Make the alert visible
            alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
        }
    }
}