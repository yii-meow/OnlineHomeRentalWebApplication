using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            String query = "SELECT PropertyType, COUNT(*) AS PropertyCount FROM Property WHERE PropertyType IN ('Residential', 'Flat', 'Apartment', 'Condominium') GROUP BY PropertyType;";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string propertyType = reader["PropertyType"].ToString();
                int count = Convert.ToInt32(reader["PropertyCount"]);

                if (propertyType == "Residential")
                {
                    lblNoOfResidential.Text = count.ToString();
                }
                else if (propertyType == "Flat")
                {
                    lblNoOfFlat.Text = count.ToString();
                }
                else if (propertyType == "Apartment")
                {
                    lblNoOfApartment.Text = count.ToString();
                }
                else if (propertyType == "Condominium")
                {
                    lblNoOfCondo.Text = count.ToString();
                }
            }
            reader.Close();

            conn.Close();
        }

        protected string GetIcon(bool status)
        {
            if (status)
            {
                return "<i class=\"bi bi-check-circle text-primary me-2\"></i>";
            }
            else
            {
                return "<i class=\"bi bi-x-circle text-red me-2\"></i>";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string propType = ddlPropType.SelectedValue;
            string airCond = ddlAirCond.SelectedValue;
            string waterHeater = ddlWaterHeater.SelectedValue;
            string wifi = ddlWifi.SelectedValue;

            Response.Redirect($"ViewProperties.aspx?propType={propType}&airCond={airCond}&waterHeater={waterHeater}&wifi={wifi}");
        }
    }
}