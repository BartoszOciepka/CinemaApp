<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddHall.aspx.cs" Inherits="AddHall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
		<b>Add new hall: </b><br />
	Number: <asp:TextBox ID="HallNumber" runat="server"></asp:TextBox><br />
	Rows: <asp:TextBox ID="RowNumber" runat="server"></asp:TextBox><br />
	Seats: <asp:TextBox ID="SeatsNumber" runat="server"></asp:TextBox><br />
	<br />
	<asp:Button ID="CreateHallButton" runat="server" Text="Create Hall" OnClick="CreateHallButton_Click" />
	<asp:GridView ID="HallList" runat="server" AutoGenerateColumns="False" OnRowDeleting="RowDeleting">
		<Columns>
			<asp:CommandField DeleteText="Delete Hall" ShowDeleteButton="True" />
			<asp:TemplateField HeaderText="number">
				<ItemTemplate>
					<asp:Label runat="server" ID="HallNumberLabel" Text='<%# Eval("number") %>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="seats">
				<ItemTemplate>
					<%# Eval("seats") %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="rows">
				<ItemTemplate>
					<%# Eval("rows") %>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<asp:Label ID="Status" runat="server"></asp:Label>
</asp:Content>

