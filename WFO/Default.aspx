<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFO.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="icon" href="Imagenes/logo.ico" sizes="32x32" />
    <title>Met Work Tool</title>

	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->
    <link href="CSS/login/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="CSS/login/fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="CSS/login/fonts/Linearicons-Free-v1.0.0/icon-font.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="CSS/login/vendor/animate/animate.css" rel="stylesheet" />
<!--===============================================================================================-->	
    <link href="CSS/login/vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="CSS/login/vendor/select2/select2.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="CSS/login/css/main.css" rel="stylesheet" />
    <link href="CSS/login/css/util.css" rel="stylesheet" />
<!--===============================================================================================-->

</head>
<body>
    <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100 p-l-30 p-r-30 p-t-20 p-b-10">
				<div style="align-items: center;text-align: center;">
                    <img src="Imagenes/logo_grap.jpg" width="200" height="50"/>
				</div>
				<hr>
                    <form id="form1" runat="server" class="login100-form validate-form">
                        <span class="login100-form-title p-b-30">
						    Met Work Tool.<br>E X P A N D
					    </span>
                        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>

                        <div class="wrap-input100 validate-input m-b-16" data-validate = "Valid email is required: ex@abc.xyz">
                            <asp:TextBox runat="server" ID="txUsuario" required="" AutoCompleteType="Disabled" class="input100" placeholder="Username"></asp:TextBox>
						    <span class="focus-input100"></span>
						    <span class="symbol-input100">
							    <span class="lnr lnr-envelope"></span>
						    </span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server" TargetControlID="txUsuario" FilterMode="ValidChars" ValidChars="1234567890._-abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
					    </div>
                        
                        <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ControlToValidate="txUsuario" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                        <div class="wrap-input100 validate-input m-b-16" data-validate = "Password is required">
                            <asp:TextBox runat="server" ID="txClave" TextMode="Password" autocomplete="off" AutoCompleteType="Disabled" placeholder="Password" required="" class="input100"  name="password"></asp:TextBox>
						    <span class="focus-input100"></span>
						    <span class="symbol-input100">
							    <span class="lnr lnr-lock"></span>
						    </span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClave" runat="server" TargetControlID="txClave" FilterMode="ValidChars" ValidChars="1234567890._-&$#?=/*abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
					    </div>
                        
                        <asp:RequiredFieldValidator ID="rfv_txClave" runat="server" ControlToValidate="txClave" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        
                        <div style="color:#835F34; text-align: center">
                            <asp:Label ID="LblMensajes" runat="server"></asp:Label><br />
                        </div>
                        <div class="container-login100-form-btn p-t-0">
                            <asp:Button ID="LoginButton" runat="server" Text="Aceptar" CausesValidation="true" class="login100-form-btn" OnClick="LoginButton_Click" />
					    </div>

					    <div class="text-center w-full p-t-5 p-b-0">
						    <span class="txt1">
                                    <asp:HyperLink ID="lnkRecuperarClave" Visible="false" runat="server" Text="Recuperar Contraseña" NavigateUrl="https://www.cloud-asae.com.mx/ASAEPasswords/"></asp:HyperLink>
							    <hr>
						    </span>
                        </div>
					    
                    </form>
                <!-- 
				<form class="login100-form validate-form">
                    
					<span class="login100-form-title p-b-30">
						Met Work Tool.<br>Individual Público.
					</span>

					<div class="wrap-input100 validate-input m-b-16" data-validate = "Valid email is required: ex@abc.xyz">
						<input class="input100" type="text" name="email" placeholder="Email">
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<span class="lnr lnr-envelope"></span>
						</span>
					</div>

					<div class="wrap-input100 validate-input m-b-16" data-validate = "Password is required">
						<input class="input100" type="password" name="pass" placeholder="Password">
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<span class="lnr lnr-lock"></span>
						</span>
					</div>
					
					<div class="container-login100-form-btn p-t-25">
						<button class="login100-form-btn">
							Aceptar
						</button>
					</div>

					<div class="text-center w-full p-t-42 p-b-22">
						<span class="txt1">
							<hr>
						</span>
					</div>
				</form>
                -->
			</div>
		</div>
	</div>




    <!--===============================================================================================-->	
    <script src="CSS/login/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="CSS/login/vendor/bootstrap/js/popper.js"></script>
    <script src="CSS/login/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="CSS/login/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="CSS/login/js/main.js"></script>
</body>
</html>
