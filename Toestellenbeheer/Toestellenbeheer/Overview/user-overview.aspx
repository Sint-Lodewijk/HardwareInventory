﻿<%@ Page Title="user-overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-overview.aspx.cs" Inherits="Toestellenbeheer.Overview.user_overview" %>
<asp:Content ID="userOverview" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gv" OnPreRender="grvPreRender" DataKeyNames="Domain Name" runat="server" AllowPaging="true" OnPageIndexChanging="gridView_PageIndexChanging" CssClass="table table-hover table-striped gridview" ></asp:GridView>
  <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_gv").tablesorter();
        });
    </script>
</asp:Content>
