<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Researcher_Specific.aspx.cs" Inherits="Research_Project.Researcher_Specific" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .hidden {display:none;}
    </style>
    <script type="text/javascript">
        function checkResearcher() {
            error_fl = false;
            $(".my").addClass("hidden");
            if ($("input[id$=txtFirstName]").val() == "") {
                $("#error").removeClass("hidden");
                $("#firstnameError").removeClass("hidden");
                error_fl = true;
            }
            if ($("input[id$=txtLastName]").val() == "") {
                $("#error").removeClass("hidden");
                $("#lastnameError").removeClass("hidden");
                error_fl = true;
            }
            if ($("textarea[id$=txtAbstract1]").val() == "") {
                $("#error").removeClass("hidden");
                $("#abstractError").removeClass("hidden");
                error_fl = true;
            }
            return !error_fl;
        }
        function checkResearch() {
            $("#lblNoResearchChosen").addClass("hidden");
            if ($("select[id$=ddlResearch]").val() == "-1") {
                $("#lblNoResearchChosen").removeClass("hidden");
                return false;
            }
        }
        function confirmDelete() {
            return confirm("Are you sure you want delete this researcher?");
        }
        $(document).ready(function () {
        });
        function onResearchDelete(index) {
            if (confirm("Are you sure you want delete this research?"))
                {
                $("input[id$=hdnResearch]").val(index);
                return true;
            }
            else return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnResearch" runat="server" />
    <asp:Label ID="lblNoResults" CssClass="alert alert-danger" Text="No Researcher Selected" Visible="false" runat="server"></asp:Label>
    <asp:Panel ID="view" runat="server">
        <div class="alert alert-info">
        Name: <asp:Label ID="lblName" runat="server"></asp:Label>
            </div>
         <div class="alert alert-info">
        Abstract: <asp:Label ID="lblAbstract" runat="server"></asp:Label>
         </div>
        <div class="alert alert-info">
        Picture: <asp:Image ID="imgPicture" runat="server"></asp:Image>
         </div>
    </asp:Panel>
    <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click" runat="server" />
    <asp:Button ID="btnDelete" Text="Delete" CssClass="btn btn-primary" OnClientClick="return confirmDelete()" OnClick="btnDelete_Click"  runat="server" />
    <asp:Panel ID="edit" Visible="false" runat="server">
        Edit:
        <div class="">
        First Name: <asp:Textbox ID="txtFirstName" runat="server"></asp:Textbox>
            </div>
        <div class="">
        Last Name: <asp:Textbox ID="txtLastName" runat="server"></asp:Textbox>
            </div>
         <div class="">
        Abstract: <asp:TextBox ID="txtAbstract1" TextMode="MultiLine" runat="server"></asp:TextBox>
         </div>
        <div class="row">
        Picture: <asp:FileUpload ID="flPicture" runat="server"></asp:FileUpload>
          Current Picture:   <asp:Panel ID="pnlPicture" CssClass="col-md-5" runat="server">
            </asp:Panel>
            
         </div>
        <div class="">
        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClientClick="return checkResearcher();" OnClick="btnSave_Click" runat="server" />
         </div>
        <br />
        <br  />
        <br />
        <div id="error" class="hidden mt-4">
            <div id="firstnameError" class="hidden alert alert-danger my">No Research First Name</div>
            <div id="lastnameError" class="hidden alert alert-danger my">No Research Last Name</div>
            <div id="abstractError" class="hidden alert alert-danger my">No Research Abstract</div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
    <asp:Panel ID="pnlResearchs" runat="server">
        Add Research: <asp:DropDownList ID="ddlResearch" runat="server"></asp:DropDownList>
        <asp:Button ID="btnSave_Research" OnClick="btnSave_Research_Click" OnClientClick="return checkResearch();" Text="Save Research" runat="server" />
        <br />
        <br />
        <div class="hidden alert alert-danger" id="lblNoResearchChosen">No Research Chosen</div>
        <br />
        <asp:Table ID="tblResearchs" CssClass="table table-striped" runat="server">
        <asp:TableHeaderRow>
            
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Date</asp:TableHeaderCell>
            <asp:TableHeaderCell>Abstract</asp:TableHeaderCell>
            <asp:TableHeaderCell>Delete</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
         <asp:Label ID="lblNoResearchs" CssClass="alert alert-danger" Text="No Researches for this Researcher" Visible="false" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>
