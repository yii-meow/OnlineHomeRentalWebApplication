<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="OnlineHomeRental.Landlord.BookingHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bookingHistory">

        <div class="col-md-5">
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="BookingId" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="BookingId" HeaderText="Booking Id" InsertVisible="False" ReadOnly="True" SortExpression="BookingId" />
                    <asp:BoundField DataField="CheckInDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Check In Date" SortExpression="CheckInDate" />
                    <asp:BoundField DataField="CheckOutDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Check Out Date" SortExpression="CheckOutDate" />
                    <asp:BoundField DataField="PropertyId" HeaderText="Property Id" InsertVisible="False" ReadOnly="True" SortExpression="PropertyId" />
                    <asp:BoundField DataField="PropertyName" HeaderText="Property Name" InsertVisible="False" ReadOnly="True" SortExpression="PropertyName" />
                    <asp:BoundField DataField="BookingStatus" HeaderText="Booking Status" SortExpression="Booking Status" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" OnClick="Chat_Click" CommandArgument='<%# Eval("TenantId") %>'>
                                <button type="button" class="btn-primary">Chat</button>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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

        <div class="col-md-6">
            <asp:DataList ID="DataList1" runat="server" CssClass="auto-style7" DataSourceID="SqlDataSource2">
                <ItemTemplate>
                    <div class="receipt-container">
                        <!-- Payment Details Section -->
                        <div class="section-header">Booking Invoice</div>
                        <span class="booking-id">Booking Id : <%# Eval("BookingId") %>
                        </span>
                        <div class="payment-details mt-3">
                            <div class="payment-status" style='<%# Eval("PaymentStatus").ToString().Equals("Successful", StringComparison.OrdinalIgnoreCase) ? "background-color: green;": "background-color: red;" %>'>
                                <%# Eval("PaymentStatus") %>
                            </div>

                            <div class="mt-3">
                                <div class="row">
                                    <!-- Left Column -->
                                    <div class="col-md-6 left-panel">
                                        <div class="mt-2 font-weight-bold"><%#Eval("Name")%></div>
                                        <div class="mt-2"><%#Eval("Email")%></div>
                                        <div class="mt-1">
                                            <%# !string.IsNullOrEmpty(Eval("BillingAddress").ToString()) ?
                                                            Eval("BillingAddress") : "(Billing Address Is Not Provided)" %>
                                        </div>
                                    </div>

                                    <!-- Right Column -->
                                    <div class="col-md-6">
                                        <table class="table table-hover">
                                            <tr>
                                                <th scope="row" class="bg-light p-2">Payment Date:</th>
                                                <td class="p-2">
                                                    <%# Convert.ToDateTime(Eval("PaymentDate")).ToString("d/M/yyyy HH:mm") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th class="bg-light p-2">Payment Method:</th>
                                                <td class="p-2"><%# Eval("PaymentMethod") %></td>
                                            </tr>
                                            <tr>
                                                <th class="bg-light p-2">Payment Amount:</th>
                                                <td class="p-2">RM <%# Convert.ToDouble(Eval("PaymentAmount")).ToString("0.00") %></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <table class="table w-100">
                            <thead>
                                <tr class="bg-light">
                                    <th>Booking</th>
                                    <th>Duration</th>
                                    <th class="text-right">Amount</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr>
                                    <td>
                                        <p>
                                            <strong><%# Eval("PropertyName") %></strong>
                                            <br />
                                            (<%# Eval("PropertyType") %>)
                                        </p>
                                        <p>
                                            <%# Convert.ToDateTime(Eval("CheckInDate")).ToString("d/M/yyyy") %> - <%# Convert.ToDateTime(Eval("CheckOutDate")).ToString("d/M/yyyy") %>
                                        </p>
                                    </td>
                                    <td><%# Eval("Duration") %> day(s)</td>
                                    <td class="text-right"><%# Convert.ToDouble(Eval("PaymentAmount")).ToString("C") %></td>
                                </tr>
                            </tbody>

                            <tfoot class="text-right">
                                <tr>
                                    <td colspan="2"><strong>Subtotal:</strong></td>
                                    <td><%# (Convert.ToDouble(Eval("PaymentAmount")) * 0.94).ToString("C") %></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Sales Tax (6%):</strong></td>
                                    <td><%# (Convert.ToDouble(Eval("PaymentAmount")) * 0.06).ToString("C") %></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Total:</strong></td>
                                    <td><%# Convert.ToDouble(Eval("PaymentAmount")).ToString("C") %></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM Booking INNER JOIN Tenant ON Booking.TenantId = Tenant.TenantId INNER JOIN Property ON Booking.PropertyId = Property.PropertyId WHERE Booking.LandlordId = @LandlordId">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT Booking.BookingId, Booking.PropertyId, Booking.CheckInDate, Booking.CheckOutDate, Booking.Duration,
                        Property.PropertyName, Property.PropertyType, Payment.PaymentAmount, Payment.PaymentMethod, Payment.PaymentDate, Payment.PaymentStatus,
                        [User].Name, [User].Gender, [User].PhoneNo, [User].Email, Tenant.BillingAddress
                    FROM Booking
                    INNER JOIN Property ON Booking.PropertyId = Property.PropertyId
                    INNER JOIN Payment ON Booking.BookingId = Payment.BookingId
                    INNER JOIN Tenant ON Booking.TenantId = Tenant.TenantId
                    INNER JOIN [User] ON Tenant.UserId = [User].UserId
                    WHERE Booking.BookingId = @BookingId;">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="BookingId" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
