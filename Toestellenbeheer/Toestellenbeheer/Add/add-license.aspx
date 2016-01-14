<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-license.aspx.cs" Inherits="Toestellenbeheer.Manage.add_license"  %>
<asp:Content ID="LicenseAdd" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="licenseCode" runat="server" Text="License code: "></asp:Label>
    <asp:TextBox ID="txtLicenseCode" runat="server" Width="324px"></asp:TextBox> <br />
    <asp:Label ID="addLicenseHardware" runat="server" Text="Hardware"></asp:Label>
    <asp:SqlDataSource ID="HardwareLicense" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT pictureLocation, typeNr, manufacturerName, internalNr FROM hardware;"></asp:SqlDataSource>
    <asp:GridView ID="grvHardwareLicenseSelect" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr" DataSourceID="HardwareLicense" OnSelectedIndexChanged = "hardwareLicenseSelection_Click">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="pictureLocation" HeaderText="pictureLocation" SortExpression="pictureLocation" />
            <asp:BoundField DataField="typeNr" HeaderText="typeNr" SortExpression="typeNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" ReadOnly="True" SortExpression="internalNr" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="assaingToSelectedHardware" runat="server" Text="Assaign" OnClick="assaingToSelectedHardware_Click" />
</asp:Content>
