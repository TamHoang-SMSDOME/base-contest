<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="Entries.aspx.cs" Inherits="BaseContest_WebForms.Views.Entries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>View Entries</h1>
                    <p>Displays all entries</p>
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
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Entry Validity</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:DropDownList CssClass="form-control" ID="ddlValidity" runat="server">
                                <asp:ListItem Text="Select All" Value="Select All" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Valid" Value="Valid"></asp:ListItem>
                                <asp:ListItem Text="InValid" Value="Invalid"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server" visible="false">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Entry Status</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server">
                                <asp:ListItem Text="Select All" Value="Select All" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Verified" Value="Verified"></asp:ListItem>
                                <asp:ListItem Text="Void" Value="Void"></asp:ListItem>
                                <asp:ListItem Text="Resent" Value="Resent"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row" runat="server" visible="false">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>With Receipt Uploaded? - Yes</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:CheckBox runat="server" ID="cbUploadStatus" Checked="false" />
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button runat="server" ID="Filter"
                                CssClass="btn btn-primary" Text="Filter" OnClick="Filter_Click" />
                        </div>
                    </div>

                    <div class="row" runat="server" id="PurgeSel">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeSelec"
                                Text="Purge Selected Entries" OnClientClick="$('#divConfirm').modal('show');return false;" />
                        </div>
                    </div>

                    <div class="row" runat="server" id="ExportDiv">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-light" runat="server" ID="ExportToCSV"
                                Text="Export To CSV using comma" OnClick="ExportToCsv_click" />
                        </div>

                    </div>

                    <div class="row" runat="server" id="DownloadDiv">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-light" runat="server" ID="DownloadFiles"
                                Text="Download Files" OnClick="DownloadFiles_click" />
                        </div>

                    </div>

                    <div class="row" runat="server" id="PurgeDiv">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-danger" runat="server" ID="Purge"
                                Text="Purge" OnClientClick="$('#divConfirmAll').modal('show'); return false;" />
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
                                <asp:BoundField DataField="EntryID" HeaderText="EntryID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                <%--     <asp:TemplateField HeaderText="Convert To Winner">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Pick Entry" CssClass="btn btn-light" ID="ConvertWinner" OnClick="ConvertWinner_Click" Visible='<%# (Convert.ToBoolean(Eval("IsValid")) && Convert.ToBoolean(Eval("ExcludePastWinner"))) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Date of Entry">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateEntry" Text='<%# (Convert.ToDateTime(Eval("DateEntry"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" />
                                <asp:BoundField DataField="EntryText" HeaderText="EntryText" />
                                <asp:BoundField DataField="IsValid" HeaderText="Is Valid" />
                                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                <asp:BoundField DataField="EntrySource" HeaderText="Entry Source" />
                                <%--  <asp:BoundField DataField="Chances" HeaderText="Chances" />
                                <asp:BoundField DataField="EntrySource" HeaderText="Source of Entry" />--%>
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <%--<asp:BoundField DataField="NRIC" HeaderText="NRIC" />--%>
                                <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt Number" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Retailer" HeaderText="Retailer" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Amount" Text='<%# (Convert.ToDecimal(Eval("Amount"))).ToString("0.00") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Link">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="FileLink" Target="_blank" NavigateUrl='<%# Eval("FileLink") %>' Text='<%# ((Eval("FileLink")).ToString() == "" ||( Eval("FileLink")).ToString().ToUpper().Contains(".PDF") != true) ? "" : "View"  %>'></asp:HyperLink>
                                        <a data-magnify="gallery" 
                                           data-caption='<%# Eval("ReceiptNo") %>'
                                           data-group=""
                                           href='<%# Eval("FileLink") %>'> <asp:Label runat="server" ID="ViewReceipt" Text='<%# ((Eval("FileLink")).ToString() == ""||( Eval("FileLink")).ToString().ToUpper().Contains(".PDF") == true) ? "" : "View"  %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Resent Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateResent" Text='<%# (Eval("DateResent")).ToString() == "" ? "" :(Convert.ToDateTime(Eval("DateResent"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Verified">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Verify" CssClass="btn btn-light" ID="Verified"
                                            OnClientClick="return confirm('Are you sure you want to proceed?');"
                                            OnClick="Verified_Click" Visible='<%# (Eval("FileLink").ToString() != ""  && Convert.ToBoolean(Eval("IsValid")) == true  && Convert.ToBoolean(Eval("IsVerified")) == false) %>' />
                                        <asp:Label Visible='<%# Convert.ToBoolean(Eval("IsVerified")) == true %>' Text='<%# (Eval("DateVerified")).ToString() == "" ? "" :(Convert.ToDateTime(Eval("DateVerified"))).ToString("dd MMM yyyy HH:mm:ss") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Reject" CssClass="btn btn-light" ID="InEligible"
                                            OnClientClick="return confirm('Are you sure you want to proceed?');"
                                            OnClick="InEligible_Click" Visible='<%# (Eval("FileLink").ToString() != "" && Convert.ToBoolean(Eval("IsValid")) == true && Convert.ToBoolean(Eval("IsVerified")) == false) %>' />
                                            <asp:Label Visible='<%# Convert.ToBoolean(Eval("IsRejected")) == true%>' Text='<%# (Eval("DateRejected")).ToString() == "" ? "" :(Convert.ToDateTime(Eval("DateRejected"))).ToString("dd MMM yyyy HH:mm:ss") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Resend">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Resend" CssClass="btn btn-light" ID="Resend"
                                            OnClientClick="return confirm('Are you sure you want to proceed?');"
                                            OnClick="Resend_Click" Visible='<%# (Eval("FileLink").ToString() != "" && Convert.ToBoolean(Eval("IsValid")) == true && Convert.ToBoolean(Eval("IsVerified")) == false) %>' />
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
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
    <div id="divPopUpNoCancel" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblModalNoCancel" Text=""> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>
    <!-- Modal -->
    <div id="divConfirm" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirm" Text="Are you sure to delete entry(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeSelected"
                        Text="Confirm" OnClick="PurgeSelected_Click" />
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>

    <div id="divConfirmAll" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirmAll" Text="Are you sure to delete entry(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeAll"
                        Text="Confirm" OnClick="Purge_Click" />
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
</asp:Content>

