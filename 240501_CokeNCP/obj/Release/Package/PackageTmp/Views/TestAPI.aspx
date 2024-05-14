<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="TestAPI.aspx.cs" Inherits="BaseContest_WebForms.Views.TestAPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href='../Content/style.css' type='text/css' rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div class="container">
                        <div class="wrapper">
                            <hr class="colorgraph">
                            <p>
                                &nbsp;
                               
                            </p>
                            <h3 class="form-signin-heading">SMSDome Contest API Test</h3>
                            <p>
                                &nbsp;
                               
                            </p>
                            <div class="mb-3">
                                <asp:TextBox runat="server" ID="MobileNo" CssClass="form-control"></asp:TextBox>
                                <%-- <input ng-Trim="false" type="text" class="form-control" ng-model="Entry.MobileNo" ng-init="Entry.MobileNo = ''" name="Mobile" placeholder="Mobile" autofocus="" />--%>
                            </div>
                            <div class="mb-3">
                                <asp:TextBox runat="server" ID="EntryText" CssClass="form-control"></asp:TextBox>
                                <%-- <input ng-Trim="false" type="text" class="form-control" ng-model="Entry.EntryText" ng-init="Entry.EntryText = KW + ' LEE GUO EN S9129952F T1001 S$500.00'" name="EntryText" placeholder="EntryText" />--%>
                            </div>
                            <div class="mb-3">
                                <asp:TextBox ID="subDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                            </div>

                              <asp:Button CssClass="btn btn-primary" runat="server" ID="Submit" 
                               Text="Submit"  OnClick="Submit_click"/>
                        </div>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
     <script>
         $(document).ready(function () {
             jQuery('.datetimepicker').datetimepicker();
         });
    </script>
</asp:Content>

