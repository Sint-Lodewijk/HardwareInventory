<%@ Page Title="My hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="my-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.my_hardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvMyHardware" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr,eventID" >
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" ReadOnly="True" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="pictureLocation" HeaderText="pictureLocation" SortExpression="pictureLocation" />

            <asp:BoundField DataField="typeNr" HeaderText="typeNr" SortExpression="typeNr" />
            <asp:BoundField DataField="returnedDate" HeaderText="returnedDate" SortExpression="returnedDate" />
            <asp:BoundField DataField="eventID" HeaderText="eventID" InsertVisible="False" ReadOnly="True" SortExpression="eventID" />
            <asp:BoundField DataField="nameAD" HeaderText="nameAD" SortExpression="nameAD" />

        </Columns>
    </asp:GridView>
    <asp:GridView ID="grvMyHardware2" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr,eventID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" ReadOnly="True" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />
            <asp:BoundField DataField="pictureLocation" HeaderText="pictureLocation" SortExpression="pictureLocation" />

            <asp:BoundField DataField="typeNr" HeaderText="typeNr" SortExpression="typeNr" />
            <asp:BoundField DataField="returnedDate" HeaderText="returnedDate" SortExpression="returnedDate" />
            <asp:BoundField DataField="eventID" HeaderText="eventID" InsertVisible="False" ReadOnly="True" SortExpression="eventID" />
            <asp:BoundField DataField="nameAD" HeaderText="nameAD" SortExpression="nameAD" />

        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT hardware.serialNr, hardware.internalNr, hardware.manufacturerName, hardware.pictureLocation, hardware.typeNr, archive.returnedDate, people.eventID, people.nameAD FROM hardware INNER JOIN archive ON hardware.serialNr = archive.serialNr AND hardware.internalNr = archive.internalNr INNER JOIN people ON hardware.eventID = people.eventID AND archive.eventID = people.eventID WHERE (people.nameAD LIKE 'testUser')"></asp:SqlDataSource>
    <asp:Label ID="lblStatus" runat="server" CssClass="control-label col-sm-12"></asp:Label>

    <asp:Label ID="lblError" runat="server" CssClass="control-label col-sm-12"></asp:Label>
</asp:Content>
