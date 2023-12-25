<%@ Page Language="C#" MasterPageFile="TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/Payment.aspx.cs" Inherits="OnlineHomeRental.Tenant.PaymentProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-payment" id="content-wrapper">
        <div class="column-left">
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

        <div class="column-right">
            <div class="form-payment">
                <form action="PaymentStatus.aspx" autocomplete="off">
                    <h3 class="title payment-title h1color-paymentstatus">Payment</h3>
                    <div class="input-container">
                        <input type="text" name="ownerName" class="input" />
                        <label for="ownerName">Card Owner Name</label>
                        <span>Card Owner Name</span>
                    </div>
                    <div class="input-container">
                        <input type="number" name="cardNo" class="input" />
                        <label for="cardNo">Card Number</label>
                        <span>Card Number</span>
                    </div>
                    <p class="expirydate-payment">Card Expiry Date :</p>
                    <div class="container-payment">
                        <div class="input-container customwidth-payment">
                            <input type="number" name="expiryMonth" class="input" min="1" max="12" />
                            <label for="expiryMonth">MM</label>
                            <span>MM</span>
                        </div>
                        <div class="input-container customwidth-payment customtoleft-payment">
                            <input type="number" name="expiryYear" class="input" />
                            <label for="expiryYear">YYYY</label>
                            <span>YYYY</span>
                        </div>
                        <div class="input-container customwidth-payment">
                            <input type="number" name="cvv" class="input" min="100" max="999" />
                            <label for="cvv">CVV</label>
                            <span>CVV</span>
                        </div>
                    </div>
                    <input type="submit" value="Pay Now" class="btn-payment" />
                </form>
            </div>
        </div>
    </div>

    <script src="js/Payment.js"></script>
</asp:Content>