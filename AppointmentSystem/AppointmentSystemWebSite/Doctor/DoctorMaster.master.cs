﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_DoctorMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../UserLogin.aspx");
        }

        lblUserName.Text = Session["LoginUserName"].ToString();
        lblDes.Text = Session["BTRole"].ToString();
    }
}
