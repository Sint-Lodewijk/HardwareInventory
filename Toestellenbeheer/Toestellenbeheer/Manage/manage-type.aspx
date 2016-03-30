<%@ Page Title="Manage type" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="manage-type.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_type" %>

<asp:Content ID="ManageType" ContentPlaceHolderID="MainContent" runat="server">
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

        <div class="form-group">

            <asp:Label ID="lblTypeName" runat="server" CssClass="control-label col-sm-2" AssociatedControlID="typeName">Type name</asp:Label>
            <div class="col-sm-7">

                <asp:TextBox ID="typeName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnAddType" runat="server" Text="Add a type" CssClass="btn btn-primary margin-top-5" OnClick="btnAddType_Click" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lblProblem" Text=""></asp:Label>
            </div>
        </div>
        <asp:GridView ID="typeSelect" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="typeSelect_SelectedIndexChanged" runat="server" Width="80%" CssClass="table table-hover table-striped gridview" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="type">
            <Columns>
                <asp:BoundField DataField="type" HeaderText="Type" ReadOnly="False" SortExpression="type" />
            </Columns>
        </asp:GridView>
        <asp:Panel ID="ButtonPanel" runat="server" Visible="false">
            <asp:Button ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" runat="server" Text="Modify" />
            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-primary" runat="server" Text="Delete" />
        </asp:Panel>
        <asp:Button ID="target" runat="server" CssClass="hide" />
        <AjaxControl:ModalPopupExtender runat="server" ID="ModifyPopUP"
            TargetControlID="target"
            PopupControlID="ModifyPanel"
            BackgroundCssClass="modalBackground"
            DropShadow="False"
            OkControlID="target"
            CancelControlID="btnCancel">
        </AjaxControl:ModalPopupExtender>
        <asp:Panel ID="ModifyPanel" runat="server">
            <asp:UpdatePanel runat="server" >
                <ContentTemplate>
                    <div class="form-group">
                        <asp:TextBox ID="txtType" runat="server" CssClass="form-control col-sm-12"></asp:TextBox>
                        <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="btn btn-primary" runat="server" Text="Update" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-info" runat="server" Text="Cancel" />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

    </body>
    </html>
</asp:Content>
