CREATE TABLE user (
  user_id INTEGER PRIMARY KEY AUTOINCREMENT,
  username VARCHAR(50) UNIQUE NOT NULL,
  password TEXT NOT NULL,
  default_startup_folder TEXT 
);

CREATE TABLE playlist (
  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  playlist_name VARCHAR(50) NOT NULL UNIQUE, 
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE playlist_song (      /*bridge */
  ps_id INTEGER PRIMARY KEY,
  playlist_id INTEGER NOT NULL,
  song_id INTEGER NOT NULL,
  FOREIGN KEY (playlist_id) REFERENCES playlist(id),
  FOREIGN KEY (song_id) REFERENCES song(id)
);

CREATE TABLE song (
  song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  song_display_name VARCHAR(50) NOT NULL,
  song_file_name VARCHAR(50) NOT NULL,
  song_path TEXT NOT NULL,
);