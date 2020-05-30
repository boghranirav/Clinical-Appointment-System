using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUserId"] != null)
        {
            switch (Session["BTRole"].ToString())
            {
                case "DOCTOR":
                    Response.Redirect("Doctor/DoctorForm.aspx");
                    break;
                case "ADMIN":
                    Response.Redirect("Admin/Receptionist.aspx");
                    break;
                case "RECPTION":
                    Response.Redirect("Receptionist/AreaMaster.aspx");
                    break;
            }
        }
        if (Request.QueryString.AllKeys.Length > 1)
        {
            Response.Redirect("UserLogin.aspx");
        }
        
        GetUser_IP();
        if (!IsPostBack)
        {
            if (this.IsCookieDisabled())
            {
                this.DisplayAsPerBrowser();
            }
            else
            {
                if (Request.Cookies["CompanyCode"] != null && Request.Cookies["CUserName"] != null && Request.Cookies["CPassword"] != null)
                {
                    txtCompanyCode.Text = Request.Cookies["CompanyCode"].Value;
                    txtLoginId.Text = Request.Cookies["CUserName"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["CPassword"].Value;
                    chkRememberMe.Checked = true;
                    txtCompanyCode.Focus();
                }
            }
        }
    }

    protected void LoginClick(object sender, EventArgs e)
    {
        if (chkRememberMe.Checked)
        {
            Response.Cookies["CompanyCode"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["CUserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["CPassword"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["CompanyCode"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["CUserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["CPassword"].Expires = DateTime.Now.AddDays(-1);
        }
        Response.Cookies["CompanyCode"].Value = txtCompanyCode.Text.ToString();
        Response.Cookies["CUserName"].Value = txtLoginId.Text.Trim();
        Response.Cookies["CPassword"].Value = txtPassword.Text.Trim();

        try
        {
            if (Response.Cookies["CookieLoginUserId"].Value != null)
            {
                ConnectionClass conLogOut = new ConnectionClass("SetUserOnline");
                List<SqlParameter> sqlpLogOut = new List<SqlParameter>();
                sqlpLogOut.Add(new SqlParameter("@SetStatus", "2"));
                sqlpLogOut.Add(new SqlParameter("@CoLoginId", Request.Cookies["CookieLoginUserId"].Value.ToString()));
                conLogOut.SaveData(sqlpLogOut);
            }
        }
        catch (Exception) { }
        this.ValidateUser();
    }

    protected void ValidateUser()
    {
        ConnectionClass conValidate = new ConnectionClass();
        ConnectionClass conTrackInvalid = new ConnectionClass("InvalidLoginTrack");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
        sqlp.Add(new SqlParameter("@CoLoginId", txtLoginId.Text.ToString()));
        sqlp.Add(new SqlParameter("@Password", txtPassword.Text.ToString()));

        string result=conValidate.checkUserLogin(sqlp);
        string status = "",browserName="";
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        browserName = browser.Browser;
        switch (result)
        {
            case "1":
                List<SqlParameter> sqlp1 = new List<SqlParameter>();
                sqlp1.Add(new SqlParameter("@LoginId", txtLoginId.Text.ToString()));
                sqlp1.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                sqlp1.Add(new SqlParameter("@WrongPassword", txtPassword.Text.ToString()));
                sqlp1.Add(new SqlParameter("@IPAddress", txtPassword.Text.ToString()));
                sqlp1.Add(new SqlParameter("@BrowserName", browserName));
                sqlp1.Add(new SqlParameter("@Latitude", "1"));
                sqlp1.Add(new SqlParameter("@Longitude", "1"));
                sqlp1.Add(new SqlParameter("@TStatus", "1"));
                conTrackInvalid.SaveData(sqlp1);
                status = "Invalid Company Code.";
                break;
            case "2":
                List<SqlParameter> sqlp2 = new List<SqlParameter>();
                sqlp2.Add(new SqlParameter("@LoginId", txtLoginId.Text.ToString()));
                sqlp2.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                sqlp2.Add(new SqlParameter("@WrongPassword", txtPassword.Text.ToString()));
                sqlp2.Add(new SqlParameter("@IPAddress", txtPassword.Text.ToString()));
                sqlp2.Add(new SqlParameter("@BrowserName", browserName));
                sqlp2.Add(new SqlParameter("@Latitude", "1"));
                sqlp2.Add(new SqlParameter("@Longitude", "1"));
                sqlp2.Add(new SqlParameter("@TStatus", "2"));
                conTrackInvalid.SaveData(sqlp2);
                status = "In Valid User Id.";
                break;
            case "3":
                List<SqlParameter> sqlp3 = new List<SqlParameter>();
                sqlp3.Add(new SqlParameter("@LoginId", txtLoginId.Text.ToString()));
                sqlp3.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                sqlp3.Add(new SqlParameter("@WrongPassword", txtPassword.Text.ToString()));
                sqlp3.Add(new SqlParameter("@IPAddress", txtPassword.Text.ToString()));
                sqlp3.Add(new SqlParameter("@BrowserName", browserName));
                sqlp3.Add(new SqlParameter("@Latitude", txtPassword.Text.ToString()));
                sqlp3.Add(new SqlParameter("@Longitude", txtPassword.Text.ToString()));
                sqlp3.Add(new SqlParameter("@TStatus", "3"));
                conTrackInvalid.SaveData(sqlp3);
                status = "Invalid Password.";
                break;
            case "4":
                List<SqlParameter> sqlp4 = new List<SqlParameter>();
                sqlp4.Add(new SqlParameter("@LoginId", txtLoginId.Text.ToString()));
                sqlp4.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                sqlp4.Add(new SqlParameter("@WrongPassword", txtPassword.Text.ToString()));
                sqlp4.Add(new SqlParameter("@IPAddress", txtPassword.Text.ToString()));
                sqlp4.Add(new SqlParameter("@BrowserName", browserName));
                sqlp4.Add(new SqlParameter("@Latitude", txtPassword.Text.ToString()));
                sqlp4.Add(new SqlParameter("@Longitude", txtPassword.Text.ToString()));
                sqlp4.Add(new SqlParameter("@TStatus", "4"));
                conTrackInvalid.SaveData(sqlp4);
                status = "Your Account Is Expired. Please Re-new To Use Account.";
                break;
            case "5":

                ConnectionClass conDispaly = new ConnectionClass("InvalidLoginidDisplay");
                List<SqlParameter> sqlpDisp = new List<SqlParameter>();
                sqlpDisp.Add(new SqlParameter("@LoginId", txtLoginId.Text.ToString()));
                sqlpDisp.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                sqlpDisp.Add(new SqlParameter("@TStatus", "3"));
                DataTable dtDisp = new DataTable();
                StringBuilder html = new StringBuilder();
                html.Append("Invalid Login In Your Account");
      
                dtDisp= conDispaly.DisplayUserData(sqlpDisp).Tables[0];

                foreach (DataRow drDisp in dtDisp.Rows)
                {
                    html.Append( "IP : " + drDisp["IPAddress"].ToString() + " Date Time : " + drDisp["TDateTime"].ToString() + " Browser : " + drDisp["BrowserName"].ToString());
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + html + "');", true);

                try
                {
                    ConnectionClass conUserInfo = new ConnectionClass("CompanyLoginInfo");
                    List<SqlParameter> sqlpUserInfo = new List<SqlParameter>();
                    sqlpUserInfo.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString()));
                    sqlpUserInfo.Add(new SqlParameter("@CoUserLoginId", txtLoginId.Text.ToString()));
                    DataTable dtUserInfo = new DataTable();
                    dtUserInfo = conUserInfo.DisplayUserData(sqlpUserInfo).Tables[0];
                    foreach (DataRow drUserInfo in dtUserInfo.Rows)
                    {
                        if (drUserInfo["IsOnline"].ToString().Equals("True"))
                        {
                            status = "All Ready Login From Other PC.";
                        }
                        else
                        {
                            Response.Cookies["CookieLoginUserId"].Value = drUserInfo["CoLoginId"].ToString();
                            Response.Cookies["CookieLoginUserId"].Expires = DateTime.Now.AddDays(30);
                            Session["LoginUserId"] = drUserInfo["CoUserLoginId"].ToString();
                            Session["CoLoginId"] = drUserInfo["CoLoginId"].ToString();
                            Session["companyid"] = drUserInfo["CompanyId"].ToString();
                            Session["LoginUserName"] = drUserInfo["CoFirstName"].ToString() + " " + drUserInfo["CoLastName"].ToString();
                            Session["BTRole"] = drUserInfo["CoRole"].ToString();

                            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                            Session.Timeout = Convert.ToInt32(drUserInfo["SessionValue"].ToString()) * 60;
                            //int timeout = (int)section.Timeout.TotalMinutes * Convert.ToInt32(drUserInfo["SessionValue"].ToString()) * 60;

                             ConnectionClass conTrackLogin = new ConnectionClass("TrackLoginTimeAdd");
                             List<SqlParameter> sqlpTrackLogin = new List<SqlParameter>();
                             sqlpTrackLogin.Add(new SqlParameter("@CoLoginId",drUserInfo["CoLoginId"].ToString()));
                             sqlpTrackLogin.Add(new SqlParameter("@IPAddress", txtCompanyCode.Text.ToString()));
                             sqlpTrackLogin.Add(new SqlParameter("@Browser", browserName));
                             conTrackLogin.SaveData(sqlpTrackLogin);

                            switch (Session["BTRole"].ToString())
                            {
                                case "DOCTOR":
                                    Response.Redirect("Doctor/DoctorForm.aspx");
                                    break;
                                case "ADMIN":
                                    Response.Redirect("Admin/Receptionist.aspx");
                                    break;
                                case "RECPTION":
                                    Response.Redirect("Receptionist/AreaMaster.aspx");
                                    break;
                            }
                        }
                        break;
                    }
                }
                catch (Exception)
                { }
            break;
        }
        spanDisplay.InnerHtml = status;
    }

    protected void GetUser_IP()
    {
        string IPAdd = string.Empty;
        IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(IPAdd))
            IPAdd = Request.ServerVariables["REMOTE_ADDR"];

        spanDisplay.InnerHtml = IPAdd;

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

    //protected string TimeMorningFrom { get; set; }
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