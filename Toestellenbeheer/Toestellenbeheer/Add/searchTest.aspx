<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="searchTest.aspx.cs" Inherits="Toestellenbeheer.Add.searchTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lbAlert" runat="server" ForeColor="Red"></asp:Label>
    <asp:TextBox ID="tbKeyWords" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Click to Search" onclick="btnSearch_Click" />

    <br /><br />

    <asp:Button ID="btnListAll" runat="server" Text="Click to show all data" onclick="btnListAll_Click" />

    <!-- Search Result -->
    <asp:Repeater ID="RepeaterSearchResult" runat="server">
        <HeaderTemplate>
            <h3>Search Result:</h3>
            <ol id="result">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <h4><a href="<%# ResolveClientUrl("~/Show.aspx?id=" + Eval("ID").ToString()) %>" class="title"><%# Server.HtmlEncode(Eval("Title").ToString())%></a></h4>
                <p class="brief"><%# Server.HtmlEncode(Eval("Content").ToString())%></p>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ol>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
