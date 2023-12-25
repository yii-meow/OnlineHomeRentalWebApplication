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
            if (!IsPostBack)
            {
                if (Request.QueryString["PropertyId"] != null)
                {
                    string propertyId = Request.QueryString["PropertyId"];

                    // Retrieve the number of properties of the landlord
                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        con.Open();

                        string strSelect = "SELECT * FROM PropertyDetails " +
                            "INNER JOIN PropertyListing ON PropertyDetails.PropertyId = PropertyListing.PropertyId " +
                            "WHERE PropertyDetails.PropertyId = @PropertyId";

                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, con))
                        {
                            cmdSelect.Parameters.AddWithValue("@PropertyId", propertyId);

                            using (SqlDataReader reader = cmdSelect.ExecuteReader())
                            {
                                // Check if there are rows returned
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string propertyType = reader.GetString(reader.GetOrdinal("PropertyType"));
                                        decimal propertyPrice = reader.GetDecimal(reader.GetOrdinal("PropertyPrice"));
                                        string listingDescription = reader.GetString(reader.GetOrdinal("ListingDescription"));
                                        int numberOfBedroom = reader.GetInt32(reader.GetOrdinal("NumberOfBedroom"));
                                        decimal area = reader.GetDecimal(reader.GetOrdinal("Area"));
                                        bool airCondAvailability = reader.GetBoolean(reader.GetOrdinal("AirCondAvailability"));
                                        bool waterHeaterAvailability = reader.GetBoolean(reader.GetOrdinal("WaterHeaterAvailability"));
                                        bool wifiAvailability = reader.GetBoolean(reader.GetOrdinal("WifiAvailability"));
                                        string preferences = reader.IsDBNull(reader.GetOrdinal("Preferences")) ? string.Empty : reader.GetString(reader.GetOrdinal("Preferences"));
                                        string propertyImages = reader.IsDBNull(reader.GetOrdinal("PropertyImages")) ? string.Empty : reader.GetString(reader.GetOrdinal("PropertyImages"));

                                        lblPropertyId.Text = propertyId.ToString();
                                        lblPropertyType.Text = propertyType;
                                        lblPropertyPrice.Text = propertyPrice.ToString("0.00");
                                        lblListingDescription.Text = listingDescription;
                                        lblNumberOfBedroom.Text = numberOfBedroom.ToString();
                                        lblArea.Text = area.ToString("0.00");  // Assuming you want to display it with two decimal places
                                        lblAirCondAvailability.Text = airCondAvailability ? "Available" : "Not Available";
                                        lblWaterHeaterAvailability.Text = waterHeaterAvailability ? "Available" : "Not Available";
                                        lblWifiAvailability.Text = wifiAvailability ? "Available" : "Not Available";
                                        lblPreferences.Text = preferences;
                                    }
                                }
                                else
                                {
                                    // Handle the case when no rows are returned
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}