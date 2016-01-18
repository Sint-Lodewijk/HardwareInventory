<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-license.aspx.cs" Inherits="Toestellenbeheer.Manage.add_license" %>

<asp:Content ID="LicenseAdd" ContentPlaceHolderID="MainContent" runat="server">



    <div id="hardwareGrides">


        <div class="form-group">
            <asp:Label ID="licenseName" runat="server" AssociatedControlID="txtLicenseName" CssClass="control-label col-sm-2">License name</asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtLicenseName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Label ID="licenseCode" runat="server" AssociatedControlID="txtLicenseCode" CssClass="control-label col-sm-2">License code: </asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtLicenseCode" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Button ID="hideShowHardware" runat="server" Text="Assign to hardware" OnClick="hideShowHardware_Click" CssClass="btn btn-info form-control" />
            </div>
        </div>
        <asp:Panel ID="hardwarePanel" runat="server">
            <div id="search" class="form-group">

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
                        <asp:ListItem Value="typeNr">Type Nr</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default margin-top-5" OnClick="Search_Click" /> 
                </div>
            </div>
            <asp:GridView ID="licenseOverviewGridSearch" OnSelectedIndexChanged="display_search_button" CssClass="table table-hover table-striped gridview" runat="server">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>

                <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <asp:SqlDataSource ID="HardwareLicense" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT typeNr, manufacturerName, internalNr, serialNr  FROM hardware;"></asp:SqlDataSource>
            <asp:GridView ID="grvHardwareLicenseSelect" CssClass="table table-hover table-striped gridview" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr,serialNr" OnSelectedIndexChanged="hardwareLicenseSelection_Click" DataSourceID="HardwareLicense" OnSorting="displayHardwarePanel">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />
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
            <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                    <asp:Button ID="btnAssignToSelectedHardware" runat="server" Text="Assign" CssClass="btn btn-primary margin-top-5" OnClick="assignToSelectedHardware_Click" />
                </div>
                <div class="col-sm-offset-5 col-sm-7">

                    <asp:Button ID="btnAssignToSelectedHardwareSearch" runat="server" Text="Assign" CssClass="btn btn-primary margin-top-5" OnClick="assignToSelectedHardwareSearch_Click" />

                </div>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Button ID="hideShowPeople" runat="server" Text="Assign to people" OnClick="hideShowPeople_Click" CssClass="btn btn-info form-control" />
                <asp:Panel ID="peoplePanel" CssClass="table table-hover table-striped gridview" GridLines="None" runat="server">
                    <asp:GridView ID="licenseOverviewGridPeopleSearch" runat="server">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>

                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>

    <asp:Label ID="testLabel" runat="server" Text=""></asp:Label>
</asp:Content>
