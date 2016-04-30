<%@ Page Title="License overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="license-overview.aspx.cs" Inherits="Toestellenbeheer.Overview.license_overview" EnableEventValidation="false" %>

<asp:Content ID="LicenseContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#code" aria-controls="code" role="tab" data-toggle="tab">License Code</a></li>
        <li role="presentation"><a href="#file" aria-controls="file" role="tab" data-toggle="tab">License File</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="code">
            <asp:GridView ID="grvLicenseCode" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="grvLicense_SelectedIndexChanged" DataKeyNames="licenseCode" OnRowDataBound="OnRowDataBound" OnRowDeleting="grvLicense_RowDeleting" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqlLicenseCode">
                <Columns>
                    <asp:BoundField DataField="licenseName" HeaderText="licenseName" SortExpression="licenseName" />
                    <asp:BoundField DataField="licenseCode" HeaderText="licenseCode" SortExpression="licenseCode" />
                    <asp:BoundField DataField="expireDate" HeaderText="expireDate" SortExpression="expireDate" />
                    <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="extraInfo" />
                </Columns>


                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />

            </asp:GridView>
            <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseCode, expireDate, extraInfo FROM license WHERE (licenseFileLocation = NULL) OR (licenseFileLocation = '')"></asp:SqlDataSource>

        </div>
        <div role="tabpanel" class="tab-pane" id="file">
            <asp:SqlDataSource ID="sqlLicenseFile" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseFileLocation, expireDate, extraInfo FROM license WHERE (licenseCode = NULL) OR (licenseCode = '')"></asp:SqlDataSource>
            <asp:GridView runat="server" DataSourceID="sqlLicenseFile" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="licenseName" HeaderText="licenseName" SortExpression="licenseName"></asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="licenseFileLocation" DataTextFormatString="~/UserUploads/License/{0}" HeaderText="licenseFileLocation" SortExpression="licenseFileLocation"></asp:HyperLinkField>
                    <asp:BoundField DataField="expireDate" HeaderText="expireDate" SortExpression="expireDate"></asp:BoundField>
                    <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="extraInfo"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


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
