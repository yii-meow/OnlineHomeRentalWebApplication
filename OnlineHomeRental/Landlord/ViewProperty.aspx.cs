using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strSelect = "SELECT COUNT(*) FROM Property WHERE LandlordId = @LandlordId";

                using (SqlCommand cmdSelect = new SqlCommand(strSelect, con))
                {
                    cmdSelect.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));

                    int numberOfProperties = (int)cmdSelect.ExecuteScalar();

                    lblTotalProperties.Text = numberOfProperties.ToString();
                }
            }
        }

        protected void ApplyFilter_Click(object sender, EventArgs e)
        {
            lblReturnedFilterRecord.Text = "";
            lblSearchedPropertyTotalResult.Text = "";
            string selectedPropertyType = propertyType.SelectedValue;

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

        protected void tbSearchbar_TextChanged(object sender, EventArgs e)
        {
            string searched_value = tbSearchbar.Text.Trim();
            lblReturnedFilterRecord.Text = "";
            lblSearchedPropertyTotalResult.Text = "";

            SqlDataSource1.SelectParameters["SearchedValue"].DefaultValue = "%" + searched_value + "%";

            Repeater1.DataBind();

            // Get total property of the searched result
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strSearchedTotalPropertyResult = "SELECT COUNT(*) FROM Property WHERE LandlordId = @LandlordId AND PropertyName LIKE @PropertyName";

                using (SqlCommand cmdSearchedTotalPropertyResult = new SqlCommand(strSearchedTotalPropertyResult, con))
                {
                    cmdSearchedTotalPropertyResult.Parameters.AddWithValue("@LandlordId", Convert.ToInt32(Session["LandlordId"]));
                    cmdSearchedTotalPropertyResult.Parameters.AddWithValue("@PropertyName", "%" + searched_value + "%");

                    int numberOfProperties = (int)cmdSearchedTotalPropertyResult.ExecuteScalar();

                    lblSearchedPropertyTotalResult.Text = "<b>" + numberOfProperties.ToString() + "</b> result(s) has been returned based on your search.";
                }
            }
        }
    }
}