using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.Security;
using System.Web.UI;

public partial class AddReservation : System.Web.UI.Page
{
	MySqlConnection connect;
	MySqlCommand command = new MySqlCommand();
	string connectionString = "Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;";
	int reservationMinutes = 30;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			CheckReservationTimes(command);
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
		string[] ticketList = TicketList.SelectedValue.Split(' ');
		string ticket_id = ticketList[0];
		string client_id = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
		
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format(connectionString);

		CheckReservationTimes(command );
		ValidateReservation(ticket_id, client_id);
		AddReservationToDatabase(command, connect, ticket_id, client_id, date, time);
		MessageBox.Text = "Added reservation";

	}

	private void BindTicketToTicketList()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string query = "SELECT * FROM ticket WHERE ticket_id NOT IN(SELECT ticket_id FROM reservation);";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			connect.Open();
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					MySqlConnection filmConnect = new MySqlConnection();
					MySqlConnection dateConnect = new MySqlConnection();
					try
					{
						
						filmConnect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
						filmConnect.Open();
						string filmNameQuery = "SELECT * FROM film WHERE film_id=(SELECT film_id FROM showing WHERE showing_id=" + reader.GetString(3) + ");";
						command = new MySqlCommand(filmNameQuery, filmConnect);
						MySqlDataReader filmReader = command.ExecuteReader();
						filmReader.Read();
						
						dateConnect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
						dateConnect.Open();
						string dateQuery = "SELECT * FROM showing WHERE showing_id=" + reader.GetString(3) + ";";
						command = new MySqlCommand(dateQuery, dateConnect);
						MySqlDataReader dateReader = command.ExecuteReader();
						filmReader.Read();
						dateReader.Read();
						TicketList.Items.Add(reader.GetString(0) + " " + filmReader.GetString(1) + " " + dateReader.GetString(1).Substring(0, 8) + " " + dateReader.GetString(2).Substring(0, 5) + " rzad:" + reader.GetString(1) + " miejsce:" + reader.GetString(2));
					}
					finally{
						dateConnect.Close();
						filmConnect.Close();
					}
				}
			}
			connect.Close();
			//MySqlDataAdapter da = new MySqlDataAdapter(command);
			//DataTable ds = new DataTable();
			//da.Fill(ds);
			//TicketList.Items.Add("tesT");
			//TicketList.DataSource = ds.DefaultView;
			//TicketList.DataTextField = "ticket_id";
			//TicketList.DataValueField = "ticket_id";
			//TicketList.DataBind();
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message);
		}
	}

	private void ValidateReservation(string ticket_id, string client_id)
	{

	}
	private void AddReservationToDatabase(MySqlCommand command, MySqlConnection connect, string ticket_id, string client_id, string date, string time)
	{
		string query = "INSERT INTO reservation VALUES (NULL, " + ticket_id + ", \"" + date + "\",\"" + time + "\", " + client_id + ");";
		command = new MySqlCommand(query, connect);
		try
		{
			connect.Open();
			command.ExecuteReader();
			connect.Close();
		}
		catch (Exception ex) {
			Console.Write(ex.Message);
		}
	}
	private void CheckReservationTimes(MySqlCommand command)
	{

		MySqlConnection connect = new MySqlConnection();
		connect.ConnectionString = string.Format(connectionString);
		string query = "SELECT * FROM reservation";
		DateTime now = DateTime.Now;
		try
		{
			command = new MySqlCommand(query, connect);
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			foreach (DataRow row in dataTable.Rows)
			{
				string date = row["date"].ToString();
				string time = row["time"].ToString();

				int year = Convert.ToDateTime(row["date"]).Year;
				int month = Convert.ToDateTime(row["date"]).Month;
				int day = Convert.ToDateTime(row["date"]).Day;
				int hour = Int32.Parse(time.Substring(0, 2));
				int minute = Int32.Parse(time.Substring(3, 2));

				DateTime reservationDateTime = new DateTime(year, month, day, hour, minute, 0);
				int reservationLength = Convert.ToInt32(Math.Floor((now - reservationDateTime).TotalMinutes));
				if (reservationLength > reservationMinutes) DeleteReservation(command, connect, row["reservation_id"].ToString());

			}
		}
		catch(Exception ex) {
			Console.Write(ex.Message);
		}
	}
	private void DeleteReservation(MySqlCommand command, MySqlConnection connect, string reservation_id)
	{
		try
		{
			string query = "DELETE FROM reservation WHERE reservation_id=" + reservation_id + ";";
			connect.Open();
			command = new MySqlCommand(query, connect);
			command.ExecuteNonQuery();
			connect.Close();
		}
		catch(Exception ex) {
			Console.Write(ex.Message);
		}
	}
}