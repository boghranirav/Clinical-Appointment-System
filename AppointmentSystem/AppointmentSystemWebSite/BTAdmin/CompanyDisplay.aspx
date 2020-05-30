<%@ Page Title="" Language="C#" MasterPageFile="~/BTAdmin/BTAdminMaster.master" AutoEventWireup="true" CodeFile="CompanyDisplay.aspx.cs" Inherits="BTAdmin_CompanyDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHorlderTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<form id="Form1" runat="server" method="post" action="CompanyDisplay.aspx"  class="form-horizontal form-bordered" name="demo-form" >
    <div class="row">
        <div class="col-md-12">
			<div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                    <h4 class="panel-title">List of Company</h4>
                </div>
                        
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="data-table" class="table table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Code</th>
                                        <th>Name</th>
                                        <th>Address</th>
                                        <th>Status</th>
                                        <th>Create Date</th>
                                        <th>Email</th>
                                        <th>Edit</th>
                                        <th>Doctor</th>
                                    </tr>
                                </thead>
                                <tbody id="displayCompany" runat="server" >
                                   
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
    </div>
</form>
</asp:Content>

