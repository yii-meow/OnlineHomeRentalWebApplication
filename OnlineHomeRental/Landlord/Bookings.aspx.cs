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
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                }
                else if (bookingStatus == "Cancelled")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
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
    }
}