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
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            string strSelect = "SELECT LandlordId FROM Landlord WHERE UserId = @UserId";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
            int LandlordId = (int)cmdSelect.ExecuteScalar();
            con.Close();

            //string script = $"alert('{LandlordId}');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);

            //SqlDataSource1.SelectParameters.Add("@LandlordId", LandlordId.ToString());

            SqlDataSource1.SelectCommand = "SELECT * FROM Booking WHERE LandlordId = @LandlordId";
            SqlDataSource1.SelectParameters.Add("@LandlordId", LandlordId.ToString());
            GridView1.DataBind();
        }
    }
}