<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-history.aspx.cs" Inherits="Toestellenbeheer.Archive.hardware_history" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvHardware" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvHardware_OnRowDataBound" OnSelectedIndexChanged="grvHardware_SelectedIndexChanged" DataKeyNames="internalNr"></asp:GridView>
    <asp:GridView ID="grvPeopleLinked" runat="server"></asp:GridView>
</asp:Content>
