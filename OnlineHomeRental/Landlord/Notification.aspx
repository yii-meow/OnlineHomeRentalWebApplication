<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="OnlineHomeRental.Landlord.Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="notification-body">
        <h2>Notifications</h2>

        <ul class="notification-list">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <li class="notification-item mt-4">
                        <%--<div class="notification-type"><%#Eval("NotificationType")%></div>--%>
                        <div class="notification-title"><%#Eval("NotificationTitle")%></div>
                        <div class="notification-description"><%#Eval("NotificationDescription")%></div>
                        <div class="notification-date"><%#Eval("NotificationTime")%></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

        <%-- If there is no any notification --%>
        <asp:Label ID="lblNoNotification" runat="server" Text="<br/><h5>No notification so far...</h5>" Visible="false"></asp:Label>
    </div>
</asp:Content>
