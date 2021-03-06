﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/ReceptionestMaster.master" AutoEventWireup="true" CodeFile="AreaMaster.aspx.cs" Inherits="Receptionest_AreaMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function deletefunction(id1, id2) { //This function call on text change.     
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                type: "POST",
                url: "AreaMaster.aspx/DeleteArea", // this for calling the web method function in cs code.  
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
                    url: 'AreaMaster.aspx',
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
 <form id="Form1" method="post" runat="server" action="AreaMaster.aspx"  class="form-horizontal form-bordered" name="demo-form">
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Area Master</li>
	</ol>	
    <h1 class="page-header">Area Master</h1>

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
                            <h4 class="panel-title">Add Area</h4>
                        </div>
                        <div class="panel-body panel-form">
                            
							<div class="form-group">
								<label class="control-label col-md-3 col-sm-3" for="fullname"> Area Name * :</label>
								<div  class="col-md-6 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" name="txtArea" placeholder="Area Name" MaxLength="50" TabIndex="1" style="text-transform :uppercase;" /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Area Name" ControlToValidate="txtArea" Display="Dynamic"  ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Area Name" ControlToValidate="txtArea" ValidationExpression="^[a-zA-Z ,.]+$" Display="Dynamic"  ForeColor="Red" />
                                    <asp:Label ID="lblDuplicate" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
							</div>

                            <div class="form-group">
								<label class="control-label col-md-3 col-sm-3"></label>
								<div class="col-md-6 col-sm-6">
									<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="submit_Click" class="btn btn-primary" TabIndex ="2" />
                                    <asp:Button ID="btnCancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="btncancel_Click" Visible="false" Text="Cancel" TabIndex ="3"/>
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
                     <h4 class="panel-title">List of Area</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Area</th>
                                     <th>Edit</th>
                                     <th>Delete</th>
                                 </tr>
                             </thead>
                        
                             <tbody id="displayArea" runat="server">
                             </tbody>
                      
                         </table>
                     </div>
                 </div>
             </div>
         </div>

     </div>
                                 
</form>
</asp:Content>

