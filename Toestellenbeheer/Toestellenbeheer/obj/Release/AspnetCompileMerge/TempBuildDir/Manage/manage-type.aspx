﻿<%@ Page Title="Manage type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-type.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_type" %>

<asp:Content ID="ManageType" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <!--
        <asp:Label ID="lblTypeNR" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="typeNr">Type nr</asp:Label>
        <div class="col-sm-4">
            <asp:TextBox ID="typeNr" runat="server" CssClass="form-control"></asp:TextBox>
        </div>-->
        <asp:Label ID="lblTypeName" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="typeName">Type name</asp:Label>
        <div class="col-sm-7">

            <asp:TextBox ID="typeName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
        <asp:Button ID="btnAddType" runat="server" Text="Add a type" CssClass="btn btn-primary margin-top-5" OnClick="btnAddType_Click" />
    </div>
        <div class="form-group">
            <asp:Label runat="server" ID="lblProblem" Text=""></asp:Label>
        </div>
    </div>
    <asp:GridView ID="typeSelect" runat="server"  CssClass="table table-hover table-striped gridview">
    </asp:GridView>
    
    
</asp:Content>
