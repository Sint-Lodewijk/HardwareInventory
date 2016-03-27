<%@ Page Title="Add a license" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="add-license.aspx.cs" Inherits="Toestellenbeheer.Manage.add_license" %>

<asp:Content ID="LicenseAdd" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControl" %>


    <html>
    <head>
        <title></title>
        <style type="text/css">
            .modalBackground {
                background-color: Gray;
                filter: alpha(opacity=70);
                opacity: 0.7;
            }

            .modalpopupdragbar {
                cursor: move;
                width: 90%;
                margin-left: auto;
                margin-right: auto;
                color: white;
                text-align: center;
                height: 20px;
                border: solid 1px #000066;
                vertical-align: middle;
                font-family: Verdana;
                font-size: 10pt;
                font-weight: bold;
                vertical-align: middle;
            }

            .modalPopup {
                background-color: #DDDDDD;
                border-width: 1px;
                border-style: solid;
                border-color: black;
                padding: 3px;
                width: 250px;
                left: 1%;
                right: 1%;
            }

            .outerPopup {
                background-color: transparent;
            }

            .innerPopup {
                background-color: #DDDDDD;
                left: 1%;
                right: 1%;
            }
        </style>

    </head>

    <body>


        <div id="hardwareGrides">
            <div class="form-group">
                <asp:Label ID="licenseName" runat="server" AssociatedControlID="txtLicenseName" CssClass="control-label col-sm-2">License name</asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtLicenseName" placeholder="Name of license" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Label ID="licenseCode" runat="server" AssociatedControlID="txtLicenseCode" CssClass="control-label col-sm-2">License code: </asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtLicenseCode" placeholder="Enter the licensecode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label AssociatedControlID="txtDatepickerExpire" CssClass="control-label col-sm-2" runat="server" Text="Expire Date:"></asp:Label>

                    <div class="col-sm-10">
                        <link rel="stylesheet" href="../../Scripts/jquery-ui.css">
                        <script src="../../Scripts/jquery-2.2.0.js"></script>
                        <script src="../../Scripts/jquery-ui.js"></script>

                        <script>
                            $(function () {
                                $("[id$=txtDatepickerExpire]").datepicker({ dateFormat: 'dd-mm-yy' }).val();
                            });
                        </script>
                        <asp:TextBox runat="server" ID="txtDatepickerExpire" placeholder="Click to select a expire date" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtExtraInfoLicense" runat="server" CssClass="control-label col-sm-2" Text="Extra info:"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtExtraInfoLicense" placeholder="Extra information of this license" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">

                    <asp:Label AssociatedControlID="LicenseFileUpload" runat="server" CssClass="control-label col-sm-2">License File:</asp:Label>

                    <div class="col-sm-8">
                        <asp:FileUpload ID="LicenseFileUpload" CssClass="btn btn-default form-control" runat="server" />
                    </div>

                    <div class="col-sm-2">
                        <asp:Button ID="btnUploadLicense" runat="server" CssClass="btn btn-info margin-top-6-2 col-sm-12" Text="Upload" OnClick="btnUploadLicense_Click" />
                    </div>
                    <div class="col-sm-10 col-sm-offset-2 form-group hint-block">

                        <asp:Label ID="ResultUploadAtta" runat="server" Text="" CssClass="hint-block"></asp:Label>

                        <asp:Label ID="TestlocationAtt" runat="server" Text="" CssClass="hint-block"></asp:Label>
                    </div>
                </div>

            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <asp:Button ID="hideShowHardware" runat="server" Text="Assign to hardware" OnClick="hideShowHardware_Click" CssClass="btn btn-info form-control" />
                </div>
            </div>

            <AjaxControl:ModalPopupExtender runat="server" ID="HardwarePanelPopUP"
                TargetControlID="hideShowHardware"
                PopupControlID="hardwarePanel"
                BackgroundCssClass="modalBackground"
                DropShadow="False"
                OkControlID="btnAssignToSelectedHardware"
                CancelControlID="btnCloseHardware">
            </AjaxControl:ModalPopupExtender>
            <asp:Panel ID="hardwarePanel" CssClass="innerPopup" runat="server">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="search" class="form-group margin-top-5">
                            <asp:Label ID="lblSearch" runat="server" CssClass="control-label col-sm-1 col-sm-offset-1" AssociatedControlID="txtSearch">Search</asp:Label>
                            <div class="col-sm-4">
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
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-control btn btn-default col-sm-12" OnClick="Search_Click" />
                            </div>
                        </div>

                        <asp:GridView ID="licenseOverviewGridSearch" OnSelectedIndexChanged="display_search_button" CssClass="table table-hover table-striped gridview" runat="server">


                            <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="HardwareLicense" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT typeNr, manufacturerName, internalNr, serialNr  FROM hardware;"></asp:SqlDataSource>
                        <asp:GridView ID="grvHardwareLicenseSelect" OnRowDataBound="OnRowDataBound" CssClass="table table-hover table-striped gridview" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="internalNr,serialNr" OnSelectedIndexChanged="hardwareLicenseSelection_Click" OnPageIndexChanged="grvHardwareLicenseSelect_PageIndexChanged" DataSourceID="HardwareLicense" OnSorting="displayHardwarePanel">
                            <Columns>
                                <asp:BoundField DataField="typeNr" HeaderText="Type nr" SortExpression="typeNr" />
                                <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                                <asp:BoundField DataField="internalNr" HeaderText="Internal nr" ReadOnly="True" SortExpression="internalNr" />
                                <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
                            </Columns>

                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                        <div class="form-group">
                            <div class="col-sm-offset-5 col-sm-7">
                                <asp:Button ID="btnAssignToSelectedHardware" runat="server" Text="Assign" CssClass="btn btn-primary margin-top-5" OnClick="assignToSelectedHardware_Click" />
                            </div>

                        </div>
                        <div class="form-group">

                            <div class="col-sm-offset-5 col-sm-7">

                                <asp:Button ID="btnAssignToSelectedHardwareSearch" runat="server" Text="Assign" CssClass="btn btn-primary margin-top-5" OnClick="assignToSelectedHardwareSearch_Click" />
                                <asp:Button ID="btnCloseHardware" runat="server" Text="Cancel" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:Button ID="hideShowPeople" runat="server" Text="Assign to people" OnClick="hideShowPeople_Click" CssClass="btn btn-info form-control" />
                </div>
            </div>
        <AjaxControl:ModalPopupExtender runat="server" ID="PeoplePopUP"
            TargetControlID="hideShowHardware"
            PopupControlID="peoplePanel"
            BackgroundCssClass="modalBackground"
            DropShadow="False"
            OkControlID="btnAssignLicenseToPeople"
            CancelControlID="btnClosePeople">
        </AjaxControl:ModalPopupExtender>

        <asp:Panel ID="peoplePanel" CssClass="innerPopup" runat="server">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="licenseOverviewGridPeople" OnPageIndexChanging="licenseOverviewGridPeople_PageIndexChanging" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="selectPeopleGridview_Click" AllowPaging="True" PageSize="9">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                    <div class="form-group col-sm-offset-4 col-sm-2">
                        <asp:Button runat="server" ID="btnAssignLicenseToPeople" OnClick="assignLicenseToPeople" CssClass="btn btn-primary margin-top-5" Text="Assign to selected person" />
                        <asp:Button ID="btnClosePeople" runat="server" Text="Cancel" CssClass="btn btn-info" />

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAssignLicenseToPeople" EventName="click" />
                    <asp:AsyncPostBackTrigger ControlID="btnClosePeople" EventName="click" />

                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>

        <div class="form-group col-sm-12">
            <asp:Button runat="server" Text="Add license only" ID="btnAddLicense" CssClass="btn btn-primary col-sm-12 margin-top-5" OnClick="btnAddLicense_click" />
        </div>
        <div class="form-group col-sm-12">
            <asp:Label ID="testLabel" runat="server" Text=""></asp:Label>
        </div>
        </div>
    </body>
    </html>
</asp:Content>
