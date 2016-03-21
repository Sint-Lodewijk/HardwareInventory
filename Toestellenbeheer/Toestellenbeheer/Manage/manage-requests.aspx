<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="manage-requests.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvRequests" CssClass="gridview table table-hover table-striped" OnSelectedIndexChanged="grvRequests_SelectedIndexChanged" OnRowDataBound="grvRequests_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="requestID,nameAD,internalNr,serialNr" DataSourceID="sqlRequest">
        <Columns>
            <asp:BoundField DataField="requestID" HeaderText="requestID" InsertVisible="False" ReadOnly="True" SortExpression="requestID" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" />
            <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
            <asp:BoundField DataField="nameAD" HeaderText="nameAD" SortExpression="nameAD" />
            <asp:BoundField DataField="requestDate" HeaderText="requestDate" SortExpression="requestDate" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnAcceptRequest" Text="Accept" OnClick="btnAcceptRequest_Click" Visible="false" runat="server" CssClass="btn btn-primary" />
    <asp:Button ID="btnDenyRequest" Text="Deny" OnClick="btnDenyRequest_Click" Visible="false" runat="server" CssClass="btn btn-primary" />
     <asp:Label ID="lblExeption" runat="server"></asp:Label>
    <asp:SqlDataSource ID="sqlRequest" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT request.requestID, request.requestAccepted, request.requestDate, people.nameAD, type.type, request.serialNr, request.internalNr FROM request INNER JOIN people ON request.eventID = people.eventID INNER JOIN hardware ON request.serialNr = hardware.serialNr AND request.internalNr = hardware.internalNr INNER JOIN type ON type.typeNr = hardware.typeNr WHERE (request.requestAccepted = 0)"></asp:SqlDataSource>
</asp:Content>
