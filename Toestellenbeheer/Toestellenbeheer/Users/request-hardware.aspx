<%@ Page Title="Request a hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="request-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.request_hardware" %>

<asp:Content ID="cntRequest" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
        <asp:Label runat="server" Text="Select a type" CssClass="control-label col-sm-2"></asp:Label>
        <div class="col-sm-10">
            <asp:DropDownList ID="drpTypeList" OnSelectedIndexChanged="typeList_SelectedIndexChanged" CssClass="form-control" runat="server" AutoPostBack="True" DataSourceID="Type" DataTextField="type" DataValueField="type" />
            <asp:SqlDataSource ID="Type" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type FROM type"></asp:SqlDataSource>
        </div>
        <asp:GridView runat="server" ID="grvAvailibleHardwareType" OnRowDataBound="grvAvailibleHardwareType_RowDataBound" OnSelectedIndexChanged="grvAvailibleHardwareType_SelectedIndexChanged" CssClass="gridview table table-striped table-hover">
            <Columns>
                
            </Columns>
            <SelectedRowStyle BackColor="#cc6600" />
        </asp:GridView>
        <div class="col-sm-12">
            <asp:Label ID="lblProblem" CssClass="col-sm-12 control-label" runat="server"></asp:Label>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="btnRequest" CssClass="btn btn-primary" runat="server" Text="Send a request" Visible="false"/>
    </div>
        

</asp:Content>
