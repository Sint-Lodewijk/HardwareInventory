<%@ Page Title="Manage requests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="manage-requests.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="modalHardware" runat="server" CssClass="modal fade" TabIndex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title" id="modalTitle" runat="server">Details</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Image runat="server" ID="imgHardware" CssClass="img-responsive center-block" />
                            <asp:GridView ID="grvDetail" DataKeyNames="internalNr" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" runat="server">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table class="table table-striped table-hover">
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
                                                        <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    <asp:GridView ID="grvRequests" OnPreRender="grvPreRender" EmptyDataText="There are no open requests!" OnRowDeleting="grvRequests_RowDeleting" CssClass="gridview table table-hover table-striped" OnSelectedIndexChanged="grvRequests_SelectedIndexChanged" OnRowDataBound="grvRequests_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="requestID,nameAD,internalNr,serialNr" DataSourceID="sqlRequest">
        <Columns>
            <asp:BoundField DataField="requestID" HeaderText="Request ID" InsertVisible="False" ReadOnly="True" SortExpression="requestID" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" SortExpression="internalNr" />
            <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
            <asp:BoundField DataField="nameAD" HeaderText="AD Name" SortExpression="nameAD" />
            <asp:BoundField DataField="requestDate" HeaderText="Request Date" SortExpression="requestDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:CommandField DeleteText="Details" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_grvRequests").tablesorter();
        });
    </script>
    <asp:Button ID="btnAcceptRequest" Text="Accept" OnClick="btnAcceptRequest_Click" Visible="false" runat="server" CssClass="btn btn-primary" />
    <asp:Button ID="btnDenyRequest" Text="Deny" OnClick="btnDenyRequest_Click" Visible="false" runat="server" CssClass="btn btn-primary" />
    <asp:Label ID="lblExeption" runat="server"></asp:Label>
    <asp:SqlDataSource ID="sqlRequest" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT request.requestID, request.requestAccepted, request.requestDate, people.nameAD, request.serialNr, request.internalNr, hardware.type FROM request INNER JOIN people ON request.eventID = people.eventID INNER JOIN hardware ON request.serialNr = hardware.serialNr AND request.internalNr = hardware.internalNr WHERE (request.requestAccepted = 0)" DeleteCommand="SELECT requestID FROM request"></asp:SqlDataSource>
</asp:Content>
