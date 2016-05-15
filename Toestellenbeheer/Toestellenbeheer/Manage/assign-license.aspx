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
            <asp:GridView ID="grvLicenseCode" CssClass="table table-hover table-striped gridview" runat="server"  DataKeyNames="licenseCode" OnRowDataBound="OnRowDataBound"  AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqlLicenseCode">
                <Columns>
                    <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                    <asp:BoundField DataField="licenseCode" HeaderText="License Code" SortExpression="licenseCode" />
                    <asp:BoundField DataField="expireDate" HeaderText="Expire Date" SortExpression="expireDate" />
                    <asp:BoundField DataField="extraInfo" HeaderText="Extra Info" SortExpression="extraInfo" />
                </Columns>
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseCode, DATE_FORMAT(expireDate, '%Y-%m-%d') 'expireDate', extraInfo FROM license WHERE (licenseFileLocation = NULL) OR (licenseFileLocation = '')"></asp:SqlDataSource>
            <asp:Panel runat="server" ID="panelCode" Visible="false">
                <asp:Button runat="server" ID="btnAssignCode" CssClass="btn btn-primary" OnClick="btnAssignCode_Click" />

            </asp:Panel>
        </div>
        <div role="tabpanel" class="tab-pane" id="file">
            <asp:SqlDataSource ID="sqlLicenseFile" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT licenseName, licenseFileLocation, DATE_FORMAT(expireDate, '%Y-%m-%d') 'expireDate', extraInfo FROM license WHERE (licenseCode = NULL) OR (licenseCode = '')"></asp:SqlDataSource>
            <asp:GridView runat="server" DataSourceID="sqlLicenseFile" CssClass="table table-hover table-striped gridview" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
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
        <asp:Panel ID="ShowPanel" Visible="false" runat="server">
            <asp:Button ID="btnAPeople" runat="server" CssClass="btn btn-primary" Text="Show People" />
            <asp:Button ID="btnAHardware" runat="server" CssClass="btn btn-primary" Text="Show Hardware" />
        </asp:Panel>
        <asp:Panel runat="server" ID="PeoplePanel" CssClass="innerPopup" Visible="false">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grvLicenseUnassignedPeople" EmptyDataText="0 license key returned from the query!" CssClass="table table-hover table-striped gridview" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAssign" runat="server" CommandName="Delete" OnClientClick="if (!confirm('Are you sure to assign selected people with the license?')) return false;" Text="Assign"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="grvLicenseUnassignedPeople" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:GridView ID="grvLicenseUnassignedHardware" OnRowDataBound="OnRowDataBound" CssClass="table table-hover table-striped gridview" OnRowDeleting="grvLicenseUnassignedHardware_RowDeleting" DataKeyNames="internalNr" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAssign" runat="server" OnClientClick="if (!confirm('Are you sure to assign selected people with the license?')) return false;" CommandName="Delete" Text="Assign"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblProblem" runat="server"></asp:Label>
  </asp:Content>
