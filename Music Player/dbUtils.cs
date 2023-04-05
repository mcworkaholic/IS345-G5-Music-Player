using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace Music_Player
{
    internal class dbUtils
    {
        // Hash a password using Bcrypt
        public static string HashPassword(string password)
        {
            string hashedPassword = BC.HashPassword(password);
            return (hashedPassword);
        }
        public string GetStartUpFolder(string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
        public void UpdateStartUpFolder(string connectionString, string musicFolderPath)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE user SET default_startup_folder = @new_folder WHERE user_id = @user_id";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", Program.user_id);
                    command.Parameters.AddWithValue("@new_folder", musicFolderPath);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetPlaylists(string connectionString)
        {
            List<string> playlists = new List<string>();

            // Add stored playlists from user to playlists combobox
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
        public string GetHash(string connectionString, string username)
        {
            // Get the stored hash 
            string storedHash = "";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
        public void InsertUser(string connectionString, string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
            }
        }
        public void InsertPlaylist(ComboBox comboBox, string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString)) // move to its own class
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
    }
}
