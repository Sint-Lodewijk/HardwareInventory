<%@ Page Title="Manage type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-type.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_type" %>

<asp:Content ID="ManageType" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="typeSelect" runat="server" DataSourceID="type" CssClass="table table-hover table-striped gridview">
    </asp:GridView>
    <asp:SqlDataSource ID="type" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT typeNr 'Type Nr', type 'Type name' FROM type"></asp:SqlDataSource>
    <div class="form-group">
        <asp:Label ID="lblTypeNR" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="typeNr">Type nr</asp:Label>
        <div class="col-sm-4">
            <asp:TextBox ID="typeNr" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Label ID="lblTypeName" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="typeName">Type name</asp:Label>
        <div class="col-sm-4">

            <asp:TextBox ID="typeName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group col-sm-offset-5 col-sm-7 margin-top-15">
        <asp:Button ID="btnAddType" runat="server" Text="Add a type" CssClass="btn btn-primary col-sm-2" OnClick="btnAddType_Click" />
    </div>
</asp:Content>
