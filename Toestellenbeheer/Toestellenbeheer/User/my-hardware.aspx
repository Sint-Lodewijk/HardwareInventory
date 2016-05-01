<%@ Page Title="My hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="my-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.my_hardware" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvMyHardware" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="grvMyHardware_SelectedIndexChanged" runat="server" CssClass="gridview table table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" ReadOnly="True" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
        </Columns>
    </asp:GridView>
    <!-- Modal -->
    <div class="modal fade" id="hardwareImageModal" tabindex="-1" role="dialog" aria-labelledby="hardwareImage">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="hardwareImage">Preview Image</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Image AlternateText="Hardware Image" CssClass="img-responsive center-block" ID="picDetail" runat="server"></asp:Image>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:Label ID="lblStatus" runat="server" CssClass="control-label col-sm-10"></asp:Label>
    <asp:Label ID="lblError" runat="server" CssClass="control-label col-sm-12"></asp:Label>
</asp:Content>
