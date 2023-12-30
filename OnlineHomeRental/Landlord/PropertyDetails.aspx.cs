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
    public partial class PropertyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set Client side Javascript confirmation dialogue for deleting property
            if (!IsPostBack)
            {
                for (int i = 1; i <= 10; i++)
                {
                    ddlNumberOfBedroom.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                HiddenField1.Value = "Are you sure you want to delete?";
            }

            // Get Average Ratings of the property
            string propertyId = "";

            if (Request.QueryString["PropertyId"] != null)
            {
                // Get the TenantId from the query string
                propertyId = Request.QueryString["PropertyId"];
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                decimal averageRatings = 0;
                string strGetAverageRatings = @"
                    SELECT AVG(CAST(RatingScore AS DECIMAL(10, 2))) AS AverageRating
                    FROM Review
                    WHERE PropertyId = @PropertyId
                ";

                using (SqlCommand cmdGetAverageRatings = new SqlCommand(strGetAverageRatings, connection))
                {
                    cmdGetAverageRatings.Parameters.AddWithValue("@PropertyId", propertyId);

                    object result = cmdGetAverageRatings.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        averageRatings = (decimal)result; 
                    }
                    else
                    {
                        lblAverageRatings.Text = "None";
                        return;
                    }
                }
                lblAverageRatings.Text = averageRatings.ToString("N2") + " <i class=\"bi bi-star-fill\"></i>";
            }
        }

        protected void btnSavePropertyDetails_Click(object sender, EventArgs e)
        {
            string propertyId = "";

            if (Request.QueryString["PropertyId"] != null)
            {
                // Get the TenantId from the query string
                propertyId = Request.QueryString["PropertyId"];
            }

            string propertyName = tbPropertyName.Text;
            string propertyAddress = tbPropertyAddress.Text;
            string propertyType = ddlPropertyType.SelectedValue;
            string propertyPrice = tbPropertyPrice.Text;
            string listingDescription = tbListingDescription.Text;
            string preferences = tbPreferences.Text;
            string numberOfBedroom = ddlNumberOfBedroom.SelectedValue;
            string areaSqft = tbAreaSqft.Text;
            bool airCondAvailability = chkAirCondAvailability.Checked;
            bool waterHeaterAvailability = chkWaterHeaterAvailability.Checked;
            bool wifiAvailability = chkWifiAvailability.Checked;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strUpdatePropertyDetails = @"
                    UPDATE [Property]
                    SET PropertyName = @PropertyName, PropertyAddress = @PropertyAddress, PropertyType = @PropertyType, PropertyPrice = @PropertyPrice,
                    ListingDescription = @ListingDescription, Preferences = @Preferences, NumberOfBedroom = @NumberOfBedroom, AreaSqft = @AreaSqft,
                    AirCondAvailability = @AirCondAvailability, WaterHeaterAvailability = @WaterHeaterAvailability, WifiAvailability = @WifiAvailability
                    WHERE PropertyId = @PropertyId
                ";

                using (SqlCommand cmdUpdatePropertyDetails = new SqlCommand(strUpdatePropertyDetails, connection))
                {
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@PropertyName", propertyName);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@PropertyAddress", propertyAddress);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@PropertyType", propertyType);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@PropertyPrice", propertyPrice);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@ListingDescription", listingDescription);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@Preferences", preferences);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@NumberOfBedroom", numberOfBedroom);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@AreaSqft", areaSqft);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@AirCondAvailability", airCondAvailability);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@WaterHeaterAvailability", waterHeaterAvailability);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@WifiAvailability", wifiAvailability);

                    int rowsAffected = cmdUpdatePropertyDetails.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        DataList1.DataBind();

                        alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Successfully Edited Property Details !";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                }
            }
        }

        protected void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            string script = "return confirm('Are you sure you want to delete?');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmBox", script, true);

            string propertyId = ((LinkButton)sender).CommandArgument;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strDeleteProperty = @"
                    DELETE FROM Property
                    WHERE PropertyId = @PropertyId
                ";

                using (SqlCommand cmdDeleteProperty = new SqlCommand(strDeleteProperty, connection))
                {
                    cmdDeleteProperty.Parameters.AddWithValue("@PropertyId", propertyId);

                    int rowsAffected = cmdDeleteProperty.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("~/Landlord/ViewProperty.aspx");
                    }
                }
            }
        }

        protected void btnRequestMaintenance_Click(object sender, EventArgs e)
        {
            string propertyId = "";

            if (Request.QueryString["PropertyId"] != null)
            {
                // Get the TenantId from the query string
                propertyId = Request.QueryString["PropertyId"];
            }

            string maintenanceType = ddlMaintenanceType.SelectedValue;
            string maintenanceDescription = tbMaintenanceDescription.Text;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strRequestMaintenance = @"
                    INSERT INTO PropertyMaintenanceRequest(PropertyId,MaintenanceType,Description,Status,DateRequested,Log) 
                    VALUES (@PropertyId,@MaintenanceType,@Description,@Status,@DateRequested,@Log)
                ";

                using (SqlCommand cmdRequestMaintenance = new SqlCommand(strRequestMaintenance, connection))
                {
                    cmdRequestMaintenance.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmdRequestMaintenance.Parameters.AddWithValue("@MaintenanceType", maintenanceType);
                    cmdRequestMaintenance.Parameters.AddWithValue("@Description", maintenanceDescription);
                    cmdRequestMaintenance.Parameters.AddWithValue("@Status", "Pending");
                    cmdRequestMaintenance.Parameters.AddWithValue("@DateRequested", DateTime.Now);
                    cmdRequestMaintenance.Parameters.AddWithValue("@Log", "");

                    int rowsAffected = cmdRequestMaintenance.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        DataList1.DataBind();

                        alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Successfully Created Maintenance Request !";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                }
            }
        }
        protected string ConvertToStars(object ratingScoreObj)
        {
            int ratingScore = 0;
            if (ratingScoreObj != null)
            {
                ratingScore = Convert.ToInt32(ratingScoreObj);
            }
            return string.Concat(Enumerable.Repeat("<i class='bi bi-star-fill'></i>", ratingScore));
        }
    }
}