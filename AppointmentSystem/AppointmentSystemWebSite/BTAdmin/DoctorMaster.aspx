<%@ Page Title="" Language="C#" MasterPageFile="~/BTAdmin/BTAdminMaster.master" AutoEventWireup="true" CodeFile="DoctorMaster.aspx.cs" Inherits="BTAdmin_DoctorMaster"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function previewFile() {
        var preview = document.querySelector('#<%=displayLogo.ClientID %>');
        var file = document.querySelector('#<%=fileCompanyLogo.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file);
        } else {
            preview.src = "";
        }
    }
</script>
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "DoctorMaster.aspx/DeleteAdmin", // this for calling the web method function in cs code.  
                data: '{docid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }
    }

    // function OnSuccess  
    function OnSuccess(response) {
        switch (response.d) {
            case "1":

                break;
            case "true":
                $.ajax({
                    type: 'POST',
                    url: 'DoctorMaster.aspx',
                    success: function () {
                        setTimeout(function () {
                            location.reload();
                        }, 500);
                    }
                });
                break;
            case "false":

                break;
        }
    }

    </script>


<form id="Form1" runat="server" class="form-horizontal form-bordered" method="post" action="DoctorMaster.aspx">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Company</a></li>
			<li class="active">Add Doctor</li>
	</ol>	
    <h1 class="page-header">Doctor Form</h1>

    <h4><asp:Label runat ="server" ID="displayLabel" ForeColor ="Red" ></asp:Label></h4>
    

    <div class="row">
    <div class="col-md-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-heading-btn">
                    <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                    <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                </div>
                <h4 class="panel-title">List of Doctors</h4>
            </div>

            <div class="panel-body">
                <div class="table-responsive">
                    <table id="data-table" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>LoginId</th>
                                <th>Edit</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody id="displayDoc" runat="server">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>


    <div class="row">
        <div class="col-md-12">
            
			<div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Add Doctor</h4>
                </div>
                <div class="panel-body panel-form">
                            
				    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname">Company Detail * :</label>
					    <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtCompCode" CssClass="form-control" name="txtCompCode" Enabled="false" style="font-size:15px;color:red;" /> 
                        </div>
                        <div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtCompName" CssClass="form-control" name="txtCompName" Enabled="false" style="font-size:15px;color:red;" /> 
                        </div>
				    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers >
                            <asp:AsyncPostBackTrigger ControlID ="txtDLastName" EventName ="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID ="txtFirstName" EventName ="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID ="txtMiddleName" EventName ="TextChanged" />
                        </Triggers>
                        <ContentTemplate >
                    
             
                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
                        <div  class="col-md-2 col-sm-2">
                            <asp:DropDownList runat="server" ID="cmbSalute" CssClass="form-control" TabIndex="1" >
                                <asp:ListItem >SELECT</asp:ListItem>
                                <asp:ListItem Value="DR" >Dr.</asp:ListItem>
                                <asp:ListItem Value="DRMRS" >Dr. Mrs</asp:ListItem>
                                <asp:ListItem Value="DRMS" >Dr. Ms</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldV" runat="server" ErrorMessage="*Select Designation." ControlToValidate="cmbSalute" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
					    <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" name="txtMiddleName" placeholder="Enter First Name" MaxLength="20" style="text-transform:uppercase"  OnTextChanged ="CreateUserId" AutoPostBack ="true"  TabIndex="2"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter First Name" ControlToValidate="txtFirstName" Display="Dynamic"  ForeColor="Red"   />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid First Name" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" name="txtMiddleName" placeholder="Enter Middle Name" MaxLength="20" style="text-transform:uppercase"  OnTextChanged ="CreateUserId" AutoPostBack ="true" TabIndex="3" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Middle Name" ControlToValidate="txtMiddleName" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Middle Name" ControlToValidate="txtMiddleName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat ="server" ID ="txtDLastName" CssClass ="form-control" name="txtDLastName" placeholder="Enter Last Name" OnTextChanged ="CreateUserId" AutoPostBack ="true" style="text-transform:uppercase" TabIndex="4" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Last Name" ControlToValidate="txtDLastName" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Last Name" ControlToValidate="txtDLastName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                      
                        </div>
				    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Login Id * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtLoginId" CssClass="form-control" name="txtLoginId" placeholder="Doctor Login Id" MaxLength="20" style="text-transform:uppercase" TabIndex="5" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Login Id" ControlToValidate="txtLoginId" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid LoginId" ControlToValidate="txtLoginId" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
        
                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Email/Mobile * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtDocEmail" CssClass="form-control" name="txtDocEmail" placeholder="Enter Doctor Email Id" MaxLength="30" TabIndex="6" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Enter Email" ControlToValidate="txtDocEmail" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Email" ControlToValidate="txtDocEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtDocMobile" CssClass="form-control" name="txtDocMobile" placeholder="Enter Doctor Mobile" MaxLength="14" TabIndex="7" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Enter Mobile Number" ControlToValidate="txtDocMobile" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Mobile Number" ControlToValidate="txtDocMobile" ValidationExpression="[789][0-9]{9}" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Degree * :</label>
					    <div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtDocDegree" CssClass="form-control" name="txtDocDegree" placeholder="Enter Doctor Degree" MaxLength="25" TabIndex="8"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Enter Doctor Degree" ControlToValidate="txtDocDegree" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Invalid Doctor Degree" ControlToValidate="txtDocDegree" ValidationExpression="^[a-zA-Z0-9 ,-.()]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Address * :</label>
					    <div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtDocAddress" CssClass="form-control" name="txtDocAddress" placeholder="Enter Doctor Address" MaxLength="100" TextMode="MultiLine" Rows ="3" TabIndex="9" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Enter Address" ControlToValidate="txtDocAddress" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Invalid Address" ControlToValidate="txtDocAddress" ValidationExpression="^[a-zA-Z0-9,.\s- ]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Appointment Number Of Days *:</label>
					    <div  class="col-md-3 col-sm-3">
                            <asp:TextBox runat="server" ID="txtAppNoOfDays" CssClass="form-control" name="txtDocAddress" placeholder="Enter Number Of Days" MaxLength="3" min="0" TabIndex="10" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*Enter Appointment For Number Of Days" ControlToValidate="txtAppNoOfDays" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtAppNoOfDays" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Visit Charge Info *:</label>
					    <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtFirstVistCharge" CssClass="form-control" name="txtFirstVistCharge" placeholder="Enter First Visit Charge" MaxLength="8" TabIndex="11" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*Enter Visit Charge" ControlToValidate="txtFirstVistCharge" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="*Invalid Charge" ControlToValidate="txtFirstVistCharge" ValidationExpression="^\d+(?:\.\d+)?$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtRoutineVCharge" CssClass="form-control" name="txtRoutineVCharge" placeholder="Enter Routine Visit Charge" MaxLength="8" TabIndex="12" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*Enter Routine Visit Charge" ControlToValidate="txtRoutineVCharge" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="*Invalid Charge" ControlToValidate="txtRoutineVCharge" ValidationExpression="^\d+(?:\.\d+)?$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtValidityDay" CssClass="form-control" name="txtValidityDay" placeholder="Enter Validity Charge" MaxLength="3" TabIndex="13" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*Enter Validity" ControlToValidate="txtValidityDay" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="*Invalid Validity" ControlToValidate="txtValidityDay" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID ="chkMorningSession" EventName ="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID ="chkEveningSession" EventName ="CheckedChanged" />
                </Triggers>
                <ContentTemplate >

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Session *:</label>
					    <div  class="col-md-3 col-sm-3">
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkMorningSession" name="chkMorningSession" runat="server" value="" OnCheckedChanged ="chkMorningSession_CheckedChanged"  AutoPostBack ="true"  Checked ="true" TabIndex ="14" />
                                Morning Session
                            </label>
                        </div>
                        <div  class="col-md-3 col-sm-3">
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkEveningSession" name="chkEveningSession" runat="server" value="" OnCheckedChanged ="chkEveningSession_CheckedChanged"  AutoPostBack ="true"  Checked ="true" TabIndex ="15" />
                                Evening Session
                            </label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Patient Per Shift *:</label>
					    <div  class="col-md-3 col-sm-3">
                            <asp:TextBox runat="server" ID="txtPatientPSMorning" CssClass="form-control" name="txtPatientPSMorning" placeholder="Enter Patient Per Shift Morning" MaxLength="3" TabIndex="16" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="*Invalid Value" ControlToValidate="txtPatientPSMorning" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-3 col-sm-3">
                            <asp:TextBox runat="server" ID="txtPatientPSEvening" CssClass="form-control" name="txtPatientPSEvening" placeholder="Enter Patient Per Shift Evening" MaxLength="3" TabIndex="17" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="*Invalid Value" ControlToValidate="txtPatientPSEvening" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
                  
				    <div class="form-group">
					    <label class="control-label col-md-3">Morning Session</label>
					    <div class="col-md-3">
						    <div class="input-group bootstrap-timepicker">
							    <input id="timepickermorfrom"  name="timepickermorfrom" type="text" class="form-control timepickermorfrom" value="<%= this.TimeMorningFrom %>" tabindex="18" />
							    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                <span class="input-group-addon">to</span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group bootstrap-timepicker">
							    <input id="timepickermorto" name="timepickermorto" type="text" class="form-control timepickermorto" value="<%= this.TimeMorningTo %>" tabindex="19" />
                                <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
						    </div>
					    </div>
				    </div>

                    <div class="form-group">
					    <label class="control-label col-md-3">Evening Session</label>
					    <div class="col-md-3">
						    <div class="input-group bootstrap-timepicker">
							    <input id="timepickerevefrom" name="timepickerevefrom" type="text" class="form-control timepickerevefrom" value="<%= this.TimeEveningFrom %>" tabindex="20" />
							    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                <span class="input-group-addon">to</span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group bootstrap-timepicker">
							    <input id="timepickereveto" name="timepickereveto" type="text" class="form-control timepickereveto" value="<%= this.TimeEveningTo %>" tabindex="21" />
                                <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
						    </div>
					    </div>
				    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname">Working Days *:</label>
					    <div  class="col-md-8 col-sm-8">
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkMonday" runat="server" value="" TabIndex="22" />
                                Monday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkTuesday" runat="server" value="" TabIndex="23"/>
                                Tuesday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkWednesday" runat="server" value="" TabIndex="24" />
                                Wednesday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkThursday" runat="server" value="" TabIndex="25"/>
                                Thursday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkFriday" runat="server" value="" TabIndex="26"/>
                                Friday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkSaturday" runat="server" value="" TabIndex="27" />
                                Saturday
                            </label>
                            <label class="checkbox-inline">
                                <asp:CheckBox ID="chkSunday" runat="server" value="" TabIndex="28"/>
                                Sunday
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-9">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Priting Information</h4>
                </div>
                <div class="panel-body panel-form">
                            
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Header Line 1 * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtPrintLineOne" CssClass="form-control" name="txtPrintLineOne" placeholder="Header Line 1" MaxLength="30" TabIndex="29" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*Enter Print Line One" ControlToValidate="txtPrintLineOne" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="*Invalid Print Line" ControlToValidate="txtPrintLineOne" ValidationExpression="^[a-zA-Z0-9 (),.-]+$" Display="Dynamic"  ForeColor="Red" />
						</div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Header Line 2 * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtPrintLineTwo" CssClass="form-control" name="txtPrintLineTwo" placeholder="Header Line 2" MaxLength="30" TabIndex="30" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ErrorMessage="*Invalid Print Line" ControlToValidate="txtPrintLineTwo" ValidationExpression="^[a-zA-Z0-9 (),.-]+$" Display="Dynamic"  ForeColor="Red" />
						</div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Bottom Line * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtBottomLine" CssClass="form-control" name="txtPrintLineTwo" placeholder="Bottom Line" MaxLength="30" TabIndex="31" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*Enter Print Line Two" ControlToValidate="txtBottomLine" ForeColor="Red" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ErrorMessage="*Invalid Print Line" ControlToValidate="txtBottomLine" ValidationExpression="^[a-zA-Z0-9 (),.-]+$" Display="Dynamic"  ForeColor="Red" />
						</div>
					</div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname"> Company Logo * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:FileUpload runat="server" ID="fileCompanyLogo" CssClass="form-control" onchange="previewFile()" name="fileCompanyLogo" TabIndex="32" />
						</div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Logo</h4>
                </div>
                <div class="panel-body panel-form">
                    <div class="form-group">
						<div  class="col-md-6 col-sm-6">
                            <asp:Image runat="server" ID="displayLogo" CssClass="form-control" name="displayLogo" Height="228" Width="203" AlternateText="Company Logo" />
						</div>
					</div>
                </div>
            </div>
        </div>
    </div>

   

    <div class="row">
        <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                        </div>
                        <h4 class="panel-title">Add Specialization</h4>
                    </div>
                    <div class="panel-body panel-form">
                            
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Select Specialization * :</label>
						<div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cmbSpecialization" class="form-control" TabIndex="33" > 
                                <asp:ListItem Value ="SELECT" >SELECT</asp:ListItem>                                           
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldVDesignation" runat="server" ErrorMessage="*Select Specialization." ControlToValidate="cmbSpecialization" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
					</div>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
            <div class="form-group">
                <label class="col-md-3 control-label">Select Forms * :</label>
                <div class="col-md-6">
                    <asp:ListBox runat="server" ID="cmbFormSelect" class="form-control" SelectionMode ="Multiple" AutoPostBack="true" OnSelectedIndexChanged="onFormIndexChange" Rows="6" TabIndex ="34">    
                    </asp:ListBox>
                </div>
            </div>

            <div class="form-group">
				<label class="control-label col-md-3">Selected Form</label>
				<div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered" DataKeyNames="FormId"  >
                    <Columns>
                        <asp:BoundField DataField="FormId" HeaderText="Key"/>
                        <asp:BoundField DataField="FormName" HeaderText="Degree Name"/>
                        <asp:TemplateField  HeaderText="SortingSeq" >
                            <ItemTemplate>
                                <div  class="col-md-1 col-sm-1">
                                <asp:TextBox ID="TextBoxS" runat ="server" Text='<%# Eval("SortingSeq") %>' Width ="2cm" />
                                </div> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
				</div>
			</div>
        </ContentTemplate>
        <Triggers >
            <asp:AsyncPostBackTrigger ControlID ="cmbFormSelect" EventName ="SelectedIndexChanged" />
        </Triggers>
</asp:UpdatePanel>

                <div class="form-group">
				</div>
                <div class="form-group">
					<label class="control-label col-md-3 col-sm-3"></label>
					<div class="col-md-6 col-sm-6">
						<asp:Button ID="submit" name="submit" runat ="server"  class="btn btn-primary" Text="Submit" OnClick="onSubmitClick"  TabIndex ="35"/>
                        <asp:LinkButton ID="txtCancel" name="txtCancel" runat="server" class="btn btn-danger" Text="Cancel" CausesValidation="false" OnClick="onCancelClick" TabIndex ="36" />
					</div>
				</div>

                <div class="form-group">
					<label class="control-label col-md-3 col-sm-3"></label>
					<div class="col-md-6 col-sm-6">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor ="Red" />
					</div>
				</div>
            </div>
        </div>
    </div>
</div>
   


    
<!--    </ContentTemplate>
     <Triggers >
        <asp:AsyncPostBackTrigger ControlID="cmbFormSelect" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
-->
</form>
</asp:Content>

