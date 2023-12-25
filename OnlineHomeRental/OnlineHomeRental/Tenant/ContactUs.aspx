<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/ContactUs.aspx.cs" Inherits="OnlineHomeRental.Tenant.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-contactus">
        <div class="form">
            <div class="contact-info">
                <h3 class="title">Get in Touch with Us</h3>
                <p class="text justify-text line-spacing">
                    Have a question or need assistance? We're here to help. Drop us a message or give us a call—we're always ready to assist you. Your feedback is valuable to us, so don't hesitate to reach out. Thank you for choosing
                    <span style="color: #00B98E; font-weight: bold;">EZRent.com</span>!
                </p>

                <div class="info">
                    <div class="information">
                        <img src="img/location.png" class="icon-contactus" alt="location" />
                        <p class="p-center">Ez Rent, Jalan Genting Klang</p>
                    </div>
                    <div class="information">
                        <img src="img/email.png" class="icon-contactus" alt="email" />
                        <p class="p-center">homerental@ezrent.com</p>
                    </div>
                    <div class="information">
                        <img src="img/phone.png" class="icon-contactus" alt="phoneNo" />
                        <p class="p-center">+60 3 3234 8692</p>
                    </div>
                </div>

                <div class="social-media">
                    <p>Connect with us :</p>
                    <div class="social-icons">
                        <a href="#">
                            <i class="fab fa-facebook-f"></i>
                        </a>
                        <a href="#">
                            <i class="fab fa-twitter"></i>
                        </a>
                        <a href="#">
                            <i class="fab fa-instagram"></i>
                        </a>
                        <a href="#">
                            <i class="fab fa-linkedin-in"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="contact-form">
                <span class="circle one"></span>
                <span class="circle two"></span>

                <form action="Homepage.aspx" autocomplete="off">
                    <h3 class="title">Contact Us</h3>
                    <div class="input-container">
                        <input type="text" name="name" class="input" />
                        <label for="">Username</label>
                        <span>Username</span>
                    </div>
                    <div class="input-container">
                        <input type="email" name="email" class="input" />
                        <label for="">Email</label>
                        <span>Email</span>
                    </div>
                    <div class="input-container">
                        <input type="tel" name="phone" class="input" />
                        <label for="">Phone</label>
                        <span>Phone</span>
                    </div>
                    <div class="input-container textarea">
                        <textarea name="message" class="input"></textarea>
                        <label for="">Message</label>
                        <span>Message</span>
                    </div>
                    <input type="submit" value="Send" class="btn-contactus" />
                </form>
            </div>
        </div>
    </div>

    <script src="js/ContactUs.js"></script>

</asp:Content>