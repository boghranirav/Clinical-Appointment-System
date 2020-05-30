<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/ReceptionestMaster.master" AutoEventWireup="true" CodeFile="ViewPatient.aspx.cs" Inherits="Receptionist_ViewPatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
         <div class="col-md-12">
             <div class="panel panel-primary">
                 <div class="panel-heading">
                     <div class="panel-heading-btn">
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                     </div>
                     <h4 class="panel-title">List of Patient</h4>
                 </div>

                 <div class="panel-body">
                     <div class="table-responsive">
                         <table id="data-table" class="table table-striped table-bordered">
                             <thead>
                                 <tr>
                                     <th>Name</th>
                                     <th>Contact No</th>
                                     <th>Gender</th>
                                     <th>Age</th>
                                     <th>Edit</th>
                                 </tr>
                             </thead>
                        
                             <tbody id="displayPatient" runat="server">
                             </tbody>
                      
                         </table>
                     </div>
                 </div>
             </div>
         </div>

     </div> 
</asp:Content>

