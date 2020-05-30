using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CompanyAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../UserLogin.aspx");
        }

        lblUserName.Text = Session["LoginUserName"].ToString();
        lblDes.Text = Session["BTRole"].ToString();

        String activepage = Request.RawUrl;

        if (activepage.Contains("Receptionist.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smRec.Attributes["class"] = "active";
            atitle.Text = "Admin Registration";
        }
        else
        if (activepage.Contains("PatientCategory.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smPactCat.Attributes["class"] = "active";
            atitle.Text = "Admin Registration";
        }else
        if (activepage.Contains("UserList.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_List.Attributes["class"] = "active";
            atitle.Text = "User List";
        }
        else
        if (activepage.Contains("SessionTime.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_Session.Attributes["class"] = "active";
            atitle.Text = "Session Time Out";
        }
        else
        if (activepage.Contains("ChangePassword.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_Password.Attributes["class"] = "active";
            atitle.Text = "Change Password";
        }
        else
        if (activepage.Contains("ResetUserPassword.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_UserPass.Attributes["class"] = "active";
            atitle.Text = "Change User Password";
        }
    }
}
