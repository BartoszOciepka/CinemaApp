using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddShowing : System.Web.UI.Page
{
	MySqlConnection connect;
	MySqlDataReader read;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			BindHallsToHallList();
			BindFilmsToFilmList();
		}
	}

	protected void FilmList_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void HallList_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

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

		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		string useDatabase = string.Format("Use cinema");

		try
		{
			string query = "SELECT * FROM hall WHERE number =" + hallNumber + ";";
			MySqlCommand command = new MySqlCommand(query, connect);
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable ds = new DataTable();
			da.Fill(ds);
			int seats = Int32.Parse(ds.Rows[0]["seats"].ToString());
			int rows = Int32.Parse(ds.Rows[0]["rows"].ToString());
			int hallID = Int32.Parse(ds.Rows[0]["hall_id"].ToString());

			query = "SELECT * FROM film WHERE name =\"" + filmName + "\";";
			command = new MySqlCommand(query, connect);
			da = new MySqlDataAdapter(command);
			ds = new DataTable();
			da.Fill(ds);
			int film_id = Int32.Parse(ds.Rows[0]["film_id"].ToString());

			//VALIDATE
			query = "INSERT INTO showing VALUES (NULL, \"" + date + "\",\"" + time +
				"\"," + hallID + "," + film_id + ");";
			connect.Open();
			command = new MySqlCommand(query, connect);
			command.ExecuteReader();
			connect.Close();

			query = "SELECT * FROM showing WHERE hall_id=" + hallID + " AND film_id=" + film_id + 
				" AND date =\"" + date + "\" AND time = \"" + time + "\";";
			command = new MySqlCommand(query, connect);
			da = new MySqlDataAdapter(command);
			ds = new DataTable();
			da.Fill(ds);
			int showing_id = Int32.Parse(ds.Rows[0]["showing_id"].ToString());

			for (int i = 1; i <= seats; i++)
			{
				for(int j = 1; j <= rows; j++)
				{
					//VALIDATE
					query = "INSERT INTO ticket VALUES (NULL, " + j + "," + i +
					"," + showing_id + ");";
					connect.Open();
					command = new MySqlCommand(query, connect);
					command.ExecuteReader();
					connect.Close();
				}
			}

		}
		catch(Exception ex)
		{

		}
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
		catch (Exception ex)
		{

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
		catch (Exception ex)
		{

		}

	}
}