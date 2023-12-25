<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/PaymentStatus.aspx.cs" Inherits="OnlineHomeRental.Tenant.PaymentStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-payment" id="content-wrapper">
        <div class="column-left">
            <div class="form-paymentstatus">
                <form class="div-paymentstatus" action="Homepage.aspx">
                    <h3 class="h3color-paymentstatus">Payment Successful</h3>
                    <p class="pcolor-paymentstatus">Payment Detail</p>
                    <input class="btn-paymentstatus" type="submit" value="Back to Homepage" />
                </form>
            </div>
        </div>

        <div class="column-right">
            <div class="featured">
                <img src="Data/Property.jpg" alt="" />
            </div>
            <div class="propertydetail-payment">
                <h1 class="h1color-paymentstatus">Property Name</h1>
                <hr />
                <h3 class="h1color-paymentstatus">RM 999.99</h3>
                <p class="pcolor-paymentstatus">Property Detail</p>
            </div>
        </div>
    </div>

</asp:Content>