<%@ Page Title="Manage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Manage.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="jumbotron">
        <h1>Manage</h1>
        <p class="lead">Manage the settings and parameters.</p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Add a type</h2>
            <p>
                Add a type.
            </p>
            <p>
                <a class="btn btn-default" href="manage-type">Add type &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2></h2>
            <p>
               Reserved
            </p>
            <p>
                <a class="btn btn-default" href="license-overview">Reserved &raquo;</a>
            </p>
        </div>
        
    </div>

</asp:Content>
