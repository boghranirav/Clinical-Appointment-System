<%@ page title="" language="C#" masterpagefile="~/Admin/CompanyAdmin.master" autoeventwireup="true" inherits="Admin_Receptionist, App_Web_xuenpgx4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "Receptionist.aspx/DeleteReceptionist", // this for calling the web method function in cs code.  
                data: '{recid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
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
                    url: 'Receptionist.aspx',
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

<form id="Form1" method="post" runat="server" action="Receptionist.aspx"  class="form-horizontal form-bordered" name="demo-form">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Admin</a></li>
			<li class="active">Receptionist</li>
	</ol>	
    <h1 class="page-header">Receptionist</h1>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger  ControlID="txtFirstName" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger  ControlID="txtMiddleName" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger  ControlID="txtLastName" EventName="TextChanged" />
    </Triggers>
    <ContentTemplate>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" ><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" ><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Add Receptionist</h4>
                </div>
                <div class="panel-body panel-form">
                            
					<div class="form-group">
					<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
						<div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" name="txtFirstName" placeholder="First Name" MaxLength="20" TabIndex="1" style="text-transform :uppercase;" OnTextChanged="CreateUserId" AutoPostBack="true" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter First Name" ControlToValidate="txtFirstName" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid First Name" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" name="txtMiddleName" placeholder="Middle Name" MaxLength="20" TabIndex="2" style="text-transform :uppercase;" OnTextChanged="CreateUserId" AutoPostBack="true" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Middle Name" ControlToValidate="txtMiddleName" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Middle Name" ControlToValidate="txtMiddleName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                        </div>
                        <div  class="col-md-2 col-sm-2">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" name="txtLastName" placeholder="Last Name" MaxLength="20" TabIndex="3" style="text-transform :uppercase;" OnTextChanged="CreateUserId" AutoPostBack="true" /> 
                          
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Login Id * :</label>
						<div  class="col-md-3 col-sm-3">
                            <asp:TextBox runat="server" ID="txtLoginId" CssClass="form-control" name="txtLoginId" placeholder="Login Id" TabIndex="4" MaxLength="20" style="text-transform :capitalize;" Enabled="false" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Login Id" ControlToValidate="txtLoginId" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Login Id" ControlToValidate="txtLoginId" ValidationExpression="^[a-zA-Z0-9]{3,}$" Display="Dynamic"  ForeColor="Red" />
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" class="btn btn-primary" TabIndex ="5" OnClick="submit_Click" />
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass ="btn btn-danger" Text="Cancel" CausesValidation ="false" TabIndex="6" Visible="false" OnClick="btnCancel_Click" />
						</div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<span style="color:red">Note :
                                <ul>
                                    <li>Once Login Id created will not be changed.</li>
                                    <li>Login Id id 3 Character logn.</li>
                                    <li>First Name + Middle Name + Last Name </li>
                                    <li>Generate by system and cannot be changed.</li>
                                </ul>
							</span>
						</div>
					</div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
</asp:UpdatePanel>
    
     <div class="row">
         <div class="col-md-12">
             <div class="panel panel-primary">
                 <div class="panel-heading">
                     <div class="panel-heading-btn">
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" ><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" ><i class="fa fa-times"></i></a>
                     </div>
                     <h4 class="panel-title">List of Receptionist</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Name</th>
                                     <th>Login Id</th>
                                     <th>IsOnline</th>
                                     <th>Edit</th>
                                     <th>Delete</th>
                                 </tr>
                             </thead>
                             <tbody id="displayRec" runat="server">
                             </tbody>
                         </table>
                     </div>
                 </div>
             </div>
         </div>
     </div>

</form>
</asp:Content>

