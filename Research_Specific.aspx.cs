using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project
{
    public partial class Research_Specific : System.Web.UI.Page
    {
        public void FillResearch()
        {

            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                bool wasResults = false;
                SqlCommand command = new SqlCommand(string.Format("Select * from Research_N where IDResearch={0}", Request.QueryString["research"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wasResults = true;
                        lblName.Text = reader[1].ToString();
                        lblDate.Text = reader[3].ToString();
                        lblAbstract.Text = reader[2].ToString();
                        lblDatePublish.Text = reader[3].ToString();

                    }
                    if (!wasResults)
                    {
                        lblNoResults.Text = "Research not exist";
                        lblNoResults.Visible = true;
                        view.Visible = false;
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        pnlResearchers.Visible = false;
                        return;
                    }
                }
            } catch (Exception e)
            {
                lblResult.Text = "Error displaying researches";
                lblResult.Visible = true;
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
                ListItem li = new ListItem();
                li.Text = "Select";
                li.Value = "-1";
                ddlResearcher.Items.Add(li);
                SqlCommand command = new SqlCommand(string.Format("Select a.IDResearcher, FirstName, LastName,  Picture, Abstract from Researcher_N a join Connection b on a.IDResearcher=b.IDResearcher and b.IDResearch={0} order by [Order]", Request.QueryString["research"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableRow tr = new TableRow();

                        TableCell tc = new TableCell();
                        Literal l = new Literal();
                        l.Text = string.Format("<a href='Researcher_Specific.aspx?researcher={0}'>{1} {2}</a>", reader[0], reader[1], reader[2]);
                        tc.Controls.Add(l);
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        l = new Literal();
                        l.Text = string.Format("<img src='{0}'>", reader[3]);
                        tc.Controls.Add(l);
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = reader[4].ToString();
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        Button btn = new Button();
                        btn.CssClass = "btn btn-danger";
                        btn.Text = "Delete";
                        btn.OnClientClick = string.Format("return onResearcherDelete({0})", reader[0]);
                        btn.Click += btnDeleteResearcher_Click;
                        tc.Controls.Add(btn);
                        tr.Cells.Add(tc);
                        tblResearchers.Rows.Add(tr);
                    }
                    if (tblResearchers.Rows.Count == 1)
                    {
                        tblResearchers.Visible = false;
                        lblNoResearchs.Visible = true;
                    }
                }
            } catch (Exception e)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in displaying researchers";
            }
        }

        

        public void FillResearchers()
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
                ddlResearcher.Items.Add(li);
                SqlCommand command = new SqlCommand(string.Format("Select IDResearcher, FirstName, LastName from Researcher_N where IDResearcher not in (select IDResearcher from Connection where  IDResearch={0})", Request.QueryString["research"]), cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        li = new ListItem();
                        li.Text = string.Format("{0} {1}", reader[1].ToString(), reader[2].ToString());
                        li.Value = reader[0].ToString();
                        ddlResearcher.Items.Add(li);
                    }
                }
            } catch(Exception e)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in displaying researchers";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["research"]))
            {
                lblNoResults.Text = "No Research selected";
                lblNoResults.Visible = true;
                view.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                pnlResearchers.Visible = false;
                return;
            }
            FillResearch();
            FillResearchers();
            FillConnections();

        }
        protected  void Page_PreRender(object sender, EventArgs e)
        {
            txtName.Text = lblName.Text;
            txtDatePublish.Text = lblDate.Text;
            txtAbstract.Text = lblAbstract.Text;
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
                SqlCommand command = new SqlCommand("delete from Research_N where  IDResearch=@IDResearch", cnn);
                command.Parameters.AddWithValue("@IDResearch", Request.QueryString["research"]);
                command = new SqlCommand("delete from Connection where  IDResearch=@IDResearch", cnn);
                command.Parameters.AddWithValue("@IDResearch", Request.QueryString["research"]);
                int result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ee)
            {
                lblResult.Visible = true;
                lblResult.Text = "Error in deleting research" ;
                lblResult.CssClass = "alert alert-danger";
            }
        }
        private void btnDeleteResearcher_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("delete from Connection where  IDResearch=@IDResearch and IDResearcher=@IDResearcher", cnn);
                command.Parameters.AddWithValue("@IDResearch", Request.QueryString["research"]);
                command.Parameters.AddWithValue("@IDResearcher", hdnResearcher.Value);
                command.Parameters.AddWithValue("@Abstract", txtAbstract.Text);
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
                SqlCommand command = new SqlCommand(string.Format("update Research_N set Name=@Name, DatePublish=@DatePublish, Abstract=@Abstract  where IDResearch={0}", Request.QueryString["research"]), cnn);
                command.Parameters.AddWithValue("@Name", txtName.Text);
                if (!string.IsNullOrEmpty(txtDatePublish.Text))
                {
                    command.Parameters.AddWithValue("@DatePublish", DateTime.Parse(txtDatePublish.Text));
                }
                else command.Parameters.AddWithValue("@DatePublish", DateTime.Parse(lblDatePublish.Text));
                command.Parameters.AddWithValue("@Abstract", txtAbstract.Text);
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

        protected void btnSave_Researcher_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn1 = new SqlConnection(cnnStr);
                cnn1.Open();
                SqlCommand command = new SqlCommand(string.Format("select count(*) as a from Connection where IDResearch={0}", Request.QueryString["research"]), cnn1);
                object count = command.ExecuteScalar();
                Int32 Total_Records = System.Convert.ToInt32(count);
                Total_Records++;
                command = new SqlCommand("insert into Connection(IDResearch,IDResearcher, [Order]) values(@IDResearch,@IDResearcher, @Order)", cnn1);
                command.Parameters.AddWithValue("@IDResearch", Request.QueryString["research"]);
                command.Parameters.AddWithValue("@IDResearcher", ddlResearcher.SelectedValue);
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