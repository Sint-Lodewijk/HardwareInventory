<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="manage-requests.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_requests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvRequests" CssClass="gridview table table-hover table-striped" OnSelectedIndexChanged="grvRequests_SelectedIndexChanged" OnRowDataBound="grvRequests_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="requestID" DataSourceID="sqlRequest">
        <Columns>
            <asp:BoundField DataField="requestID" HeaderText="requestID" InsertVisible="False" ReadOnly="True" SortExpression="requestID" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" />
            <asp:CheckBoxField DataField="requestAccepted" HeaderText="requestAccepted" SortExpression="requestAccepted" />
            <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
            <asp:BoundField DataField="nameAD" HeaderText="nameAD" SortExpression="nameAD" />
            <asp:BoundField DataField="requestDate" HeaderText="requestDate" SortExpression="requestDate" />
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="sqlRequest" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT request.requestID, request.internalNr, request.requestAccepted, type.type, people.nameAD, request.requestDate FROM people INNER JOIN request ON people.eventID = request.eventID INNER JOIN hardware ON people.eventID = hardware.eventID AND request.serialNr = hardware.serialNr AND request.internalNr = hardware.internalNr INNER JOIN type ON hardware.typeNr = type.typeNr"></asp:SqlDataSource>
</asp:Content>
