<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="modify-hardware.aspx.cs" Inherits="Toestellenbeheer.Overview.modify_hardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DetailsView runat="server" CssClass="gridview form-control table table-hover table-striped" AutoGenerateRows="False" DataSourceID="sqlDetail" DataKeyNames="serialNr,internalNr">
        <Fields>
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal nr" SortExpression="internalNr" />
            <asp:BoundField DataField="warranty" HeaderText="Warranty" SortExpression="warranty" />
            <asp:BoundField DataField="extraInfo" HeaderText="Extra info" SortExpression="extraInfo" />
            <asp:BoundField DataField="modelNr" HeaderText="Model nr" SortExpression="modelNr" />
            <asp:BoundField DataField="purchaseDate" HeaderText="Purchase Date" SortExpression="purchaseDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="type" HeaderText="Type" SortExpression="Type" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="attachmentLocation" HeaderText="Attachment" SortExpression="attachmentLocation" InsertVisible="False" />
            <asp:BoundField DataField="pictureLocation" HeaderText="Picture file" SortExpression="pictureLocation" InsertVisible="False" />

            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Fields>

    </asp:DetailsView>
    <asp:SqlDataSource ID="sqlDetail" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT purchaseDate, serialNr, internalNr, warranty, extraInfo, manufacturerName, pictureLocation, type, attachmentLocation,  modelNr FROM hardware JOIN type ON type.typeNr = hardware.typeNr WHERE (internalNr = @internalNr)" UpdateCommand="UPDATE hardware SET purchaseDate = @purchaseDate, serialNr = @serialNr, internalNr = @internalNr, warranty = @warranty, extraInfo = @extraInfo, manufacturerName = @manufacturerName, modelNr = modelNr">
        <SelectParameters>
            <asp:SessionParameter Name="@internalNr" SessionField="internalNr" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
