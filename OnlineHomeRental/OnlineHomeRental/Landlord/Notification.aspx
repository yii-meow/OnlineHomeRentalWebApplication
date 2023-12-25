<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="notification-body">
        <h2>Notifications</h2>

        <ul class="notification-list">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <li class="notification-item">
                        <div class="notification-type"><%#Eval("NotificationType")%></div>
                        <div class="notification-title"><%#Eval("NotificationTitle")%></div>
                        <div class="notification-description"><%#Eval("NotificationDescription")%></div>
                        <div class="notification-date"><%#Eval("NotificationTime")%></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>            
        </ul>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM Notification"></asp:SqlDataSource>
</asp:Content>
