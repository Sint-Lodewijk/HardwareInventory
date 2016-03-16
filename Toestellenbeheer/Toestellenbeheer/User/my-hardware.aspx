<%@ Page Title="My hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="my-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.my_hardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvMyHardware" runat="server" CssClass="gridview table table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr">
        <Columns>
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="serialNr" HeaderText="serialNr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="internalNr" SortExpression="internalNr" ReadOnly="True" />
            <asp:BoundField DataField="manufacturerName" HeaderText="manufacturerName" SortExpression="manufacturerName" />

            <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
        </Columns>
    </asp:GridView>


    <asp:Label ID="lblStatus" runat="server" CssClass="control-label col-sm-10"></asp:Label>

    <asp:Label ID="lblError" runat="server" CssClass="control-label col-sm-12"></asp:Label>
</asp:Content>
