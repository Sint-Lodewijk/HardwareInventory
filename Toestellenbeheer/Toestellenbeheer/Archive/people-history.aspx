<%@ Page Title="People history" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="people-history.aspx.cs" Inherits="Toestellenbeheer.Archive.people_history" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvPeopleAD" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvPeopleAD_OnRowDataBound" OnSelectedIndexChanged="grvPeopleAD_SelectedIndexChanged">
        <SelectedRowStyle BackColor="Azure" />
    </asp:GridView>
    <asp:GridView ID="grvHardwareOfPeople" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="Serial Nr"/>
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" />
            <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" />
            <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" NullDisplayText="Not returned yet."/>

        </Columns>
    </asp:GridView>
</asp:Content>
