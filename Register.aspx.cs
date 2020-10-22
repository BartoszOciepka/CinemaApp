using System;
using System.Web.Security;

public partial class Register : System.Web.UI.Page
{
	const string passwordQuestion = "What is your favorite color";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
			SecurityQuestion.Text = passwordQuestion;
	}

	protected void CreateAccountButton_Click(object sender, EventArgs e)
	{
		MembershipCreateStatus createStatus;
		if (!IsValidEmail(Email.Text))
		{
			StatusMessage.Text = "There email address you provided in invalid.";
			return;
		}
		MembershipUser newUser = Membership.CreateUser(Username.Text, Password.Text, Email.Text, passwordQuestion, SecurityAnswer.Text, true, out createStatus);
		switch (createStatus)
		{
			case MembershipCreateStatus.Success:
				StatusMessage.Text = "The user account was successfully created!";
				FormsAuthentication.RedirectFromLoginPage(Username.Text, false);
				Response.Redirect("Default.aspx");
				break;
			case MembershipCreateStatus.DuplicateUserName:
				StatusMessage.Text = "There already exists a user with this username.";
				break;

			case MembershipCreateStatus.DuplicateEmail:
				StatusMessage.Text = "There already exists a user with this email address.";
				break;
			case MembershipCreateStatus.InvalidEmail:
				StatusMessage.Text = "There email address you provided in invalid.";
				break;
			case MembershipCreateStatus.InvalidAnswer:
				StatusMessage.Text = "There security answer was invalid.";
				break;
			case MembershipCreateStatus.InvalidPassword:
				StatusMessage.Text = "The password you provided is invalid. It must be seven characters long and have at least one non-alphanumeric character.";

				break;
			default:
				StatusMessage.Text = "There was an unknown error; the user account was NOT created.";
				break;
		}
	}

	bool IsValidEmail(string email)
	{
		try
		{
			var addr = new System.Net.Mail.MailAddress(email);
			return addr.Address == email;
		}
		catch
		{
			return false;
		}
	}
}