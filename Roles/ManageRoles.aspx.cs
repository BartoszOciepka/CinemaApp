using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Roles_ManageRoles : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
			DisplayRolesInGrid();
	}
	
	protected void CreateRoleButton_Click(object sender, EventArgs e)
	{
		string newRoleName = RoleName.Text.Trim();

		if (!Roles.RoleExists(newRoleName))
			// Create the role
			Roles.CreateRole(newRoleName);
			
		DisplayRolesInGrid();
		RoleName.Text = string.Empty;
	}

	private void DisplayRolesInGrid()
	{
		RoleList.DataSource = Roles.GetAllRoles();
		RoleList.DataBind();
	}

	protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		// Get the RoleNameLabel
		Label RoleNameLabel = RoleList.Rows[e.RowIndex].FindControl("RoleNameLabel") as Label;

		// Delete the role
		Roles.DeleteRole(RoleNameLabel.Text, false);

		// Rebind the data to the RoleList grid
		DisplayRolesInGrid();

	}
}
