using System;
using System.Web.Security;

public partial class _default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.IsAuthenticated)
		{

			WelcomeBackMessage.Text = "Welcome back " + User.Identity.Name;
			UserEmail.Text = Membership.GetUser(User.Identity.Name).Email;


			AuthenticatedMessagePanel.Visible = true;
			AnonymousMessagePanel.Visible = false;
		}
		else
		{
			AuthenticatedMessagePanel.Visible = false;
			AnonymousMessagePanel.Visible = true;
		}
	}

	protected void SignOut(object sender, EventArgs e)
	{
		FormsAuthentication.SignOut();
		Session.Abandon();


		Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
		FormsAuthentication.RedirectToLoginPage();
	}

	protected void ChangeEmailButton_Click(object sender, EventArgs e)
	{
		//VALIDATE
		string newEmail = NewEmail.Text;
		MembershipUser u = Membership.GetUser(User.Identity.Name);
		u.Email = newEmail;
		Membership.UpdateUser(u);
		Response.Redirect(Request.RawUrl);
	}
}