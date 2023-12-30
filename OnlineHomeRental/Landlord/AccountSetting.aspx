﻿<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="AccountSetting.aspx.cs" Inherits="OnlineHomeRental.Landlord.AccountSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="alertDiv" runat="server" class="alert d-none alert-dismissible fade show" role="alert">
        <span id="alertMessage"></span>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>

    <div class="account-setting">
        <div class="container">
            <div class="card">
                <div class="card-body">
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <!-- Personal Information Section -->
                            <h2 class="card-title">Personal Information</h2>
                            <p class="text-muted">View and update your personal information.</p>

                            <!-- Name -->
                            <div class="form-group">
                                <label for="Name">Name</label>
                                <input type="text" class="form-control" id="Name" value='<%# Eval("Name") %>' readonly>
                            </div>

                            <!-- Email -->
                            <div class="form-group">
                                <label for="email">Email</label>
                                <input type="email" class="form-control" id="email" value='<%# Eval("Email") %>' readonly>
                            </div>

                            <!-- Phone No -->
                            <div class="form-group">
                                <label for="phoneNo">Phone No</label>
                                <input type="email" class="form-control" id="phoneNo" value='<%# Eval("PhoneNo") %>' readonly>
                            </div>

                            <!-- Bank Account No -->
                            <div class="form-group">
                                <label for="bankAccount">Bank Account</label>
                                <input type="email" class="form-control" id="bankAccount" value='<%# Eval("BankAccount") != DBNull.Value ? Eval("BankAccount").ToString() : "None" %>' readonly>
                            </div>

                            <button type="button" class="btn btn-save mt-2 w-25" data-toggle="modal" data-target="#editDetailsModal"
                                data-name='<%# Eval("Name") %>'
                                data-email='<%# Eval("Email") %>'
                                data-phone-no='<%# Eval("PhoneNo") %>'
                                data-bank-account='<%# Eval("BankAccount") != DBNull.Value ? Eval("BankAccount").ToString() : "None" %>'>
                                <i class="bi bi-pencil-square text-success text-light mr-3" style="font-size: 1.2em;"></i>Edit Details
                            </button>
                            <p class="text-right">Account created at: <%# Eval("AccountCreatedDate") %></p>
                            <hr />
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Image Section -->
                    <h2 class="card-title mt-4">Profile Image</h2>
                    <p class="text-muted">Update your profile image.</p>
                    <!-- Update Profile Image -->
                    <div class="form-group">
                        <p>Current Profile Image</p>
                        <% 
                            // Retrieve the value of the "UserProfileImage" cookie
                            string userProfileImagePath = Request.Cookies["UserProfileImage"]?.Value;

                            // Check if the cookie exists and has a value
                            if (!string.IsNullOrEmpty(userProfileImagePath))
                            {
                        %>
                        <!-- User Image -->
                        <img src="<%= userProfileImagePath %>" alt="User Image" class="user-image">
                        <%
                            }
                            else
                            {
                                // If the cookie doesn't exist or has no value, provide a default image
                        %>
                        <!-- Default User Image -->
                        <img src="/Data/defaultPP.png" alt="Default User Image" class="user-image">
                        (No Profile Image)
                            <%
                                }
                            %>
                        <p class="mt-3">Choose New Profile Image</p>
                        <asp:FileUpload ID="fileUploadImage" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fileUploadImage" ErrorMessage="Image is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="btnUploadImage" runat="server" Text="Upload" OnClick="btnUploadImage_Click" CssClass="btn btn-primary mt-4 w-25" />
                    </div>
                    <hr />

                    <!-- Change Password Section -->
                    <h2 class="card-title mt-4">Change Password</h2>
                    <p class="text-muted">Update your account password.</p>
                    <!-- New Password -->
                    <div class="form-group">
                        <label for="currentPasword">Current Password</label>
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="tbCurrentPassword" CssClass="form-control" TextMode="Password" placeholder="Enter current password" />
                            <div class="input-group-append">
                                <span class="input-group-text">
                                    <i class="fa fa-eye"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <!-- New Password -->
                    <div class="form-group">
                        <label for="newPassword">New Password</label>
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="tbNewPassword" CssClass="form-control" TextMode="Password" placeholder="Enter new password" />
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
                            <asp:TextBox runat="server" ID="tbNewPasswordRepeat" CssClass="form-control" TextMode="Password" placeholder="Repeat new password" />
                            <div class="input-group-append">
                                <span class="input-group-text">
                                    <i class="fa fa-eye"></i>
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Save Button -->
                    <asp:Button ID="btnUpdatePassword" runat="server" OnClick="btnUpdatePassword_Click" Text="Update Password" CssClass="btn btn-save mt-2" />
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Personal Details Modal -->
    <div class="modal fade" id="editDetailsModal" tabindex="-1" role="dialog" aria-labelledby="editDetailsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editDetailsModalLabel">Edit Details</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-light bg-danger p-2 mb-3" />

                    <!-- Form fields for editing details -->
                    <div class="form-group">
                        <label for="editName">Name</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbEditName" ErrorMessage="Name is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="tbEditName" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="form-group">
                        <label for="editEmail">Email</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEditEmail" ErrorMessage="Email is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="tbEditEmail" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="form-group">
                        <label for="editPhoneNo">Phone No</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbEditPhoneNo" ErrorMessage="Phone No is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="tbEditPhoneNo" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="form-group">
                        <label for="editBankAccount">Bank Account</label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBankAccount" runat="server"
                            ControlToValidate="tbEditBankAccount"
                            ErrorMessage="Bank Account Number must be 12 digits."
                            ForeColor="Red"
                            ValidationExpression="^\d{12}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:TextBox runat="server" ID="tbEditBankAccount" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSavePersonalDetails" type="button" class="btn btn-primary" OnClick="btnSavePersonalDetails_Click" Text="Save Changes" />
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM [User] INNER JOIN Landlord ON [User].UserId = Landlord.UserId WHERE [User].UserId = @UserId">
        <SelectParameters>
            <asp:SessionParameter Name="UserId" SessionField="UserId" Type="string" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script>
        // Preset Edit Personal Details textboxes value
        $('#editDetailsModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal

            // Extract data from attributes of the triggering button
            var name = button.data('name');
            var email = button.data('email');
            var phoneNo = button.data('phone-no');
            var bankAccount = button.data('bank-account');

            // Set the values in the modal textboxes
            $('#tbEditName').val(name);
            $('#tbEditEmail').val(email);
            $('#tbEditPhoneNo').val(phoneNo);
            $('#tbEditBankAccount').val(bankAccount);
        });
    </script>
</asp:Content>
