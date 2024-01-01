<%@ Page Language="C#" MasterPageFile="~/Tenant/TenantMenu.Master" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="OnlineHomeRental.Tenant.PersonalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="alertDiv" runat="server" class="alert d-none alert-dismissible fade show" role="alert">
        <span id="alertMessage"></span>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>

    <div class="bankCards">
        <h3>Bank Cards</h3>
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addBankCardModal">Add Bank Card</button>

        <div class="bankCardDetails">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="bankCard">
                        <div class="bankCardDetails">
                            <label>Name :</label>
                            <span id="name"><%# Eval("CardHolderName") %></span>
                            <br />

                            <label>Card Number :</label>
                            <span id="formattedCardNumber"><%# "**** **** **** " + Eval("CardNumber").ToString().Substring(Eval("CardNumber").ToString().Length - 4) %></span>
                            <br />

                            <label>Card Expiry Date :</label>
                            <span id="expiryDate"><%# ((DateTime)Eval("CardExpiryDate")).ToString("MM/yyyy") %></span>
                            <br />

                            <label>CVV :</label>
                            <span id="cvv"><%# Eval("CVV") %></span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <!-- Add Bank Card Modal -->
    <div class="modal fade" id="addBankCardModal" tabindex="-1" role="dialog" aria-labelledby="addBankCardModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addBankCardModalLabel">Request Maintenance</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary ID="ValidationMaintenanceRequestSummary" runat="server" CssClass="text-light bg-danger p-2 mb-3" />
                    <div class="form-group">
                        <label for="description">Card Holder Name</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbCardHolderName" ErrorMessage="Card Holder Name is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbCardHolderName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="description">Card Number</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbCardNumber" ErrorMessage="Card Number is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidatorCardNumber" runat="server"
                            ControlToValidate="tbCardNumber"
                            ValidationExpression="^\d{16}$"
                            ErrorMessage="Please enter a valid 16-digit card number"
                            ForeColor="Red"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="tbCardNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="description">Card Expiry Date (MM/YYYY)</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbCardExpiry" ErrorMessage="Card Expiry Date is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidatorExpiryDate" runat="server"
                            ControlToValidate="tbCardExpiry"
                            ValidationExpression="^(0[1-9]|1[0-2])\/20[2-9][0-9]$"
                            ErrorMessage="Please enter a valid MM/YYYY format"
                            ForeColor="Red"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="customValidatorExpiryDate" runat="server"
                            ControlToValidate="tbCardExpiry"
                            OnServerValidate="customValidatorExpiryDate_ServerValidate"
                            ErrorMessage="Expiry date should be greater than or equal to the current date"
                            ForeColor="Red"
                            Display="Dynamic">
                        </asp:CustomValidator>
                        <asp:TextBox ID="tbCardExpiry" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="description">Card CVV: (3 digits)</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbCVV" ErrorMessage="Card CVV is Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidatorCVV" runat="server"
                            ControlToValidate="tbCVV"
                            ValidationExpression="^\d{3}$"
                            ErrorMessage="Please enter a valid 3-digit CVV"
                            ForeColor="Red"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="tbCVV" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnRequestMaintenance" type="button" class="btn btn-primary" Text="Save Changes" OnClick="btnAddBankCard_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM BankCardDetails WHERE TenantId = @TenantId">
        <SelectParameters>
            <asp:SessionParameter Name="TenantId" SessionField="TenantId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
