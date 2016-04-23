<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="manage-manufacturer.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_manufacturer" %>
<asp:Content ID="ManageManufacturer" ContentPlaceHolderID="MainContent" runat="server">
       
        <div class="form-group">

            <asp:Label ID="lblManufacturerName" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="txtManufacturerName">Manufacturer</asp:Label>
            <div class="col-sm-7">

                <asp:TextBox ID="txtManufacturerName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnAddManufacturer" runat="server" Text="Add a manufacturer" CssClass="btn btn-primary margin-top-5" OnClick="btnAddManufacturer_Click" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lblProblem" Text=""></asp:Label>
            </div>
        </div>
        <asp:GridView ID="grvManufacturer" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="grvManufacturer_SelectedIndexChanged" runat="server"  CssClass="table table-hover table-striped gridview" AllowPaging="True"  AutoGenerateColumns="False" DataKeyNames="manufacturerName">
            <Columns>
                <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer" ReadOnly="False" SortExpression="manufacturerName" />
            </Columns>
            <SelectedRowStyle CssClass="success" />
        </asp:GridView>
        <asp:Panel ID="ButtonPanel" runat="server" Visible="false">
            <asp:Button ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" runat="server" Text="Modify" />
            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-primary" runat="server" Text="Delete" />
        </asp:Panel>
        <asp:Button ID="target" runat="server" CssClass="hide" />
    
    <div class="modal fade" id="modalManufacturer" tabindex="-1" role="dialog" aria-labelledby="manufacturerModalTitle">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" runat="server" id="manufacturerModalTitle">Modify manufacturer</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpDetails">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:TextBox ID="txtManufacturerModifying" runat="server" CssClass="form-control col-sm-12"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>                            
                    <br />

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button1" OnClick="btnUpdate_Click" CssClass="btn btn-primary" runat="server" Text="Update" />
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
