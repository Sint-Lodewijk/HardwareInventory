<%@ Page Title="Hardware overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" EnableEventValidation="false" Inherits="Toestellenbeheer.hardware_overview" %>

<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
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
                    <asp:ListItem Value="typeNr">Type Nr</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">

                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default margin-top-5" OnClick="Search" />
            </div>
        </div>
    </asp:Panel>

    <asp:GridView ID="HardwareOverviewGridSearch" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" DataKeyNames="pictureLocation,attachmentLocation" runat="server">
        <Columns>

            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />

            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkShowMoreInfo" runat="server" CommandName="Delete" Text="Details"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>

        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    <asp:GridView ID="HardwareOverviewGrid" OnRowDeleting="details" OnRowDataBound="OnRowDataBound" CssClass="table table-hover table-striped gridview" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkShowMoreInfo" runat="server" CommandName="Delete" Text="Details"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>


        <EditRowStyle BackColor="#999999" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:GridView ID="selectedRow" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" runat="server">
        <Columns>
          
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:TemplateField>

                <ItemTemplate>

                    <table class="table table-striped table-hover">

                        <tr>
                            <td class="col-sm-6">
                                <asp:Label ID="Label8" runat="server" Text="Purchase date: ">
                                </asp:Label>
                            </td>
                            <td class="col-sm-6">
                                <asp:Label ID="lblPDate" runat="server" Text='<%#Eval("Purchase date")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Type Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Type nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Manufacturer: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Manufacturer")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Serial Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Serial Nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Internal Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Internal Nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Warranty: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Warranty")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Extra info: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("Extra info")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Added date: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Added date")%>'>
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
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>

    <asp:GridView ID="grvPeopleLinked" CssClass="table table-hover table-striped gridview" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="Serial Nr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" />
            <asp:BoundField DataField="nameAD" HeaderText="Domain Name" />
            <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" />
            <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" NullDisplayText="Not returned yet" />

        </Columns>
    </asp:GridView>
    <asp:Button ID="btnReturn" runat="server" Text="Return to the overview page" OnClick="btnReturn_Click" CssClass="btn btn-primary" />
    
    <asp:Label ID="lblTotalQuery" runat="server" Text=""></asp:Label>
 
</asp:Content>

