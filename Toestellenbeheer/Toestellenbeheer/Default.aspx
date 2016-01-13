<%@ Page Title="Manage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Inventive Designers</h1>
        <p class="lead">Manage the hardware, users, licenses and more...</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Add a hardware</h2>
            <p>
                Add a hardware to the hardware assets.
            </p>
            <p>
                <a class="btn btn-default" href="~/Manage/Hardware">Add now &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Add a person</h2>
            <p>
                Add a person into the active directory.
            </p>
            <p>
                <a class="btn btn-default" href="~/Manage/Person">Add now &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Add a licence</h2>
            <p>
                Add a licence and assign it to the hardware or people. 
            </p>
            <p>
                <a class="btn btn-default" href="~/Manage/Licence">Add now &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>

