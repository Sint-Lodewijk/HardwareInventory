<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" Inherits="Toestellenbeheer.hardware_overview" %>
<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:GridView ID="HardwareOverview" runat="server" AllowSorting="True" CellPadding="4" DataSourceID="HardwareOverviewGridView" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="purchaseDate" HeaderText="purchaseDate" SortExpression="purchaseDate" />
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="warranty" HeaderText="warranty" SortExpression="warranty" />
            <asp:BoundField DataField="extraInfo" HeaderText="extraInfo" SortExpression="extraInfo" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="addedDate" HeaderText="addedDate" SortExpression="addedDate" />
            <asp:BoundField DataField="pictureLocation" HeaderText="pictureLocation" SortExpression="pictureLocation" />
            <asp:BoundField DataField="typeNr" HeaderText="typeNr" SortExpression="typeNr" />
        </Columns>
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
    <asp:SqlDataSource ID="HardwareOverviewGridView" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT * FROM hardware;"></asp:SqlDataSource>
    
</asp:Content>
