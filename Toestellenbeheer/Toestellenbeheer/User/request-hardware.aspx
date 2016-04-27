<%@ Page Title="Request a hardware" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="request-hardware.aspx.cs" Inherits="Toestellenbeheer.Users.request_hardware" %>

<asp:Content ID="cntRequest" ContentPlaceHolderID="MainContent" runat="server">
        <div class="modal fade" id="hardwareImageModal" tabindex="-1" role="dialog" aria-labelledby="hardwareImage">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="hardwareImage">Preview Image</h4>
                        </div>
                        <div class="modal-body">

                            <asp:Image AlternateText="Hardware Image" CssClass="img-responsive center-block" ID="picDetail" runat="server"></asp:Image>


                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Select a type" CssClass="control-label col-sm-2"></asp:Label>
        <div class="col-sm-10">
            <asp:DropDownList ID="drpTypeList" OnSelectedIndexChanged="typeList_SelectedIndexChanged" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="type" DataValueField="type" />
        </div>
        <asp:GridView runat="server" ID="grvAvailableHardwareType" AutoGenerateColumns="False" DataKeyNames="serialNr,internalNr" OnRowDataBound="grvAvailibleHardwareType_RowDataBound" OnSelectedIndexChanged="grvAvailibleHardwareType_SelectedIndexChanged" CssClass="gridview table table-striped table-hover" OnRowDeleting="grvAvailableHardwareType_RowDeleting">
            <Columns>
                
                <asp:BoundField DataField="serialNr" HeaderText="Serial Nr" ReadOnly="True" SortExpression="serialNr" />
                <asp:BoundField DataField="internalNr" HeaderText="Internal Nr" ReadOnly="True" SortExpression="internalNr" />
                <asp:BoundField DataField="manufacturerName" HeaderText="Manufacturer name" SortExpression="manufacturerName" />
                <asp:BoundField DataField="modelNr" HeaderText="Model Nr" SortExpression="modelNr" />

                <asp:CommandField DeleteText="Picture" ShowDeleteButton="True" />

            </Columns>
            <SelectedRowStyle BackColor="#cc6600" />
        </asp:GridView>
        <div class="col-sm-12">
            <asp:Label ID="lblProblem" CssClass="col-sm-12 control-label" runat="server"></asp:Label>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="btnRequest" CssClass="btn btn-primary" runat="server" Text="Send a request" Visible="false" OnClick="btnRequest_Click" />
    </div>


</asp:Content>
