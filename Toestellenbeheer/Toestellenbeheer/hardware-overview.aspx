<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" Inherits="Toestellenbeheer.hardware_overview" %>
<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label>
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    &nbsp;<asp:Label ID="lblOn" runat="server" Text=" on "></asp:Label>
    <asp:DropDownList ID="drpSearchItem" runat="server">
        <asp:ListItem Value="internalNr">Internal Nr</asp:ListItem>
        <asp:ListItem Value="manufacturerName">Manufacturer</asp:ListItem>
        <asp:ListItem Value="typeNr">Type Nr</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Search" />
    <asp:GridView ID="HardwareOverviewGrid" runat="server" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="True"     DataKeyNames="serialNr">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
   <!-- <asp:SqlDataSource ID="HardwareOverviewGridView" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT * FROM hardware;"></asp:SqlDataSource>
    -->
</asp:Content>

