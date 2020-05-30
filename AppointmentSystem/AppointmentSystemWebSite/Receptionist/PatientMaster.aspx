<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/ReceptionestMaster.master" AutoEventWireup="true" CodeFile="PatientMaster.aspx.cs" Inherits="Receptionist_PatientMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function previewFile() {
        var preview = document.querySelector('#<%=displayImg.ClientID %>');
        var file = document.querySelector('#<%=imgPatientImage.ClientID %>').files[0];
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

 <form id="Form1" method="post" runat="server" action="PatientMaster.aspx"  class="form-horizontal form-bordered" name="demo-form" enctype="multipart/form-data">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Patient Master</li>
	</ol>	
    <h1 class="page-header">Patient Master</h1>

    <div class="row">
        <div class="col-md-8" >
            
			<!-- begin panel -->
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Patient Master</h4>
                </div>
                <div class="panel-body panel-form">
                            
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
						<div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" name="txtName" placeholder="Patient Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Patient Name" ControlToValidate="txtName" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid Patient Name" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z0-9 ,.-]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Address * :</label>
						<div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" name="txtAddress" placeholder="Address" MaxLength="200" TabIndex="2" style="text-transform :capitalize;" TextMode="MultiLine" Rows="3" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Address" ControlToValidate="txtAddress" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Address" ControlToValidate="txtAddress" ValidationExpression="^[a-zA-Z0-9,.\s-\\\/ ]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Gender * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbGender" runat="server" class="form-control" TabIndex="3" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                            <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Select Gender." ControlToValidate="cmbGender" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="radioChild" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="radioAdult" EventName="CheckedChanged" />
                </Triggers>
                <ContentTemplate>
                        <div class="col-md-4 col-sm-4" >
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="radioChild" GroupName="companyStatus" name="radioCompStatus" value="CHILD" TabIndex="4" Checked="true" OnCheckedChanged="radioChild_CheckedChanged" AutoPostBack="true" />
                                <span id="span1" runat="server" style="" > Child</span>
                                            
                            </label>
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="radioAdult" GroupName="companyStatus" name="radioCompStatus" value="ADULT" TabIndex="5" OnCheckedChanged="radioChild_CheckedChanged" AutoPostBack="true" />
                                <span id="span2" runat="server" style="" > Adult</span>
                            </label>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
					</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtBirthDate" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Birth Date * :</label>
            
						    <div  class="col-md-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" name="txtBirthDate" placeholder="Patient Name" TabIndex="6" TextMode="Date"  /> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Birth Date" ControlToValidate="txtBirthDate" Display="Dynamic"  ForeColor="Red" />
                            </div>
            		</div>
                    
                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Age * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtAge" CssClass="form-control" name="txtAge" placeholder="Patient Age" MaxLength="3" TabIndex="7" TextMode="Number" OnTextChanged ="txtBirthDate_TextChanged" AutoPostBack="true" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Patient Age" ControlToValidate="txtAge" Display="Dynamic"  ForeColor="Red" />
                        </div>

                        <div  class="col-md-4 col-sm-4">
                            <asp:DropDownList ID="cmbAgeUnit" runat="server" class="form-control" TabIndex="8" >
                            <asp:ListItem Value="SELECT">UNITS</asp:ListItem>
                            <asp:ListItem Value="YEAR">YEAR</asp:ListItem>
                            <asp:ListItem Value="MONTH">MONTH</asp:ListItem>
                            <asp:ListItem Value="DAYS">DAYS</asp:ListItem>
                            <asp:ListItem Value="HOURS">HOURS</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Select Unit." ControlToValidate="cmbAgeUnit" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
					</div>
                 </ContentTemplate>
            </asp:UpdatePanel>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Email/Contact No * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" name="txtEmail" placeholder="Email" MaxLength="40" TabIndex="9" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" name="txtMobile" placeholder="Contact Number" MaxLength="13" TabIndex="10"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Enter Contact Number" ControlToValidate="txtMobile" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtMobile" ValidationExpression="[789][0-9]{9}" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Blood Group * :</label>
					    <div  class="col-md-4 col-sm-4">
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

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Allergy Details * :</label>
						<div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtAllergy" CssClass="form-control" name="txtAllergy" placeholder="Allergy Details" MaxLength="200" TabIndex="12" style="text-transform :capitalize;" TextMode="MultiLine" Rows="3" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Allergy" ControlToValidate="txtAllergy" ValidationExpression="^[a-zA-Z0-9 ,-.()\s\[\]\\\/*+]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Enroll Date :</label>
						<div  class="col-md-8 col-sm-8">
                            <asp:TextBox runat="server" ID="txtEnrollDate" CssClass="form-control" name="txtEnrollDate" TabIndex="13" TextMode="Date"  /> 
                        </div>
					</div>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="submitRefDoctor" EventName="Click" />
                </Triggers>
                <ContentTemplate>         
                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Reference Doctor * :</label>
					    <div  class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="cmbRefDoctor" runat="server" class="form-control" TabIndex="14" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        <asp:Label runat="server" ID="lblRefDoc" Text="" ForeColor="Red" ></asp:Label>
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <a href="#modal-without-animation" class="btn btn-sm btn-success" data-toggle="modal">Add Ref Doctor</a>
                        </div>
					</div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="submitArea" EventName="Click" />
                </Triggers>
                <ContentTemplate>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Area * :</label>
					    <div  class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="cmbArea" runat="server" class="form-control" TabIndex="15" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        <asp:Label ID="lblArea" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <a href="#modal-dialog" class="btn btn-sm btn-success" data-toggle="modal">Add Area</a>
                        </div>
					</div>


        
                </ContentTemplate>
            </asp:UpdatePanel>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Category * :</label>
					    <div  class="col-md-8 col-sm-8">
                            <asp:DropDownList ID="cmbCategory" runat="server" class="form-control" TabIndex="16" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Select Categoty." ControlToValidate="cmbCategory" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-7 col-sm-7">
							<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="submit_Click" class="btn btn-primary" TabIndex ="17" />
                            <asp:Button ID="btnCancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="btnCancel_Click" Visible="false" Text="Cancel" TabIndex ="18" CausesValidation="false"/>
						</div>
					</div>
                </div>
            </div>
        </div>

        <div class="col-md-4" style="margin-left:0;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Patient Image</h4>
                </div>

                <div class="panel-body panel-form">
                    <div class="form-group">
						<div  class="col-md-11 col-sm-11">
                              <asp:FileUpload runat="server" ID="imgPatientImage" onchange="previewFile()"  name="imgPatientImage" class="form-control" /> 
                        </div>
                    </div>

                    <div class="form-group">
						<div  class="col-md-10 col-sm-10">
                            <asp:Image runat="server" ID="displayImg" CssClass="form-control" name="displayImg" Height="225" Width="225" AlternateText="Patient Image" />
						</div>
					</div>
                </div>
            </div>
        </div>

        <div class="col-md-4" style="margin-left:0;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Patient History</h4>
                </div>

                <div class="panel-body panel-form">
                    
                    
                </div>
            </div>
        </div>

        
    </div>

    <div class="modal fade" id="modal-dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">Add New Area</h4>
				</div>
				<div class="modal-body">
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Area Name * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtNewArea" CssClass="form-control" name="txtNewArea" placeholder="Area Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Invalid Area Name" ControlToValidate="txtNewArea" ValidationExpression="^[a-zA-Z ,.]+$" Display="Dynamic"  ForeColor="Red" />
                            <asp:Label ID="lblDuplicate" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
					</div>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
					<asp:LinkButton class="btn btn-sm btn-success" runat="server" ID="submitArea" OnClick="submitArea_Click" CausesValidation="false" >Action</asp:LinkButton>
				</div>
			</div>
		</div>
	</div> 

     <div class="modal" id="modal-without-animation">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">Add Reference Doctor Name</h4>
				</div>
				<div class="modal-body">
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtNewRefDoctor" CssClass="form-control" name="txtNewRefDoctor" placeholder="Reference Doctor Name" MaxLength="60" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtNewRefDoctor" ValidationExpression="^[a-zA-Z ,.]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
                    <asp:LinkButton runat="server" ID="submitRefDoctor" class="btn btn-sm btn-success" CausesValidation="false" Text="Submit" OnClick="submitRefDoctor_Click" ></asp:LinkButton>
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
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                     </div>
                     <h4 class="panel-title">List of Patient</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Name</th>
                                     <th>Contact No</th>
                                     <th>Gender</th>
                                     <th>Age</th>
                                     <th>Edit</th>
                                 </tr>
                             </thead>
                        
                             <tbody id="displayPatient" runat="server">
                             </tbody>
                      
                         </table>
                     </div>
                 </div>
             </div>
         </div>

     </div> 
</form>
</asp:Content>

