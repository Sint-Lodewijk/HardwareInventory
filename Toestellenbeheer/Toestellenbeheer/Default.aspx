<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer._Default" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15">
        <div class="container title-container">

            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">Inventive Designers</h1>
                <p class="lead text-center">Manage the hardware, users, licenses and more...</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Overview">

                    <img src="~/Images/overview-icon.png" runat="server" alt="Overview" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Get a overview of the hardware and license in the database.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Overview">Overview &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Add">

                    <img src="~/Images/add-icon.png" runat="server" alt="Add" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Add a license, licensefile, hardware or a person into the database.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Add">Add now &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Manage">

                    <img src="~/Images/settings-icon.png" runat="server" alt="Manage" class="img-responsive max-p80">
                </a>

                <div class="caption">

                    <p>
                        Manage the parameters, accept the request, assign a hardware or license to a people ... 
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Manage">Manage &raquo;</a>
                    </p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

