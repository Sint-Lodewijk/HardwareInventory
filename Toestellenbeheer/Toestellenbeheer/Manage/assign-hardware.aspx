<%@ Page Title="Assign hardware to people" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="assign-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.manage_hardware" %>

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

        <asp:GridView ID="grvHardwarePoolUnassigned" OnSelectedIndexChanged="grvHardwarePoolUnassigned_SelectedIndexChanged" DataKeyNames="serialNr,internalNr,manufacturerName,type" OnRowDataBound="grvHardwarePoolUnassigned_OnRowDataBound" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped gridview">
            <Columns>
                <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                    NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                    <ControlStyle CssClass="picutureGrid"></ControlStyle>
                </asp:ImageField>
                <asp:BoundField DataField="serialNr" HeaderText="Serial nr" ReadOnly="True" SortExpression="serialNr" />
                <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
                <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />


            </Columns>
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:Button ID="btnOpenPeoplePopUp" runat="server" CssClass="btn btn-primary" Text="Assign to people" Visible="false" OnClick="btnOpenPeoplePopUp_Click" />
    <asp:Button ID="target" CssClass="hide" runat="server" />
        <AjaxControl:ModalPopupExtender runat="server" ID="PeoplePopUp"
            TargetControlID="btnOpenPeoplePopUp"
            PopupControlID="PeoplePanel"
            BackgroundCssClass="modalBackground"
            DropShadow="False"
        
            CancelControlID="btnCancel">
        </AjaxControl:ModalPopupExtender>
        <asp:Panel ID="PeoplePanel" runat="server" CssClass="innerPopup" Visible="false">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grvPeopleAD" runat="server" AllowPaging="true" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="grvPeopleAD_OnRowDataBound" CssClass="table table-hover table-striped gridview">
                 
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:Button ID="btnAssignHardwarePeople" runat="server" Text="Assign" CssClass="btn btn-primary margin-top-5 col-centered" OnClick="assignHardwarePeople_Click" />
                    <asp:button ID="btnCancel" Text="Cancel" runat="server" cssclass="btn btn-info" onclick="btnCancel_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grvPeopleAD" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnAssignHardwarePeople" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Label ID="lblResult" runat="server"></asp:Label>
        </body></html>
</asp:Content>
