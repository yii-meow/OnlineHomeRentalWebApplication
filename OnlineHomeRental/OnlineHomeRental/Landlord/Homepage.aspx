<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home">
        <div>
            <div class="summary-cards">
                <!-- Overall Revenue Card -->
                <div class="summary-card">
                    <i class="fa-solid fa-chart-simple text-success"></i>
                    <h5>Total Revenue</h5>
                    <p class="value">RM 60,000</p>
                    <!-- Add additional details or icons as needed -->
                </div>

                <!-- Overall Rating Card -->
                <div class="summary-card">
                    <i class="fa-regular fa-star text-warning"></i>
                    <h5>Overall Rating</h5>
                    <p class="value">4.5 / 5</p>
                    <!-- Add additional details or icons as needed -->
                </div>

                <!-- Top Booked Property Card -->
                <div class="summary-card">
                    <i class="fa-solid fa-fire text-danger"></i>
                    <h5>Top Booked Property</h5>
                    <p class="value">Property ABC</p>
                    <!-- Add additional details or icons as needed -->
                </div>
            </div>

            <h5><b>Details View</b></h5>

            <div class="dashboard-cards">
                <div class="dashboard-card">
                    <span class="text-secondary">Bookings Placed This Month</span>
                    <p>50</p>
                    <!-- Card Content for Card 1 -->
                </div>

                <div class="dashboard-card">
                    <span class="text-secondary">Revenue This Month</span>
                    <p>RM 15,000</p>
                </div>

                <div class="dashboard-card">
                    <span class="text-secondary">Net Profit This Month</span>
                    <p>RM 10,000</p>
                </div>

                <div class="dashboard-card">
                    <span class="text-secondary">Average Booking Price</span>
                    <p>RM 500</p>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Repeated User This Month</span>
                            <p>5</p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="fa-solid fa-repeat fa-xl text-info"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Completion Rate</span>
                            <p class="text-success">99 %</p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="fa-solid fa-check fa-xl text-success"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Cancel Rate</span>
                            <p class="text-danger">1 %</p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="fa-solid fa-x fa-xl text-danger"></i>
                        </div>
                    </div>
                </div>

                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-10">
                            <span class="text-secondary">Absence Rate</span>
                            <p class="text-danger">1 %</p>
                        </div>

                        <div class="col-md-2 mt-5">
                            <i class="fa-solid fa-person-circle-question fa-xl"></i>
                        </div>
                    </div>
                </div>
            </div>

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
