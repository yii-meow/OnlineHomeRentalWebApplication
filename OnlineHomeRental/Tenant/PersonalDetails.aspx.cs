using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class PersonalDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void customValidatorExpiryDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime expiryDate;
            if (DateTime.TryParseExact(args.Value, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))
            {
                if (expiryDate < DateTime.Now)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }


        protected void btnAddBankCard_Click(object sender, EventArgs e)
        {
            string cardHolderName = tbCardHolderName.Text;
            string cardNumber = tbCardNumber.Text;
            string cardExpiryDate = tbCardExpiry.Text;
            string cvv = tbCVV.Text;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string strAddBankCard = @"
                    INSERT INTO BankCardDetails(TenantId,CardHolderName,CardNumber,CardExpiryDate,CVV)
                    VALUES (@TenantId,@CardHolderName,@CardNumber,@CardExpiryDate,@CVV)
                ";

                using (SqlCommand cmdAddBankCard = new SqlCommand(strAddBankCard, connection))
                {
                    cmdAddBankCard.Parameters.AddWithValue("@TenantId", Session["TenantId"]);
                    cmdAddBankCard.Parameters.AddWithValue("@CardHolderName", cardHolderName);
                    cmdAddBankCard.Parameters.AddWithValue("@CardNumber", cardNumber);

                    if (DateTime.TryParseExact(cardExpiryDate, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
                    {
                        // Use SqlDbType.Date for SQL Server date type
                        cmdAddBankCard.Parameters.Add("@CardExpiryDate", SqlDbType.Date).Value = expiryDate;
                    }

                    cmdAddBankCard.Parameters.AddWithValue("@CVV", cvv);

                    int rowAffected = cmdAddBankCard.ExecuteNonQuery();

                    if(rowAffected > 0)
                    {
                        // Both updates were successful
                        alertDiv.Attributes["class"] = $"alert alert-success alert-dismissible fade show";

                        // Set the alert message
                        alertDiv.InnerHtml = "Successfully Added Bank Card !";

                        // Make the alert visible
                        alertDiv.Attributes["class"] = alertDiv.Attributes["class"].Replace("d-none", "");
                    }
                }
            }
        }
    }
}