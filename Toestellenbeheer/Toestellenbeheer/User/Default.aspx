<%@ Page Title="MySpace" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Users.Default" %>
<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15 red-title">
        <div class="container title-container">
            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">
                    <img runat="server" src="~/Images/single-user-icon.png" class="img-responsive center-image" /></h1>
                <p class="lead text-center">My license, hardware and request a hardware...</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="conUser" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container margin-top-20">
        <div class="col-md-4 text-center thumbnail no-border">
            <a href="./my-license">
                <img src="~/Images/license-icon.png" runat="server" alt="My license" class="img-responsive max-p80">
            </a>
            <div class="caption">
                <p>
                    List out my license key and license file.               
                </p>
                <p>
                    <a class="btn btn-default col-md-12" href="./my-license">My license &raquo;</a>
                </p>
            </div>
        </div>
        <div class="col-md-4 text-center thumbnail no-border">
            <a href="./my-hardware">
                <img src="~/Images/overview-icon.png" runat="server" alt="My hardware" class="img-responsive max-p80">
            </a>
            <div class="caption">
                <p>
                    List out my hardware.
                </p>
                <p>
                    <a class="btn btn-default col-md-12" href="./my-hardware">My hardware &raquo;</a>
                </p>
            </div>
        </div>
        <div class="col-md-4 text-center thumbnail no-border">
            <a href="./request-hardware">
                <img src="~/Images/request-icon.png" runat="server" alt="Request hardware" class="img-responsive max-p80">
            </a>
            <div class="caption">
                <p>
                    Request a hardware.
                </p>
                <p>
                    <a class="btn btn-default col-md-12" href="./request-hardware">Request hardware &raquo;</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
