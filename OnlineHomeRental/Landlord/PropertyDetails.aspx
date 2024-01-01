<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/PropertyDetails.aspx.cs" Inherits="OnlineHomeRental.Landlord.PropertyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="alertDiv" runat="server" class="alert d-none alert-dismissible fade show" role="alert">
        <span id="alertMessage"></span>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <div class="morePropertyDetails">
        <div class="mt-1">
            <div class="container">
                <div>
                    <p class="mb-4">
                        Average Ratings:
                        <asp:Literal ID="lblAverageRatings" runat="server" />
                    </p>
                </div>
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <button type="button" class="btn btn-primary viewRatingButton" data-toggle="modal" data-target="#viewRatingsModal">View Ratings</button>

                        <h2><%# Eval("PropertyName") %></h2>
                        <hr />
                        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                            <ol class="carousel-indicators">
                                <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                                <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                                <%# !Convert.IsDBNull(Eval("Image2")) ? "<li data-target='#carouselExampleIndicators' data-slide-to='2'></li>" : "" %>
                                <%# !Convert.IsDBNull(Eval("Image3")) ? "<li data-target='#carouselExampleIndicators' data-slide-to='3'></li>" : "" %>
                            </ol>
                            <div class="carousel-inner">
                                <div class="carousel-item active">
                                    <img class="d-block w-100" src='<%# Eval("Thumbnail") %>' alt="First slide">
                                </div>
                                <div class="carousel-item">
                                    <img class="d-block w-100" src='<%# Eval("Image1") %>' alt="Second slide">
                                </div>
                                <%# !Convert.IsDBNull(Eval("Image2")) ? "<div class='carousel-item'><img class='d-block w-100' src='" + Eval("Image2") + "' alt='Third slide'></div>" : "" %>
                                <%# !Convert.IsDBNull(Eval("Image3")) ? "<div class='carousel-item'><img class='d-block w-100' src='" + Eval("Image3") + "' alt='Fourth slide'></div>" : "" %>
                            </div>
                            <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>

                        <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                        </div>

                        <div class="property-details-container">
                            <div class="property-details">
                                <div class="main-details">
                                    <p class="mb-4">
                                        Property ID:
                                        <asp:Label ID="lblPropertyId" runat="server" Text='<%# Eval("PropertyId") %>' />
                                    </p>
                                    <p class="mb-4">
                                        Address:
                                        <asp:Label ID="lblPropertyAddress" runat="server" Text='<%# Eval("PropertyAddress") %>'></asp:Label>
                                    </p>
                                    <p class="mb-4">
                                        Type:
                                <asp:Label ID="lblPropertyType" runat="server" Text='<%# Eval("PropertyType") %>'></asp:Label>
                                    </p>
                                    <p class="mb-4">
                                        Price: RM
                                <asp:Label ID="lblPropertyPrice" runat="server" Text='<%# Eval("PropertyPrice") %>'></asp:Label>
                                        / night
                                    </p>
                                </div>

                                <div class="property-description">
                                    <p class="mb-4">
                                        <b><u>Description</u></b><br />
                                        <br />
                                        <asp:Label ID="lblListingDescription" runat="server" Text='<%# Eval("ListingDescription") %>'></asp:Label>
                                    </p>
                                </div>

                                <div class="property-preferences">
                                    <p class="mb-4">
                                        <b><u>Preferences</u></b><br />
                                        <br />
                                        <asp:Label ID="lblPreferences" runat="server" Text='<%# Eval("Preferences") %>'></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="position-relative">
                                <div class="property-moreDetails mb-4">
                                    <p class="mb-4">
                                        Number of Bedrooms:
                                <asp:Label ID="lblNumberOfBedroom" runat="server" Text='<%# Eval("NumberOfBedroom") %>'></asp:Label>
                                    </p>
                                    <p class="mb-5">
                                        Area:
                                <asp:Label ID="lblArea" runat="server" Text='<%# Eval("AreaSqft") %>'></asp:Label>
                                        ft&sup2;
                                    </p>
                                    <p class="mt-3 mb-2">
                                        <div class="amenitiesLabel">
                                            <i class="bi bi-fan text-info"></i>
                                            <div>Air Conditioning</div>
                                        </div>
                                        <asp:Label ID="lblAirCondAvailability" runat="server"
                                            Text='<%# (bool)Eval("AirCondAvailability") ? "<i class=\"bi bi-check-circle-fill text-success amenities\"></i>" : "<i class=\"bi bi-x-circle-fill text-danger amenities\"></i>" %>'></asp:Label>
                                    </p>
                                    <p class="mb-4">
                                        <div class="amenitiesLabel">
                                            <i class="bi bi-droplet-fill text-primary"></i>
                                            <div>Water Heater</div>
                                        </div>
                                        <asp:Label ID="lblWaterHeaterAvailability" runat="server"
                                            Text='<%# (bool)Eval("WaterHeaterAvailability") ? "<i class=\"bi bi-check-circle-fill text-success amenities\"></i>" : "<i class=\"bi bi-x-circle-fill text-danger amenities\"></i>" %>'></asp:Label>
                                    </p>
                                    <p class="mb-4">
                                        <div class="amenitiesLabel">
                                            <i class="bi bi-wifi text-success"></i>
                                            <div>Wi-Fi</div>
                                        </div>
                                        <asp:Label ID="lblWifiAvailability" runat="server"
                                            Text='<%# (bool)Eval("WifiAvailability") ? "<i class=\"bi bi-check-circle-fill text-success amenities\"></i>" : "<i class=\"bi bi-x-circle-fill text-danger amenities\"></i>" %>'></asp:Label>
                                    </p>
                                </div>

                                <button type="button" class="btn btn-info w-100 mt-5" data-toggle="modal" data-target="#requestMaintenanceModal"
                                    data-property-name='<%# Eval("PropertyName") %>'>
                                    <i class="bi bi-telephone-fill text-success text-light mr-3" style="font-size: 1.2em;"></i>Request Maintenance
                                </button>

                                <button type="button" class="btn btn-success w-100 editPropertyDetailsButton mt-4" data-toggle="modal" data-target="#editPropertyDetailsModal"
                                    data-property-name='<%# Eval("PropertyName") %>'
                                    data-property-address='<%# Eval("PropertyAddress") %>'
                                    data-property-type='<%# Eval("PropertyType") %>'
                                    data-property-price='<%# Eval("PropertyPrice") %>'
                                    data-listing-description='<%# Eval("ListingDescription") %>'
                                    data-preferences='<%# Eval("Preferences") %>'
                                    data-number-of-bedroom='<%# Eval("NumberOfBedroom") %>'
                                    data-area-sqft='<%# Eval("AreaSqft") %>'
                                    data-air-cond-availability='<%# Eval("AirCondAvailability") %>'
                                    data-water-heater-availability='<%# Eval("WaterHeaterAvailability") %>'
                                    data-wifi-availability='<%# Eval("WifiAvailability") %>'>
                                    <i class="bi bi-pencil-square text-success text-light mr-3" style="font-size: 1.2em;"></i>Edit Property Details
                                </button>

                                <asp:LinkButton ID="btnDeleteProperty" runat="server" CssClass="btn btn-danger w-100 deletePropertyDetailsButton mt-4" OnClick="btnDeleteProperty_Click" OnClientClick="return Confirm();" CommandArgument='<%# Eval("PropertyId") %>'>
                                    <i class="bi bi-trash3-fill mr-3"></i> Delete Property
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <hr />

                <div class="propertyBookingDetails">
                    <h3>Booking Details</h3>
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource2" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="BookingId" ForeColor="#333333" GridLines="None" Width="100%">
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
                            <h5>No Bookings so far...</h5>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- View Ratings Modal -->
    <div class="modal fade" id="viewRatingsModal" tabindex="-1" role="dialog" aria-labelledby="viewRatingsModalModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="viewRatingsModalModalLabel">View Ratings</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource3">
                            <ItemTemplate>
                                <div class="viewRating">
                                    <p><%# Eval("UserId") %></p>
                                    <p class="text-danger"><%# ConvertToStars(Eval("RatingScore")) %></p>
                                    <p class="text-secondary text-sm"><%# Eval("ReviewDate") %></p>
                                    <p class="mt-2"><%# Eval("ReviewMessage") %></p>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Request Maintenance Modal -->
    <div class="modal fade" id="requestMaintenanceModal" tabindex="-1" role="dialog" aria-labelledby="requestMaintenanceModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="requestMaintenanceModalLabel">Request Maintenance</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary ID="ValidationMaintenanceRequestSummary" runat="server" CssClass="text-light bg-danger p-2 mb-3" ValidationGroup="ValidationMaintenanceRequest" />
                    <div class="form-group">
                        <label for="propertyType">Maintenance Type</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlMaintenanceType" ErrorMessage="Maintenance Type is Required." ForeColor="Red" ValidationGroup="ValidationMaintenanceRequest">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlMaintenanceType" CssClass="form-control" runat="server" ClientIDMode="Static">
                            <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                            <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                            <asp:ListItem Value="Wifi">Wifi</asp:ListItem>
                            <asp:ListItem Value="WaterHeater">Water Heater</asp:ListItem>
                            <asp:ListItem Value="Safety">Safety</asp:ListItem>
                            <asp:ListItem Value="Others">Others</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="description">Maintenance Description</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbMaintenanceDescription" ErrorMessage="Maintenance Description is Required." ForeColor="Red" ValidationGroup="ValidationMaintenanceRequest">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbMaintenanceDescription" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnRequestMaintenance" type="button" class="btn btn-primary" Text="Save Changes" OnClick="btnRequestMaintenance_Click" ValidationGroup="ValidationMaintenanceRequest" />
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Property Details Modal -->
    <div class="modal fade" id="editPropertyDetailsModal" tabindex="-1" role="dialog" aria-labelledby="editPropertyDetailsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editDetailsModalLabel">Edit Details</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary ID="ValidationPropertyDetailsSummary" runat="server" CssClass="text-light bg-danger p-2 mb-3" ValidationGroup="ValidationPropertyDetails" />

                    <div class="form-group">
                        <label for="propertyName">Property Name</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPropertyName" ErrorMessage="Property Name is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbPropertyName" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <label for="propertyAddress">Property Address</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPropertyAddress" ErrorMessage="Property Address is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbPropertyAddress" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <label for="propertyType">Property Type</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPropertyType" ErrorMessage="Property type is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlPropertyType" CssClass="form-control" runat="server" ClientIDMode="Static">
                            <asp:ListItem Value="Residential">Residential</asp:ListItem>
                            <asp:ListItem Value="Flat">Flat</asp:ListItem>
                            <asp:ListItem Value="Apartment">Apartment</asp:ListItem>
                            <asp:ListItem Value="Condominium">Condominium</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="propertyPrice">Property Price</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbPropertyPrice" ErrorMessage="Property price is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                            ControlToValidate="tbPropertyPrice"
                            ErrorMessage="Please enter a valid decimal value for price."
                            ValidationExpression="^\d+(\.\d+)?$"
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="ValidationPropertyDetails">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="tbPropertyPrice" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <label for="listingDescription">Listing Description</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbListingDescription" ErrorMessage="Listing description is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbListingDescription" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <label for="preferences">Preferences</label>
                        <asp:TextBox ID="tbPreferences" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <label for="numberOfBedroom">Number of Bedroom</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlNumberOfBedroom" ErrorMessage="Number of Bedroom is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlNumberOfBedroom" runat="server" CssClass="form-control" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="areaSqft">Area Sqft</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbAreaSqft" ErrorMessage="Area sqft is Required." ForeColor="Red" ValidationGroup="ValidationPropertyDetails">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="tbAreaSqft"
                            ErrorMessage="Please enter a valid decimal value for area sqft."
                            ValidationExpression="^\d+(\.\d+)?$"
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="ValidationPropertyDetails">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="tbAreaSqft" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>

                    <div class="form-group">
                        <div class="form-check">
                            <asp:CheckBox ID="chkAirCondAvailability" CssClass="form-check-input" runat="server" ClientIDMode="Static" />
                            <label class="form-check-label" for="airCondAvailability">Air-Cond</label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-check">
                            <asp:CheckBox ID="chkWaterHeaterAvailability" CssClass="form-check-input" runat="server" ClientIDMode="Static" />
                            <label class="form-check-label" for="waterHeaterAvailability">Water Heater</label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-check">
                            <asp:CheckBox ID="chkWifiAvailability" CssClass="form-check-input" runat="server" ClientIDMode="Static" />
                            <label class="form-check-label" for="wifiAvailability">Wi-Fi</label>
                        </div>
                    </div>
                    <hr />
                    <h4>Images</h4>
                    <div class="form-group mt-2">
                        Thumbnail:
                        <asp:FileUpload ID="fuThumbnail" runat="server" /><br />
                        <br />
                        Image 1:
                        <asp:FileUpload ID="fuImage1" runat="server" /><br />
                        <br />
                        Image 2:
                        <asp:FileUpload ID="fuImage2" runat="server" /><br />
                        <br />
                        Image 3:
                        <asp:FileUpload ID="fuImage3" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSavePropertyDetails" type="button" class="btn btn-primary" Text="Save Changes" OnClick="btnSavePropertyDetails_Click" ValidationGroup="ValidationPropertyDetails" />
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM Property Where PropertyId = @PropertyId AND LandlordId = @LandlordId">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
            <asp:QueryStringParameter Name="PropertyId" QueryStringField="PropertyId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM Booking INNER JOIN Tenant ON Booking.TenantId = Tenant.TenantId WHERE LandlordId = @LandlordId AND PropertyId = @PropertyId ORDER BY Booking.BookingTime DESC">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
            <asp:QueryStringParameter Name="PropertyId" QueryStringField="PropertyId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT [User].UserId, Review.RatingScore, Review.ReviewMessage, Review.ReviewDate FROM Review INNER JOIN Booking ON Booking.BookingId = Review.BookingId 
        INNER JOIN Tenant ON Booking.TenantId = Tenant.TenantId 
        INNER JOIN [User] ON [User].UserId = Tenant.UserId 
        WHERE Booking.PropertyId = @PropertyId 
        ORDER BY Review.ReviewDate DESC">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
            <asp:QueryStringParameter Name="PropertyId" QueryStringField="PropertyId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script>
        // Preset Edit Property Details textboxes value
        $('#editPropertyDetailsModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal

            // Extract data from attributes of the triggering button
            var propertyName = button.data('property-name');
            var propertyAddress = button.data('property-address');
            var propertyType = button.data('property-type');
            var propertyPrice = button.data('property-price');
            var listingDescription = button.data('listing-description');
            var preferences = button.data('preferences');
            var numberOfBedroom = button.data('number-of-bedroom');
            var areaSqft = button.data('area-sqft');
            var airCondAvailability = button.data('air-cond-availability') === "True" ? true : false;
            var waterHeaterAvailability = button.data('water-heater-availability') === "True" ? true : false;
            var wifiAvailability = button.data('wifi-availability') === "True" ? true : false;

            // Set the values in the modal textboxes
            $('#tbPropertyName').val(propertyName);
            $('#tbPropertyAddress').val(propertyAddress);
            $('#ddlPropertyType').val(propertyType);
            $('#tbPropertyPrice').val(propertyPrice);
            $('#tbListingDescription').val(listingDescription);
            $('#tbPreferences').val(preferences);
            $('#ddlNumberOfBedroom').val(numberOfBedroom);
            $('#tbAreaSqft').val(areaSqft);
            $('#chkAirCondAvailability').prop('checked', airCondAvailability);
            $('#chkWaterHeaterAvailability').prop('checked', waterHeaterAvailability);
            $('#chkWifiAvailability').prop('checked', wifiAvailability);
        });

        function Confirm() {
            var message = document.getElementById('<%= HiddenField1.ClientID %>').value;
            if (message) {
                return confirm(message);
            }
        }
    </script>
</asp:Content>
