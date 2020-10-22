<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<div>
		<h1>Log In</h1>
		<hr />
		<asp:Label ID="StatusText" runat="server"></asp:Label>
		<p>
			Username:
		<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
		</p>
		<p>
			Password:
		<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
		</p>
		<p>
			<asp:CheckBox ID="RememberMe" runat="server" Text="Remember Me" />
		</p>
		<p>
			<asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="SignIn" />
		</p>
		<p>
			<asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" Text="Your username or password is invalid. Please try again."
				Visible="False"></asp:Label>
		</p>
	</div>
</asp:Content>
