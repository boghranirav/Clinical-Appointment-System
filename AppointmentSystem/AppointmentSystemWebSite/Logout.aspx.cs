using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ConnectionClass conLogOut = new ConnectionClass("SetUserOnline");

            if (Request.Cookies["CookieLoginUserId"] != null)
            {
                List<SqlParameter> sqlpLogOut = new List<SqlParameter>();
                sqlpLogOut.Add(new SqlParameter("@SetStatus", "2"));
                sqlpLogOut.Add(new SqlParameter("@CoLoginId", Request.Cookies["CookieLoginUserId"].Value.ToString()));
                conLogOut.SaveData(sqlpLogOut);
            }
            else
            {
                List<SqlParameter> sqlpLogOut = new List<SqlParameter>();
                sqlpLogOut.Add(new SqlParameter("@SetStatus", "2"));
                sqlpLogOut.Add(new SqlParameter("@CoLoginId", Session["LoginUserId"].ToString()));
                conLogOut.SaveData(sqlpLogOut);
            }
            Response.Cookies["CookieLoginUserId"].Expires = DateTime.Now.AddDays(-1);
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("UserLogin.aspx");
        }
        catch (Exception) { Response.Redirect("UserLogin.aspx"); }
    }

    [System.Web.Services.WebMethod]
    public static string LogoutUser(string userid)
    {
        ConnectionClass conLogOut = new ConnectionClass("SetUserOnline");
        string retval="";
        List<SqlParameter> sqlpLogOut = new List<SqlParameter>();
        sqlpLogOut.Add(new SqlParameter("@SetStatus", "2"));
        sqlpLogOut.Add(new SqlParameter("@CoLoginId", userid));
        conLogOut.SaveData(sqlpLogOut);

        bool i = conLogOut.SaveData(sqlpLogOut);
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