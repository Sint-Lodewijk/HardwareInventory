<%@ Page Title="Manage type" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="manage-type.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_type" %>
<asp:Content ID="ManageType" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="input-group">
            <asp:TextBox ID="typeName" placeholder="type" runat="server" CssClass="form-control"></asp:TextBox>
            <div class="input-group-btn">
                <asp:Button ID="btnAddType" runat="server" Text="Add a type" CssClass="btn btn-primary" OnClick="btnAddType_Click" />
            </div>
            <asp:Label runat="server" ID="lblProblem" Text=""></asp:Label>
        </div>
    </div>
    <asp:GridView ID="typeSelect" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="typeSelect_SelectedIndexChanged" runat="server" CssClass="table table-hover table-striped gridview text-center" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="type">
        <Columns>
            <asp:BoundField DataField="type" HeaderText="Type" ReadOnly="False" SortExpression="type" />
        </Columns>
        <SelectedRowStyle CssClass="success" />
    </asp:GridView>
    <asp:Panel ID="ButtonPanel" runat="server" Visible="false">
        <asp:Button ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" runat="server" Text="Modify" />
        <asp:Button ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-primary" runat="server" Text="Delete" />
    </asp:Panel>
    <div class="modal fade" id="modalType" tabindex="-1" role="dialog" aria-labelledby="typeModalTitle">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" runat="server" id="typeModalTitle">Modify type</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpDetails">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:TextBox ID="txtType" runat="server" CssClass="form-control col-sm-12"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="btn btn-primary" runat="server" Text="Update" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
