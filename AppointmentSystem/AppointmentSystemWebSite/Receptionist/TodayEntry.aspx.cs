using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Receptionist_TodayEntry : System.Web.UI.Page
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
                combo_fill_Doctor();
                lblTodayDate.Text = DateTime.Now.ToShortDateString();
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

    protected void fillAppointments()
    {
        try
        {
            if (cmbDoctor.SelectedValue.ToString() != "SELECT" && cmbShift.SelectedValue.ToString() != "SELECT")
            {
                string appointDate = DateTime.Now.ToShortDateString();
                ConnectionClass conPInfo = new ConnectionClass("displayTodayAppointment");
                List<SqlParameter> sqlPInfo = new List<SqlParameter>();
                sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlPInfo.Add(new SqlParameter("@DoctorId", cmbDoctor.SelectedValue.ToString()));
                sqlPInfo.Add(new SqlParameter("@ShiftNumber", cmbShift.SelectedValue.ToString()));
                sqlPInfo.Add(new SqlParameter("@AppointmentDate", appointDate));

                gridTodayAppointment.DataSource = conPInfo.DisplayUserData(sqlPInfo).Tables[0];
                gridTodayAppointment.DataBind();
            }
        }
        catch (Exception) { }
    }
    protected void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillAppointments();
        fillPresentEntry();
    }

    protected void gridTodayAppointment_SelectedIndexChanged(object sender, EventArgs e)
    {
        string appointID = gridTodayAppointment.SelectedValue.ToString();
        try
        {
            ConnectionClass conAdd = new ConnectionClass("RecPresentAppointmentEntry");

            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
            sqlp.Add(new SqlParameter("@AppointmentId", appointID));
            sqlp.Add(new SqlParameter("@LoginId", Session["CoLoginId"].ToString()));


            gridTodayAppointment.DataSource = conAdd.DisplayUserData(sqlp).Tables[0];
            gridTodayAppointment.DataBind();
            
        }
        catch (Exception) { }
    }

    protected void gridTodayAppointment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
            {
                DateTime appTime = DateTime.Parse ( e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = appTime.ToString("H") ;

                if (button.CommandName == "Select")
                {
                    button.Attributes["onclick"] = "if(!confirm('Do you want to Continue Appointment Of " + item + " ')){ return false; };";
                }
            }
        }
    }

    protected void fillPresentEntry()
    {
        try
        {
            if (cmbDoctor.SelectedValue.ToString() != "SELECT" && cmbShift.SelectedValue.ToString() != "SELECT")
            {
                string appointDate = DateTime.Now.ToShortDateString();
                ConnectionClass conPInfo = new ConnectionClass("displayPresentAptPatient");
                List<SqlParameter> sqlPInfo = new List<SqlParameter>();
                sqlPInfo.Add(new SqlParameter("@CompanyId", Session["companyid"].ToString()));
                sqlPInfo.Add(new SqlParameter("@DoctorId", cmbDoctor.SelectedValue.ToString()));
                sqlPInfo.Add(new SqlParameter("@ShiftNumber", cmbShift.SelectedValue.ToString()));
                sqlPInfo.Add(new SqlParameter("@AppointmentDate", appointDate));

                gridPresentAppointment.DataSource = conPInfo.DisplayUserData(sqlPInfo).Tables[0];
                gridPresentAppointment.DataBind();
            }
        }
        catch (Exception) { }
    }
}