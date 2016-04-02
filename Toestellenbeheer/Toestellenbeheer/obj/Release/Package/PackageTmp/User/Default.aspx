<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Users.Default" %>

<asp:Content ID="conUser" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>User panel</h1>
        <p class="lead">Request a hardware, see my hardware</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>My license</h2>
            <p>
                List out of my license.
            </p>
            <p>
                <a class="btn btn-default" href="my-license">Get &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">

            <h2>My hardware</h2>
            <p>
                List out of my lend hardware.
            </p>
            <p>
                <a class="btn btn-default" href="my-hardware">Get &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Request hardware</h2>
            <p>
                Request a hardware
            </p>
            <p>
                <a class="btn btn-default" href="request-hardware">Request &raquo;</a>
            </p>
        </div>


    </div>


</asp:Content>
