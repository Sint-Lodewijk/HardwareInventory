<%@ Page Title="People history" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="people-history.aspx.cs" Inherits="Toestellenbeheer.Archive.people_history" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvPeopleAD" runat="server" OnPageIndexChanging="gridView_PageIndexChanging" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvPeopleAD_OnRowDataBound" OnSelectedIndexChanged="grvPeopleAD_SelectedIndexChanged">
        <SelectedRowStyle BackColor="Azure" />
    </asp:GridView>
    <asp:Panel ID="modalHardware" runat="server" CssClass="modal fade" TabIndex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="modalTitle" runat="server">Loading...</h4>
                        </div>
                        <div class="modal-body modal-margin">
                            <asp:Label ID="lblResult" CssClass="col-sm-12" runat="server"></asp:Label>
                            <asp:GridView ID="grvHardwareOfPeople" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview">
                                <Columns>
                                    <asp:BoundField DataField="serialNr" HeaderText="Serial Nr" />
                                    <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" />
                                    <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" />
                                    <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" NullDisplayText="Not returned yet." />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
