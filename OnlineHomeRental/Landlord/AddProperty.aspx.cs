using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
            if (!IsPostBack)
            {
                for (int i = 1; i <= 10; i++)
                {
                    ddlNumberOfBedroom.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }

        protected void btnAddProperty_Click(object sender, EventArgs e)
        {
            // Get user input
            int landlordId = 0;
            string propertyName = txtPropertyName.Text;
            string propertyType = ddlPropertyType.SelectedValue;
            string propertyAddress = txtPropertyAddress.Text;
            string listingDescription = txtListingDescription.Text;
            Decimal propertyPrice = Convert.ToDecimal(txtPropertyPrice.Text);
            int numberOfBedroom = int.Parse(ddlNumberOfBedroom.Text);
            Decimal areaSqft = Convert.ToDecimal(txtAreaSqft.Text);
            bool airCondAvailability = cbAirConditioning.Checked;
            bool waterHeaterAvailability = cbWaterHeater.Checked;
            bool wifiAvailability = cbWiFi.Checked;
            string preferences = txtPreferences.Text;

            string thumbnail = Path.GetFileName(thumbnailUpload.FileName);
            thumbnailUpload.SaveAs(Server.MapPath("/Data/" + thumbnail));

            string image1 = Path.GetFileName(image1Upload.FileName);
            image1Upload.SaveAs(Server.MapPath("/Data/" + image1));

            string image2 = null;
            string image3 = null;

            if (image2Upload.HasFile)
            {
                image2 = Path.GetFileName(image2Upload.FileName);
                image2Upload.SaveAs(Server.MapPath("/Data/" + image2));
            }

            if (image3Upload.HasFile)
            {
                image3 = Path.GetFileName(image3Upload.FileName);
                image3Upload.SaveAs(Server.MapPath("/Data/" + image3));
            }

            // Assuming you have a connection string in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL query to insert a record
            string insertQuery = "INSERT INTO [Property] (LandlordId, PropertyName, PropertyType, PropertyAddress, ListingDescription, PropertyPrice, NumberOfBedroom, AreaSqft, AirCondAvailability, WaterHeaterAvailability, WifiAvailability, Preferences, Thumbnail, Image1, Image2, Image3) " +
                                "VALUES (@LandlordId, @PropertyName, @PropertyType, @PropertyAddress, @ListingDescription, @PropertyPrice, @NumberOfBedroom, @AreaSqft, @AirCondAvailability, @WaterHeaterAvailability, @WifiAvailability, @Preferences, @Thumbnail, @Image1, @Image2, @Image3)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // retrieve LandlordId for inserting record

                using (SqlCommand cmdSelect = new SqlCommand("SELECT LandlordId FROM Landlord WHERE UserId = @UserId", connection))
                {
                    cmdSelect.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    landlordId = (int)cmdSelect.ExecuteScalar();
                }

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@LandlordId", landlordId);
                    command.Parameters.AddWithValue("@PropertyName", propertyName);
                    command.Parameters.AddWithValue("@PropertyType", propertyType);
                    command.Parameters.AddWithValue("@PropertyAddress", propertyAddress);
                    command.Parameters.AddWithValue("@ListingDescription", listingDescription);
                    command.Parameters.AddWithValue("@PropertyPrice", propertyPrice);
                    command.Parameters.AddWithValue("@NumberOfBedroom", numberOfBedroom);
                    command.Parameters.AddWithValue("@AreaSqft", areaSqft);
                    command.Parameters.AddWithValue("@AirCondAvailability", airCondAvailability);
                    command.Parameters.AddWithValue("@WaterHeaterAvailability", waterHeaterAvailability);
                    command.Parameters.AddWithValue("@WifiAvailability", wifiAvailability);
                    command.Parameters.AddWithValue("@Preferences", preferences);
                    command.Parameters.AddWithValue("@Thumbnail", $"/Data/{thumbnail}");
                    command.Parameters.AddWithValue("@Image1", $"/Data/{image1}");
                    command.Parameters.AddWithValue("@Image2", $"/Data/{image2}");
                    command.Parameters.AddWithValue("@Image3", $"/Data/{image3}");

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

            // Set the alert message
            alertDiv.InnerHtml = "Successfully Added Property <b>" + propertyName + "</b> !";

            // Make the alert visible
            alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
        }
    }
}