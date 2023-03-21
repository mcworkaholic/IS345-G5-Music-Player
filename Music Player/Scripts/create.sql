CREATE TABLE user (
  user_id NUMBER PRIMARY KEY,
  username VARCHAR2(50) NOT NULL,
  password VARCHAR2(50) NOT NULL
);

CREATE TABLE playlist (
  playlist_id NUMBER PRIMARY KEY,
  user_id NUMBER NOT NULL,
  playlist_name VARCHAR2(50) NOT NULL,
  CONSTRAINT fk_user_id FOREIGN KEY (user_id)
    REFERENCES user (user_id)
);

CREATE TABLE song (
  song_id NUMBER PRIMARY KEY,
  title VARCHAR2(50) NOT NULL,
  artist VARCHAR2(50) NOT NULL,
  album VARCHAR2(50),
  playlist_id NUMBER,
  CONSTRAINT fk_playlist_id FOREIGN KEY (playlist_id)
    REFERENCES playlist (playlist_id)
);