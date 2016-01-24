<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="return-hardware.aspx.cs" EnableEventValidation="false" Inherits="Toestellenbeheer.Manage.return_hardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvHardwarePoolAssigned" AutoGenerateColumns="false" OnRowDataBound="grvHardwarePoolAssigned_OnRowDataBound" runat="server" CssClass="table table-hover table-striped gridview" DataKeyNames="internalNr">
        <Columns>
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="nameAD" HeaderText="User name" />
            <asp:BoundField DataField="typeNr" HeaderText="Type Nr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" />

        </Columns>
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />

    </asp:GridView>
    <asp:Button ID="btnReturnHardware" runat="server" Text="Return the selected hardware" CssClass="btn btn-primary" OnClick="btnReturnHardware_Click" />
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
</asp:Content>
