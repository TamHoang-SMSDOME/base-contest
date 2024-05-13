<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout.Master" AutoEventWireup="true" CodeBehind="PickWinners.aspx.cs" Inherits="BaseContest_WebForms.Views.PickWinners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-3">
                    <h1>Winner Selection</h1>
                    <p>Pick Winner based on group</p>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Group Name</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" ID="GroupName" CssClass="btn btn-light" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Number of Winners to Pick</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" ID="NoOfRecords" CssClass="btn btn-light" Text="" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <p>&nbsp;</p>
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
                    <div class="row" runat="server" visible="false">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Exclude Past Winner's NRIC?</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:CheckBox runat="server" ID="ExcludePastNRIC"/>
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Exclude Past Winner's Mobile?</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:CheckBox runat="server" ID="ExcludePastMob" Checked="true" />
                        </div>
                    </div>
                    <p>&nbsp;</p>
                    <div class="row" runat="server" visible="false">
                        <div class="col-lg-offset-2 col-lg-2">
                            <label>Entry Source</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:DropDownList CssClass="form-control" ID="ddlEntryType" runat="server">
                                <asp:ListItem Text="Select All" Value="Select All" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="SMS" Value="SMS"></asp:ListItem>
                                <asp:ListItem Text="WEB" Value="WEB"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <p>&nbsp;</p>
                    <div class="row" runat="server">
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
                                CssClass="btn btn-primary" Text="Pick Winners" OnClick="Filter_Click" />
                        </div>
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
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Tick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                <asp:TemplateField HeaderText="Date Won">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateWon" Text='<%# (Convert.ToDateTime(Eval("DateWon"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EntryID" HeaderText="EntryID" Visible="false" />
                                <asp:TemplateField HeaderText="Date of Entry">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DateEntry" Text='<%# (Convert.ToDateTime(Eval("DateEntry"))).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" />
                                <asp:BoundField DataField="EntryText" HeaderText="EntryText" />
                                <%--<asp:BoundField DataField="IsValid" HeaderText="Is Valid" />--%>
                        <%--        <asp:BoundField DataField="Reason" HeaderText="Reason" />--%>
                                <%--  <asp:BoundField DataField="Chances" HeaderText="Chances" />
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
                                <asp:BoundField DataField="WinnerID" HeaderText="WinnerID" Visible="false" />
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
 
</asp:Content>

