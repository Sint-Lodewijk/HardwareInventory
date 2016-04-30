<%@ Page Title="Assign license" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assign-license.aspx.cs" Inherits="Toestellenbeheer.Manage.assign_license" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            .hide {
                display: none;
            }
            .innerPopup {
                background-color: #DDDDDD;
                left: 1%;
                right: 1%;
            }
        </style>
    </head>
    <body>
        <asp:GridView ID="grvLicense" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="grvLicense_SelectedIndexChanged" OnRowDataBound="OnRowDataBound" DataKeyNames="License Code">
        </asp:GridView>
        <asp:Panel ID="ShowPanel" Visible="false" runat="server">
            <asp:Button ID="btnAPeople" runat="server" CssClass="btn btn-primary" OnClick="btnAPeople_Click" Text="Show People" />
            <asp:Button ID="btnAHardware" runat="server" CssClass="btn btn-primary" OnClick="btnAHardware_Click" Text="Show Hardware" />
        </asp:Panel>
        <AjaxControl:ModalPopupExtender runat="server" ID="PeoplePopUP"
            TargetControlID="btnAPeople"
            PopupControlID="PeoplePanel"
            BackgroundCssClass="modalBackground"
            DropShadow="False"
            >
        </AjaxControl:ModalPopupExtender>
        <asp:Panel runat="server" ID="PeoplePanel" CssClass="innerPopup" Visible="false">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grvLicenseUnassignedPeople" CssClass="table table-hover table-striped gridview" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAssign" runat="server" CommandName="Delete" OnClientClick="if (!confirm('Are you sure to assign selected people with the license?')) return false;" Text="Assign"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="grvLicenseUnassignedPeople" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:GridView ID="grvLicenseUnassignedHardware" OnRowDataBound="OnRowDataBound" CssClass="table table-hover table-striped gridview" OnRowDeleting="grvLicenseUnassignedHardware_RowDeleting" DataKeyNames="internalNr" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAssign" runat="server" OnClientClick="if (!confirm('Are you sure to assign selected people with the license?')) return false;" CommandName="Delete" Text="Assign"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblProblem" runat="server"></asp:Label>
    </body>
    </html>
</asp:Content>
