<%@ page title="" language="C#" masterpagefile="~/Admin/CompanyAdmin.master" autoeventwireup="true" inherits="Admin_SessionTime, App_Web_xuenpgx4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="Form1" method="post" runat="server" action="SessionTime.aspx"  class="form-horizontal form-bordered" name="demo-form">
    
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Doctor Admin</a></li>
			<li class="active">Session Time</li>
	</ol>	
    <h1 class="page-header">Session Time Out</h1>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" ><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" ><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" ><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Add Receptionist</h4>
                </div>
                <div class="panel-body panel-form">
					<div class="form-group">
					<label class="control-label col-md-3 col-sm-3" for="fullname"> Session Time Out In Hours * :</label>
						<div  class="col-md-4 col-sm-4">
                            <asp:TextBox runat="server" ID="txtSession" CssClass="form-control" name="txtSession" placeholder="Session Time Out" MaxLength="1" TabIndex="1" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Session Time Out" ControlToValidate="txtSession" Display="Dynamic"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="*Invalid Session Time" ControlToValidate="txtSession" ValidationExpression="^[1-8]{1}" Display="Dynamic"  ForeColor="Red" />
                        </div>
                    </div>
          
                    
                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" class="btn btn-primary" TabIndex ="2" OnClick="submit_Click" />
						</div>
					</div>

                     <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<span style="color:red">
                               * Maximum 8 Hours And Minimum 1 Hour
							</span>
						</div>
					</div>
                </div>
            </div>
        </div>
    </div>

</form>
</asp:Content>

