<%@ Page Title="License overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="license-overview.aspx.cs" Inherits="Toestellenbeheer.Overview.license_overview" EnableEventValidation="false" %>

<asp:Content ID="LicenseContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvLicenseCode" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="grvLicense_SelectedIndexChanged" DataKeyNames="licenseCode" OnRowDataBound="OnRowDataBound" OnRowDeleting="grvLicense_RowDeleting" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqlLicenseCode">
        <Columns>
            <asp:BoundField DataField="licenseName" HeaderText="licenseName" SortExpression="licenseName" />
            <asp:BoundField DataField="licenseCode" HeaderText="licenseCode" SortExpression="licenseCode" />
            <asp:BoundField DataField="expireDate" HeaderText="expireDate" SortExpression="expireDate" />
            <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="extraInfo" />
        </Columns>


        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseCode, expireDate, extraInfo FROM license WHERE (licenseFileLocation = NULL) OR (licenseFileLocation = '')"></asp:SqlDataSource>
    <asp:GridView ID="grvLicenseAssignedPeople" OnSelectedIndexChanged="grvLicenseAssignedPeople_SelectedIndexChanged" CssClass="table table-hover table-striped gridview" OnRowDeleting="grvLicenseAssignedPeople_RowDeleting" DataKeyNames="licenseEventID" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUnassign" runat="server" CommandName="Delete" OnClientClick="if (!confirm('Are you sure to unassign selected people with the license?')) return false;" Text="Unassign"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:GridView ID="grvLicenseAssignedHardware" CssClass="table table-hover table-striped gridview" OnRowDeleting="grvLicenseAssignedHardware_RowDeleting" DataKeyNames="internalNr" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUnassign" runat="server" OnClientClick="if (!confirm('Are you sure to unassign selected people with the license?')) return false;" CommandName="Delete" Text="Unassign"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="form-group">
        <div class="col-sm-12">
            <asp:Label ID="lblCountPeople" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblCountHardware" runat="server" Text=""></asp:Label>

        </div>
    </div>
    <asp:Label ID="lblProblem" runat="server"></asp:Label>

</asp:Content>
