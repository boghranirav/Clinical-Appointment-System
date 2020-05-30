using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_SpecializationMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        if (Request.QueryString.AllKeys.Contains("fid"))
        {
            if (!Request.QueryString["fid"].ToString().All(char.IsDigit))
            {
                Response.Redirect("SpecializationMaster.aspx");
            }

            btncancel.Visible = true;
            btncancel.CausesValidation = false;
            submit.Text = "Update";
            string id = Request.QueryString["fid"];
            if (id == null)
            {
                Response.Redirect("SpecializationMaster.aspx");
            }
            Session["fid"] = Request.QueryString["fid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "SpecializationMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "SpecializationId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];

            if (dt.Rows.Count == 0) { Response.Redirect("SpecializationMaster.aspx"); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtSpecialization.Text = dr["SpecializationDescription"].ToString();
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("SpecializationMaster.aspx");
        }

        if (!IsPostBack)
        {
            txtSpecialization.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass sp3 = new ConnectionClass("displayData");

        DataTable dt = new DataTable();
        dt = sp3.DisplayData("SpecializationMaster").Tables[0];
        StringBuilder html = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            html.Append("<tr>");
            html.Append("<td style='text-transform :capitalize;'>" + dr["SpecializationDescription"] + "</td>");
            html.Append("<td align='center' width='8%'><a href='SpecializationMaster.aspx?fid=" + dr["SpecializationId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + dr["SpecializationId"] + "," + Session["LoginAdminId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displaySpec.InnerHtml = html.ToString();
    }

    protected void onSubmit_Click(object sender, EventArgs e)
    {
        if (submit.Text == "Submit")
        {
            ConnectionClass conAdd = new ConnectionClass("AdminSpecializationAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@SpecializationId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@SpecializationCode", congetMax.GetMaxTableCode("SpecializationMaster", "SpecializationCode")));
            sqlp.Add(new SqlParameter("@SepcializationDec", txtSpecialization.Text.ToString()));
            sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("SpecializationMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("AdminSpecializationUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            string id = Session["fid"].ToString();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@SpecializationId", id));
            sqlp.Add(new SqlParameter("@SepcializationDec", txtSpecialization.Text.ToString()));
            sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
            bool i2 = conUpd.SaveData(sqlp);
            Session.Remove("fid");
            if (i2 == true)
            {
                Response.Redirect("SpecializationMaster.aspx");
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static string DeleteSpecialazation(string spid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "SpecializationId"));
        sqlp.Add(new SqlParameter("@TableName", "SpecializationMaster"));
        sqlp.Add(new SqlParameter("@TableId", spid));
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

    protected void onCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("sid");
        Response.Redirect("SpecializationMaster.aspx");
    }
}