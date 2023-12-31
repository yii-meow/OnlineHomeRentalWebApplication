using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class Notification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if there are any notifications
                bool hasNotifications = CheckIfNotificationsExist();

                if (hasNotifications)
                {
                    // If there are notifications, retrieve the data and bind it to the Repeater
                    DataTable notificationsData = GetNotificationsData();
                    Repeater1.DataSource = notificationsData;
                    Repeater1.DataBind();

                    // Make the Repeater visible and the "No notification" label invisible
                    Repeater1.Visible = true;
                    lblNoNotification.Visible = false;
                }
                else
                {
                    // If there are no notifications, display the "No notification" label
                    Repeater1.Visible = false;
                    lblNoNotification.Visible = true;
                }
            }
        }

        // Check if there are any notifications in the database
        private bool CheckIfNotificationsExist()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Notification WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    int notificationCount = (int)command.ExecuteScalar();

                    // If there are notifications (count is greater than 0), return true; otherwise, return false
                    return notificationCount > 0;
                }
            }
        }

        // Retrieve notifications data from the database
        private DataTable GetNotificationsData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string query = "SELECT * FROM Notification WHERE UserId = @UserId ORDER BY NotificationTime DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
    }
}