<%@ Page Language="C#" MasterPageFile="~/Landlord/LandlordMenu.Master" AutoEventWireup="true" CodeBehind="AddRating.aspx.cs" Inherits="OnlineHomeRental.Landlord.AddRating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <button type="button" class="btn btn-save mt-2 w-25" data-toggle="modal" data-target="#addRatingsModal" data-booking-id="1" data-property-id="1">
        <i class="bi bi-pencil-square text-success text-light mr-3" style="font-size: 1.2em;"></i>Add Ratings
    </button>

    <!-- Edit Personal Details Modal -->
    <div class="modal fade" id="addRatingsModal" tabindex="-1" role="dialog" aria-labelledby="addRatingsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addRatingsModalLabel">Add Rating</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary ID="ValidationReviewSummary" runat="server" CssClass="text-light bg-danger p-2 mb-3" ValidationGroup="ValidationReview" />
                    Property ID:
                    <asp:Label ID="lblPropertyId" runat="server" ClientIDMode="Static" />

                    <div class="form-group">
                        Booking ID:
                        <asp:Label ID="lblBookingId" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="form-group">
                        <label for="starRating">Rating</label>
                        <div id="starRating" class="rating">
                            <i class="fas fa-star" data-rating="1"></i>
                            <i class="fas fa-star" data-rating="2"></i>
                            <i class="fas fa-star" data-rating="3"></i>
                            <i class="fas fa-star" data-rating="4"></i>
                            <i class="fas fa-star" data-rating="5"></i>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnRating" ClientIDMode="Static" />
                    </div>
                    <div class="form-group">
                        <label for="editName">Review Message</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbReviewMessage" ErrorMessage="Review Message is Required." ForeColor="Red" ValidationGroup="ValidationReview">*</asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="tbReviewMessage" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" ValidationGroup="ValidationReview" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnAddRating" type="button" class="btn btn-primary" OnClick="addRating_Click" Text="Save Changes" ValidationGroup="ValidationReview" />
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            // Initialize star rating
            $('.rating i').click(function () {
                var rating = $(this).data('rating');

                // Highlight selected stars
                $('.rating i').removeClass('active');
                $(this).prevAll().addBack().addClass('active');

                // Set the hidden field value
                $('#<%= hdnRating.ClientID %>').val(rating);
            });
        })

        $('#addRatingsModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal

            // Extract data from attributes of the triggering button
            var bookingId = button.data('booking-id');
            var propertyId = button.data('property-id');

            // Set the values in the modal textboxes
            $('#lblBookingId').text(bookingId);
            $('#lblBookingId').val(bookingId);

            $('#lblPropertyId').text(propertyId);
            $('#lblPropertyId').val(propertyId);
        });
    </script>
</asp:Content>
