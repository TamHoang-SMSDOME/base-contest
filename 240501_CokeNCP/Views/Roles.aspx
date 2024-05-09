<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="BaseContest_WebForms.Views.Roles" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-offset-2 col-lg-2">
                    <h3 class="form-signin-heading">Add/Update Password</h3>
                </div>
                </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-2">
                    <h5 style="font-weight: bold">Role: </h5>
                </div>
                <div class="col-lg-4">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control" ID="ddlRole" runat="server">
                                    <asp:ListItem Text="User" Value="User" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                             <asp:ListItem Text="IdentityPW" Value="IdentityPW"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-2">
                    <h5 style="font-weight: bold">New Password*</h5>
                </div>
                <div class="col-lg-4">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="PassWord" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
                    </div>
                

            </div>

            <div class="col-lg-8 col-lg-offset-3">
                    <div class="row">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button runat="server" ID="Filter"
                                CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" />
                        </div>
                    </div>
                </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="divPopUp" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblModal" Text=""> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
     <script>
         $(document).ready(function () {
             jQuery('.datetimepicker').datetimepicker();
         });
    </script>
</asp:Content>