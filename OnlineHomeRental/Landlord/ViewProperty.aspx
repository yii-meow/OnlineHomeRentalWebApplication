<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/ViewProperty.aspx.cs" Inherits="OnlineHomeRental.Landlord.ViewProperty" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="viewProperty">
        <h3>Owned Property</h3>
        <div class="list-view">
            <button type="button" class="viewList btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                View in data list
            </button>
        </div>

        <h5>Total Properties : 
        <asp:Label ID="lblTotalProperties" runat="server" />
        </h5>

        <div class="filter-container">
            <label for="propertyType">Filter by Property Type:</label>
            <asp:DropDownList ID="propertyType" runat="server">
                <asp:ListItem Value="all">All</asp:ListItem>
                <asp:ListItem Value="residential">Residential</asp:ListItem>
                <asp:ListItem Value="flat">Flat</asp:ListItem>
                <asp:ListItem Value="apartment">Apartment</asp:ListItem>
                <asp:ListItem Value="condominium">Condominium</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="applyFilter" runat="server" class="btn bg-primary ml-3 text-white" Text="Apply Filter" OnClick="ApplyFilter_Click" />
            <div class="mt-3">
                <asp:Label ID="lblReturnedFilterRecord" runat="server" />
            </div>
        </div>

        <div class="property-cards">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="property-card">
                        <img class="property-img" src="property.jpg" alt="Property Image">
                        <div class="property-details">
                            <div class="property-id">ID: <%#Eval("PropertyId")%></div>
                            <div class="property-name"><%#Eval("PropertyName")%></div>
                            <div class="property-type">Type : <%#Eval("PropertyType")%></div>
                            <!-- Add Icon -->
                            <div class="property-address">Address : <%#Eval("PropertyAddress")%></div>
                            <div class="property-description"><%#Eval("ListingDescription")%></div>
                            <div class="property-ratings">
                                <!-- Calculate Ratings -->
                                ⭐⭐⭐⭐⭐
                            </div>
                            <div class="property-price">RM <%#Eval("PropertyPrice")%> <span class="text-dark">/ night </span></div>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Landlord/PropertyDetails.aspx?PropertyId=" + Eval("PropertyId") %>'>
                                <button type="button" class="details-button">Check for more details</button>
                            </asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
        <SelectParameters>
            <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Property Lists</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="PropertyId" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="PropertyId" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="PropertyId" />
                                    <asp:BoundField DataField="PropertyName" HeaderText="Property Name" SortExpression="PropertyName" />
                                    <asp:BoundField DataField="PropertyType" HeaderText="Property Type" SortExpression="PropertyType" />
                                    <asp:BoundField DataField="PropertyPrice" HeaderText="Property Price" SortExpression="PropertyPrice" DataFormatString="RM {0:0.00}" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# "~/Landlord/PropertyDetails.aspx?PropertyId=" + Eval("PropertyId") %>'>
                                                <button type="button" class="btn-primary">Check</button>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
