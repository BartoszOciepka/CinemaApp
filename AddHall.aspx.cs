using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddHall : System.Web.UI.Page
{
	MySqlConnection connect;
	MySqlDataReader read;
	string connectionString = "Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
			DisplayHallsInGrid();
	}

	private void DisplayHallsInGrid()
	{
		HallList.DataSource = getAllHalls().DefaultView;
		HallList.DataMember = "name";
		HallList.DataBind();
	}

	private DataTable getAllHalls()
	{
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format(connectionString);
		connect.Open();
		string query = "SELECT * FROM hall;";
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

	public void CreateHallButton_Click(object sender, EventArgs e)
	{
		//VALIDATE
		string hallNumber = HallNumber.Text;

		string rowNumber = RowNumber.Text;
		string seatsNumber = SeatsNumber.Text;
		try
		{
			if(Int32.Parse(hallNumber)<=0)
			{
				Status.Text = "Hall number can't be lower or equal 0";
				return;
			}
			if (Int32.Parse(rowNumber) <= 0)
			{
				Status.Text = "Row number can't be lower or equal 0";
				return;
			}
			if(Int32.Parse(seatsNumber) <=0)
			{
				Status.Text = "Seat number can't be lower or equal 0";
				return;
			}
		}
		catch(Exception ex)
		{
			Status.Text = ex.Message;
		}
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		connect.Open();
		string useDatabase = string.Format("Use cinema");
		string selectQuery = "SELECT * FROM hall;";
		string insertQuery = "INSERT INTO hall VALUES (NULL, \"" + hallNumber + "\", " +rowNumber +" , "+seatsNumber+");";
		MySqlCommand command = new MySqlCommand(selectQuery, connect);
		try
		{
			read = command.ExecuteReader();
			if (read.HasRows)
			{
				while (read.Read())
				{
					if (read.GetString(1) == hallNumber)
					{
						Status.Text = "Hall with such number already exists in database";
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
		DisplayHallsInGrid();
		connect.Close();

	}
	public void RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		string hallNumber = HallNumber.Text;
		connect = new MySqlConnection();
		connect.ConnectionString = string.Format("Server=localhost;Port=3306;Database=cinema;Uid=root;Pwd=root;");
		connect.Open();
		string useDatabase = string.Format("Use cinema");
		Label lb = (Label)HallList.Rows[e.RowIndex].Cells[1].FindControl("HallNumberLabel");
		string deleteQuery = "DELETE FROM hall where number= \"" + lb.Text + "\";";
		MySqlCommand command = new MySqlCommand(deleteQuery, connect);
		command.ExecuteNonQuery();
		DisplayHallsInGrid();
	}
}