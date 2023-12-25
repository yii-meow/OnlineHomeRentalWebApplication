using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineHomeRental.Landlord
{
    public partial class ViewProperty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the number of properties of the landlord
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            string strSelect = "SELECT NumberOfProperties FROM Landlord WHERE LandlordId = @LandlordId";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@LandlordId", 1);
            int numberOfProperties = (int)cmdSelect.ExecuteScalar();
            con.Close();

            lblTotalProperties.Text = numberOfProperties.ToString();
        }
    }
}