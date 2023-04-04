using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Music_Player
{
    internal class dbUtils
    {
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
