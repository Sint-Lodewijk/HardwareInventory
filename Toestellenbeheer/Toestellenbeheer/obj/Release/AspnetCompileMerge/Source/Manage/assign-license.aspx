<%@ Page Title="Assign license" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assign-license.aspx.cs" Inherits="Toestellenbeheer.Manage.assign_license" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvLicense" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="grvLicense_SelectedIndexChanged" OnRowDataBound="OnRowDataBound" OnRowDeleting="grvLicense_RowDeleting" DataKeyNames="License Code">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>

                    <asp:LinkButton ID="delete" runat="server"
                        OnClientClick="if (!confirm('Are you sure you want delete?')) return false;"
                        CommandName="Delete">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
</asp:Content>
