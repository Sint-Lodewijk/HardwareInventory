﻿<%@ Page Title="Manage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer.Manage.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Manage</h1>
        <p class="lead">Manage the settings and parameters.</p>
    </div>

    <div class="row">
        <div class="col-md-3">
            <h2>Add a type</h2>
            <p>
                Add a type.
            </p>
            <p>
                <a class="btn btn-default" href="manage-type">Add type &raquo;</a>
            </p>
        </div>
          <div class="col-md-3">
            <h2>Assign license</h2>
            <p>
                Assign a license to a hardware or a people.
            </p>
            <p>
                <a class="btn btn-default" href="assign-license">Assign &raquo;</a>
            </p>
        </div>

        <div class="col-md-3">
            <h2>Assign hardware
            </h2>
            <p>
                Assign a hardware to a person
            </p>
            <p>
                <a class="btn btn-default" href="assign-hardware">Assign hardware to person &raquo;</a>
            </p>
        </div>
        <div class="col-md-3">
            <h2>Return hardware
            </h2>
            <p>
                Get the returned hardware
            </p>
            <p>
                <a class="btn btn-default" href="return-hardware">Return hardware &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
