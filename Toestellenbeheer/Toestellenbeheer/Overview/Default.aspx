<%@ Page Title="Overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Overview.Default" %>
<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15">
        <div class="container title-container">
            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">
                    <img runat="server" src="~/Images/hardware-main-icon.png" class="img-responsive center-image" /></h1>
                <p class="lead text-center">Hardware and license overview...</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-6 text-center thumbnail no-border">
                <a href="./hardware-overview">
                    <img src="~/Images/overview-icon.png" runat="server" alt="Hardware overview" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        List out the hardware in the database and where the hardware has been assigned to.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./hardware-overview">Hardware overview &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-6 text-center thumbnail no-border">
                <a href="./license-overview">
                    <img src="~/Images/license-icon.png" runat="server" alt="License overview" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        List out the license in the database, and where the license has been assigned to.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./license-overview">License overview &raquo;</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

