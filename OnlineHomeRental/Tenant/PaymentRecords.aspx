<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Tenant/TenantMenu.Master" CodeBehind="PaymentRecords.aspx.cs" Inherits="OnlineHomeRental.Tenant.PaymentRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <button type="button" class="btn btn-save mt-2 w-25" data-toggle="modal" data-target="#editDetailsModal">
        <i class="bi bi-pencil-square text-success text-light mr-3" style="font-size: 1.2em;"></i>Edit Details
    </button>

    <!-- Edit Personal Details Modal -->
    <div class="modal fade" id="editDetailsModal" tabindex="-1" role="dialog" aria-labelledby="editDetailsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editDetailsModalLabel">Edit Details</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-- Form fields for editing details -->
                    <div class="form-group">
                        <label for="editName">Name</label>
                        <asp:TextBox runat="server" ID="tbEditName" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnAddReview" type="button" class="btn btn-primary" OnClick="btnAddReview_Click" Text="Add Review" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
