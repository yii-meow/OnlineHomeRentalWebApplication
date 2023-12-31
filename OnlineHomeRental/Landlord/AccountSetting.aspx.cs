using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

                                string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                        "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                                using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                                    command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                                    command.Parameters.AddWithValue("@NotificationTitle", "Account Details Update");
                                    command.Parameters.AddWithValue("@NotificationDescription", "Successfully updated account details.");
                                    command.Parameters.AddWithValue("@NotificationType", "Account Details Update");
                                    command.ExecuteNonQuery();
                                }

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
                        string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                        "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                        using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                            command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                            command.Parameters.AddWithValue("@NotificationTitle", "Update Profile Image");
                            command.Parameters.AddWithValue("@NotificationDescription", "Successfully updated profile image.");
                            command.Parameters.AddWithValue("@NotificationType", "Update Profile Image");
                            command.ExecuteNonQuery();
                        }

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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["UserId"] != null)
            {
                args.IsValid = (args.Value != Session["UserId"].ToString());
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = tbCurrentPassword.Text;
            string newPassword = tbNewPassword.Text;
            string newPasswordRepeat = tbNewPasswordRepeat.Text;

            // Repeated password not matched
            if (!newPassword.Equals(newPasswordRepeat))
            {
                return;
            }

            // Password same with username
            if(newPassword == Session["UserId"].ToString())
            {
                alertDiv.Attributes["class"] = $"";

                // Set the alert message
                alertDiv.InnerHtml = "";

                // Make the alert visible
                alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");

                return;
            }

            // Compare current password with the database
            string strGetPassword = "SELECT UserPassword FROM [User] WHERE UserId = @UserId";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string dbPassword = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdGetPassword = new SqlCommand(strGetPassword, connection))
                {
                    cmdGetPassword.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    connection.Open();
                    object result = cmdGetPassword.ExecuteScalar();

                    if(result!= DBNull.Value && result != null)
                    {
                        dbPassword = (string)result;
                    }
                }
            }

            // Password not match with db
            if (HashPassword(currentPassword) != dbPassword)
            {
                // Both updates were successful
                alertDiv.Attributes["class"] = $"alert alert-danger alert-dismissible fade show";

                // Set the alert message
                alertDiv.InnerHtml = "Current Password is not matched with existing record !";

                // Make the alert visible
                alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
            }
            else
            {
                string newPasswordHash = HashPassword(newPassword);

                string strUpdatePassword = "UPDATE [USER] SET UserPassword = @Password WHERE UserId = @UserId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdUpdatePassword = new SqlCommand(strUpdatePassword, connection))
                    {
                        cmdUpdatePassword.Parameters.AddWithValue("@UserId", Session["UserId"]);
                        cmdUpdatePassword.Parameters.AddWithValue("@Password", newPasswordHash);

                        connection.Open();
                        int rowAffected = cmdUpdatePassword.ExecuteNonQuery();

                        if (rowAffected > 0)
                        {
                            string insertNotificationQuery = "INSERT INTO Notification(UserId,NotificationTime,NotificationTitle,NotificationDescription,NotificationType) " +
                                        "VALUES(@UserId,@NotificationTime,@NotificationTitle, @NotificationDescription, @NotificationType)";

                            using (SqlCommand command = new SqlCommand(insertNotificationQuery, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", Session["UserId"]);
                                command.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                                command.Parameters.AddWithValue("@NotificationTitle", "Update Password");
                                command.Parameters.AddWithValue("@NotificationDescription", "Successfully updated account password.");
                                command.Parameters.AddWithValue("@NotificationType", "Update Password");
                                command.ExecuteNonQuery();
                            }

                            // Both updates were successful
                            alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                            // Set the alert message
                            alertDiv.InnerHtml = "Updated Password Successfully !";

                            // Make the alert visible
                            alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                        }

                        else
                        {
                            alertDiv.Attributes["class"] = $"alert alert-danger alert-dismissible fade show";

                            // Set the alert message
                            alertDiv.InnerHtml = "Failed to Update Password !";

                            // Make the alert visible
                            alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                        }
                    }
                }
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
    }
}