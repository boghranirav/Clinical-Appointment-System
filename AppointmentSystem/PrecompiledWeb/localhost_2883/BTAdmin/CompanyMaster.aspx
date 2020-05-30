<%@ page title="" language="C#" masterpagefile="~/BTAdmin/BTAdminMaster.master" autoeventwireup="true" inherits="BTAdmin_CompanyMaster, App_Web_2lyrwnv0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function previewFile() {
        var preview = document.querySelector('#<%=displayImg.ClientID %>');
        var file = document.querySelector('#<%=imgCompanyImage.ClientID %>').files[0];
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

<form id="Form1" method="post" runat="server" action="CompanyMaster.aspx"  class="form-horizontal form-bordered" name="demo-form" enctype="multipart/form-data">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          

    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Admin</a></li>
			<li class="active">Company Master</li>
	</ol>	
    <h1 class="page-header">New Company Master</h1>


    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">General Details</h4>
                </div>

                    <div class="panel-body panel-form">
                        <div class="form-group">
						    <label class="control-label col-md-4 col-sm-4" > Company Code/Image * :</label>
						    <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtCompanyCode" CssClass="form-control" name="txtCompanyCode" placeholder="Company Code" MaxLength="6" TabIndex="1" Enabled="false" /> 
                            </div>
                            <div  class="col-md-5 col-sm-5">
                                    <input type="file"  runat="server" id="imgCompanyImage" onchange="previewFile()"  name="imgCompanyImage" class="form-control" /> 
                            </div>
                        </div>
                        
                        

                        <div class="form-group">
						    <label class="control-label col-md-4 col-sm-4" > Company Name * :</label>
						    <div  class="col-md-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control" name="txtCompanyName" placeholder="Company Name" MaxLength="80" TabIndex="2" style="text-transform :capitalize ;" /> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Company Name" ControlToValidate="txtCompanyName" Display="Dynamic"  ForeColor="Red" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Company Name" ControlToValidate="txtCompanyName" ValidationExpression="^[a-zA-Z0-9 .()-]+$" Display="Dynamic"  ForeColor="Red" />
                            </div>
					    </div>
                    
                        <div class="form-group">
						    <label class="control-label col-md-4 col-sm-4" > Company Address*:</label>
						    <div  class="col-md-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" name="txtAddress" placeholder="Company Address" MaxLength="200" TabIndex="3" TextMode="MultiLine" Rows="4" /> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Company Address" ControlToValidate="txtAddress" Display="Dynamic"  ForeColor="Red" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Company Address" ControlToValidate="txtAddress" ValidationExpression="^[a-zA-Z0-9,.\s ]+$" Display="Dynamic"  ForeColor="Red" />
                            </div>
					    </div>
                    	
                    </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Tax Detail</h4>
                </div>
                <div class="panel-body panel-form">
                            <div class="form-group">
						        <label class="control-label col-md-4 col-sm-4" > Company PAN :</label>
						        <div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtPAN" CssClass="form-control" name="txtPAN" placeholder="Company PAN" MaxLength="10" TabIndex="4" style="text-transform:uppercase"  /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Company PAN" ControlToValidate="txtPAN" ValidationExpression="^[a-zA-Z0-9]{10}" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

                            <div class="form-group">
						        <label class="control-label col-md-4 col-sm-4" > Company TAN :</label>
						        <div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtTAN" CssClass="form-control" name="txtTAN" placeholder="Company TAN" MaxLength="10" TabIndex="5" style="text-transform:uppercase"  /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Invalid Company TAN" ControlToValidate="txtTAN" ValidationExpression="^[a-zA-Z0-9]{10}" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

                            <div class="form-group">
						        <label class="control-label col-md-4 col-sm-4" > Company GTIN :</label>
						        <div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtGTIN" CssClass="form-control" name="txtGTIN" placeholder="Company GTIN" MaxLength="25" TabIndex="6" /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Invalid Company GTIN" ControlToValidate="txtGTIN" ValidationExpression="^[a-zA-Z0-9 \-\.,]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

                            <div class="form-group">
						        <label class="control-label col-md-4 col-sm-4" > Company CTIN :</label>
						        <div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtCTIN" CssClass="form-control" name="txtCTIN" placeholder="Company CTIN" MaxLength="25" TabIndex="7" /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="*Invalid Company CTIN" ControlToValidate="txtCTIN" ValidationExpression="^[a-zA-Z0-9 \-\.,:]+$" Display="Dynamic"  ForeColor="Red" />
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
                        <h4 class="panel-title">Contral Detail</h4>
                    </div>
                        <div class="panel-body  panel-form">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID="radioCompRegular" EventName ="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID ="radioCompDemo" EventName="CheckedChanged" />
               
            </Triggers>
            <ContentTemplate >
                             <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Company Status * :</label>
						        <div class="col-md-3 col-sm-3" >
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="radioCompRegular" GroupName="companyStatus" name="radioCompStatus" value="REGULAR" AutoPostBack="true" OnCheckedChanged="onStatusCheckedChange" TabIndex="8" />
                                         <span id="span1" runat="server" style="" > REGULAR</span>
                                            
                                        </label>
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="radioCompDemo" GroupName="companyStatus" name="radioCompStatus" value="DEMO" AutoPostBack="true" OnCheckedChanged="onStatusCheckedChange" TabIndex="9" />
                                         <span id="span2" runat="server" style="" >DEMO</span>
                                        </label>
                                </div>

                                 <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtNumberOfPat" CssClass="form-control" name="txtNumberOfPat" placeholder="Number Of Patient" MaxLength="2" TabIndex="10" Enabled ="false"  /> 
                                     <asp:RequiredFieldValidator ID="RequiredFieldValPatientNo" runat="server" ErrorMessage="*Enter Number Of patient" ControlToValidate="txtNumberOfPat" Display="Dynamic"  ForeColor="Red"  Enabled="false" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtNumberOfPat" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>
                            
          </ContentTemplate>
