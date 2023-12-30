using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class BookingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming that BookingStatus is in the first cell (index 3), adjust accordingly
                string bookingStatus = DataBinder.Eval(e.Row.DataItem, "BookingStatus").ToString();

                if (bookingStatus == "Completed")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                }
                else if (bookingStatus == "Cancelled")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Assuming you have a DataRowView as your data item
                DataRowView dataItem = (DataRowView)e.Item.DataItem;

                // Find the DropDownList in the current DataListItem
                DropDownList ddlBookingStatus = (DropDownList)e.Item.FindControl("ddlBookingStatus");
                
                Button btnUpdateBooking = (Button)e.Item.FindControl("btnUpdateBookingStatus"); // Replace "btnYourButton" with the actual ID of your button

                // Get the BookingStatus value from the data item
                string bookingStatus = dataItem["BookingStatus"].ToString();

                // Set the selected item in the DropDownList based on the BookingStatus value
                ListItem item = ddlBookingStatus.Items.FindByValue(bookingStatus);
                if (item != null)
                {
                    item.Selected = true;

                    // Disable the DropDownList if BookingStatus is "Cancelled"
                    ddlBookingStatus.Enabled = !(bookingStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) ||
                        bookingStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase));

                    btnUpdateBooking.Enabled = !(bookingStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) || bookingStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        protected void Chat_Click(object sender, EventArgs e)
        {
            string tenantId = ((LinkButton)sender).CommandArgument;

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strFindChatSession = "SELECT COUNT(*) FROM ChatSession WHERE LandlordId = @LandlordId AND TenantId = @TenantId";
                int count = 0;

                using (SqlCommand cmd = new SqlCommand(strFindChatSession, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@LandlordId", Session["LandlordId"]);
                    cmd.Parameters.AddWithValue("@TenantId", tenantId);

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
                        cmd.Parameters.AddWithValue("@LandlordId", Session["LandlordId"]);
                        cmd.Parameters.AddWithValue("@TenantId", tenantId);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Get Chat Session Id
                string strFindChatSessionId = "SELECT ChatSessionId FROM ChatSession WHERE LandlordId = @LandlordId AND TenantId = @TenantId";
                int chatSessionId = 0;

                using (SqlCommand cmd = new SqlCommand(strFindChatSessionId, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@LandlordId", Session["LandlordId"]);
                    cmd.Parameters.AddWithValue("@TenantId", tenantId);

                    chatSessionId = (int)cmd.ExecuteScalar();
                }

                Response.Redirect("~/Landlord/Chat.aspx?ChatSessionId=" + chatSessionId);
            }
        }

        protected void btnUpdateBookingStatus_Click(object sender, EventArgs e)
        {
            // Get the DataListItem that contains the button that was clicked
            Button btnUpdateBookingStatus = (Button)sender;
            DataListItem item = (DataListItem)btnUpdateBookingStatus.NamingContainer;

            // Find the DropDownList in the current DataListItem
            DropDownList ddlBookingStatus = (DropDownList)item.FindControl("ddlBookingStatus");

            // Get the selected value from the DropDownList
            string selectedBookingStatus = ddlBookingStatus.SelectedValue;

            string bookingId = btnUpdateBookingStatus.CommandArgument.ToString();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string strUpdateBookingStatus = @"
                UPDATE Booking
                SET BookingStatus = @BookingStatus
                WHERE BookingId = @BookingId
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdUpdateBookingStatus = new SqlCommand(strUpdateBookingStatus, connection))
                {
                    cmdUpdateBookingStatus.Parameters.AddWithValue("@BookingStatus", selectedBookingStatus);
                    cmdUpdateBookingStatus.Parameters.AddWithValue("@BookingId", bookingId);

                    connection.Open();

                    int rowAffected = cmdUpdateBookingStatus.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        GridView1.DataBind();
                        // Both updates were successful
                        alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Updated Booking Status Successfully !";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }

                    else
                    {
                        alertDiv.Attributes["class"] = $"alert alert-danger alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Failed to Update Booking Status !";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                }
            }
        }

    }
}