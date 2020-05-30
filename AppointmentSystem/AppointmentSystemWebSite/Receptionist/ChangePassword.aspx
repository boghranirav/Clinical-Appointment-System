<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/ReceptionestMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Receptionist_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<form id="Form1" method="post" runat="server" action="ChangePassword.aspx"  class="form-horizontal form-bordered" name="demo-form">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Doctor Admin</a></li>
			<li class="active">Change Password</li>
	</ol>	
    <h1 class="page-header">Change Password</h1>

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
                    <h4 class="panel-title">Change Password</h4>
                </div>
                <div class="panel-body panel-form">


                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3" for="fullname">Old Password * :</label>
                        <div class="col-md-5 col-sm-5">
                            <asp:TextBox runat="server" ID="txtOldPassword" CssClass="form-control" name="txtOldPassword" placeholder="Old Password" MaxLength="20" TabIndex="1"  TextMode="Password"  OnTextChanged="txtOldPassword_TextChanged" AutoPostBack="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldVFirstName" runat="server" ErrorMessage="*Enter Old Password" ControlToValidate="txtOldPassword" Display="Dynamic" ForeColor="Red" />
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="txtOldPassword" EventName="TextChanged"/>
        </Triggers>
        <ContentTemplate>
                            <asp:Label runat="server" ID="lblOldPass" Text="" ForeColor="Red"></asp:Label>
         </ContentTemplate>
    </asp:UpdatePanel>
                        </div>
                    </div>
           
                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname">New Password * :</label>
                        <div class="col-md-5 col-sm-5">
                            <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" name="txtNewPassword" placeholder="New Password" MaxLength="20" TabIndex="2" TextMode="Password"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Enter New Password" ControlToValidate="txtNewPassword" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*Password must contain: Minimum 6 characters atleast 1 Alphabet and 1 Number." ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" Display="Dynamic" ForeColor="Red" />
                        </div>
                    </div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname">Re-Enter Password * :</label>
                        <div class="col-md-5 col-sm-5">
                            <asp:TextBox runat="server" ID="txtRePassword" CssClass="form-control" name="txtRePassword" placeholder="Re-Enter Password" MaxLength="20" TabIndex="3" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Enter Password" ControlToValidate="txtRePassword" Display="Dynamic" ForeColor="Red" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Does Not Match." ControlToValidate="txtRePassword" ControlToCompare="txtNewPassword" Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                        </div>
                    </div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3"></label>
						<div class="col-md-6 col-sm-6">
							<asp:Button ID="submit" name="submit" runat ="server" Text="Submit" class="btn btn-primary" TabIndex ="4" OnClick="submit_Click" />
						</div>
					</div>
                </div>
            </div>
        </div>
    </div>

</form>
</asp:Content>

