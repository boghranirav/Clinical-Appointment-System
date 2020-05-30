using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ConnectionClass con = new ConnectionClass();

            if (Request.Cookies["CookieLoginUserId"] != null)
            {
                con.AdminLogout(Request.Cookies["CookieLoginUserId"].Value.ToString());
            }
            else
            {
                con.AdminLogout(Session["LoginUserId"].ToString());
            }
            Response.Cookies["CookieLoginUserId"].Expires = DateTime.Now.AddDays(-1);
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("adminlogin.aspx");
        }
        catch (Exception) { }

    }
}