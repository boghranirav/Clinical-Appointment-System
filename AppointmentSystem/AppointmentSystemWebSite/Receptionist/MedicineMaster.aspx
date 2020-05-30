<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/ReceptionestMaster.master" AutoEventWireup="true" CodeFile="MedicineMaster.aspx.cs" Inherits="Receptionist_MedicineMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "MedicineMaster.aspx/DeleteMedicine", // this for calling the web method function in cs code.  
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
                    url: 'MedicineMaster.aspx',
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
 <form id="Form1" method="post" runat="server" action="MedicineMaster.aspx"  class="form-horizontal form-bordered" name="demo-form">
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Medicine Master</li>
	</ol>	
    <h1 class="page-header">Medicine Master</h1>

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
                            <h4 class="panel-title">Add Medicine </h4>
                        </div>
                        <div class="panel-body panel-form">
                            
							<div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Name * :</label>
								<div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtMName" CssClass="form-control" name="txtMName" placeholder="Medicine Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Medicine Name" ControlToValidate="txtMName" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Name" ControlToValidate="txtMName" ValidationExpression="^[a-zA-Z0-9 ,-.()\[\]\\\/]+$" Display="Dynamic"  ForeColor="Red" />
                                    <asp:Label ID="lblDuplicate" runat="server" ForeColor="Red"></asp:Label>
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Child/Adult Dose * :</label>
								<div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtChildDose" CssClass="form-control" name="txtChildDose" placeholder="Child Dose" MaxLength="20" TabIndex="2" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Child Dose" ControlToValidate="txtChildDose" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*Invalid Child Dose" ControlToValidate="txtChildDose" ValidationExpression="^[0-9 ,-.()\[\]\\\/]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
                                <div  class="col-md-3 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtAdultDose" CssClass="form-control" name="txtAdultDose" placeholder="Adult Dose" MaxLength="20" TabIndex="3"/> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Enter Adult Dose" ControlToValidate="txtAdultDose" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Invalid Adult Dose" ControlToValidate="txtAdultDose" ValidationExpression="^[0-9 ,-.()\[\]\\\/]+$" Display="Dynamic"  ForeColor="Red" />
                                </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3"></label>
								<div class="col-md-6 col-sm-6">
									<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="submit_Click" class="btn btn-primary" TabIndex ="4" />
                                    <asp:Button ID="btnCancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="btnCancel_Click" Visible="false" Text="Cancel" TabIndex ="5"/>
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
                                     <th>Child Dose</th>
                                     <th>Adult Dose</th>
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

