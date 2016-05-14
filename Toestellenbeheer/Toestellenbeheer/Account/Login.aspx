<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Toestellenbeheer.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container body-content">
        <div class="row">
            <h2 class="col-md-12">Log in</h2>
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h4 id="TitleType" runat="server">Please use your Active Directory Account to log into this application.</h4>
                        <hr />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User Name</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="text-danger" ErrorMessage="The username is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe" data-container="body">Remember me?</asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="errorLabel" runat="server" Text=""></asp:Label>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button title="Login using the AD credentials" ID="btnLogin" runat="server" OnClick="Login_Click" Text="Log in" data-toggle="popover" data-placement="top" data-content="Remember me next time." CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <h4 class="hint-block">Do not have an account?
                    </h4>
                    <hr />
                    <p>
                        If you do not have an account of this domain, please contact your domainadministrator in order to create a account.
                    </p>
                </div>
                <div class="alert alert-db alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <strong>Having another credential?</strong>
                    <br />
                    <asp:LinkButton ID="lnkAuthType" OnClick="DBAUTH" runat="server" Text="DB Authentication" CausesValidation="false"></asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="alert alert-warning" data-dismiss="alert">
                <strong>Default AD credential: Only for test purpose </strong>
                <div class="row">
                    <div class="col-sm-6">AD username: </div>
                    <div class="col-sm-6">jhli </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">AD password: </div>
                    <div class="col-sm-6">Pwd1234 </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
