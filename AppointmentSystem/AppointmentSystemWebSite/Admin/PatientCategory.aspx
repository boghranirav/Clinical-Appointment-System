<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/CompanyAdmin.master" AutoEventWireup="true" CodeFile="PatientCategory.aspx.cs" Inherits="Admin_PatientCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "PatientCategory.aspx/DeletePatient", // this for calling the web method function in cs code.  
                data: '{pid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
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
                    url: 'PatientCategory.aspx',
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
<form id="Form1" method="post" runat="server" action="PatientCategory.aspx"  class="form-horizontal form-bordered" name="demo-form">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Doctor Admin</a></li>
			<li class="active">Patient Category Master</li>
	</ol>	
    <h1 class="page-header">Patient Category Master</h1>

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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="submit"  EventName="Click"/>
                    </Triggers>
                    <ContentTemplate>

					<div class="form-group">
					<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
						<div  class="col-md-6 col-sm-6">
                            <asp:TextBox runat="server" ID="txtPatientCategory" CssClass="form-control" name="txtPatientCategory" placeholder="Patient Category" MaxLength="20" TabIndex="1" style="text-transform :uppercase;" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Patient Category" ControlToValidate="txtPatientCategory" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid Category" ControlToValidate="txtPatientCategory" ValidationExpression="^[a-zA-Z0-9 ]+$" Display="Dynamic"  ForeColor="Red" />
                            <asp:Label ID="lblCheckDuplicate" runat="server" Text="" ForeColor="Red" ></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>

                    <div class="form-group">
					<label class="control-label col-md-3 col-sm-3" for="fullname"> Pick Color * :</label>
						<div  class="col-md-6 col-sm-6">
                            <div class="input-group colorpicker-component" data-color="rgb(0, 0, 0)" data-color-format="rgb"  id="colorpicker-prepend">
							<input runat="server" id="txtColor" type="text" value="#000000" readonly="" class="form-control" />
							<span class="input-group-addon" ><i></i></span>
						</div>
                        </div>
                    </div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" class="btn btn-primary" TabIndex ="3" OnClick="submit_Click" />
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass ="btn btn-danger" Text="Cancel" CausesValidation ="false" TabIndex="4" Visible="false" OnClick="btnCancel_Click" />
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
                                     <th></th>
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

