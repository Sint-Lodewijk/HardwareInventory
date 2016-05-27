<%@ Page Title="Add a DB user" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-db-user.aspx.cs" Inherits="Toestellenbeheer.Add.add_db_user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row no-15 sub-title-bar blue-title">
        <div class="container">
            <div class="col-sm-12 text-center">
                <h4>Create an DB account.</h4>
                <p class="head-text">By filling those boxes to create an account in to the database.</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="AddPersonContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content">
        <div class="row">
            <section id="loginForm">
                <div class="form-horizontal">
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group col-sm-12">
                        <asp:Label runat="server" AssociatedControlID="drpRoleSelect" CssClass="col-md-2 margin-bottom-16 control-label">Role</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="drpRoleSelect" CssClass="selectpicker margin-bottom-16 form-control" runat="server">
                                <asp:ListItem Value="noneSelect" Selected="True">*** Please select a role ***</asp:ListItem>
                                <asp:ListItem>gg_hardware_administration</asp:ListItem>
                                <asp:ListItem>gg_hardware_admin</asp:ListItem>
                                <asp:ListItem>gg_hardware_user</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-sm-12">

                        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">DB account</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="The DB username is required." />
                        </div>
                    </div>
                    <div class="form-group col-sm-12">

                        <asp:Label runat="server" AssociatedControlID="ADAccount" CssClass="col-md-2 control-label">Associated AD</asp:Label>
                        <div class="col-md-10" style="margin-bottom: 16px">
                            <asp:TextBox runat="server" ID="ADAccount" placeholder="Optional" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group col-sm-12">

                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    <div class="form-group col-sm-12">

                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" CssClass="text-danger" ErrorMessage="The confirm password field is required." />
                        </div>
                    </div>
                    <asp:Label ID="errorLabel" runat="server" Text=""></asp:Label>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="CreateAccount_Click" Text="Create account" CssClass="btn btn-default" />
                        </div>
                    </div>

                </div>
            </section>
        </div>
    </div>
</asp:Content>
