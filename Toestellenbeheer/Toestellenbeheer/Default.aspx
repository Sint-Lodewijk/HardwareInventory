<%@ Page Title="Manage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Inventive Designers</h1>
        <p class="lead">Manage the hardware, users, licenses and more...</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Overview</h2>
            <p>
                Get a overview of the hardware and license in the database.
            </p>
            <p>
                <a class="btn btn-default" href="Overview">Overview &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Add</h2>
            <p>
                Add a license, hardware or a person into the database.
            </p>
            <p>
                <a class="btn btn-default" href="Add">Add now &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Manage</h2>
            <p>
                Managepanel for administrators. 
            </p>
            <p>
                <a class="btn btn-default" href="Manage">Add now &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>

