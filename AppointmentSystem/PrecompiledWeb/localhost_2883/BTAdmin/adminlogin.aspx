<%@ page language="C#" autoeventwireup="true" inherits="BTAdmin_adminlogin, App_Web_hmrbbchy" enableeventvalidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Admin Login</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />

	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet"/>
	<link href="../assets/plugins/jquery-ui-1.10.4/themes/base/minified/jquery-ui.min.css" rel="stylesheet" />
	<link href="../assets/plugins/bootstrap-3.2.0/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../assets/plugins/font-awesome-4.2.0/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../assets/css/animate.min.css" rel="stylesheet" />
	<link href="../assets/css/style.min.css" rel="stylesheet" />
	<link href="../assets/css/style-responsive.min.css" rel="stylesheet" />
</head>
<body>
    <div class="login-cover">
	    <div class="login-cover-image"><img src="../assets/img/login-bg/bg-6.jpg" data-id="login-cover-image" alt="" /></div>
	    <div class="login-cover-bg"></div>
	</div>
	<!-- begin #page-container -->
	<div id="page-container" class="fade">
	    <!-- begin login -->
        <div class="login login-v2" data-pageload-addclass="animated flipInX">
            <!-- begin brand -->
            <div class="login-header">
                <div class="brand" style="font-family:Stencil;font-size:34px;">
                    <img src="../assets/img/LOGOIMAGE.bmp" style="height:50px; width:50px;" /> Blue-Tech
                   
                </div>
                <div class="icon">
                    <i class="fa fa-sign-in"></i>
                </div>
            </div>
            <!-- end brand -->
            <div class="login-content">
                <form id="Form1" action="adminlogin.aspx" method="POST" runat="server" class="margin-bottom-0">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers > 
                            <asp:AsyncPostBackTrigger ControlID="submit" EventName="Click" />
                        </Triggers>
                        <ContentTemplate >

                            <div class="form-group m-b-20">
                                <asp:TextBox ID="txtLoginId" runat="server" class="form-control input-lg" placeholder="Login ID" MaxLength="20" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Please Enter Login Id." ControlToValidate="txtLoginId" ForeColor="#ff0000" Font-Size="15px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularFieldValidate1" runat="server" ControlToValidate="txtLoginId" ErrorMessage="*Invalid Login Id." ValidationExpression="^[a-zA-Z0-9]+$" Display="Dynamic"  ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group m-b-20">
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control input-lg" placeholder="Password" TextMode="Password" MaxLength="44"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Please Enter Password." ControlToValidate="txtPassword" ForeColor="#ff0000" Font-Size="15px" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                            </div>
                            <div class="checkbox m-b-20">
                                <label>
                                    <asp:CheckBox ID="chkRememberMe" runat="server"  /> Remember Me
                                </label>
                            </div>
                    
                            <div class="login-buttons">
                                <asp:Button ID="submit" runat="server" class="btn btn-success btn-block btn-lg" OnClick="LoginClick" Text="Sign Me In"/>
                            </div>
                            <div class="form-group m-b-20" style="margin-top :1cm">
                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="#ff0000" Font-Size="15px"></asp:Label>
                            </div>
                            <div class="form-group m-b-20" style="margin-top :1cm">
                                <span id="spanDisplay" runat="server" style="color:#f3f3f3" ></span>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>
            </div>
        </div>
        <!-- end login -->
    
	</div>
	
	<!-- ================== BEGIN BASE JS ================== -->
	<script src="../assets/plugins/jquery-1.8.2/jquery-1.8.2.min.js"></script>
	<script src="../assets/plugins/jquery-ui-1.10.4/ui/minified/jquery-ui.min.js"></script>
	<script src="../assets/plugins/bootstrap-3.2.0/js/bootstrap.min.js"></script>
	<!--[if lt IE 9]>
		<script src="assets/crossbrowserjs/html5shiv.js"></script>
		<script src="assets/crossbrowserjs/respond.min.js"></script>
		<script src="assets/crossbrowserjs/excanvas.min.js"></script>
	<![endif]-->
	<script src="../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
	<script src="../assets/plugins/jquery-cookie/jquery.cookie.js"></script>
	<!-- ================== END BASE JS ================== -->
	
	<!-- ================== BEGIN PAGE LEVEL JS ================== -->
	<script src="../assets/js/login-v2.demo.min.js"></script>
	<script src="../assets/js/apps.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->

	<script>
	    $(document).ready(function () {
	        App.init();
	        LoginV2.init();
	    });
	</script>
</body>
</html>
