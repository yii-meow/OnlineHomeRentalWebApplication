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

                Response.Redirect("/Landlord/HomePage.aspx");
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

                // Insert query with parameters
                string insertQuery = "INSERT INTO [dbo].[User] (UserId, UserPassword, UserType, AccountCreatedDate, Name, Gender, PhoneNo, Email) " +
                                     "VALUES (@UserId, @UserPassword, @UserType, @AccountCreatedDate, @Name, @Gender, @PhoneNo, @Email)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@UserId", LandlordRegUsername);
                    cmd.Parameters.AddWithValue("@UserPassword", LandlordRegPassword);
                    cmd.Parameters.AddWithValue("@UserType", "landlord");
                    cmd.Parameters.AddWithValue("@AccountCreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Name", LandlordRegName);
                    cmd.Parameters.AddWithValue("@Gender", LandlordRegGender);
                    cmd.Parameters.AddWithValue("@PhoneNo", LandlordRegPhoneNo);
                    cmd.Parameters.AddWithValue("@Email", LandlordRegEmail);

                    cmd.ExecuteNonQuery();
                }
            }

            // Redirect or perform any other actions after successful registration
            string script = $"alert('Register Successfully! Please use your username and password to login.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            Response.Redirect("/LandlordLoginPage.aspx");
        }

    }
}