<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UsersAndRoles.aspx.cs" Inherits="Roles_UsersAndRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<div>
		<h3>Manage Roles By User</h3>
		<p>
			<b>Select a User:</b>
			<asp:DropDownList ID="UserList" runat="server" AutoPostBack="True"
				DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged">
			</asp:DropDownList>
		</p>
		<p>
			<asp:Label runat="server" ID="ActionStatus"></asp:Label><br />
			<asp:Repeater ID="UsersRoleList" runat="server">
				<ItemTemplate>
					<asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true"
						Text='<%# Container.DataItem %>'
						OnCheckedChanged="RoleCheckBox_CheckChanged" 
						Checked="false"/>
					<br />
				</ItemTemplate>
			</asp:Repeater>
		</p>
	</div>
</asp:Content>
