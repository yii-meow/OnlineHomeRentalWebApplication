﻿<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/Chat.aspx.cs" Inherits="OnlineHomeRental.Landlord.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="chat">
        <div class="chat-container">

            <div class="user-list">
                <asp:Repeater ID="TenantRepeater" runat="server" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <div class="user">
                            <asp:LinkButton ID="btnChatUser" Text='<%# Eval("UserId")%>' runat="server" OnClick="Session_click" CommandArgument='<%# Eval("ChatSessionId") %>' CssClass="userSession_button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="chat-content">
                <div class="messages-container">
                    <asp:Repeater ID="MessageRepeater" runat="server" DataSourceID="">
                        <ItemTemplate>
                            <%# Eval("Sender").ToString() == "Tenant" ? 
                                "<div class='message received'>" :
                                "<div class='message sent'>" %>
                            <p><%# Eval("Message") %></p>
                            <span class="time"><%# Eval("ChatTime") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="message-input">
                    <input type="text" placeholder="Type your message...">
                    <button>Send</button>
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT T.UserId, C.ChatSessionId FROM ChatSession C INNER JOIN Tenant T ON C.TenantId = T.TenantId WHERE LandlordId = @LandlordId ">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <!-- Temporary value -->




</asp:Content>
