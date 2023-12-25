<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="property">
        <div class="cards">
            <!-- Card 1: View Property -->
            <div class="card view-property">
                <img src="/Data/Property.jpg" alt="View Property Image" />
                <h3>View Property</h3>
                <p>Explore more details about your owned properties.</p>
                <div class="options">
                    <a href="./ViewProperty.aspx" class="button">View Property</a>
                </div>
            </div>

            <!-- Card 2: Add Property -->
            <div class="card add-property">
                <img src="/Data/AddProperty.jpg" alt="Add Property Image" />
                <h3>Add Property</h3>
                <p>Have a new property to rent? Add it to your list now!</p>
                <div class="options">
                    <a href="./AddProperty.aspx" class="button">Add Property</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
