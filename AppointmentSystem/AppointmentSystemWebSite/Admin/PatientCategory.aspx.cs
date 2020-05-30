using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PatientCategory : System.Web.UI.Page
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
            sqlp.Add(new SqlParameter("@TableName", "PatientCategoryMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "CategoryId"));
            sqlp.Add(new SqlParameter("@TableId", id));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

            DataTable dt = new DataTable();
            dt = conc.DisplayUserData(sqlp).Tables[0];

            if (dt.Rows.Count == 0) { call_On_Cancel(); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["CategoryName"] = dr["CategoryName"].ToString();
                    txtPatientCategory.Text = dr["CategoryName"].ToString();
                    txtColor.Value = dr["CategoryColor"].ToString();
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            call_On_Cancel();
        }

        if (!IsPostBack)
        {
            txtPatientCategory.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "PatientCategoryMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId",Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td width='72%'>" + drUserInfo["CategoryName"] + "</td>");
            html.Append("<td width='4%' style='background-color:" + drUserInfo["CategoryColor"] + "'></td>");
            html.Append("<td align='center' width='12%'><a href='PatientCategory.aspx?fid=" + drUserInfo["CategoryId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='12%'><a href='Javascript:deletefunction(" + drUserInfo["CategoryId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayRec.InnerHtml = html.ToString();
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                ConnectionClass conCheckDuplicate = new ConnectionClass("MasterDuplicateCheck");
                List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
                sqlpCheckDuplicate.Add(new SqlParameter("@TableName", "PatientCategoryMaster"));
                sqlpCheckDuplicate.Add(new SqlParameter("@FieldName", "CategoryName"));
                sqlpCheckDuplicate.Add(new SqlParameter("@CheckValue", txtPatientCategory.Text.ToString().ToUpper()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    submit_data();
                }
                else
                {

                    if (ViewState["CategoryName"] == null)
                    {
                        lblCheckDuplicate.Text = "Data Exist If Not Found In List Check Deleted Data And Restore It.";
                    }
                    else
                    {
                        if (ViewState["CategoryName"].ToString() == txtPatientCategory.Text.ToString())
                        {
                            submit_data();
                        }
                        else
                        {
                            lblCheckDuplicate.Text = "Data Exist If Not Found In List Check Deleted Data And Restore It.";

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
            ConnectionClass conAdd = new ConnectionClass("CAdminPatientCategoryMaster");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@CategoryId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@CategoryCode", congetMax.GetMaxTableCode("PatientCategoryMaster", "CategoryCode")));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@CategoryName", txtPatientCategory.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@CategoryColor", txtColor.Value.ToString()));
            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("PatientCategory.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("CAdminPatientCategoryMasterUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            string id = Session["fid"].ToString();
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@CategoryId", id));
            sqlp.Add(new SqlParameter("@CategoryName", txtPatientCategory.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@CategoryColor", txtColor.Value.ToString()));
            sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
            bool i2 = conUpd.SaveData(sqlp);
            Session.Remove("fid");
            if (i2 == true)
            {
                Response.Redirect("PatientCategory.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        call_On_Cancel();
    }

    protected void call_On_Cancel()
    {
        Session.Remove("fid");
        Response.Redirect("PatientCategory.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeletePatient(string pid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "CategoryId"));
        sqlp.Add(new SqlParameter("@TableName", "PatientCategoryMaster"));
        sqlp.Add(new SqlParameter("@TableId", pid));
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