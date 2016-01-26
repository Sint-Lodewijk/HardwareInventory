<%@ Page Title="Hardware overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-overview.aspx.cs" EnableEventValidation="false" Inherits="Toestellenbeheer.hardware_overview" %>

<asp:Content ID="hardwareOverview" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="searchPanel" runat="server">

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
    </asp:Panel>

    <asp:GridView ID="HardwareOverviewGridSearch" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" DataKeyNames="pictureLocation,attachmentLocation" runat="server">
        <Columns>

            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />

            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkShowMoreInfo" runat="server" CommandName="Delete" Text="Details"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>

        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    <asp:GridView ID="HardwareOverviewGrid" OnRowDeleting="details" OnRowDataBound="OnRowDataBound" CssClass="table table-hover table-striped gridview" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr" AllowPaging="True">
        <Columns>
            <%-- 
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
            </asp:ImageField> --%>

            <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
            <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
            <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
            <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />

            <%--             <asp:TemplateField HeaderText="Attachment">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" CommandName="Select" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            --%>


            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkShowMoreInfo" runat="server" CommandName="Delete" Text="Details"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>


        <EditRowStyle BackColor="#999999" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:GridView ID="selectedRow" AutoGenerateColumns="false" CssClass="table table-hover table-striped gridview" runat="server">
        <Columns>
            <%--<asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="Purchase date" HeaderText="Purchase date" />
            <asp:BoundField DataField="Type nr" HeaderText="Type Nr" />
            <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" />
            <asp:BoundField DataField="Serial Nr" HeaderText="Serial Nr" />
            <asp:BoundField DataField="Internal Nr" HeaderText="Internal Nr" />
            <asp:BoundField DataField="Warranty" HeaderText="Warranty" /> --%>
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:TemplateField>

                <ItemTemplate>

                    <table class="table table-striped table-hover">

                        <tr>
                            <td class="col-sm-6">
                                <asp:Label ID="Label8" runat="server" Text="Purchase date: ">
                                </asp:Label>
                            </td>
                            <td class="col-sm-6">
                                <asp:Label ID="lblPDate" runat="server" Text='<%#Eval("Purchase date")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Type Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Type nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Manufacturer: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Manufacturer")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Serial Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Serial Nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Internal Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Internal Nr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Warranty: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Warranty")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Extra info: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("Extra info")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Added date: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Added date")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="Attachment: ">
                                </asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
            <%-- 
            <asp:TemplateField HeaderText="Attachment">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" CommandArgument='<%# Eval("attachmentLocation") %>' runat="server" OnClick="DownloadFile" Text='<%# Convert.ToString(Eval("attachmentLocation")).Length < 1 ? "" : Convert.ToString(Eval("attachmentLocation")) %>'>Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnReturn" runat="server" Text="Return to the overview page" OnClick="btnReturn_Click" CssClass="btn btn-primary"/>
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
    <asp:Label ID="lblTotalQuery" runat="server" Text=""></asp:Label>
    <!-- <asp:SqlDataSource ID="HardwareOverviewGridView" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT * FROM hardware;"></asp:SqlDataSource>
    -->

</asp:Content>

