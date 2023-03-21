CREATE TABLE user (
  user_id INTEGER PRIMARY KEY AUTOINCREMENT,
  username TEXT UNIQUE NOT NULL,
  password TEXT NOT NULL
);

CREATE TABLE playlist (
  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  playlist_name TEXT NOT NULL UNIQUE, 
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE song (
  song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  song_title TEXT NOT NULL,
  song_display_name TEXT NOT NULL,
  song_file_name TEXT NOT NULL,
  playlist_id INTEGER NOT NULL,
  FOREIGN KEY (playlist_id) REFERENCES playlist (playlist_id)
);