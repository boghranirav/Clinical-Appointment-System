using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_PatientMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {
            cmb_Fill_Cetegory();
            if (Request.QueryString.AllKeys.Contains("fid"))
            {
                if (!Request.QueryString["fid"].ToString().All(char.IsDigit))
                {
                    call_On_Cancel();
                }

                btnCancel.Visible = true;
                submit.Text = "Update";

                string id = Request.QueryString["fid"];

                if (id == null)
                {
                    call_On_Cancel();
                }
                Session["fid"] = Request.QueryString["fid"];
                ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@TableName", "PatientMaster"));
                sqlp.Add(new SqlParameter("@FieldName", "PatientId"));
                sqlp.Add(new SqlParameter("@TableId", id));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                DataTable dt = new DataTable();
                dt = conc.DisplayUserData(sqlp).Tables[0];

                if (dt.Rows.Count == 0) { call_On_Cancel(); }
                else
                {
                    try
                    {
                        cmb_Fill_RefDoctor();
                        cmb_Fill_Area();
                        foreach (DataRow dr in dt.Rows)
                        {
                            txtName.Text = dr["PatientName"].ToString();
                            txtAddress.Text = dr["PatientAddress"].ToString();
                            cmbGender.SelectedValue = dr["PatientGender"].ToString();
                            if (dr["PatentType"].ToString().Equals("CHILD"))
                            {
                                radioChild.Checked = true;
                                span1.Attributes["style"] = "color:red;";
                            }
                            else
                            {
                                radioAdult.Checked = true;
                                span1.Attributes["style"] = "color:black;";
                                span2.Attributes["style"] = "color:red;";
                            }

                            string d = dr["PatientBirthDate"].ToString();
                            txtBirthDate.Text = Convert.ToDateTime(d).ToString("yyyy-MM-dd");
                            txtAge.Text = dr["PatientAge"].ToString();
                            cmbAgeUnit.SelectedValue = dr["PatientAgeUnit"].ToString();
                            txtEmail.Text = dr["PatientEmail"].ToString();
                            txtMobile.Text = dr["PatientMobile"].ToString();
                            cmbBloodGroup.SelectedValue = dr["PatientBloodGroup"].ToString();
                            txtAllergy.Text = dr["PatientAllergyDetails"].ToString();
                            try
                            {
                                txtEnrollDate.Text = Convert.ToDateTime(dr["PatientEnrollDate"].ToString()).ToString("yyyy-MM-dd");
                            }
                            catch (Exception)
                            {
                                txtEnrollDate.Text = "";
                            }
                            cmbRefDoctor.SelectedValue = dr["ReferenceId"].ToString();
                            cmbArea.SelectedValue = dr["AreaId"].ToString();
                            cmbCategory.SelectedValue = dr["CategoryId"].ToString();

                            byte[] bytes = (byte[])dr["PatientImage"];
                            Session["imageupdate"] = bytes;
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            displayImg.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }
                    catch (Exception) { }
                }
            }
            else if (Request.QueryString.AllKeys.Length > 0)
            {
                call_On_Cancel();
            }

            
            if (!IsPostBack)
            {
                txtName.Focus();
                if (!Request.QueryString.AllKeys.Contains("fid"))
                {
                    cmb_Fill_RefDoctor();
                    cmb_Fill_Area();
                }
                fillTable();
                try
                {
                    ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@TableName", "CompanyMaster"));
                    sqlp.Add(new SqlParameter("@FieldName", "CompanyId"));
                    sqlp.Add(new SqlParameter("@TableId", Session["companyid"].ToString()));
                    sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                    DataTable dt = new DataTable();
                    dt = conc.DisplayUserData(sqlp).Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        txtEnrollDate.Attributes["min"] = DateTime.Parse(dr["CompanyCreateDate"].ToString()).ToString("yyyy-MM-dd");
                        break;
                    }


                    string datemin = "01-01-1900";
                    txtBirthDate.Attributes["min"] = DateTime.Parse(datemin.ToString()).ToString("yyyy-MM-dd");
                    txtBirthDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                    txtEnrollDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                    

                    txtAge.Attributes["min"] = "0";
                    txtAge.Attributes["max"] = "150";

                    if (!Request.QueryString.AllKeys.Contains("fid"))
                    {
                        Session.Remove("fid");
                        span1.Attributes["style"] = "color:red;";
                        txtEnrollDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                }
                catch (Exception) { }

                }
            
        }
    }

    public void fillTable()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "PatientMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            html.Append("<tr>");
            html.Append("<td >" + drUserInfo["PatientName"] + "</td>");
            html.Append("<td width='15%'>" + drUserInfo["PatientMobile"] + "</td>");
            html.Append("<td width='15%'>" + drUserInfo["PatientGender"] + "</td>");
            html.Append("<td width='10%'>" + drUserInfo["PatientAge"] + "</td>");
            html.Append("<td align='center'  width='10%' ><a href='PatientMaster.aspx?fid=" + drUserInfo["PatientId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
            html.Append("</tr>");
        }
        displayPatient.InnerHtml = html.ToString();
    }

    protected void cmb_Fill_Area()
    {
        ConnectionClass conArea = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlArea = new List<SqlParameter>();
        sqlArea.Add(new SqlParameter("@TableName", "AreaMaster"));
        sqlArea.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtArea = new DataTable();
        dtArea = conArea.DisplayUserData(sqlArea).Tables[0];
        cmbArea.Items.Clear();
        cmbArea.Items.Add(new ListItem("SELECT","SELECT"));
        foreach (DataRow drUserInfo in dtArea.Rows)
        {
            cmbArea.Items.Add(new ListItem(drUserInfo["AreaName"].ToString(), drUserInfo["AreaId"].ToString()));
        }
     }

    protected void cmb_Fill_RefDoctor()
    {
        ConnectionClass conRInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlRInfo = new List<SqlParameter>();
        sqlRInfo.Add(new SqlParameter("@TableName", "ReferenceDoctor"));
        sqlRInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtArea = new DataTable();
        dtArea = conRInfo.DisplayUserData(sqlRInfo).Tables[0];
        cmbRefDoctor.Items.Clear();
        cmbRefDoctor.Items.Add(new ListItem("SELECT", "SELECT"));
        foreach (DataRow drUserInfo in dtArea.Rows)
        {
            cmbRefDoctor.Items.Add(new ListItem(drUserInfo["RefName"].ToString(), drUserInfo["ReferenceId"].ToString()));
        }
    }

    public void cmb_Fill_Cetegory()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDate");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "PatientCategoryMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            cmbCategory.Items.Add(new ListItem(drUserInfo["CategoryName"].ToString(), drUserInfo["CategoryId"].ToString()));
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try{
                string ptype = "", bgroup = "";
                bool filevalid = true;
                Byte[] bytes = null;

                if (radioAdult.Checked == true){ ptype = "ADULT";} else {  ptype = "CHILD";}

                if (cmbBloodGroup.SelectedValue.ToString().Equals("SELECT"))
                {
                    bgroup = "";
                }
                else
                {
                    bgroup = cmbBloodGroup.SelectedValue.ToString();
                }

                

                if (imgPatientImage.HasFile)
                {
                    String ext = imgPatientImage.PostedFile.ContentType;
                    if (ext.ToLower() == "image/jpeg" || ext.ToLower() == "image/jpg" || ext.ToLower() == "image/png")
                    {
                        if (imgPatientImage.PostedFile.ContentLength > 0)
                        {
                            Stream fs = imgPatientImage.PostedFile.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                    else
                    {
                        filevalid = false;
                        Response.Write("<script>");
                        Response.Write("alert('Invalid Company Image " + ext + ". Add Image File.');");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    if (Session["imageupdate"] != null)
                    {
                        bytes = (Byte[])Session["imageupdate"];
                    }
                    else
                    {
                        bytes = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                    }
                }

                if (filevalid == true)
                {

                    if (submit.Text == "Submit")
                    {
                        ConnectionClass conAdd = new ConnectionClass("RecPatientMasterAdd");
                        ConnectionClass congetMax = new ConnectionClass();

                        List<SqlParameter> sqlp = new List<SqlParameter>();
                        sqlp.Add(new SqlParameter("@PatientId", congetMax.GetGlobalId()));
                        sqlp.Add(new SqlParameter("@PatientCode", congetMax.GetMaxTableCode("PatientMaster", "PatientCode")));
                        sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                        sqlp.Add(new SqlParameter("@PatientName", txtName.Text.ToString().ToUpper()));
                        sqlp.Add(new SqlParameter("@PatientAddress", txtAddress.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientGender", cmbGender.SelectedValue.ToString()));
                        sqlp.Add(new SqlParameter("@PatientType", ptype));
                        sqlp.Add(new SqlParameter("@PatientAge", txtAge.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientAgeUnit", cmbAgeUnit.SelectedValue.ToString()));
                        sqlp.Add(new SqlParameter("@PatientMobile", txtMobile.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientEmail", txtEmail.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientBloodGroup", bgroup));
                        sqlp.Add(new SqlParameter("@PatientAllergyDetails", txtAllergy.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientEnrollDate", Convert.ToDateTime(txtEnrollDate.Text.ToString())));

                        if (cmbRefDoctor.SelectedValue.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@ReferenceId", DBNull.Value));
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@ReferenceId", Convert.ToInt32(cmbRefDoctor.SelectedValue.ToString())));
                        }
                        
                        try
                        {
                            sqlp.Add(new SqlParameter("@PatientBirthDate", Convert.ToDateTime(txtBirthDate.Text.ToString())));
                        }
                        catch (Exception)
                        {
                            sqlp.Add(new SqlParameter("@PatientBirthDate", DBNull.Value));
                        }
                        if (cmbArea.SelectedValue.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@AreaId", DBNull.Value));                            
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@AreaId",cmbArea.SelectedValue.ToString()));
                        }

                        if (cmbCategory.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@CategoryId",DBNull.Value));
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@CategoryId", Convert.ToInt32(cmbCategory.SelectedValue.ToString())));
                        }
                        
                        sqlp.Add(new SqlParameter("@PatientImage", bytes));
                        sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                        bool i2 = conAdd.SaveData(sqlp);
                        if (i2 == true)
                        {
                            Response.Redirect("PatientMaster.aspx");
                        }
                    }
                    else
                    {
                        ConnectionClass conUpd = new ConnectionClass("RecPatientMasterUpdate");
                        ConnectionClass congetMax = new ConnectionClass();

                        string id = Session["fid"].ToString();
                        List<SqlParameter> sqlp = new List<SqlParameter>();
                        sqlp.Add(new SqlParameter("@PatientId", id));
                        sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                        sqlp.Add(new SqlParameter("@PatientName", txtName.Text.ToString().ToUpper()));
                        sqlp.Add(new SqlParameter("@PatientAddress", txtAddress.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientGender", cmbGender.SelectedValue.ToString()));
                        sqlp.Add(new SqlParameter("@PatientType", ptype));
                        sqlp.Add(new SqlParameter("@PatientAge", txtAge.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientAgeUnit", cmbAgeUnit.SelectedValue.ToString()));
                        sqlp.Add(new SqlParameter("@PatientMobile", txtMobile.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientEmail", txtEmail.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientBloodGroup", bgroup));
                        sqlp.Add(new SqlParameter("@PatientAllergyDetails", txtAllergy.Text.ToString()));
                        sqlp.Add(new SqlParameter("@PatientEnrollDate", Convert.ToDateTime(txtEnrollDate.Text.ToString())));

                        if (cmbRefDoctor.SelectedValue.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@ReferenceId", DBNull.Value));
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@ReferenceId", Convert.ToInt32(cmbRefDoctor.SelectedValue.ToString())));
                        }

                        try
                        {
                            sqlp.Add(new SqlParameter("@PatientBirthDate", Convert.ToDateTime(txtBirthDate.Text.ToString())));
                        }
                        catch (Exception)
                        {
                            sqlp.Add(new SqlParameter("@PatientBirthDate", DBNull.Value));
                        }
                        if (cmbArea.SelectedValue.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@AreaId", DBNull.Value));
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@AreaId",cmbArea.SelectedValue.ToString()));
                        }

                        if (cmbCategory.ToString().Equals("SELECT"))
                        {
                            sqlp.Add(new SqlParameter("@CategoryId", DBNull.Value));
                        }
                        else
                        {
                            sqlp.Add(new SqlParameter("@CategoryId", Convert.ToInt32(cmbCategory.SelectedValue.ToString())));
                        }

                        sqlp.Add(new SqlParameter("@PatientImage", bytes));
                        sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));
                        bool i2 = conUpd.SaveData(sqlp);
                        Session.Remove("fid");
                        if (i2 == true)
                        {
                            Response.Redirect("PatientMaster.aspx");
                        }
                    }
                }
                else { }
            }catch(Exception)
            {
            }

              
        }
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        call_On_Cancel();
    }

    protected void call_On_Cancel()
    {
        Session.Remove("fid");
        Response.Redirect("PatientMaster.aspx");
    }

    protected void radioChild_CheckedChanged(object sender, EventArgs e)
    {
        if (radioChild.Checked == true)
        {
            span1.Attributes["style"] = "color:red;";
            span2.Attributes["style"] = "color:black;";
        }
        else
        {
            span1.Attributes["style"] = "color:black;";
            span2.Attributes["style"] = "color:red;";
        }
    }
    protected void txtBirthDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime Dob = DateTime.ParseExact(txtBirthDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;

            if (Years >= 1)
            {
                txtAge.Text = ""+Years;
                cmbAgeUnit.SelectedValue = "YEAR";
            }else
                if (Months >= 1)
                {
                    txtAge.Text = "" + Months;
                    cmbAgeUnit.SelectedValue = "MONTH";
                }
                else
                    if (Days >= 1)
                    {
                        txtAge.Text = "" + Days;
                        cmbAgeUnit.SelectedValue = "DAYS";
                    }
                    else
                    {
                        txtAge.Text = "" + Hours;                           
                        cmbAgeUnit.SelectedValue = "HOURS";
                    }
            
        }
        catch (Exception) { }
        // return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s)",
        // Years, Months, Days, Hours);
    }
    protected void submitArea_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewArea.Text == null || txtNewArea.Text == "")
            {
                lblArea.Text = "* Enter Area Name";
            }
            else
            {
                ConnectionClass conCheckDuplicate = new ConnectionClass("MasterDuplicateCheck");
                List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
                sqlpCheckDuplicate.Add(new SqlParameter("@TableName", "AreaMaster"));
                sqlpCheckDuplicate.Add(new SqlParameter("@FieldName", "AreaName"));
                sqlpCheckDuplicate.Add(new SqlParameter("@CheckValue", txtNewArea.Text.ToString().ToUpper()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    ConnectionClass conAdd = new ConnectionClass("RecAreaMaterAdd");
                    ConnectionClass congetMax = new ConnectionClass();

                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@AreaId", congetMax.GetGlobalId()));
                    sqlp.Add(new SqlParameter("@AreaCode", congetMax.GetMaxTableCode("AreaMaster", "AreaCode")));
                    sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                    sqlp.Add(new SqlParameter("@AreaName", txtNewArea.Text.ToString().ToUpper()));
                    sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                    bool i2 = conAdd.SaveData(sqlp);
                    if (i2 == true)
                    {
                        txtNewArea.Text = "";
                        lblArea.Text = "Area Added.";
                        cmb_Fill_Area();
                    }
                }
                else
                {
                    lblArea.Text = "*Area Exist If Not Found In List Check Deleted Data And Restore It.";
                }
            }
        }
        catch (Exception) { }
        
    }
    protected void submitRefDoctor_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewRefDoctor.Text == null || txtNewRefDoctor.Text == "")
            {
                lblRefDoc.Text = "* Enter Doctor Name.";
            }
            else
            {
                ConnectionClass conAdd = new ConnectionClass("RecRefDcotorMaster");
                ConnectionClass congetMax = new ConnectionClass();

                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@ReferenceId", congetMax.GetGlobalId()));
                sqlp.Add(new SqlParameter("@ReferenceCode", congetMax.GetMaxTableCode("ReferenceDoctor", "ReferenceCode")));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@RefName", txtNewRefDoctor.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@RefAddress", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefMobile", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefEmail", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefShare", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefSType", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefSendEmail", DBNull.Value));
                sqlp.Add(new SqlParameter("@RefSendMobile", DBNull.Value));
                sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                bool i2 = conAdd.SaveData(sqlp);
                if (i2 == true)
                {
                    cmb_Fill_RefDoctor();
                    lblRefDoc.Text = "Reference Doctor Added.";
                }
            }
        }
        catch (Exception) { }
    }

}