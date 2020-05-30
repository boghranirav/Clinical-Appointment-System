<%@ page title="" language="C#" masterpagefile="~/Receptionist/ReceptionestMaster.master" autoeventwireup="true" inherits="Receptionist_TodayEntry, App_Web_xkji30bz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <form id="Form1" method="post" runat="server" action="TodayEntry.aspx"  class="form-horizontal form-bordered" name="demo-form">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cmbDoctor" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="cmbShift" EventName="SelectedIndexChanged" />
    </Triggers>
        <ContentTemplate>

    <ol class="breadcrumb pull-right">
			<li><a href="javascript:;">Reception</a></li>
			<li class="active">Today Entry</li>
	</ol>	
    <h1 class="page-header">Today Entry <asp:Label ID ="lblTodayDate" runat ="server"  ></asp:Label></h1>

    <div class="row">
        <div class="col-md-12" style="margin-left:-4px;padding:0;">
           <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">Select</h4>
                </div>
                <div class="panel-body panel-form">
                            
					<div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Select Doctor * :</label>
						<div  class="col-md-5 col-sm-5">
                            <asp:DropDownList ID="cmbDoctor" runat="server" CssClass="form-control" TabIndex="3" OnSelectedIndexChanged="cmbDoctor_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
					</div>

                    <div class="form-group">
						<label class="control-label col-md-3 col-sm-3" for="fullname"> Select Session * :</label>
						<div  class="col-md-3 col-sm-3">
                            <asp:DropDownList ID="cmbShift" runat="server" CssClass="form-control" TabIndex="3" OnSelectedIndexChanged="cmbDoctor_SelectedIndexChanged" AutoPostBack ="true"  >
                            <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                            <asp:ListItem Value="0">MORNING</asp:ListItem>
                            <asp:ListItem Value="1">EVENING</asp:ListItem>
                            </asp:DropDownList>
                        </div>
					</div>

                </div>
            </div>
        </div>
    </div>
               
     
     <div class="row">
        <div class="col-md-6" style="margin-left:-4px;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">
                        <asp:Label ID="lblPatientToday" runat="server" Text="Today Appointments" ></asp:Label>
                    </h4>
                </div>
                <div class="panel-body panel-form">
                    <asp:GridView ID="gridTodayAppointment" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="AppointmentId" OnSelectedIndexChanged="gridTodayAppointment_SelectedIndexChanged" OnRowDataBound ="gridTodayAppointment_RowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="PatientName" HeaderText="Name" ReadOnly="true" ItemStyle-Width="250px" />
                            <asp:BoundField DataField="AppointmentTime" DataFormatString="{0:H:mm}" HeaderText="Time" HtmlEncode="false" ReadOnly="true" />
                            <asp:CommandField ShowSelectButton ="true" SelectText ="Select" ButtonType ="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="col-md-6" style="margin-left:2px;padding:0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                    </div>
                    <h4 class="panel-title">
                        <asp:Label ID="lblPatientPresent" runat="server" Text="Patients Present"></asp:Label>
                    </h4>
                </div>
                <div class="panel-body panel-form">
                    <asp:GridView ID="gridPresentAppointment" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="AppointmentId" >
                        <Columns>
                            <asp:BoundField DataField="PatientName" HeaderText="Name" ReadOnly="true" ItemStyle-Width="250px" />
                            <asp:BoundField DataField="AppointmentTime" HeaderText="Time" ReadOnly="true" />

                            <asp:TemplateField HeaderText="ArriveTime" ItemStyle-Width="120px" >
                                <ItemTemplate>
                                    <asp:Label ID="Label2" Text='<%# Eval("AppointmentTime") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ArriveTime" ItemStyle-Width="120px" >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" Text='<%# Eval("ArrivedDateTime", "{0:h:mm tt}") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton ="true" SelectText ="Select" ButtonType ="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div> 

    </ContentTemplate>
</asp:UpdatePanel>                 
</form>
</asp:Content>

