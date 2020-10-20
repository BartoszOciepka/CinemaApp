using System;
using System.Web;
using System.Web.Security;


public partial class Login : System.Web.UI.Page
{
	protected void SignIn(object sender, EventArgs e)
	{
		if (Membership.ValidateUser(UserName.Text, Password.Text))
		{
			FormsAuthentication.RedirectFromLoginPage(UserName.Text, RememberMe.Checked);
		}

		StatusText.Text = "Invalid username or password";
	}

	protected void SignOut(object sender, EventArgs e)
	{
		var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
		authenticationManager.SignOut();
		Response.Redirect("~/Login.aspx");
	}
}