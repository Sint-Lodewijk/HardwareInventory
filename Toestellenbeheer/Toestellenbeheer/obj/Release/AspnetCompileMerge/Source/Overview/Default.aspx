<%@ Page Title="Overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Overview.Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Overview</h1>
        <p class="lead">Choose between hardware and license overview.</p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Hardware overview</h2>
            <p>
                List out the hardware in the database.
            </p>
            <p>
                <a class="btn btn-default" href="hardware-overview">Hardware overview &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>License overview</h2>
            <p>
               List out the license in the database, and wherever the license has been assigned to.
            </p>
            <p>
                <a class="btn btn-default" href="license-overview">License overview &raquo;</a>
            </p>
        </div>
        
    </div>

</asp:Content>

