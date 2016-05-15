<%@ Page Title="Assign license" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assign-license.aspx.cs" Inherits="Toestellenbeheer.Manage.assign_license" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#code" aria-controls="code" role="tab" data-toggle="tab">License Code</a></li>
        <li role="presentation"><a href="#file" aria-controls="file" role="tab" data-toggle="tab">License File</a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="code">
            <asp:GridView ID="grvLicenseCode" EmptyDataText="No availible license." CssClass="table table-hover table-striped gridview" runat="server" DataKeyNames="licenseCode" OnRowDataBound="OnRowDataBound" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqlLicenseCode">
                <Columns>
                    <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                    <asp:BoundField DataField="licenseCode" HeaderText="License Code" SortExpression="licenseCode" />
                    <asp:BoundField DataField="expireDate" HeaderText="Expire Date" SortExpression="expireDate" />
                    <asp:BoundField DataField="extraInfo" HeaderText="Extra Info" SortExpression="extraInfo" />
                </Columns>
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseCode, DATE_FORMAT(expireDate, '%Y-%m-%d') 'expireDate', extraInfo FROM license WHERE (licenseFileLocation = NULL) OR (licenseFileLocation = '')"></asp:SqlDataSource>
        </div>
        <div role="tabpanel" class="tab-pane" id="file">
            <asp:SqlDataSource ID="sqlLicenseFile" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseFileLocation, DATE_FORMAT(expireDate, '%Y-%m-%d') 'expireDate', extraInfo FROM license WHERE (licenseCode = NULL) OR (licenseCode = '')"></asp:SqlDataSource>
            <asp:GridView runat="server" EmptyDataText="No availible license file." ID="grvLicenseFile" DataKeyNames="licenseFileLocation"  DataSourceID="sqlLicenseFile" CssClass="table table-hover table-striped gridview" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName"></asp:BoundField>
                    <asp:BoundField DataField="expireDate" HeaderText="Expire Date" SortExpression="expireDate"></asp:BoundField>
                    <asp:BoundField DataField="extraInfo" HeaderText="Extra Info" SortExpression="extraInfo"></asp:BoundField>
                    <asp:HyperLinkField HeaderText="License File" DataNavigateUrlFields="licenseFileLocation" Text="Download" DataTextFormatString="~/UserUploads/License/{0}" SortExpression="licenseFileLocation"></asp:HyperLinkField>
                </Columns>
            </asp:GridView>
            <asp:Panel runat="server" ID="panelLicense" Visible="false">
                <asp:Button runat="server" ID="btnAssignFile" />
            </asp:Panel>
        </div>
    </div>
    <div class="modal fade" id="modalHardware" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="hardwarePanel" CssClass="modal-content" runat="server">
                <asp:UpdatePanel runat="server" ID="udpHardware" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <div id="search" class="form-group margin-top-5">
                                <asp:Label ID="lblSearch" runat="server" CssClass="control-label col-sm-1 col-sm-offset-1" AssociatedControlID="txtSearch">Search</asp:Label>
                                <div class="col-sm-4">
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
                                    <asp:Button ID="btnSearch" data-toggle="tooltip" data-placement="left" title="Search" runat="server" Text="Search" CssClass="form-control btn btn-default col-sm-12" OnClick="Search_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="licenseOverviewGridSearch" EmptyDataText="No hardware availible according your search criteria." AutoGenerateColumns="false" OnRowDataBound="SearchBound" CssClass="table table-hover table-striped gridview" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                    <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                                    <asp:BoundField DataField="internalNr" HeaderText="Internal nr" ReadOnly="True" SortExpression="internalNr" />
                                    <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
                                </Columns>
                                <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="HardwareLicense" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type, manufacturerName, internalNr, serialNr FROM hardware;"></asp:SqlDataSource>
                            <asp:GridView ID="grvHardwareLicenseSelect" EmptyDataText="No availible hardware in database." CssClass="table table-hover table-striped gridview" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr,serialNr" OnRowDataBound="Hardware_OnRowDataBound" OnPageIndexChanged="grvHardwareLicenseSelect_PageIndexChanged" DataSourceID="HardwareLicense">
                                <Columns>
                                    <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                    <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                                    <asp:BoundField DataField="internalNr" HeaderText="Internal nr" ReadOnly="True" SortExpression="internalNr" />
                                    <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
                                </Columns>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAssignToSelectedHardware" runat="server" Text="Assign" CssClass="btn btn-primary" OnClick="assignToSelectedHardwareSearch_Click" />
                            <asp:Button ID="btnAssignToSelectedHardwareSearch" runat="server" Text="Assign" CssClass="btn btn-primary " OnClick="assignToSelectedHardwareSearch_Click" />
                            <asp:Button ID="btnCloseHardware" runat="server" Text="Cancel" CssClass="btn btn-info" />
                        </div>
                        <div class="form-group col-sm-12">
                            <asp:Label ID="lblSearchResult" runat="server" Text=""></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="btnCloseHardware" />
                        <asp:PostBackTrigger ControlID="btnAssignToSelectedHardware" />
                        <asp:PostBackTrigger ControlID="btnAssignToSelectedHardwareSearch" />
                        <asp:AsyncPostBackTrigger ControlID="licenseOverviewGridSearch" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="grvHardwareLicenseSelect" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>

    <div class="modal fade" id="peopleModal" tabindex="-1" role="dialog" aria-labelledby="pMTitle">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <asp:Panel ID="peoplePanel" runat="server">
                    <asp:UpdatePanel ID="udpPeople" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="pMTitle">Select a people</h4>
                            </div>
                            <div class="modal-body">

                                <asp:GridView ID="licenseOverviewGridPeople" DataKeyNames="Domain Name" OnPreRender="grvPreRender" OnPageIndexChanging="licenseOverviewGridPeople_PageIndexChanging" CssClass="table table-hover table-striped gridview" runat="server" OnRowDataBound="PeopleBound" AllowPaging="True" PageSize="9">
                                    <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="White" />

                                </asp:GridView>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <asp:Button runat="server" ID="btnAssignLicenseToPeople" OnClick="assignLicenseToPeople" CssClass="btn btn-primary " Text="Assign to selected person" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAssignLicenseToPeople" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_licenseOverviewGridPeople").tablesorter();
        });
    </script>
    <asp:Panel runat="server" ID="ShowPanel">
        <div class="btn-group-wrap">
            <div class="btn-group btn-group-justified">
                <div class="btn-group" role="group">
                    <button type="button" class="btn btn-info" data-toggle="modal" data-target="#modalHardware">Assign to hardware</button>
                </div>

                <div class="btn-group" role="group">
                    <button type="button" class="btn btn-info" data-toggle="modal" data-target="#peopleModal">Assign to people</button>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblProblem" runat="server"></asp:Label>
</asp:Content>
