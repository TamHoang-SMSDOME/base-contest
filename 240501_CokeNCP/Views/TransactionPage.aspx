<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="TransactionPage.aspx.cs" Inherits="BaseContest_WebForms.Views.TransactionPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>View Transactions</h1>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Start Date</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="startDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>End Date</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="endDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button runat="server" ID="Filter"
                                CssClass="btn btn-primary" Text="Filter" OnClick="Filter_Click" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="ExportDiv">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-light" runat="server" ID="ExportToCSV"
                                Text="Export To CSV using comma" OnClick="ExportToCsv_click" />
                        </div>
                    </div>
                </div>
            </div>
            <p>&nbsp;</p>
            <div class="row" runat="server" id="PagingDiv">
                <div class="col-lg-12">
                    <div style="text-align: center">
                        <asp:Button CssClass="btn btn-light" runat="server" ID="FirstPage"
                            Text="First Page" OnClick="FirstPage_Click" />

                        <asp:Button CssClass="btn btn-light" runat="server" ID="PreviousPage"
                            Text="<" OnClick="PreviousPage_Click" />

                        <asp:TextBox runat="server" ID="CurrentPage" Style="width: 4%" TextMode="Number" Text="1"></asp:TextBox>

                        <span class="label label-default">/
                            <asp:Label runat="server" ID="lblTotalPages"></asp:Label></span>

                        <asp:Button CssClass="btn btn-light" runat="server" ID="Go"
                            Text="GO" OnClick="Filter_Click" />

                        <asp:Button CssClass="btn btn-light" runat="server" ID="NextPage"
                            Text=">" OnClick="NextPage_Click" />

                        <asp:Button CssClass="btn btn-light" runat="server" ID="LastPage"
                            Text="Last Page" OnClick="LastPage_Click" />

                        <span class="label label-default">No Of Total Records :
                            <asp:Label runat="server" ID="lblTotal"></asp:Label></span>
                    </div>
                </div>

            </div>
            <p>&nbsp;</p>
            <div id="divShowTotal" runat="server">
                <div class="row" style="text-align:center">
                <div class="col-xs-4" style="padding-right: 0px;">
                    <h5>No of SMS credits used - <span id="smsTotal" runat="server"></span></h5>
                </div>
                <div class="col-xs-4">
                    <h5>No of Email credits used - <span id="emailTotal" runat="server"></span></h5>
                </div>
                <div class="col-xs-4">
                    <h5>No of Whatsapp credits used - <span id="whatsappTotal" runat="server"></span></h5>
                </div>
            </div>
            </div>
            <p>&nbsp;</p>
            <div class="row" runat="server" id="LoadedDiv">
                <div class="col-lg-12" style="overflow: auto;">
                    <div class="table-responsive">
                        <asp:GridView ID="EntriesGV" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Tick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LogID" HeaderText="LogID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                <%--     <asp:TemplateField HeaderText="Convert To Winner">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Pick Entry" CssClass="btn btn-light" ID="ConvertWinner" OnClick="ConvertWinner_Click" Visible='<%# (Convert.ToBoolean(Eval("IsValid")) && Convert.ToBoolean(Eval("ExcludePastWinner"))) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LogDate" Text='<%# (Convert.ToDateTime(Eval("LogDate"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Recipient" HeaderText="Recipient" />
                                <asp:BoundField DataField="LogType" HeaderText="Log Type" />
                                <asp:BoundField DataField="Content" HeaderText="Content" />
                                <asp:BoundField DataField="CreditsUsed" HeaderText="Credits Used" />
                            </Columns>
                        </asp:GridView>
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
    <!-- Modal -->
    <div id="divPopUpNoCancel" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblModalNoCancel" Text=""> </asp:Label></h3>
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
</asp:Content>
