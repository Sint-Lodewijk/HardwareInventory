<%@ Page Title="Hardware history" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-history.aspx.cs" Inherits="Toestellenbeheer.Archive.hardware_history" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
            <asp:GridView ID="grvHardware" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvHardware_OnRowDataBound" OnSelectedIndexChanged="grvHardware_SelectedIndexChanged" DataKeyNames="internalNr">
                <Columns>
                    <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
                    <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
                    <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                    <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />


                </Columns>
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

                        <div class="modal-body">

                            <asp:GridView ID="grvPeopleLinked" CssClass="table table-hover table-striped gridview" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="nameAD" HeaderText="Domain Name" />
                                    <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" />
                                    <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" NullDisplayText="Not returned yet" />

                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
