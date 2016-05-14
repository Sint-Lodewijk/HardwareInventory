<%@ Page Title="Assign license" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assign-license.aspx.cs" Inherits="Toestellenbeheer.Manage.assign_license" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:GridView ID="grvLicense" CssClass="table table-hover table-striped gridview" runat="server" OnSelectedIndexChanged="grvLicense_SelectedIndexChanged" OnRowDataBound="OnRowDataBound" DataKeyNames="License Code">
        </asp:GridView>
        <asp:Panel ID="ShowPanel" Visible="false" runat="server">
            <asp:Button ID="btnAPeople" runat="server" CssClass="btn btn-primary" OnClick="btnAPeople_Click" Text="Show People" />
            <asp:Button ID="btnAHardware" runat="server" CssClass="btn btn-primary" OnClick="btnAHardware_Click" Text="Show Hardware" />
        </asp:Panel>
        <asp:Panel runat="server" ID="PeoplePanel" CssClass="innerPopup" Visible="false">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grvLicenseUnassignedPeople" EmptyDataText="0 license key returned from the query!" CssClass="table table-hover table-striped gridview" runat="server">
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
  </asp:Content>
