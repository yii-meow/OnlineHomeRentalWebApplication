﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            if (Session["UserId"] == null || Session["LandlordId"] == null)
            {
                Response.Redirect("/LandlordLoginPage.aspx");
            }

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
                lblAverageRatings.Text = averageRatings.ToString("N2") + " <i class=\"bi bi-star-fill text-danger\"></i>";
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

            string thumbnail = null;
            string image1 = null;
            string image2 = null;
            string image3 = null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string strGetImages = "SELECT Thumbnail, Image1, Image2, Image3 FROM Property WHERE PropertyId = @PropertyId";

                using (SqlCommand cmdGetImages = new SqlCommand(strGetImages, connection))
                {
                    // Assuming you have the PropertyId value stored in a variable named propertyId
                    cmdGetImages.Parameters.AddWithValue("@PropertyId", propertyId);

                    connection.Open();
                    using (SqlDataReader reader = cmdGetImages.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve values from the reader and assign them to variables
                            thumbnail = reader["Thumbnail"].ToString();
                            image1 = reader["Image1"].ToString();
                            image2 = reader["Image2"].ToString();
                            image3 = reader["Image3"].ToString();
                        }
                    }
                }
            }

            if (fuThumbnail.HasFile)
            {
                thumbnail = "/Data/" + Path.GetFileName(fuThumbnail.FileName);
                fuThumbnail.SaveAs(Server.MapPath(thumbnail));
            }

            if (fuImage1.HasFile)
            {
                image1 = "/Data/" + Path.GetFileName(fuImage1.FileName);
                fuImage1.SaveAs(Server.MapPath(image1));
            }

            if (fuImage2.HasFile)
            {
                image2 = "/Data/" + Path.GetFileName(fuImage2.FileName);
                fuImage2.SaveAs(Server.MapPath(image2));
            }

            if (fuImage3.HasFile)
            {
                image3 = "/Data/" + Path.GetFileName(fuImage3.FileName);
                fuImage3.SaveAs(Server.MapPath(image3));
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strUpdatePropertyDetails = @"
                    UPDATE [Property]
                    SET PropertyName = @PropertyName, PropertyAddress = @PropertyAddress, PropertyType = @PropertyType, PropertyPrice = @PropertyPrice,
                    ListingDescription = @ListingDescription, Preferences = @Preferences, NumberOfBedroom = @NumberOfBedroom, AreaSqft = @AreaSqft,
                    AirCondAvailability = @AirCondAvailability, WaterHeaterAvailability = @WaterHeaterAvailability, WifiAvailability = @WifiAvailability,
                    Thumbnail = @Thumbnail, Image1 = @Image1, Image2 = @Image2, Image3 = @Image3
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
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@Thumbnail", thumbnail);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@Image1", image1);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@Image2", image2);
                    cmdUpdatePropertyDetails.Parameters.AddWithValue("@Image3", image3);

                    int rowsAffected = cmdUpdatePropertyDetails.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        DataList1.DataBind();

                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                    "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Property Amendment");
                            command.Parameters.AddWithValue("@NotificationDescription", "Amended Property: " + propertyName);
                            command.Parameters.AddWithValue("@NotificationType", "Property Amendment");
                            command.ExecuteNonQuery();
                        }

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
                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                    "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Property Deletion");
                            command.Parameters.AddWithValue("@NotificationDescription", "Deleted Property: " + propertyId);
                            command.Parameters.AddWithValue("@NotificationType", "Property Deletion");
                            command.ExecuteNonQuery();
                        }

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
                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                    "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Request Property Maintenance");
                            command.Parameters.AddWithValue("@NotificationDescription", "Successfully requeted maintenance for property ID: " + propertyId + ".Maintenance Type: " + maintenanceType);
                            command.Parameters.AddWithValue("@NotificationType", "Request Property Maintenance");
                            command.ExecuteNonQuery();
                        }

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