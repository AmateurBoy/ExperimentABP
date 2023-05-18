using AB_testsABP.Entitys;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace AB_testsABP.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        readonly SqlConnection connectionString;

        public DatabaseManager(SqlConnection connectionString)
        {
            this.connectionString = connectionString;
        }
        //удалит все данные с таблиц Options,Experiment и создаст дефолтные значения 
        public void RecreateDefoltsValueTables()
        {
            List<string> nameExperiment = new List<string>() {
        "button_color",
        "price"
        };
            Dictionary<string, List<string>> GetValueTables = new Dictionary<string, List<string>>()
        {
            {"button_color", new List<string>(){
                "#FF0000",
                "#00FF00",
                "#0000FF" } },
            {"price", new List<string>(){ "10", "20", "50", "5" } }
        };
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string delateQuery = "DELETE FROM [dbo].[Options]";
                string delateQuery1 = "DELETE FROM [dbo].[Experiment]";
                using (SqlCommand command = new SqlCommand(delateQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(delateQuery1, connection))
                {
                    command.ExecuteNonQuery();
                }
                foreach (var nameEx in nameExperiment)
                {
                    int id = 0;
                    //generation Value Experement Table
                    string query = $"Insert INTO Experiment (name) VALUES ('{nameEx}')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    string queryCheckValueOptions = $"SELECT Id,name FROM Experiment WHERE name = '{nameEx}'";
                    using (SqlCommand command = new SqlCommand(queryCheckValueOptions, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                id = (int)reader["Id"];
                            }
                        }
                    }
                    foreach (var nameOpiration in GetValueTables[nameEx])
                    {
                        string qureyCreateValueOptions = $"Insert INTO Options (name,ExperimentId) VALUES ('{nameOpiration}','{id}')";
                        using (SqlCommand command1 = new SqlCommand(qureyCreateValueOptions, connection))
                        {
                            command1.ExecuteNonQuery();
                        }
                    }


                }
            }
        }

        #region Create
        public int CreateUser(User user)
        {
            int newUserId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (deviceToken) VALUES (@Name);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    newUserId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return newUserId;

        }
        public int CreateOption(Option option)
        {
            int newUserId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {

                connection.Open();
                string query = "INSERT INTO YourTableName (name,ExperimentId) VALUES (@Name,@ExpId); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", option.Name);
                    command.Parameters.AddWithValue("@Name", option.Experiment.Id);
                    newUserId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return newUserId;

        }
        #endregion
        #region Read
        public Experiment GetExperiment(int id)
        {
            Experiment experiment = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Experiment WHERE Id = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    experiment.Name = reader["name"].ToString();
                    experiment.Id = Convert.ToInt32(reader["Id"]);
                }
            };
            return experiment;
        }
        public Experiment GetExperiment(string name)
        {
            Experiment experiment = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Experiment WHERE name = @name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    experiment.Name = reader["name"].ToString();
                    experiment.Id = Convert.ToInt32(reader["Id"]);
                }
            };
            return experiment;
        }
        public User GetUser(int id)
        {
            User user = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Users WHERE Id = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user.Name = reader["name"].ToString();
                    user.Id = Convert.ToInt32(reader["Id"]);
                }
            };
            return user;
        }
        public User GetUser(string name)
        {
            User user = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Users WHERE name = @name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user.Name = reader["name"].ToString();
                    user.Id = Convert.ToInt32(reader["Id"]);
                }
            };
            return user;
        }
        public Option GetOption(int id)
        {
            Option option = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Options WHERE Id = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var exp = new Experiment();
                    exp.Id = Convert.ToInt32(reader["Id"]);
                    option.Name = reader["name"].ToString();
                    option.Id = Convert.ToInt32(reader["Id"]);
                    option.Experiment = exp;
                }
            };
            return option;
        }
        public Option GetOption(string name)
        {
            Option option = new();
            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            string query = "SELECT * FROM Options WHERE name = @name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var exp = new Experiment();
                    exp.Id = Convert.ToInt32(reader["Id"]);
                    option.Name = reader["name"].ToString();
                    option.Id = Convert.ToInt32(reader["Id"]);
                    option.Experiment = exp;
                }
            };
            return option;
        }
        #endregion
        #region Update
        //Update
        #endregion
        #region Delete 
        //Delete
        public string DelateUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                string query = "DELETE FROM Users WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return $"Юзер с id: {id} удален";
                }
            }
        }
        #endregion
    }
}
