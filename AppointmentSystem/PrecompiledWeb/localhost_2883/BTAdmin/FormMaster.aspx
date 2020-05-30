<%@ page title="" language="C#" masterpagefile="~/BTAdmin/BTAdminMaster.master" autoeventwireup="true" inherits="BTAdmin_FormMaster, App_Web_2lyrwnv0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "FormMaster.aspx/DeleteForm", // this for calling the web method function in cs code.  
                data: '{fid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
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
                    url: 'FormMaster.aspx',
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
    <form id="Form1" method="post" runat="server" action="FormMaster.aspx"  class="form-horizontal form-bordered" name="demo-form">
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Admin</a></li>
			<li class="active">Forms</li>
	</ol>	
    <h1 class="page-header">Forms</h1>

    <div class="row">
        <div class="col-md-12">
            
			        <!-- begin panel -->
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-heading-btn">
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                            </div>
                            <h4 class="panel-title">Add Form</h4>
                        </div>
                        <div class="panel-body panel-form">
                            
								<div class="form-group">
									<label class="control-label col-md-3 col-sm-3" > Form Name * :</label>
									<div  class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtFormName" CssClass="form-control" name="txtFormName" placeholder="Form Name" MaxLength="50" TabIndex="1" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Form Name" ControlToValidate="txtFormName" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Form Name" ControlToValidate="txtFormName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                                     </div>
								</div>

                                <div class="form-group">
									<label class="control-label col-md-3 col-sm-3" > Form Button Name * :</label>
									<div  class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtButtonName" CssClass="form-control" name="txtButtonName" placeholder="Form Button Name" MaxLength="30" TabIndex="2" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Button Name" ControlToValidate="txtButtonName" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Button Name" ControlToValidate="txtButtonName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"  ForeColor="Red" />
                                     </div>
								</div>

                                <div class="form-group">
									<label class="control-label col-md-3 col-sm-3" > Form Category * :</label>
									<div  class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtCategory" CssClass="form-control" name="txtCategory" placeholder="Form Category" MaxLength="30" TabIndex="3" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Category" ControlToValidate="txtCategory" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Category" ControlToValidate="txtCategory" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"  ForeColor="Red" />
                                     </div>
								</div>

                            	<div class="form-group">
									<label class="control-label col-md-3 col-sm-3"></label>
									<div class="col-md-6 col-sm-6">
										<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="onSubmit_Click" class="btn btn-primary" TabIndex="4" />
                                        <asp:Button ID="btncancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="onCancel_Click" Visible="false" Text="Cancel" TabIndex="5"/>
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
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                     </div>
                     <h4 class="panel-title">List of Forms</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Form Name</th>
                                     <th>Button Name</th>
                                     <th>Category</th>
                                     <th>Edit</th>
                                     <th>Delete</th>
                                 </tr>
                             </thead>
                             <tbody id="displayForm" runat="server">
                             </tbody>
                         </table>
                     </div>
                 </div>
             </div>
         </div>
     </div>
</form>
</asp:Content>

