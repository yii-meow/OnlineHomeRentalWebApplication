<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/AddProperty.aspx.cs" Inherits="OnlineHomeRental.Landlord.AddProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="addProperty">
        <div id="alertDiv" runat="server" class="alert d-none alert-dismissible fade show" role="alert">
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
                    <label for="txtPropertyPrice">Number of bedroom:</label>
                    <asp:DropDownList ID="ddlNumberOfBedroom" runat="server"></asp:DropDownList>
                </div>

                <div>
                    <label for="txtAreaSqft">Area in Sqft:</label>
                    <asp:TextBox ID="txtAreaSqft" runat="server"></asp:TextBox>
                </div>

                <div class="amenities">
                    <asp:CheckBox ID="cbAirConditioning" runat="server" Text="Air Conditioning" />
                    <asp:CheckBox ID="cbWaterHeater" runat="server" Text="Water Heater" />
                    <asp:CheckBox ID="cbWiFi" runat="server" Text="Wi-Fi" />
                </div>

                <div>
                    <label for="txtPreferences">Preferences:</label>
                    <asp:TextBox ID="txtPreferences" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>

                <hr />
                <div>
                    <h3>Image Section</h3>
                    <div class="images_upload">
                        <div class="thumbnail_upload">
                            <label for="thumbnailUpload">Choose Thumbnail:</label>
                            <asp:FileUpload ID="thumbnailUpload" runat="server" />
                        </div>
                        <div class="image1_upload">
                            <label for="image1Upload">Choose Image 1:</label>
                            <asp:FileUpload ID="image1Upload" runat="server" />
                        </div>
                        <div class="image2_upload">
                            <label for="image2Upload">Choose Image 2:</label>
                            <asp:FileUpload ID="image2Upload" runat="server" />
                        </div>
                        <div class="image3_upload">
                            <label for="image3Upload">Choose Image 3:</label>
                            <asp:FileUpload ID="image3Upload" runat="server" />
                        </div>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnAddProperty" runat="server" Text="Add Property" OnClick="btnAddProperty_Click" CssClass="btn btn-primary"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
