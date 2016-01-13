<%@ Page Title="Add a hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-hardware.aspx.cs" Inherits="Toestellenbeheer.Manage.add_hardware" %>
<asp:Content ID="addHardware" ContentPlaceHolderID="MainContent" runat="server">

            <div id="add-hardware">
        <fieldset>
            <legend>Voer de nodige gegevens in.</legend>
           <label for="type">type</label>&nbsp;
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <br />
            <label for="Manufacturer">Manufacturer<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            </label>
                <br />
            <label for="purchasedate">Purchase date</label>
                <asp:Calendar ID="purchasedateCalendar" runat="server"></asp:Calendar>
            <br />
                
            <label for="Serialnr">Serial Number</label>
           <asp:TextBox id="Serialnr" runat="server"  placeholder="serial number can be found on the box of the product" /> <br />

            <label for="internalNr">Internal Number</label>
            <asp:TextBox runat="server" id="internalNr" placeholder="internal number can be found under the hardware" /> <br />

            <label for="Picture">Picture -&nbsp; </label> add a picture - select one from the computer.
            <asp:FileUpload ID="PictureUpload" runat="server" />
            <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="Upload_Click" />
            <asp:Label ID="ResultUploadImg" runat="server" Text=""></asp:Label>
            <asp:Label ID="Testlocation" runat="server" Text=""></asp:Label>
            <br />
            

            <label for="warrantyInfo">Warranty information</label>
            <asp:TextBox id="warrantyInfo" runat="server" placeholder="internal number can be found under the hardware" Width="128px" /> <br />

            <label for="extraInfo">Extra information</label>
            <asp:TextBox runat="server" id="extraInfo" placeholder="internal number can be found under the hardware" /> <br />


            <label for="attachments">Add attachments</label>&nbsp;
            <asp:FileUpload ID="AttachmentUpload" runat="server" />
            <asp:Button ID="UploadAttachment" runat="server" Text="Upload" OnClick="UploadAttachment_Click" />
            <asp:Label ID="ResultUploadAtta" runat="server" Text=""></asp:Label>
            <asp:Label ID="TestlocationAtt" runat="server" Text=""></asp:Label>
            <br />

           <asp:Button ID="Submit" runat="server" Text="Add hardware" OnClick="Submit_Click" />
            <asp:TextBox id="test" runat="server"></asp:TextBox>
        </fieldset>
    </div>
</asp:Content>
