<%@ page title="" language="C#" masterpagefile="~/Receptionist/ReceptionestMaster.master" autoeventwireup="true" inherits="Receptionist_AppointmentEntry, App_Web_xkji30bz" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../assets/css/gridview.css" rel="stylesheet" />
<script type="text/javascript">
    function myMorFunction() {
        var label = document.getElementById("<%=lblAppShift.ClientID%>");
        label.innerHTML = "Morning";
        document.getElementById("<%=hfLabel.ClientID %>").value = label.innerHTML;
     }
    
   
</script>
<script>
    function myFunction() {
        var label = document.getElementById("<%=lblAppShift.ClientID%>");
        label.innerHTML = "Evening";
        document.getElementById("<%=hfLabel.ClientID %>").value = label.innerHTML;
    }


</script>

<script type="text/javascript">
    function showModal() {
        $('.modal-backdrop').remove();
        $('#modaldialog').modal('show');
    }
    function showModalAppCancel() {
        $('.modal-backdrop').remove();
        $('#modelCancelApp').modal('show');
    }
</script>
<script>
    function ClosePopup() {
        $('.modal-backdrop').remove();
        $('#modaldialog').modal('hide');
    }

    function ClosePopupAppCancel() {
        $('.modal-backdrop').remove();
        $('#modelCancelApp').modal('hide');
    }
</script>
<script>
    function CloseArea() {
        $('#modalarea').modal('hide');
        $('#modaldialog').modal('show');
    }

</script>


