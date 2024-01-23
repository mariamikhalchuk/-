using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace lab6
{
    public class DatabaseHelper : IDisposable
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\development\\labs\\lab6\\Pc.mdb;";
        private OleDbConnection connection;

        public DatabaseHelper()
        {
            connection = new OleDbConnection(ConnectionString);
            connection.Open();
            CreateTableIfNotExists();
        }

        private void CreateTableIfNotExists()
        {
            try
            {
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM Pc", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (OleDbException)
            {
                try
                {
                    using (OleDbCommand createTableCommand = new OleDbCommand("CREATE TABLE Pc (ID AUTOINCREMENT PRIMARY KEY, Type TEXT, Model TEXT, Price FLOAT)", connection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show($"Ubale to create tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public DataTable LoadData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                string selectQuery = "SELECT * FROM Pc";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Unable to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateTable();
            }

            return dataTable;
        }

        public void InsertTicket(string Type, string Model, float Price)
        {
            try
            {
                string insertQuery = "INSERT INTO Pc (Type, Model, Price) VALUES (@Type, @Model, @Price)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Type", Type);
                    command.Parameters.AddWithValue("@Model", Model);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Unable to add data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateTable();
            }
        }

        public void Update(int pcId, string Type, string Model, float Price)
        {
            try
            {
                string updateQuery = "UPDATE Pc SET Type = @Type, Model = @Model, Price = @Price WHERE ID = @PcId";
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Type", Type);
                    command.Parameters.AddWithValue("@Model", Model);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@PclId", pcId);
                    command.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Unable to update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateTable();
            }
        }

        public void Delete(int pcId)
        {
            try
            {
                string deleteQuery = "DELETE FROM Pc WHERE ID = @PcId";
                using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@PcId", pcId);
                    command.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Unable to delete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateTable();
            }
        }

        private void UpdateTable()
        {
            Console.WriteLine("Table has been updated");
        }

        public void Dispose()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
