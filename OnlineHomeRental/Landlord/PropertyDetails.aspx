<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/PropertyDetails.aspx.cs" Inherits="OnlineHomeRental.Landlord.PropertyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="morePropertyDetails">
        <div class="mt-1">
            <div class="container">
                <h1><%# Eval("PropertyName") %></h1>
                <p class="mb-4">Ratings: ⭐⭐⭐⭐⭐</p>
                <div class="property-images">
                    <!-- Add a carousel or image slider for property images -->
                    <!-- Use a JavaScript library like Bootstrap Carousel -->
                    <img src="/Data/Property.jpg" />
                </div>

                <div class="property-details-container">
                    <div class="property-details">
                        <div class="main-details">
                            <p class="mb-4">
                                Property ID:
                                <asp:Label ID="lblPropertyId" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Address:
                                <asp:Label ID="lblPropertyAddress" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Type:
                                <asp:Label ID="lblPropertyType" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Price: RM
                                <asp:Label ID="lblPropertyPrice" runat="server" Text="Label"></asp:Label>
                                / night
                            </p>
                        </div>

                        <div class="property-description">
                            <p class="mb-4">
                                <b><u>Description</u></b><br />
                                <br />
                                <asp:Label ID="lblListingDescription" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        
                        <div class="property-description">
                            <p class="mb-4">
                                <b><u>Preferences</u></b><br />
                                <br />
                                <asp:Label ID="lblPreferences" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>

                    <div>
                        <div class="property-moreDetails">
                            <p class="mb-4">
                                Number of Bedrooms:
                                <asp:Label ID="lblNumberOfBedroom" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Area:
                                <asp:Label ID="lblArea" runat="server" Text="Label"></asp:Label>
                                ft&sup2;
                            </p>
                            <p class="mb-4">
                                Air Conditioning:
                                <asp:Label ID="lblAirCondAvailability" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Water Heater:
                                <asp:Label ID="lblWaterHeaterAvailability" runat="server" Text="Label"></asp:Label>
                            </p>
                            <p class="mb-4">
                                Wi-Fi:
                                <asp:Label ID="lblWifiAvailability" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
