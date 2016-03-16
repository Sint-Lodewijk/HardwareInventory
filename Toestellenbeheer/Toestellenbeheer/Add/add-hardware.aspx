<%@ Page Title="Add a hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.add_hardware" %>

<asp:Content ID="addHardware" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="addHardwarePanel" runat="server">
        <fieldset class="hardware-add-item">
            <legend>Please fill those info to add a hardware into the database.</legend>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-2" runat="server" AssociatedControlID="typeList">Type</asp:Label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="typeList" CssClass="form-control" runat="server" EnableViewState="true" AutoPostBack="true" />
                </div>

            </div>

       
            <div class="form-group">

                <asp:Label CssClass="control-label col-sm-2" runat="server" AssociatedControlID="manufacturerList">Manufacturer</asp:Label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="manufacturerList" CssClass="form-control normal-height" runat="server" AutoPostBack="True" Height="34px">
                        <asp:ListItem>Apple</asp:ListItem>
                        <asp:ListItem>Lenovo</asp:ListItem>
                        <asp:ListItem>HP</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group">
                <asp:Label AssociatedControlID="txtDatepicker" CssClass="control-label col-sm-2" runat="server">Purchasedate</asp:Label>

                <div class="col-sm-10">
                    <link rel="stylesheet" href="../../Scripts/jquery-ui.css">
                    <script src="../../Scripts/jquery-2.2.0.js"></script>
                    <script src="../../Scripts/jquery-ui.js"></script>

                    <script>
                        $(function () {
                            $("[id$=txtDatepicker]").datepicker({ dateFormat: 'dd-mm-yy' }).val();
                        });
                    </script>
                    <asp:TextBox runat="server" ID="txtDatepicker" CssClass="form-control" />
                </div>
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
                <div class="col-sm-8">
                    <asp:FileUpload ID="PictureUpload" CssClass="btn btn-default form-control" runat="server" />
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-info margin-top-6-2" OnClick="Upload_Click" />
                </div>
            </div>
            <div class="form-group col-sm-10 col-sm-offset-2 hint-block">
                <asp:Label ID="ResultUploadImg" runat="server" Text="" CssClass="hint-block"></asp:Label>
                <asp:Label ID="Testlocation" runat="server" Text="" CssClass="hint-block"></asp:Label>
            </div>
            <div class="form-group">

                <asp:Label AssociatedControlID="AttachmentUpload" runat="server" CssClass="control-label col-sm-2">Attachments</asp:Label>

                <div class="col-sm-8">
                    <asp:FileUpload ID="AttachmentUpload" CssClass="btn btn-default form-control" runat="server" />
                </div>

                <div class="col-sm-2">
                    <asp:Button ID="UploadAttachment" runat="server" CssClass="btn btn-info margin-top-6-2" Text="Upload" OnClick="UploadAttachment_Click" />

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
            </Columns>
        </asp:GridView>
        <div class="col-sm-12 margin-top-26">
            <asp:Label ID="txtResultUpload" CssClass="control-label" runat="server"></asp:Label>
        </div>
        <asp:Button ID="btnAddAnotherHardware" runat="server" Text="Add another hardware" CssClass="btn btn-primary margin-top-5 col-sm-offset-5" OnClick="btnAddAnotherHardware_Click" />
    </asp:Panel>
</asp:Content>
