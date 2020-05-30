using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_UtilityMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {

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
                sqlp.Add(new SqlParameter("@TableName", "UtilityMaster"));
                sqlp.Add(new SqlParameter("@FieldName", "UtilityId"));
                sqlp.Add(new SqlParameter("@TableId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                DataTable dt = new DataTable();
                dt = conc.DisplayUserData(sqlp).Tables[0];

                if (dt.Rows.Count == 0) { call_On_Cancel(); }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // txtName.Text = dr["RefName"].ToString();
                        ViewState["UtilityName"] = dr["UtilityName"].ToString();
                        txtName.Text = dr["UtilityName"].ToString(); ;
                        txtAddress.Text = dr["UtilityAdress"].ToString(); ;
                        txtEmail.Text = dr["UtilityEmail"].ToString(); ;
                        txtMobile.Text = dr["UtilityContact"].ToString(); ;
                        txtRemark.Text = dr["UtilityRemark"].ToString(); ;
                       // txtAdultDose.Text = dr["AdultDose"].ToString();

                    }
                }
            }
            else if (Request.QueryString.AllKeys.Length > 0)
            {
                call_On_Cancel();
            }

            if (!IsPostBack)
            {
                txtName.Focus();
                fillTable();
            }
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "UtilityMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["UtilityName"] + "</td>");
            html.Append("<td >" + drUserInfo["UtilityAdress"] + "</td>");
            html.Append("<td width='18%' >" + drUserInfo["UtilityEmail"] + "</td>");
            html.Append("<td width='15%'  >" + drUserInfo["UtilityContact"] + "</td>");
            html.Append("<td >" + drUserInfo["UtilityRemark"] + "</td>");
            html.Append("<td align='center'  width='8%' ><a href='UtilityMaster.aspx?fid=" + drUserInfo["UtilityId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center'  width='8%' ><a href='Javascript:deletefunction(" + drUserInfo["UtilityId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayDoctor.InnerHtml = html.ToString();
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                ConnectionClass conCheckDuplicate = new ConnectionClass("MasterDuplicateCheck");
                List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
                sqlpCheckDuplicate.Add(new SqlParameter("@TableName", "UtilityMaster"));
                sqlpCheckDuplicate.Add(new SqlParameter("@FieldName", "UtilityName"));
                sqlpCheckDuplicate.Add(new SqlParameter("@CheckValue", txtName.Text.ToString().ToUpper()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    submit_data();
                }
                else
                {

                    if (ViewState["UtilityName"] == null)
                    {
                        lblDuplicate.Text = "*Area Exist If Not Found In List Check Deleted Data And Restore It.";
                    }
                    else
                    {
                        if (ViewState["UtilityName"].ToString() == txtName.Text.ToString())
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
            ConnectionClass conAdd = new ConnectionClass("RecUtilityMasterAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@UtilityId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@UtilityCode", congetMax.GetMaxTableCode("UtilityMaster", "UtilityId")));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@UtilityName", txtName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@UtilityAddress", txtAddress.Text.ToString()));
            sqlp.Add(new SqlParameter("@UtilityEmail", txtEmail.Text.ToString()));
            sqlp.Add(new SqlParameter("@UtilityContact", txtMobile.Text.ToString()));
            sqlp.Add(new SqlParameter("@UtilityRemark", txtRemark.Text.ToString()));
            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("UtilityMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("RecUtilityMasterUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            try
            {
                string id = Session["fid"].ToString();
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@UtilityId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@UtilityName", txtName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@UtilityAddress", txtAddress.Text.ToString()));
                sqlp.Add(new SqlParameter("@UtilityEmail", txtEmail.Text.ToString()));
                sqlp.Add(new SqlParameter("@UtilityContact", txtMobile.Text.ToString()));
                sqlp.Add(new SqlParameter("@UtilityRemark", txtRemark.Text.ToString()));
                sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
                bool i2 = conUpd.SaveData(sqlp);
                Session.Remove("fid");
                if (i2 == true)
                {
                    Response.Redirect("UtilityMaster.aspx");
                }
            }
            catch (Exception)
            {
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
        Response.Redirect("UtilityMaster.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteMedicine(string aid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "UtilityId"));
        sqlp.Add(new SqlParameter("@TableName", "UtilityMaster"));
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