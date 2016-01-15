<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-license.aspx.cs" Inherits="Toestellenbeheer.Manage.add_license"  %>
<asp:Content ID="LicenseAdd" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="licenseName" runat="server" Text="License name: "></asp:Label>
    <asp:TextBox ID="txtLicnseName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="licenseCode" runat="server" Text="License code: "></asp:Label>
    <asp:TextBox ID="txtLicenseCode" runat="server" Width="324px"></asp:TextBox> <br />
    <asp:Label ID="addLicenseHardware" runat="server" Text="Hardware"></asp:Label>
    <asp:SqlDataSource ID="HardwareLicense" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT typeNr, manufacturerName, internalNr, serialNr  FROM hardware;" OnSelecting="HardwareLicense_Selecting"></asp:SqlDataSource>
    <asp:GridView ID="grvHardwareLicenseSelect" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr,serialNr"  OnSelectedIndexChanged = "hardwareLicenseSelection_Click">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="typeNr" HeaderText="typeNr" SortExpression="typeNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" ReadOnly="True" />
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="assignToSelectedHardware" runat="server" Text="Assign" OnClick="assignToSelectedHardware_Click" />

    <asp:Label ID="testLabel" runat="server" Text=""></asp:Label>

</asp:Content>
