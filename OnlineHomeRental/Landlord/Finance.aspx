<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="~/Landlord/Finance.aspx.cs" Inherits="OnlineHomeRental.Landlord.Finance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="finance">
        <header>
            <div class="header-container">
                <h2>Financial Information</h2>
            </div>
        </header>

        <section>
            <h5 class="mt-4"><b>Summary of Records <asp:Literal ID="lblMonthRecord" runat="server" /> (compared to last month)</b></h5>
            <div class="finance-cards mt-3 p-3">
                <div class="finance-item">
                    <p>Total Sales (+)</p>
                    <p class="value"><asp:Label ID="lblTotalSales" runat="server" /> (<asp:Label ID="lblTotalSalesDif" runat="server"/>)</p>
                </div>
                <div class="finance-item">
                    <p>Total Booking (+)</p>
                    <p class="value"><asp:Literal ID="lblTotalBooking" runat="server" /> (<asp:Literal ID="lblTotalBookingDif" runat="server" />)</p>
                </div>
                <div class="finance-item">
                    <p>Net Profit:</p>
                    <p class="value"><asp:Literal ID="lblNetProfit" runat="server" /> (<asp:Literal ID="lblNetProfitDif" runat="server" />)</p>
                </div>
                <div class="finance-item">
                    <p>Cancelled Booking (-)</p>
                    <p class="value"><asp:Literal ID="lblCancelledBooking" runat="server" /> (<asp:Literal ID="lblCancelledBookingDif" runat="server" />)</p>
                </div>
                <div class="finance-item">
                    <p>Unused Property Duration:</p>
                    <p class="value"><asp:Literal runat="server" ID="lblUnusedDuration" /> (<asp:Literal runat="server" ID="lblUnusedDurationDif" />)</p>
                </div>
                <div class="finance-item">
                    <p>Average Booking Price</p>
                    <p class="value"><asp:Literal ID="lblAvgBooking" runat="server" /> (<asp:Literal ID="lblAvgBookingDif" runat="server" />)</p>
                </div>
            </div>
        </section>

        <section class="summary">
            <div class="revenue-chart">
                <span><u>Revenue (last 6 months)</u></span>
                <canvas id="revenueChart"></canvas>
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
            <h2>Transaction History</h2>
            <table>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Description</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>2023-11-15</td>
                        <td>Booking #123456</td>
                        <td>$500.00</td>
                    </tr>
                    <tr>
                        <td>2023-11-10</td>
                        <td>Booking #123455</td>
                        <td>$700.00</td>
                    </tr>
                    <tr>
                        <td>2023-11-10</td>
                        <td>Booking #123455</td>
                        <td>$700.00</td>
                    </tr>
                    <tr>
                        <td>2023-11-10</td>
                        <td>Booking #123455</td>
                        <td>$700.00</td>
                    </tr>
                    <tr>
                        <td>2023-11-10</td>
                        <td>Booking #123455</td>
                        <td>$700.00</td>
                    </tr>
                </tbody>
            </table>
        </section>
    </div>
    <script>
        // Get the canvas element
        const canvas = document.getElementById('revenueChart');
        const ctx = canvas.getContext('2d');

        // Data for the revenue chart (sample data)
        const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];
        const revenueData = [2000, 2500, 1800, 3000, 2800, 3500];

        // Calculate the maximum revenue for scaling
        const maxRevenue = Math.max(...revenueData);

        // Set the chart properties
        const chartWidth = canvas.width - 40;
        const chartHeight = canvas.height - 40;
        const barWidth = chartWidth / months.length;
        const barColor = 'blue';

        // Draw the axes
        ctx.beginPath();
        ctx.moveTo(30, 10);
        ctx.lineTo(30, chartHeight + 10);
        ctx.lineTo(chartWidth + 30, chartHeight + 10);
        ctx.stroke();

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

            // Display data values on the y-axis
            ctx.fillText(revenueData[i], x + (barWidth - 5) / 2, chartHeight + 10 - barHeight - 5);
        }
    </script>
</asp:Content>
