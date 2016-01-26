<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="people-history.aspx.cs" Inherits="Toestellenbeheer.Archive.people_history" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvPeopleAD" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvPeopleAD_OnRowDataBound" OnSelectedIndexChanged="grvPeopleAD_SelectedIndexChanged">
        <SelectedRowStyle BackColor="Azure" />
    </asp:GridView>
    <asp:GridView ID="grvHardwareOfPeople" runat="server" CssClass="table table-hover table-striped gridview"></asp:GridView>
</asp:Content>
