<%@ Page Title="Manage database" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-database.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DatabaseContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#backup" aria-controls="backup" role="tab" data-toggle="tab">Backup database</a></li>
            <li role="presentation"><a href="#restore" aria-controls="restore" role="tab" data-toggle="tab">Restore database</a></li>
            <li role="presentation"><a href="#destroy" aria-controls="destroy" role="tab" data-toggle="tab">Destroy database</a></li>

        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="backup">
                <asp:Button runat="server" ID="btnBackup" OnClick="btnBackup_Click" Text="Backup" CssClass="btn btn-primary" />

            </div>
            <div role="tabpanel" class="tab-pane" id="restore">
                <div class="input-group">
                    <asp:FileUpload ID="fileRestore" CssClass="form-control" runat="server" />
                    <span class="input-group-btn">
                        <asp:Button runat="server" ID="btnRestore" OnClick="btnRestore_Click" Text="Restore" CssClass="btn btn-primary" />
                    </span>
                </div>
                <!-- /input-group -->

            </div>
            <div role="tabpanel" class="tab-pane" id="destroy">
                <asp:Button runat="server" OnClick="btnTruncate_Click" CssClass="btn btn-danger" Text="Truncate all tables" ID="btnTruncate" />
            </div>
        </div>

    </div>
</asp:Content>
