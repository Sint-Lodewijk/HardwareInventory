<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="Toestellenbeheer.Download" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" Text="Not downloading? Click link below"></asp:Label>
    <asp:LinkButton runat="server" ID="lnkDownload" OnClick="lnkDownload_Click"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
