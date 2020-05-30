using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

public partial class BTAdmin_BTAdminCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        if (!IsPostBack)
        {
            ConnectionClass conOnline = new ConnectionClass();
            conOnline.SetAdminOnline(Session["LoginUserId"].ToString());
            txtFirstName.Focus();
            fillTable();
        }

        if (Request.QueryString.AllKeys.Contains("aid"))
        {
            if (Request.QueryString["aid"].ToString().All(char.IsDigit))
            {}
            else
            {
                Response.Redirect("BTAdminCreate.aspx");
            }
            btncancel.Visible = true;
            btncancel.CausesValidation = false;
            RequiredFieldVPassword.Enabled = false;
            RegularExpressionValidator7.Enabled = false;
            submit.Text = "Update";
            string id = Request.QueryString["aid"];
            if (id == null)
            {
                Response.Redirect("BTAdminCreate.aspx");
            }
            Session["aid"] = Request.QueryString["aid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "BtAdminMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "BtAdminId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];
            if (dt.Rows.Count == 0) { Response.Redirect("BTAdminCreate.aspx"); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtFirstName.Text = dr["BtFirstName"].ToString();
                    txtMiddleName.Text = dr["BtMiddleName"].ToString();
                    txtLastName.Text = dr["BtLastName"].ToString();
                    cmbDesignation.Text = dr["BtDesignation"].ToString();
                    txtLoginId.Text = dr["BtLoginId"].ToString();
                    txtEmail.Text = dr["BtEmail"].ToString();
                    txtPhone.Text = dr["BtPhone"].ToString();
                }
            }
        }else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("BTAdminCreate.aspx");
        }

        
    }

    public void fillTable()
    {
        ConnectionClass sp3 = new ConnectionClass("displayData");
      
        DataTable dt = new DataTable();
        dt = sp3.DisplayData("BtAdminMaster").Tables[0];
        StringBuilder html = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            string statusO = "";
            string ena = "";
            if (dr["IsOnline"].ToString() == "False")
            {
                statusO = "Offline";
                ena = "enabled";
            }
            else {
                statusO = "Online";
                ena = "disabled";
            }
           
            html.Append("<tr>");
            html.Append ( "<td>" + dr["BtFirstName"] + " " + dr["BtMiddleName"] + " " + dr["BtLastName"] + "</td>");
            html.Append ("<td>" + dr["BtLoginId"] + "</td><td>" + dr["BtPhone"] + "</td><td>" + dr["BtDesignation"] + "</td><td>" + statusO + "</td>");
            html.Append("<td align='center' width='8%'><a href='BTAdminCreate.aspx?aid=" + dr["BtAdminId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append( "<td align='center' width='4%'><a href='Javascript:deletefunction(" + dr["BtAdminId"] + "," + Session["LoginAdminId"].ToString() + ");' class=" + ena + "><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
            
        }
        displayAdmin.InnerHtml = html.ToString();
    }

    protected void submit_click(object sender, EventArgs e)
    {
        if (submit.Text == "Submit")
        {
            ConnectionClass conAdd = new ConnectionClass("BTAdminAddNew_SP");
            ConnectionClass congetMax = new ConnectionClass();

            MD5CryptoServiceProvider MD5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashbytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashbytes = MD5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text));
           // string passwordEnc=FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text,"SHA1");

                 
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@BtAdminId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@BtAdminCode", congetMax.GetMaxTableCode("BtAdminMaster", "BtAdminCode")));
            sqlp.Add(new SqlParameter("@BtFirstName", txtFirstName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@BtMiddleName", txtMiddleName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@BtLastName", txtLastName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@BtLoginId", txtLoginId.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@BtPassword", Encrypt(txtPassword.Text.ToString())));
            sqlp.Add(new SqlParameter("@BtPhone", txtPhone.Text.ToString()));
            sqlp.Add(new SqlParameter("@BtDesignation", cmbDesignation.SelectedValue.ToString()));
            sqlp.Add(new SqlParameter("@BtEmail", txtEmail.Text.ToString()));
            sqlp.Add(new SqlParameter("@IsOnline", "0"));
            sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));
            sqlp.Add(new SqlParameter("@LoginDate", DateTime.Now));
            sqlp.Add(new SqlParameter("@DeleteFlg", "0"));
            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("BTAdminCreate.aspx");
            }
        }else
        {
                ConnectionClass conUpd = new ConnectionClass("BTAdminUpdate");
                ConnectionClass congetMax = new ConnectionClass();

                string id = Session["aid"].ToString();
                
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@BtAdminId", id));
                sqlp.Add(new SqlParameter("@BtFirstName", txtFirstName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@BtMiddleName", txtMiddleName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@BtLastName", txtLastName.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@BtPhone", txtPhone.Text.ToString()));
                sqlp.Add(new SqlParameter("@BtDesignation", cmbDesignation.SelectedValue.ToString()));
                sqlp.Add(new SqlParameter("@BtEmail", txtEmail.Text.ToString()));
                sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
                sqlp.Add(new SqlParameter("@EditDate", DateTime.Now));
                bool i2 = conUpd.SaveData(sqlp);
                Session.Remove("aid");
                if (i2 == true)
                {
                    Response.Redirect("BTAdminCreate.aspx");
                }
            }
    }


    protected void CreateUserId(object sender, EventArgs e)
    {
        if(Session["aid"] == null)
        {
        string uid="";
        if (txtFirstName.Text.ToString() != "")
        uid=txtFirstName.Text.Substring(0,1).ToUpper();
        if (txtMiddleName.Text.ToString() != "")
        uid += txtMiddleName.Text.Substring(0, 1).ToUpper();
        if (txtLastName.Text.ToString() != "")
        uid += txtLastName.Text.Substring(0, 1).ToUpper();


        ConnectionClass con = new ConnectionClass();
        txtLoginId.Text = con.AdminUserIdCheck(uid);
        
        }
    }

    [System.Web.Services.WebMethod]
    public static string DeleteAdmin(string adminid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "BtAdminId"));
        sqlp.Add(new SqlParameter("@TableName", "BtAdminMaster"));
        sqlp.Add(new SqlParameter("@TableId", adminid));
        sqlp.Add(new SqlParameter("@DeleteId", userid));

        bool i = con.DeleteAdminData(sqlp);
            if (i==true)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }
       // }
        return retval;
    }

    protected void onCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("aid");
        Response.Redirect("BTAdminCreate.aspx");
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}