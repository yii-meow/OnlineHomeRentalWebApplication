<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/AccountSetting.aspx.cs" Inherits="OnlineHomeRental.Landlord.AccountSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account-setting">
        <div class="container">
            <div class="card">
                <div class="card-body">
                    <!-- Personal Information Section -->
                    <h2 class="card-title">Personal Information</h2>
                    <p class="text-muted">View and update your personal information.</p>

                        <!-- Name -->
                        <div class="form-group">
                            <label for="fullName">Full Name</label>
                            <input type="text" class="form-control" id="fullName" value="John Doe" readonly>
                        </div>

                        <!-- Username -->
                        <div class="form-group">
                            <label for="username">Username</label>
                            <input type="text" class="form-control" id="username" value="johndoe123" readonly>
                        </div>

                        <!-- Email -->
                        <div class="form-group">
                            <label for="email">Email</label>
                            <input type="email" class="form-control" id="email" value="john.doe@example.com" readonly>
                        </div>

                        <button type="button" class="btn btn-save mt-2">Save Changes</button>

                        <!-- Change Password Section -->
                        <h2 class="card-title mt-5">Change Password</h2>
                        <p class="text-muted">Update your account password.</p>

                        <!-- New Password -->
                        <div class="form-group">
                            <label for="newPassword">New Password</label>
                            <div class="input-group">
                                <input type="password" class="form-control" id="newPassword" placeholder="Enter new password">
                                <div class="input-group-append">
                                    <span class="input-group-text">
                                        <i class="fa fa-eye"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <!-- Repeat Password -->
                        <div class="form-group">
                            <label for="repeatPassword">Repeat Password</label>
                            <div class="input-group">
                                <input type="password" class="form-control" id="repeatPassword" placeholder="Repeat new password">
                                <div class="input-group-append">
                                    <span class="input-group-text">
                                        <i class="fa fa-eye"></i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <!-- Save Button -->
                        <button type="button" class="btn btn-save mt-2">Update Password</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
