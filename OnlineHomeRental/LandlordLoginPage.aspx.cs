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

        protected void Login_click(object sender, EventArgs e)
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

    }
}