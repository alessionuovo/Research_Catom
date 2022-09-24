<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Research.aspx.cs" Inherits="Research_Project.Research" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .hidden {display:none;}
    </style>
    <script type="text/javascript">
        function checkResearch() {
            error_fl = false;
            $(".my").addClass("hidden");
            if ($("input[id$=txtName]").val() == "") {
                $("#error").removeClass("hidden");
                $("#nameError").removeClass("hidden");
                error_fl = true;
            }
            if ($("input[id$=txtDatePublish]").val() == "") {
                $("#error").removeClass("hidden");
                $("#dateError").removeClass("hidden");
                error_fl = true;
            }
            if ($("input[id$=txtAbstract]").val() == "") {
                $("#error").removeClass("hidden");
                $("#abstractError").removeClass("hidden");
                error_fl = true;
            }
            return !error_fl;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="tblResearchs" CssClass="table table-striped" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Date</asp:TableHeaderCell>
            <asp:TableHeaderCell>Abstract</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Label ID="lblNoResearchs" runat="server" Visible="false" CssClass="alert alert-danger"> </asp:Label>
    <br />
    <br />
    Add Research: <br />
    <div class="row">
     <div class="col-md-1"> Name:</div>    
     <div class="col-md-2"> <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox></div>
        <div class="col-md-1"> Date:</div>    
     <div class="col-md-2"> <asp:TextBox type="date" CssClass="form-control" ID="txtDatePublish" runat="server"></asp:TextBox></div>
        <div class="col-md-1"> Abstract:</div>    
     <div class="col-md-2"> <asp:TextBox ID="txtAbstract" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
         <div class="col-md-1">
             <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary" OnClientClick="return checkResearch();" OnClick="btnSave_Click" />
         </div>

     </div>
        <br />
        <br />
        <br />
        <div id="error" class="hidden mt-4">
            <div id="nameError" class="hidden alert alert-danger my">No Research Name</div>
            <div id="dateError" class="hidden alert alert-danger my">No Research Date</div>
            <div id="abstractError" class="hidden alert alert-danger my">No Research Abstract</div>
        </div>
        <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
</asp:Content>
