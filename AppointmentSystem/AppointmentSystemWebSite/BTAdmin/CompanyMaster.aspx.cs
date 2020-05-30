using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BTAdmin_CompanyMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["LoginUserId"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }

        ConnectionClass conMarketing = new ConnectionClass("displayData");
        DataTable dtMkt = new DataTable();
        dtMkt = conMarketing.DisplayData("MarketingMaster").Tables[0];
        

        if (Request.QueryString.AllKeys.Contains("fid"))
        {
            if (Request.QueryString["fid"].ToString().All(char.IsDigit))
            { }
            else
            {
                Response.Redirect("CompanyMaster.aspx");
            }

            foreach (DataRow drMkt in dtMkt.Rows)
            {
                cmbRefSelect.Items.Add(new ListItem(drMkt["MarketingName"].ToString(), drMkt["MarketingId"].ToString()));
            }

            btnCancel.Visible = true;
            submit.Text = "Update";
            string id = Request.QueryString["fid"];
            if (id == null)
            {
                Response.Redirect("CompanyMaster.aspx");
            }
            Session["fid"] = Request.QueryString["fid"];
            ConnectionClass conc = new ConnectionClass("AdminDisplayById");
            List<SqlParameter> sqlp = new List<SqlParameter>();
            sqlp.Add(new SqlParameter("@TableName", "CompanyMaster"));
            sqlp.Add(new SqlParameter("@FieldName", "CompanyId"));
            sqlp.Add(new SqlParameter("@TableId", id));

            DataTable dt = new DataTable();
            dt = conc.GetAdminDetailById(sqlp).Tables[0];

            if (dt.Rows.Count == 0) { Response.Redirect("CompanyMaster.aspx"); }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtCompanyCode.Text = dr["CompanyCode"].ToString();
                    txtCompanyName.Text = dr["CompanyName"].ToString();
                    txtAddress.Text = dr["CompanyAddress"].ToString();
                    txtPAN.Text = dr["CPAN"].ToString();
                    txtTAN.Text = dr["CTAN"].ToString();
                    txtCTIN.Text = dr["CTin"].ToString();
                    txtGTIN.Text = dr["CTin"].ToString();
                    if (dr["CompanyStatus"].ToString().Equals("REGULAR"))
                    { 
                        radioCompRegular.Checked = true;
                        txtNumberOfPat.Enabled = false;
                        span1.Attributes["style"] = "color:red;";
                        span2.Attributes["style"] = "color:black;";
                    }
                    else 
                    { 
                        radioCompDemo.Checked = true;
                        txtNumberOfPat.Enabled = true;
                        span1.Attributes["style"] = "color:black;";
                        span2.Attributes["style"] = "color:red;";
                    }
                    txtNumberOfPat.Text = dr["NoOfPatient"].ToString();
                    txtCompanyPhone.Text = dr["CompanyPhoneNo1"].ToString();
                    txtCompanyEmail.Text = dr["CompanyEmail"].ToString();
                    txtNumberOfDoc.Text = dr["NoOfDoctor"].ToString();
                    try
                    {
                        txtOperationDate.Value = DateTime.Parse(dr["LastOperationDate"].ToString()).ToString("dd-MM-yyyy");
                    }catch(Exception ){}
                    if  (dr["SendEmail"].ToString().Equals("True"))
                    { chkEmail.Checked = true;}
                    if (dr["SendSMS"].ToString().Equals("True"))
                    { chkSMS.Checked = true; }
                    try
                    {
                        txtCreateDate.Value = DateTime.Parse(dr["CompanyCreateDate"].ToString()).ToString("dd-MM-yyyy");
                    }catch(Exception ){}

                    if (dr["ReceiptOption"].ToString().Equals("False"))
                    {
                        radioRecCom.Checked = true;
                        span3.Attributes["style"] = "color:red;";
                        span4.Attributes["style"] = "color:black;";
                    }
                    else { 
                        radioRecDoc.Checked = true;
                        span4.Attributes["style"] = "color:red;";
                        span3.Attributes["style"] = "color:black;";
                    }

                    cmbRefSelect.SelectedValue = dr["MarketingId"].ToString();
                    txtReferBy.Text = dr["ReferenceBy"].ToString();
                    byte[] bytes;
                    try
                    {
                        bytes = (byte[])dr["CompanyImage"];
                        Session["imageupdate"] = bytes;
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        displayImg.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    catch (Exception)
                    {
                        bytes = null;
                    }
                    
                }
            }
        }
        else if (Request.QueryString.AllKeys.Length > 0)
        {
            Response.Redirect("CompanyMaster.aspx");
        }

        //Fill Marketing Master Combo
        

        if (!Request.QueryString.AllKeys.Contains("fid"))
        {
            if (!IsPostBack)
            {
                txtCompanyName.Focus();
                //Fill Company Code
                radioCompRegular.Checked = true;
                span1.Attributes["style"] = "color:red;";
                span3.Attributes["style"] = "color:red;";
                txtCreateDate.Value = DateTime.Now.ToString("dd-MM-yyyy");
                txtOperationDate.Value = DateTime.Now.ToString("dd-MM-yyyy");
                ConnectionClass conGetMax = new ConnectionClass();
                int compCode = conGetMax.GetMaxTableCode("CompanyMaster", "CompanyCode");
                if (compCode == 1)
                {
                    compCode = 1501;
                }
                txtCompanyCode.Text = compCode.ToString();

                foreach (DataRow drMkt in dtMkt.Rows)
                {
                    cmbRefSelect.Items.Add(new ListItem(drMkt["MarketingName"].ToString(), drMkt["MarketingId"].ToString()));
                }
            }
        }

    }

    protected void on_Submit_Click(object sender, EventArgs e)
    {
        string compStatus = "";
        int noOfPat, statusEmail, statusSMS, MktRefId,statusReceipt;
        if (radioCompRegular.Checked == true)
        {
            compStatus = "REGULAR";
            noOfPat = 0;
        }
        else
        {
            compStatus = "DEMO";
            noOfPat = Convert.ToInt16(txtNumberOfPat.Text.ToString());
        }

        if (cmbRefSelect.SelectedValue.ToString() == "SELECT")
        { MktRefId = 0; }
        else { MktRefId = Convert.ToInt32(cmbRefSelect.SelectedValue.ToString()); }

        if (chkEmail.Checked == true)
        { statusEmail = 1; }
        else { statusEmail = 0; }

        if (chkSMS.Checked == true)
        { statusSMS = 1; }
        else { statusSMS = 0; }

        if (radioRecCom.Checked == true)
        { statusReceipt = 0; }
        else { statusReceipt = 1; }

        if (submit.Text.ToString().Equals("Submit"))
        {
            String ext = imgCompanyImage.PostedFile.ContentType;
            if (ext.ToLower() == "image/jpeg" || ext.ToLower() == "image/jpg" || ext.ToLower() == "image/png")
            {
                ConnectionClass conAdd = new ConnectionClass("AdminCompanyAdd");
                ConnectionClass congetMax = new ConnectionClass();
                Byte[] bytes = null;
                if (imgCompanyImage.PostedFile.ContentLength > 0)
                {
                    Stream fs = imgCompanyImage.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
          
                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@CompanyId", congetMax.GetGlobalId()));
                sqlp.Add(new SqlParameter("@CompanyCode", txtCompanyCode.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyName", txtCompanyName.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyAddress", txtAddress.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyPhoneNo1", txtCompanyPhone.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CPAN", txtPAN.Text.ToString().Trim().ToUpper()));
                sqlp.Add(new SqlParameter("@CTAN", txtTAN.Text.ToString().Trim().ToUpper()));
                sqlp.Add(new SqlParameter("@CTin", txtCTIN.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@GTin", txtGTIN.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyEmail", txtCompanyEmail.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyImage", bytes));
                sqlp.Add(new SqlParameter("@ComapnyStatus", compStatus));
                sqlp.Add(new SqlParameter("@LastOperationDate", Convert.ToDateTime(txtOperationDate.Value)));
                sqlp.Add(new SqlParameter("@NoOfDoctor", txtNumberOfDoc.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@NoOfPatient", noOfPat));
                sqlp.Add(new SqlParameter("@SendEmai", statusEmail));
                sqlp.Add(new SqlParameter("@SendSMS", statusSMS));
                sqlp.Add(new SqlParameter("@MktRefId", MktRefId));
                sqlp.Add(new SqlParameter("@ReferBy", txtReferBy.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyCreateDate", Convert.ToDateTime(txtCreateDate.Value)));
                sqlp.Add(new SqlParameter("@ReceiptOption", statusReceipt));
                sqlp.Add(new SqlParameter("@LoginId", Session["LoginAdminId"].ToString()));
                bool i2 = conAdd.SaveData(sqlp);

                if (i2 == true)
                {
                    Response.Redirect("CompanyMaster.aspx");
                }
                else
                {
                    Response.Write("<script>");
                    Response.Write("alert('Company Not Added.');");
                    Response.Write("</script>");
                }
            }
            else
            {
                Response.Write("<script>");
                Response.Write("alert('Invalid Company Image " + ext + ". Add Image File.');");
                Response.Write("</script>");
            }
        }
        else
        {
                ConnectionClass conAdd = new ConnectionClass("AdminCompanyUpdate");
                ConnectionClass congetMax = new ConnectionClass();
                Byte[] bytes = null;

                if (imgCompanyImage.PostedFile.ContentLength > 0)
                {
                    Stream fs = imgCompanyImage.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
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

                List<SqlParameter> sqlp = new List<SqlParameter>();
                sqlp.Add(new SqlParameter("@CompanyId", Session["fid"].ToString()));
                sqlp.Add(new SqlParameter("@CompanyName", txtCompanyName.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyAddress", txtAddress.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyPhoneNo1", txtCompanyPhone.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CPAN", txtPAN.Text.ToString().Trim().ToUpper()));
                sqlp.Add(new SqlParameter("@CTAN", txtTAN.Text.ToString().Trim().ToUpper()));
                sqlp.Add(new SqlParameter("@CTin", txtCTIN.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@GTin", txtGTIN.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyEmail", txtCompanyEmail.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyImage", bytes));
                sqlp.Add(new SqlParameter("@ComapnyStatus", compStatus));
                sqlp.Add(new SqlParameter("@LastOperationDate", Convert.ToDateTime(txtOperationDate.Value)));
                sqlp.Add(new SqlParameter("@NoOfDoctor", txtNumberOfDoc.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@NoOfPatient", noOfPat));
                sqlp.Add(new SqlParameter("@SendEmail", statusEmail));
                sqlp.Add(new SqlParameter("@SendSMS", statusSMS));
                sqlp.Add(new SqlParameter("@MktRefId", MktRefId));
                sqlp.Add(new SqlParameter("@ReferBy", txtReferBy.Text.ToString().Trim()));
                sqlp.Add(new SqlParameter("@CompanyCreateDate", Convert.ToDateTime(txtCreateDate.Value)));
                sqlp.Add(new SqlParameter("@ReceiptOption", statusReceipt));
                sqlp.Add(new SqlParameter("@EditId", Session["LoginAdminId"].ToString()));
                bool i2 = conAdd.SaveData(sqlp);
                Session.Remove("fid");
                Session.Remove("imageupdate");
                if (i2 == true)
                {
                    Response.Redirect("CompanyMaster.aspx");
                }
                else
                {
                    Response.Write("<script>");
                    Response.Write("alert('Company Not Updated.');");
                    Response.Write("</script>");
                }
        }
    }

    protected void onStatusCheckedChange(object sender, EventArgs e)
    {
        if (radioCompRegular.Checked == true)
        {
            txtNumberOfPat.Text = "0";
            txtNumberOfPat.Enabled = false;
            span1.Attributes["style"] = "color:red;";
            span2.Attributes["style"] = "color:black;";
            RequiredFieldValPatientNo.Enabled = false;
        }
        else
        {
            txtNumberOfPat.Enabled = true;
            RequiredFieldValPatientNo.Enabled = true;
            span1.Attributes["style"] = "color:black;";
            span2.Attributes["style"] = "color:red;";
        }

    }

    protected void onLinkButtonClick(object sender, EventArgs e)
    {
        Session.Remove("fid");
        Session.Remove("imageupdate");
        Response.Redirect("CompanyDisplay.aspx");
    }

    protected void onLinkButtonClick_Cancel(object sender, EventArgs e)
    {
        Session.Remove("fid");
        Session.Remove("imageupdate");
        Response.Redirect("CompanyMaster.aspx");
    }
    protected void radioRecCom_CheckedChanged(object sender, EventArgs e)
    {
        if (radioRecCom.Checked == true)
        {
            span3.Attributes["style"] = "color:red;";
            span4.Attributes["style"] = "color:black;";
        }
        else
        {
            span3.Attributes["style"] = "color:black;";
            span4.Attributes["style"] = "color:red;";
        }
    }
}