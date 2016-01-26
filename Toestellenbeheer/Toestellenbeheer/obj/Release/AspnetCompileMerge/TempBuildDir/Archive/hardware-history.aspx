<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hardware-history.aspx.cs" Inherits="Toestellenbeheer.Archive.hardware_history" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grvHardware" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped gridview" OnRowDataBound="grvHardware_OnRowDataBound" OnSelectedIndexChanged="grvHardware_SelectedIndexChanged" DataKeyNames="internalNr">
        <Columns>
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


        </Columns>
    </asp:GridView>
    <asp:GridView ID="grvPeopleLinked" CssClass="table table-hover table-striped gridview" runat="server"></asp:GridView>
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
</asp:Content>
