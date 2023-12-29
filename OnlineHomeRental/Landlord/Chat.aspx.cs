﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
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
            string chatSessionId = ((LinkButton)sender).CommandArgument;
            Bind_Message(chatSessionId);
        }

        private void Bind_Message(string ChatSessionId)
        {
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
            btnSendMessage.CommandArgument = ChatSessionId;
        }

        protected void Send_Message(object sender, EventArgs e)
        {
            Button btnSendMessage = (Button)sender;
            string chatSessionId = btnSendMessage.CommandArgument;

            // Get the message from the TextBox
            string message = lblSendMessage.Text.Trim();

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                string strSendMessage = "INSERT INTO ChatMessage(ChatSessionId,Message,SenderType,ChatTime) VALUES(@ChatSessionId,@Message,@SenderType,@ChatTime)";

                using (SqlCommand cmdSendMessage = new SqlCommand(strSendMessage, con))
                {
                    cmdSendMessage.Parameters.AddWithValue("@ChatSessionId", chatSessionId);
                    cmdSendMessage.Parameters.AddWithValue("@Message", message);
                    cmdSendMessage.Parameters.AddWithValue("@SenderType", "Landlord");
                    cmdSendMessage.Parameters.AddWithValue("@ChatTime", DateTime.Now);

                    cmdSendMessage.ExecuteNonQuery();
                }
            }
            Bind_Message(chatSessionId);
            lblSendMessage.Text = "";
        }
    }
}