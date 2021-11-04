using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace InventorySystem
{
    class DataBaseTranslator
    {
        //Private variables to be used with getters and setters.
        private DataTable table;
        private const int SoftDelete = 0;

        private string DataBaseString()
            //Builds a string for connection to the database being hosted in the cloud on a Azure server.
        {
            //String builder for the database connection.
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "inventory-management.database.windows.net";
            builder.UserID = "adm";
            builder.Password = "vxa72mYFLFhNMBQ";
            builder.InitialCatalog = "reselling-inventory";
            //Returns the string for connection to the database.
            return builder.ConnectionString;
        }

        public void ReadData()
            //Makes a connection to the database for reading the inventory and preparing to display in gui.
        {
            //Connection to the database.
            SqlConnection connection = new SqlConnection(DataBaseString());
            //Open connection.
            connection.Open();
            //Sql query.
            SqlCommand cmd = new SqlCommand("Select * From Instock", connection);
            //Preparing for the dataset.
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Creation of a dataset 
            DataSet ds = new DataSet();
            //Fills dataset with data from the table.
            da.Fill(ds);
            //Connection closes;
            connection.Close();
            //Sets table equal to the data in the data table.
            table = ds.Tables[0];
        }

        public void DeleteData(string _name)
            //This will take the selected rows name and preform a soft delete on that row in the database.
        {
            //Connection.
            SqlConnection connection = new SqlConnection(DataBaseString());
            connection.Open();
            //Sql query that will change the sold column to a 1 indicating it has been sold and will soft delete from the table.
            SqlCommand cmd = new SqlCommand("UPDATE Inventory SET Sold = @num " + 
                                            "WHERE Name = @name", connection);
            //Connecting sql variables.
            cmd.Parameters.AddWithValue("@name", _name);
            cmd.Parameters.AddWithValue("@num", SoftDelete);
            //Query execution.
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateData(string _name, string _company, string _category, double _price, DateTime _date)
            //Makes updates to the database to update existing inventory.
        {
            //Database connection.
            SqlConnection connection = new SqlConnection(DataBaseString());
            connection.Open();
            //Query to update a row in the database based off of the users selection and changes.
            SqlCommand cmd = new SqlCommand("UPDATE Inventory " +
                                            "SET Company = @company, " +
                                                "Category = @category, " +
                                                "Price = @price, " +
                                                "DatePurchased = @date " +
                                            "WHERE Name = @name ", connection);
            //Binding variables with query.
            cmd.Parameters.AddWithValue("@company", _company);
            cmd.Parameters.AddWithValue("@category", _category);
            cmd.Parameters.AddWithValue("@price", _price);
            cmd.Parameters.AddWithValue("@date", _date);
            cmd.Parameters.AddWithValue("@name", _name);
            //Query execution.
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertData(string _name, string _company, string _category, double _price, DateTime _date)
            //Makes inserts into the database to create new inventory.
        {
            SqlConnection connection = new SqlConnection(DataBaseString());
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Inventory (Name, Company, Category, Price, DatePurchased) " +
                                            "VALUES (@name, @company, @category, @price, @date)", connection);
            cmd.Parameters.AddWithValue("@name", _name);
            cmd.Parameters.AddWithValue("@company", _company);
            cmd.Parameters.AddWithValue("@category", _category);
            cmd.Parameters.AddWithValue("@price", _price);
            cmd.Parameters.AddWithValue("@date", _date);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

//------------------------------------GETTERS-------------------------------------------
        public DataTable GetTable()
        {
            return table;
        }
    }
}
