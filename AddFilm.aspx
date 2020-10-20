<%@ Page Title="AddFilm" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddFilm.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
		<b>Add new film: </b>
	<asp:TextBox ID="FilmName" runat="server"></asp:TextBox>
	<br />
	<asp:Button ID="CreateFilmButton" runat="server" Text="Create Film" OnClick="CreateFilmButton_Click" />
	<asp:GridView ID="FilmList" runat="server" AutoGenerateColumns="False" OnRowDeleting="RowDeleting">
		<Columns>
			<asp:CommandField DeleteText="Delete Film" ShowDeleteButton="True" />
			<asp:TemplateField HeaderText="Film">
				<ItemTemplate>
					<asp:Label runat="server" ID="FilmNameLabel" Text='<%# Eval("name") %>' />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<asp:Label ID="Status" runat="server"></asp:Label>
</asp:Content>

