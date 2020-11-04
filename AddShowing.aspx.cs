using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

public partial class AddShowing : System.Web.UI.Page
{
	MySqlConnection connect;
	string connectionString = "Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			BindHallsToHallList();
			BindFilmsToFilmList();
		}
	}

	protected void FilmList_SelectedIndexChanged(object sender, EventArgs e){	}
	protected void HallList_SelectedIndexChanged(object sender, EventArgs e){	}
	protected void CreateShowingButton_Click(object sender, EventArgs e)
	{
		//VALIDATE
		string year = txtDatePicker.Text.Substring(6, 4);
		string month = txtDatePicker.Text.Substring(0, 2);
		string day = txtDatePicker.Text.Substring(3, 2);
		string hour = Regex.Match(txtDatePicker.Text, "..(?=:)").ToString().Trim();
		string minute = Regex.Match(txtDatePicker.Text, ":(..)").ToString().Trim().Substring(1,2);
		string ampm = txtDatePicker.Text.Substring(16, 2);
		if (ampm == "PM") hour = (Int32.Parse(hour) + 12).ToString();

		string date = year + "-" + month + "-" + day;
		string time = hour + ":" + minute;

		string hallNumber = HallList.SelectedValue;
		string filmName = FilmList.SelectedValue;

		AddShowingToDatabase(hallNumber, filmName, date, time);
	}
	private void BindHallsToHallList()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string useDatabase = string.Format("Use cinema");
		string query = "SELECT * FROM hall;";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable ds = new DataTable();
			da.Fill(ds);
			HallList.DataSource = ds.DefaultView;
			HallList.DataTextField = "number";
			HallList.DataValueField = "number";
			HallList.DataBind();
		}
		catch (Exception ex){
			Console.Write(ex.Message);
		}
	}
	private void BindFilmsToFilmList()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string useDatabase = string.Format("Use cinema");
		string query = "SELECT * FROM film;";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable ds = new DataTable();
			da.Fill(ds);
			FilmList.DataSource = ds.DefaultView;
			FilmList.DataTextField = "name";
			FilmList.DataValueField = "name";
			FilmList.DataBind();
		}
		catch (Exception ex){
			Console.Write(ex.Message);
		}
	}
	private void AddShowingToDatabase(string hallNumber, string filmName, string date, string time) {
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format(connectionString);

		DataTable dataTable = new DataTable();
		MySqlCommand command = new MySqlCommand();
		try
		{

			int seats = GetSeatsByHallName(command, connect, hallNumber);
			int rows = GetRowsByHallName(command, connect, hallNumber);
			int hallID = GetHallIDByHallName(command, connect, hallNumber);

			int film_id = GetFilmIdByName(command, connect, filmName);

			//VALIDATE
			int showing_id = AddShowingToDatabase(command, connect, hallID.ToString(), film_id.ToString(), date, time);

			for (int i = 1; i <= seats; i++)
			{
				for (int j = 1; j <= rows; j++)
				{
					//VALIDATE
					ValidateTickets();
					GenerateTickets(command, connect, j, i, showing_id);
				}
			}
		}
		catch (Exception ex){
			Console.Write(ex.Message);
		}
	}
	private void GenerateTickets(MySqlCommand command, MySqlConnection connect, int row, int seat, int showing_id)
	{
		string query = "INSERT INTO ticket VALUES (NULL, " + row + "," + seat + "," + showing_id + ");";
		connect.Open();
		command = new MySqlCommand(query, connect);
		command.ExecuteReader();
		connect.Close();
	}
	private void ValidateTickets() { }
	private int AddShowingToDatabase(MySqlCommand command, MySqlConnection connect, string hall_id, string film_id, string date, string time)
	{
		string query = "INSERT INTO showing VALUES (NULL, \"" + date + "\",\"" + time +
				"\"," + hall_id + "," + film_id + ");";
		connect.Open();
		command = new MySqlCommand(query, connect);
		command.ExecuteReader();
		connect.Close();

		query = "SELECT * FROM showing WHERE hall_id=" + hall_id + " AND film_id=" + film_id +
			" AND date =\"" + date + "\" AND time = \"" + time + "\";";
		command = new MySqlCommand(query, connect);
		MySqlDataAdapter da = new MySqlDataAdapter(command);
		DataTable dataTable = new DataTable();
		da.Fill(dataTable);
		return Int32.Parse(dataTable.Rows[0]["showing_id"].ToString());
	}
	private int GetFilmIdByName(MySqlCommand command, MySqlConnection connect, string filmName)
	{
		//VALIDATE
		string query = "SELECT * FROM film WHERE name =\"" + filmName + "\";";
		command = new MySqlCommand(query, connect);
		MySqlDataAdapter da = new MySqlDataAdapter(command);
		DataTable dataTable = new DataTable();
		da.Fill(dataTable);
		return Int32.Parse(dataTable.Rows[0]["film_id"].ToString());
	}
	private int GetSeatsByHallName(MySqlCommand command, MySqlConnection connect, string hallNumber)
	{
		string query = "SELECT * FROM hall WHERE number =" + hallNumber + ";";
		command = new MySqlCommand(query, connect);
		MySqlDataAdapter da = new MySqlDataAdapter(command);
		DataTable dataTable = new DataTable();
		da.Fill(dataTable);
		return Int32.Parse(dataTable.Rows[0]["seats"].ToString());
	}
	private int GetRowsByHallName(MySqlCommand command, MySqlConnection connect, string hallNumber)
	{
		string query = "SELECT * FROM hall WHERE number =" + hallNumber + ";";
		command = new MySqlCommand(query, connect);
		MySqlDataAdapter da = new MySqlDataAdapter(command);
		DataTable dataTable = new DataTable();
		da.Fill(dataTable);
		return Int32.Parse(dataTable.Rows[0]["rows"].ToString());
	}
	private int GetHallIDByHallName(MySqlCommand command, MySqlConnection connect, string hallNumber)
	{
		string query = "SELECT * FROM hall WHERE number =" + hallNumber + ";";
		command = new MySqlCommand(query, connect);
		MySqlDataAdapter da = new MySqlDataAdapter(command);
		DataTable dataTable = new DataTable();
		da.Fill(dataTable);
		return Int32.Parse(dataTable.Rows[0]["hall_id"].ToString());
	}
}