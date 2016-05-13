<%@ Page Title="Hardware overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" EnableEventValidation="false" Inherits="Toestellenbeheer.hardware_overview" %>

<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_grvHardware").tablesorter();
        });
    </script>
    <asp:UpdatePanel runat="server" ID="udpHardware" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="searchPanel" runat="server">
                <div id="search" class="form-group">
                    <asp:Label ID="lblSearch" runat="server" CssClass="control-label col-sm-1" AssociatedControlID="txtSearch">Search</asp:Label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:Label ID="lblOn" runat="server" AssociatedControlID="drpSearchItem" CssClass="control-label">on</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="drpSearchItem" CssClass="form-control" runat="server">
                            <asp:ListItem Value="internalNr">Internal Nr</asp:ListItem>
                            <asp:ListItem Value="manufacturerName">Manufacturer</asp:ListItem>
                            <asp:ListItem Value="type">Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Search" />
                    </div>
                </div>
            </asp:Panel>
            <asp:GridView ID="grvHardware" OnPreRender="grvHardware_PreRender" OnSelectedIndexChanged="details" data-toggle="modal" data-target="#HardwareDetailModal" OnRowDataBound="OnRowDataBound" OnRowDeleting="details" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" DataKeyNames="internalNr, serialNr" runat="server">
                <Columns>
                    <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="Serial Nr" />
                    <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="Internal Nr" />
                    <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="Manufacturer" />
                    <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                    <asp:BoundField DataField="modelNr" HeaderText="Model Nr" />
                </Columns>
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

            <div class="form-group">
                <asp:Label ID="lblGridTotalResult" CssClass="col-sm-12" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="grvHardware" />
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Modal -->
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
                            <h4 class="modal-title" id="modalTitle" runat="server">Loading...</h4>
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
                                <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#modalConfirm">
                                    Remove
                                </button>
                                <asp:Button ID="btnModifying" runat="server" OnClick="btnModifying_Click" Text="Modify" CssClass="btn btn-primary" />
                                <asp:Label ID="lblTotalQuery" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblInternalNr" Visible="false" runat="server" Text='<%#Eval("Internal Nr")%>' />
                                <asp:Label ID="lblProblem" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnModifying" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="modal fade" id="modalConfirm" tabindex="-1" role="dialog" aria-labelledby="confirmLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="confirmLabel">Confirm remove</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure to remove this hardware? This action is not reversible, but you can stil restore it from the previous backup.</p>
                                <p>Continue to proceed?</p>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-danger" Text="Remove" OnClick="btnRemove_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
