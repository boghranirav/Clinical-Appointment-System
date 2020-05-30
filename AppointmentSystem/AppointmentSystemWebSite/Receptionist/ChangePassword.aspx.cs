using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }

        if (!IsPostBack)
        {

        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            OldPassword_Match();
            if (ViewState["OldPass"].ToString().Equals("True"))
            {
                try
                {
                    ConnectionClass conPassUpdate = new ConnectionClass("UpdateUserPassword");
                    List<SqlParameter> sqlpPassUpdate = new List<SqlParameter>();
                    sqlpPassUpdate.Add(new SqlParameter("@CoLoginId", Session["CoLoginId"].ToString()));
                    sqlpPassUpdate.Add(new SqlParameter("@CoUserPassword", txtNewPassword.Text.ToString()));
                    sqlpPassUpdate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                    sqlpPassUpdate.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));

                    bool i = conPassUpdate.SaveData(sqlpPassUpdate);

                    if (i == true)
                    {
                        string message = "Password Updated Successfully.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                }
                catch (Exception) { }

            }
        }
    }
  
    protected void OldPassword_Match()
    {
        try
        {
            ConnectionClass conCheckDuplicate = new ConnectionClass("MatchOldPassword");
            List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
            string sid = Session["CoLoginId"].ToString();
            string cid = Session["companyid"].ToString();
            sqlpCheckDuplicate.Add(new SqlParameter("@CoLoginId",sid));
            sqlpCheckDuplicate.Add(new SqlParameter("@CoUserPassword", txtOldPassword.Text.ToString()));
            sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", cid));


            int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
            if (i == 0)
            {
                ViewState["OldPass"] = "False";
                lblOldPass.Text = "Old Password Does Not Match.";
                txtOldPassword.Focus();
            }
            else
            {
                ViewState["OldPass"] = "True";
            }
        }
        catch (Exception) { }
    }
    protected void txtOldPassword_TextChanged(object sender, EventArgs e)
    {
        lblOldPass.Text = "";
    }
}