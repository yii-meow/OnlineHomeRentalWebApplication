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
            cmdSelect.Parameters.AddWithValue("@UserId", Session["UserId"].ToString()); // temporary value
            int numberOfProperties = (int)cmdSelect.ExecuteScalar();
            con.Close();

            lblTotalProperties.Text = numberOfProperties.ToString();

            if (!IsPostBack)
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM Property INNER JOIN Landlord ON Property.LandlordId = Landlord.LandlordId WHERE Landlord.UserId = @UserId";
                SqlDataSource1.SelectParameters.Add("@UserId", Session["UserId"].ToString()); // Assuming you have a function to get the UserId
                //SqlDataSource1.SelectParameters["UserId"].DefaultValue = Session["UserId"].ToString();

                // Call DataBind() on the control you want to bind data to
                //Repeater1.DataBind();
            }
        }

        protected void ApplyFilter_Click(object sender, EventArgs e)
        {
            string selectedPropertyType = propertyType.SelectedValue;

            // Update the SqlDataSource SelectCommand based on the selected property type
            SqlDataSource1.SelectCommand = "SELECT * FROM [Property]";

            if (selectedPropertyType != "all")
            {
                SqlDataSource1.SelectCommand += $" WHERE [PropertyType] = '{selectedPropertyType}'";
            }

            // Rebind the data to the Repeater
            Repeater1.DataBind();
        }
    }
}