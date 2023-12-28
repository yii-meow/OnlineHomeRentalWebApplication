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
    public partial class ViewProperty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the number of properties of the landlord
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            string strSelect = "SELECT COUNT(*) FROM Property WHERE LandlordId = @LandlordId";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@LandlordId", Session["LandlordId"].ToString());
            int numberOfProperties = (int)cmdSelect.ExecuteScalar();
            con.Close();

            lblTotalProperties.Text = numberOfProperties.ToString();
        }

        protected void ApplyFilter_Click(object sender, EventArgs e)
        {
            lblReturnedFilterRecord.Text = "";
            string selectedPropertyType = propertyType.SelectedValue;

            // Update the SqlDataSource SelectCommand based on the selected property type
            //SqlDataSource1.SelectCommand = "SELECT * FROM Property WHERE LandlordId = @LandlordId";

            if (selectedPropertyType != "all")
            {
                SqlDataSource1.SelectCommand += $" AND [PropertyType] = '{selectedPropertyType}'";
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);
                con.Open();

                string strSelect = "SELECT COUNT(*) FROM Property WHERE LandlordId = @LandlordId AND Property.PropertyType = @PropertyType";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@LandlordId", Session["LandlordId"].ToString());
                cmdSelect.Parameters.AddWithValue("@PropertyType", selectedPropertyType);
                int returned_record = (int)cmdSelect.ExecuteScalar();
                con.Close();

                lblReturnedFilterRecord.Text = "<b>" + returned_record.ToString() + "</b> record(s).";
            }
            Repeater1.DataBind();
        }
    }
}