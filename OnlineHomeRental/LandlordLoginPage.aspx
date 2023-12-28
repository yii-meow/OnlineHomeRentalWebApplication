﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandlordLoginPage.aspx.cs" Inherits="OnlineHomeRental.Landlord.LandlordLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Landlord Login Page</title>
    <link rel="stylesheet" href="https://unicons.iconscout.com/release/v2.1.9/css/unicons.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/CSS/LandLordLP.css" />
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="section">
                <div class="container">
                    <div class="row full-height justify-content-center">
                        <div class="col-12 text-center py-5">
                            <div class="section pb-5 pt-5 pt-sm-2 text-center">
                                <h2 class="mb-2 pb-2">Landlord Portal</h2>
                                <h6 class="mb-0 pb-3"><span>Log In </span><span>Sign Up</span></h6>
                                <input class="checkbox" type="checkbox" id="reg-log" name="reg-log" />
                                <label for="reg-log"></label>
                                <div class="card-3d-wrap mx-auto">
                                    <div class="card-3d-wrapper">
                                        <div class="card-front">
                                            <div class="center-wrap">
                                                <div class="dropdown">
                                                    <!-- The button with the icon -->
                                                    <button class="dropdown-button" onclick="toggleDropdown()">
                                                        <i class="input-icon uil uil-user icon"></i>
                                                        <!-- The dropdown content -->
                                                        <div class="dropdown-content" id="myDropdown" style="display: none; left: -150px; top: -35px;">
                                                            <!-- Dropdown items -->
                                                            <asp:HyperLink ID="Landlord_Login_Link" runat="server" NavigateUrl="~/LandlordLoginPage.aspx" Text="Landlord" CssClass="dropdown-item" />
                                                            <asp:HyperLink ID="Tenant_Login_Link" runat="server" NavigateUrl="~/LandlordLoginPage.aspx" Text="Tenant" CssClass="dropdown-item" />
                                                        </div>
                                                    </button>
                                                </div>
                                                <div class="section text-center">
                                                    <asp:Label ID="lblError" runat="server" />
                                                    <h4 class="mb-4 pb-3">Log In</h4>

                                                    <div class="form-group">
                                                        <asp:TextBox CssClass="form-style" ID="tbLandlordUsername" runat="server" placeholder="Username" />
                                                        <i class="input-icon uil uil-at"></i>
                                                    </div>

                                                    <div class="form-group mt-2">
                                                        <asp:TextBox CssClass="form-style" ID="tbLandlordPassword" runat="server" placeholder="Password" TextMode="Password" />
                                                        <i class="input-icon uil uil-lock-alt"></i>
                                                    </div>

                                                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn mt-4" OnClick="Login_Click" Text="Login" />

                                                    <p class="mb-0 mt-4 text-center">
                                                        <asp:HyperLink ID="tenant_forgetPassword_link" runat="server" NavigateUrl="~/TenantForgetPassword.aspx" Text="Forget your password?" CssClass="link" />
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Registration Page -->
                                        <div class="card-back">
                                            <div class="center-wrap">
                                                <div class="section text-center">
                                                    <h4 class="mb-3 pb-3 mt-3">Sign Up</h4>
                                                    <div class="form-group">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordUsername" runat="server" placeholder="Username" title="Username must contains 8 or more characters" />
                                                        <i class="input-icon uil uil-user"></i>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordPassword" runat="server" placeholder="Password" TextMode="Password" title="Password must contains 10 or more characters including alphanumeric characters" />
                                                        <i class="input-icon uil uil-lock-alt"></i>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordConfirmedPassword" runat="server" placeholder="Confirmed Password" TextMode="Password" title="Confirmed password must matched with the password" />
                                                        <i class="input-icon uil uil-lock-alt"></i>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordName" runat="server" placeholder="Name" />
                                                        <i class="input-icon uil uil-chat-bubble-user"></i>
                                                    </div>
                                                    <div class="form-group gender-radio mt-3">
                                                        <asp:RadioButtonList CssClass="form-style gender-radio" ID="rbRegLandlordGender" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Male" Value="M" />
                                                            <asp:ListItem Text="Female" Value="F" CssClass="female"/>
                                                        </asp:RadioButtonList>
                                                        <i class="input-icon uil uil-mars"></i>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordPhoneNo" runat="server" placeholder="Phone Number" />
                                                        <i class="input-icon uil-phone"></i>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <asp:TextBox CssClass="form-style" ID="tbRegLandlordEmail" runat="server" placeholder="Email" title="Enter your email address" />
                                                        <i class="input-icon uil uil-fast-mail"></i>
                                                    </div>
                                                    <asp:LinkButton runat="server" ID="RegLink" OnClick="Register_Click" class="btn mt-4" Text="Register" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        // JavaScript to toggle the dropdown
        function toggleDropdown() {
            var dropdown = document.getElementById("myDropdown");
            dropdown.style.display = (dropdown.style.display === "block") ? "none" : "block";
        }

        // Close the dropdown if the user clicks outside of it
        window.onclick = function (event) {
            if (!event.target.matches('.icon') && !event.target.matches('.dropdown-button')) {
                var dropdowns = document.getElementsByClassName("dropdown-content");
                for (var i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.style.display === "block") {
                        openDropdown.style.display = "none";
                    }
                }
            }
        }
    </script>
</body>
</html>