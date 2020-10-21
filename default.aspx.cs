using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.IsAuthenticated)
		{
			WelcomeBackMessage.Text = "Welcome back " + User.Identity.Name;

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
}