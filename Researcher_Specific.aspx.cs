using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project
{
    public partial class Researcher_Specific : System.Web.UI.Page
    {
        public void FillResearcher()
        {
            
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand(string.Format("Select * from Researcher_N where IDResearcher={0}", Request.QueryString["researcher"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool wasResults = false;
                    while (reader.Read())
                    {

                        lblName.Text = string.Format("{0} {1}", reader[1], reader[2]);
                        lblAbstract.Text = reader[3].ToString();
                        Literal l = new Literal();
                        imgPicture.ImageUrl = reader[4].ToString();
                        l.Text = string.Format("<img src='{0}'>", reader[4].ToString());
                        pnlPicture.Controls.Add(l);
                        pnlPicture.Attributes["my_src"] = reader[4].ToString();
                        wasResults = true;
                    }
                    if (!wasResults)
                    {
                        lblNoResults.Text = "Researcher not exist";
                        lblNoResults.Visible = true;
                        view.Visible = false;
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        pnlResearchs.Visible = false;
                        return;
                    }
                }
            } catch (Exception e)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in displaying researchers";
            }
        }
        public void FillConnections()
        {
            String cnnStr = System.Configuration.ConfigurationManager.
  ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand(string.Format("Select a.IDResearch, Name, DatePublish, Abstract from Research_N a join Connection b on a.IDResearch=b.IDResearch and b.IDResearcher={0}", Request.QueryString["researcher"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableRow tr = new TableRow();
                        TableCell tc = new TableCell();
                        Literal l = new Literal();
                        l.Text = string.Format("<a href='Research_Specific.aspx?research={0}'>{1} </a>", reader[0], reader[1]);
                        tc.Controls.Add(l);
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = reader[2].ToString();
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = reader[3].ToString();
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        Button btn = new Button();
                        btn.CssClass = "btn btn-danger";
                        btn.Text = "Delete";
                        btn.OnClientClick = string.Format("return onResearchDelete({0})", reader[0]);
                        btn.Click += btnDeleteResearch_Click;
                        tc.Controls.Add(btn);
                        tr.Cells.Add(tc);
                        tblResearchs.Rows.Add(tr);
                    }
                    if (tblResearchs.Rows.Count == 1)
                    {
                        tblResearchs.Visible = false;
                        lblNoResearchs.Visible = true;
                    }
                }
            } catch (Exception e)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in displaying researches";
            }
    }

        

        public void FillResearchs()
        {
            String cnnStr = System.Configuration.ConfigurationManager.
  ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                ListItem li = new ListItem();
                li.Text = "Select";
                li.Value = "-1";
                ddlResearch.Items.Add(li);
                SqlCommand command = new SqlCommand(string.Format("Select IDResearch, Name from Research_N where IDResearch not in (select IDResearch from Connection where  IDResearcher={0})", Request.QueryString["researcher"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        li = new ListItem();
                        li.Text = reader[1].ToString();
                        li.Value = reader[0].ToString();
                        ddlResearch.Items.Add(li);
                    }
                }
            } catch (Exception e)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in displaying researches";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["researcher"]))
            {
                lblNoResults.Visible = true;
                view.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                pnlResearchs.Visible = false;
                return;
            }
            FillResearcher();
            FillResearchs();
            FillConnections();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            String [] parts = lblName.Text.Split();
            txtFirstName.Text = parts[0];
            if (parts.Length >= 2)
                txtLastName.Text = parts[1];
            txtAbstract1.Text = lblAbstract.Text;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            view.Visible = false;
            edit.Visible = true;
            btnEdit.Visible = false;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("delete from Researcher_N where  IDResearcher=@IDResearcher", cnn);
                command.Parameters.AddWithValue("@IDResearcher", Request.QueryString["researcher"]);
                int result = command.ExecuteNonQuery();
                command = new SqlCommand("delete from Connection where  IDResearcher=@IDResearcher", cnn);
                command.Parameters.AddWithValue("@IDResearcher", Request.QueryString["researcher"]);
                result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ee)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in deleting researcher" ;
                lblResult.CssClass = "alert alert-danger";
            }
        }
        private void btnDeleteResearch_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("delete from Connection where  IDResearch=@IDResearch and IDResearcher=@IDResearcher", cnn);
                command.Parameters.AddWithValue("@IDResearch", hdnResearch.Value);
                command.Parameters.AddWithValue("@IDResearcher", Request.QueryString["researcher"]);

                int result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ee)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in deleting researcher" ;
                lblResult.CssClass = "alert alert-danger";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand(string.Format("update Researcher_N set FirstName=@FirstName, LastName=@LastName, Abstract=@Abstract, Picture=@Picture where IDResearcher={0}", Request.QueryString["researcher"]), cnn);
                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command.Parameters.AddWithValue("@Abstract", txtAbstract1.Text);
                string picture = string.Format("images/{0}", flPicture.FileName);
                if (!string.IsNullOrEmpty(flPicture.FileName))
                {
                    string path = Path.Combine(Server.MapPath("images"), flPicture.FileName);
                    flPicture.SaveAs(path);
                }
                else picture = pnlPicture.Attributes["my_src"];
                command.Parameters.AddWithValue("@Picture", picture);
                int result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ee)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in save" ;
                lblResult.CssClass = "alert alert-danger";
            }
        }

        protected void btnSave_Research_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn1 = new SqlConnection(cnnStr);
                cnn1.Open();
                SqlCommand command = new SqlCommand(string.Format("select count(*) as a from Connection where IDResearch={0}", ddlResearch.SelectedValue), cnn1);
                object count = command.ExecuteScalar();
                Int32 Total_Records = System.Convert.ToInt32(count);
                Total_Records++;
                 command = new SqlCommand("insert into Connection(IDResearch,IDResearcher, [Order]) values(@IDResearch,@IDResearcher, @Order)", cnn1);
                command.Parameters.AddWithValue("@IDResearch", ddlResearch.SelectedValue);
                command.Parameters.AddWithValue("@IDResearcher", Request.QueryString["researcher"]);
                command.Parameters.AddWithValue("@Order", Total_Records);
                int result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ee)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in save" ;
                lblResult.CssClass = "alert alert-danger";
            }
        }
    } 
}
   