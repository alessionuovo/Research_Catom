<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Researcher.aspx.cs" Inherits="Research_Project.Researcher" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
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
    <asp:Table ID="tblResearchers" CssClass="table table-striped" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Abstract</asp:TableHeaderCell>
            <asp:TableHeaderCell>Picture</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Label ID="lblNoResearchers" runat="server" Visible="false" CssClass="alert alert-danger"> </asp:Label>
     Add Researcher: <br />
    <div class="row">
     <div class="col-md-2"> First Name:</div>    
     <div class="col-md-2"> <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox></div>
        <div class="col-md-2"> Last Name:</div>    
     <div class="col-md-2"> <asp:TextBox ID="txtLastName" CssClass="form-control"  runat="server"></asp:TextBox></div>
        <div class="col-md-2"> Abstract:</div> 
    
     <div class="col-md-2"> <asp:TextBox ID="txtAbstract" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
     </div>
     <div class="row">
         <div class="offset-md-4 col-md-1">
             Picture:
             </div>
         <div class="col-md-2"> 
             <asp:FileUpload ID="flPicture" runat="server" Text=" Upload" />
         </div>
         </div>
            <div class="row">

             <div class="offset-md-5">   
             <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary" OnClientClick="return checkResearcher();" OnClick="btnSave_Click" />
               </div>
                   </div>
         </div>
        <br />
        <br />
        <br />
        <div id="error" class="hidden mt-4">
            <div id="firstnameError" class="hidden alert alert-danger my">Fill Researcher's First Name</div>
            <div id="lastnameError" class="hidden alert alert-danger my">Fill Researcher's Last Name</div>
            <div id="abstractError" class="hidden alert alert-danger my">Fill Researcher's Abstract</div>
            <div id="uploadError" class="hidden alert alert-danger my">Upload Researcher's Picture</div>
        </div>
     
        <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
</asp:Content>
