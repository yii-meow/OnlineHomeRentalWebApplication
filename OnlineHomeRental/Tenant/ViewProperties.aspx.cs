using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineHomeRental.Tenant
{
    public partial class ViewProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["TenantId"] == null)
            {
                Response.Redirect("/TenantLoginPage.aspx");
            }

            DisplayLabel();

            if (!IsPostBack)
            {
                if (Request.QueryString["propType"] != null && Request.QueryString["airCond"] != null &&
                    Request.QueryString["waterHeater"] != null && Request.QueryString["wifi"] != null)
                {
                    string propType = Request.QueryString["propType"];
                    string airCond = Request.QueryString["airCond"];
                    string waterHeater = Request.QueryString["waterHeater"];
                    string wifi = Request.QueryString["wifi"];

                    if (propType.Equals("Residential", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox1.Checked = true;
                    }
                    else if (propType.Equals("Flat", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox2.Checked = true;
                    }
                    else if (propType.Equals("Apartment", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox3.Checked = true;
                    }
                    else if (propType.Equals("Condominium", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox4.Checked = true;
                    }

                    if (airCond.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox9.Checked = true;
                    }

                    if (waterHeater.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox10.Checked = true;
                    }

                    if (wifi.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckBox11.Checked = true;
                    }
                }

                bool allUnchecked = !AreAnyCheckboxesChecked();

                if (allUnchecked)
                {
                    DisplayAllProperties();
                }
                else
                {
                    FilterAndSearch();
                }
            }
        }

        private void DisplayLabel()
        {
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            String query1 = "SELECT PropertyType, COUNT(*) AS PropertyCount FROM Property WHERE PropertyType IN ('Residential', 'Flat', 'Apartment', 'Condominium') GROUP BY PropertyType;";

            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                string propertyType = reader1["PropertyType"].ToString();
                int count = Convert.ToInt32(reader1["PropertyCount"]);

                if (propertyType == "Residential")
                {
                    lblResidential.Text = count.ToString();
                }
                else if (propertyType == "Flat")
                {
                    lblFlat.Text = count.ToString();
                }
                else if (propertyType == "Apartment")
                {
                    lblApartment.Text = count.ToString();
                }
                else if (propertyType == "Condominium")
                {
                    lblCondominium.Text = count.ToString();
                }
            }
            reader1.Close();

            String query2 = "SELECT " +
                           "SUM(CASE WHEN PropertyPrice >= 0 AND PropertyPrice < 100 THEN 1 ELSE 0 END) AS PriceRange1, " +
                           "SUM(CASE WHEN PropertyPrice >= 100 AND PropertyPrice < 200 THEN 1 ELSE 0 END) AS PriceRange2, " +
                           "SUM(CASE WHEN PropertyPrice >= 200 AND PropertyPrice < 300 THEN 1 ELSE 0 END) AS PriceRange3, " +
                           "SUM(CASE WHEN PropertyPrice >= 300 THEN 1 ELSE 0 END) AS PriceRange4 " +
                           "FROM Property;";

            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader reader2 = cmd2.ExecuteReader();

            if (reader2.Read())
            {
                lblPriceRange1.Text = reader2["PriceRange1"].ToString();
                lblPriceRange2.Text = reader2["PriceRange2"].ToString();
                lblPriceRange3.Text = reader2["PriceRange3"].ToString();
                lblPriceRange4.Text = reader2["PriceRange4"].ToString();
            }
            reader2.Close();

            String query3 = "SELECT " +
                           "SUM(CASE WHEN AirCondAvailability = 1 THEN 1 ELSE 0 END) AS AirCond, " +
                           "SUM(CASE WHEN WaterHeaterAvailability = 1 THEN 1 ELSE 0 END) AS WaterHeater, " +
                           "SUM(CASE WHEN WifiAvailability = 1 THEN 1 ELSE 0 END) AS Wifi " +
                           "FROM Property;";

            SqlCommand cmd3 = new SqlCommand(query3, conn);
            SqlDataReader reader3 = cmd3.ExecuteReader();

            if (reader3.Read())
            {
                lblAirCond.Text = reader3["AirCond"].ToString();
                lblWaterHeater.Text = reader3["WaterHeater"].ToString();
                lblWifi.Text = reader3["Wifi"].ToString();
            }
            reader3.Close();

            conn.Close();
        }

        private void DisplayAllProperties()
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM Property";
            Repeater1.DataSource = SqlDataSource1;
            Repeater1.DataBind();
        }

        protected void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool allUnchecked = !AreAnyCheckboxesChecked();

            if (allUnchecked)
            {
                Panel1.Visible = false;
                DisplayAllProperties();
            }
            else
            {
                FilterAndSearch();
            }
        }

        private bool AreAnyCheckboxesChecked()
        {
            return CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked
                || CheckBox4.Checked || CheckBox5.Checked || CheckBox6.Checked
                || CheckBox7.Checked || CheckBox8.Checked || CheckBox9.Checked
                || CheckBox10.Checked || CheckBox11.Checked;
        }

        protected void FilterAndSearch()
        {
            List<string> conditions = new List<string>();

            List<string> propertyTypeConditions = new List<string>();
            if (CheckBox1.Checked)
            {
                propertyTypeConditions.Add("PropertyType = 'Residential'");
            }
            if (CheckBox2.Checked)
            {
                propertyTypeConditions.Add("PropertyType = 'Flat'");
            }
            if (CheckBox3.Checked)
            {
                propertyTypeConditions.Add("PropertyType = 'Apartment'");
            }
            if (CheckBox4.Checked)
            {
                propertyTypeConditions.Add("PropertyType = 'Condominium'");
            }
            if (propertyTypeConditions.Any())
            {
                conditions.Add("(" + string.Join(" OR ", propertyTypeConditions) + ")");
            }

            List<string> priceConditions = new List<string>();
            if (CheckBox5.Checked)
            {
                priceConditions.Add("PropertyPrice BETWEEN 0 AND 99.99");
            }
            if (CheckBox6.Checked)
            {
                priceConditions.Add("PropertyPrice BETWEEN 100 AND 199.99");
            }
            if (CheckBox7.Checked)
            {
                priceConditions.Add("PropertyPrice BETWEEN 200 AND 299.99");
            }
            if (CheckBox8.Checked)
            {
                priceConditions.Add("PropertyPrice >= 300");
            }
            if (priceConditions.Any())
            {
                conditions.Add("(" + string.Join(" OR ", priceConditions) + ")");
            }

            if (CheckBox9.Checked)
            {
                conditions.Add("AirCondAvailability = 1");
            }
            if (CheckBox10.Checked)
            {
                conditions.Add("WaterHeaterAvailability = 1");
            }
            if (CheckBox11.Checked)
            {
                conditions.Add("WifiAvailability = 1");
            }

            string query = "SELECT * FROM Property";

            if (conditions.Any())
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            SqlDataSource1.SelectCommand = query;
            Repeater1.DataSource = SqlDataSource1;
            Repeater1.DataBind();

            if (Repeater1.Items.Count == 0)
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
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
    }
}