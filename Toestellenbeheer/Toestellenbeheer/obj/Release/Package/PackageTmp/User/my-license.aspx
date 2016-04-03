<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="my-license.aspx.cs" Inherits="Toestellenbeheer.User.my_license" %>

<asp:Content ID="MyLicense" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-xs-6">
                <asp:GridView ID="grvMyLicenseCode" CssClass="gridview table table-hover table-striped" runat="server" DataSourceID="sqlLicenseCode" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                        <asp:BoundField DataField="licenseCode" HeaderText="License Code" SortExpression="licenseCode" />
                        <asp:BoundField DataField="expireDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Expire Date" SortExpression="expireDate" />
                        <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="Info" />
                    </Columns>
                </asp:GridView>
            </div>

            <asp:SqlDataSource ID="sqlLicenseCode" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT license.licenseName, license.licenseCode, license.expireDate, license.extraInfo FROM license INNER JOIN licensehandler ON license.licenseID = licensehandler.licenseID WHERE (license.licenseCode &lt;&gt; '' OR license.licenseCode &lt;&gt; NULL) AND (licensehandler.eventID = @eventID)
">
                <SelectParameters>
                    <asp:SessionParameter Name="@eventID" SessionField="eventID" />
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="col-xs-6">

                <asp:GridView ID="grvMyLicenseFile" CssClass="gridview table table-hover table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="sqlLicenseFile">
                    <Columns>
                        <asp:BoundField DataField="licenseName" HeaderText="Name" SortExpression="licenseName" />
                        <asp:BoundField DataField="expireDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Expire Date" SortExpression="expireDate" />
                        <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="Info" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("licenseFileLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("licenseFileLocation")).Length < 1 ? "" : Convert.ToString(Eval("licenseFileLocation")) %>'>License File</asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:SqlDataSource ID="sqlLicenseFile" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT license.licenseName, license.licenseFileLocation, license.expireDate, license.extraInfo FROM license INNER JOIN licensehandler ON license.licenseID = licensehandler.licenseID WHERE (license.licenseFileLocation &lt;&gt; '') OR (license.licenseFileLocation &lt;&gt; NULL) AND eventID = @eventID">
            <SelectParameters>
                <asp:SessionParameter Name="@eventID" SessionField="eventID" />
            </SelectParameters>
        </asp:SqlDataSource>
</div>
</asp:Content>
