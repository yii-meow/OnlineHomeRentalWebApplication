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

            string strSelect = "SELECT COUNT(*) FROM Property INNER JOIN Landlord ON Property.LandlordId = Landlord.LandlordId WHERE Landlord.UserId = @UserId";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
            int numberOfProperties = (int)cmdSelect.ExecuteScalar();
            con.Close();

            lblTotalProperties.Text = numberOfProperties.ToString();

            if (!IsPostBack)
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM Property INNER JOIN Landlord ON Property.LandlordId = Landlord.LandlordId WHERE Landlord.UserId = @UserId";
                SqlDataSource1.SelectParameters.Add("@UserId", Session["UserId"].ToString());
            }
        }

        protected void ApplyFilter_Click(object sender, EventArgs e)
        {
            lblReturnedFilterRecord.Text = "";
            string selectedPropertyType = propertyType.SelectedValue;

            // Update the SqlDataSource SelectCommand based on the selected property type
            SqlDataSource1.SelectCommand = "SELECT * FROM Property INNER JOIN Landlord ON Property.LandlordId = Landlord.LandlordId WHERE Landlord.UserId = @UserId";

            if (selectedPropertyType != "all")
            {
                SqlDataSource1.SelectCommand += $" AND [PropertyType] = '{selectedPropertyType}'";
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);
                con.Open();

                string strSelect = "SELECT COUNT(*) FROM Property INNER JOIN Landlord ON Property.LandlordId = Landlord.LandlordId WHERE Landlord.UserId = @UserId AND Property.PropertyType = @PropertyType";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                cmdSelect.Parameters.AddWithValue("@PropertyType", selectedPropertyType);
                int returned_record = (int)cmdSelect.ExecuteScalar();
                con.Close();

                lblReturnedFilterRecord.Text = "<b>" + returned_record.ToString()+ "</b> record(s).";
            }
            // Rebind the data to the Repeater
            Repeater1.DataBind();
        }
    }
}