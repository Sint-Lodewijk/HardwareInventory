﻿<%@ Page Title="Hardware history" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-history.aspx.cs" Inherits="Toestellenbeheer.Archive.hardware_history" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="modal fade foreground" id="modalDownload" tabindex="-1" role="dialog" aria-labelledby="modalDownloadTitle">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="modalDownloadTitle">Downloading your file...</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeDownload" class="center-block no-border hide" runat="server"></iframe>
                    <asp:LinkButton ID="lnkDownloadB" OnClick="lnkDownloadB_Click" runat="server"></asp:LinkButton>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="modalHardware" runat="server" CssClass="modal fade" TabIndex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="modalTitle" runat="server">Details</h4>
                        </div>
                        <div class="modal-body">
                            <div id="carousel-details" class="carousel slide" data-ride="carousel">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                    <li data-target="#carousel-details" data-slide-to="0" class="active"></li>
                                    <li data-target="#carousel-details" data-slide-to="1"></li>
                                </ol>
                                <!-- Wrapper for slides -->
                                <div class="carousel-inner" role="listbox">
                                    <div class="item active">
                                        <asp:Image runat="server" ID="imgHardware" CssClass="img-responsive center-block" />
                                    </div>
                                    <div class="item">
                                        <asp:GridView ID="grvDetail" DataKeyNames="internalNr" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table class="table table-striped table-hover table-responsive">
                                                            <tr>
                                                                <td class="col-sm-6">
                                                                    <asp:Label ID="Label8" runat="server" Text="Purchase date: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td class="col-sm-6">
                                                                    <asp:Label ID="lblPDate" runat="server" Text='<%#Eval("purchaseDate")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="col-sm-6">
                                                                    <asp:Label ID="Label4" runat="server" Text="Model Nr: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td class="col-sm-6">
                                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("modelNr")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text="Type: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Type")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text="Manufacturer: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("manufacturerName")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="Serial Nr: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("serialNr")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="Internal Nr: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblInternalNr" runat="server" Text='<%#Eval("internalNr")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="Warranty: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("warranty")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text="Extra info: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("extraInfo")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" Text="Added date: ">
                                                                    </asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("addedDate")%>'>
                                                                    </asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text="Attachment: ">
                                                                    </asp:Label></td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="panelDownload">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- Controls -->
                                <a class="left carousel-control" href="#carousel-details" role="button" data-slide="prev">
                                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#carousel-details" role="button" data-slide="next">
                                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    <asp:GridView ID="grvHardware" EmptyDataText="There are no hardware present in the database!" OnRowDeleting="grvHardware_RowDeleting" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvHardware_OnRowDataBound" OnSelectedIndexChanged="grvHardware_SelectedIndexChanged" DataKeyNames="internalNr">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
            <asp:BoundField DataField="modelNr" HeaderText="Model Nr" />
            <asp:CommandField DeleteText="Details" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Panel ID="modalHardwarePeople" runat="server" CssClass="modal fade" TabIndex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetailsP" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="modalTitleP" runat="server">People Linked</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="grvPeopleLinked" CssClass="table table-hover table-striped gridview" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="nameAD" HeaderText="Domain Name" />
                                    <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" />
                                    <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" NullDisplayText="Not returned yet" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
