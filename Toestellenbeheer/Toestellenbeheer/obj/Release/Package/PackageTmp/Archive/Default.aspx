<%@ Page Title="Archive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Archive.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron">
        <h1>Archive</h1>
        <p class="lead">See who accessed some hardware and where some hardware has been used.</p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Hardware history</h2>
            <p>
                Get a historical overview of the hardware, get a list who accessed the hardware
            </p>
            <p>
                <a class="btn btn-default" href="./hardware-history">Add now &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>People history</h2>
            <p>
                Get a historical overview of the people who accessed some hardware
            </p>
            <p>
                <a class="btn btn-default" href="./people-history">Get &raquo;</a>
            </p>
        </div>
        
    </div>



</asp:Content>
