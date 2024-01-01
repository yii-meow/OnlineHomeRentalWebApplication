using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class PropertyDetail : System.Web.UI.Page
    {
        private int passPropertyId
        {
            get { return ViewState["PropertyId"] != null ? (int)ViewState["PropertyId"] : 0; }
            set { ViewState["PropertyId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["TenantId"] == null)
            {
                Response.Redirect("/TenantLoginPage.aspx");
            }

            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["PropertyId"], out int propertyId))
                {
                    passPropertyId = propertyId;

                    string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();

                        String query = "SELECT * FROM Property WHERE PropertyId = @PropertyId";

                        using (SqlCommand cmdAdd = new SqlCommand(query, conn))
                        {
                            cmdAdd.Parameters.AddWithValue("@PropertyId", propertyId);

                            using (SqlDataReader reader = cmdAdd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string propertyName = reader["PropertyName"].ToString();
                                    string propertyType = reader["PropertyType"].ToString();
                                    string propertyAddress = reader["PropertyAddress"].ToString();
                                    string listingDescription = reader["ListingDescription"].ToString();
                                    decimal propertyPrice = reader.GetDecimal(reader.GetOrdinal("PropertyPrice"));
                                    int noOfBedroom = reader.GetInt32(reader.GetOrdinal("NumberOfBedroom"));
                                    decimal areaSqft = reader.GetDecimal(reader.GetOrdinal("AreaSqft"));
                                    bool airCond = reader.GetBoolean(reader.GetOrdinal("AirCondAvailability"));
                                    bool waterHeater = reader.GetBoolean(reader.GetOrdinal("WaterHeaterAvailability"));
                                    bool wifi = reader.GetBoolean(reader.GetOrdinal("WifiAvailability"));
                                    string thumbnail = reader["Thumbnail"].ToString();
                                    string image1 = reader["Image1"].ToString();
                                    string image2 = reader["Image2"].ToString();
                                    string image3 = reader["Image3"].ToString();

                                    lblPropertyName.Text = propertyName;
                                    lblPropertyType.Text = propertyType;
                                    lblPropertyAddress.Text = propertyAddress;
                                    lblListingDescription.Text = listingDescription;
                                    lblPropertyPrice.Text = propertyPrice.ToString();
                                    lblNoOfBedroom.Text = noOfBedroom.ToString();

                                    int areaSqftInt = Convert.ToInt32(areaSqft);
                                    lblAreaSqft.Text = areaSqftInt.ToString();

                                    string airCondIcon = airCond ? "<i class=\"bi bi-check-circle text-primary me-2\"></i>" : "<i class=\"bi bi-x-circle text-red me-2\"></i>";
                                    string waterHeaterIcon = waterHeater ? "<i class=\"bi bi-check-circle text-primary me-2\"></i>" : "<i class=\"bi bi-x-circle text-red me-2\"></i>";
                                    string wifiIcon = wifi ? "<i class=\"bi bi-check-circle text-primary me-2\"></i>" : "<i class=\"bi bi-x-circle text-red me-2\"></i>";
                                    lblAirCond.Controls.Add(new LiteralControl(airCondIcon));
                                    lblWaterHeater.Controls.Add(new LiteralControl(waterHeaterIcon));
                                    lblWifi.Controls.Add(new LiteralControl(wifiIcon));

                                    imgThumbnail.Attributes["src"] = string.IsNullOrEmpty(thumbnail) ? "/img/noimage.png" : thumbnail;
                                    imgImage1.Attributes["src"] = string.IsNullOrEmpty(image1) ? "/img/noimage.png" : image1;
                                    imgImage2.Attributes["src"] = string.IsNullOrEmpty(image2) ? "/img/noimage.png" : image2;
                                    imgImage3.Attributes["src"] = string.IsNullOrEmpty(image3) ? "/img/noimage.png" : image3;

                                    reader.Close();
                                }
                            }
                        }

                        conn.Close();
                    }
                }
            }
        }

        protected void CustomCompare1(object sender, ServerValidateEventArgs e)
        {
            DateTime dateToday = DateTime.Today;
            DateTime dateFrom;

            if (DateTime.TryParse(txtBookFrom.Text, out dateFrom))
            {
                if (dateFrom.Date >= dateToday)
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                }
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            if (passPropertyId > 0 && Page.IsValid)
            {
                string dateFrom = txtBookFrom.Text;
                string dateTo = txtBookTo.Text;

                Response.Redirect($"Payment.aspx?PropertyId={passPropertyId}&DateFrom={dateFrom}&DateTo={dateTo}");
            }
        }

        //protected void CustomCompare2(object sender, ServerValidateEventArgs e)
        //{
        //    DateTime dateFrom, dateTo;

        //    if (DateTime.TryParse(txtBookFrom.Text, out dateFrom) && DateTime.TryParse(txtBookTo.Text, out dateTo))
        //    {
        //        int monthsDifference = ((dateTo.Year - dateFrom.Year) * 12) + dateTo.Month - dateFrom.Month;
        //        DateTime expectedEndDate = dateFrom.AddMonths(monthsDifference);
        //        expectedEndDate = expectedEndDate.AddDays(-1);

        //        if (dateTo.Date == expectedEndDate.Date)
        //        {
        //            e.IsValid = true;
        //        }
        //        else
        //        {
        //            e.IsValid = false;
        //        }
        //    }
        //    else
        //    {
        //        e.IsValid = false;
        //    }
        //}

        protected void Chat_Click(object sender, EventArgs e)
        {
            string propertyId = "";

            if (Request.QueryString["PropertyId"] != null)
            {
                // Get the TenantId from the query string
                propertyId = Request.QueryString["PropertyId"];
            }

            string landlordId = GetLandlordId(propertyId);

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strFindChatSession = "SELECT COUNT(*) FROM ChatSession WHERE LandlordId = @LandlordId AND TenantId = @TenantId";
                int count = 0;

                using (SqlCommand cmd = new SqlCommand(strFindChatSession, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@LandlordId", landlordId);
                    cmd.Parameters.AddWithValue("@TenantId", Session["TenantId"]);

                    count = (int)cmd.ExecuteScalar();
                }

                // Chat Session not yet being created
                if (count == 0)
                {
                    // Insert query with parameters
                    string insertQuery = "INSERT INTO ChatSession(LandlordId,TenantId) VALUES(@LandlordId,@TenantId)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@LandlordId", landlordId);
                        cmd.Parameters.AddWithValue("@TenantId", Session["TenantId"]);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Get Chat Session Id
                string strFindChatSessionId = "SELECT ChatSessionId FROM ChatSession WHERE LandlordId = @LandlordId AND TenantId = @TenantId";
                int chatSessionId = 0;

                using (SqlCommand cmd = new SqlCommand(strFindChatSessionId, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@LandlordId", landlordId);
                    cmd.Parameters.AddWithValue("@TenantId", Session["TenantId"]);

                    chatSessionId = (int)cmd.ExecuteScalar();
                }

                Response.Redirect("~/Tenant/TenantChat.aspx?ChatSessionId=" + chatSessionId);
            }
        }

        private string GetLandlordId(string propertyId)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                string query = "SELECT Landlord.LandlordId FROM Landlord INNER JOIN Property ON Landlord.LandlordId = Property.LandlordId WHERE Property.PropertyId = @PropertyId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", propertyId);

                    int LandlordId = (int)command.ExecuteScalar();

                    connection.Close();

                    return LandlordId.ToString();
                }
            }
        }
    }
}