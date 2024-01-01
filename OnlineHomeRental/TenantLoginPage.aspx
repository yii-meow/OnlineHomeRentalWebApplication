﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantLoginPage.aspx.cs" Inherits="OnlineHomeRental.Tenant.TenantLoginPage" %>

<!doctype html>
<html lang="en">
<head>
    <title>OnlineHomeRental</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="stylesheet" href="https://unicons.iconscout.com/release/v2.1.9/css/unicons.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="/CSS/LandlordLP.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="section">
            <div class="container">
                <div class="row full-height justify-content-center">
                    <div class="col-12 text-center py-5">
                        <div class="section pb-5 pt-5 pt-sm-2 text-center">
                            <h2 class="mb-0 pb-2">Tenant Portal</h2>
                            <h6 class="mb-0 pb-3"><span>Log In </span><span>Sign Up</span></h6>
                            <input class="checkbox" type="checkbox" id="reg-log" name="reg-log" />
                            <label for="reg-log"></label>
                            <div class="card-3d-wrap mx-auto">
                                <asp:ValidationSummary ID="ValidationLoginSummary" runat="server" CssClass="text-light bg-danger p-2 mb-3" ValidationGroup="ValidationLogin" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-light bg-danger p-2 mb-3" ValidationGroup="ValidationRegister" />
                                <asp:Label ID="lblError" runat="server" CssClass="text-light bg-danger mb-3 text-lg" />

                                <div class="card-3d-wrapper">
                                    <div class="card-front">
                                        <div class="center-wrap">
                                            <div class="dropdown">
                                                <!-- The button with the icon -->
                                                <button type="button" class="dropdown-button" onclick="toggleDropdown()">
                                                    <i class="input-icon uil uil-user icon"></i>
                                                    <!-- The dropdown content -->
                                                    <div class="dropdown-content" id="myDropdown" style="display: none; left: -150px; top: -35px;">
                                                        <!-- Dropdown items -->
                                                        <asp:HyperLink ID="Landlord_Login_Link" runat="server" NavigateUrl="~/LandlordLoginPage.aspx" Text="Landlord" CssClass="dropdown-item" />
                                                        <asp:HyperLink ID="Tenant_Login_Link" runat="server" NavigateUrl="~/TenantLoginPage.aspx" Text="Tenant" CssClass="dropdown-item" />
                                                    </div>
                                                </button>
                                            </div>
                                            <div class="section text-center">
                                                <h4 class="mb-4 pb-3">Log In</h4>
                                                <div class="form-group">
                                                    <asp:TextBox CssClass="form-style" ID="tbTenantUsername" runat="server" placeholder="Username" />
                                                    <i class="input-icon uil uil-at"></i>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbTenantUsername" ErrorMessage="Username is Required." ForeColor="Red" ValidationGroup="ValidationLogin">*</asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group mt-2">
                                                    <asp:TextBox CssClass="form-style" ID="tbTenantPassword" runat="server" placeholder="Password" TextMode="Password" />
                                                    <i class="input-icon uil uil-lock-alt"></i>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbTenantPassword" ErrorMessage="Password is Required." ForeColor="Red" ValidationGroup="ValidationLogin">*</asp:RequiredFieldValidator>
                                                </div>

                                                <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn mt-4" OnClick="Login_Click" Text="Login" ValidationGroup="ValidationLogin" />

                                                <p class="mb-0 mt-4 text-center">
                                                    <asp:HyperLink ID="tenant_forgetPassword_link" runat="server" NavigateUrl="~/TenantForgetPassword.aspx" Text="Forget your password?" CssClass="link" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-back">
                                        <div class="center-wrap">
                                            <div class="section text-center">
                                                <h4 class="mb-3 pb-3 mt-3">Sign Up</h4>
                                                <div class="form-group">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantUsername" runat="server" placeholder="Username" title="Username must contains 8 or more characters" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbRegTenantUsername" ErrorMessage="Username is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbRegTenantUsername"
                                                        ErrorMessage="Username must contain 8 or more characters." ForeColor="Red"
                                                        ValidationExpression="^.{8,}$" Display="Dynamic"
                                                        ValidationGroup="ValidationRegister"></asp:RegularExpressionValidator>
                                                    <i class="input-icon uil uil-user"></i>
                                                </div>
                                                <div class="form-group mt-3">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantPassword" runat="server" placeholder="Password" TextMode="Password" title="Password must be at least 8 characters long and contain both alphabetic and numeric characters." />
                                                    <i class="input-icon uil uil-lock-alt"></i>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbRegTenantPassword" ErrorMessage="Password is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbRegTenantPassword" ControlToCompare="tbRegTenantUsername" Type="String" Operator="Equal" ErrorMessage="Password cannot match with username." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:CompareValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="tbRegTenantPassword"
                                                        ErrorMessage="Password must be at least 8 characters long and contain both alphabetic and numeric characters."
                                                        ForeColor="Red" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d).{8,}$" ValidationGroup="ValidationRegister">*</asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group mt-3">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantConfirmedPassword" runat="server" placeholder="Confirmed Password" TextMode="Password" title="Confirmed password must matched with the password" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbRegTenantConfirmedPassword" ErrorMessage="Confirmed Password is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <i class="input-icon uil uil-lock-alt"></i>
                                                </div>
                                                <div class="form-group mt-3">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantName" runat="server" placeholder="Name" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbRegTenantName" ErrorMessage="Name is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <i class="input-icon uil uil-chat-bubble-user"></i>
                                                </div>
                                                <div class="form-group gender-radio mt-3">
                                                    <asp:RadioButtonList CssClass="form-style gender-radio" ID="rbRegTenantGender" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Male" Value="M" />
                                                        <asp:ListItem Text="Female" Value="F" CssClass="female" />
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rbRegTenantGender" ErrorMessage="Gender is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <i class="input-icon uil uil-mars"></i>
                                                </div>
                                                <div class="form-group mt-3">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantPhoneNo" runat="server" placeholder="Phone Number" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbRegTenantPhoneNo" ErrorMessage="Phone No is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server" ControlToValidate="tbRegTenantPhoneNo"
                                                        ErrorMessage="Enter a valid Malaysian phone number" ForeColor="Red"
                                                        ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$" ValidationGroup="ValidationRegister">*</asp:RegularExpressionValidator>
                                                    <i class="input-icon uil-phone"></i>
                                                </div>
                                                <div class="form-group mt-3">
                                                    <asp:TextBox CssClass="form-style" ID="tbRegTenantEmail" runat="server" placeholder="Email" title="Enter your email address" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbRegTenantEmail" ErrorMessage="Email is Required." ForeColor="Red" ValidationGroup="ValidationRegister">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="tbRegTenantEmail"
                                                        ErrorMessage="Email Address is not Valid." ForeColor="Red"
                                                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                        ValidationGroup="ValidationRegister">*</asp:RegularExpressionValidator>
                                                    <i class="input-icon uil uil-fast-mail"></i>
                                                </div>
                                                <asp:LinkButton runat="server" ID="RegLink" OnClick="Register_Click" class="btn mt-4" Text="Register" ValidationGroup="ValidationRegister" />
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