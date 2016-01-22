<%@ Page Title="Manage hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_hardware" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="grvHardwarePoolUnassigned" runat="server"  CssClass="table table-hover table-striped gridview" >
    </asp:GridView>
    <asp:GridView ID="grvPeopleAD" runat="server"  CssClass="table table-hover table-striped gridview" >
    </asp:GridView>
</asp:Content>
