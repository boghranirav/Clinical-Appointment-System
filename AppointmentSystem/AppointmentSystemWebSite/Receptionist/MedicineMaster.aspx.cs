using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_MedicineMaster : System.Web.UI.Page
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
                sqlp.Add(new SqlParameter("@TableName", "MedicineMaster"));
                sqlp.Add(new SqlParameter("@FieldName", "MedicineId"));
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
                        ViewState["MedicineName"] = dr["MedicineName"].ToString();
                       txtMName.Text = dr["MedicineName"].ToString();
                       txtChildDose.Text = dr["ChildDose"].ToString();
                       txtAdultDose.Text= dr["AdultDose"] .ToString();

                    }
                }
            }
            else if (Request.QueryString.AllKeys.Length > 0)
            {
                call_On_Cancel();
            }

            if (!IsPostBack)
            {
                txtMName.Focus();
                fillTable();
            }
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "MedicineMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["MedicineName"] + "</td>");
            html.Append("<td >" + drUserInfo["ChildDose"] + "</td>");
            html.Append("<td >" + drUserInfo["AdultDose"] + "</td>");
            html.Append("<td align='center'  width='10%' ><a href='MedicineMaster.aspx?fid=" + drUserInfo["MedicineId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center'  width='10%' ><a href='Javascript:deletefunction(" + drUserInfo["MedicineId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
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
                sqlpCheckDuplicate.Add(new SqlParameter("@TableName", "MedicineMaster"));
                sqlpCheckDuplicate.Add(new SqlParameter("@FieldName", "MedicineName"));
                sqlpCheckDuplicate.Add(new SqlParameter("@CheckValue", txtMName.Text.ToString().ToUpper()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    submit_data();
                }
                else
                {

                    if (ViewState["MedicineName"] == null)
                    {
                        lblDuplicate.Text = "*Area Exist If Not Found In List Check Deleted Data And Restore It.";
                    }
                    else
                    {
                        if (ViewState["MedicineName"].ToString() == txtMName.Text.ToString())
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
            ConnectionClass conAdd = new ConnectionClass("RecMedicineMasterAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@MedicineId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@MedicineCode", congetMax.GetMaxTableCode("MedicineMaster", "MedicineCode")));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@MedicineName", txtMName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@ChildDose", txtChildDose.Text.ToString()));
            sqlp.Add(new SqlParameter("@AdultDose", txtAdultDose.Text.ToString()));
            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("MedicineMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("RecMedicineMasterUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            try
            {
                string id = Session["fid"].ToString();
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@MedicineId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@MedicineName", txtMName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@ChildDose", txtChildDose.Text.ToString()));
                sqlp.Add(new SqlParameter("@AdultDose", txtAdultDose.Text.ToString()));
                sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
                bool i2 = conUpd.SaveData(sqlp);
                Session.Remove("fid");
                if (i2 == true)
                {
                    Response.Redirect("MedicineMaster.aspx");
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
        Response.Redirect("MedicineMaster.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteMedicine(string aid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "MedicineId"));
        sqlp.Add(new SqlParameter("@TableName", "MedicineMaster"));
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