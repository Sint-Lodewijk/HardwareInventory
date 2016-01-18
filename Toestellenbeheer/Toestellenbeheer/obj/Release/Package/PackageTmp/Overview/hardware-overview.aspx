<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" Inherits="Toestellenbeheer.hardware_overview" %>

<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
    <div id="search" class="form-group">

        <asp:Label ID="lblSearch" runat="server" CssClass="control-label col-sm-1" AssociatedControlID="txtSearch">Search</asp:Label>
        <div class="col-sm-5">
            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-1">

            <asp:Label ID="lblOn" runat="server" AssociatedControlID="drpSearchItem" CssClass="control-label">on</asp:Label>
        </div>
        <div class="col-sm-3">

            <asp:DropDownList ID="drpSearchItem" CssClass="form-control" runat="server">
                <asp:ListItem Value="internalNr">Internal Nr</asp:ListItem>
                <asp:ListItem Value="manufacturerName">Manufacturer</asp:ListItem>
                <asp:ListItem Value="typeNr">Type Nr</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-2">

            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default margin-top-5" OnClick="Search" />
        </div>
    </div>
  

    <asp:GridView ID="HardwareOverviewGridSearch" CssClass="table table-hover table-striped gridview"   runat="server">

        
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:GridView ID="HardwareOverviewGrid" CssClass="table table-hover table-striped gridview" runat="server" AllowSorting="True"  AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr" AllowPaging="True" DataSourceID="SqlDataSource1">

        <Columns>
            <asp:BoundField DataField="purchaseDate" HeaderText="Purchase date" SortExpression="purchaseDate" />
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="warranty" HeaderText="Warranty" SortExpression="warranty" />
            <asp:BoundField DataField="extraInfo" HeaderText="Extra info" SortExpression="extraInfo" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="addedDate" HeaderText="Added date" SortExpression="addedDate" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, warranty, extraInfo, DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate' FROM hardware"></asp:SqlDataSource>
    <asp:Label ID="lblTotalQuery" runat="server" Text=""></asp:Label>
    <!-- <asp:SqlDataSource ID="HardwareOverviewGridView" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT * FROM hardware;"></asp:SqlDataSource>
    -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=HardwareOverviewGrid]').footable();
        });
    </script>

</asp:Content>

