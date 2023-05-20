using ExperimentABP.Entitys;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace ExperimentABP.Data
{
    public class DatabaseManager : IDatabaseManager,IDefaultCreator
    {
        readonly SqlConnection _conector;
        //Реализовать запросы через процедуры
        public DatabaseManager(SqlConnection connectionString)
        {
            this._conector = connectionString;
        }


        #region Create
        public Device CreateDevice(string deviceToken)
        {
            Device divece = new Device();

            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {                
                connection.Open();
                string query = "INSERT INTO Devices ([device-token]) VALUES (@Token);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", deviceToken);
                    command.ExecuteNonQuery();
                }
                divece = GetDevice(deviceToken);

            }
            return divece;

        }
        public DeviceOption CreateUserOption(Device device, Option option)
        {
            DeviceOption diveceOptions = null;
            string query = "";
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                connection.Open();
                query = "INSERT INTO DeviceOptions (DeviceId,OptionId) VALUES (@devID,@optionID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@devID", device.Id);
                    command.Parameters.AddWithValue("@optionID", option.Id);
                    command.ExecuteScalar();
                }
                query = "SELECT Id FROM DeviceOptions WHERE DeviceId = @devID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@devID", device.Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            diveceOptions = GetDeviceOption(Convert.ToInt32(reader["Id"]));
                        }
                    }
                }
            }

            return diveceOptions;
        }
        #endregion
        #region Read
        public Experiment GetExperiment(int id)
        {
            Experiment experiment = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Experiments WHERE Id = @ID";

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
            };

            return experiment;
        }
        public Experiment GetExperiment(string name)
        {
            Experiment experiment = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Experiments WHERE name = @name";

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
            };

            return experiment;
        }
        public Device GetDevice(int id)
        {
            Device user = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Devices WHERE Id = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Name = reader["device-token"].ToString();
                        user.Id = Convert.ToInt32(reader["Id"]);
                    }
                };
            };
            return user;
        }
        public Device GetDevice(string name)
        {
            Device user = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Devices WHERE [device-token] = @name";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Name = reader["device-token"].ToString();
                        user.Id = Convert.ToInt32(reader["Id"]);
                    }
                };
            };
            return user;
        }
        public Option GetOption(int idOption)
        {
            Option option = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Options WHERE Id = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", idOption);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        option.Name = reader["name"].ToString();
                        option.Id = Convert.ToInt32(reader["Id"]);
                        var exp = GetExperiment(Convert.ToInt32(reader["ExperimentId"]));
                        option.Experiment = exp;
                    }
                };
            };
            return option;
        }
        public Option GetOption(string nameOption)
        {
            Option option = new();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Options WHERE name = @name";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", nameOption);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        option.Name = reader["name"].ToString();
                        option.Id = Convert.ToInt32(reader["Id"]);
                        var exp = GetExperiment(Convert.ToInt32(reader["ExperimentId"]));
                        option.Experiment = exp;
                    }
                };
            };
            return option;
        }
        public List<Option> GetOptions(int ExperimentId)
        {
            List<Option> options = new();
            var experiment = GetExperiment(ExperimentId);
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT * FROM Options WHERE ExperimentId = @ExperimentId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ExperimentId", ExperimentId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        var option = new Option();
                        option.Name = reader["name"].ToString();
                        option.Id = Convert.ToInt32(reader["Id"]);
                        option.Experiment = experiment;
                        options.Add(option);
                    }
                };
            };
            return options;
        }
        public DeviceOption GetDeviceOption(int DeviceOptionId)
        {
            var divOpt = new DeviceOption();
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT " +

                    "Options.name as optionName,Options.Id as optionId," +
                    "Experiments.Id as experimentId, Experiments.name as experimentName, " +
                    "Devices.[device-token], Devices.Id as deviceId," +
                    "DeviceOptions.Id as DevOptId " +

                    "From DeviceOptions " +

                    "Join Options On (OptionId = Options.Id) " +
                    "Join Devices On (DeviceId = Devices.Id) " +
                    "Join Experiments On(Options.ExperimentId = Experiments.Id) " +
                    "WHERE (Devices.Id = @id)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", DeviceOptionId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        divOpt.Option = new Option();
                        divOpt.Device = new Device();
                        divOpt.Option.Experiment = new Experiment();

                        divOpt.Id = Convert.ToInt32(reader["DevOptId"]);
                        divOpt.Device.Id = Convert.ToInt32(reader["deviceId"]);
                        divOpt.Device.Name = reader["device-token"].ToString();
                        divOpt.Option.Id = Convert.ToInt32(reader["optionId"]);
                        divOpt.Option.Name = reader["optionName"].ToString();
                        divOpt.Option.Experiment.Id = Convert.ToInt32(reader["experimentId"]);
                        divOpt.Option.Experiment.Name = reader["experimentName"].ToString();                        
                    }
                };
            };
            return divOpt;
        }
        public List<DeviceOption> GetDeviceOptions(int DiveceId)
        {
            var DiveceOptions = new List<DeviceOption>();


            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                string query = "SELECT " +
                    
                    "Options.name as optionName,Options.Id as optionId," +
                    "Experiments.Id as experimentId, Experiments.name as experimentName, " +
                    "Devices.[device-token], Devices.Id as deviceId," +
                    "DeviceOptions.Id as DevOptId " +

                    "From DeviceOptions " +

                    "Join Options On (OptionId = Options.Id) " +
                    "Join Devices On (DeviceId = Devices.Id) " +
                    "Join Experiments On(Options.ExperimentId = Experiments.Id) " +
                    "WHERE (Devices.Id = @id)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", DiveceId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var divOpt = new DeviceOption();
                        divOpt.Option = new Option();
                        divOpt.Device = new Device();
                        divOpt.Option.Experiment = new Experiment();

                        divOpt.Id = Convert.ToInt32(reader["DevOptId"]);
                        divOpt.Device.Id = Convert.ToInt32(reader["deviceId"]);
                        divOpt.Device.Name = reader["device-token"].ToString();
                        divOpt.Option.Id = Convert.ToInt32(reader["optionId"]);
                        divOpt.Option.Name = reader["optionName"].ToString();
                        divOpt.Option.Experiment.Id = Convert.ToInt32(reader["experimentId"]);
                        divOpt.Option.Experiment.Name = reader["experimentName"].ToString();
                        DiveceOptions.Add(divOpt);
                    }
                };
            };
            return DiveceOptions;
        }
        public List<DeviceOption> GetAllDeviceOptions()
        {
            List<DeviceOption> DiveceOptions = new List<DeviceOption>();

            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                connection.Open();

                //Dead X_X
                string query = "SELECT " +

                    "Options.name as optionName,Options.Id as optionId," +
                    "Experiments.Id as experimentId, Experiments.name as experimentName, " +
                    "Devices.[device-token], Devices.Id as deviceId," +
                    "DeviceOptions.Id as DevOptId " +

                    "From DeviceOptions " +

                    "Join Options On (OptionId = Options.Id) " +
                    "Join Devices On (DeviceId = Devices.Id) " +
                    "Join Experiments On(Options.ExperimentId = Experiments.Id)";

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var divOpt = new DeviceOption();
                        divOpt.Option = new Option();
                        divOpt.Device = new Device();

                        divOpt.Id = Convert.ToInt32(reader["DevOptId"]);
                        divOpt.Device.Id = Convert.ToInt32(reader["deviceId"]);
                        divOpt.Device.Name = reader["device-token"].ToString();
                        divOpt.Option.Id = Convert.ToInt32(reader["optionId"]);
                        divOpt.Option.Name = reader["optionName"].ToString();

                        DiveceOptions.Add(divOpt);
                    }
                };
            };

            return DiveceOptions;
        }
        #endregion
        #region Update

        #endregion
        #region Delete         
        public string DelateUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
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
        #region Optional
        public void СreateDefoltsValueTables()
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
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                connection.Open();

                foreach (var nameEx in nameExperiment)
                {
                    int id = 0;
                    //generation Value Experement Table
                    string query = $"Insert INTO Experiments (name) VALUES ('{nameEx}')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    string queryCheckValueOptions = $"SELECT Id,name FROM Experiments WHERE name = '{nameEx}'";
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
        public void DefaultCreateTables()
        {
            #region Command
            const string queryCreateTableDiveces =
                "CREATE TABLE [dbo].[Devices](" +
                "[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY," +
                "[device-token] VARCHAR(20))";
            const string queryCreateTableOptions =
                 "CREATE TABLE [dbo].[Options](" +
                 "[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY," +
                 "[name] VARCHAR(20)," +
                 "[ExperimentId] INT)";
            const string queryCreateTableExperiment =
                 "CREATE TABLE [dbo].[Experiments](" +
                 "[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY," +
                 "[name] VARCHAR(20))";
            const string queryCreateTableDiveceOptions =
                 "CREATE TABLE [dbo].[DeviceOptions](" +
                 "[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY," +
                 "[DeviceId] INT," +
                 "[OptionId] INT)";
            #endregion
            string[] listCommand = new string[]
            {
                queryCreateTableDiveces,
                queryCreateTableOptions,
                queryCreateTableExperiment,
                queryCreateTableDiveceOptions
            };

            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                connection.Open();
                void QuerySQL(string command)
                {
                    using (SqlCommand commandSQL = new SqlCommand(command, connection))
                    {
                        commandSQL.ExecuteNonQuery();
                    }
                }
                foreach (var command in listCommand)
                {
                    QuerySQL(command);
                }
            }
        }
        public void RemoverToTablets()
        {
            using (SqlConnection connection = new SqlConnection(_conector.ConnectionString))
            {
                connection.Open();
                string delateQuery = "DROP TABLE [dbo].[Options]";
                string delateQuery1 = "DROP TABLE [dbo].[Experiments]";
                string delateQuery2 = "DROP TABLE [dbo].[Devices]";
                string delateQuery3 = "DROP TABLE [dbo].[DeviceOptions]";
                using (SqlCommand command = new SqlCommand(delateQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(delateQuery1, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(delateQuery2, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(delateQuery3, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
