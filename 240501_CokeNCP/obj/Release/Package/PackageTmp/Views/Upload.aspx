<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="BaseContest_WebForms.Views.Upload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>Upload Entries</h1>
                     <p>Requirements : <br />
                        1) Only accept .CSV File<br />
                        2) No Headers<br />
                        3) Fields : MobileNo,Name,NRIC,ReceiptNo,Amount<br />
                <%--        4) Dates must be in yyyy-MM-dd format<br />--%>
                     </p>
                    <asp:FileUpload ID="UploadF" runat="server" />
                    <h5 style="font-weight: bold" runat="server" id="FileSpecs" visible="false"></h5>
                    <div class="row">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button runat="server" ID="Filter"
                                CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" />
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