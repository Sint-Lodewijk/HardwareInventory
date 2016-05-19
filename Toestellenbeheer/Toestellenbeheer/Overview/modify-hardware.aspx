<%@ Page Title="Modify hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="modify-hardware.aspx.cs" Inherits="Toestellenbeheer.Overview.modify_hardware" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        td {
            text-align: left !important;
        }
    </style>
    <asp:GridView ID="grvModifyHardware" AutoGenerateColumns="false" CssClass="table table-striped table-hover gridview" runat="server">
        <Columns>
            <asp:ImageField DataImageUrlField="pictureLocation" DataImageUrlFormatString="../UserUploads/Images/{0}" HeaderText="Preview Image" AlternateText="Hardware Image"
                NullDisplayText="No image associated." ControlStyle-CssClass="picutureGrid" ReadOnly="True">
                <ControlStyle CssClass="picutureGrid"></ControlStyle>
            </asp:ImageField>
            <asp:TemplateField>
                <ItemTemplate>
                    <table class="table table-striped table-hover">
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Serial Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("serialNr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Internal Nr: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("internalNr")%>'>
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td class="col-sm-6">
                                <asp:Label ID="Label8" runat="server" Text="Purchase date: ">
                                </asp:Label>
                            </td>
                            <td class="col-sm-6">
                                    <script>
                                        $(function () {
                                            $("[id$=txtPDate]").datepicker({ dateFormat: 'dd-mm-yy' }).val();
                                        });
                                    </script>
                                    <asp:TextBox ID="txtPDate" runat="server" Text='<%#Eval("purchaseDate")%>' CssClass="form-control">
                                    </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Type: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" AutoPostBack="false" CssClass="selectpicker form-control" runat="server" SelectedValue='<%# Eval("type") %>' DataSourceID="sqlType" DataTextField="type" DataValueField="type"></asp:DropDownList>
                                <asp:SqlDataSource ID="sqlType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type FROM type"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Manufacturer: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlManufacturer" AutoPostBack="false" CssClass="selectpicker form-control" runat="server" SelectedValue='<%# Eval("manufacturerName") %>' DataSourceID="sqlManufacturer" DataTextField="manufacturerName" DataValueField="manufacturerName"></asp:DropDownList>
                                <asp:SqlDataSource ID="sqlManufacturer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT manufacturerName FROM manufacturer"></asp:SqlDataSource>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Warranty: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWarranty" CssClass="form-control" runat="server" Text='<%#Eval("warranty")%>'>
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Extra info: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExtra" runat="server" CssClass="form-control" Text='<%#Eval("extraInfo")%>'>
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Extra info: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtModelNr" runat="server" CssClass="form-control" Text='<%#Eval("ModelNr")%>'>
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Added date: ">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server"  Text='<%#Eval("addedDate")%>'>
                                </asp:Label></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button runat="server" OnClick="btnModify_Click" ID="btnModify" CssClass="btn btn-primary" Text="Modify" />
</asp:Content>
