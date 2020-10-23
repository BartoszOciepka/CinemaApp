<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddReservation.aspx.cs" Inherits="AddReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
			<b>Add new reservation: </b><br />
				<asp:DropDownList ID="TicketList" runat="server" AutoPostBack="True"
				DataTextField="ticket_id" DataValueField="ticket_id" OnSelectedIndexChanged="TicketList_SelectedIndexChanged">
			</asp:DropDownList>
	<br />
	<asp:Button ID="CreateReservationButton" runat="server" Text="Create Reservation" OnClick="CreateReservationButton_Click" />
</asp:Content>

