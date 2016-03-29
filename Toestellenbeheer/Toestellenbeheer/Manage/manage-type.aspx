<%@ Page Title="Manage type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-type.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_type" %>

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
    <asp:GridView ID="typeSelect" OnRowEditing="typeSelect_RowEditing" runat="server" Width="80%"  CssClass="table table-hover table-striped gridview" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="type" DataSourceID="sqlType">
        <Columns>
            <asp:BoundField DataField="type" HeaderText="Type" ReadOnly="False" SortExpression="type" />
            <asp:CommandField EditText="Modify" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    
    
    <asp:SqlDataSource ID="sqlType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type FROM type" DeleteCommand="DELETE FROM type WHERE (type = @type)" UpdateCommand="UPDATE type SET type = @Type where type = '@typeSelected'">
        <UpdateParameters>
            <asp:SessionParameter Name="@typeSelected" SessionField="type" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    
</asp:Content>
