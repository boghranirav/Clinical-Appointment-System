using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_CompanyDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        if (!IsPostBack)
        {
            fillTable();
        }
        
    }

   protected void fillTable()
    {
        ConnectionClass sp3 = new ConnectionClass("displayData");

        DataTable dt = new DataTable();
        dt = sp3.DisplayData("CompanyMaster").Tables[0];
        StringBuilder html = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            html.Append("<tr>");
            html.Append("<td>" + dr["CompanyCode"] + "</td>");
            html.Append("<td>" + dr["CompanyName"] + "</td>");
            html.Append("<td>" + dr["CompanyAddress"] + "</td>");
            html.Append("<td>" + dr["CompanyStatus"] + "</td>");
            html.Append("<td>" + dr["CompanyCreateDate"].ToString().Trim() + "</td>");
            html.Append("<td>" + dr["CompanyEmail"] + "</td>");
            html.Append("<td align='center' width='8%'><a href='CompanyMaster.aspx?fid=" + dr["CompanyId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='4%'><a href='DoctorMaster.aspx?fid=" + dr["CompanyId"] + "'>Add Doctor</a></td>");
            html.Append("</tr>");
        }
        displayCompany.InnerHtml = html.ToString();
    }

   
}