<%@ Page Title="Manage database" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage-database.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DatabaseContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpSuccess" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="alert fade in" id="successMessageAlert">
                
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            <asp:Label runat="server" ID="lblAlert"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#backup" aria-controls="backup" role="tab" data-toggle="tab">Backup database</a></li>
        <li role="presentation"><a href="#restore" aria-controls="restore" role="tab" data-toggle="tab">Restore database</a></li>
        <li role="presentation"><a href="#destroy" aria-controls="destroy" role="tab" data-toggle="tab">Destroy database</a></li>

    </ul>
         
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="backup">
            <h3>Backup</h3>
            <p>Back up the database regularly on preventing data lose.</p>
            <asp:Button runat="server" ID="btnBackup" OnClick="btnBackup_Click" Text="Backup" CssClass="btn btn-primary" />
      

        </div>
        <div role="tabpanel" class="tab-pane" id="restore">
            <h3>Restore</h3>
            <p>Restore the database from the sql file.</p>

            <div class="input-group">
                <asp:FileUpload ID="fileRestore" CssClass="form-control" runat="server" />
                <span class="input-group-btn">
                    <asp:Button runat="server" ID="btnRestore" OnClick="btnRestore_Click" Text="Restore" CssClass="btn btn-primary" />
                </span>
            </div>
            <!-- /input-group -->

        </div>
        <div role="tabpanel" class="tab-pane" id="destroy">
            <h3>Truncate database</h3>
            <p>Truncate all content in the database!</p>
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#confirmTruncateModal">
                Truncate database
            </button>
        </div>

    </div>


    <!-- Modal -->
    <div class="modal fade" id="confirmTruncateModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="confirmTruncateTitle">Confirm truncate</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <p>Are you sure to truncate the database? This action is not reversible, but you can still restore the previous database with the restore function.</p>
                        <p>Continue to proceed?</p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" OnClick="btnTruncate_Click" CssClass="btn btn-danger" Text="Truncate all tables" ID="btnTruncate" />
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
