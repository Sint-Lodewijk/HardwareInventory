<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Toestellenbeheer._Default" %>

<asp:Content ID="HeadCon" ContentPlaceHolderID="HeadContent" runat="server">
    <div class="row title-bar no-15">
        <div class="container title-container">
            <div class="col-lg-12 p6-margin">
                <h1 class="text-center">Inventive Designers</h1>
                <p class="lead text-center">Manage the hardware, users, licenses and more...</p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="udpInitialize" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="initSetupModal" tabindex="-1" role="dialog" aria-labelledby="initSetupTitle">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="background: url('<%= ResolveUrl("~/Images/modal-background.jpg")%>') no-repeat left center; background-size: cover;">
                        <div class="modal-header">
                            <h4 class="modal-title" id="initSetupTitle">Initial setup</h4>
                        </div>
                        <div class="modal-body no-padding">
                            <div id="carousel-init" class="carousel slide carousel-text" data-ride="carousel" data-interval="false">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                    <li data-target="#carousel-init" data-slide-to="0" class="active"></li>
                                    <li data-target="#carousel-init" data-slide-to="1"></li>
                                    <li data-target="#carousel-init" data-slide-to="2"></li>
                                    <li data-target="#carousel-init" data-slide-to="3"></li>

                                </ol>
                                <!-- Wrapper for slides -->
                                <div class="carousel-inner" role="listbox">
                                    <div class="item active">
                                        <div class="carousel-caption">
                                            <h3>HELLO!</h3>
                                            <p class="text-center">Please help us to initialize this application in just few steps!</p>
                                        </div>
                                    </div>
                                    <div class="item">
                                        <div class="carousel-caption">
                                            <h3>Type</h3>
                                            <p>Let's add some type hardware into the database!</p>
                                            <div class="row">
                                                <div class="input-group three-fourth">
                                                    <input type="text" runat="server" id="txtType" class="form-control">
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="btnType" OnClick="btnType_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                                                    </span>
                                                </div>
                                            </div>
                                            <ul class="list-group">
                                                <li class="list-group-item transparent three-fourth"><span class="badge" runat="server" id="lblTypeAvailible"></span>Availible type</li>
                                            </ul>
                                            <div class="row">
                                                <p>Or let's <a href="~/Manage/manage-type" runat="server">modify</a> the type</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="item">
                                        <div class="carousel-caption">
                                            <h3>Manufacturer</h3>
                                            <p>Let's add some manufacturers into the database!</p>
                                            <div class="row">
                                                <div class="input-group three-fourth">
                                                    <input type="text" runat="server" id="txtManufacturer" class="form-control">
                                                    <span class="input-group-btn">
                                                        <asp:Button OnClick="btnManufacturer_Click" CssClass="btn btn-primary" ID="btnManufacturer" runat="server" Text="Add" />
                                                    </span>
                                                </div>
                                            </div>
                                            <ul class="list-group">
                                                <li class="list-group-item  transparent three-fourth"><span class="badge" runat="server" id="lblAvailibleManufacturer"></span>Availible Manufacturer</li>
                                            </ul>
                                            <div class="row">
                                                <p>Or let's <a href="~/Manage/manage-manufacturer" runat="server">modify</a> the manufacturer</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="item">
                                        <div class="carousel-caption">
                                            <h3>Congratulations</h3>
                                            <p>You just finished the first time setup!</p>
                                            <p>You can start using this application right now!</p>
                                            <a href="./Default.aspx">By clicking here.</a>
                                        </div>
                                    </div>
                                </div>
                                <!-- Controls -->
                                <a class="left carousel-control" href="#carousel-init" role="button" data-slide="prev">
                                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#carousel-init" role="button" data-slide="next">
                                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnType" />
            <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnManufacturer" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="container margin-top-20">
        <div class="row no-15">
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Overview">
                    <img src="~/Images/overview-icon.png" runat="server" alt="Overview" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        Get a overview of the hardware and license in the database.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Overview">Overview &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Add">
                    <img src="~/Images/add-icon.png" runat="server" alt="Add" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        Add a license, licensefile, hardware or a person into the database.
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Add">Add now &raquo;</a>
                    </p>
                </div>
            </div>
            <div class="col-md-4 text-center thumbnail no-border">
                <a href="Manage">
                    <img src="~/Images/settings-icon.png" runat="server" alt="Manage" class="img-responsive max-p80">
                </a>
                <div class="caption">
                    <p>
                        Manage the parameters, accept the request, assign a hardware or license to a people ... 
                    </p>
                    <p>
                        <a class="btn btn-default col-md-12" href="Manage">Manage &raquo;</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

