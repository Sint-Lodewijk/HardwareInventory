<%@ Page Title="Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Manage.Default" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15 blue-title">
        <div class="container title-container">

            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">
                    <img runat="server" src="~/Images/add-icon.png" class="img-responsive center-image" /></h1>
                <p class="lead text-center">Add the hardware, users, licenses and more...</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./add-hardware">

                    <img src="~/Images/overview-icon.png" runat="server" alt="Add hardware" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Add a hardware to the hardware assets.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./add-hardware">Add now &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./add-person">

                    <img src="~/Images/people-icon.png" runat="server" alt="Add People" class="img-responsive max-p80">
                </a>
                <div class="caption">

                    <p>
                        Add a person into the active directory with selected role.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./add-person">Add now &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="./add-license">

                    <img src="~/Images/license-icon.png" runat="server" alt="Manage" class="img-responsive max-p80">
                </a>

                <div class="caption">

                    <p>
                        Add a license and assign it to the hardware or people. 
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="./add-license">Add license &raquo;</a>
                    </p>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
