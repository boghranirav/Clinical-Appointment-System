using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_AppointmentEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                spanMor.Visible = false;
                spanEve.Visible = false;
                combo_fill_Doctor();
                fill_Patient_Info();
                cmb_Fill_Area();
                cmb_Fill_RefDoctor();
                cmb_Fill_Cetegory();

                txtBirthDate.Attributes["min"] = DateTime.Parse("01-01-1900").ToString("yyyy-MM-dd");
                txtBirthDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                txtEnrollDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");


                txtAge.Attributes["min"] = "0";
                txtAge.Attributes["max"] = "150";
            }
        }
    }
   
    public void combo_fill_Doctor()
    {
        ConnectionClass conPInfo = new ConnectionClass("RecDoctorInformation");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];

        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            cmbDoctor.Items.Add(new ListItem(drUserInfo["CoFirstName"].ToString() + " " + drUserInfo["CoMiddleName"].ToString() + " " + drUserInfo["CoLastName"].ToString(), drUserInfo["CoLoginId"].ToString()));
        }
    }

    protected void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblSelectedDate.Text = "";
            lblMorningApp.Text = "";
            lblEveningApp.Text = "";
            gridMorningApp.DataSource = null;
            gridMorningApp.DataBind();

            gridEveningApp.DataSource = null;
            gridEveningApp.DataBind();

            if (cmbDoctor.SelectedValue == "SELECT")
            {
                grid_Date.DataSource = null;
                grid_Date.DataBind();
                spanMor.Visible = false;
                spanEve.Visible = false;
                lblPatientMor.Text = "Morning Shift";
                lblPatientEve.Text = "Evening Shift";
            }
            else
            {
                ConnectionClass conc = new ConnectionClass("AdminDisplayById");
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@TableName", "CompanyDoctorMaster"));
                sqlp.Add(new SqlParameter("@FieldName", "CoLoginId"));
                sqlp.Add(new SqlParameter("@TableId", cmbDoctor.SelectedValue.ToString()));

                DataTable dt = new DataTable();
                dt = conc.DisplayUserData(sqlp).Tables[0];
                string dayMon = "", dayTue = "", dayWed = "", dayThu = "", dayFri = "", daySat = "", daySun = "";
                int noofday = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PatientPerShift_Morning"].ToString()) > 0)
                    {
                        lblDoctorName.Text = "Appointment Of " + cmbDoctor.SelectedItem.ToString();
                        lblPatientMor.Text = "Patient In Morning Shift :" + dr["PatientPerShift_Morning"].ToString();
                        lblPatientMor.ForeColor = System.Drawing.Color.White;
                        ViewState["mTimeFrom"] = Convert.ToDateTime(dr["MorningTimeFrom"].ToString()).ToString("hh:mm tt");
                        ViewState["mTimeTo"] = Convert.ToDateTime(dr["MorningTimeTo"].ToString()).ToString("hh:mm tt");
                        lblDocTime.InnerHtml = "Morning Time : " + ViewState["mTimeFrom"] + " to " + ViewState["mTimeTo"];
                        spanMor.Visible = true;
                    }
                    else
                    {
                        lblPatientMor.Text = "No Patient In Morning";
                        //lblPatientMor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#cdef1d");
                        spanMor.Visible = false;
                    }

                    if (Convert.ToInt32(dr["PatientPerShift_Evening"].ToString()) > 0)
                    {
                        lblPatientEve.Text = "Patient In Evening Shift :" + dr["PatientPerShift_Evening"].ToString();
                        lblPatientEve.ForeColor = System.Drawing.Color.White;
                        ViewState["eTimeFrom"] = Convert.ToDateTime(dr["EveningTimeFrom"].ToString()).ToString("hh:mm tt");
                        ViewState["eTimeTo"] = Convert.ToDateTime(dr["EveningTimeTo"].ToString()).ToString("hh:mm tt");

                        lblDocTime.InnerHtml = lblDocTime.InnerHtml + "</br> Evening Time : " + ViewState["eTimeFrom"].ToString() + " to " + ViewState["eTimeTo"];

                        //   txtMorTime.Attributes["min"] = DateTime.Parse(dr["EveningTimeFrom"].ToString()).ToString();
                        //   txtMorTime.Attributes["max"] = DateTime.Parse(dr["EveningTimeTo"].ToString()).ToString();
                        spanEve.Visible = true;
                    }
                    else
                    {
                        lblPatientEve.Text = "No Patient In Evening";
                        //lblPatientEve.ForeColor = System.Drawing.ColorTranslator.FromHtml("#cdef1d");
                        spanEve.Visible = false;
                    }

                    noofday = Convert.ToInt32(dr["ApptOfNoOfDays"].ToString());
                    dayMon = dr["Monday"].ToString();
                    dayTue = dr["Tuesday"].ToString();
                    dayWed = dr["Wednesday"].ToString();
                    dayThu = dr["Thursday"].ToString();
                    dayFri = dr["Friday"].ToString();
                    daySat = dr["Saturday"].ToString();
                    daySun = dr["Sunday"].ToString();
                    break;
                }

                List<DateTime> allDates = new List<DateTime>();

                for (int i = 0; i <= noofday; i++)
                {
                    string dayname = DateTime.Now.AddDays(i).DayOfWeek.ToString();
                    switch (dayname)
                    {
                        case "Monday":
                            if (dayMon.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Tuesday":
                            if (dayTue.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Wednesday":
                            if (dayWed.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Thrusday":
                            if (dayThu.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Friday":
                            if (dayFri.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Saturday":
                            if (daySat.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                        case "Sunday":
                            if (daySun.Equals("True")) { allDates.Add(DateTime.Now.AddDays(i)); }
                            break;
                    }
                }


                DataTable dtData = new DataTable();
                dtData.Columns.Add("Date");
                dtData.Columns.Add("DateId");
                dtData.Columns.Add("MorningCount");
                dtData.Columns.Add("EveningCount");
                int count = 0;
                foreach (DateTime dates in allDates)
                {
                    DataRow dr = dtData.NewRow();
                    dr["Date"] = DateTime.Parse(dates.ToString()).ToString("dd-MM-yy");
                    dr["MorningCount"] = countPatientMorning(0, Convert.ToDateTime(dates.ToString()).ToString("yyyy-MM-dd")).ToString();
                    dr["EveningCount"] = countPatientMorning(1, Convert.ToDateTime(dates.ToString()).ToString("yyyy-MM-dd")).ToString();
                    dr["DateId"] = count;
                    count += 1;
                    dtData.Rows.Add(dr);
                }
                grid_Date.DataSource = dtData.Copy();
                grid_Date.DataBind();


                //Fill Patients
            }
        }
        catch (Exception) { }
    }
  
    protected void grid_Date_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["selectedDate"] =Convert.ToDateTime(grid_Date.SelectedRow.Cells[0].Text).ToString("yyyy-MM-dd");
            lblMDate.Text = grid_Date.SelectedRow.Cells[0].Text;
            lblSelectedDate.Text = "Selected Date : " + grid_Date.SelectedRow.Cells[0].Text;
            fillMorningAppointments();
            fillEveningAppointments();
            //countPatientMorning();
        }
        catch (Exception) { }
    }

    protected void fill_Patient_Info()
    {
        ConnectionClass conPInfo = new ConnectionClass("displayUserDateOrderBy");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@TableName", "PatientMaster"));
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlPInfo.Add(new SqlParameter("@OrderByField", "PatientName"));

        DataTable dtUserInfo = new DataTable();
        dtUserInfo = conPInfo.DisplayUserData(sqlPInfo).Tables[0];
        
        foreach (DataRow drUserInfo in dtUserInfo.Rows)
        {
            cmbSelectPatient.Items.Add(new ListItem(drUserInfo["PatientName"].ToString(), drUserInfo["PatientId"].ToString()));
       
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            List<SqlParameter> sqlp = new List<SqlParameter>();
            string session = lblAppShift.Text;
            bool valid = false,areaStatus=false,refDocStatus=false;
            ConnectionClass conAdd=null;
            string ptype = "", bgroup = "";
            int shift = 0;
            if (session.ToString().Equals("Morning")) { shift = 0; }
            if (session.ToString().Equals("Evening")) { shift = 1; }

           
                if (shift == 0)
                {

                    if (DateTime.Parse(txtTime.Text.ToString()) > DateTime.Parse(ViewState["mTimeFrom"].ToString()) && DateTime.Parse(txtTime.Text.ToString()) < DateTime.Parse(ViewState["mTimeTo"].ToString())) { valid = true; }
                    else { ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Invalid Time');", true); }
                }

                if (shift == 1)
                {
                    if (DateTime.Parse(txtTime.Text.ToString()) > DateTime.Parse(ViewState["eTimeFrom"].ToString()) && DateTime.Parse(txtTime.Text.ToString()) < DateTime.Parse(ViewState["eTimeTo"].ToString())) { valid = true; }
                    else { ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Invalid Time');", true); }
                }

                if (radioAdult.Checked == true) { ptype = "ADULT"; } else { ptype = "CHILD"; }

                if (cmbBloodGroup.SelectedValue.ToString().Equals("SELECT"))
                {
                    bgroup = "";
                }
                else
                {
                    bgroup = cmbBloodGroup.SelectedValue.ToString();
                }
             
                ConnectionClass conCheckDuplicate = new ConnectionClass("RecCheckAppointmentTime");
                List<SqlParameter> sqlpCheckDuplicate = new List<SqlParameter>();
                sqlpCheckDuplicate.Add(new SqlParameter("@AppointmentTime", DateTime.Parse(txtTime.Text.ToString()).ToString("hh:mm tt")));
                sqlpCheckDuplicate.Add(new SqlParameter("@AppointmentDate", ViewState["selectedDate"].ToString()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CoLoginId", cmbDoctor.SelectedValue.ToString()));
                sqlpCheckDuplicate.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            
                int i = conCheckDuplicate.CountRecords(sqlpCheckDuplicate);
                if (i == 0)
                {
                    if (Page.IsValid && valid == true)
                    {
                        if (cmbArea.SelectedValue == "SELECT" && txtNewArea.Text != "")
                        {
                            areaStatus = AddNewArea();
                        }
                        else
                        {
                            ViewState["VSAreaId"] = cmbArea.SelectedValue.ToString();
                            areaStatus = true;
                        }

                        if (areaStatus == true)
                        {
                            if (cmbRefDoctor.SelectedValue == "SELECT" && txtNewRefDoctor.Text != "")
                            {
                                refDocStatus = AddNewRefDoctor();
                            }
                            else
                            {
                                ViewState["VSRefDoc"] = cmbRefDoctor.SelectedValue.ToString();
                                refDocStatus = true;
                            }
                        }


                        if (areaStatus == true && refDocStatus == true)
                        {
                            if (ViewState["VSAppointID"] != null)
                            {
                                conAdd = new ConnectionClass("RecAppointmentEntryUpdate");
                                sqlp.Add(new SqlParameter("@PatientId", cmbSelectPatient.SelectedValue.ToString()));
                                sqlp.Add(new SqlParameter("@AppointmentId", ViewState["VSAppointID"]));
                            }
                            else
                            if (cmbSelectPatient.SelectedValue == "SELECT")
                            {
                                Byte[] bytes = null;
                                bytes = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                                conAdd = new ConnectionClass("RecAppointmentEntryAddNewPatient");
                                sqlp.Add(new SqlParameter("@PatientImage", bytes));
                            }
                            else
                            {
                                conAdd = new ConnectionClass("RecAppointmentEntryAdd");
                                sqlp.Add(new SqlParameter("@PatientId", cmbSelectPatient.SelectedValue.ToString()));
                            }


                            string appointDate = ViewState["selectedDate"].ToString();
                            string appointTime = DateTime.Parse(txtTime.Text.ToString()).ToString("hh:mm tt");

                            ConnectionClass congetMax = new ConnectionClass();

                            if (ViewState["VSAppointID"] == null)
                            {
                                sqlp.Add(new SqlParameter("@AppointmentId", congetMax.GetGlobalId()));
                                sqlp.Add(new SqlParameter("@AppointmentCode", congetMax.GetMaxTableCode("AppointmentEntry", "AppointmentCode")));
                                sqlp.Add(new SqlParameter("@CoLoginId", cmbDoctor.SelectedValue.ToString()));
                                sqlp.Add(new SqlParameter("@EntryDate", DateTime.Now.ToString()));
                                sqlp.Add(new SqlParameter("@ShiftNumber", shift));
                                sqlp.Add(new SqlParameter("@ApptStatus", "1"));
                            }
                            
                            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                            sqlp.Add(new SqlParameter("@AppointmentDate", appointDate));
                            sqlp.Add(new SqlParameter("@AppointmentTime", appointTime));
                            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

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

                            if (txtEnrollDate.Text == "")
                            {
                                sqlp.Add(new SqlParameter("@PatientEnrollDate", DBNull.Value));
                            }
                            else
                            {
                                sqlp.Add(new SqlParameter("@PatientEnrollDate", Convert.ToDateTime(txtEnrollDate.Text.ToString())));
                            }

                            if (cmbRefDoctor.SelectedValue.ToString().Equals("SELECT") && ViewState["VSRefDoc"].ToString() == "SELECT")
                            {
                                sqlp.Add(new SqlParameter("@ReferenceId", DBNull.Value));
                            }
                            else
                            {
                                sqlp.Add(new SqlParameter("@ReferenceId", ViewState["VSRefDoc"].ToString()));
                            }

                            try
                            {
                                sqlp.Add(new SqlParameter("@PatientBirthDate", Convert.ToDateTime(txtBirthDate.Text.ToString())));
                            }
                            catch (Exception)
                            {
                                sqlp.Add(new SqlParameter("@PatientBirthDate", DBNull.Value));
                            }
                            string ss = ViewState["VSAreaId"].ToString();
                            if (cmbArea.SelectedValue.ToString().Equals("SELECT") && ss == "SELECT")
                            {
                                sqlp.Add(new SqlParameter("@AreaId", DBNull.Value));
                            }
                            else
                            {
                                sqlp.Add(new SqlParameter("@AreaId", ViewState["VSAreaId"].ToString()));
                            }

                            if (cmbCategory.ToString().Equals("SELECT"))
                            {
                                sqlp.Add(new SqlParameter("@CategoryId", DBNull.Value));
                            }
                            else
                            {
                                sqlp.Add(new SqlParameter("@CategoryId", Convert.ToInt32(cmbCategory.SelectedValue.ToString())));
                            }


                            bool i2 = conAdd.SaveData(sqlp);
                            if (i2 == true)
                            {
                                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Appointment is Booked.');", true);
                                //   ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                                //   "alert('Appointment Is Booked.'); window.location='" +
                                //   Request.ApplicationPath + "Receptionist/AppointmentEntry.aspx';", true);
                                if (session.ToString().Equals("Morning")) { fillMorningAppointments(); }
                                if (session.ToString().Equals("Evening")) { fillEveningAppointments(); }
                                clearFields();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosePopup();", true);
                            }
                        }
                        else
                            if (areaStatus == false)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                            }
                            else if (refDocStatus == false)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                            }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + txtTime.Text + " is Booked. Please Select Another Time.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
        }
        catch (Exception) { }
    }

    protected void fillMorningAppointments()
    {

        string appointDate =  ViewState["selectedDate"].ToString();
        ConnectionClass conPInfo = new ConnectionClass("displayAppointments");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlPInfo.Add(new SqlParameter("@DoctorId", cmbDoctor.SelectedValue.ToString()));
        sqlPInfo.Add(new SqlParameter("@ShiftNumber", "0"));
        sqlPInfo.Add(new SqlParameter("@AppointmentDate", appointDate));

        gridMorningApp.DataSource = conPInfo.DisplayUserData(sqlPInfo).Tables[0];
        gridMorningApp.DataBind();
        if (gridMorningApp.Rows.Count == 0)
        {
            lblMorningApp.Text = "No Appointment Found.";
        }
        else
        {
            lblMorningApp.Text = "";
        }
    }

    protected void fillEveningAppointments()
    {
        string appointDate = ViewState["selectedDate"].ToString();
        ConnectionClass conPInfo = new ConnectionClass("displayAppointments");
        List<SqlParameter> sqlPInfo = new List<SqlParameter>();
        sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlPInfo.Add(new SqlParameter("@DoctorId", cmbDoctor.SelectedValue.ToString()));
        sqlPInfo.Add(new SqlParameter("@ShiftNumber", "1"));
        sqlPInfo.Add(new SqlParameter("@AppointmentDate", appointDate));

        gridEveningApp.DataSource = conPInfo.DisplayUserData(sqlPInfo).Tables[0];
        gridEveningApp.DataBind();

        if (gridEveningApp.Rows.Count == 0)
        {
            lblEveningApp.Text = "No Appointment Found.";
        }
        else
        {
            lblEveningApp.Text = "";
        }
    }

    protected int countPatientMorning(int shiftno,string sdate)
    {
        ConnectionClass conCount = new ConnectionClass("RecCountPatientDoctor");
        List<SqlParameter> sqlpCount = new List<SqlParameter>();
        sqlpCount.Add(new SqlParameter("@CoLoginId", cmbDoctor.SelectedValue.ToString()));
        sqlpCount.Add(new SqlParameter("@AppointmentDate", sdate));
        sqlpCount.Add(new SqlParameter("@ShiftNumber", shiftno));
        sqlpCount.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        int i = conCount.CountRecords(sqlpCount);

        return i;
    }

    protected void gridMorningApp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gridMorningApp.DataKeys[e.RowIndex].Value);
                
        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "AppointmentId"));
        sqlp.Add(new SqlParameter("@TableName", "AppointmentEntry"));
        sqlp.Add(new SqlParameter("@TableId", id));
        sqlp.Add(new SqlParameter("@DeleteId", Session["CoLoginId"].ToString()));

        bool i = con.DeleteAdminData(sqlp);
        if (i == true)
        {
            fillMorningAppointments();
     
        }

    }
    protected void gridMorningApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item3 = e.Row.Cells[2].Text;
            foreach (TableCell cell in e.Row.Cells)
            {
                if (item3.Equals("True"))
                {
                    cell.BackColor = Color.Red;
                    e.Row.Cells[2].Text = "OFF";
                }
                else
                {
                    e.Row.Cells[2].Text = "ON";
                }
            }

            string item = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                {
                    button.Attributes["onclick"] = "if(!confirm('Do you want to delete Appointment Of "+ item+" ')){ return false; };";
                }
            }
        }
    }

    protected void cmb_Fill_Area()
    {
        ConnectionClass conArea = new ConnectionClass("displayUserDateOrderBy");
        List<SqlParameter> sqlArea = new List<SqlParameter>();
        sqlArea.Add(new SqlParameter("@TableName", "AreaMaster"));
        sqlArea.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlArea.Add(new SqlParameter("@OrderByField", "AreaName"));

        DataTable dtArea = new DataTable();
        dtArea = conArea.DisplayUserData(sqlArea).Tables[0];
        cmbArea.Items.Clear();
        cmbArea.Items.Add(new ListItem("SELECT", "SELECT"));
        foreach (DataRow drUserInfo in dtArea.Rows)
        {
            cmbArea.Items.Add(new ListItem(drUserInfo["AreaName"].ToString(), drUserInfo["AreaId"].ToString()));
        }
    }

    protected void cmb_Fill_RefDoctor()
    {
        ConnectionClass conRInfo = new ConnectionClass("displayUserDateOrderBy");
        List<SqlParameter> sqlRInfo = new List<SqlParameter>();
        sqlRInfo.Add(new SqlParameter("@TableName", "ReferenceDoctor"));
        sqlRInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
        sqlRInfo.Add(new SqlParameter("@OrderByField", "RefName"));


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

    protected void btnAddNewArea_Click(object sender, EventArgs e)
    {
        cmbArea.SelectedValue = "SELECT";
        divCmbArea.Visible = false;
        divArea.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnCnacelArea_Click(object sender, EventArgs e)
    {
        divCmbArea.Visible = true;
        divArea.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnAddRefDoc_Click(object sender, EventArgs e)
    {
        cmbRefDoctor.SelectedValue = "SELECT";
        divCmbRefDoc.Visible = false;
        divtxtRefDoc.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnCancelRefDoc_Click(object sender, EventArgs e)
    {
        divCmbRefDoc.Visible = true;
        divtxtRefDoc.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    protected void clearFields()
    {
        ViewState["VSAppointID"] = null;
        ViewState["VSAppTime"] = null;
        txtTime.Text = "";
        txtName.Text = "";
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtAge.Text = "";
        cmbAgeUnit.SelectedValue = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        cmbBloodGroup.SelectedValue = "";
        txtAllergy.Text = "";
        txtEnrollDate.Text = "";
        cmbSelectPatient.SelectedValue = "SELECT";
        cmbBloodGroup.SelectedValue = "SELECT";
        cmbRefDoctor.SelectedValue = "SELECT";
        cmbArea.SelectedValue = "SELECT";
        cmbCategory.SelectedValue = "SELECT";
    }
   
    protected void cmbSelectPatient_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbSelectPatient.SelectedValue == "SELECT")
            {
                clearFields();
            }
            else
            {
                patient_details(cmbSelectPatient.SelectedValue.ToString());
            }
        }
        catch (Exception) { }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    protected void patient_details(string patientId)
    {
        ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableName", "PatientMaster"));
        sqlp.Add(new SqlParameter("@FieldName", "PatientId"));
        sqlp.Add(new SqlParameter("@TableId", patientId));
        sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dt = new DataTable();
        dt = conc.DisplayUserData(sqlp).Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            txtName.Text = dr["PatientName"].ToString();
            txtAddress.Text = dr["PatientAddress"].ToString();

            cmbGender.SelectedValue = dr["PatientGender"].ToString();
            if (dr["PatentType"].ToString().Equals("CHILD"))
            {
                radioChild.Checked = true;
            }
            else
            {
                radioAdult.Checked = true;
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
            string ref1 = dr["ReferenceId"].ToString();
            if (dr["ReferenceId"].ToString() != "")
            {
                cmbRefDoctor.SelectedValue = dr["ReferenceId"].ToString();
            }
            else
            {
                cmbRefDoctor.SelectedValue = "SELECT";
            }
            if (dr["AreaId"].ToString() != "")
            {
                cmbArea.SelectedValue = dr["AreaId"].ToString();
            }
            else
            {
                cmbArea.SelectedValue = "SELECT";
            }
            if (dr["CategoryId"].ToString() != "")
            {
                cmbCategory.SelectedValue = dr["CategoryId"].ToString();
            }
            else
            {
                cmbCategory.SelectedValue = "SELECT";
            }
        }
    }
    public bool AddNewArea()
    {
        try
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
                ViewState["VSAreaId"] = congetMax.GetGlobalId();
                sqlp.Add(new SqlParameter("@AreaId", ViewState["VSAreaId"].ToString()));
                sqlp.Add(new SqlParameter("@AreaCode", congetMax.GetMaxTableCode("AreaMaster", "AreaCode")));
                sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlp.Add(new SqlParameter("@AreaName", txtNewArea.Text.ToString().ToUpper()));
                sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));

                bool i2 = conAdd.SaveData(sqlp);
                if (i2 == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                divCmbArea.Visible = true;
                divArea.Visible = false;
                txtNewArea.Text = "";
                lblDuplicateArea.Text = "Area You Entered Already Exists Select And Proceed.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected bool AddNewRefDoctor()
    {
        try
        {
            ConnectionClass conAdd = new ConnectionClass("RecRefDcotorMaster");
            ConnectionClass congetMax = new ConnectionClass();

            List<SqlParameter> sqlp = new List<SqlParameter>();
            ViewState["VSRefDoc"] = congetMax.GetGlobalId();
            sqlp.Add(new SqlParameter("@ReferenceId", ViewState["VSRefDoc"].ToString()));
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
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception) { return false; }
    }

    protected void gridEveningApp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gridEveningApp.DataKeys[e.RowIndex].Value);

        ConnectionClass con = new ConnectionClass("AdminDataDelete");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableFieldName", "AppointmentId"));
        sqlp.Add(new SqlParameter("@TableName", "AppointmentEntry"));
        sqlp.Add(new SqlParameter("@TableId", id));
        sqlp.Add(new SqlParameter("@DeleteId", Session["CoLoginId"].ToString()));

        bool i = con.DeleteAdminData(sqlp);
        if (i == true)
        {
            fillEveningAppointments();

        }
    }

    protected void gridMorningApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearFields();
            btnCancelAppointment.Visible = true;
            string appointID = gridMorningApp.SelectedValue.ToString();
            cmbSelectPatient.Attributes["disabled"] = "true";
            lblAppShift.Text = "Morning";
            fill_dialog_for_update(appointID);
            btnSubmit.Text = "UPDATE";
        }
        catch (Exception) { }
    }
    protected void gridEveningApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearFields();
            btnCancelAppointment.Visible = true;
            string appointID = gridEveningApp.SelectedValue.ToString();
            cmbSelectPatient.Attributes["disabled"] = "true";
            lblAppShift.Text = "Evening";
            fill_dialog_for_update(appointID);
            btnSubmit.Text = "UPDATE";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        catch (Exception) { }
    }
    
    protected void fill_dialog_for_update(string appointID)
    {
        ViewState["VSAppointID"]=appointID;
        ConnectionClass conc = new ConnectionClass("DisplayUserByIdComp");
        List<SqlParameter> sqlp = new List<SqlParameter>();
        sqlp.Add(new SqlParameter("@TableName", "AppointmentEntry"));
        sqlp.Add(new SqlParameter("@FieldName", "AppointmentId"));
        sqlp.Add(new SqlParameter("@TableId", appointID));
        sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));

        DataTable dt = new DataTable();
        dt = conc.DisplayUserData(sqlp).Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            cmbSelectPatient.SelectedValue = dr["PatientId"].ToString();
            patient_details(dr["PatientId"].ToString());
            ViewState["VSAppTime"] = dr["AppointmentTime"].ToString();
            txtTime.Text = dr["AppointmentTime"].ToString();
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    protected void btnSelectPatient_Click(object sender, EventArgs e)
    {
        clearFields();
        btnCancelAppointment.Visible = false;
        lblAppShift.Text = "Morning";
        cmbSelectPatient.Attributes.Remove("disabled");
        btnSubmit.Text = "BOOK";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnSelectPatientEvening_Click(object sender, EventArgs e)
    {
        clearFields();
        btnCancelAppointment.Visible = false;
        lblAppShift.Text = "Evening";
        cmbSelectPatient.Attributes.Remove("disabled");
        btnSubmit.Text = "BOOK";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnCancelAppointment_Click(object sender, EventArgs e)
    {
        txtReasonCancel.Attributes["maxlength"] = "100";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalAppCancel();", true);
    }
    protected void btnCancelApp_Click(object sender, EventArgs e)
    {
        try
        {
            ConnectionClass conAdd = new ConnectionClass("RecCancelAppointment");
            ConnectionClass congetMax = new ConnectionClass();


            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@AppointmentId", ViewState["VSAppointID"].ToString()));
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            if (System.Text.RegularExpressions.Regex.IsMatch(txtReasonCancel.Text.ToString().Trim(), @"^[a-zA-Z0-9,.\s-\\\/ ]+$"))
            {
                sqlp.Add(new SqlParameter("@CancelReason", txtReasonCancel.Text.ToString().Trim()));
            }
            else
            {
                string r = txtReasonCancel.Text.ToString().Trim();
                r = r.Replace("'", " ");
                r = r.Replace("\"", " ");
                sqlp.Add(new SqlParameter("@CancelReason", r.Trim()));
            }
            sqlp.Add(new SqlParameter("@EditId", Session["CoLoginId"].ToString()));

            bool i2 = conAdd.SaveData(sqlp);
            if (i2 == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosePopupAppCancel();", true);
                txtReasonCancel.Text = "";
                if (lblAppShift.Text.Equals("Morning"))
                {
                    fillMorningAppointments();
                }
                if (lblAppShift.Text.Equals("Evening"))
                {
                    fillEveningAppointments();
                }
            }
        }
        catch (Exception) { }
    }
    protected void gridMorningApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMorningApp.PageIndex = e.NewPageIndex;
        gridMorningApp.DataBind();
        fillMorningAppointments();
    }
}