<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/AddProperty.aspx.cs" Inherits="OnlineHomeRental.Landlord.AddProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="addProperty">
        <div id="alertDiv" runat="server" class="alert d-none alert-dismissible fade show" role="alert">
            <!-- Alert content will be updated dynamically -->
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span id="alertMessage"></span>
                <span aria-hidden="true">&times;</span>
            </button>
        </div>

        <h2>Select Property Type</h2>

        <div class="property-cards">
            <div class="addPropertyForm">
                <h2>Add Property</h2>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <div>
                    <label for="txtPropertyName">Property Name:</label>
                    <asp:TextBox ID="txtPropertyName" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label for="ddlPropertyType">Property Type:</label>
                    <asp:DropDownList ID="ddlPropertyType" runat="server">
                        <asp:ListItem Text="Residential" Value="Residential"></asp:ListItem>
                        <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
                        <asp:ListItem Text="Apartment" Value="Apartment"></asp:ListItem>
                        <asp:ListItem Text="Condominium" Value="Condominium"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <label for="txtPropertyAddress">Property Address:</label>
                    <asp:TextBox ID="txtPropertyAddress" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label for="txtListingDescription">Listing Description:</label>
                    <asp:TextBox ID="txtListingDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div>
                    <label for="txtPropertyPrice">Property Price:</label>
                    <asp:TextBox ID="txtPropertyPrice" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnAddProperty" runat="server" Text="Add Property" OnClick="btnAddProperty_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
