
namespace ADO.NET_Ex
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        // witch server, witch database , you can bind with my computer with my data
        // first use master database to create empty database then use newDatabase
        // first create tables, then insert data to them and comment previos command to not create errors

        const string SqlConnectionString = "Server=.;Database=MinionsDB;Integrated Security = true";
        public static void Main()
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
            }


        }

        private static void IncreaseAgeWithStoredProdcedure(SqlConnection connection)
        {
            var id = int.Parse(Console.ReadLine());
            string query = @"EXEC usp_GetOlder @id";
           using var  command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            string selectQuery = @"SELECT [Name], Age FROM Minions WHERE Id = @Id";
          using var  selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@id", id);
         using var   reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]} - {reader[1]} years old");
            }
        }

        private static void IncreaseMinionAge(SqlConnection connection)
        {
            var minionsIds = Console.ReadLine().
                 Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            string updateMinionsQuery = @"UPDATE Minions 
                    SET [Name] = UPPER(LEFT([Name],1)) + SUBSTRING([Name], 2,LEN([Name])), Age += 1
                     WHERE Id  = @Id";


            foreach (var id in minionsIds)
            {
                using var updateMinionsCommand = new SqlCommand(updateMinionsQuery, connection);
                updateMinionsCommand.Parameters.AddWithValue("@Id", id);
                updateMinionsCommand.ExecuteNonQuery();
            }

            var selectMinions = @"SELECT [Name], Age FROM Minions";
            using var sqlCommand = new SqlCommand(selectMinions, connection);
            using var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]} {reader[1]}");
            }
        }

        private static SqlCommand PrintAllMinionNames(SqlConnection connection)
        {
            var minionsQuery = @"SELECT [Name] FROM Minions";
            var selectMinionsCommand = new SqlCommand(minionsQuery, connection);
            using (var reader = selectMinionsCommand.ExecuteReader())
            {
                var minions = new List<string>();
                while (reader.Read())
                {
                    minions.Add((string)reader[0]);
                }
                var counter = 0;


                for (int i = 0; i < minions.Count / 2; i++)
                {
                    Console.WriteLine(minions[0 + counter]);
                    Console.WriteLine(minions[minions.Count - 1 - counter]);
                    counter++;
                }
                if (minions.Count % 2 != 0)
                {
                    Console.WriteLine(minions[minions.Count / 2]);
                }

            }

            return selectMinionsCommand;
        }

        private static void RemoveVillainAndHisMinions(SqlConnection connection)
        {
            string evalNameQuery = @"SELECT [Name] FROM Villains WHERE Id = @villainId";
            var value = int.Parse(Console.ReadLine());
            using var sqlCommand = new SqlCommand(evalNameQuery, connection);
            sqlCommand.Parameters.AddWithValue("@villainId", value);

            var name = (string)sqlCommand.ExecuteScalar();

            if (name == null)
            {
                Console.WriteLine("No such villain was found.");
                return;
            }

            var deleteMinionsVillainsQuery = @"DELETE FROM MinionsVillains WHERE VillainId = @villainId";

            using var sqlDeleteMVCommand = new SqlCommand(deleteMinionsVillainsQuery, connection);
            sqlDeleteMVCommand.Parameters.AddWithValue("@villainId", value);
            var affectedRows = sqlDeleteMVCommand.ExecuteNonQuery();

            var deleteVillainsQuery = @"DELETE FROM Villains WHERE Id = @villainId";

            using var sqlDeleteVCommand = new SqlCommand(deleteVillainsQuery, connection);
            sqlDeleteVCommand.Parameters.AddWithValue("@villainId", value);
            sqlDeleteVCommand.ExecuteNonQuery();

            Console.WriteLine($"{name} was deleted.");
            Console.WriteLine($"{affectedRows} minion were released.");
        }
        private static void ChangeTownNamesCasing(SqlConnection connection)
        {
            string updateTownNamesQuery = @"UPDATE Towns
                         SET [Name] = UPPER([Name])
                          WHERE CountryCode = 
                         ( SELECT c.Id FROM Countries AS c WHERE c.[Name] = @countryName)";

            var countryName = Console.ReadLine();

            string selectTownNamesQuery = @"SELECT t.[Name] FROM Towns AS t
	                 JOIN Countries AS c ON c.Id = t.CountryCode
	                   WHERE c.[Name] = @countryName";

            using (var updateCommand = new SqlCommand(updateTownNamesQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@countryName", countryName);
                var affectedRows = updateCommand.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{affectedRows} town names were affected.");
                    using var selectCommand = new SqlCommand(selectTownNamesQuery, connection);
                    selectCommand.Parameters.AddWithValue("@countryName", countryName);
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        var towns = new List<string>();
                        while (reader.Read())
                        {
                            towns.Add((string)reader[0]);
                        }
                        Console.WriteLine($"[{string.Join(", ", towns)}]");
                    }
                }
            }
        }

        private static void createMinionTownVillain(SqlConnection connection)
        {
            string[] minionInfo = Console.ReadLine().Split(' ');
            string[] villainInfo = Console.ReadLine().Split(' ');
            var minionName = minionInfo[1];
            var age = int.Parse(minionInfo[2]);
            var town = minionInfo[3];

            var villainName = villainInfo[1];
            // nullable  have null values
            int? townId = GetTownId(connection, town);

            if (townId == null)
            {
                string createTownQuery = "INSERT INTO Towns(Id,[Name]) VALUES (12,@name)";
                using var sqlCommand = new SqlCommand(createTownQuery, connection);
                sqlCommand.Parameters.AddWithValue("@name", town);
                sqlCommand.ExecuteNonQuery();
                townId = GetTownId(connection, town);
                Console.WriteLine($"Town {town} was added to the database.");
            }

            int? villainId = GetVillainId(connection, villainName);
            if (villainId == null)
            {
                string createVillainQuery = "INSERT INTO Villains ([Name],EvilnessFactorId) VALUES (@villainNAME,6)";
                using var sqlCommand = new SqlCommand(createVillainQuery, connection);
                sqlCommand.Parameters.AddWithValue("@villainName", villainName);
                sqlCommand.ExecuteNonQuery();
                villainId = GetVillainId(connection, villainName);
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }

            CreateMinion(connection, minionName, age, townId);

            var minionId = GetMinionId(connection, minionName);

            InsertMinionVillains(connection, villainId, minionId);
            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");
        }

        private static void InsertMinionVillains(SqlConnection connection, int? villainId, int? minionId)
        {
            var insertIntoMinVil = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
            var sqlCommand = new SqlCommand(insertIntoMinVil, connection);
            sqlCommand.Parameters.AddWithValue("@villainId", villainId);
            sqlCommand.Parameters.AddWithValue("@minionId", minionId);
            sqlCommand.ExecuteNonQuery();


        }

        private static int? GetMinionId(SqlConnection connection, string minionName)
        {
            var minionIdQuery = "SELECT Id FROM Minions WHERE [Name] = @Name";
            var sqlCommand = new SqlCommand(minionIdQuery, connection);
            sqlCommand.Parameters.AddWithValue("@Name", minionName);
            var minionId = sqlCommand.ExecuteNonQuery();
            return (int?)minionId;
        }

        private static void CreateMinion(SqlConnection connection, string minionName, int age, int? townId)
        {
            string createMinionQuery = "INSERT INTO Minions ([Name],Age,TownId) VALUES (@name,@age, @townId)";
            var sqlCommand = new SqlCommand(createMinionQuery, connection);
            sqlCommand.Parameters.AddWithValue("@name", minionName);
            sqlCommand.Parameters.AddWithValue("@age", age);
            sqlCommand.Parameters.AddWithValue("@townId", townId);
            sqlCommand.ExecuteNonQuery();

        }

        private static int? GetVillainId(SqlConnection connection, string villainName)
        {
            string villainQuery = "SELECT Id FROM Villains WHERE [Name] = @Name";
            using var sqlCommand = new SqlCommand(villainQuery, connection);
            sqlCommand.Parameters.AddWithValue("@name", villainName);
            var villainId = sqlCommand.ExecuteScalar();

            return (int?)villainId;
        }

        private static int? GetTownId(SqlConnection connection, string town)
        {
            string townIdQuery = "SELECT Id FROM Towns WHERE [Name] = @townName";
            using var sqlCommand = new SqlCommand(townIdQuery, connection);
            sqlCommand.Parameters.AddWithValue("@townName", town);
            var townId = sqlCommand.ExecuteScalar();

            return (int?)townId;

        }

        // ex 3
        private static SqlCommand MinionNamesByEveryVillain(SqlConnection connection)
        {
            var id = int.Parse(Console.ReadLine());
            string villainNameQuery = "SELECT [Name] FROM Villains WHERE Id = @Id";
            var command = new SqlCommand(villainNameQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            var result = command.ExecuteScalar();
            string minionsQuery = @"SELECT ROW_NUMBER() OVER (ORDER BY m.[Name]) AS RowNum, m.[Name], m.Age 
                                  FROM MinionsVillains AS mv 
                                  JOIN Minions AS m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                        ORDER BY m.[Name]";
            if (result == null)
            {
                Console.WriteLine($"No villain with ID {id} exist in the database.");
            }
            else
            {
                Console.WriteLine($"Villain: {result}");
                using (var minionCommand = new SqlCommand(minionsQuery, connection))
                {
                    minionCommand.Parameters.AddWithValue("@Id", id);
                    using (var reader = minionCommand.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}.{reader[1]} {reader[2]}");
                        }
                        // have not mininons
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("(no minions)");
                        }
                    }


                }
            }

            return command;
        }

        private static object ExecuteScalar(SqlConnection connection, string query, params KeyValuePair<string, string>[] keyValuePairs)
        {
            using var command = new SqlCommand(query, connection);

            foreach (var kvp in keyValuePairs)
            {
                command.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }
            var result = command.ExecuteScalar();
            return result;

        }


        // second ex
        private static void VillainNames(SqlConnection connection)
        {
            // first query is if have more different data, because i have not this use second query
            /* string query = @"SELECT [Name], COUNT(mv.MinionId) FROM Villains AS v LEFT JOIN MinionsVillains  AS mv ON MV.VillainId = v.Id GROUP BY v.Id, v.[Name] HAVING COUNT(mv.MinionId) > 3";*/
            string query = @"SELECT [Name], COUNT(mv.MinionId) FROM Villains AS v LEFT JOIN MinionsVillains  AS mv ON MV.VillainId = v.Id GROUP BY v.Id, v.[Name]";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader[0];
                        var count = reader[1];
                        Console.WriteLine($"{ name} - {count}");
                    }
                }
            }
        }

        //first ex
        private static void InitialSetUp(SqlConnection connection)
        {
            //   string createDatabase = "CREATE DATABASE MinionsDB";

            var createTableStatements = GetCreateTableStatements();
            foreach (var query in createTableStatements)
            {
                ExecuteNonQuery(connection, query);
            }

            var insertStatements = GetInsertDataStatements();

            foreach (var query in insertStatements)
            {
                ExecuteNonQuery(connection, query);
            }
        }

        private static string[] GetInsertDataStatements()
        {
            var result = new string[]
            {
                "INSERT INTO Countries(Id,[Name]) VALUES(1,'Bulgaria'), (2,'Norway'), (3,'Cypris'), (4,'Greece'), (5,'UK')",
               "INSERT INTO Towns(Id,[Name], CountryCode) VALUES(1,'Plovdiv',1), (2,'Oslo',2), (3,'Larnaca',3), (4,'Athens',4), (5,'London', 5)",
               "INSERT INTO Minions VALUES(1,'Natasha', 20,1),(2,'Thor', 24,2),(3,'Capitan America', 12,3),(4,'Vision', 15,4),(5,'Tony Stark', 25,5)",
               "INSERT INTO EvilnessFactors VALUES(1,'supergood'), (2,'good'), (3,'bad'), (4,'evil'), (5,'super evil')",
               "INSERT INTO Villains VALUES(1,'Hulk',1), (2,'Loky',2), (3,'Winter Soldier',3), (4,'Thanos',4), (5,'Doctor Doom',5)",
               "INSERT INTO MinionsVillains VALUES(1,1), (2,2), (3,3), (4,4), (5,5)"

            };
            return result;
        }

        private static string[] GetCreateTableStatements()
        {
            var result = new string[]
            {
               " CREATE TABLE Countries(Id INT PRIMARY KEY, [Name] VARCHAR(50))",
               "CREATE TABLE Towns (Id INT PRIMARY KEY,[Name] VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))",
               "CREATE TABLE Minions(Id INT PRIMARY KEY, [Name] VARCHAR(50), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))",
               "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY, [Name] VARCHAR(50))",
               "CREATE TABLE Villains(Id INT PRIMARY KEY, [Name] VARCHAR(50),EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))",
               "CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MininonsVillains PRIMARY KEY(MinionId, VillainId))"

            };
            return result;
        }

        private static void ExecuteNonQuery(SqlConnection connection, string query)
        {
            using var command = new SqlCommand(query, connection);
            var result = command.ExecuteNonQuery();
        }


    }
}

