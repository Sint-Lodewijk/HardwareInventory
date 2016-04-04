<%@ Page Title="Manage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Manage.Default1" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15 gray-title">
        <div class="container title-container">

            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">
                    <img runat="server" src="~/Images/3-settings-icon.png" class="img-responsive center-image" /></h1>
                <p class="lead text-center">Manage the settings and parameters.</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ManageContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./manage-type">

                    <img src="~/Images/type-icon.png" runat="server" alt="Type" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Add and manage existing type of hardware
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./manage-type">Manage type &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./manage-manufacturer">

                    <img src="~/Images/manufacturer-icon.png" runat="server" alt="License overview" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Add and manage existing manufacturers
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./manage-manufacturer">Manage manufacturer &raquo;</a>
                    </p>
                </div>
            </div>

            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./assign-license">

                    <img src="~/Images/license-icon.png" runat="server" alt="Assign license" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Assign license to people or a hardware                       
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./assign-license">Assign license &raquo;</a>
                    </p>
                </div>
            </div>
        </div>
        <div class="row no-15">
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./assign-hardware">

                    <img src="~/Images/overview-icon.png" runat="server" alt="Assign hardware" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Assign hardware to people                       
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./assign-hardware">Assign hardware &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./return-hardware">

                    <img src="~/Images/return-icon.png" runat="server" alt="Return hardware" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Return the hardware                    
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./return-hardware">Return hardware &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./manage-requests">

                    <img src="~/Images/request-icon.png" runat="server" alt="Return hardware" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Accept or deny the hardware requests                   
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./manage-requests">Manage requests &raquo;</a>
                    </p>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
