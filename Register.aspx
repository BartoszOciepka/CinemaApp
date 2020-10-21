<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<div>
		<h4 style="font-size: medium">Register a new user</h4>
		<hr />
		<p>
			<asp:Literal runat="server" ID="StatusMessage" />
		</p>
		<div style="margin-bottom: 10px">
			<asp:Label runat="server" AssociatedControlID="Username">User name</asp:Label>
			<div>
				<asp:TextBox runat="server" ID="Username" />
			</div>
		</div>
		<div style="margin-bottom: 10px">
			<asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
			<div>
				<asp:TextBox runat="server" ID="Password" TextMode="Password" />
			</div>
		</div>
		<div style="margin-bottom: 10px">
			<asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
			<div>
				<asp:TextBox runat="server" ID="Email" />
			</div>
		</div>
		<div style="margin-bottom: 10px">
			<asp:Label runat="server" AssociatedControlID="SecurityQuestion" ID="SecurityQuestion"></asp:Label>
			<div>
				<asp:TextBox runat="server" ID="SecurityAnswer" />
			</div>
		</div>
		<div>
			<div>
				<asp:Button runat="server" OnClick="CreateAccountButton_Click" Text="Register" />
			</div>
		</div>
	</div>
</asp:Content>
