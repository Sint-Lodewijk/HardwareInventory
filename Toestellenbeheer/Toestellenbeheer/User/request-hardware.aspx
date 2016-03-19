<%@ Page Title="Request a hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="request-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.request_hardware" %>

<asp:Content ID="cntRequest" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
        <asp:Label runat="server" Text="Select a type" CssClass="control-label col-sm-2"></asp:Label>
        <div class="col-sm-10">
            <asp:DropDownList ID="drpTypeList" OnSelectedIndexChanged="typeList_SelectedIndexChanged" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="type" DataValueField="type" />
        </div>
        <asp:GridView runat="server" ID="grvAvailibleHardwareType" AutoGenerateColumns="false" DataKeyNames="serialNr, internalNr" OnRowDataBound="grvAvailibleHardwareType_RowDataBound" OnSelectedIndexChanged="grvAvailibleHardwareType_SelectedIndexChanged" CssClass="gridview table table-striped table-hover">
            <Columns>
                <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
                <asp:BoundField DataField="serialNr" HeaderText="Serial Nr" ReadOnly="True" SortExpression="serialNr" />
                <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
                <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                <asp:BoundField DataField="modelNr" HeaderText="Model Nr" SortExpression="modelNr" />

            </Columns>
            <SelectedRowStyle BackColor="#cc6600" />
        </asp:GridView>
        <div class="col-sm-12">
            <asp:Label ID="lblProblem" CssClass="col-sm-12 control-label" runat="server"></asp:Label>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="btnRequest" CssClass="btn btn-primary" runat="server" Text="Send a request" Visible="false" OnClick="btnRequest_Click" />
    </div>


</asp:Content>
