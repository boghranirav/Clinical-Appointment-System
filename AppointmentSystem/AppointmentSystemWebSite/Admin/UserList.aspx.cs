using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserList : System.Web.UI.Page
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

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            string onlinestatus = "";
            if (drUserInfo["IsOnline"].ToString().Equals("True"))
            { onlinestatus = "Online"; }
            else { onlinestatus = "Offline"; }

            html.Append("<tr>");
            html.Append("<td>RECEPTIONIST</td>");
            html.Append("<td>" + drUserInfo["CoFirstName"] + " " + drUserInfo["CoMiddleName"] + " " + drUserInfo["CoLastName"] + "</td>");
            html.Append("<td>" + drUserInfo["CoUserLoginId"] + "</td>");
            html.Append("<td>" + onlinestatus + "</td>");
            html.Append("<td>" + drUserInfo["LoginDate"] + "</td>");
            html.Append("</tr>");
        }

        List<SqlParameter> sqlUserDoc = new List<SqlParameter>();
        sqlUserDoc.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlUserDoc.Add(new SqlParameter("@CoRole", "DOCTOR"));
        DataTable dtUserDoc = new DataTable();
        dtUserDoc = conUserInfo.DisplayUserData(sqlUserDoc).Tables[0];
        
        foreach (DataRow drUserInfo in dtUserDoc.Rows)
        {
            string onlinestatus = "";
            if (drUserInfo["IsOnline"].ToString().Equals("True"))
            { onlinestatus = "Online"; }
            else { onlinestatus = "Offline"; }

            html.Append("<tr>");
            html.Append("<td>" + drUserInfo["CoRole"] + "</td>");
            html.Append("<td>" + drUserInfo["CoSalute"] + " " + drUserInfo["CoFirstName"] + " " + drUserInfo["CoMiddleName"] + " " + drUserInfo["CoLastName"] + "</td>");
            html.Append("<td>" + drUserInfo["CoUserLoginId"] + "</td>");
            html.Append("<td>" + onlinestatus + "</td>");
            html.Append("<td>" + drUserInfo["LoginDate"] + "</td>");
            html.Append("</tr>");
        }

        displayDoc.InnerHtml = html.ToString();
    }
}