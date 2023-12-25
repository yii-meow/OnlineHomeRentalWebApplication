<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/PropertyDetail.aspx.cs" Inherits="OnlineHomeRental.Tenant.PropertyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <head>
        <title>EZRent.com | Property Detail</title>
        <link rel="stylesheet" href="/CSS/PropertyDetail.css" />
    </head>

    <div class="content-propertydetail" id="content-wrapper">
        <div class="column-left">
            <img id="featured" src="/Data/Property.jpg" />

            <div id="slide-wrapper-propertydetail">
                <img id="slideLeft" class="arrow" src="/img/arrow-left.png" />

                <div id="slider-propertydetail">
                    <img class="thumbnail active" src="/Data/Property.jpg" />
                    <img class="thumbnail" src="/Data/AddProperty.jpg" />
                    <img class="thumbnail" src="/Data/Property.jpg" />
                    <img class="thumbnail" src="/Data/AddProperty.jpg" />
                    <img class="thumbnail" src="/Data/Property.jpg" />
                    <img class="thumbnail" src="/Data/AddProperty.jpg" />
                    <img class="thumbnail" src="/Data/Property.jpg" />
                </div>

                <img id="slideRight" class="arrow" src="/img/arrow-right.png" />
            </div>
        </div>

        <div class="column-right">
            <h1 class="h1color-paymentstatus">Property Name</h1>
            <hr />
            <h3 class="h1color-paymentstatus">RM 999.99</h3>
            <p class="pcolor-paymentstatus">Property Detail</p>

            <form runat="server">
                <div class="calendar-firstrow">
                    <asp:Calendar CssClass="calendar-pd1" ID="Calendar1" runat="server"></asp:Calendar>
                    <asp:Calendar CssClass="calendar-pd2" ID="Calendar2" runat="server"></asp:Calendar>
                </div>

                <div class="calendar-secondrow">
                        <asp:TextBox CssClass="calendar-column1" ID="TextBox1" placeholder="Choose a Date From :" runat="server"></asp:TextBox>
                        <asp:TextBox CssClass="calendar-column2" ID="TextBox2" placeholder="Choose a Date To :" runat="server"></asp:TextBox>
                </div>
            </form>

            <a class="btn-propertydetail" href="Payment.aspx">Book Now</a>
        </div>
    </div>

    <script type="text/javascript" src="/js/PropertyDetail.js"></script>

</asp:Content>
