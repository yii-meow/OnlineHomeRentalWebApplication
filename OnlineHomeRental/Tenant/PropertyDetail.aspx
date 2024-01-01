<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/PropertyDetail.aspx.cs" Inherits="OnlineHomeRental.Tenant.PropertyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <head>
        <title>EZRent.com | Property Detail</title>
        <link rel="stylesheet" href="/CSS/PropertyDetail.css" />
    </head>

    <div class="content-propertydetail" id="content-wrapper">
        <div class="column-left">
            <img id="featured" src="/img/LandlordPropertyImages/dorm1.jpg" alt="property" />

            <div id="slide-wrapper-propertydetail">
                <img id="slideLeft" class="arrow" src="/img/arrow-left.png" />

                <div id="slider-propertydetail">
                    <img class="thumbnail active" id="imgThumbnail" alt="property" runat="server" />
                    <img class="thumbnail" id="imgImage1" alt="property" runat="server" />
                    <img class="thumbnail" id="imgImage2" alt="property" runat="server" />
                    <img class="thumbnail" id="imgImage3" alt="property" runat="server" />
                </div>

                <img id="slideRight" class="arrow" src="/img/arrow-right.png" />
            </div>
        </div>

        <div class="column-right">
            <p class="bold-propdetail">
                <asp:Label ID="lblPropertyType" runat="server" Text=""></asp:Label>
            </p>
            <h1 class="h1color-paymentstatus">
                <asp:Label ID="lblPropertyName" runat="server" Text=""></asp:Label>
            </h1>
            <hr />
            <h3 class="h1color-paymentstatus">RM
                    <asp:Label ID="lblPropertyPrice" runat="server" Text=""></asp:Label>
                <span style="font-size: 16px">per day</span>
            </h3>
            <p class="color-propdetail">
                <asp:Label ID="lblPropertyAddress" runat="server" Text=""></asp:Label>
            </p>
            <p class="font-propdetail">
                <asp:Label ID="lblListingDescription" runat="server" Text=""></asp:Label>
            </p>
            <div class="color-propdetail">
                <p>
                    <asp:Label ID="lblNoOfBedroom" runat="server" Text=""></asp:Label>
                    Bedroom /
                        <asp:Label ID="lblAreaSqft" runat="server" Text=""></asp:Label>
                    Sqft
                </p>
                <p>
                    <asp:Label ID="lblAirCond" runat="server" Text="Label"></asp:Label>Air Conditional
                </p>
                <p>
                    <asp:Label ID="lblWaterHeater" runat="server" Text="Label"></asp:Label>Water Heater
                </p>
                <p>
                    <asp:Label ID="lblWifi" runat="server" Text="Label"></asp:Label>Wifi
                </p>
            </div>

            <div class="func-propertydetail">
                <div>
                    <p>Choose a booking date From :</p>
                    <asp:TextBox CssClass="form-control-propdetail py-3 marg-propdetail" ID="txtBookFrom" runat="server" type="date"></asp:TextBox>
                </div>
                <div class="func2-propertydetail">
                    <p>Choose a booking date To :</p>
                    <asp:TextBox CssClass="form-control-propdetail py-3 marg-propdetail" ID="txtBookTo" runat="server" type="date"></asp:TextBox>
                </div>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please choose a booking date From." Display="Dynamic" ControlToValidate="txtBookFrom" SetFocusOnError="True" ForeColor="Red" Font-Size="15px" ValidationGroup="bookingValidation"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please choose a booking date To." Display="Dynamic" ControlToValidate="txtBookTo" SetFocusOnError="True" ForeColor="Red" Font-Size="15px" ValidationGroup="bookingValidation"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* The 'Date From' should be a later date than the Today's Date." Display="Dynamic" ControlToValidate="txtBookFrom" OnServerValidate="CustomCompare1" SetFocusOnError="True" ForeColor="Red" Font-Size="15px" ValidationGroup="bookingValidation"></asp:CustomValidator>
            </div>
            <div>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="* The 'Date To' should be a later date than the 'Date From'." Display="Dynamic" ControlToCompare="txtBookFrom" ControlToValidate="txtBookTo" SetFocusOnError="True" ForeColor="Red" Font-Size="15px" Operator="GreaterThan" Type="Date" ValidationGroup="bookingValidation"></asp:CompareValidator>
            </div>

            <asp:Button CssClass="btn btn-dark border-0 w-100 py-3 btnMarg-propdetail" ID="btnProceed" runat="server" Text="Proceed to Payment" OnClick="btnProceed_Click" ValidationGroup="bookingValidation" />

            <asp:LinkButton runat="server" OnClick="Chat_Click">
                <button class="btn btn-info border-0 w-100 py-3 btnMarg-propdetail mt-5" type="button">Chat with landlord</button>
            </asp:LinkButton>
        </div>
    </div>

    <script type="text/javascript" src="/js/PropertyDetail.js"></script>

</asp:Content>
