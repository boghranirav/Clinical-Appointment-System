﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BTAdmin/BTAdminMaster.master" AutoEventWireup="true" CodeFile="SpecializationAdd.aspx.cs" Inherits="BTAdmin_SpecializationAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script type="text/javascript">
         function deletefunction(id1, id2) { //This function call on text change.     
             if (confirm("Are you sure you want to delete?")) {
                 $.ajax({
                     type: "POST",
                     url: "SpecializationAdd.aspx/DeleteSpecialazation", // this for calling the web method function in cs code.  
                     data: '{spid: "' + id1 + '" ,userid: "' + id2 + '"}', // user name or email value  
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
                         url: 'SpecializationAdd.aspx',
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
 <form id="Form1" method="post" runat="server" action="SpecializationAdd.aspx"  class="form-horizontal form-bordered" name="demo-form">

    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Admin</a></li>
			<li class="active">Specialization</li>
	</ol>	
    <h1 class="page-header">Specialization</h1>

 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <Triggers >
        <asp:AsyncPostBackTrigger ControlID="cmbFormSelect" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate >
    
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
                            <h4 class="panel-title">Add Specialization</h4>
                        </div>
                        <div class="panel-body panel-form">
                            
								<div class="form-group">
									<label class="control-label col-md-3 col-sm-3" for="fullname"> Specialization Name * :</label>
									<div  class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtSpecialization" CssClass="form-control" name="txtSpecialization" placeholder="Specialization" MaxLength="60" TabIndex="1" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Specialization" ControlToValidate="txtSpecialization" Display="Dynamic"  ForeColor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*Invalid Specialization Description" ControlToValidate="txtSpecialization" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"  ForeColor="Red" />
                                     </div>
								</div>

                                <div class="form-group">
                                    <label class="col-md-3 control-label">Select (multiple)</label>
                                    <div class="col-md-6">
                                        <asp:ListBox runat="server" ID="cmbFormSelect" class="form-control" SelectionMode ="Multiple" AutoPostBack="true" OnSelectedIndexChanged="onFormIndexChange">                                            
                                            
                                        </asp:ListBox>
                                    </div>
                                </div>

                                <div class="form-group">
									<label class="control-label col-md-3">Selected Form</label>
									<div class="col-md-6">
                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered" DataKeyNames="FormId"  >
                                        <Columns>
                                            <asp:BoundField DataField="FormId" HeaderText="Key"/>
                                            <asp:BoundField DataField="FormName" HeaderText="Degree Name"/>
                                            <asp:TemplateField  HeaderText="SortingSeq" >
                                                <ItemTemplate>
                                                    <div  class="col-md-1 col-sm-1">
                                                    <asp:TextBox ID="TextBoxS" runat ="server" Text='<%# Eval("SortingSeq") %>' Width ="2cm" />
                                                    </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
									</div>
								</div>

                            	<div class="form-group">
									<label class="control-label col-md-3 col-sm-3"></label>
									<div class="col-md-6 col-sm-6">
										<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" OnClick="onSubmit_Click" class="btn btn-primary" />
                                        <asp:Button ID="btncancel" name="btncancel" runat="server" class="btn btn-danger" OnClick="onCancel_Click" Visible="false" Text="Cancel"/>
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
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                     </div>
                     <h4 class="panel-title">List of Specialization</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Code</th>
                                     <th>Specialization</th>
                                     <th>Edit</th>
                                     <th>Delete</th>
                                 </tr>
                             </thead>
                             <tbody id="displaySpec" runat="server">
                             </tbody>
                         </table>
                     </div>
                 </div>
             </div>
         </div>
     </div>
</form>
</asp:Content>

