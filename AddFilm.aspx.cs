using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
	MySqlConnection connect;
	MySqlDataReader read;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
			DisplayFilmsInGrid();
	}

	private void DisplayFilmsInGrid()
	{

		FilmList.DataSource = getAllFilms().DefaultView;
		FilmList.DataMember = "name";
		FilmList.DataBind();
	}

	private DataTable getAllFilms()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		connect.Open();
		string useDatabase = string.Format("Use cinema");
		string query = "SELECT * FROM film;";
		MySqlCommand command = new MySqlCommand(query, connect);
		try
		{
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			DataTable ds = new DataTable();
			da.Fill(ds);
			return ds;
		}
		catch (Exception ex)
		{
			Console.Write(ex);
			return null;
		}
	}
	public void CreateFilmButton_Click(object sender, EventArgs e) {

		string filmName = FilmName.Text;
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		connect.Open();
		string useDatabase = string.Format("Use cinema");
		string selectQuery = "SELECT * FROM film;";
		string insertQuery = "INSERT INTO film VALUES (NULL, \""  + filmName + "\");";
		MySqlCommand command = new MySqlCommand(selectQuery, connect);
		try
		{
			read = command.ExecuteReader();
			//wykonujemy operacje na danych
			if (read.HasRows)
			{
				while (read.Read())
				{
					if (read.GetString(1) == filmName)
					{
						Status.Text = "Film with such name already exists in database";
						return;
					}
				}
			}
			else
			{
				Console.WriteLine("No rows found.");
			}
			read.Close();
		}
		catch (Exception ex)
		{
			Console.Write(ex);
		}

		command = new MySqlCommand(insertQuery, connect);
		try
		{
			command.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			Console.Write(ex);
		}
		DisplayFilmsInGrid();
		connect.Close();

	}
	public void RowDeleting(object sender, GridViewDeleteEventArgs e) {
		string filmName = FilmName.Text;
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		connect.Open();
		string useDatabase = string.Format("Use cinema");
		Label lb = (Label)FilmList.Rows[e.RowIndex].Cells[1].FindControl("FilmNameLabel");
		string deleteQuery = "DELETE FROM film where name= \"" + lb.Text + "\";";
		MySqlCommand command = new MySqlCommand(deleteQuery, connect);
		command.ExecuteNonQuery();
		DisplayFilmsInGrid();


	}

}