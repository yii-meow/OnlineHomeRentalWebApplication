<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/Finance.aspx.cs" Inherits="OnlineHomeRental.Landlord.Finance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="finance">
        <header>
            <div class="header-container">
                <h2>Financial Information</h2>
            </div>
        </header>

        <section>
            <h5 class="mt-4"><b>Summary of Records
                <asp:Literal ID="lblMonthRecord" runat="server" />
                (compared to
                <asp:Literal ID="lblLastMonthRecord" runat="server" />)</b></h5>
            <div class="finance-cards mt-3 p-3">
                <div class="finance-item">
                    <p>Total Booking</p>
                    <p class="value">
                        <asp:Label ID="lblTotalSales" runat="server" />
                        (<asp:Label ID="lblTotalSalesDif" runat="server" />)
                    </p>
                </div>
                <div class="finance-item">
                    <p>Total Booking</p>
                    <p class="value">
                        <asp:Label ID="lblTotalBooking" runat="server" />
                        (<asp:Label ID="lblTotalBookingDif" runat="server" />)
                    </p>
                </div>
                <div class="finance-item">
                    <p>Net Profit</p>
                    <p class="value">
                        <asp:Label ID="lblNetProfit" runat="server" />
                        (<asp:Label ID="lblNetProfitDif" runat="server" />)
                    </p>
                </div>
                <div class="finance-item">
                    <p>Cancelled Booking</p>
                    <p class="value">
                        <asp:Label ID="lblCancelledBooking" runat="server" />
                        (<asp:Label ID="lblCancelledBookingDif" runat="server" />)
                    </p>
                </div>
                <div class="finance-item">
                    <p>Unused Property Duration</p>
                    <p class="value">
                        <asp:Label runat="server" ID="lblUnusedDuration" />
                        (<asp:Label runat="server" ID="lblUnusedDurationDif" />)
                    </p>
                </div>
                <div class="finance-item">
                    <p>Average Booking Price</p>
                    <p class="value">
                        <asp:Label ID="lblAvgBooking" runat="server" />
                        (<asp:Label ID="lblAvgBookingDif" runat="server" />)
                    </p>
                </div>
            </div>
        </section>

        <section class="summary">
            <div class="revenue-chart">
                <span><u>Revenue (last 6 months)</u></span>
                <canvas id="revenueChart" width="600" height="400"></canvas>
            </div>

            <div class="reports">
                <h5>Monthly Reports</h5>
                <ul class="report-list">
                    <li><a href="/Data/1.pdf" target="_blank">November 2023</a></li>
                    <li><a href="/Data/1.pdf" target="_blank">October 2023</a></li>
                    <li><a href="/Data/1.pdf" target="_blank">September 2023</a></li>
                    <li><a href="/Data/1.pdf" target="_blank">August 2023</a></li>
                    <li><a href="/Data/1.pdf" target="_blank">July 2023</a></li>
                    <li><a href="/Data/1.pdf" target="_blank">June 2023</a></li>
                </ul>
            </div>
        </section>

        <section class="transaction-history">
            <h2>Recent Transaction History</h2>
            <table>
                <thead>
                    <tr>
                        <th>Payment Date</th>
                        <th>Payment Amount</th>
                        <th>Payment Method</th>
                        <th>Payment Status</th>
                        <th>Booking Id</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("PaymentDate") %></td>
                                <td><%# ((decimal)Eval("PaymentAmount")).ToString("C") %></td>
                                <td><%# Eval("PaymentMethod") %></td>
                                <td style='<%# GetPaymentStatusStyle(Eval("PaymentStatus").ToString()) %>'>
                                    <%# Eval("PaymentStatus") %>
                                </td>
                                <td><%# Eval("BookingId") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </section>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM Payment INNER JOIN Booking ON Payment.BookingId = Booking.BookingId WHERE LandlordId = @LandlordId ORDER BY PaymentDate DESC">
        <SelectParameters>
            <asp:SessionParameter Name="LandlordId" SessionField="LandlordId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script type="text/javascript">
        function DrawCanvas(data) {
            // Parse the JSON data
            const bookingData = data;

            // Get the canvas element
            const canvas = document.getElementById('revenueChart');
            const ctx = canvas.getContext('2d');

            // Extract months and revenue data from the JSON
            const months = bookingData.map(entry => getMonthName(entry.BookingMonth));
            const revenueData = bookingData.map(entry => entry.TotalRevenue);

            // Calculate the maximum revenue for scaling
            const maxRevenue = Math.max(...revenueData);

            // Set the chart properties
            const chartWidth = canvas.width - 40;
            const chartHeight = canvas.height - 40;
            const barWidth = chartWidth / months.length;
            const barColor = 'orange';

            // Draw the axes
            ctx.beginPath();
            ctx.moveTo(30, 10);
            ctx.lineTo(30, chartHeight + 10);
            ctx.lineTo(chartWidth + 30, chartHeight + 10);
            ctx.stroke();

            ctx.font = 'bold 14px Arial'; // Adjust the font size and style as needed

            // Draw the bars for each month
            for (let i = 0; i < months.length; i++) {
                const x = 30 + i * barWidth;
                const barHeight = (revenueData[i] / maxRevenue) * chartHeight;

                // Draw bars
                ctx.fillStyle = barColor;
                ctx.fillRect(x, chartHeight + 10 - barHeight, barWidth - 5, barHeight);

                // Display month labels
                ctx.fillStyle = '#000';
                ctx.textAlign = 'center';
                ctx.fillText(months[i], x + (barWidth - 5) / 2, chartHeight + 30);

                // Adjust the y position for the highest bar
                let yPos = chartHeight + 10 - barHeight - 5;
                if (barHeight > chartHeight - 20) { // Adjust this value as needed
                    yPos += 20; // This will lower the text by 20 units
                }

                // Set the fillStyle to green
                ctx.fillStyle = 'red';

                // Display data values on the y-axis
                ctx.fillText(`RM${revenueData[i].toFixed(2)}`, x + (barWidth - 5) / 2, yPos);
            }


        }

        // Helper function to convert month number to month name
        function getMonthName(monthNumber) {
            const months = [
                'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
            ];
            return months[monthNumber - 1]; // Adjust for zero-based index
        }

    </script>
</asp:Content>
