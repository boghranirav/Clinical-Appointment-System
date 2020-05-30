using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_ViewPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                fillTable();
            }
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "PatientMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["PatientName"] + "</td>");
            html.Append("<td width='15%'>" + drUserInfo["PatientMobile"] + "</td>");
            html.Append("<td width='15%'>" + drUserInfo["PatientGender"] + "</td>");
            html.Append("<td width='10%'>" + drUserInfo["PatientAge"] + "</td>");
            html.Append("<td align='center'  width='10%' ><a href='PatientMaster.aspx?fid=" + drUserInfo["PatientId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("</tr>");
        }
        displayPatient.InnerHtml = html.ToString();
    }
}