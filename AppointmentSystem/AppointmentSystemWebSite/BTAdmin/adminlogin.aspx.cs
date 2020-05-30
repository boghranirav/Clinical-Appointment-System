using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public partial class BTAdmin_adminlogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUserId"] != null)
        {
            Response.Redirect("BTAdminCreate.aspx");
        }

        if (!IsPostBack)
        {
            if (this.IsCookieDisabled())
            {
                this.DisplayAsPerBrowser();
            }
            else
            {
                try
                {
                    ConnectionClass con = new ConnectionClass();
                    if (Request.Cookies["CookieLoginUserId"] != null)
                    {
                        con.AdminLogout(Request.Cookies["CookieLoginUserId"].Value.ToString());
                    }
                }
                catch (Exception) { }
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtLoginId.Text = Request.Cookies["UserName"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                    chkRememberMe.Checked = true;
                }
            }
        }
    }

    private bool IsCookieDisabled()
    {
        string currentUrl = Request.RawUrl;
        if (Request.QueryString["cookieCheck"] == null)
        {
            try
            {
                HttpCookie c = new HttpCookie("SupportCookies", "true");
                Response.Cookies.Add(c);
                if (currentUrl.IndexOf("?") > 0)
                    currentUrl = currentUrl + "&cookieCheck=true";
                else
                    currentUrl = currentUrl + "?cookieCheck=true";
                Response.Redirect(currentUrl);
            }
            catch (Exception)
            {
                return false;
            }
        }

        if (!Request.Browser.Cookies || Request.Cookies["SupportCookies"] == null)
            return true;

        return false;
    }

    protected void LoginClick(object sender, EventArgs e)
    {
        if (chkRememberMe.Checked)
        {
            if (Request.Browser.Cookies)
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["UserName"].Value = txtLoginId.Text.Trim();
                Response.Cookies["Password"].Value = txtPassword.Text.Trim();
            }
        }
        else
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
        }
        
        ConnectionClass con = new ConnectionClass();
        bool i=con.UserLogin(txtLoginId.Text.ToString());

        if (i == true)
        {
            MD5CryptoServiceProvider MD5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashbytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashbytes = MD5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text));
            //Encrypt(txtPassword.Text.ToString())
            string s = txtPassword.Text.ToString();
            bool b = con.UserLoginPassword(txtLoginId.Text.ToString(), txtPassword.Text.ToString());

            if (b == true)
            {
                ConnectionClass conc = new ConnectionClass();
                DataTable dt = new DataTable();
                dt = conc.GetAdminDetail(txtLoginId.Text).Tables[0];
                    
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["IsOnline"].ToString().Equals("True"))
                    {
                        Label1.Text = "All Ready Login From Other PC.";
                    }
                    else
                    {
                        Response.Cookies["CookieLoginUserId"].Value = dr["BtLoginId"].ToString();
                        Response.Cookies["CookieLoginUserId"].Expires = DateTime.Now.AddDays(30);
                        
                        Session["LoginUserId"] = dr["BtLoginId"];
                        Session["LoginAdminId"] = dr["BtAdminId"];
                        Session["LoginUserName"] = dr["BtFirstName"].ToString() + " " + dr["BtLastName"].ToString();
                        Session["UserRole"] = dr["BtDesignation"];
                        Session.Timeout = (8 * 60) * 60;
                        Response.Redirect("BTAdminCreate.aspx");
                    }
                    break;
                }
            }
            else
            {
                Label1.Text = "*Invalid Password.";
            }
        }
        else
        {
            Label1.Text = "*Invalid User Id.";
        }

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

    private void DisplayAsPerBrowser()
    {
        StringBuilder html = new StringBuilder();
        html.Append("Looks like your browser's cookies are disabled. To enable cookies follow following points.");

        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        if (browser.Type.Contains("Firefox"))
        {
            Response.Write("<script>");
            Response.Write("window.open('https://support.mozilla.org/en-US/kb/enable-and-disable-cookies-website-preferences','_blank','resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=no')");
            Response.Write("</script>");
        }
        else if (browser.Type.Contains("IE"))
        {
            Response.Write("<script>");
            Response.Write("window.open('https://www.timeanddate.com/custom/cookiesie.html','_blank','resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=no')");
            Response.Write("</script>");
        }
        else if (browser.Type.Contains("Chrome"))
        {
            html.Append("<br> For Chrome");
            html.Append("<br> 1. In the top-right corner of Chrome, click the Menu > Settings.");
            html.Append("<br> 2. At the bottom of the page, click \"Show advanced settings\".");
            html.Append("<br> 3. In the Privacy section, click the \"Content settings\" button.");
            html.Append("<br> 4. In the Cookies section, choose your preferred setting.");
        }
        else if (browser.Type.Contains("Opera"))
        {
            html.Append(" Opera");
            html.Append("<br> 1. Click on the \"Tools\" menu Opera.");
            html.Append("<br> 2. Click \"Preferences\".");
            html.Append("<br> 3. Change to the \"Advanced tab\", and to the cookie section.");
            html.Append("<br> 4. Select \"Accept cookies only from the site I visit\" or \"Accept cookies\"");
            html.Append("<br> 5. Ensure \"Delete new cookies when exiting Opera\" is not ticked.");
            html.Append("<br> 6. Click OK.");
        }

        spanDisplay.InnerHtml = html.ToString();
    }
}