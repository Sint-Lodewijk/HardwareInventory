<%@ Page Title="Add a license" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="add-license.aspx.cs" Inherits="Toestellenbeheer.Manage.add_license" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row no-15 sub-title-bar blue-title">
        <div class="container">
            <div class="col-sm-12">
                <p class="text-center head-text">Fill those info to add a license or a licensefile into the database.</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="LicenseAdd" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="licenseName" runat="server" AssociatedControlID="txtLicenseName" CssClass="control-label col-sm-2">Name</asp:Label>
    <div class="col-sm-4">
        <asp:TextBox ID="txtLicenseName" placeholder="Name of license" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <asp:Label ID="licenseCode" runat="server" AssociatedControlID="txtLicenseCode" CssClass="control-label col-sm-2">Code: </asp:Label>
    <div class="col-sm-4">
        <asp:TextBox ID="txtLicenseCode" placeholder="Enter the licensecode" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label AssociatedControlID="txtDatepicker" CssClass="control-label col-sm-2" runat="server">Expire Date</asp:Label>
        <div class="col-sm-10">

            <div class="input-group date" id="input-date">
                <asp:TextBox runat="server" ID="txtDatepicker" placeholder="Click to select a date." CssClass="form-control" />
                <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
            </div>

        </div>
        <script>
            $(function () {
                $("[id$=input-date]").datepicker({
                    format: "dd-mm-yyyy",
                    autoclose: true,
                    todayHighlight: true
                });
            });
        </script>
    </div>
    <div class="form-group">
        <asp:Label AssociatedControlID="txtExtraInfoLicense" runat="server" CssClass="control-label col-sm-2" Text="Extra info:"></asp:Label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtExtraInfoLicense" placeholder="Extra information of this license" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <asp:Label AssociatedControlID="LicenseFileUpload" runat="server" CssClass="control-label col-sm-2">License File:</asp:Label>
    <div class="input-group col-sm-10 input-upload-adjust">
        <asp:FileUpload ID="LicenseFileUpload" CssClass="form-control" runat="server" />
        <span class="input-group-btn">
            <asp:Button ID="btnUploadLicense" runat="server" CssClass="btn btn-info" Text="Upload" OnClick="btnUploadLicense_Click" />
        </span>
    </div>

    <div class="col-sm-10 col-sm-offset-2 form-group hint-block">
        <asp:Label ID="ResultUploadAtta" runat="server" Text="" CssClass="hint-block"></asp:Label>
        <asp:Label ID="TestlocationAtt" runat="server" Text="" CssClass="hint-block"></asp:Label>
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
                            <asp:GridView ID="grvHardwareLicenseSelect" EmptyDataText="No availible hardware in database." CssClass="table table-hover table-striped gridview" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr,serialNr" OnRowDataBound="OnRowDataBound" OnPageIndexChanged="grvHardwareLicenseSelect_PageIndexChanged" DataSourceID="HardwareLicense" OnSorting="displayHardwarePanel">
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
                            <asp:Button ID="btnAssignToSelectedHardware" runat="server" Text="Assign" CssClass="btn btn-primary" OnClick="assignToSelectedHardware_Click" />
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
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
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
    <div class="btn-group-wrap">
        <div class="btn-group btn-group-justified">
            <div class="btn-group" role="group">

                <asp:Button ID="hideShowHardware" runat="server" Text="Assign to hardware" OnClick="hideShowHardware_Click" CssClass="btn btn-info" />
            </div>
            <div class="btn-group" role="group">
                <asp:Button runat="server" Text="Add license only" ID="btnAddLicense" CssClass="btn btn-primary" OnClick="btnAddLicense_click" />
            </div>
            <div class="btn-group" role="group">
                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#peopleModal">Assign to people</button>
            </div>
        </div>
    </div>
    <div class="form-group col-sm-12">
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
