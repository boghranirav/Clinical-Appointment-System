using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_FormMaster : System.Web.UI.Page
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
                Response.Redirect("FormMaster.aspx");
            }

            btncancel.Visible = true;
            btncancel.CausesValidation = false;
            submit.Text = "Update";
            string id = Request.QueryString["fid"];
            if (id == null)
            {
                Response.Redirect("FormMaster.aspx");
            }
            Session["fid"] = Request.QueryString["fid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "FormMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "FormId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];
            if (dt.Rows.Count == 0) { Response.Redirect("FormMaster.aspx"); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtFormName.Text = dr["FormName"].ToString();
                    txtButtonName.Text = dr["FormButtonName"].ToString();
                    txtCategory.Text = dr["FormCategory"].ToString();
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("FormMaster.aspx");
        }

        if (!IsPostBack)
        {
            txtFormName.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass sp3 = new ConnectionClass("displayData");

        DataTable dt = new DataTable();
        dt = sp3.DisplayData("FormMaster").Tables[0];
        StringBuilder html = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            html.Append("<tr>");
            html.Append("<td>" + dr["FormName"] + "</td>");
            html.Append("<td>" + dr["FormButtonName"] + "</td>");
            html.Append("<td>" + dr["FormCategory"] + "</td>");
            html.Append("<td align='center' width='8%'><a href='FormMaster.aspx?fid=" + dr["FormId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + dr["FormId"] + "," + Session["LoginAdminId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayForm.InnerHtml = html.ToString();
    }

    protected void onSubmit_Click(object sender, EventArgs e)
    {
        if (submit.Text == "Submit")
        {
            ConnectionClass conAdd = new ConnectionClass("AdminFormAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@FormId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@FormCode", congetMax.GetMaxTableCode("FormMaster", "FormCode")));
            sqlp.Add(new SqlParameter("@FormName", txtFormName.Text.ToString()));
            sqlp.Add(new SqlParameter("@FormButtonName", txtButtonName.Text.ToString()));
            sqlp.Add(new SqlParameter("@FormCategory", txtCategory.Text.ToString()));
            sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("FormMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("AdminFormUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            string id = Session["fid"].ToString();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@FormId", id));
            sqlp.Add(new SqlParameter("@FormName", txtFormName.Text.ToString()));
            sqlp.Add(new SqlParameter("@FormButtonName", txtButtonName.Text.ToString()));
            sqlp.Add(new SqlParameter("@FormCategory", txtCategory.Text.ToString()));
            sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
            bool i2 = conUpd.SaveData(sqlp);
            Session.Remove("fid");
            if (i2 == true)
            {
                Response.Redirect("FormMaster.aspx");
            }
        }
    }

    protected void onCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("fid");
        Response.Redirect("FormMaster.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteForm(string fid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "FormId"));
        sqlp.Add(new SqlParameter("@TableName", "FormMaster"));
        sqlp.Add(new SqlParameter("@TableId", fid));
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
}