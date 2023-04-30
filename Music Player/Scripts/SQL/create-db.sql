CREATE TABLE IF NOT EXISTS user (
  user_id INTEGER PRIMARY KEY AUTOINCREMENT,
  username VARCHAR(50) UNIQUE NOT NULL,
  password TEXT NOT NULL,
  default_startup_folder TEXT,
  encrypt_on_exit TEXT CHECK (encrypt_on_exit IN ('True', 'False', 'NULL')),
  show_art TEXT CHECK (show_art IN ('True', 'False', 'NULL')),
  sbit TEXT
);

CREATE TABLE IF NOT EXISTS playlist (
  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  playlist_name VARCHAR(50) NOT NULL COLLATE NOCASE UNIQUE, 
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE IF NOT EXISTS playlist_song (
  playlist_song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  playlist_id INTEGER NOT NULL,
  song_id INTEGER NOT NULL,
  FOREIGN KEY (playlist_id) REFERENCES playlist(playlist_id),
  FOREIGN KEY (song_id) REFERENCES song(song_id)
);

CREATE TABLE IF NOT EXISTS song (
  song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  song_display_name VARCHAR(50) NOT NULL,
  song_artist TEXT NOT NULL,
  song_album TEXT NOT NULL,
  song_genre TEXT,
  song_release TEXT,
  song_duration INTEGER
);