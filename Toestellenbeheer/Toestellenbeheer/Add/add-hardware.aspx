<%@ Page Title="Add a hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.add_hardware" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row no-15 sub-title-bar blue-title">
        <div class="container">
            <div class="col-sm-12">
                <p class="text-center head-text">Fill those info to add a hardware into the database.</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="addHardware" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="addHardwarePanel" runat="server">
        <fieldset class="hardware-add-item">
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-2" runat="server" AssociatedControlID="typeList">Type</asp:Label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="typeList" CssClass="form-control" runat="server" AutoPostBack="True" DataSourceID="sqlType" DataTextField="type" DataValueField="type" />
                    <asp:SqlDataSource ID="sqlType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type FROM type"></asp:SqlDataSource>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-2" runat="server" AssociatedControlID="manufacturerList">Manufacturer</asp:Label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="manufacturerList" CssClass="form-control normal-height" runat="server" AutoPostBack="True" Height="34px" DataSourceID="sqlManufacturer" DataTextField="manufacturerName" DataValueField="manufacturerName">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlManufacturer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT manufacturerName FROM manufacturer"></asp:SqlDataSource>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="txtDatepicker" CssClass="control-label col-sm-2" runat="server">Purchasedate</asp:Label>
                <div class="col-sm-10">
                    
                    <div class="input-group date" id="input-date">
                        <asp:TextBox runat="server" ID="txtDatepicker" placeholder="Click to select a date." CssClass="form-control" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                    </div>
                
                </div>
                <script>
                        $(function () {
                            $("[id$=input-date]").datepicker({
                                format: "dd-mm-yyyy",
                                autoclose: true,
                                todayHighlight: true
                            });
                        });
                    </script>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="Serialnr" CssClass="control-label col-sm-2" runat="server">Serial Number</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="Serialnr" CssClass="form-control" runat="server" placeholder="serial number can be found on the box of the product" />
                </div>
            </div>
            <br />
            <div class="form-group">
                <asp:Label runat="server" CssClass="control-label col-sm-2" AssociatedControlID="internalNr">Internal Nr</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="internalNr" CssClass="form-control" placeholder="internal number can be found under the hardware" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="control-label col-sm-2" AssociatedControlID="modelNr">Model nr</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="modelNr" CssClass="form-control" placeholder="model number of the hardware" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="warrantyInfo" CssClass="control-label col-sm-2" runat="server">Warranty</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="warrantyInfo" runat="server" CssClass="form-control" placeholder="warranty information" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="extraInfo" runat="server" CssClass="control-label col-sm-2">Extra info</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="extraInfo" CssClass="form-control" placeholder="additional info related to this hardware" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="PictureUpload" CssClass="control-label col-sm-2" runat="server">Picture</asp:Label>
                <div class="col-sm-10">
                    <div class="input-group">
                        <asp:FileUpload ID="PictureUpload" CssClass="btn btn-default form-control" runat="server" />
                        <div class="input-group-btn">
                            <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-info" OnClick="Upload_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group col-sm-10 col-sm-offset-2 hint-block">
                <asp:Label ID="ResultUploadImg" runat="server" Text="" CssClass="hint-block"></asp:Label>
                <asp:Label ID="Testlocation" runat="server" Text="" CssClass="hint-block"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="AttachmentUpload" runat="server" CssClass="control-label col-sm-2">Attachments</asp:Label>
                <div class="col-sm-10">
                    <div class="input-group">
                        <asp:FileUpload ID="AttachmentUpload" CssClass="btn btn-default form-control" runat="server" />
                        <div class="input-group-btn">
                            <asp:Button ID="UploadAttachment" runat="server" CssClass="btn btn-info" Text="Upload" OnClick="UploadAttachment_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group col-sm-10 col-sm-offset-2 hint-block">
                <asp:Label ID="ResultUploadAtta" runat="server" Text="" CssClass="hint-block"></asp:Label>
                <asp:Label ID="TestlocationAtt" runat="server" Text="" CssClass="hint-block"></asp:Label>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                    <asp:Button ID="Submit" runat="server" Text="Add hardware" CssClass="btn btn-primary margin-top-15" OnClick="Submit_Click" />
                    <!--<asp:Label ID="testSelected" runat="server" Text="testSelected"></asp:Label>
                <asp:TextBox ID="test" CssClass="form-control" runat="server"></asp:TextBox>-->
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="addResultPanel" runat="server">
        <asp:GridView ID="grvJustAddedHardware" AutoGenerateColumns="false" CssClass="table table-striped table-hover gridview" runat="server">
            <Columns>
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
                                    <asp:Label ID="lblPDate" runat="server" Text='<%#Eval("purchaseDate")%>'>
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Type: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("type")%>'>
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Manufacturer: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("manufacturerName")%>'>
                                    </asp:Label></td>
                            </tr>
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
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="Warranty: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("warranty")%>'>
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="Extra info: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("extraInfo")%>'>
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Added date: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("addedDate")%>'>
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
            </Columns>
        </asp:GridView>
        <div class="col-sm-12 margin-top-26">
            <asp:Label ID="txtResultUpload" CssClass="control-label" runat="server"></asp:Label>
        </div>
        <asp:Button ID="btnAddAnotherHardware" runat="server" Text="Add another hardware" CssClass="btn btn-primary margin-top-5 col-sm-offset-5" OnClick="btnAddAnotherHardware_Click" />
    </asp:Panel>
</asp:Content>
