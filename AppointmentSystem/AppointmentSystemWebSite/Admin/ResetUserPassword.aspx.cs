using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ResetUserPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }

        if (!IsPostBack)
        {
            fillTable();
        }
    }

    protected void fillTable()
    {
        ConnectionClass conUserInfo = new ConnectionClass("DisplayUserByRole");
        List<SqlParameter> sqlUserInfo = new List<SqlParameter>();
        sqlUserInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlUserInfo.Add(new SqlParameter("@CoRole", "RECPTION"));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conUserInfo.DisplayUserData(sqlUserInfo).Tables[0];

        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            cmbUserId.Items.Add(new ListItem(drUserInfo["CoUserLoginId"].ToString(), drUserInfo["CoLoginId"].ToString()));
        }

        List<SqlParameter> sqlUserDoc = new List<SqlParameter>();
        sqlUserDoc.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlUserDoc.Add(new SqlParameter("@CoRole", "DOCTOR"));
        DataTable dtUserDoc = new DataTable();
        dtUserDoc = conUserInfo.DisplayUserData(sqlUserDoc).Tables[0];

        foreach (DataRow drUserInfo in dtUserDoc.Rows)
        {
            cmbUserId.Items.Add(new ListItem(drUserInfo["CoUserLoginId"].ToString(), drUserInfo["CoLoginId"].ToString()));
        }    
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ConnectionClass conPassUpdate = new ConnectionClass("UpdateUserPassword");
                List<SqlParameter> sqlpPassUpdate = new List<SqlParameter>();
                sqlpPassUpdate.Add(new SqlParameter("@CoLoginId", cmbUserId.SelectedValue.ToString()));
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
        }
        catch (Exception) { }
    }
}