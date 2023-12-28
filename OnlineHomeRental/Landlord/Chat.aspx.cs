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
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Session_click(object sender, EventArgs e)
        {
            LinkButton user = (LinkButton)sender;
            string ChatSessionId = user.CommandArgument;

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strSelect = "SELECT * FROM ChatMessage WHERE ChatSessionId = @ChatSessionId";

                using (SqlCommand cmdSelect = new SqlCommand(strSelect, con))
                {
                    cmdSelect.Parameters.AddWithValue("@ChatSessionId", ChatSessionId);

                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        MessageRepeater.DataSource = reader;
                        MessageRepeater.DataBind();
                    }
                }
            }
        }
    }
}