<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/ViewProperties.aspx.cs" Inherits="OnlineHomeRental.Tenant.ViewProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <head>
        <title>EZRent.com | View Properties</title>
        <link rel="stylesheet" href="/css/ViewProperties.css" />
    </head>

    <div class="container-viewproperties">
        <div class="column-left">
            <h1>Property Listing</h1>
            <p>
                Browse through detailed property listings offering a variety of living spaces.<br />
                Find your perfect home hassle-free with detailed information and images.
            </p>

            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="text-start mx-auto wow slideInLeft" data-wow-delay="0.1s">
                        <div class="property-viewproperties">
                            <div class="property-img-viewproperties">
                                <img src="<%# Eval("Thumbnail") %>" alt="property" />
                            </div>
                            <div class="property-detail-viewproperties">
                                <p class="p-viewproperties"><%# Eval("PropertyType") %></p>
                                <a href="PropertyDetail.aspx?PropertyId=<%# Eval("PropertyId") %>">
                                    <h3><%# Eval("PropertyName") %></h3>
                                </a>
                                <p><%# Eval("PropertyAddress") %></p>
                                <p>
                                    <%# Eval("NumberOfBedroom") %> Bedroom /
                                    <%# Convert.ToInt32(Eval("AreaSqft")) %> Sqft
                                </p>
                                <p>
                                    <%# GetIcon(Convert.ToBoolean(Eval("AirCondAvailability"))) %>Air Conditional
                                </p>
                                <p>
                                    <%# GetIcon(Convert.ToBoolean(Eval("WaterHeaterAvailability"))) %>Water Heater
                                </p>
                                <p>
                                    <%# GetIcon(Convert.ToBoolean(Eval("WifiAvailability"))) %>Wifi
                                </p>
                                <div class="property-price-viewproperties">
                                    <h4>RM <%# Eval("PropertyPrice") %> <span>per day</span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Property]"></asp:SqlDataSource>

            <asp:Panel ID="Panel1" runat="server" Visible="false">
                <div style="background-color: rgb(223, 240, 216); width: 100%;">
                    <p style="color: rgb(60, 118, 61); padding: 15px;">No property found.</p>
                </div>
            </asp:Panel>
        </div>
        <div class="column-right">
            <div class="checkboxlist-viewproperties">
                <h2>Filter & Search Properties</h2>

                <h3><u>Property Type</u></h3>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Residential" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblResidential" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Flat" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblFlat" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Apartment" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblApartment" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Condominium" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblCondominium" runat="server" Text="0"></asp:Label>)</span>
                </div>

                <h3><u>Property Price</u></h3>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox5" runat="server" Text="RM 0 - RM 99" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblPriceRange1" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox6" runat="server" Text="RM 100 - RM 199" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblPriceRange2" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox7" runat="server" Text="RM 200 - RM 299" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblPriceRange3" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox8" runat="server" Text="RM 300 and above" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblPriceRange4" runat="server" Text="0"></asp:Label>)</span>
                </div>

                <h3><u>Amenities</u></h3>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox9" runat="server" Text="Air Conditional" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblAirCond" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox10" runat="server" Text="Water Heater" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblWaterHeater" runat="server" Text="0"></asp:Label>)</span>
                </div>
                <div class="filter-search-viewproperties">
                    <asp:CheckBox ID="CheckBox11" runat="server" Text="Wifi" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                    <p></p>
                    <span>(<asp:Label ID="lblWifi" runat="server" Text="0"></asp:Label>)</span>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
