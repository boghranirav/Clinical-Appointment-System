using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MarketingMaster : System.Web.UI.Page
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
                Response.Redirect("MarketingMaster.aspx");
            }

            btncancel.Visible = true;
            btncancel.CausesValidation = false;
            submit.Text = "Update";
            string id = Request.QueryString["fid"];
            if (id == null)
            {
                Response.Redirect("MarketingMaster.aspx");
            }
            Session["fid"] = Request.QueryString["fid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "MarketingMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "MarketingId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];
            if (dt.Rows.Count == 0) { Response.Redirect("MarketingMaster.aspx"); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtMName.Text = dr["MarketingName"].ToString();
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("MarketingMaster.aspx");
        }

        if (!IsPostBack)
        {
            txtMName.Focus();
            fillTable();
        }
    }

    public void fillTable()
    {
        ConnectionClass sp3 = new ConnectionClass("displayData");

        DataTable dt = new DataTable();
        dt = sp3.DisplayData("MarketingMaster").Tables[0];
        StringBuilder html = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            html.Append("<tr>");
            html.Append("<td>" + dr["MarketingName"] + "</td>");
            html.Append("<td align='center' width='8%'><a href='MarketingMaster.aspx?fid=" + dr["MarketingId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + dr["MarketingId"] + "," + Session["LoginAdminId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displayMarketing.InnerHtml = html.ToString();
    }

    protected void onSubmit_Click(object sender, EventArgs e)
    {
        if (submit.Text == "Submit")
        {
            ConnectionClass conAdd = new ConnectionClass("AdminMarketingAdd");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@MarketingId", congetMax.GetGlobalId()));
            sqlp.Add(new SqlParameter("@MarketingCode", congetMax.GetMaxTableCode("MarketingMaster", "MarketingCode")));
            sqlp.Add(new SqlParameter("@MarketingName", txtMName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                Response.Redirect("MarketingMaster.aspx");
            }
        }
        else
        {
            ConnectionClass conUpd = new ConnectionClass("AdminMarketingUpdate");
            ConnectionClass congetMax = new ConnectionClass();

            string id = Session["fid"].ToString();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@MarketingId", id));
            sqlp.Add(new SqlParameter("@MarketingName", txtMName.Text.ToString().ToUpper()));
            sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
            bool i2 = conUpd.SaveData(sqlp);
            Session.Remove("fid");
            if (i2 == true)
            {
                Response.Redirect("MarketingMaster.aspx");
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static string DeleteMarketing(string spid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "MarketingId"));
        sqlp.Add(new SqlParameter("@TableName", "MarketingMaster"));
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
        Response.Redirect("MarketingMaster.aspx");
    }
}