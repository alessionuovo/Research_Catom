using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project
{
    public partial class Research : System.Web.UI.Page
    {
        public void FillResearchs()
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("Select * from Research_N", cnn);
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
                        tc.Text = reader[3].ToString();
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = reader[2].ToString();
                        tr.Cells.Add(tc);
                        tblResearchs.Rows.Add(tr);
                    }
                    if (tblResearchs.Rows.Count == 1)
                    {
                        tblResearchs.Visible = false;
                        lblNoResearchs.Visible = true;
                        lblNoResearchs.Text = "No Researches found";
                    }
                }
            } catch (Exception e)
            {
                tblResearchs.Visible = false;
                lblNoResearchs.Visible = true;
                lblNoResearchs.Text = "Error displaying researches";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FillResearchs();
        }
            protected void btnSave_Click(object sender, EventArgs e)
        {
            lblResult.Visible = true;
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("insert into Research_N(Name,DatePublish, Abstract) values(@Name,@PublishDate, @Abstract)", cnn);
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@PublishDate", DateTime.Parse(txtDatePublish.Text));
                command.Parameters.AddWithValue("@Abstract", txtAbstract.Text);
                int result = command.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            } catch (Exception ee)
            {
                lblResult.Text = "Error in save";
                lblResult.CssClass = "alert alert-danger";
            }
        }
    }
}