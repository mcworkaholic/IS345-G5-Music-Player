CREATE TABLE user (
  user_id INTEGER PRIMARY KEY AUTOINCREMENT,
  username VARCHAR(50) UNIQUE NOT NULL,
  password TEXT NOT NULL
);

CREATE TABLE playlist (
  playlist_id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id INTEGER NOT NULL,
  playlist_name VARCHAR(50) NOT NULL UNIQUE, 
  FOREIGN KEY (user_id) REFERENCES user (user_id)
);

CREATE TABLE song (
  song_id INTEGER PRIMARY KEY AUTOINCREMENT,
  song_display_name VARCHAR(50) NOT NULL,
  song_file_name VARCHAR(50) NOT NULL,
  song_path TEXT NOT NULL,
  playlist_id INTEGER NOT NULL,
  FOREIGN KEY (playlist_id) REFERENCES playlist (playlist_id)
);