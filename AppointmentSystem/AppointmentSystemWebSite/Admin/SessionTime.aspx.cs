using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SessionTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }

        if (!IsPostBack)
        {
            txtSession.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "SessionManage"));
        sqlPInfo.Add(new SqlParameter("@CompanyId",Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            txtSession.Text= drUserInfo["SessionValue"].ToString();          
            break;
        }
    }
    

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                ConnectionClass conUpd = new ConnectionClass("SessionTimeUpdate");
                ConnectionClass congetMax = new ConnectionClass();

                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@SessionValue", txtSession.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));

                bool i2 = conUpd.SaveData(sqlp);
                if (i2 == true)
                {
                    string message = "Session Updated Successfully.";
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