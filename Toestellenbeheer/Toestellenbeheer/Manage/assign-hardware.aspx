<%@ Page Title="Assign hardware to people" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="assign-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_hardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="grvHardwarePoolUnassigned" DataKeyNames="serialNr,internalNr,manufacturerName,typeNr" OnRowDataBound="grvHardwarePoolUnassigned_OnRowDataBound" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped gridview">
        <Columns>
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />


        </Columns>
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:GridView ID="grvPeopleAD" runat="server" allowPaging="true" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="grvPeopleAD_OnRowDataBound" CssClass="table table-hover table-striped gridview">
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />

    </asp:GridView>
    <asp:Button ID="btnAssignHardwarePeople" runat="server" Text="Assign the selected hardware with selected person" CssClass="btn btn-primary margin-top-5 col-centered" OnClick="assignHardwarePeople_Click"/>
    <asp:Label ID="lblResult" runat="server"></asp:Label>
</asp:Content>
