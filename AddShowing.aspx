<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddShowing.aspx.cs" Inherits="AddShowing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
				<asp:DropDownList ID="FilmList" runat="server" AutoPostBack="True"
				DataTextField="name" DataValueField="name" OnSelectedIndexChanged="FilmList_SelectedIndexChanged">
			</asp:DropDownList>
					<asp:DropDownList ID="HallList" runat="server" AutoPostBack="True"
				DataTextField="number" DataValueField="number" OnSelectedIndexChanged="HallList_SelectedIndexChanged">
			</asp:DropDownList>
			<asp:TextBox ID="txtDatePicker" runat="server" CssClass="txtDatePicker"/>
			<asp:Button ID="CreateShowingButton" runat="server" Text="Create Showing" OnClick="CreateShowingButton_Click" />
</asp:Content>


