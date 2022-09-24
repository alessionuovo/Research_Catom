using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project
{
    public partial class Researcher : System.Web.UI.Page
    {
        public void FillResearchers()
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("Select * from Researcher_N", cnn);
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
                        tc.Text = reader[3].ToString();
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        l = new Literal();
                        l.Text = string.Format("<img src='{0}'>", reader[4]);
                        tc.Controls.Add(l);
                        tr.Cells.Add(tc);
                        tblResearchers.Rows.Add(tr);
                        if (tblResearchers.Rows.Count == 1)
                        {
                            tblResearchers.Visible = false;
                            lblNoResearchers.Visible = true;
                            lblNoResearchers.Text = "No Researches found";
                        }
                    }
                }
            } catch (Exception e)
            {
                tblResearchers.Visible = false;
                lblNoResearchers.Visible = true;
                lblNoResearchers.Text = "Error displaying researches";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FillResearchers();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            String cnnStr = System.Configuration.ConfigurationManager.
    ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(cnnStr);
                cnn.Open();
                SqlCommand command = new SqlCommand("insert into Researcher_N(FirstName,LastName, Abstract, Picture) values(@FirstName,@LastName, @Abstract, @Picture )", cnn);
                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command.Parameters.AddWithValue("@Abstract", txtAbstract.Text);
                string picture = string.Format("images/{0}", flPicture.FileName);
                if (!string.IsNullOrEmpty(flPicture.FileName))
                {
                    string path = Path.Combine(Server.MapPath("images"), flPicture.FileName);
                    flPicture.SaveAs(path);

                }
                else picture = "";
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
    }
}