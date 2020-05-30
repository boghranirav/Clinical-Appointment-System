using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_SpecializationAdd : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        ConnectionClass conForm = new ConnectionClass("displayData");
        DataTable dt1 = new DataTable();
        dt1 = conForm.DisplayData("FormMaster").Tables[0];
        if (!IsPostBack)
        {
            foreach (DataRow dr1 in dt1.Rows)
            {
                cmbFormSelect.Items.Add(new ListItem(dr1["FormName"].ToString(), dr1["FormId"].ToString()));
            }

            fillTable();
        }

        if (Request.QueryString.AllKeys.Contains("sid"))
        {
            if (!Request.QueryString["sid"].ToString().All(char.IsDigit))
            {
                Response.Redirect("SpecializationAdd.aspx");
            }

            btncancel.Visible = true;
            btncancel.CausesValidation = false;
            submit.Text = "Update";
            string id = Request.QueryString["sid"];
            if (id == null)
            {
                Response.Redirect("SpecializationAdd.aspx");
            }
            Session["sid"] = Request.QueryString["sid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "SpecializationMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "SpecializationId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                txtSpecialization.Text = dr["SpecializationDescription"].ToString();
            }

            ConnectionClass conc2 = new ConnectionClass("AdminSpecFormDetail");
            DataTable dt2 = new DataTable();
            //GridView1.DataSource = conc2.DisplayDataById(Convert.ToInt32 (id), "@SpecializationId").Tables[0];
            dt2 = conc2.DisplayDataById(Convert.ToInt32(id), "@SpecializationId").Tables[0];
            DataTable dtData = new DataTable();
            dtData.Columns.Add("FormName");
            dtData.Columns.Add("FormId");
            dtData.Columns.Add("SortingSeq");
            //int i = 1;
            foreach (DataRow dr2 in dt2.Rows)
            {
                DataRow drTab = dtData.NewRow();
                drTab["FormId"] = dr2["FormId"].ToString();
                drTab["FormName"] = dr2["FormName"].ToString();
                drTab["SortingSeq"] = dr2["SortingSeq"].ToString();
                //TextBox txtSrNo = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextBoxS");
                //txtSrNo.Text = "1";
                dtData.Rows.Add(drTab);
            }
            GridView1.DataSource = dtData.Copy();
            GridView1.DataBind();
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("SpecializationAdd.aspx");
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
            html.Append("<td>" + dr["SpecializationCode"] + "</td>");
            html.Append("<td>" + dr["SpecializationDescription"] +  "</td>");
            html.Append("<td align='center' width='8%'><a href='SpecializationAdd.aspx?sid=" + dr["SpecializationId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + dr["SpecializationId"] + "," + Session["LoginAdminId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
            html.Append("</tr>");
        }
        displaySpec.InnerHtml = html.ToString();
    }

    protected void onSubmit_Click(object sender, EventArgs e)
    {

        if (submit.Text == "Submit")
        {
            ConnectionClass congetMax = new ConnectionClass();
            int globalID = congetMax.GetGlobalId();
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "AdminSpecializationAdd";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@SpecializationId",SqlDbType.BigInt).Value =globalID;
                cmd.Parameters.Add("@SpecializationCode",SqlDbType.Int).Value =congetMax.GetMaxTableCode("SpecializationMaster", "SpecializationCode");
                cmd.Parameters.Add("@SepcializationDec",SqlDbType.VarChar).Value =txtSpecialization.Text.ToString();
                cmd.Parameters.Add("@LoginId",SqlDbType.BigInt).Value =Session["LoginAdminId"].ToString();
                cmd.ExecuteNonQuery();

                SqlCommand cmd2;
                int i = 1;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    cmd2 = con.CreateCommand();
                    cmd2.CommandText = "AdminSpecFormAdd";
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@SpecializationId", SqlDbType.BigInt).Value = globalID;
                    cmd2.Parameters.Add("@Srno", SqlDbType.Int).Value = i;
                    int b = Convert.ToInt32(row.Cells[0].Text);
                    cmd2.Parameters.Add("@FormId", SqlDbType.BigInt).Value = b;
                    cmd2.Parameters.Add("@SortingSeq", SqlDbType.Int).Value = ((TextBox)row.Cells[2].FindControl("TextBoxS")).Text;
                    cmd2.ExecuteNonQuery();
                    i++;
                }
                
            }
            catch (Exception )
            { }
            con.Close();
            Response.Redirect("SpecializationAdd.aspx");
        }
        else
        {
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "AdminSpecializationUpdate";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string id = Session["sid"].ToString();

                cmd.Parameters.Add("@SpecializationId",SqlDbType.BigInt).Value = Convert.ToInt32(id);
                cmd.Parameters.Add("@SepcializationDec", SqlDbType.VarChar).Value =txtSpecialization.Text.ToString();
                cmd.Parameters.Add("@EditId", SqlDbType.BigInt).Value =Session["LoginAdminId"].ToString();
                cmd.ExecuteNonQuery();

                SqlCommand cmd2;
                int i = 1;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    cmd2 = con.CreateCommand();
                    cmd2.CommandText = "AdminSpecFormAdd";
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@SpecializationId", SqlDbType.BigInt).Value = id;
                    cmd2.Parameters.Add("@Srno", SqlDbType.Int).Value = i;
                    int b = Convert.ToInt32(row.Cells[0].Text);
                    cmd2.Parameters.Add("@FormId", SqlDbType.BigInt).Value = b;
                    cmd2.Parameters.Add("@SortingSeq", SqlDbType.Int).Value = ((TextBox)row.Cells[2].FindControl("TextBoxS")).Text;
                    cmd2.ExecuteNonQuery();
                    i++;
                }
            }
            catch (Exception)
            { }
            con.Close();
            Session.Remove("sid");
            Response.Redirect("SpecializationAdd.aspx");
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
        Response.Redirect("SpecializationAdd.aspx");
    }

    protected void onFormIndexChange(object sender, EventArgs e)
    {

        DataTable dtData = new DataTable();
        dtData.Columns.Add("FormName");
        dtData.Columns.Add("FormId");
        dtData.Columns.Add("SortingSeq");
        int cou = 1;
        foreach (int i in cmbFormSelect.GetSelectedIndices())
        {
            DataRow dr = dtData.NewRow();
            dr["FormId"] = cmbFormSelect.Items[i].Value;
            dr["FormName"] = cmbFormSelect.Items[i].Text;
            dr["SortingSeq"] = cou.ToString();
            cou++;
            dtData.Rows.Add(dr);
        }
        GridView1.DataSource = dtData.Copy();
        GridView1.DataBind();
        cmbFormSelect.Focus();
        
    }
}