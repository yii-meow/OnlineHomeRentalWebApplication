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

                <hr />

                <div class="propertyBookingDetails">
                    <h3>Booking Details</h3>

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="BookingId" ForeColor="#333333" GridLines="None" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="BookingId" HeaderText="Booking Id" InsertVisible="False" ReadOnly="True" SortExpression="BookingId" />
                            <asp:BoundField DataField="CheckInDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Check In Date" SortExpression="CheckInDate" />
                            <asp:BoundField DataField="CheckOutDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Check Out Date" SortExpression="CheckOutDate" />
                            <asp:BoundField DataField="BookingStatus" HeaderText="Booking Status" SortExpression="Booking Status" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        <EmptyDataTemplate>
                            <h3>No Bookings so far...</h3>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM Booking INNER JOIN Tenant ON Booking.TenantId = Tenant.TenantId WHERE LandlordId = @LandlordId AND PropertyId = @PropertyId" >
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
            <asp:QueryStringParameter Name="PropertyId" QueryStringField="PropertyId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
