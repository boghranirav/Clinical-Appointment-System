using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_RefDoctor : System.Web.UI.Page
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
                sqlp.Add(new SqlParameter("@TableName", "ReferenceDoctor"));
                sqlp.Add(new SqlParameter("@FieldName", "ReferenceId"));
                sqlp.Add(new SqlParameter("@TableId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                DataTable dt = new DataTable();
                dt = conc.DisplayUserData(sqlp).Tables[0];

                if (dt.Rows.Count == 0) { call_On_Cancel(); }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtName.Text = dr["RefName"].ToString();
                        txtAddress.Text = dr["RefAddress"].ToString();
                        txtEmail.Text=  dr["RefEmail"].ToString();
                        txtMobile.Text = dr["RefMobile"].ToString();
                        txtShare.Text = dr["RefShare"].ToString();
                        cmbShareType.SelectedValue = dr["RefSType"].ToString();
                        if (dr["RefSendEmail"].ToString().Equals("True")) { chkEmail.Checked = true; }
                        if (dr["RefSendSMS"].ToString().Equals("True")) { chkSMS.Checked = true; }

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
        sqlPInfo.Add(new SqlParameter("@TableName", "ReferenceDoctor"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            string stype = "",sendEmail="",sendSMS="";
            if (drUserInfo["RefSType"].ToString().Equals("SELECT")) { stype = ""; }
            else if (drUserInfo["RefSType"].ToString().Equals("PER")) { stype = "%"; }
            else if (drUserInfo["RefSType"].ToString().Equals("RUPEES")) { stype = "Rs."; }

            if (drUserInfo["RefSendEmail"].ToString().Equals("True")) { sendEmail = "YES"; } else { sendEmail = "NO"; }
            if (drUserInfo["RefSendSMS"].ToString().Equals("True")) { sendSMS = "YES"; } else { sendSMS = "NO"; }


            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["RefName"] + "</td>");
            html.Append("<td >" + drUserInfo["RefAddress"] + "</td>");
            html.Append("<td >" + drUserInfo["RefMobile"] + "</td>");
            html.Append("<td >" + drUserInfo["RefShare"] + " " + stype + "</td>");
            html.Append("<td >" +  sendEmail + "</td>");
            html.Append("<td >" + sendSMS + "</td>");
            html.Append("<td align='center'  width='10%' ><a href='RefDoctor.aspx?fid=" + drUserInfo["ReferenceId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center'  width='10%' ><a href='Javascript:deletefunction(" + drUserInfo["ReferenceId"] + "," + Session["CoLoginId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayDoctor.InnerHtml = html.ToString();
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int sendsms,sendemail;
            if(chkEmail.Checked==true){sendemail=1;}else {sendemail=0;}
            if(chkSMS.Checked==true){sendsms=1;}else{sendsms=0;}

            if (submit.Text == "Submit")
            {
                ConnectionClass conAdd = new ConnectionClass("RecRefDcotorMaster");
                ConnectionClass congetMax = new ConnectionClass();

                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@ReferenceId", congetMax.GetGlobalId()));
                sqlp.Add(new SqlParameter("@ReferenceCode", congetMax.GetMaxTableCode("ReferenceDoctor", "ReferenceCode")));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@RefName", txtName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@RefAddress", txtAddress.Text.ToString()));
                sqlp.Add(new SqlParameter("@RefMobile", txtMobile.Text.ToString()));
                sqlp.Add(new SqlParameter("@RefEmail", txtEmail.Text.ToString()));
                sqlp.Add(new SqlParameter("@RefShare", txtShare.Text.ToString()));
                sqlp.Add(new SqlParameter("@RefSType", cmbShareType.SelectedValue.ToString()));
                sqlp.Add(new SqlParameter("@RefSendEmail",sendemail ));
                sqlp.Add(new SqlParameter("@RefSendMobile",sendsms));
                sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                bool i2 = conAdd.SaveData(sqlp);
                if (i2 == true)
                {
                    Response.Redirect("RefDoctor.aspx");
                }
            }
            else
            {
                ConnectionClass conUpd = new ConnectionClass("RecRefDcotorMasterUpdate");
                ConnectionClass congetMax = new ConnectionClass();

                try
                {
                    string id = Session["fid"].ToString();
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@ReferenceId", id));
                    sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                    sqlp.Add(new SqlParameter("@RefName", txtName.Text.ToString().ToUpper()));
                    sqlp.Add(new SqlParameter("@RefAddress", txtAddress.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RefMobile", txtMobile.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RefEmail", txtEmail.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RefShare", txtShare.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RefSType", cmbShareType.SelectedValue.ToString()));
                    sqlp.Add(new SqlParameter("@RefSendEmail", sendemail));
                    sqlp.Add(new SqlParameter("@RefSendMobile", sendsms));
                    sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
                    bool i2 = conUpd.SaveData(sqlp);
                    Session.Remove("fid");
                    if (i2 == true)
                    {
                        Response.Redirect("RefDoctor.aspx");
                    }
                }
                catch (Exception)
                {
                }
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
        Response.Redirect("RefDoctor.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteDoctor(string aid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "ReferenceId"));
        sqlp.Add(new SqlParameter("@TableName", "ReferenceDoctor"));
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