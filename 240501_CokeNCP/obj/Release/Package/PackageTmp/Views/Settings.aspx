<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="BaseContest_WebForms.Views.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>Manage Settings</h1>
                    <p>Edit Settings</p>
                    <div class="row">
                        <div class="col-lg-2">
                            <asp:Button runat="server" ID="GetCount"
                                CssClass="btn btn-primary" Text="Remaining Credits" OnClick="GetCount_Click" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-2">
                            <p>Valid Entry Response</p>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button runat="server" ID="ValidEntryResponse"
                                CssClass="btn btn-primary" Text="Update" OnClick="ValidEntryResponse_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <p>Generic Error Message</p>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button runat="server" ID="GenericErrorMessage"
                                CssClass="btn btn-primary" Text="Update"  OnClick="GenericErrorMessage_Click" />
                        </div>
                    </div>
                
                </div>
            </div>
            <p>&nbsp;</p>

        </div>
    </div>
    <!-- Modal -->
    <div id="divPopUp" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblModal" Text=""> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>
    <!-- Modal -->
    <%-- <div id="divConfirm" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirm" Text="Are you sure to delete entry(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeSelected"
                        Text="Confirm" OnClick="PurgeSelected_Click" />
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>--%>

    <%--  <div id="divConfirmAll" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
              
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirmAll" Text="Are you sure to delete entry(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeAll"
                        Text="Confirm" OnClick="Purge_Click" />
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
    <script>
        $(document).ready(function () {
            jQuery('.datetimepicker').datetimepicker();
        });
    </script>
</asp:Content>

