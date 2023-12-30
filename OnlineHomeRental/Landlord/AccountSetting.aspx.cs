using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace OnlineHomeRental.Landlord
{
    public partial class AccountSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSavePersonalDetails_Click(object sender, EventArgs e)
        {
            string editedName = tbEditName.Text;
            string editedEmail = tbEditEmail.Text;
            string editedPhoneNo = tbEditPhoneNo.Text;
            string editedBankAccount = tbEditBankAccount.Text;

            if (editedBankAccount == "None")
            {
                editedBankAccount = "";
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strUpdatePersonalDetails = @"
                    UPDATE [User] 
                    SET [User].Name = @Name, [User].Email = @Email, [User].PhoneNo = @PhoneNo 
                    FROM [User]
                    WHERE [User].UserId = @UserId
                ";

                using (SqlCommand cmdUpdatePersonalDetails = new SqlCommand(strUpdatePersonalDetails, connection))
                {
                    cmdUpdatePersonalDetails.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    cmdUpdatePersonalDetails.Parameters.AddWithValue("@Name", editedName);
                    cmdUpdatePersonalDetails.Parameters.AddWithValue("@Email", editedEmail);
                    cmdUpdatePersonalDetails.Parameters.AddWithValue("@PhoneNo", editedPhoneNo);

                    int rowsAffected = cmdUpdatePersonalDetails.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        string strUpdateBankAccount = @"
                            UPDATE Landlord
                            SET BankAccount = @BankAccount
                            WHERE UserId = @UserId
                        ";

                        using (SqlCommand cmdUpdateBankAccount = new SqlCommand(strUpdateBankAccount, connection))
                        {

                            if (editedBankAccount == "None" || string.IsNullOrEmpty(editedBankAccount))
                            {
                                cmdUpdateBankAccount.Parameters.AddWithValue("@BankAccount", DBNull.Value);
                            }
                            else
                            {
                                cmdUpdateBankAccount.Parameters.AddWithValue("@BankAccount", editedBankAccount);
                            }

                            cmdUpdateBankAccount.Parameters.AddWithValue("@UserId", Session["UserId"]);

                            // Execute the update for Landlord table
                            int rowsAffectedLandlord = cmdUpdateBankAccount.ExecuteNonQuery();

                            // Check the result
                            if (rowsAffectedLandlord > 0)
                            {
                                Repeater1.DataBind();

                                // Both updates were successful
                                alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                                // Set the alert message
                                alertDiv.InnerHtml = "Successfully Edited Personal Details !";

                                // Make the alert visible
                                alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                            }
                            else
                            {
                                // Something went wrong with Landlord table update
                                alertDiv.Attributes["class"] = $"alert alert-danger alert-dismissible fade show";

                                // Set the alert message
                                alertDiv.InnerHtml = "Failed to edit personal details !";

                                // Make the alert visible
                                alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                            }
                        }
                    }
                }
            }
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (fileUploadImage.HasFile)
            {
                string fileName = Path.GetFileName(fileUploadImage.FileName);
                string filePath = "/Data/" + fileName;

                // Save the file
                fileUploadImage.SaveAs(Server.MapPath(filePath));

                // Update the database with the file path
                UpdateProfileImageInDatabase(filePath);
            }
        }

        private void UpdateProfileImageInDatabase(string imagePath)
        {
            // Use parameterized query to prevent SQL injection
            string strUpdateProfileImage = "UPDATE [User] SET ProfileImage = @ImagePath WHERE UserId = @UserId";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdUpdateProfileImage = new SqlCommand(strUpdateProfileImage, connection))
                {
                    cmdUpdateProfileImage.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    cmdUpdateProfileImage.Parameters.AddWithValue("@ImagePath", imagePath);

                    connection.Open();
                    int rowAffected = cmdUpdateProfileImage.ExecuteNonQuery();

                    // Check the result
                    if (rowAffected > 0)
                    {
                        // Reset Profile Image Cookie
                        HttpCookie profileImageCookie = new HttpCookie("UserProfileImage");
                        profileImageCookie.Value = imagePath;
                        profileImageCookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(profileImageCookie);

                        Response.Redirect(Request.RawUrl, false);

                        // Both updates were successful
                        alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Updated Profile Image Successfully!";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                    else
                    {
                        alertDiv.Attributes["class"] = $"alert alert-danger alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Failed to upload profile image!";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                }
            }
        }
    }
}