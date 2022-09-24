<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Research_Specific.aspx.cs" Inherits="Research_Project.Research_Specific" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <style type="text/css">
        .hidden {display:none;}
    </style>
    <script>
        $(document).ready(function () {
            
        });
          function confirmDelete() {
            return confirm("Are you sure you want delete this researcher?");
        }
        function onResearcherDelete(index) {
            if (confirm("Are you sure you want delete this researcher?"))
                {
                $("input[id$=hdnResearcher]").val(index);
                return true;
            }
            else return false;
        }

        function checkResearch() {
            error_fl = false;
            $(".my").addClass("hidden");
            if ($("input[id$=txtName]").val() == "") {
                $("#error").removeClass("hidden");
                $("#nameError").removeClass("hidden");
                error_fl = true;
            }
            if ($("textarea[id$=txtAbstract]").val() == "") {
                $("#error").removeClass("hidden");
                $("#abstractError").removeClass("hidden");
                error_fl = true;
            }
            return !error_fl;
        }
        function checkResearcher() {
            $("#lblNoResearcherChosen").addClass("hidden");
            if ($("select[id$=ddlResearcher]").val() == "-1") {
                $("#lblNoResearcherChosen").removeClass("hidden");
                return false;
            }
        }
    </script>
   
    
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblNoResults" CssClass="alert alert-danger"  Visible="false" runat="server"></asp:Label>
    <asp:HiddenField ID="hdnResearcher" runat="server" />
    <asp:Panel ID="view" runat="server">
        <div class="alert alert-info">
        Name: <asp:Label ID="lblName" runat="server"></asp:Label>
            </div>
         <div class="alert alert-info">
        Publication Date: <asp:Label ID="lblDate" runat="server"></asp:Label>
         </div>
        <div class="alert alert-info">
        Abstract: <asp:Label ID="lblAbstract" runat="server"></asp:Label>
         </div>
    </asp:Panel>
    <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click" runat="server" />
    <asp:Button ID="btnDelete" Text="Delete" CssClass="btn btn-primary" OnClientClick="return confirmDelete()" OnClick="btnDelete_Click"  runat="server" />
    <asp:Panel ID="edit" Visible="false" runat="server">
        Edit:
        <div class="">
        Name: <asp:Textbox ID="txtName" runat="server"></asp:Textbox>
            </div>
        <div class="row">
        <span class="col-md-1">Publish Date: </span> <asp:Textbox type="date" ID="txtDatePublish" CssClass="col-md-2" runat="server"></asp:Textbox>
            <div class="col-md-5" id="pnlPublishDate">
            Current Date: <asp:Label ID="lblDatePublish" runat="server"></asp:Label></div>
            </div>
         <div class="">
        Abstract: <asp:TextBox ID="txtAbstract" TextMode="MultiLine" runat="server"></asp:TextBox>
         </div>
        <div class="">
        <asp:Button ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return checkResearch();" runat="server" />
         </div>
        <br />
        <br  />
        <br />
        <div id="error" class="hidden mt-4">
            <div id="nameError" class="hidden alert alert-danger my">No Research Name</div>
            <div id="abstractError" class="hidden alert alert-danger my">No Research Abstract</div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
    <asp:Panel ID="pnlResearchers" runat="server">
        Add Researcher: <asp:DropDownList ID="ddlResearcher" runat="server"></asp:DropDownList>
        <asp:Button ID="btnSave_Researcher" OnClick="btnSave_Researcher_Click" OnClientClick="return checkResearcher();"  CssClass="btn btn-primary" Text="Save Researcher" runat="server" />
        <br />
        <br />
        <div class="hidden alert alert-danger" id="lblNoResearcherChosen">No Researcher Chosen</div>
        <br />
        <asp:Table ID="tblResearchers" CssClass="table table-striped" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Picture</asp:TableHeaderCell>
            <asp:TableHeaderCell>Abstract</asp:TableHeaderCell>
            <asp:TableHeaderCell>Delete</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
         <asp:Label ID="lblNoResearchs" CssClass="alert alert-danger" Text="There are no researchers for this Research" Visible="false" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>
