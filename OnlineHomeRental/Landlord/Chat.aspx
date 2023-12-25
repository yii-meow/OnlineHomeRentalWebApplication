<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/Chat.aspx.cs" Inherits="OnlineHomeRental.Landlord.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="chat">
        <div class="chat-container">
            <div class="user-list">
                <div class="user bg-primary">Tenant 1</div>
                <div class="user">Tenant 2</div>
                <div class="user">Tenant 3</div>
                <div class="user">Tenant 1</div>
                <div class="user">Tenant 2</div>
                <div class="user">Tenant 3</div>
                <div class="user">Tenant 1</div>
                <div class="user">Tenant 2</div>
                <div class="user">Tenant 3</div>
                <div class="user">Tenant 1</div>
                <div class="user">Tenant 2</div>
                <div class="user">Tenant 3</div>
                <div class="user">Tenant 1</div>
                <div class="user">Tenant 2</div>
                <div class="user">Tenant 3</div>
            </div>

            <div class="chat-content">
                <div class="messages-container">
                    <div class="message received">
                        <p>Hello there!</p>
                        <span class="time">10:30 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message received">
                        <p>Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!Hello there!</p>
                        <span class="time">10:30 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>

                    <div class="message sent">
                        <p>Hi! How can I help you?</p>
                        <span class="time">10:35 AM</span>
                    </div>
                </div>

                <div class="message-input">
                    <input type="text" placeholder="Type your message...">
                    <button>Send</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
