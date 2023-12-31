using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Landlord
{
    public partial class LandlordLoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string LandlordUsername = tbLandlordUsername.Text;
            string LandlordPassword = tbLandlordPassword.Text;
            string hashedPassword = HashPassword(LandlordPassword);

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            // Successfully Login
            if (CheckLogin(LandlordUsername, hashedPassword))
            {
                Session["UserId"] = LandlordUsername;
                Session["LandlordId"] = GetLandlordId(LandlordUsername);

                string imagePath = GetUserProfileImagePath(LandlordUsername);

                HttpCookie profileImageCookie = new HttpCookie("UserProfileImage");

                // Add imagepath to the cookie
                if (imagePath != null)
                {
                    profileImageCookie.Value = imagePath;
                }
                else
                {
                    profileImageCookie.Value = null;
                }

                Response.Cookies.Add(profileImageCookie);
                profileImageCookie.Expires = DateTime.Now.AddDays(15);

                Response.Redirect("/Landlord/Homepage.aspx");
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

                string query = "SELECT COUNT(*) FROM [User] WHERE [UserId] = @UserId AND [UserPassword] = @UserPassword AND [UserType] = 'Landlord'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", username);
                    command.Parameters.AddWithValue("@UserPassword", hashedPassword);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private int GetLandlordId(string LandlordUsername)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                string query = "SELECT LandlordId from Landlord WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", LandlordUsername);

                    int LandlordId = (int)command.ExecuteScalar();

                    connection.Close();

                    return LandlordId;
                }
            }
        }

        private string GetUserProfileImagePath(string UserId)
        {
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                string query = "SELECT ProfileImage from [User] WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string userProfileImagePath = null;

                    command.Parameters.AddWithValue("@UserId", UserId);

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        userProfileImagePath = (string)result;
                    }
                    return userProfileImagePath;
                }
            }
        }

        // Register Landlord
        protected void Register_Click(object sender, EventArgs e)
        {
            string LandlordRegUsername = tbRegLandlordUsername.Text;
            string LandlordRegPassword = HashPassword(tbRegLandlordPassword.Text);
            string LandlordRegName = tbRegLandlordName.Text;
            string LandlordRegGender = rbRegLandlordGender.SelectedValue;
            string LandlordRegPhoneNo = tbRegLandlordPhoneNo.Text;
            string LandlordRegEmail = tbRegLandlordEmail.Text;

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
                    cmdCreateUser.Parameters.AddWithValue("@UserId", LandlordRegUsername);
                    cmdCreateUser.Parameters.AddWithValue("@UserPassword", LandlordRegPassword);
                    cmdCreateUser.Parameters.AddWithValue("@UserType", "landlord");
                    cmdCreateUser.Parameters.AddWithValue("@AccountCreatedDate", DateTime.Now);
                    cmdCreateUser.Parameters.AddWithValue("@Name", LandlordRegName);
                    cmdCreateUser.Parameters.AddWithValue("@Gender", LandlordRegGender);
                    cmdCreateUser.Parameters.AddWithValue("@PhoneNo", LandlordRegPhoneNo);
                    cmdCreateUser.Parameters.AddWithValue("@Email", LandlordRegEmail);

                    int rowAffected = cmdCreateUser.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        // create new landlord
                        string strCreateLandlord = "INSERT INTO Landlord(UserId) VALUES (@UserId)";

                        using (SqlCommand cmdCreateLandlord = new SqlCommand(strCreateLandlord, con))
                        {
                            // Add parameters
                            cmdCreateLandlord.Parameters.AddWithValue("@UserId", LandlordRegUsername);
                            cmdCreateLandlord.ExecuteNonQuery();
                        }

                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                            "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, con))
                        {
                            command.Parameters.AddWithValue("@UserId", LandlordRegUsername);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Account Creation");
                            command.Parameters.AddWithValue("@NotificationDescription", "Successfully create account ! We are happy to have you as one of our landlord!");
                            command.Parameters.AddWithValue("@NotificationType", "Account Creation");
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Redirect or perform any other actions after successful registration
            string script = "alert('Register Successfully! Please use your username and password to login.'); " +
                "setTimeout(function() { window.location.href = '/LandlordLoginPage.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
        }

    }
}