</asp:UpdatePanel> 
                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Phone/Email *:</label>
						        <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtCompanyPhone" CssClass="form-control" name="txtCompanyPhone" placeholder="Company Phone Number" MaxLength="11" TabIndex="11" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Enter Company Phone Number" ControlToValidate="txtCompanyPhone" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Company Phone Number" ControlToValidate="txtCompanyPhone" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
                                <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtCompanyEmail" CssClass="form-control" name="txtCompanyEmail" placeholder="Company Email" MaxLength="40" TabIndex="12" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Company Email Address" ControlToValidate="txtCompanyEmail" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Company Email Address" ControlToValidate="txtCompanyEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

                           <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Number Of Doctor * :</label>
						        <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtNumberOfDoc" CssClass="form-control" name="txtNumberOfDoc" placeholder="Number Of Doctor" MaxLength="2" TabIndex="13" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*Enter Number Of Doctor" ControlToValidate="txtNumberOfDoc" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtNumberOfDoc" ValidationExpression="^[0-9]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
                                
					        </div>

                            <div class="form-group">
                                <label class="control-label col-md-3 col-sm-3" >Text Messages : </label>
						        <div  class="col-md-6 col-sm-6">
                                    <label class="checkbox-inline">
                                        <asp:CheckBox ID="chkEmail" runat="server" value="" TabIndex="14" />
                                        Send Email
                                    </label>
                                    <label class="checkbox-inline">
                                        <asp:CheckBox ID="chkSMS" runat="server" value="" TabIndex="15" />
                                        Send SMS 
                                    </label>
                                </div>
                            </div>

                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Last Operation/Company Create Date * :</label>
                                <div  class="col-md-3 col-sm-3">
                                    <div class="input-group date" id="datepicker-disabled-past" data-date-format="dd-mm-yyyy" data-date-start-date="Date.default">
                                        <input type="text" id="txtOperationDate" class="form-control" runat="server" placeholder="Last Operation Date" tabindex="16" />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Select Last Operation Date" ControlToValidate="txtOperationDate" Display="Dynamic"  ForeColor="Red" />
                                </div>
						        <div  class="col-md-3 col-sm-3">
                                    <div class="input-group date" id="datepicker-default" data-date-format="dd-mm-yyyy" data-date-start-date="Date.default">
                                        <input type="text" id="txtCreateDate" class="form-control" runat="server" tabindex ="17" placeholder="Company Create Date" />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Select Company Create Date" ControlToValidate="txtCreateDate" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Reference :</label>
						        <div  class="col-md-3 col-sm-3">
                                    <asp:DropDownList ID="cmbRefSelect" runat="server" class="form-control" TabIndex="18">
                                        <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtReferBy" CssClass="form-control" name="txtReferBy" placeholder="Reference Person Name" MaxLength="25" TabIndex="19" /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtReferBy" ValidationExpression="^[a-zA-Z0-9 -.,:\\\/]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
					        </div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID ="radioRecCom" EventName ="CheckedChanged" /> 
                    <asp:AsyncPostBackTrigger ControlID ="radioRecDoc" EventName ="CheckedChanged" /> 
                </Triggers>
                <ContentTemplate >

                
                            <div class="form-group">
						        <label class="control-label col-md-3 col-sm-3" > Receipt Option*:</label>
						        <div class="col-md-6 col-sm-6" >
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="radioRecCom" GroupName="companyReceipt" name="radioCompStatus" value="REGULAR" Checked="true" OnCheckedChanged ="radioRecCom_CheckedChanged" AutoPostBack="true" TabIndex ="20"  />
                                         <span id="span3" runat="server" style="" > Combine Receipt No Number</span>
                                            
                                        </label>
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="radioRecDoc" GroupName="companyReceipt" name="radioCompStatus" value="DEMO" OnCheckedChanged ="radioRecCom_CheckedChanged" AutoPostBack="true" TabIndex="21" />
                                         <span id="span4" runat="server" style="" >Doctor Wise Receipt Number</span>
                                        </label>
                                </div>
					        </div>
               </ContentTemplate>
            </asp:UpdatePanel>
                            <div class="form-group">
							    <label class="control-label col-md-3 col-sm-3"></label>
							    <div class="col-md-6 col-sm-6">
								    <asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="on_Submit_Click" class="btn btn-primary" TabIndex="22" />
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass ="btn btn-danger" Text="Cancel" CausesValidation ="false" OnClick="onLinkButtonClick_Cancel"  Visible="false" TabIndex="23" />
                                    <asp:LinkButton ID="displayCompany" runat="server" CssClass ="btn btn-danger" Text="View Company" CausesValidation ="false" OnClick="onLinkButtonClick" TabIndex="24" />
							    </div>
						    </div>
                        </div> 
                </div> 
            </div> 
         </div>
       
        
     <div>
		<asp:Image id="displayImg" runat="server" Height="150px" Width="150px" AlternateText="Company Image" />
    </div>
    </form> 
</asp:Content>

