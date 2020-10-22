<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Panel runat="server" ID="AuthenticatedMessagePanel">
        <asp:Label runat="server" ID="WelcomeBackMessage"></asp:Label><br />
		Your email: <asp:Label runat="server" ID="UserEmail"></asp:Label><br/>
		Your new email: <asp:TextBox ID="NewEmail" runat="server"></asp:TextBox> <br />
		<asp:Button ID = "ChangeEmail" runat = "server" Text = "Change e-mail" OnClick = "ChangeEmailButton_Click" /><br/>
		Click <asp:LinkButton id="LoginLink" Text="here" 
                      OnClick="SignOut" runat="server" /> to sign out
    </asp:Panel>
    
    <asp:Panel runat="Server" ID="AnonymousMessagePanel">
        <asp:HyperLink runat="server" ID="lnkLogin" Text="Log In" NavigateUrl="~/Login.aspx"></asp:HyperLink>
    </asp:Panel>
</asp:Content>
