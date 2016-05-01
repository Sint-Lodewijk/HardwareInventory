<%@ Page Title="Add a AD account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-person.aspx.cs" Inherits="Toestellenbeheer.Add.add_person" %>
<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row no-15 sub-title-bar blue-title">
        <div class="container">
            <div class="col-sm-12 text-center">
                <h4>Create an AD account.</h4>
                <p class="head-text">By filling those boxes to create an account in to active directory.</p>
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
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="drpRoleSelect" CssClass="col-md-2 control-label">Role</asp:Label>
                            <div class="col-md-10">
                                <asp:DropDownList ID="drpRoleSelect" CssClass="form-control margin-bottom-16" runat="server">
                                    <asp:ListItem Value="noneSelect" Selected="True">*** Please select a role ***</asp:ListItem>
                                    <asp:ListItem>gg_hardware_administration</asp:ListItem>
                                    <asp:ListItem>gg_hardware_admin</asp:ListItem>
                                    <asp:ListItem>gg_hardware_user</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtGivenName" CssClass="col-md-2 control-label" Text="Given name"></asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtGivenName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGivenName"
                                    CssClass="text-danger" ErrorMessage="The Given name is required." />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 control-label">Last name</asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="text-danger" ErrorMessage="The AD username is required." />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">AD account</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="text-danger" ErrorMessage="The AD username is required." />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                            <asp:Label ID="errorLabel" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <div class="col-md-offset-2 col-md-10">
                                    <asp:Button runat="server" OnClick="CreateAccount_Click" Text="Create account" CssClass="btn btn-default" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
</asp:Content>
