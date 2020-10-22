using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddReservation : System.Web.UI.Page
{
	MySqlConnection connect;
	MySqlDataReader read;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			BindTicketToTicketList();
		}
	}
	protected void TicketList_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void CreateReservationButton_Click(object sender, EventArgs e)
	{
		string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
		string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
		string ticket_id = TicketList.SelectedValue;
		string client_id = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();

		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string useDatabase = string.Format("Use cinema");
		string query = "INSERT INTO reservation VALUES (NULL, " + ticket_id + ", \"" + date + "\",\"" + time + "\", " + client_id + ");";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			connect.Open();
			command.ExecuteReader();
			connect.Close();
		}
		catch (Exception ex) { }

	}

	private void BindTicketToTicketList()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string useDatabase = string.Format("Use cinema");
		string query = "SELECT * FROM ticket;";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable ds = new DataTable();
			da.Fill(ds);
			TicketList.DataSource = ds.DefaultView;
			TicketList.DataTextField = "ticket_id";
			TicketList.DataValueField = "ticket_id";
			TicketList.DataBind();
		}
		catch (Exception ex)
		{

		}

	}

	private void ValidateReservations()
	{

	}
}