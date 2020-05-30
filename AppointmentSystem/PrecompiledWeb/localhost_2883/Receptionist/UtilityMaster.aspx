<%@ page title="" language="C#" masterpagefile="~/Receptionist/ReceptionestMaster.master" autoeventwireup="true" inherits="Receptionist_UtilityMaster, App_Web_xkji30bz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "UtilityMaster.aspx/DeleteMedicine", // this for calling the web method function in cs code.  
                data: '{aid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
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
                    url: 'UtilityMaster.aspx',
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
 <form id="Form1" method="post" runat="server" action="UtilityMaster.aspx"  class="form-horizontal form-bordered" name="demo-form">
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Utility Master</li>
	</ol>	
    <h1 class="page-header">Uitility Master</h1>

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
                            <h4 class="panel-title">Add Uitility </h4>
                        </div>
                        <div class="panel-body panel-form">
                            
							<div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
								<div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" name="txtName" placeholder="Uitility Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Uitility Name" ControlToValidate="txtName" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z0-9 ,-.()\[\]\\\/]+$" Display="Dynamic"  ForeColor="Red" />
                                    <asp:Label ID="lblDuplicate" runat="server" ForeColor="Red"></asp:Label>
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Address * :</label>
								<div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" name="txtAddress" placeholder="Address" MaxLength="200" TabIndex="2" style="text-transform :capitalize;" TextMode="MultiLine" Rows="3" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter Address" ControlToValidate="txtAddress" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Address" ControlToValidate="txtAddress" ValidationExpression="^[a-zA-Z0-9,.\s-\\\/ ]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Email/Contact No * :</label>
								<div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" name="txtEmail" placeholder="Email" MaxLength="40" TabIndex="3" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Email" ControlToValidate="txtEmail" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"  ForeColor="Red" />
                                </div>
                                <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" name="txtMobile" placeholder="Contact Number" MaxLength="25" TabIndex="4"/> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Contact Number" ControlToValidate="txtMobile" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Number" ControlToValidate="txtMobile" ValidationExpression="^[0-9 ,-]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Remark * :</label>
								<div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" name="txtRemark" placeholder="Remark" MaxLength="200" TabIndex="5" TextMode="MultiLine" Rows="3"  /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Enter Remark" ControlToValidate="txtRemark" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Invalid Remark" ControlToValidate="txtRemark" ValidationExpression="^[a-zA-Z0-9 ,-.()\s\[\]\\\/*+]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3"></label>
								<div class="col-md-6 col-sm-6">
									<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="submit_Click" class="btn btn-primary" TabIndex ="6" />
                                    <asp:Button ID="btnCancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="btnCancel_Click" Visible="false" Text="Cancel" TabIndex ="7"/>
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
                     <h4 class="panel-title">List of Reference Doctor</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Name</th>
                                     <th>Address</th>
                                     <th>Email</th>
                                     <th>Contact No</th>
                                     <th>Remark</th>
                                     <th>Edit</th>
                                     <th>Delete</th>
                                 </tr>
                             </thead>
                        
                             <tbody id="displayDoctor" runat="server">
                             </tbody>
                      
                         </table>
                     </div>
                 </div>
             </div>
         </div>

     </div>
                                 
</form>
</asp:Content>

