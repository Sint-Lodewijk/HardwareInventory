<%@ Page Title="Hardware overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" Inherits="Toestellenbeheer.hardware_overview" %>

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


    <asp:GridView ID="HardwareOverviewGridSearch" CssClass="table table-hover table-striped gridview" DataKeyNames="pictureLocation,attachmentLocation" runat="server">
        <Columns>

            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>


            <asp:TemplateField HeaderText="Attachment">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>

        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:GridView ID="HardwareOverviewGrid" CssClass="table table-hover table-striped gridview" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr" AllowPaging="True" DataSourceID="HardwareDS">

        <Columns>

            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />

            <asp:TemplateField HeaderText="Attachment">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton Text="Details" ID="lnkShowMoreInfo" runat="server" OnClick="lnkShowMoreInfo_Click" />

                </ItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowSelectButton="True" />

        </Columns>


        <EditRowStyle BackColor="#999999" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:GridView ID="selectedRow" CssClass="table table-hover table-striped gridview" runat="server">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblPDate" runat="server" Text='<%#Eval("Purchase date")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <asp:Label ID="lblManu" runat="server" Text='<%#Eval("Manufacturer")%>'>
                            </asp:Label><td></td>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSeri" runat="server" Text='<%#Eval("Serial Nr")%>'>
                                    </asp:Label></td>
                            </tr>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type nr") %>'>

                                </asp:Label></td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblInternal" runat="server" Text='<%#Eval("Internal Nr") %>'>

                                </asp:Label></td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblWarr" runat="server" Text='<%#Eval("Warranty") %>'>

                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblExtra" runat="server" Text='<%#Eval("Extra info") %>'>

                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAdded" runat="server" Text='<%#Eval("Added date") %>'>

                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAtt" runat="server" Text='<%#Eval("attachmentLocation") %>'>

                                </asp:Label></td>
                        </tr>
                        <table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <!--
    <link rel="stylesheet" href="../../Scripts/jquery-ui.css">
    <script src="../../Scripts/jquery-2.2.0.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).on("click", "[id*=lnkShowMoreInfo]", function () {
            $("#serialNr").html($(".serialNr", $(this).closest("tr")).html());
            $("#internalNr").html($(".internalNr", $(this).closest("tr")).html());
            $("#manufacturerName").html($(".manufacturerName", $(this).closest("tr")).html());
            $("#dialog").dialog({
                title: "View Details",
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
            return false;
        });
    </script>
    <div id="dialog" style="display: none">
        <b>serialNr:</b> <span id="serialNr"></span>
        <br />
        <b>internalNr:</b> <span id="internalNr"></span>
        <br />
        <b>manufacturerName:</b> <span id="manufacturerName"></span>
    </div>
    -->
    <asp:SqlDataSource ID="HardwareDS" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, warranty, extraInfo, DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation FROM hardware"></asp:SqlDataSource>
    <asp:Label ID="lblTotalQuery" runat="server" Text=""></asp:Label>
    <!-- <asp:SqlDataSource ID="HardwareOverviewGridView" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT * FROM hardware;"></asp:SqlDataSource>
    -->

</asp:Content>

