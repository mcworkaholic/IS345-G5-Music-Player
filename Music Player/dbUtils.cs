using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace Music_Player
{
    internal class DBUtils
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
            var dbFolderPath = Path.Combine(appDataPath, @"RhythmRanger\");
            var dbPath = Path.Combine(dbFolderPath, @"users.db");
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
                File.Create(dbPath).Close();
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string createTables = "CREATE TABLE user (\r\n  user_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  username VARCHAR(50) UNIQUE NOT NULL,\r\n  password TEXT NOT NULL,\r\n  default_startup_folder TEXT\r\n);\r\n\r\nCREATE TABLE soundcloud_details (\r\n  scld_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  user_id INTEGER NOT NULL,\r\n  scld_client_id TEXT,\r\n  scld_auth_token TEXT,\r\n  FOREIGN KEY (user_id) REFERENCES user (user_id)\r\n);\r\n\r\nCREATE TABLE playlist (\r\n  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  user_id INTEGER NOT NULL,\r\n  playlist_name VARCHAR(50) NOT NULL COLLATE NOCASE UNIQUE, \r\n  FOREIGN KEY (user_id) REFERENCES user (user_id)\r\n);\r\n\r\nCREATE TABLE playlist_song (\r\n  playlist_song_id INTEGER PRIMARY KEY,\r\n  playlist_id INTEGER NOT NULL,\r\n  song_id INTEGER NOT NULL,\r\n  FOREIGN KEY (playlist_id) REFERENCES playlist(playlist_id),\r\n  FOREIGN KEY (song_id) REFERENCES song(song_id)\r\n);\r\n\r\nCREATE TABLE song (\r\n  song_id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n  song_display_name VARCHAR(50) NOT NULL,\r\n  song_artist TEXT NOT NULL,\r\n  song_album TEXT NOT NULL,\r\n  song_genre TEXT,\r\n  song_release TEXT,\r\n  song_duration INTEGER\r\n);";
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
                string playlistInsert = "INSERT INTO playlist (user_id, playlist_name) VALUES (@user_id, 'Library')";
                using (SQLiteCommand command = new SQLiteCommand(playlistInsert, connection))
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
        public void InsertSong(FileSystemTreeNode currentNode, string playlist_name)
        {
            string fullPath = currentNode.FullPath;

            // Grab metadata and try to insert into songs table
            TagLib.File file_TAG = TagLib.File.Create(fullPath);
            string title = file_TAG.Tag.Title;
            string artist = file_TAG.Tag.AlbumArtists.Length > 0 ? file_TAG.Tag.AlbumArtists[0] : "";
            string album = file_TAG.Tag.Album;
            string genre = file_TAG.Tag.Genres.Length > 0 ? file_TAG.Tag.Genres[0] : "";
            string year = file_TAG.Tag.Year.ToString();
            TimeSpan duration = file_TAG.Properties.Duration;

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                string songInsert = "INSERT INTO song (song_display_name, song_artist, song_album, song_genre, song_release, song_duration) " +
                    "VALUES (@display_name, @artist, @album, @genre, @release, @duration)";
                using (SQLiteCommand command = new SQLiteCommand(songInsert, connection))
                {
                    //we use the ternary operator ? : to check whether each metadata value is null or empty.
                    //If it's not null or empty, we use the value itself. Otherwise, we use properties of the node via the FileSystemTreeNode class.
                    command.Parameters.AddWithValue("@display_name", !string.IsNullOrEmpty(title) ? title : currentNode.DisplayName);
                    command.Parameters.AddWithValue("@artist", !string.IsNullOrEmpty(artist) ? artist : currentNode.Parent.Parent.DisplayName);
                    command.Parameters.AddWithValue("@album", !string.IsNullOrEmpty(album) ? album : currentNode.Parent.DisplayName);
                    command.Parameters.AddWithValue("@genre", !string.IsNullOrEmpty(genre) ? genre : "Unknown");
                    command.Parameters.AddWithValue("@release", !string.IsNullOrEmpty(year.ToString()) ? year : "Unknown");
                    command.Parameters.AddWithValue("@duration", (int)duration.TotalSeconds);
                    command.ExecuteNonQuery();
                }
                // First, retrieve the last insert rowid from the song table
                long lastId = 0;
                using (SQLiteCommand getLastId = new SQLiteCommand("SELECT last_insert_rowid() FROM song", connection))
                {
                    lastId = (long)getLastId.ExecuteScalar();
                }
                string playlistInsert = "INSERT INTO playlist_song (playlist_id, song_id) VALUES ((SELECT playlist_id FROM playlist WHERE playlist_name = @playlist_name), @song_id)";
                using (SQLiteCommand command = new SQLiteCommand(playlistInsert, connection))
                {
                    command.Parameters.AddWithValue("@playlist_name", playlist_name);
                    command.Parameters.AddWithValue("@song_id", lastId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetPlaylistSongs(int userId, string playlistName)
        {
            List<string> songs = new List<string>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString}"))
            {
                connection.Open();
                using
                    (SQLiteCommand command = new SQLiteCommand("SELECT song.song_display_name FROM song " +
                                                   "INNER JOIN playlist_song ON song.song_id = playlist_song.song_id " +
                                                   "INNER JOIN playlist ON playlist.playlist_id = playlist_song.playlist_id " +
                                                   "INNER JOIN user ON user.user_id = playlist.user_id " +
                                                   "WHERE playlist.playlist_name = @playlist_name " +
                                                   "AND user.user_id = @user_id", connection))
                {
                    command.Parameters.AddWithValue("@playlist_name", playlistName);
                    command.Parameters.AddWithValue("@user_id", userId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string songDisplayName = reader.GetString(0);
                            songs.Add(songDisplayName);
                        }
                    }
                }
            }
            return songs;
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
