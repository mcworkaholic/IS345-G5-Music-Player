using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace Music_Player
{
    internal class dbUtils
    {
        // set SQlite connection once application loads
        public string connectionString { get; set; }

        // Hash a password using Bcrypt
        public static string HashPassword(string password)
        {
            string hashedPassword = BC.HashPassword(password);
            return (hashedPassword);
        }
        public void GetConnection()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbFolderPath = Path.Combine(appDataPath, @"Ugli\");
            var dbPath = Path.Combine(dbFolderPath, @"users.db");
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
                File.Create(dbPath).Close();
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string createTables = "CREATE TABLE user (\r\n  user_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  username VARCHAR(50) UNIQUE NOT NULL,\r\n  password TEXT NOT NULL,\r\n  default_startup_folder TEXT\r\n);\r\n\r\nCREATE TABLE soundcloud_details (\r\n  scld_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  user_id INTEGER NOT NULL,\r\n  scld_client_id TEXT,\r\n  scld_auth_token TEXT,\r\n  FOREIGN KEY (user_id) REFERENCES user (user_id)\r\n);\r\n\r\nCREATE TABLE playlist (\r\n  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  user_id INTEGER NOT NULL,\r\n  playlist_name VARCHAR(50) NOT NULL COLLATE NOCASE UNIQUE, \r\n  FOREIGN KEY (user_id) REFERENCES user (user_id)\r\n);\r\n\r\nCREATE TABLE playlist_song (\r\n  playlist_song_id INTEGER PRIMARY KEY,\r\n  playlist_id INTEGER NOT NULL,\r\n  song_id INTEGER NOT NULL,\r\n  FOREIGN KEY (playlist_id) REFERENCES playlist(playlist_id),\r\n  FOREIGN KEY (song_id) REFERENCES song(song_id)\r\n);\r\n\r\nCREATE TABLE song (\r\n  song_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  song_display_name VARCHAR(50) NOT NULL,\r\n  song_file_name VARCHAR(50) NOT NULL,\r\n  song_path TEXT NOT NULL,\r\n  song_artist TEXT NOT NULL,\r\n  song_album TEXT,\r\n  song_genre TEXT,\r\n  song_release_year INTEGER,\r\n  song_duration INTEGER\r\n);";
                    using (SQLiteCommand command = new SQLiteCommand(createTables, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            connectionString = dbPath;
        }
        public string GetStartUpFolder()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                string startupPath = "";
                connection.Open();
                string sql = "SELECT default_startup_folder FROM user WHERE user_id = @user_id";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            startupPath = reader["default_startup_folder"].ToString();
                        }
                    }
                }
                return startupPath;
            }
        }

        public List<string> GetPlaylists()
        {
            List<string> playlists = new List<string>();

            // Add stored playlists from user to playlists combobox
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                string sql = "SELECT playlist_name FROM playlist WHERE user_id = @user_id";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playlists.Add(reader["playlist_name"].ToString());
                        }
                    }
                }
            }
            return playlists;
        }
        public string GetHash(string username)
        {
            // Get the stored hash 
            string storedHash = "";
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                string sql = "SELECT password, user_id FROM user WHERE username=@username";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader.GetString(0);
                            Program.user_id = reader.GetInt32(1);
                        }
                    }
                }
            }
            return storedHash;
        }
        public void InsertUser(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                string hash = "";
                string storedHash;
                connection.Open();
                string insert = "INSERT INTO user (username, password) VALUES (@username, @password)";
                using (SQLiteCommand command = new SQLiteCommand(insert, connection))
                {
                    hash = HashPassword(password);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hash);
                    command.ExecuteNonQuery();
                }
                string select = "SELECT password, user_id FROM user WHERE username=@username";
                using (SQLiteCommand command = new SQLiteCommand(select, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader.GetString(0);
                            Program.user_id = reader.GetInt32(1);
                        }
                    }
                }
                string scldInsert = "INSERT INTO soundcloud_details (user_id) VALUES (@user_id)";
                using (SQLiteCommand command = new SQLiteCommand(scldInsert, connection))
                {
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertPlaylist(System.Windows.Forms.ComboBox comboBox)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                string sql = "INSERT INTO playlist (user_id, playlist_name) SELECT @user_id, @playlist_name";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@playlist_name", comboBox.Text);
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetUserConfig()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT username AS 'Application Username', default_startup_folder AS 'Music Folder', scld_client_id, scld_auth_token FROM user " +
                "INNER JOIN soundcloud_details ON user.user_id = soundcloud_details.user_id " +
                "WHERE user.user_id = @user_id;";
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    try
                    {
                        connection.Open();
                        var da = new SQLiteDataAdapter(command);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            return dt;
        }
        public void UpdateChanges(string table, string attribute, string new_value)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                string sql = $"Update {table} SET {attribute} = @new_value";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@new_value", new_value);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
