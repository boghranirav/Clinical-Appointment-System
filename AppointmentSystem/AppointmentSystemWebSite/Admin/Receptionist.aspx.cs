using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Receptionist : System.Web.UI.Page
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
                txtLoginId.Enabled = false;
                string id = Request.QueryString["fid"];

                if (id == null)
                {
                    call_On_Cancel();
                }
                Session["fid"] = Request.QueryString["fid"];
                ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@TableName", "CompanyLoginMaster"));
                sqlp.Add(new SqlParameter("@FieldName", "CoLoginId"));
                sqlp.Add(new SqlParameter("@TableId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                DataTable dt = new DataTable();
                dt = conc.DisplayUserData(sqlp).Tables[0];

                if (dt.Rows.Count == 0) { call_On_Cancel(); }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtFirstName.Text = dr["CoFirstName"].ToString();
                        txtMiddleName.Text = dr["CoMiddleName"].ToString();
                        txtLastName.Text = dr["CoLastName"].ToString();
                        txtLoginId.Text = dr["CoUserLoginId"].ToString();
                    }
                }
            }
            else if (Request.QueryString.AllKeys.Length > 0)
            {
                call_On_Cancel();
            }

            if (!IsPostBack)
            {

                ConnectionClass conLogIn = new ConnectionClass("SetUserOnline");
                List<SqlParameter> sqlpLogIn = new List<SqlParameter>();
                sqlpLogIn.Add(new SqlParameter("@SetStatus", "1"));
                sqlpLogIn.Add(new SqlParameter("@CoLoginId", Request.Cookies["CookieLoginUserId"].Value.ToString()));
                conLogIn.SaveData(sqlpLogIn);

                txtFirstName.Focus();
                fillTable();
            }
        }
    }

    public void fillTable()
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
                html.Append("<td>" + drUserInfo["CoFirstName"] + " " + drUserInfo["CoMiddleName"] + " " + drUserInfo["CoLastName"] + "</td>");
                html.Append("<td>" + drUserInfo["CoUserLoginId"] + "</td>");
                html.Append("<td>" + onlinestatus + "</td>");
                html.Append("<td align='center' width='8%'><a href='Receptionist.aspx?fid=" + drUserInfo["CoLoginId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
                html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + drUserInfo["CoLoginId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
                html.Append("</tr>");
        }
        displayRec.InnerHtml = html.ToString();
    }

    
    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (submit.Text == "Submit")
            {
                ConnectionClass conAdd = new ConnectionClass("CAdminReceptionistAdd");
                ConnectionClass congetMax = new ConnectionClass();

                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@CoLoginId", congetMax.GetGlobalId()));
                sqlp.Add(new SqlParameter("@CoLoginCode", congetMax.GetMaxTableCode("CompanyLoginMaster", "CoLoginCode")));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@CoFirstName", txtFirstName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@CoMiddleName", txtMiddleName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@CoLastName", txtLastName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@CoUserLoginId", txtLoginId.Text.ToString()));
                sqlp.Add(new SqlParameter("@CoUserPassword", txtLoginId.Text.ToString()));
                sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                bool i2 = conAdd.SaveData(sqlp);
                if (i2 == true)
                {
                    Response.Redirect("Receptionist.aspx");
                }
            }
            else
            {
                ConnectionClass conUpd = new ConnectionClass("CAdminReceptionistUpdate");
                ConnectionClass congetMax = new ConnectionClass();

                try
                {
                    string id = Session["fid"].ToString();
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@CoLoginId", id));
                    sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                    sqlp.Add(new SqlParameter("@CoFirstName", txtFirstName.Text.ToString().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoMiddleName", txtMiddleName.Text.ToString().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoLastName", txtLastName.Text.ToString().ToUpper()));
                    sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
                    bool i2 = conUpd.SaveData(sqlp);
                    Session.Remove("fid");
                    if (i2 == true)
                    {
                        Response.Redirect("Receptionist.aspx");
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }

    protected void CreateUserId(object sender, EventArgs e)
    {
        if (Session["fid"] == null)
        {
            string uid = "";
            if (txtFirstName.Text.ToString() != "")
                uid = txtFirstName.Text.Substring(0, 1).ToUpper();
            if (txtMiddleName.Text.ToString() != "")
                uid += txtMiddleName.Text.Substring(0, 1).ToUpper();
            if (txtLastName.Text.ToString() != "")
                uid += txtLastName.Text.Substring(0, 1).ToUpper();
           
            ConnectionClass con = new ConnectionClass("AdminCreateDoctorLoginId");
            txtLoginId.Text = con.DoctorUserIdCreate(uid, Convert.ToInt32(Session["companyid"].ToString()));
        }
        TextBox someButton = sender as TextBox;
        someButton.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        call_On_Cancel();
    }

    protected void call_On_Cancel()
    {
        Session.Remove("fid");
        Response.Redirect("Receptionist.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteReceptionist(string recid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "CoLoginId"));
        sqlp.Add(new SqlParameter("@TableName", "CompanyLoginMaster"));
        sqlp.Add(new SqlParameter("@TableId", recid));
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