using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class TenantLoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Session["UserId"] = "";

            string LandlordUsername = tbTenantUsername.Text;
            string LandlordPassword = tbTenantPassword.Text;
            string hashedPassword = HashPassword(LandlordPassword);

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            // Successfully Login
            if (CheckLogin(LandlordUsername, hashedPassword))
            {
                Session["UserId"] = LandlordUsername;
                Session["TenantId"] = GetTenantId(LandlordUsername);

                Response.Redirect("/Tenant/Homepage.aspx");
            }
            // Login failed
            else
            {
                lblError.Text = "Invalid username or password.";
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool CheckLogin(string username, string hashedPassword)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [User] WHERE [UserId] = @UserId AND [UserPassword] = @UserPassword AND [UserType] = 'Tenant'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", username);
                    command.Parameters.AddWithValue("@UserPassword", hashedPassword);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private int GetTenantId(string TenantUsername)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                string query = "SELECT TenantId from Tenant WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", TenantUsername);

                    int tenantId = (int)command.ExecuteScalar();

                    connection.Close();

                    return tenantId;
                }
            }
        }

        // Register Tenant
        protected void Register_Click(object sender, EventArgs e)
        {
            string TenantRegUsername = tbRegTenantUsername.Text;
            string TenantRegPassword = HashPassword(tbRegTenantPassword.Text);
            string TenantRegName = tbRegTenantName.Text;
            string TenantRegGender = rbRegTenantGender.SelectedValue;
            string TenantRegPhoneNo = tbRegTenantPhoneNo.Text;
            string TenantRegEmail = tbRegTenantEmail.Text;

            // Connection string
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                // create new user
                string strCreateUser = "INSERT INTO [dbo].[User] (UserId, UserPassword, UserType, AccountCreatedDate, Name, Gender, PhoneNo, Email) " +
                                     "VALUES (@UserId, @UserPassword, @UserType, @AccountCreatedDate, @Name, @Gender, @PhoneNo, @Email)";

                using (SqlCommand cmdCreateUser = new SqlCommand(strCreateUser, con))
                {
                    // Add parameters
                    cmdCreateUser.Parameters.AddWithValue("@UserId", TenantRegUsername);
                    cmdCreateUser.Parameters.AddWithValue("@UserPassword", TenantRegPassword);
                    cmdCreateUser.Parameters.AddWithValue("@UserType", "tenant");
                    cmdCreateUser.Parameters.AddWithValue("@AccountCreatedDate", DateTime.Now);
                    cmdCreateUser.Parameters.AddWithValue("@Name", TenantRegName);
                    cmdCreateUser.Parameters.AddWithValue("@Gender", TenantRegGender);
                    cmdCreateUser.Parameters.AddWithValue("@PhoneNo", TenantRegPhoneNo);
                    cmdCreateUser.Parameters.AddWithValue("@Email", TenantRegEmail);

                    int rowAffected = cmdCreateUser.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        // create new landlord
                        string strCreateLandlord = "INSERT INTO Tenant(UserId,TenantStatus) VALUES (@UserId,@TenantStatus)";

                        using (SqlCommand cmdCreateLandlord = new SqlCommand(strCreateLandlord, con))
                        {
                            // Add parameters
                            cmdCreateLandlord.Parameters.AddWithValue("@UserId", TenantRegUsername);
                            cmdCreateLandlord.Parameters.AddWithValue("@TenantStatus", "active");
                            cmdCreateLandlord.ExecuteNonQuery();
                        }

                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                            "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, con))
                        {
                            command.Parameters.AddWithValue("@UserId", TenantRegUsername);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Account Creation");
                            command.Parameters.AddWithValue("@NotificationDescription", "Successfully create account ! We are happy to have you as one of our tenant!");
                            command.Parameters.AddWithValue("@NotificationType", "Account Creation");
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Redirect or perform any other actions after successful registration
            string script = "alert('Register Successfully! Please use your username and password to login.'); " +
                "setTimeout(function() { window.location.href = '/TenantLoginPage.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
        }

    }
}