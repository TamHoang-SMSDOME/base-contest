<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="Winners.aspx.cs" Inherits="BaseContest_WebForms.Views.Winners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>View Winners</h1>
                    <p>Displays all winners</p>
                    <!--   <div class="row">
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
                            <label>Won Start Date</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="wstartDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Won End Date</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="wendDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                        </div>
                    </div>-->
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Winner Groups</label>
                        </div>
                        <div class="col-lg-2">

                            <asp:DropDownList CssClass="form-control" ID="ddlGroupName" runat="server">
                                <%-- <asp:ListItem Text="Select All" Value="Select All" Selected="true"></asp:ListItem>--%>
                            </asp:DropDownList>

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
                                Text="Purge Selected Winners" OnClientClick="$('#divConfirm').modal('show');return false;" />
                        </div>
                    </div>

                    <div class="row" runat="server" id="ExportDiv">
                        <div class="col-lg-offset-4 col-lg-2">
                            <asp:Button CssClass="btn btn-default" runat="server" ID="ExportToCSV"
                                Text="Export To CSV using comma" OnClick="ExportToCsv_click" />
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
                        <asp:Button CssClass="btn btn-default" runat="server" ID="FirstPage"
                            Text="First Page" OnClick="FirstPage_Click" />

                        <asp:Button CssClass="btn btn-default" runat="server" ID="PreviousPage"
                            Text="<" OnClick="PreviousPage_Click" />

                        <asp:TextBox runat="server" ID="CurrentPage" Style="width: 4%" TextMode="Number" Text="1"></asp:TextBox>

                        <span class="label label-default">/
                            <asp:Label runat="server" ID="lblTotalPages"></asp:Label></span>

                        <asp:Button CssClass="btn btn-default" runat="server" ID="Go"
                            Text="GO" OnClick="Filter_Click" />

                        <asp:Button CssClass="btn btn-default" runat="server" ID="NextPage"
                            Text=">" OnClick="NextPage_Click" />

                        <asp:Button CssClass="btn btn-default" runat="server" ID="LastPage"
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
                        <asp:GridView ID="WinnersGV" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Tick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="WinnerID" HeaderText="WinnerID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                <asp:TemplateField HeaderText="Date Won">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateWon" Text='<%# (Convert.ToDateTime(Eval("DateWon"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EntryID" HeaderText="EntryID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                <asp:TemplateField HeaderText="Date of Entry">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateEntry" Text='<%# (Convert.ToDateTime(Eval("DateEntry"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" />
                                <asp:BoundField DataField="EntryText" HeaderText="EntryText" />
                                <%--<asp:BoundField DataField="IsValid" HeaderText="Is Valid" />--%>
                      <%--          <asp:BoundField DataField="Reason" HeaderText="Reason" />--%>
                    <%--            <asp:BoundField DataField="Chances" HeaderText="Chances" />
                                <asp:BoundField DataField="EntrySource" HeaderText="Source of Entry" />--%>
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <%--<asp:BoundField DataField="NRIC" HeaderText="NRIC" />--%>
                                <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt Number" />
                                                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Retailer" HeaderText="Retailer" />
                                <%--<asp:BoundField DataField="Email" HeaderText="Email" />--%>
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

                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--<table st-table="Entries" class="table table-striped">
                        <thead>
                            <tr>
                                @if (signedin && User.IsInRole("Superusers"))
                                {
                                   
                                <th>
                                    <input type="checkbox" ng-model="Check" ng-click="CheckAll()" />
                                </th>
                                }
                                @*<th>No.</th>
                                *@
                               
                                <th>DateEntry</th>
                                <th ng-repeat="h in EntryHeaders" ng-if="h != 'Checked' && h!= 'DateEntry' && h != 'FileLink'">{{h}}</th>

                                <th ng-if="EntryHeaders.indexOf('FileLink') != -1">FileLink</th>
                                @*
                                   
                                <th>EntryText</th>
                                <th>Name</th>
                                <th>NRIC</th>
                                <th>Amount</th>
                                <th>ReceiptNo</th>
                                <th>Valid</th>
                                <th>Reason</th>
                                *@
                           
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="row in Entries">
                                @if (signedin && User.IsInRole("Superusers"))
                                {
                                   
                                <td>
                                    <input type="checkbox" ng-model="row.Checked" />
                                </td>
                                }
                                @*<td>{{((Selections.Page -1) * Selections.PageSize) + $index + 1}}</td>
                                *@
                               
                                <td>{{row.DateEntry | date : dd-MMM-yyyy}}</td>

                                <td ng-repeat="h in EntryHeaders" ng-if="h != 'Checked' && h!= 'DateEntry' && h != 'FileLink' ">{{row[h]}}
                                </td>

                                <td ng-if="row.hasOwnProperty('FileLink')">
                                    <a ng-click="GetLink(row.FileLink)">{{row.FileLink.split("/")[row.FileLink.split("/").length - 1]}}</a>
                                </td>


                                @*
                                                                
                                <td>{{row.DOB | date : dd-MMM-yyyy}}</td>
                                <td>{{row.EntryText}}</td>
                                <td>{{row.Name}}</td>
                                <td>{{row.NRIC}}</td>
                                <td>{{row.Amount}}</td>
                                <td>{{row.ReceiptNo}}</td>
                                <td>{{row.IsValid}}</td>
                                <td>{{row.Reason}}</td>
                                *@
                           
                            </tr>
                        </tbody>
                    </table>--%>
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
    <div id="divConfirm" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirm" Text="Are you sure to delete entry(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeSelected"
                        Text="Confirm" OnClick="PurgeSelected_Click" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
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
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblConfirmAll" Text="Are you sure to delete winner(s)?"> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="PurgeAll"
                        Text="Confirm" OnClick="Purge_Click" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
  
</asp:Content>

