using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionest_AreaMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }

        if (Request.QueryString.AllKeys.Contains("fid"))
        {
            if (!Request.QueryString["fid"].ToString().All(char.IsDigit))
            {
                call_On_Cancel();
            }

            btnCancel.Visible = true;
            submit.Text = "Update";

            string id = Request.QueryString["fid"];

            if (id == null)
            {
                call_On_Cancel();
            }
            Session["fid"] = Request.QueryString["fid"];
            ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "AreaMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "AreaId"));
            sqlp.Add(new SqlParameter("@TableId", id));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

            DataTable dt = new DataTable();
            dt = conc.DisplayUserData(sqlp).Tables[0];

            if (dt.Rows.Count == 0) { call_On_Cancel(); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["AreaName"] = dr["AreaName"].ToString();
                    txtArea.Text = dr["AreaName"].ToString();
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            call_On_Cancel();
        }

        if (!IsPostBack)
        {
          //  ConnectionClass conLogIn = new ConnectionClass("SetUserOnline");
          //  List<SqlParameter> sqlpLogIn = new List<SqlParameter>();
          //  sqlpLogIn.Add(new SqlParameter("@SetStatus", "1"));
          //  sqlpLogIn.Add(new SqlParameter("@CoLoginId", Request.Cookies["CookieLoginUserId"].Value.ToString()));
          //  conLogIn.SaveData(sqlpLogIn);

            txtArea.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "AreaMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["AreaName"] + "</td>");
            html.Append("<td align='center' width='15%'><a href='AreaMaster.aspx?fid=" + drUserInfo["AreaId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='15%'><a href='Javascript:deletefunction(" + drUserInfo["AreaId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayArea.InnerHtml = html.ToString();
    }

   

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                ConnectionClass conCheckDuplicate = new ConnectionClass("MasterDuplicateCheck");
                List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
                sqlpCheckDuplicate.Add(new SqlParameter("@TableName", "AreaMaster"));
                sqlpCheckDuplicate.Add(new SqlParameter("@FieldName", "AreaName"));
                sqlpCheckDuplicate.Add(new SqlParameter("@CheckValue", txtArea.Text.ToString().ToUpper()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    submit_data();
                }
                else
                {

                    if (ViewState["AreaName"] == null)
                    {
                        lblDuplicate.Text = "*Area Exist If Not Found In List Check Deleted Data And Restore It.";
                    }
                    else
                    {
                        if (ViewState["AreaName"].ToString() == txtArea.Text.ToString())
                        {
                            submit_data();
                        }
                        else
                        {
                            lblDuplicate.Text = "*Area Exist If Not Found In List Check Deleted Data And Restore It.";
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    protected void submit_data()
    {
        if (submit.Text == "Submit")
        {
            ConnectionClass conAdd = new ConnectionClass("RecAreaMaterAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@AreaId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@AreaCode", congetMax.GetMaxTableCode("AreaMaster", "AreaCode")));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@AreaName", txtArea.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
               // fillTable();
                Response.Redirect("AreaMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("RecAreaMaterUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            string id = Session["fid"].ToString();
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@AreaId", id));
            sqlp.Add(new SqlParameter("@AreaName", txtArea.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
            bool i2 = conUpd.SaveData(sqlp);
            Session.Remove("fid");
            if (i2 == true)
            {
                Response.Redirect("AreaMaster.aspx");
            }
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        call_On_Cancel();
    }

    protected void call_On_Cancel()
    {
        Session.Remove("fid");
        Response.Redirect("AreaMaster.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteArea(string aid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "AreaId"));
        sqlp.Add(new SqlParameter("@TableName", "AreaMaster"));
        sqlp.Add(new SqlParameter("@TableId", aid));
        sqlp.Add(new SqlParameter("@DeleteId", userid));

        bool i = con.DeleteAdminData(sqlp);
        if (i == true)
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }
        return retval;
    }
}