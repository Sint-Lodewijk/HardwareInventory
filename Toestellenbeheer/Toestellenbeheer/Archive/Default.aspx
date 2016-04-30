<%@ Page Title="Archive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Archive.Default" %>
<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15 purple-title">
        <div class="container title-container">
            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">
                    <img runat="server" src="~/Images/archive-icon.png" class="img-responsive center-image" /></h1>
                <p class="lead text-center">See who accessed some hardware and where some hardware has been used.</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ArchiveDefault" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-6 text-center thumbnail no-border">
                <a href="./hardware-history">
                    <img src="~/Images/overview-icon.png" runat="server" alt="Hardware overview" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        Get a historical overview of the hardware, get a list who accessed the hardware
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./hardware-history">Hardware history &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-6 text-center thumbnail no-border">
                <a href="./people-history">
                    <img src="~/Images/people-icon.png" runat="server" alt="License overview" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        Get a historical overview of the people who accessed some hardware
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./people-history">People history &raquo;</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
