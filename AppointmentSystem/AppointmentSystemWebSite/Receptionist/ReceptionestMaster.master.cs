using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionest_ReceptionestMaster : System.Web.UI.MasterPage
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

        if (activepage.Contains("AreaMaster.aspx"))
        {
            li_MasterForms.Attributes["class"] = "has-sub active";
            sm_AreaMaster.Attributes["class"] = "active";
            atitle.Text = "Area Master";
        }
        else
        if (activepage.Contains("RefDoctor.aspx"))
        {
            li_MasterForms.Attributes["class"] = "has-sub active";
            sm_RefMaster.Attributes["class"] = "active";
            atitle.Text = "Reference Doctor Master";
        }
        else
            if (activepage.Contains("MedicineMaster.aspx"))
        {
            li_MasterForms.Attributes["class"] = "has-sub active";
            sm_MedicineMaster.Attributes["class"] = "active";
            atitle.Text = "Medicine Master";
        }
        else
        if (activepage.Contains("UtilityMaster.aspx"))
        {
            li_MasterForms.Attributes["class"] = "has-sub active";
            sm_UtilityMaster.Attributes["class"] = "active";
            atitle.Text = "Utility Master";
        }
        else
        if (activepage.Contains("PatientMaster.aspx"))
        {
            li_MasterForms.Attributes["class"] = "has-sub active";
            sm_PatientMaster.Attributes["class"] = "active";
            atitle.Text = "Patient Master";
        }
        else
        if (activepage.Contains("ChangePassword.aspx"))
        {
            menu_password.Attributes["class"] = "active";
            atitle.Text = "Change Password";
        }
        else
        if (activepage.Contains("ViewPatient.aspx"))
        {
            menu_viewpat.Attributes["class"] = "active";
            atitle.Text = "View Patient";
        }
        else
        if (activepage.Contains("AppointmentEntry.aspx"))
        {
            li_AppoMaster.Attributes["class"] = "has-sub active";
            sm_AppointmentEntry.Attributes["class"] = "active";
            atitle.Text = "Appointment Entry";
        }
        else
        if (activepage.Contains("TodayEntry.aspx"))
        {
            li_AppoMaster.Attributes["class"] = "has-sub active";
            sm_AppointmentToday.Attributes["class"] = "active";
            atitle.Text = "Today Entry";
        }
    }
}
