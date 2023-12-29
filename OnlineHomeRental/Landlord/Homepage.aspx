<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="OnlineHomeRental.Landlord.Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home">
        <div>
            <div class="summary-cards">
                <!-- Overall Revenue Card -->
                <div class="summary-card">
                    <i class="fa-solid fa-chart-simple text-success"></i>
                    <h5>Total Revenue</h5>
                    <p class="value">
                        <asp:Literal runat="server" ID="lblTotalRevenue" />
                    </p>
                </div>

                <!-- Overall Rating Card -->
                <div class="summary-card">
                    <i class="fa-regular fa-star text-warning"></i>
                    <h5>Overall Rating</h5>
                    <p class="value">
                        <asp:Literal runat="server" ID="lblRating" />4.5 / 5
                    </p>
                </div>

                <!-- Top Booked Property Card -->
                <div class="summary-card">
                    <i class="fa-solid fa-fire text-danger"></i>
                    <h5>Top Booked Property</h5>
                    <p class="value">
                        <asp:Literal runat="server" ID="lblTopBookedProperty" />
                    </p>
                </div>
            </div>

            <h5><b>Details View (<asp:Literal ID="lblMonth" runat="server" />)</b></h5>

            <div class="dashboard-cards">
                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Bookings Placed This Month</span>
                            <p>
                                <asp:Literal runat="server" ID="lblBookingThisMonth" />
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-person-walking"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">

                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Revenue This Month</span>
                            <p>
                                <asp:Literal runat="server" ID="lblRevenueThisMonth" />
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-database-fill-up text-success"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Net Profit This Month <small>(-6% Government Tax)</small></span>
                            <p>
                                <asp:Literal runat="server" ID="lblProfitThisMonth" />
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-graph-up text-success"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Average Booking Price Size</span>
                            <p>
                                <asp:Literal runat="server" ID="lblAvgBooking" />
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-aspect-ratio text-info"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Completion Rate</span>
                            <p class="text-success">
                                <asp:Literal runat="server" ID="lblCompletionRate" />
                                %
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-check-all text-success"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Total Unused Property Duration</span>
                            <p>
                                <asp:Literal runat="server" ID="lblUnusedDuration" />
                                day(s)
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="fa-solid fa-person-circle-question text-warning"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Total Potential Loss</span>
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="lblTotalPotentialLoss" />
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-graph-down-arrow text-danger"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card topBookedProperty">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Top 3 Booked Property</span>
                            <p class="font-weight-normal">
                                <asp:Literal runat="server" ID="lblTop3BookedProperties"/>
                            </p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="bi bi-hand-thumbs-up-fill"></i>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Most Recent 5 notifications -->
            <div class="activity-feed">
                <h5><b>Recent Activity Feed</b></h5>
                <div class="activity-cards">
                    <!-- Dynamic content for activity feed goes here -->
                    <div class="activity-card">
                        <div class="activity-icon">
                            <!-- Add an icon or image related to the activity -->
                            🏠
                        </div>
                        <div class="activity-details">
                            <p><b>Booked a property:</b></p>
                            <p>Property XYZ on December 5, 2023</p>
                        </div>
                    </div>
                    <div class="activity-card">
                        <div class="activity-icon">
                            <!-- Add an icon or image related to the activity -->
                            ❌
                        </div>
                        <div class="activity-details">
                            <p><b>Cancelled booking:</b></p>
                            <p>Booking ABC on November 28, 2023</p>
                        </div>
                    </div>
                    <div class="activity-card">
                        <div class="activity-icon">
                            <!-- Add an icon or image related to the activity -->
                            ❌
                        </div>
                        <div class="activity-details">
                            <p><b>Cancelled booking:</b></p>
                            <p>Booking ABC on November 28, 2023</p>
                        </div>
                    </div>
                    <div class="activity-card">
                        <div class="activity-icon">
                            <!-- Add an icon or image related to the activity -->
                            ❌
                        </div>
                        <div class="activity-details">
                            <p><b>Cancelled booking:</b></p>
                            <p>Booking ABC on November 28, 2023</p>
                        </div>
                    </div>
                    <div class="activity-card">
                        <div class="activity-icon">
                            <!-- Add an icon or image related to the activity -->
                            ❌
                        </div>
                        <div class="activity-details">
                            <p><b>Cancelled booking:</b></p>
                            <p>Booking ABC on November 28, 2023</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
