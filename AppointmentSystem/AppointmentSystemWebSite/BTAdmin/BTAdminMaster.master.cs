using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_BTAdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        lblUserName.Text = Session["LoginUserName"].ToString();
        lblUserName2.Text = Session["LoginUserName"].ToString();
        lblDes.Text = Session["UserRole"].ToString();

        String activepage = Request.RawUrl;

        if (activepage.Contains("BTAdminCreate.aspx"))
        {
            //dashboard.Attributes["class"] = "active";
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smcreateadmin.Attributes["class"] = "active";
            atitle.Text = "Admin Registration";
        }
        else if (activepage.Contains("SpecializationMaster.aspx"))
        {
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smspecilazation.Attributes["class"] = "active";
            atitle.Text = "Specialization Master";
        }
        else if (activepage.Contains("MarketingMaster.aspx"))
        {
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smmarketing.Attributes["class"] = "active";
            atitle.Text = "Marketing Master";
        }
        else if (activepage.Contains("FormMaster.aspx"))
        {
            id_AdminMaster.Attributes["class"] = "has-sub active";
            id_smforms.Attributes["class"] = "active";
            atitle.Text = "Form Master";
        }
        else if (activepage.Contains("CompanyMaster.aspx"))
        {
            id_CompanyMaster.Attributes["class"] = "has-sub active";
            id_smCompanyCreate.Attributes["class"] = "active";
            atitle.Text = "Company Master";
        }
        else if (activepage.Contains("CompanyDisplay.aspx"))
        {
            id_CompanyMaster.Attributes["class"] = "has-sub active";
            id_smCompanyDisplay.Attributes["class"] = "active";
            atitle.Text = "Display Company";
        }
        else if (activepage.Contains("DoctorMaster.aspx"))
        {
            id_CompanyMaster.Attributes["class"] = "has-sub active";
            id_smCompanyDisplay.Attributes["class"] = "active";
            atitle.Text = "Doctor Master";
        }


    }
}
