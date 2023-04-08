CREATE TABLE user (
  user_id INTEGER PRIMARY KEY AUTOINCREMENT,
  username VARCHAR(50) UNIQUE NOT NULL,
  password TEXT NOT NULL,
  default_startup_folder TEXT
);

CREATE TABLE soundcloud_details (
  scld_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  scld_client_id TEXT,
  scld_auth_token TEXT,
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE playlist (
  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  playlist_name VARCHAR(50) NOT NULL COLLATE NOCASE UNIQUE, 
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE playlist_song (
  playlist_song_id INTEGER PRIMARY KEY,
  playlist_id INTEGER NOT NULL,
  song_id INTEGER NOT NULL,
  FOREIGN KEY (playlist_id) REFERENCES playlist(playlist_id),
  FOREIGN KEY (song_id) REFERENCES song(song_id)
);

CREATE TABLE song (
  song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  song_display_name VARCHAR(50) NOT NULL,
  song_file_name VARCHAR(50) NOT NULL,
  song_path TEXT NOT NULL,
  song_artist TEXT NOT NULL,
  song_album TEXT,
  song_genre TEXT,
  song_release_year INTEGER,
  song_duration INTEGER
);