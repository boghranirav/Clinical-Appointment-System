using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_DoctorMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        if (Request.QueryString.AllKeys.Length < 0)
        {
            Response.Redirect("CompanyDisplay.aspx");
        }

        if (!IsPostBack)
        {
            if (Request.QueryString.AllKeys.Contains("fid"))
            {
                if (Request.QueryString["fid"].ToString().All(char.IsDigit))
                {
                    Session["companyid"] = Request.QueryString["fid"];
                }
                else
                {
                    Response.Redirect("CompanyDisplay.aspx");
                }
            }

            ConnectionClass conChe = new ConnectionClass();
            string s= conChe.checkNoDoctor(Session["companyid"].ToString());
            if (s.Equals("2"))
            {
                submit.Attributes["class"] = "btn btn-primary disabled";
                displayLabel.Text = "Can Not Add New Doctor.";
            }
            
           
            //Dropdown Fill Specialization and Forms
            ConnectionClass conFillSpe = new ConnectionClass("displayData");
            DataTable dtFillSpe = new DataTable();
            dtFillSpe = conFillSpe.DisplayData("SpecializationMaster").Tables[0];

            ConnectionClass conFillForms = new ConnectionClass("displayData");
            DataTable dtFillForms = new DataTable();
            dtFillForms = conFillForms.DisplayData("FormMaster").Tables[0];

            if (HttpContext.Current.Session["companyid"] == null)
            {
                Response.Redirect("CompanyDisplay.aspx");
            }
            ConnectionClass conFillCompanyInfo = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "CompanyMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "CompanyId"));
            sqlp.Add(new SqlParameter("@TableId", Session["companyid"].ToString()));
            DataTable dtCompanyInfo = new DataTable();
            dtCompanyInfo = conFillCompanyInfo.GetAdminDetailById(sqlp).Tables[0];
            if (dtCompanyInfo.Rows.Count == 0) { Response.Redirect("CompanyDisplay.aspx"); }
            foreach (DataRow drCompInfo in dtCompanyInfo.Rows)
            {
                txtCompCode.Text = drCompInfo["CompanyCode"].ToString();
                txtCompName.Text = drCompInfo["CompanyName"].ToString();
                txtDocAddress.Text = drCompInfo["CompanyAddress"].ToString();
            }

            foreach (DataRow drFillSpe in dtFillSpe.Rows)
            {
                cmbSpecialization.Items.Add(new ListItem(drFillSpe["SpecializationDescription"].ToString(), drFillSpe["SpecializationId"].ToString()));
            }

            foreach (DataRow drFillForms in dtFillForms.Rows)
            {
                cmbFormSelect.Items.Add(new ListItem(drFillForms["FormName"].ToString(), drFillForms["FormId"].ToString()));
            }
            fillTable();
        }

        if (Request.QueryString.AllKeys.Contains("eid"))
        {
            submit.Attributes["class"] = "btn btn-primary";
            displayLabel.Text = "";
            submit.Text = "Update";
            
            if (Request.QueryString["eid"].ToString().All(char.IsDigit))
            {
                Session["doctorid"] = Request.QueryString["eid"];
                ConnectionClass conDisSDoc = new ConnectionClass("AdminDisplayById");
                List<SqlParameter> sqlDisSDoc = new List<SqlParameter>();
                sqlDisSDoc.Add(new SqlParameter("@TableName", "CompanyLoginMaster"));
                sqlDisSDoc.Add(new SqlParameter("@FieldName", "CoLoginId"));
                sqlDisSDoc.Add(new SqlParameter("@TableId", Session["doctorid"].ToString()));
                DataTable dtDisSDoc = new DataTable();
                dtDisSDoc = conDisSDoc.GetAdminDetailById(sqlDisSDoc).Tables[0];

                if (dtDisSDoc.Rows.Count == 0) { Response.Redirect("CompanyDisplay.aspx"); }
                foreach(DataRow drDisSDoc in dtDisSDoc.Rows)
                {
                    txtFirstName.Text = drDisSDoc["CoFirstName"].ToString();
                    txtMiddleName.Text = drDisSDoc["CoMiddleName"].ToString();
                    txtDLastName.Text = drDisSDoc["CoLastName"].ToString();
                    txtLoginId.Text = drDisSDoc["CoUserLoginId"].ToString();
                    cmbSalute.SelectedValue = drDisSDoc["CoSalute"].ToString();

                    ConnectionClass conDocInfo = new ConnectionClass("AdminDisplayById");
                    List<SqlParameter> sqlDocInfo = new List<SqlParameter>();
                    sqlDocInfo.Add(new SqlParameter("@TableName", "CompanyDoctorMaster"));
                    sqlDocInfo.Add(new SqlParameter("@FieldName", "CoLoginId"));
                    sqlDocInfo.Add(new SqlParameter("@TableId", Session["doctorid"].ToString()));
                    DataTable dtDocInfo = new DataTable();
                    dtDocInfo = conDocInfo.GetAdminDetailById(sqlDocInfo).Tables[0];

                    ConnectionClass conc2 = new ConnectionClass("DoctorFormDetailDisplay");
                    DataTable dt2 = new DataTable();
                    
                    foreach (DataRow drDocInfo in dtDocInfo.Rows)
                    {
                        txtDocEmail.Text = drDocInfo["DrEmail"].ToString();
                        txtDocMobile.Text = drDocInfo["DrMobile"].ToString();
                        txtDocDegree.Text = drDocInfo["DrDegree"].ToString();
                        txtDocAddress.Text = drDocInfo["DrAddress"].ToString();
                        txtAppNoOfDays.Text =drDocInfo["ApptOfNoOfDays"].ToString ();
                        txtFirstVistCharge.Text =drDocInfo["FirstVisitCharge"].ToString ();
                        txtRoutineVCharge.Text =drDocInfo["RoutineVisitCharge"].ToString ();
                        txtValidityDay.Text = drDocInfo["ValidityOfFirstChargeDay"].ToString();

                        if (drDocInfo["MorningSession"].ToString().Equals("True"))
                        {
                            chkMorningSession.Checked = true;
                        }
                        else
                        {
                            chkMorningSession.Checked = false;
                            txtPatientPSMorning.Enabled = false;
                        }
                        if (drDocInfo["EveningSession"].ToString().Equals("True"))
                        { chkEveningSession.Checked = true; }
                        else
                        {
                            chkEveningSession.Checked = false;
                            txtPatientPSEvening.Enabled = false;
                        }

                        txtPatientPSMorning.Text = drDocInfo["PatientPerShift_Morning"].ToString();
                        txtPatientPSEvening.Text = drDocInfo["PatientPerShift_Evening"].ToString();

                        this.TimeMorningFrom = DateTime.Parse(drDocInfo["MorningTimeFrom"].ToString()).ToString("hh:mm:ss tt");
                        this.TimeMorningTo = DateTime.Parse(drDocInfo["MorningTimeTo"].ToString()).ToString("hh:mm:ss tt");
                        this.TimeEveningFrom = DateTime.Parse(drDocInfo["EveningTimeFrom"].ToString()).ToString("hh:mm:ss tt");
                        this.TimeEveningTo = DateTime.Parse(drDocInfo["EveningTimeTo"].ToString()).ToString("hh:mm:ss tt"); 

                        if (drDocInfo["Monday"].ToString().Equals("True")) chkMonday.Checked = true; else chkMonday.Checked = false;
                        if (drDocInfo["Tuesday"].ToString().Equals("True")) chkTuesday.Checked = true; else chkTuesday.Checked = false;
                        if (drDocInfo["Wednesday"].ToString().Equals("True")) chkWednesday.Checked = true; else chkWednesday.Checked = false;
                        if (drDocInfo["Thursday"].ToString().Equals("True")) chkThursday.Checked = true; else chkThursday.Checked = false;
                        if (drDocInfo["Friday"].ToString().Equals("True")) chkFriday.Checked = true; else chkFriday.Checked = false;
                        if (drDocInfo["Saturday"].ToString().Equals("True")) chkSaturday.Checked = true; else chkSaturday.Checked = false;
                        if (drDocInfo["Sunday"].ToString().Equals("True")) chkSunday.Checked = true; else chkSunday.Checked = false;

                        txtPrintLineOne.Text = drDocInfo["HeaderLine1"].ToString();
                        txtPrintLineTwo.Text = drDocInfo["HeaderLine2"].ToString();
                        txtBottomLine.Text = drDocInfo["BottomLine"].ToString();

                        cmbSpecialization.SelectedValue = drDocInfo["SpecializationId"].ToString();

                        byte[] bytes = (byte[])drDocInfo["CompanyLogo"];
                        Session["imageupdate"] = bytes;
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        displayLogo.ImageUrl = "data:image/png;base64," + base64String;
                    }

                    dt2 = conc2.DisplayDataById(Convert.ToInt32(Session["doctorid"].ToString()), "@DoctorId").Tables[0];
                    DataTable dtData = new DataTable();
                    dtData.Columns.Add("FormName");
                    dtData.Columns.Add("FormId");
                    dtData.Columns.Add("SortingSeq");

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        DataRow drTab = dtData.NewRow();
                        drTab["FormId"] = dr2["FormId"].ToString();
                        drTab["FormName"] = dr2["FormName"].ToString();
                        drTab["SortingSeq"] = dr2["SortingSeq"].ToString();
                        dtData.Rows.Add(drTab);
                    }
                    GridView1.DataSource = dtData.Copy();
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Redirect("CompanyDisplay.aspx");
            }
        }
      
        
    }

    public void fillTable()
    {
        ConnectionClass conDisDoc = new ConnectionClass("DisplayUserByRole");
        List<SqlParameter> sqlDisDoc = new List<SqlParameter>();
        sqlDisDoc.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlDisDoc.Add(new SqlParameter("@CoRole", "DOCTOR"));

        DataTable dtDisDoc = new DataTable();
        dtDisDoc = conDisDoc.DisplayUserData(sqlDisDoc).Tables[0];

       // ConnectionClass conDisDoc = new ConnectionClass("AdminDisplayById");
       // List<SqlParameter> sqlDisDoc = new List<SqlParameter>();
       // sqlDisDoc.Add(new SqlParameter("@TableName", "CompanyLoginMaster"));
       // sqlDisDoc.Add(new SqlParameter("@FieldName", "CompanyId"));
       // sqlDisDoc.Add(new SqlParameter("@TableId", Session["companyid"].ToString()));
       // DataTable dtDisDoc = new DataTable();
       // dtDisDoc = conDisDoc.GetAdminDetailById(sqlDisDoc).Tables[0];

        StringBuilder html = new StringBuilder();
        foreach (DataRow drDisDoc in dtDisDoc.Rows)
        {
                html.Append("<tr>");
                html.Append("<td>" + drDisDoc["CoFirstName"] + " " + drDisDoc["CoMiddleName"] + " " + drDisDoc["CoLastName"] + "</td>");
                html.Append("<td>" + drDisDoc["CoUserLoginId"] + "</td>");
                html.Append("<td align='center' width='8%'><a href='DoctorMaster.aspx?eid=" + drDisDoc["CoLoginId"] + "'><i class='fa fa-1x fa-pencil'></i></a></td>");
                html.Append("<td align='center' width='4%'><a href='Javascript:deletefunction(" + drDisDoc["CoLoginId"] + "," + Session["LoginAdminId"].ToString() + ");'><i class='fa fa-1x fa-trash-o'></i></a></td>");
                html.Append("</tr>");
        }
        displayDoc.InnerHtml = html.ToString();
    }
 
    protected void onSubmitClick(object sender, EventArgs e)
    {
        if (DateTime.Parse(String.Format("{0}", Request.Form["timepickermorfrom"])) > DateTime.Parse(String.Format("{0}", Request.Form["timepickermorto"])))
        {
            Response.Write("<script>");
            Response.Write("alert('Mornning Session Time From Can Not Be Grater Then To.');");
            Response.Write("</script>");
        }
        else
            if (DateTime.Parse(String.Format("{0}", Request.Form["timepickerevefrom"])) > DateTime.Parse(String.Format("{0}", Request.Form["timepickereveto"])))
        {
            Response.Write("<script>");
            Response.Write("alert('Mornning Session Time From Can Not Be Grater Then To.');");
            Response.Write("</script>");
        }
        else
        if (chkMorningSession.Checked == false && chkEveningSession.Checked == false)
        {
            Response.Write("<script>");
            Response.Write("alert('Selectct Atleast On Session.');");
            Response.Write("</script>");
        }
        else
        {
            string patMor = "", patEve = "";
            bool filevalid = true;
            int sessionMor, sessionEve;
            int monday, tuesday, wednesday, thursday, friday, saturday, sunday;
            Byte[] bytes = null;

            string MorFrom = String.Format("{0}", Request.Form["timepickermorfrom"]);
            string MorTo = String.Format("{0}", Request.Form["timepickermorto"]);
            string EveFrom = String.Format("{0}", Request.Form["timepickerevefrom"]);
            string EveTo = String.Format("{0}", Request.Form["timepickereveto"]);

            if (chkMorningSession.Checked == true)
            {
                sessionMor = 1;
                patMor = txtPatientPSMorning.Text;
            }
            else
            {
                sessionMor = 0;
                patMor = "";
                MorFrom = "";
                MorTo = "";
            }
            if (chkEveningSession.Checked == true)
            {
                sessionEve = 1;
                patEve = txtPatientPSEvening.Text;
            }
            else
            {
                sessionEve = 0;
                patEve = "";
                EveFrom = "";
                EveTo = "";
            }

            if (chkMonday.Checked == true) monday = 1; else monday = 0;
            if (chkTuesday.Checked == true) tuesday = 1; else tuesday = 0;
            if (chkWednesday.Checked == true) wednesday = 1; else wednesday = 0;
            if (chkThursday.Checked == true) thursday = 1; else thursday = 0;
            if (chkFriday.Checked == true) friday = 1; else friday = 0;
            if (chkSaturday.Checked == true) saturday = 1; else saturday = 0;
            if (chkSunday.Checked == true) sunday = 1; else sunday = 0;


            if (fileCompanyLogo.HasFile)
            {
                String ext = fileCompanyLogo.PostedFile.ContentType;
                if (ext.ToLower() == "image/jpeg" || ext.ToLower() == "image/jpg" || ext.ToLower() == "image/png")
                {
                    if (fileCompanyLogo.PostedFile.ContentLength > 0)
                    {
                        Stream fs = fileCompanyLogo.PostedFile.InputStream;
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
                if (submit.Text.ToString().Equals("Submit"))
                {
                    ConnectionClass conAdd = new ConnectionClass("AdminDoctorAdd");
                    ConnectionClass congetMax = new ConnectionClass();
                    int globalId = congetMax.GetGlobalId();
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@CoLoginId", globalId));
                    sqlp.Add(new SqlParameter("@CoLoginCode", congetMax.GetMaxTableCode("CompanyLoginMaster", "CoLoginCode")));
                    sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                    sqlp.Add(new SqlParameter("@CoSalute", cmbSalute.SelectedValue.ToString()));
                    sqlp.Add(new SqlParameter("@CoFirstName", txtFirstName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoMiddleName", txtMiddleName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoLastName", txtDLastName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoRole", "DOCTOR"));
                    sqlp.Add(new SqlParameter("@CoUserLoginId", txtLoginId.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoUserPassword", txtLoginId.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));
                    sqlp.Add(new SqlParameter("@DrEmail", txtDocEmail.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrDegree", txtDocDegree.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrMobile", txtDocMobile.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrAddress", txtDocAddress.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@SpecializationId", cmbSpecialization.SelectedValue.ToString()));
                    sqlp.Add(new SqlParameter("@ApptOfNoOfDays", Convert.ToInt16(txtAppNoOfDays.Text)));
                    sqlp.Add(new SqlParameter("@FirstVisitCharge", txtFirstVistCharge.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RoutineVisitCharge", txtRoutineVCharge.Text.ToString()));
                    sqlp.Add(new SqlParameter("@ValidityOfFirstChargeDay", Convert.ToInt16(txtValidityDay.Text)));
                    sqlp.Add(new SqlParameter("@MorningSession", sessionMor));
                    sqlp.Add(new SqlParameter("@PatientPerShiftMorning", patMor));
                    sqlp.Add(new SqlParameter("@EveningSession", sessionEve));
                    sqlp.Add(new SqlParameter("@PatientPerShiftEvening", patEve));
                    sqlp.Add(new SqlParameter("@MorningTimeFrom", MorFrom));
                    sqlp.Add(new SqlParameter("@MorningTimeTo", MorTo));
                    sqlp.Add(new SqlParameter("@EveningTimeFrom", EveFrom));
                    sqlp.Add(new SqlParameter("@EveningTimeTo", EveTo));
                    sqlp.Add(new SqlParameter("@Monday", monday));
                    sqlp.Add(new SqlParameter("@Tuesday", tuesday));
                    sqlp.Add(new SqlParameter("@Wednesday", wednesday));
                    sqlp.Add(new SqlParameter("@Thursday", thursday));
                    sqlp.Add(new SqlParameter("@Friday", friday));
                    sqlp.Add(new SqlParameter("@Saturday", saturday));
                    sqlp.Add(new SqlParameter("@Sunday", sunday));
                    sqlp.Add(new SqlParameter("@HeaderLine1", txtPrintLineOne.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@HeaderLine2", txtPrintLineTwo.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@BottomLine", txtBottomLine.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@CompanyLogo", bytes));

                    bool i2 = conAdd.SaveData(sqlp);
                    if (i2 == true)
                    {
                        try
                        {
                            SqlCommand cmd2;
                            int i = 1;
                            con.Open();
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                cmd2 = con.CreateCommand();
                                cmd2.CommandText = "DoctorFormDetailsAdd";
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd2.Parameters.Add("@CDoctorId", SqlDbType.BigInt).Value = globalId;
                                cmd2.Parameters.Add("@Srno", SqlDbType.Int).Value = i;
                                int b = Convert.ToInt32(row.Cells[0].Text);
                                cmd2.Parameters.Add("@FormId", SqlDbType.BigInt).Value = b;
                                cmd2.Parameters.Add("@SortingSeq", SqlDbType.Int).Value = ((TextBox)row.Cells[2].FindControl("TextBoxS")).Text;
                                cmd2.ExecuteNonQuery();
                                i++;
                            }
                            con.Close();
                            Response.Redirect("DoctorMaster.aspx");
                        }
                        catch (Exception)
                        {
                            con.Close();
                            Response.Write("<script>");
                            Response.Write("alert('Doctor Not Added.');");
                            Response.Write("</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Doctor Not Added.');");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    ConnectionClass conAdd = new ConnectionClass("AdminDoctorUpdate");
                    ConnectionClass congetMax = new ConnectionClass();
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@CoLoginId", Session["doctorid"].ToString()));
                    sqlp.Add(new SqlParameter("@CoSalute", cmbSalute.SelectedValue.ToString()));
                    sqlp.Add(new SqlParameter("@CoFirstName", txtFirstName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoMiddleName", txtMiddleName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@CoLastName", txtDLastName.Text.ToString().Trim().ToUpper()));
                    sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
                    sqlp.Add(new SqlParameter("@DrEmail", txtDocEmail.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrDegree", txtDocDegree.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrMobile", txtDocMobile.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@DrAddress", txtDocAddress.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@SpecializationId", cmbSpecialization.SelectedValue.ToString()));
                    sqlp.Add(new SqlParameter("@ApptOfNoOfDays", Convert.ToInt16(txtAppNoOfDays.Text)));
                    sqlp.Add(new SqlParameter("@FirstVisitCharge", txtFirstVistCharge.Text.ToString()));
                    sqlp.Add(new SqlParameter("@RoutineVisitCharge", txtRoutineVCharge.Text.ToString()));
                    sqlp.Add(new SqlParameter("@ValidityOfFirstChargeDay", Convert.ToInt16(txtValidityDay.Text)));
                    sqlp.Add(new SqlParameter("@MorningSession", sessionMor));
                    sqlp.Add(new SqlParameter("@PatientPerShiftMorning", patMor));
                    sqlp.Add(new SqlParameter("@EveningSession", sessionEve));
                    sqlp.Add(new SqlParameter("@PatientPerShiftEvening", patEve));
                    sqlp.Add(new SqlParameter("@MorningTimeFrom", MorFrom));
                    sqlp.Add(new SqlParameter("@MorningTimeTo", MorTo));
                    sqlp.Add(new SqlParameter("@EveningTimeFrom", EveFrom));
                    sqlp.Add(new SqlParameter("@EveningTimeTo", EveTo));
                    sqlp.Add(new SqlParameter("@Monday", monday));
                    sqlp.Add(new SqlParameter("@Tuesday", tuesday));
                    sqlp.Add(new SqlParameter("@Wednesday", wednesday));
                    sqlp.Add(new SqlParameter("@Thursday", thursday));
                    sqlp.Add(new SqlParameter("@Friday", friday));
                    sqlp.Add(new SqlParameter("@Saturday", saturday));
                    sqlp.Add(new SqlParameter("@Sunday", sunday));
                    sqlp.Add(new SqlParameter("@HeaderLine1", txtPrintLineOne.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@HeaderLine2", txtPrintLineTwo.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@BottomLine", txtBottomLine.Text.ToString().Trim()));
                    sqlp.Add(new SqlParameter("@CompanyLogo", bytes));


                    bool i2 = conAdd.SaveData(sqlp);

                    if (i2 == true)
                    {
                        try
                        {
                            SqlCommand cmd2;
                            int i = 1;
                            con.Open();
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                cmd2 = con.CreateCommand();
                                cmd2.CommandText = "DoctorFormDetailsAdd";
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd2.Parameters.Add("@CDoctorId", SqlDbType.BigInt).Value = Session["doctorid"].ToString();
                                cmd2.Parameters.Add("@Srno", SqlDbType.Int).Value = i;
                                int b = Convert.ToInt32(row.Cells[0].Text);
                                cmd2.Parameters.Add("@FormId", SqlDbType.BigInt).Value = b;
                                cmd2.Parameters.Add("@SortingSeq", SqlDbType.Int).Value = ((TextBox)row.Cells[2].FindControl("TextBoxS")).Text;
                                cmd2.ExecuteNonQuery();
                                i++;
                            }
                            con.Close();
                            Response.Redirect("DoctorMaster.aspx");
                        }
                        catch (Exception)
                        {
                            con.Close();
                            Response.Write("<script>");
                            Response.Write("alert('Doctor Not Added.');");
                            Response.Write("</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Doctor Not Updated.');");
                        Response.Write("</script>");
                    }
                }
            }
        }
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

    protected void CreateUserId(object sender, EventArgs e)
    {
        if (Session["doctorid"] == null)
        {
            string uid = "";
            if (txtFirstName.Text.ToString() != "")
                uid = txtFirstName.Text.Substring(0, 1);
            if (txtMiddleName.Text.ToString() != "")
                uid += txtMiddleName.Text.Substring(0, 1);
            if (txtDLastName.Text.ToString() != "")
                uid += txtDLastName.Text.Substring(0, 1);

            ConnectionClass con = new ConnectionClass("AdminCreateDoctorLoginId");
            txtLoginId.Text = con.DoctorUserIdCreate(uid, Convert.ToInt32(Session["companyid"].ToString()));
            
        }
        
    }
  
    protected string TimeMorningFrom { get; set; }
    protected string TimeMorningTo { get; set; }

    protected string TimeEveningFrom { get; set; }
    protected string TimeEveningTo { get; set; }

    protected void onCancelClick(object sender, EventArgs e)
    {
        Session.Remove("companyid");
        Session.Remove("doctorid");
        Session.Remove("imageupdate");
        Response.Redirect("CompanyDisplay.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string DeleteAdmin(string docid, string userid)
    {
        string retval = "";
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "CoLoginId"));
        sqlp.Add(new SqlParameter("@TableName", "CompanyLoginMaster"));
        sqlp.Add(new SqlParameter("@TableId", docid));
        sqlp.Add(new SqlParameter("@DeleteId", userid));

        ConnectionClass conD = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlpD = new List<SqlParameter>();
        sqlpD.Add(new SqlParameter("@TableFieldName", "CoLoginId"));
        sqlpD.Add(new SqlParameter("@TableName", "CompanyDoctorMaster"));
        sqlpD.Add(new SqlParameter("@TableId", docid));
        sqlpD.Add(new SqlParameter("@DeleteId", userid));

        bool i = con.DeleteAdminData(sqlp);
        bool j = con.DeleteAdminData(sqlpD);
        if (j == true)
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }
        // }
        return retval;
    }

    protected void chkMorningSession_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMorningSession.Checked == true)
        {
            txtPatientPSMorning.Text = "";
            txtPatientPSMorning.Enabled = true;
        }
        else
        {
            txtPatientPSMorning.Text = "0";
            txtPatientPSMorning.Enabled = false;
        }
    }
    protected void chkEveningSession_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEveningSession.Checked == true)
        {
            txtPatientPSEvening.Text = "";
            txtPatientPSEvening.Enabled = true;
        }
        else
        {
            txtPatientPSEvening.Text = "0";
            txtPatientPSEvening.Enabled = false;
        }
    }
}