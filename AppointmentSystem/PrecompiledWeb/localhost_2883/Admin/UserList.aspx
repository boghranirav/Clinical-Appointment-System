<%@ page title="" language="C#" masterpagefile="~/Admin/CompanyAdmin.master" autoeventwireup="true" inherits="Admin_UserList, App_Web_xuenpgx4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Doctor Admin</a></li>
			<li class="active">List Of User</li>
	</ol>	
    <h1 class="page-header">User List</h1>

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
                                     <th>Role</th>
                                     <th>User Name</th>
                                     <th>Login Id</th>
                                     <th>IsOnline</th>
                                     <th>Created On</th>
                                 </tr>
                             </thead>
                             <tbody id="displayDoc" runat="server">
                             </tbody>
                         </table>
                     </div>
                 </div>
             </div>
         </div>
     </div>
</asp:Content>

