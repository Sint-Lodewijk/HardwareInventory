<%@ Page Title="My license" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="my-license.aspx.cs" Inherits="Toestellenbeheer.User.my_license" %>
<asp:Content ID="MyLicense" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#licenseCode" aria-controls="home" role="tab" data-toggle="tab">License Code</a></li>
            <li role="presentation"><a href="#licenseFile" aria-controls="profile" role="tab" data-toggle="tab">License File</a></li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active fade in" id="licenseCode">
                <asp:GridView ID="grvMyLicenseCode" EmptyDataText="You do not have any license code yet." CssClass="gridview table table-hover table-striped" runat="server" DataSourceID="sqlLicenseCode" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                        <asp:BoundField DataField="licenseCode" HeaderText="License Code" SortExpression="licenseCode" />
                        <asp:BoundField DataField="expireDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Expire Date" SortExpression="expireDate" />
                        <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="Info" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblLicenseCode" runat="server"  CssClass="control-label"></asp:Label>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="licenseFile">
                <asp:GridView ID="grvMyLicenseFile" EmptyDataText="You do not have any license file yet." CssClass="gridview table table-hover table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="sqlLicenseFile">
                    <Columns>
                        <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                        <asp:BoundField DataField="expireDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Expire Date" SortExpression="expireDate" />
                        <asp:BoundField DataField="extraInfo" HeaderText="Extra info" SortExpression="Info" />
                        <asp:TemplateField HeaderText="License File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("licenseFileLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("licenseFileLocation")).Length < 1 ? "" : Convert.ToString(Eval("licenseFileLocation")) %>'>License File</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblLicenseFile" runat="server"  CssClass="control-label"></asp:Label>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT license.licenseName, license.licenseCode,  DATE_FORMAT(license.expireDate, '%Y-%m-%d') 'expireDate', license.extraInfo FROM license INNER JOIN licenseHandler ON license.licenseID = licenseHandler.licenseID WHERE (license.licenseCode &lt;&gt; '' OR license.licenseCode &lt;&gt; NULL) AND (licenseHandler.eventID = @eventID)
">
        <SelectParameters>
            <asp:SessionParameter Name="@eventID" SessionField="eventID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlLicenseFile" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT license.licenseName, license.licenseFileLocation, DATE_FORMAT(license.expireDate, '%Y-%m-%d') 'expireDate', license.extraInfo FROM license INNER JOIN licenseHandler ON license.licenseID = licenseHandler.licenseID WHERE (license.licenseFileLocation &lt;&gt; '') OR (license.licenseFileLocation &lt;&gt; NULL) AND eventID = @eventID">
        <SelectParameters>
            <asp:SessionParameter Name="@eventID" SessionField="eventID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
