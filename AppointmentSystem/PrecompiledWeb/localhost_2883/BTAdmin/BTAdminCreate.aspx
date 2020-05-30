<%@ page title="" language="C#" masterpagefile="~/BTAdmin/BTAdminMaster.master" autoeventwireup="true" inherits="BTAdmin_BTAdminCreate, App_Web_2lyrwnv0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
	.disabled {
        pointer-events: none;
        cursor: default;
        opacity: 0.6;
    }
</style>
    <script type="text/javascript">
        function deletefunction(id1,id2) { //This function call on text change.     
            if (confirm("Are you sure you want to delete?")) {
                $.ajax({
                    type: "POST",
                    url: "BTAdminCreate.aspx/DeleteAdmin", // this for calling the web method function in cs code.  
                    data: '{adminid: "' + id1 + '" ,userid: "'+ id2 +'"}', // user name or email value  
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
                        url: 'BTAdminCreate.aspx',
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
    

<form id="Form1" runat="server" method="post" action="BTAdminCreate.aspx"  class="form-horizontal form-bordered" name="demo-form">
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Admin</a></li>
			<li class="active">Admin Registration</li>
	</ol>	
    <h1 class="page-header">Admin Registration</h1>

    <div class="row">
        <div class="col-md-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="txtLastName" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtMiddleName" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate >

                
            
			        <!-- begin panel -->
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-heading-btn">
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                            </div>
                            <h4 class="panel-title">Admin Registration Form</h4>
                        </div>
                        <div class="panel-body panel-form">
                            
								<div class="form-group">
									<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
									<div  class="col-md-2 col-sm-2">
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" name="txtFirstName" placeholder="First Name" MaxLength="15" TabIndex="1" style="text-transform :uppercase;" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter First Name" ControlToValidate="txtFirstName" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"  ForeColor="Red" />
                                    </div>
                                    <div  class="col-md-2 col-sm-2">
                                        <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" name="txtMiddleName" placeholder="Middle Name" MaxLength="15" TabIndex="2" style="text-transform :uppercase;" OnTextChanged ="CreateUserId" AutoPostBack="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldVMiddleName" runat="server" ErrorMessage="*Enter Middle Name" ControlToValidate="txtMiddleName" Display="Dynamic"  ForeColor="Red"/>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtMiddleName" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"  ForeColor="Red" />
                                    </div>
                                    <div  class="col-md-2 col-sm-2">
                                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" name="txtLastName" placeholder="Last Name" MaxLength="15" TabIndex="3" OnTextChanged ="CreateUserId" AutoPostBack="true" style="text-transform :uppercase;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldVLastName" runat="server" ErrorMessage="*Enter Last Name" ControlToValidate="txtLastName" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"  ForeColor="Red" />
									</div>
								</div>

                                <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3">Designation * :</label>
                                        <div class="col-md-6 col-sm-6" >
                                            <asp:DropDownList ID="cmbDesignation" runat="server" class="form-control" TabIndex="4" >
                                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                                            <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                                            <asp:ListItem Value="MKT">MARKETING</asp:ListItem>                                                
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldVDesignation" runat="server" ErrorMessage="*Select Designation." ControlToValidate="cmbDesignation" ForeColor="Red" InitialValue="SELECT" Display="Dynamic" />
                                        </div>
                                    
                                </div>

								<div class="form-group">
									<label class="control-label col-md-3 col-sm-3" >Login Id * :</label>
									<div class="col-md-6 col-sm-6">
										<asp:TextBox runat="server" ID="txtLoginId" class="form-control" name="txtLoginId" placeholder="Login Id"  TabIndex="5" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldVLogin" runat="server" ErrorMessage="*Enter Login Id" ControlToValidate="txtLoginId" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Invalid LoginId" ControlToValidate="txtLoginId" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*$" Display="Dynamic"  ForeColor="Red" />
                                        <div id="checkusernameoremail" runat="server">                             
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>  
                                        </div>
									</div>
								</div>

                                <div class="form-group">
									<label class="control-label col-md-3 col-sm-3" >Password * :</label>
									<div class="col-md-6 col-sm-6">
										<asp:TextBox runat="server" ID="txtPassword" class="form-control" name="txtPassword" placeholder="Password" TextMode ="Password" TabIndex="6" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldVPassword" runat="server" ErrorMessage="*Enter Password" ControlToValidate="txtPassword" Display="Dynamic"  ForeColor="Red"  />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Password must contain: Minimum 6 characters atleast 1 Alphabet and 1 Number." ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" ForeColor="Red" ControlToValidate="txtPassword" Display="Dynamic" ></asp:RegularExpressionValidator>
									</div>
								</div>

                                  <div class="form-group">
									<label class="control-label col-md-3 col-sm-3" for="email">Email/Phone * :</label>
									<div  class="col-md-4 col-sm-4 ">
                                            <asp:TextBox runat="server" ID="txtEmail" class="form-control" name="txtEmail" placeholder="Email"  MaxLength ="30" TabIndex ="7"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldVEmail" runat="server" ErrorMessage="*Enter Email" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                            <asp:TextBox runat ="server" ID="txtPhone"  name="txtPhone" class="form-control" placeholder="Phone Number" MaxLength ="10" TabIndex ="8" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Enter Valid Mobile Number." ControlToValidate="txtPhone" ValidationExpression="[789][0-9]{9}" Display="Dynamic"  ForeColor="Red" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldVPhone" runat="server" ErrorMessage="*Enter Phone" ControlToValidate="txtPhone" Display="Dynamic"  ForeColor="Red" />
									</div> 
								</div>

								<div class="form-group">
									<label class="control-label col-md-3 col-sm-3"></label>
									<div class="col-md-6 col-sm-6">
										<asp:Button ID="submit" name="submit" runat ="server" OnClick="submit_click" Text="Submit" class="btn btn-primary" TabIndex="8" />
                                        <asp:Button ID="btncancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="onCancel_Click" Visible="false" Text="Cancel" TabIndex="9"/>
									</div> 
            					</div>
                            
                        </div>
                    </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
                    <!-- end panel -->
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
                    <h4 class="panel-title">List of Admin</h4>
                </div>
                        
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="data-table" class="table table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>LoginId</th>
                                        <th>Phone</th>
                                        <th>Designation</th>
                                        <th>IsOnline</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody id="displayAdmin" runat="server">
                                        
                                        
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
    </div>

</form>

</asp:Content>

