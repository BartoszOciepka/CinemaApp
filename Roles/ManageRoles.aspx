<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageRoles.aspx.cs" Inherits="Roles_ManageRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<b>Create a New Role: </b>
	<asp:TextBox ID="RoleName" runat="server"></asp:TextBox>
	<br />
	<asp:Button ID="CreateRoleButton" runat="server" Text="Create Role" OnClick="CreateRoleButton_Click" />
	<asp:GridView ID="RoleList" runat="server" AutoGenerateColumns="False" OnRowDeleting="RowDeleting">
		<Columns>
			<asp:CommandField DeleteText="Delete Role" ShowDeleteButton="True" />
			<asp:TemplateField HeaderText="Role">
				<ItemTemplate>
					<asp:Label runat="server" ID="RoleNameLabel" Text='<%# Container.DataItem %>' />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

</asp:Content>
