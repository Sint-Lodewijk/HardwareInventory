﻿<%@ Page Title="Add a hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.add_hardware" %>

<asp:Content ID="addHardware" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset class="hardware-add-item">
        <legend>Voer de nodige gegevens in.</legend>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-2" runat="server" AssociatedControlID="typeList">Type</asp:Label>
            <div class="col-sm-10">
                <asp:DropDownList ID="typeList" CssClass="form-control" runat="server" />
            </div>

        </div>

        <!-- <asp:SqlDataSource ID="TypeListDB" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ProviderName="<%$ ConnectionStrings:DefaultConnection.ProviderName %>" SelectCommand="SELECT type FROM type;"></asp:SqlDataSource>
           -->
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
            <asp:Label AssociatedControlID="purchasedateCalendar" CssClass="control-label col-sm-2" runat="server">Purchase date</asp:Label>
            <div class="col-sm-10">
                <asp:Calendar ID="purchasedateCalendar" runat="server"></asp:Calendar>
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
            <asp:Label AssociatedControlID="PictureUpload" CssClass="control-label col-sm-2 input-group" runat="server">Picture</asp:Label>
            <span class="col-sm-8">
                <asp:FileUpload ID="PictureUpload" CssClass="btn btn-default form-control" runat="server" /></span>
            <span class="col-sm-1">
                <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-default margin-top-5" OnClick="Upload_Click" /></span>
            <asp:Label ID="ResultUploadImg" runat="server" Text=""></asp:Label>

            <asp:Label ID="Testlocation" runat="server" Text=""></asp:Label>

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
                <asp:TextBox runat="server" ID="extraInfo" CssClass="form-control" placeholder="internal number can be found under the hardware" />
            </div>
        </div>

        <div class="form-group">

            <asp:Label AssociatedControlID="AttachmentUpload" runat="server" CssClass="control-label col-sm-2">Attachments</asp:Label>

            <div class="col-sm-8">
                <asp:FileUpload ID="AttachmentUpload" CssClass="btn btn-default form-control" runat="server" />
            </div>

            <div class="col-sm-1">
                <asp:Button ID="UploadAttachment" runat="server" CssClass="btn btn-default margin-top-5" Text="Upload" OnClick="UploadAttachment_Click" />

            </div>

            <asp:Label ID="ResultUploadAtta" runat="server" Text=""></asp:Label>

            <asp:Label ID="TestlocationAtt" runat="server" Text=""></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="txtResultUpload" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="Submit" runat="server" Text="Add hardware" CssClass="btn btn-default margin-top-15" OnClick="Submit_Click" />
                <!--<asp:Label ID="testSelected" runat="server" Text="testSelected"></asp:Label>
                <asp:TextBox ID="test" CssClass="form-control" runat="server"></asp:TextBox>-->
            </div>
        </div>

    </fieldset>

</asp:Content>