<form id="Form1" method="post" runat="server" action="#"  class="form-horizontal form-bordered" name="demo-form">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cmbDoctor" EventName="SelectedIndexChanged" />

    </Triggers>
    <ContentTemplate>

    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Appointment Entry</li>
	</ol>	
    <h1 class="page-header">Appointment Entry</h1>

    <div class="row">
        <div class="col-md-12" style="margin-left:-6px;padding:0;" >
              <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-heading-btn">
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                            </div>
                            <h4 class="panel-title">Select Doctor </h4>
                        </div>
                        <div class="panel-body panel-form">
                            
						<div class="form-group">
							<label class="control-label col-md-3 col-sm-3" for="fullname"> Select Doctor * :</label>
							<div  class="col-md-4 col-sm-4">
                                <asp:DropDownList ID="cmbDoctor" runat="server" class="form-control" TabIndex="3" OnSelectedIndexChanged="cmbDoctor_SelectedIndexChanged" AutoPostBack="true" >
                                <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div  class="col-md-4 col-sm-4">
                                <asp:TextBox runat="server" ID="lblSelectedDate" ForeColor="Red" ReadOnly="true" class="form-control" ></asp:TextBox>
                            </div>
						</div>

                    </div>
                </div>
       </div>
    </div>

    <div class="row">
        <div class="col-md-2" style="margin-left:-6px;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    
                    <h4 class="panel-title">Select Date </h4>
                </div>
                <div class="panel-body panel-form"  style ="height:300px; overflow:auto;">
                            <asp:GridView ID="grid_Date" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                RowStyle-CssClass="rows" 
                                 DataKeyNames="DateId" OnSelectedIndexChanged="grid_Date_SelectedIndexChanged" >
                            <Columns>
                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="70px" ControlStyle-Width="70px"/>
                                <asp:BoundField DataField="MorningCount" HeaderText="M" ItemStyle-Width="20px" ControlStyle-Width="20px" />
                                <asp:BoundField DataField="EveningCount" HeaderText="E" ItemStyle-Width="20px" ControlStyle-Width="20px"  />
                                <asp:CommandField ShowSelectButton="true" SelectText="Sel" ItemStyle-Width="5px" />
                            </Columns>
                        </asp:GridView>
                    <asp:Label runat="server" ID="displayDate"></asp:Label>
                </div>
            </div>
        </div>

        <div class="col-md-5" style="margin-left:2px;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">
                        
                        <asp:Label ID="lblPatientMor" runat="server" Text="Morning Shift" ></asp:Label>
                    </h4>
                </div>
                <div class="panel-body panel-form" style ="height:300px; overflow:auto;">

                    <span runat="server" id="spanMor">
                        <asp:Button ID ="btnSelectPatient" runat ="server" class="btn btn-sm btn-danger" Text="Select Patient" OnClick ="btnSelectPatient_Click"  CausesValidation ="false"  />
                    </span>
                    <br />
                    <asp:Label runat="server" ID="lblMorningApp"></asp:Label>  
                    <asp:GridView ID="gridMorningApp" runat="server" AutoGenerateColumns="False" CssClass="table"  
                        DataKeyNames="AppointmentId" OnRowDeleting="gridMorningApp_RowDeleting" 
                        OnRowDataBound="gridMorningApp_RowDataBound" 
                        OnSelectedIndexChanged ="gridMorningApp_SelectedIndexChanged" AllowPaging="true" OnPageIndexChanging="gridMorningApp_PageIndexChanging" PageSize="10" >
                        <Columns>
                            <asp:BoundField DataField="PatientName" HeaderText="Name" ReadOnly="true" ItemStyle-Width="220px" />
                            <asp:BoundField DataField="AppointmentTime" DataFormatString="{0:H:mm}" HeaderText="Time" HtmlEncode="false" ReadOnly="true"  />
                            <asp:BoundField DataField="CancelStatus" ReadOnly="true" />
                            <asp:CommandField ShowSelectButton ="true" ButtonType ="Button" SelectText="Edit" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Del" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="col-md-5" style="margin-left:2px;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">
                        <asp:Label ID="lblPatientEve" runat="server" Text="Evening Shift"></asp:Label>
                    </h4>
                </div>
                <div class="panel-body panel-form" style ="height:300px; overflow:auto;">
                    <span id="spanEve" runat="server">
                        <asp:Button ID ="btnSelectPatientEvening" runat ="server" class="btn btn-sm btn-danger" Text="Select Patient" OnClick ="btnSelectPatientEvening_Click"  CausesValidation ="false"  />
                    </span>
                    <br />
                    <asp:Label ID="lblEveningApp" runat="server"></asp:Label>
                    <asp:GridView ID="gridEveningApp" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                        CssClass="table table-bordered" DataKeyNames="AppointmentId" 
                        OnRowDeleting="gridEveningApp_RowDeleting" OnRowDataBound="gridMorningApp_RowDataBound" 
                        OnSelectedIndexChanged ="gridEveningApp_SelectedIndexChanged"  >
                        <Columns>
                            <asp:BoundField DataField="PatientName" HeaderText="Name" ReadOnly="true" ItemStyle-Width="220px" />
                            <asp:BoundField DataField="AppointmentTime" DataFormatString="{0:H:mm}" HeaderText="Time" HtmlEncode="false" ReadOnly="true" />
                            <asp:BoundField DataField="CancelStatus" ReadOnly="true" />
                            <asp:CommandField ShowSelectButton ="true" SelectText ="Edit" ButtonType ="Button"   />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Del" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>


    <!-- Morning Patient Select -->
    <div class="modal modal-message fade" id="modaldialog">
		<div class="modal-dialog" >
			<div class="modal-content">
				<div class="modal-header">
                    <button type="button" id="btnClosePopup" class="close" onclick="ClosePopup();" style="font-size:35px" >×</button>
                    <h4 class="modal-title">Select Patient For
                        <asp:Label ID="lblAppShift" runat="server"></asp:Label>
                        <asp:Label ID="lblMDate" runat="server" ></asp:Label>
                        <asp:HiddenField ID="hfLabel" runat="server" />
                        <br />
                        <asp:Label ID="lblDoctorName" runat="server"></asp:Label>
					</h4>
				</div>
				<div class="modal-body">

                    <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Select Patient/Category * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbSelectPatient" runat="server" class="form-control" TabIndex="3" OnSelectedIndexChanged="cmbSelectPatient_SelectedIndexChanged" AutoPostBack="true" >
                                <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbCategory" runat="server" class="form-control" TabIndex="16" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*Select Categoty." ControlToValidate="cmbCategory" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Name * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" name="txtName" placeholder="Patient Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Patient Name" ControlToValidate="txtName" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid Patient Name" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z0-9 ,.-]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:DropDownList ID="cmbGender" runat="server" class="form-control" TabIndex="3" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                            <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Select Gender." ControlToValidate="cmbGender" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
                        <div class="col-md-4 col-sm-4" >
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="radioChild" GroupName="companyStatus" name="radioCompStatus" value="CHILD" TabIndex="4" Checked="true"/>
                                <span id="span1" runat="server" style="" > Child</span>
                            </label>
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="radioAdult" GroupName="companyStatus" name="radioCompStatus" value="ADULT" TabIndex="5" />
                                <span id="span2" runat="server" style="" > Adult</span>
                            </label>
                         </div>
					</div>

                     <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Address/Allergy * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" name="txtAddress" placeholder="Address" MaxLength="200" TabIndex="2" style="text-transform :capitalize;" TextMode="MultiLine" Rows="2" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Enter Address" ControlToValidate="txtAddress" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Address" ControlToValidate="txtAddress" ValidationExpression="^[a-zA-Z0-9,.\s-\\\/ ]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                         <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtAllergy" CssClass="form-control" name="txtAllergy" placeholder="Allergy Details" MaxLength="200" TabIndex="12" style="text-transform :capitalize;" TextMode="MultiLine" Rows="2" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Allergy" ControlToValidate="txtAllergy" ValidationExpression="^[a-zA-Z0-9 ,-.()\s\[\]\\\/*+]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Birth Date * :</label>
            
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" name="txtBirthDate" placeholder="Patient Name" TabIndex="6" TextMode="Date"  /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Enter Birth Date" ControlToValidate="txtBirthDate" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtAge" CssClass="form-control" name="txtAge" placeholder="Patient Age" MaxLength="3" TabIndex="7" TextMode="Number"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Enter Patient Age" ControlToValidate="txtAge" Display="Dynamic"  ForeColor="Red" />
                        </div>

                        <div  class="col-md-2 col-sm-2">
                            <asp:DropDownList ID="cmbAgeUnit" runat="server" class="form-control" TabIndex="8" >
                            <asp:ListItem Value="SELECT">UNITS</asp:ListItem>
                            <asp:ListItem Value="YEAR">YEAR</asp:ListItem>
                            <asp:ListItem Value="MONTH">MONTH</asp:ListItem>
                            <asp:ListItem Value="DAYS">DAYS</asp:ListItem>
                            <asp:ListItem Value="HOURS">HOURS</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*Select Unit." ControlToValidate="cmbAgeUnit" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
            		</div>

                    <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Email/Contact No * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" name="txtEmail" placeholder="Email" MaxLength="40" TabIndex="9" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" name="txtMobile" placeholder="Contact Number" MaxLength="13" TabIndex="10"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*Enter Contact Number" ControlToValidate="txtMobile" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtMobile" ValidationExpression="[789][0-9]{9}" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Enroll Date/Blood Group * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtEnrollDate" CssClass="form-control" name="txtEnrollDate" TabIndex="13" TextMode="Date"  /> 
                        </div>
                         <div  class="col-md-2 col-sm-2">
                            <asp:DropDownList ID="cmbBloodGroup" runat="server" class="form-control" TabIndex="11" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            <asp:ListItem Value="ANEG">A-</asp:ListItem>
                            <asp:ListItem Value="APOS">A+</asp:ListItem>
                            <asp:ListItem Value="BNEG">B-</asp:ListItem>
                            <asp:ListItem Value="BPOS">B+</asp:ListItem>
                            <asp:ListItem Value="ABNEG">AB-</asp:ListItem>
                            <asp:ListItem Value="ABPOS">AB+</asp:ListItem>
                            <asp:ListItem Value="ONEG">O-</asp:ListItem>
                            <asp:ListItem Value="OPOS">O+</asp:ListItem>
                            </asp:DropDownList>
                        </div>
					</div>

                    <div class="form-group" runat="server" id="divCmbRefDoc">
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Reference Doctor * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbRefDoctor" runat="server" class="form-control" TabIndex="14" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        <asp:Label runat="server" ID="lblRefDoc" Text="" ForeColor="Red" ></asp:Label>
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:Button class="btn btn-sm btn-success" runat="server" ID="btnAddRefDoc" OnClick="btnAddRefDoc_Click" Text="Add Ref Doctor" CausesValidation="false" />
                        </div>
					</div>

					<div class="form-group" runat="server" id="divtxtRefDoc" visible="false">
						<label class="control-label col-md-2 col-sm-2" for="fullname">Ref. Doc. Name * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtNewRefDoctor" CssClass="form-control" name="txtNewRefDoctor" placeholder="Reference Doctor Name" MaxLength="60" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtNewRefDoctor" ValidationExpression="^[a-zA-Z ,.]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:Button class="btn btn-sm btn-danger" runat="server" ID="btnCancelRefDoc" OnClick="btnCancelRefDoc_Click" Text="Cancel" CausesValidation="false" />
                        </div>
					</div>

                     <div class="form-group" runat="server" id="divCmbArea" >
						<label class="control-label col-md-2 col-sm-2" for="fullname"> Area * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbArea" runat="server" class="form-control" TabIndex="15" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        <asp:Label ID="lblDuplicateArea" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:Button CssClass="btn btn-sm btn-success" runat="server" ID="btnAddNewArea" Text="Add Area" CausesValidation="false" OnClick="btnAddNewArea_Click" />
                        </div>
					</div>

                    <div class="form-group" runat="server" id="divArea" visible="false">
					<label class="control-label col-md-2 col-sm-2" for="fullname"> Area Name * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtNewArea" CssClass="form-control" name="txtNewArea" placeholder="Area Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Invalid Area Name" ControlToValidate="txtNewArea" ValidationExpression="^[a-zA-Z ,.]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:Button CssClass="btn btn-sm btn-danger" runat="server" ID="btnCnacelArea" Text="Cancel" CausesValidation="false" OnClick="btnCnacelArea_Click" />
                        </div>
                    </div>

                    <div class="form-group">
					    <label class="control-label col-md-2 col-sm-2" for="fullname"> Select Time * :</label>
					    <div  class="col-md-4 col-sm-4">
                            <asp:TextBox ID="txtTime" runat="server" TextMode="Time" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Select Time." ControlToValidate="txtTime" ForeColor="Red" Display="Dynamic" />
                        </div>
        			    <div  class="col-md-4 col-sm-4">
                            <span id="lblDocTime" runat="server"></span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Close This And Select Doctor To Book Appointment." ControlToValidate="cmbDoctor" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Select Date To Book Appointment." ControlToValidate="lblSelectedDate" ForeColor="Red" Display="Dynamic" />                            
                        </div>
				    </div>

				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
                    <asp:Button ID="btnCancelAppointment" runat="server" class="btn btn-sm btn-danger" OnClick="btnCancelAppointment_Click" CausesValidation="false" Visible="false" Text ="Cancel Appointment" />
					<asp:Button CssClass="btn btn-sm btn-success" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text ="BOOK" />
				</div>
			</div>
		</div>
	</div> 


    <div class="modal fade" id="modelCancelApp">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">Reason For Cancelling Appointment</h4>
				</div>
				<div class="modal-body">
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Reason * :</label>
						<div  class="col-md-9 col-sm-9">
                            <asp:TextBox runat="server" ID="txtReasonCancel" CssClass="form-control" name="txtNewRefDoctor" placeholder="Enter Reason" MaxLength="100" TextMode ="MultiLine" Rows="3" /> 
                        </div>
					</div>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
                    <asp:Button runat="server" ID="btnCancelApp" class="btn btn-sm btn-success" CausesValidation="false" Text="Submit" OnClick="btnCancelApp_Click" />
				</div>
			</div>
		</div>
	</div>
 </ContentTemplate>
</asp:UpdatePanel>
</form>
</asp:Content>

