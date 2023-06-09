﻿// Author: Weston Evans/mcworkaholic on Github
// 
// Application purpose:  This application was intended to become a decently
// Functioning music player with a mix of old and new technologies. Heavily built with the help of AI, limited by my own creativity and perseverance.
// Thankfully, I'm stubborn and useful material is scattered throughout the internet based on WinForms age. This makes WinForms a perfect candidate for AI based applications and its limited use cases to before 2021.
// In the future I plan on adding archiving functionality with a few Python scripts, with the ability to sync with services such as Soundcloud, Spotify, and Youtube Music.
// The design is pretty intuitive, all you have to do is first create an account(this is what keeps track of each profile's playlists)
// and then double click on any song in the listbox to get started.
// In the future I'm going to allow the user to define what folder is opened up at the start.  ✔️
// Each icon that does not have text, has a tooltip describing what the action does. ✔️
// The search textbox is autopopulated with every node contained in the tree besides the root from FileSystemTreeNode.cs,
// to allow the user to make fast searches and either play a song or open an album.
// The audio is controlled from MusicPlayer.cs via the CSCore audio library, and the audio/video is synced with the visuals from the WindowsMediaPlayer component(hacky I know).
// TODO: (.ogg) files are not yet supported, looking at Concentus library next
// TODO: Whitecap, Aero, and G-force visual plugins need to be modified to work correctly without memory leaks

using AxWMPLib;
using CSCore.CoreAudioAPI;
using Microsoft.Win32;
using Music_Player.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using Cursors = System.Windows.Forms.Cursors;

namespace Music_Player
{
    public partial class RhythmRanger : Form
    {
        // Rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public string albumVisible;

        // Class Instantiation
        private Config config = new Config();
        private Utes utes = new Utes();
        private MusicPlayerClass MusicPlayerClass = new MusicPlayerClass(); //создание объекта музыкального плеера

        private GlobalKeyboardHook _globalKeyboardHook; // initiate
        private DBUtils dbUtils = new DBUtils();
        private EQ equalizerWindow;

        // tracking for queuing
        private int currentSelectedIndex;

        // for moving borderless form
        private int movX, movY;
        private bool isMoving;

        // initial states
        private bool shuffleButtonClicked = false;
        private bool listBoxDoubleClick = false;
        private bool listViewDoubleClick = false;
        private bool ctrlDown = false;
        private bool stopUpdate;
        private bool playlistViewing = false;


        private int shuffleButtonClickCount = 0;
        private int playlistIndexChanged = 0;
        private int deviceIndexChanged = 0;
        private int lastVolume = 85;
        private int atIndex = 0;


        public static List<string> paths;
        private List<int> trackIndexes = new List<int>();

        // variables that are set on formload event
        private string connectionString;
        private (Dictionary<string, List<string>>, string) devices;

        // Modified throughout use
        private List<int> queueList = new List<int>();

        // for storing initial root node
        private FileSystemTreeNode rootNode;

        private string songObj = "(Song)";
        private string albumObj = "(Album)";
        private string artistObj = "(Artist)";

        // Fullscreen capabilities
        private bool _isFullscreenToggle = false;
        public bool IsFullscreen
        {
            get { return _isFullscreenToggle; }
            set { _isFullscreenToggle = value; }
        }
        private Size _previousVideoContainerSize = new Size();
        public RhythmRanger()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        public string GlobalConnectionString
        {
            get { return connectionString; }
        }

        
        private void buttonHook_Click(object sender, EventArgs e)
        {
            // Hooks only into specified Keys (here "Space").
            _globalKeyboardHook = new GlobalKeyboardHook(new Keys[] { Keys.Space, Keys.LControlKey, Keys.RControlKey, Keys.Right });
            // Hooks into all keys.
            //_globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }
        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            // Now you can access both, the key and virtual code
            Keys loggedKey = e.KeyboardData.Key;
            int loggedVkCode = e.KeyboardData.VirtualCode;


            // Stops sound from cutting out when user is typing on a different application or webpage
            if (this.Focused == true)
            {
                // EDT: No need to filter for VkSnapshot anymore. This now gets handled
                // through the constructor of GlobalKeyboardHook(...).
                if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
                {
                    if (loggedKey == Keys.Space)
                    {
                        MusicPlayerClass.Volume = 0;
                        volumeBar.Value = 0;
                    }
                    else if (loggedKey == Keys.LControlKey)
                    {
                        ctrlDown = true;
                    }
                    else if (ctrlDown && loggedKey == Keys.Right)
                    {
                        NextVisualization();
                    }
                }
                else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
                {
                    if (loggedKey == Keys.Space)
                    {
                        volumeBar.Value = lastVolume;
                        MusicPlayerClass.Volume = volumeBar.Value;
                    }
                    else if (loggedKey == Keys.Control)
                    {
                        ctrlDown = false;
                    }
                }
            }
        }
        public void SetCurrentEffectPreset(int value)
        {
            // requires registry permission. need to configure for users who do not have registry editing permissions
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            string path = string.Format(@"{0}\Software\Microsoft\MediaPlayer\Preferences", identity.User.Value);
            RegistryKey key = Registry.Users.OpenSubKey(path, true) ?? throw new Exception("Registry key not found!");
            key.SetValue("SaveSettingsOnExit", value, RegistryValueKind.DWord);
        }
        private void WindowsMediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            foreach (Control control in audioControllerPanel.Controls)
            {
                control.Enabled = true;
            }
            if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                // start a timer to initiate autoplay of songs with the current play state (shuffle or next)
                playTimer.Interval = 100;
                playTimer.Enabled = true;
                timer1.Stop();
            }
            else if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                playPauseButton.Image = Properties.Resources.Play;
                timer1.Stop();
            }
            else if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                audioPosTrackBar.Maximum = (int)WindowsMediaPlayer.Ctlcontrols.currentItem.duration;
                timer1.Start();
                playPauseButton.Image = Properties.Resources.Pause;
            }
            deviceBox.Enabled = true;
            eqButton.Enabled = true;
            playlistBox.Enabled = true;
            searchTextBox.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                audioPosTrackBar.Value = (int)WindowsMediaPlayer.Ctlcontrols.currentPosition;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void playTimer_Tick(object sender, EventArgs e)
        {
            if (queueList.Count == 0)
            {
                queueLabel.Visible = false;
                clearQueueButton.Visible = false;
            }
            else
            {
                // Step through the queued songs sequentially
                WindowsMediaPlayer.URL = paths[queueList[0]];
                Play(WindowsMediaPlayer.URL);
                queueList.RemoveAt(0);
                queueLabel.Text = $"Queued Songs: {queueList.Count}";
            }
            if (!queueLabel.Visible)
            {
                if (shuffleButtonClicked == false)
                {
                    if (songslistView.SelectedItems.Count > 0 && songslistView.SelectedItems[0].Index < songslistView.Items.Count - 1)
                    {
                        // sets the next consecutive song to be played next
                        songslistView.Items[songslistView.SelectedItems[0].Index + 1].Selected = true;
                    }
                    else
                    {
                        // if at the end of the selected list, jump to the first song (index 0 of current list of songs)
                        songslistView.Items[0].Selected = true;
                    }
                    int currentIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(songslistView.SelectedItems[0].Text, songObj).FullPath));
                    WindowsMediaPlayer.URL = paths[currentIndex];
                    songslistView.Items[currentIndex].Selected = true;
                }
                else
                {
                    if (atIndex == trackIndexes.Count - 1)
                    {
                        trackIndexes.Clear();
                        CallShuffle();
                        atIndex = 0;
                    }
                    atIndex++;
                    WindowsMediaPlayer.URL = paths[trackIndexes[atIndex]];
                    songslistView.Items[trackIndexes[atIndex]].Selected = true;
                }
                Play(WindowsMediaPlayer.URL);
            }
            playTimer.Enabled = false;
        }
        private void previousBox_Click(object sender, EventArgs e)
        {
            songslistView.Focus();
            if (shuffleButtonClicked == false)
            {
                if (songslistView.SelectedItems.Count > 0)
                {
                    // Go back 1 song from the current index of the library
                    int currentIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(songslistView.SelectedItems[0].Text, songObj).FullPath));
                    songslistView.Items[currentIndex].Selected = false;
                    if (currentIndex != 0)
                    {
                        Play(paths[currentIndex - 1]);
                        songslistView.Items[currentIndex - 1].Selected = true;
                        songslistView.Items[currentIndex - 1].EnsureVisible();
                    }
                    else
                    {
                        Play(paths[paths.Count - 1]);
                        int lastIndex = songslistView.Items.Count - 1;
                        songslistView.Items[lastIndex].Selected = true;
                        songslistView.Items[lastIndex].EnsureVisible();
                    }

                }
                else if (songslistView.SelectedItems.Count == 0)
                {
                    // Go back to the start/top of the library
                    songslistView.Items[0].Selected = true;
                    songslistView.Items[0].EnsureVisible();
                    Play(paths[0]);
                }
            }
            else if (shuffleButtonClicked == true)
            {
                if (atIndex == trackIndexes.Count - 1)
                {
                    trackIndexes.Clear();
                    CallShuffle();
                    atIndex = 0;
                }
                atIndex++;
                int prevIndex = trackIndexes[atIndex];
                songslistView.SelectedItems.Clear();
                songslistView.Items[prevIndex].Selected = true;
                songslistView.Items[prevIndex].EnsureVisible();
                WindowsMediaPlayer.URL = paths[prevIndex];
                Play(WindowsMediaPlayer.URL);
            }
        }
        private void Play(string songPath)
        {
            List<string> deviceId;
            string selectedDeviceName = deviceBox.SelectedItem.ToString();
            if (devices.Item1.ContainsKey(selectedDeviceName))
            {
                deviceId = devices.Item1[selectedDeviceName];
                if (deviceId.Count > 0)
                {
                    // Set the default device to a new device based on its device ID 
                    var currentDevice = MusicPlayerClass.GetSoundDevice(deviceId.First());
                    WindowsMediaPlayer.URL = songPath;
                    MusicPlayerClass.Open(songPath, currentDevice);
                    MusicPlayerClass.Play();
                    timer1.Start();
                }
            }
        }
        private void nextBox_Click(object sender, EventArgs e)
        {
            songslistView.Focus();
            if (queueList.Count > 0)
            {
                Play(paths[queueList[0]]);
                queueList.RemoveAt(0);
                queueLabel.Text = $"Queued Songs: {queueList.Count}";
            }
            else
            {
                clearQueueButton.Visible = false;
                queueLabel.Visible = false;
                if (shuffleButtonClicked == false)
                {
                    if (songslistView.SelectedIndices.Count == 0)
                    {
                        int currentIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                        if (paths.Count - 1 == currentIndex)
                        {
                            songslistView.Items[0].Selected = true;
                            songslistView.Items[0].EnsureVisible();
                        }
                        else
                        {
                            songslistView.Items[currentIndex + 1].Selected = true;
                            songslistView.Items[currentIndex + 1].EnsureVisible();
                        }
                        WindowsMediaPlayer.URL = paths[songslistView.SelectedIndices[0]];
                    }
                    else if (songslistView.SelectedIndices[0] < songslistView.Items.Count - 1)
                    {
                        int currentIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                        WindowsMediaPlayer.URL = paths[currentIndex + 1];
                        songslistView.Items[songslistView.SelectedIndices[0] + 1].Selected = true;
                        songslistView.Items[songslistView.SelectedIndices[0]].EnsureVisible();
                    }
                    else if (songslistView.SelectedIndices[0] == songslistView.Items.Count - 1)
                    {
                        songslistView.Items[0].Selected = true;
                        songslistView.Items[0].EnsureVisible();
                        WindowsMediaPlayer.URL = paths[songslistView.SelectedIndices[0]];
                    }
                    Play(WindowsMediaPlayer.URL);
                }
                else if (shuffleButtonClicked == true)
                {
                    if (atIndex == trackIndexes.Count - 1)
                    {
                        trackIndexes.Clear();
                        CallShuffle();
                        atIndex = 0;
                    }
                    atIndex++;
                    songslistView.Items[trackIndexes[atIndex]].Selected = true;
                    songslistView.Items[trackIndexes[atIndex]].EnsureVisible();
                    WindowsMediaPlayer.URL = paths[songslistView.SelectedIndices[0]];
                    Play(WindowsMediaPlayer.URL);
                }
            }
        }
        private void CallShuffle()
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < songslistView.Items.Count; i++)
            {
                indexList.Add(i);
            }
            indexList.Remove(songslistView.SelectedIndices[0]); //////////////////////////////////////////
            trackIndexes = MusicPlayerClass.Shuffle(indexList);
        }
        private void shuffleButton_Click(object sender, EventArgs e)
        {
            // each button click increments the global counter
            // this is  a method of turning off and on shuffle mode, odd clicks turn it on, even clicks turn it off.
            // if the current click count divided by 2 yields a remainder of 1,
            // shuffleButtonClickCount == odd and shuffle mode == off
            // Look at ToggleState function in Utes.cs 

            shuffleButtonClicked = utes.ToggleState(shuffleButtonClicked, ref shuffleButtonClickCount);
            if (shuffleButtonClicked)
            {
                //set button text as indicator
                // shuffle mode ON
                shuffleButton.Text = "🔀 Shuffle";
                CallShuffle();
            }
            else
            {
                //set button text as indicator
                // shuffle mode OFF
                shuffleButton.Text = "Shuffle";
                trackIndexes.Clear();
                atIndex = 0;
            }
        }
        private void queueButton_Click(object sender, EventArgs e)
        {
            queueLabel.Visible = true;
            clearQueueButton.Visible = true;
            if (songslistView.Visible == true)
            {
                currentSelectedIndex = songslistView.FocusedItem.Index;
            }
            else if (treeView.Visible == true)
            {
                currentSelectedIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(treeView.SelectedNode.Text, songObj).FullPath));
            }

            queueList.Add(currentSelectedIndex);
            queueLabel.Text = $"Queued Songs: {queueList.Count}";
        }
        public static void SetArtwork(string audioFilePath, string artworkFilePath)
        {
            try
            {
                var file = TagLib.File.Create(audioFilePath);
                var picture = new TagLib.Picture(artworkFilePath);
                file.Tag.Pictures = new TagLib.IPicture[] { picture };

                // Set the MIME type of the picture based on the file extension
                if (Path.GetExtension(artworkFilePath).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase))
                {
                    picture.MimeType = TagLib.Picture.GetMimeFromExtension(".jpg");
                }
                else if (Path.GetExtension(artworkFilePath).Equals(".png", StringComparison.InvariantCultureIgnoreCase))
                {
                    picture.MimeType = TagLib.Picture.GetMimeFromExtension(".png");
                }

                // Save the changes to the file
                file.Save();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error setting artwork for {audioFilePath}: {ex.Message}");
            }
        }
        private void GetAlbumArt()
        {
            if (albumVisible == "True")
            {
                bool albumArtFound = false;
                string currentSongPath = WindowsMediaPlayer.currentMedia.sourceURL;
                var currentNode = rootNode.FindNodeByFullPath(currentSongPath, songObj);
                if (currentNode != null)
                {
                    try
                    {
                        TagLib.File file_TAG = TagLib.File.Create(currentNode.FullPath);
                        if (file_TAG.Tag.Pictures.Length >= 1)
                        {
                            var bin = file_TAG.Tag.Pictures[0].Data.Data;
                            Image image = Image.FromStream(new MemoryStream(bin));
                            albumArtBox.Image = image;
                            albumArtBox.Visible = true;
                        }
                        else if (file_TAG.Tag.Pictures.Length == 0)
                        {
                            foreach (string path in Directory.EnumerateFiles(currentNode.Parent.FullPath))
                            {
                                if (Path.GetExtension(path) == ".jpg" || Path.GetExtension(path) == ".png")
                                {
                                    Bitmap albumArtBitmap = new Bitmap(Image.FromFile(path));
                                    albumArtBox.Image = albumArtBitmap;
                                    albumArtBox.Visible = true;

                                    //// Set the artwork of the song to the found file
                                    SetArtwork(currentNode.FullPath, path);

                                    albumArtFound = true; // flag that an album art file was found
                                    break;
                                }
                            }

                            if (!albumArtFound) // set album art box to invisible if no file was found
                            {
                                albumArtBox.Image = Resources.EmptyAlbumArtwork;
                                if (albumVisible == "True")
                                {
                                    albumArtBox.Visible = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("An unexpected error has occurred: " + ex.StackTrace + "\n" + "\n" + ex.Message); }
                }
            }
            else
            {
                albumArtBox.Visible = false;
            }
            if (albumArtBox.Visible == true)
            {
                showToolStripMenuItem.Checked = true;
                hideToolStripMenuItem.Checked = false;
            }
            else
            {
                showToolStripMenuItem.Checked = false;
                hideToolStripMenuItem.Checked = true;
            }
        }
        private void WindowsMediaPlayer_MediaChange(object sender, _WMPOCXEvents_MediaChangeEvent e)
        {
            // Get the duration of the media file being played
            int duration = (int)WindowsMediaPlayer.currentMedia.duration;
            audioPosTrackBar.Maximum = (int)duration;
            // Set volume of music to 0 while its changing, then set again to value of the volume trackbar
            MusicPlayerClass.Volume = 0;
            MusicPlayerClass.Volume = volumeBar.Value;
            //  display it in durationLabel
            durationLabel.Visible = true;
            durationLabel.Text = $"Length: {TimeSpan.FromSeconds(duration):mm\\:ss}";
            GetAlbumArt();

        }
        public void LoadLibrary(string path)
        {
            // designate initial root for tree traversal
            rootNode = FileSystemTreeNode.BuildTree(path);
            if (songslistView.Items.Count > 0)
            {
                // clear list, listview and reset visibilities
                songslistView.Items.Clear();
                paths.Clear();
                songslistView.Visible = true;
                albumPanel.Visible = false;
                configPanel.Visible = false;
            }
            // Set up the ListView control
            songslistView.View = View.Details;
            songslistView.Columns.Add("Song", -1, HorizontalAlignment.Left);
            songslistView.Columns.Add("Artist", -1, HorizontalAlignment.Left);
            songslistView.Columns.Add("Album", -1, HorizontalAlignment.Left);
            songslistView.Columns.Add("Genre", -1, HorizontalAlignment.Left);
            songslistView.Columns.Add("Release Date", -1, HorizontalAlignment.Left);

            // Loop through the songs and add them to the ListView control
            // Get the third level children of the root node
            List<FileSystemTreeNode> thirdLevelChildren = new List<FileSystemTreeNode>();
            foreach (var child in rootNode.Children)
            {
                foreach (var grandChild in child.Children)
                {
                    foreach (var greatGrandChild in grandChild.Children)
                    {
                        thirdLevelChildren.Add(greatGrandChild);
                        if (greatGrandChild.NodeType == NodeType.File)
                        {
                            // Get the song data
                            string songName = greatGrandChild.DisplayName;
                            string albumName = greatGrandChild.Parent.DisplayName;
                            string artistName = greatGrandChild.Parent.Parent.DisplayName;
                            string genreName = ""; // Get the genre from the song metadata
                            string releaseDate = ""; // Get the release date from the song metadata

                            // Create a new ListViewItem with subitems for each column
                            var item = new ListViewItem(songName);
                            item.SubItems.Add(artistName);
                            item.SubItems.Add(albumName);
                            item.SubItems.Add(genreName);
                            item.SubItems.Add(releaseDate);

                            // Add the item to the ListView control
                            songslistView.Items.Add(item);
                        }
                    }
                }
            }
            // Get the names of the files in the third level children
            List<string> fileNames = new List<string>();
            List<string> filePaths = new List<string>();
            foreach (var child in thirdLevelChildren)
            {
                if (child.NodeType == NodeType.File)
                {
                    fileNames.Add(child.FileName);
                    filePaths.Add(child.FullPath);
                }
            }
            paths = filePaths;
            if (deviceBox.Items.Count == 0)
            {
                // Populate Device dropdown with the system's devices, play through default
                LoadDevices();
            }
            foreach (ColumnHeader column in songslistView.Columns)
            {
                int maxPixelLength = -1;
                foreach (ListViewItem item in songslistView.Items)
                {
                    if (column.Index < item.SubItems.Count)
                    {
                        int pixelLength = TextRenderer.MeasureText(item.SubItems[column.Index].Text, songslistView.Font).Width;
                        if (pixelLength > maxPixelLength)
                        {
                            maxPixelLength = pixelLength;
                        }
                    }
                }
                column.Width = maxPixelLength + 10;
            }
            WindowsMediaPlayer.Ctlenabled = false;
        }
        private (Dictionary<string, List<string>>, string) GetAudioDevices()
        {
            var devices = new Dictionary<string, List<string>>();
            // Retrieve all active audio devices
            foreach (var dev in MusicPlayerClass.EnumerateWasapiDevices(DataFlow.Render, DeviceState.Active))
            {
                if (!devices.ContainsKey(dev.FriendlyName))
                {
                    devices.Add(dev.FriendlyName, new List<string>());
                }
                // Dictionary holds device's friendly names as keys and IDs as values
                devices[dev.FriendlyName].Add(dev.DeviceID.ToString());
            }
            // get the default audio endpoint
            string defaultDeviceName = MusicPlayerClass.GetDefaultSoundDevice().FriendlyName;
            return (devices, defaultDeviceName);
        }
        private void LoadDevices()
        {
            // Add available audio devices
            devices = GetAudioDevices();
            foreach (var device in devices.Item1)
            {
                deviceBox.Items.Add(device.Key);
            }
            // Set the initial selected index based on the default audio endpoint
            deviceBox.SelectedIndex = deviceBox.FindStringExact(devices.Item2);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buttonHook_Click(sender, e);
            dbUtils.GetConnection();

            List<string> userConfig = dbUtils.GetUserConfig();
            if (userConfig != null)
            {
                // Grab some values
                albumVisible = userConfig[4];
            }
            WindowsMediaPlayer.settings.volume = 0;
            playPauseButton.Image = Properties.Resources.PausePlay;

            // Add a delegate for the MediaChange event.
            WindowsMediaPlayer.MediaChange += new AxWMPLib._WMPOCXEvents_MediaChangeEventHandler(WindowsMediaPlayer_MediaChange);

            // set visualization preset from windows media player
            SetCurrentEffectPreset(4);
            connectionString = dbUtils.connectionString;
            string startupPath = dbUtils.GetStartUpFolder();
            if (deviceBox.Items.Count == 0)
            {
                LoadDevices();
            }
            if (startupPath != string.Empty && startupPath != null)
            {
                LoadLibrary(startupPath);
                AddSearchSource();
                if (songslistView.Items.Count > 0)
                {
                    // Add Playlists
                    GetPlaylists("Load");
                    songslistView.Visible = true;
                    songslistView.Location = new Point(529, 49);
                    albumPanel.Visible = false;
                    configPanel.Visible = false;
                    configurehintLabel.Visible = false;
                    nolibbox.Visible = false;
                    noLibraryLabel.Visible = false;
                    searchTextBox.Enabled = true;
                    playlistBox.Enabled = true;
                    noLibraryLabel.Visible = false;
                }
                else
                {
                    configPanel.Location = new Point(529, 49);
                    configPanel.Visible = true;
                    songslistView.Visible = false;
                    configPanel.BringToFront();
                    noLibraryLabel.Visible = true;
                    noLibraryLabel.BringToFront();
                    configurehintLabel.Visible = true;
                    configurehintLabel.BringToFront();
                    nolibbox.Visible = true;
                    nolibbox.BringToFront();
                    searchTextBox.Enabled = false;
                    playlistBox.Enabled = false;
                }

            }
            else
            {
                configPanel.Location = new Point(529, 49);
                configPanel.Visible = true;
                songslistView.Visible = false;
                configPanel.BringToFront();
                noLibraryLabel.Visible = true;
                noLibraryLabel.BringToFront();
                configurehintLabel.Visible = true;
                configurehintLabel.BringToFront();
                nolibbox.Visible = true;
                nolibbox.BringToFront();
                searchTextBox.Enabled = false;
                playlistBox.Enabled = false;
            }
            WindowsMediaPlayer.Ctlenabled = false;
        }
        public void GetPlaylists(string action)
        {
            int selectedIndex = playlistBox.SelectedIndex; // Store the selected index
            playlistBox.DataSource = dbUtils.GetPlaylists();
            playlistBox.SelectedIndex = selectedIndex; // Set the selected index again
            if (action == "Add")
            {
                playlistBox.SelectedIndex = playlistBox.Items.Count - 1;
            }
        }
        public void AddSearchSource()
        {
            var allNodes = rootNode.GetAllNodesExceptRoot();
            var autoCompleteSource = allNodes.Select(node => $"{node.DisplayName} {node.ObjectType}").ToList();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (var node in autoCompleteSource)
            {
                MyCollection.Add(node);
            }
            searchTextBox.AutoCompleteCustomSource = MyCollection;
        }
        private void clearQueueButton_Click(object sender, EventArgs e)
        {
            queueList.Clear();
            queueLabel.Visible = false;
            clearQueueButton.Visible = false;
        }
        private void ViewAlbum(FileSystemTreeNode node)
        {
            // clear existing treeview if there is one
            treeView.Nodes.Clear();

            // Get the first song in the album
            var firstSongNode = node.Children.FirstOrDefault();

            // Check if the album has any songs
            if (firstSongNode != null)
            {
                // Get the album art from the first song
                TagLib.File file_TAG = TagLib.File.Create(firstSongNode.FullPath);
                if (file_TAG.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file_TAG.Tag.Pictures[0].Data.Data);
                    Image image = Image.FromStream(new MemoryStream(bin));
                    libraryAlbumArtBox.Image = image;
                }

                // If no image tags were found, look for an image file with a .jpg or .png extension in the album folder with LINQ
                else
                {
                    string albumFolder = Path.GetDirectoryName(firstSongNode.FullPath);
                    var imageFiles = Directory.EnumerateFiles(albumFolder, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".jpeg")).ToList();
                    if (imageFiles.Count > 0)
                    {
                        libraryAlbumArtBox.Image = Image.FromFile(imageFiles[0]);
                    }
                    else
                    {
                        libraryAlbumArtBox.Image = Resources.EmptyAlbumArtwork;
                    }
                }


                // Add the album node to the TreeView control
                var albumNode = new TreeNode(node.DisplayName);
                treeView.Nodes.Add(albumNode);

                // Add the first song to the TreeView control
                var firstSongTreeNode = new TreeNode(firstSongNode.DisplayName);
                albumNode.Nodes.Add(firstSongTreeNode);

                // Iterate over the rest of the songs and add them to the TreeView control
                foreach (var songNode in node.Children.Skip(1))
                {
                    var treeNode = new TreeNode(songNode.DisplayName);
                    albumNode.Nodes.Add(treeNode);
                }
                // Show the TreeView control and hide the songslistBox control
                treeView.ExpandAll();
                songslistView.Visible = false;
                albumPanel.Location = new Point(529, 49);
                albumPanel.Visible = true;
                albumPanel.BringToFront();
                libraryAlbumArtBox.Visible = true;
                treeView.Visible = true;
                backBox.Visible = true;
            }
        }
        private void OpenorPlay(string item, string itemType)
        {
            if (itemType.StartsWith("s"))
            {
                // Searching
                List<string> exceptions = new List<string> { "(Artist)", "(Album)", "(Song)" };
                // setting initial flag
                bool resultsFound = true;
                string nodeType;
                if (item.Contains(exceptions[2]))
                {
                    nodeType = "(Song)";
                }
                else if (item.Contains(exceptions[1]))
                {
                    nodeType = "(Album)";
                }
                else
                {
                    nodeType = "(Artist)";
                }
                // remove exceptions from searchText
                foreach (string exception in exceptions)
                {
                    item = item.Replace(exception, "").Trim();
                }
                foreach (FileSystemTreeNode node in rootNode.GetAllNodesExceptRoot())
                {
                    // song name, not filename
                    if (node.DisplayName == item && node.ObjectType == nodeType)
                    {
                        if (node.ObjectType == "(Song)")
                        {
                            // Play the song associated with the node
                            string songPath = node.FullPath;
                            WindowsMediaPlayer.URL = songPath;
                            Play(songPath);
                            openplayButton.Enabled = false;
                            resultsFound = true;
                            break; // Stop searching
                        }
                        else if (node.ObjectType == "(Album)")
                        {
                            ViewAlbum(rootNode.FindNodeByDisplayName(item, albumObj));
                            resultsFound = true;
                            break;
                        }
                        else if (node.ObjectType == "(Artist)")
                        {
                            // Not Implemented yet
                        }
                    }
                    else { resultsFound = false; }
                }
                if (resultsFound == false)
                {
                    MessageBox.Show("No Results Found.");
                    searchTextBox.Focus();
                }
                clearBox.Visible = true;
            }
            else
            {
                // View Playlist Songs
                if (playlistBox.SelectedIndex == 0)
                {
                    // Playlist == library, so reload the library
                    string startupPath = dbUtils.GetStartUpFolder();
                    LoadLibrary(startupPath);
                    //hide panel
                    albumPanel.Visible = false;
                }
                else
                {
                    // set bool
                    playlistViewing = true;
                    // Clear the listbox as well as the list holding the song titles
                    songslistView.Items.Clear();
                    paths.Clear();
                    //hide panel
                    albumPanel.Visible = false;
                    List<string> songs = dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Item1;
                    paths = dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Item2;
                    foreach (string song in songs)
                    {
                        // now add to both items that were just cleared
                        songslistView.Items.Add(song);
                        //paths.Add(rootNode.FindNodeByDisplayName(song, songObj).FullPath);
                    }
                    songslistView.Visible = true;
                }
            }
        }
        private void openplayButton_Click(object sender, EventArgs e)
        {
            string selectedPlaylist = playlistBox.SelectedItem?.ToString();
            string searchText = searchTextBox.Text?.ToString();
            string query;
            string queryType;
            if (!string.IsNullOrEmpty(searchText) && playlistBox.Text != string.Empty)
            {
                MessageBox.Show("Please select either a playlist or enter a search term before clicking, not both.");
                return;
            }
            else if (!string.IsNullOrEmpty(searchText))
            {
                query = searchText;
                queryType = "search";
            }
            else if (!string.IsNullOrEmpty(selectedPlaylist))
            {
                query = selectedPlaylist;
                queryType = "playlist";
            }
            else
            {
                MessageBox.Show("Please select a playlist or enter a search term.");
                return;
            }
            OpenorPlay(query, queryType);
        }
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
            {
                openplayButton.Enabled = true;
            }
            else
            {
                openplayButton.Enabled = false;
            }
        }
        private void newButton_Click(object sender, EventArgs e)
        {
            if (playlistBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a unique name in the playlists textbox to create a new playlist.", "No Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dbUtils.InsertPlaylist(playlistBox);
                MessageBox.Show($"Playlist '{playlistBox.Text}' Added");
                playlistBox.Text = string.Empty;
                GetPlaylists("Add");
            }
        }
        private void addtoplaylistButton_Click(object sender, EventArgs e)
        {
            if (songslistView.Visible == true)
            {
                if (songslistView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("No song was highlighted in the listview", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (playlistBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a playlist to add to from the playlist dropdown.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //insert song into playlist from library
                    if (!dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Item1.Contains(songslistView.SelectedItems[0].Text))
                        dbUtils.InsertSong(rootNode.FindNodeByDisplayName((string)songslistView.SelectedItems[0].Text, songObj), playlistBox.SelectedItem.ToString());
                    else
                        MessageBox.Show("Song is already in the selected playlist");
                }
            }
            else
            {
                //insert song into playlist from album
                if (!dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Item1.Contains(treeView.SelectedNode.Text))
                    dbUtils.InsertSong(rootNode.FindNodeByDisplayName(treeView.SelectedNode.Text, songObj), playlistBox.SelectedItem.ToString());
                else
                    MessageBox.Show("Song is already in the selected playlist");
            }
        }
        private void playlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            playlistIndexChanged++;
            if (playlistIndexChanged > 2) // Playlist index gets changed twice before the form loads, so check for this.
            {
                newButton.Enabled = false;
                openplayButton.Enabled = true;
                addtoplaylistButton.Enabled = true;
                clearButton.Enabled = true;
                clearButton.Visible = true;
            }
        }
        private void playlistBox_TextChanged(object sender, EventArgs e)
        {
            if (playlistBox.Text.Length > 0)
            {  // Show new button for playlist creation and hide clear button
                newButton.Enabled = true;
                clearButton.Visible = false;
            }
            else
            {   // Hide newButton, addtoplaylistbutton, and clearbutton
                newButton.Enabled = false;
                addtoplaylistButton.Enabled = false;
                clearButton.Visible = false;
            }
        }
        // Clears playlistBox values, toggles behaviors
        private void clearButton_Click(object sender, EventArgs e)
        {
            playlistBox.Text = string.Empty;
            playlistBox.ResetText();
            addtoplaylistButton.Enabled = false;
            openplayButton.Enabled = false;
            clearButton.Visible = false;
        }
        // Next 5 events handle the moving of the form + opening and closing ////
        private void topPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isMoving = true;
            movX = e.X;
            movY = e.Y;
        }
        private void topPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMoving) // based on bool flag
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        private void topPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isMoving = false;
        }
        private void minimizeBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void closeBox_Click(object sender, EventArgs e)
        {
            this.Close();
            MusicPlayerClass.Dispose();
        }
        // Hovering functionality for buttons and pictureboxes
        private void MouseHover(object sender, EventArgs e)
        {
            // hover on = 👆
            this.Cursor = Cursors.Hand;
        }
        private void MouseLeave(object sender, EventArgs e)
        {
            // Change cursor to default when hovering away
            this.Cursor = Cursors.Default;
        }
        private void eqButton_Click(object sender, EventArgs e)
        {
            equalizerWindow = new EQ(MusicPlayerClass.GetEqualizer());
            equalizerWindow.Show();
        }
        private void deviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            deviceIndexChanged++;
            if (deviceIndexChanged > 1) // device index gets changed once before the form loads, so check for this.
            {
                List<string> deviceId;
                string selectedDeviceName = deviceBox.SelectedItem.ToString();
                if (devices.Item1.ContainsKey(selectedDeviceName))
                {
                    deviceId = devices.Item1[selectedDeviceName];
                    if (deviceId.Count > 0)
                    {
                        // Set the default device to a new device based on its device ID 
                        var newDefaultDevice = MusicPlayerClass.GetSoundDevice(deviceId.First());
                        // Get the current position in milliseconds from WindowsMediaPlayer
                        double currentPositionMs = WindowsMediaPlayer.Ctlcontrols.currentPosition;
                        // find out by what percentage the carriage has moved
                        if (WindowsMediaPlayer.currentMedia != null)
                        {
                            double percent = currentPositionMs / TimeSpan.FromMilliseconds(WindowsMediaPlayer.currentMedia.duration).TotalMilliseconds;
                            // find out the current position of the track by multiplying the percentage by the total duration of the track
                            TimeSpan position = TimeSpan.FromMilliseconds(MusicPlayerClass.Length.TotalMilliseconds * percent);
                            MusicPlayerClass.Open(WindowsMediaPlayer.URL, newDefaultDevice);
                            MusicPlayerClass.Position = position;
                            MusicPlayerClass.Volume = volumeBar.Value;
                            WindowsMediaPlayer.Ctlcontrols.play();
                            MusicPlayerClass.Play();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No device IDs found for selected device name.");
                    }
                }
                else
                {
                    MessageBox.Show("Device not found.");
                }
            }
        }
        private void clearBox_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            clearBox.Visible = false;
        }

        // Opens GitHub link with default web browser
        private void githublinkBox_Click(object sender, EventArgs e)
        {
            utes.OpenLink("githublinkBox");
        }

        private void settingsBox_Click(object sender, EventArgs e)
        {
            Config newConfig = new Config();
            Config config = (Config)Application.OpenForms["Config"];
            if (config == null)
            {
                newConfig.Show();
                newConfig.GlobalConnectionString = this.GlobalConnectionString;
            }
            else
            {
                config.Close();
                config.Dispose();
                newConfig.Show();
                newConfig.GlobalConnectionString = this.GlobalConnectionString;
            }
        }
        private void playPauseButton_Click(object sender, EventArgs e)
        {
            if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                WindowsMediaPlayer.Ctlcontrols.pause();
                MusicPlayerClass.Pause();
                timer1.Stop();
            }
            else
            {
                WindowsMediaPlayer.Ctlcontrols.play();
                MusicPlayerClass.Play();
                timer1.Start();
            }
        }
        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            if (volumeBar.Value == 0)
            {
                volumeIconBox.Image = Properties.Resources.Mute;
            }
            else if (volumeBar.Value <= 25)
            {
                volumeIconBox.Image = Properties.Resources.Volume20;
            }
            else if (volumeBar.Value <= 75)
            {
                volumeIconBox.Image = Properties.Resources.Volume60;
            }
            else
            {
                volumeIconBox.Image = Properties.Resources.Volume;
            }
            MusicPlayerClass.Volume = volumeBar.Value;
        }
        private void volumeIconBox_Click(object sender, EventArgs e)
        {
            if (MusicPlayerClass.Volume == 0)
            {
                MusicPlayerClass.Volume = lastVolume;
                volumeBar.Value = lastVolume;
            }
            else
            {
                volumeBar.Value = 0;
            }
        }
        private void audioPosTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (stopUpdate)
            {
                double percent = audioPosTrackBar.Value / (double)audioPosTrackBar.Maximum;
                // find out by what percentage the carriage has moved
                // find out the current position of the track by multiplying the percentage by the total duration of the track
                TimeSpan position = TimeSpan.FromMilliseconds(MusicPlayerClass.Length.TotalMilliseconds * percent);
                MusicPlayerClass.Position = position;
                WindowsMediaPlayer.Ctlcontrols.currentPosition = position.TotalSeconds;
            }
        }
        private void audioPosTrackBar_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                stopUpdate = false;
                MusicPlayerClass.Volume = volumeBar.Value;
                MusicPlayerClass.Play();
                WindowsMediaPlayer.Ctlcontrols.play();
            }
        }
        private void audioPosTrackBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                stopUpdate = true;
                MusicPlayerClass.Volume = 0;
                WindowsMediaPlayer.Ctlcontrols.pause();
                MusicPlayerClass.Pause();
            }
        }
        // Stores last known volume 
        private void volumeBar_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            lastVolume = volumeBar.Value;
        }
        private void backBox_Click(object sender, EventArgs e)
        {
            albumPanel.Visible = false;
            treeView.Visible = false;
            backBox.Visible = false;
            //librarylistBox.Visible = true;
            songslistView.Visible = true;
        }
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                var clickedNode = rootNode.FindNodeByDisplayName(e.Node.Text, songObj);
                if (clickedNode.ObjectType == "(Song)")
                {
                    MusicPlayerClass.Pause();
                    WindowsMediaPlayer.Ctlcontrols.pause();
                    WindowsMediaPlayer.URL = clickedNode.FullPath;
                    Play(clickedNode.FullPath);
                }
            }
        }
        private void deviceBox_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer.Ctlcontrols.pause();
            MusicPlayerClass.Pause();
        }
        private void playlistBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Mutes the annoying windows beep sound
                openplayButton.PerformClick();
            }
        }
        private void albumArtBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowsMediaPlayer.currentMedia != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        {
                            rightClickMenuStrip.Width = 130;
                            rightClickMenuStrip.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                        }
                        break;
                }
            }
        }
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            albumArtBox.Hide();
            showToolStripMenuItem.Checked = false;
            hideToolStripMenuItem.Checked = true;
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool albumArtFound = false;
            string currentSongPath = WindowsMediaPlayer.currentMedia.sourceURL;
            var currentNode = rootNode.FindNodeByFullPath(currentSongPath, songObj);
            TagLib.File file_TAG = TagLib.File.Create(currentNode.FullPath);
            if (file_TAG.Tag.Pictures.Length >= 1)
            {
                var bin = file_TAG.Tag.Pictures[0].Data.Data;
                Image image = Image.FromStream(new MemoryStream(bin));
                albumArtBox.Image = image;
            }
            else if (file_TAG.Tag.Pictures.Length == 0)
            {
                foreach (string path in Directory.EnumerateFiles(currentNode.Parent.FullPath))
                {
                    if (Path.GetExtension(path) == ".jpg" || Path.GetExtension(path) == ".png")
                    {
                        Bitmap albumArtBitmap = new Bitmap(Image.FromFile(path));
                        albumArtBox.Image = albumArtBitmap;
                        albumArtBox.Visible = true;

                        //// Set the artwork of the song to the found file
                        SetArtwork(currentNode.FullPath, path);

                        albumArtFound = true; // flag that an album art file was found
                        break;
                    }
                }

                if (!albumArtFound) // set album art box to invisible if no file was found
                {
                    albumArtBox.Image = Resources.EmptyAlbumArtwork;
                    if (albumVisible == "True")
                    {
                        albumArtBox.Visible = true;
                    }
                }
            }
            albumArtBox.Visible = true;
            hideToolStripMenuItem.Checked = false;
            showToolStripMenuItem.Checked = true;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAlbum(rootNode.FindNodeByFullPath(WindowsMediaPlayer.URL, songObj).Parent);
        }
        private void WindowsMediaPlayer_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            if (WindowsMediaPlayer.currentMedia != null && WindowsMediaPlayer.fullScreen == false)
            {
                switch (e.nButton)
                {
                    case 2:
                        {   
                            if (albumArtBox.Visible == true)
                            {
                                showToolStripMenuItem.Checked = true;
                                hideToolStripMenuItem.Checked = false;
                            }
                            else
                            {
                                showToolStripMenuItem.Checked = false;
                                hideToolStripMenuItem.Checked = true;
                            }
                            rightClickMenuStrip.Width = 170;
                            rightClickMenuStrip.Show(this, new Point(e.fX, e.fY));//places the menu at the pointer position
                        }
                        break;
                }
            }
        }
        private void ToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            // highligh gray like the other toolstrip
        }
        private void MusicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (config.encryptAfterExit != null && config.encryptAfterExit == "true")
            {
                foreach (FileSystemTreeNode fstn in rootNode.GetAllNodesExceptRoot())
                {
                    if (fstn.NodeType == NodeType.File)
                    {
                        // encrypt contents
                        //string password = "myPassword123"; // Replace with user's password
                        //byte[] salt = new byte[32]; // Generate a random salt
                        //using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                        //{
                        //    rng.GetBytes(salt);
                        //}
                        //byte[] key = new byte[32]; // Generate key using PBKDF2
                        //using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                        //{
                        //    key = pbkdf2.GetBytes(32);
                        //}
                        //// Encrypt the file using AES
                        //using (Aes aes = Aes.Create())
                        //{
                        //    aes.Key = key;
                        //    using (FileStream inputFileStream = new FileStream("inputFile.txt", FileMode.Open, FileAccess.Read))
                        //    {
                        //        using (FileStream outputFileStream = new FileStream("encryptedFile.txt", FileMode.Create, FileAccess.Write))
                        //        {
                        //            using (ICryptoTransform encryptor = aes.CreateEncryptor())
                        //            {
                        //                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                        //                {
                        //                    inputFileStream.CopyTo(cryptoStream);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
            }
            else
            {
                return;
            }
        }
        private void MusicPlayer_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int currentIndex;
            if (e.Button == MouseButtons.Left)
            {
                if (songslistView.Items.Count > 0)
                {
                    if (playlistViewing == false)
                    {
                        WindowsMediaPlayer.URL = paths[songslistView.SelectedItems[0].Index];
                    }
                    else
                    {
                        //currentIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(songslistView.SelectedItems[0].Text, songObj).FullPath));
                        // The index was not found
                        currentIndex = songslistView.SelectedItems[0].Index;
                        WindowsMediaPlayer.URL = paths[currentIndex];
                    }

                    // Check for file extension
                    string extension = Path.GetExtension(WindowsMediaPlayer.URL);
                    string[] videoFormats = Utes.videoFormats;
                    string[] audioFormats = Utes.audioFormats;

                    if (audioFormats.Contains(extension))
                    {
                        if (albumVisible == "True")
                        {
                            GetAlbumArt();
                        }
                    }
                    else if (videoFormats.Contains(extension))
                    {
                        albumArtBox.Visible = false;
                    }
                    
                    // Set the focus back to the ListView to avoid a bug where the selected item doesn't display correctly
                    songslistView.Focus();

                    // Start playing the selected song
                    Play(WindowsMediaPlayer.URL);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDoubleClick)
            {
                if (songslistView.SelectedItems.Count > 0)
                {
                    int currentIndex = songslistView.SelectedItems[0].Index;
                    //play music 
                    //only plays when double clicked, a single click only changes the listbox so the user can add the selected song to a queue or playlist
                    WindowsMediaPlayer.URL = paths[currentIndex];
                    Play(WindowsMediaPlayer.URL);
                    // reset the flag
                    listBoxDoubleClick = false;
                }
            }
            else
            {
                if (playlistBox.SelectedIndex != -1)
                {
                    addtoplaylistButton.Enabled = true;
                }
                else
                {
                    addtoplaylistButton.Enabled = false;
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Mutes the annoying windows beep sound

                //// Play the selected song 
                int currentIndex = songslistView.SelectedItems[0].Index;

                // pause/play music 
                MusicPlayerClass.Pause();
                WindowsMediaPlayer.Ctlcontrols.pause();
                WindowsMediaPlayer.URL = paths[currentIndex];
                Play(WindowsMediaPlayer.URL);

                //// Create a new thread to play the media file
                //Thread thread = new Thread(() =>
                //{
                //    // Set the URL of the media file
                //    WindowsMediaPlayer.URL = clickedNode.FullPath;

                //    // Call the Play method
                //    Play(WindowsMediaPlayer.URL);

                //    // Update the UI as needed (e.g., disable buttons)
                //    this.Invoke((MethodInvoker)delegate
                //    {
                //        openplayButton.Enabled = false;
                //    });
                //});

                //// Start the thread
                //thread.Start();
            }
        }
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Mutes the annoying windows beep sound

                ////// Play the selected song 
                var clickedNode = rootNode.FindNodeByDisplayName(treeView.SelectedNode.Text, songObj);
                MusicPlayerClass.Pause();
                WindowsMediaPlayer.Ctlcontrols.pause();
                WindowsMediaPlayer.URL = clickedNode.FullPath;
                Play(clickedNode.FullPath);

                //// Create a new thread to play the media file
                //Thread thread = new Thread(() =>
                //{
                //    // Set the URL of the media file
                //    WindowsMediaPlayer.URL = clickedNode.FullPath;

                //    // Call the Play method
                //    Play(WindowsMediaPlayer.URL);

                //    // Update the UI as needed (e.g., disable buttons)
                //    this.Invoke((MethodInvoker)delegate
                //    {
                //        openplayButton.Enabled = false;
                //    });
                //});

                //// Start the thread
                //thread.Start();
            }
        }

        private void hideThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightClickMenuStrip.Hide();
        }

        private void fullScreenModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer.fullScreen = true;
        }

        private void downloadsButton_Click(object sender, EventArgs e)
        {
            Downloads newDownloads = new Downloads();
            Downloads downloads = (Downloads)Application.OpenForms["Downloads"];
            if (downloads == null)
            {
                newDownloads.Show();
            }
            else
            {
                downloads.Close();
                downloads.Dispose();
                newDownloads.Show();
            }
        }

        private void NextVisualization()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Hide();

            // Get the center point of the button in screen coordinates
            Point center = vizButton.PointToScreen(new Point(vizButton.Width / 2, vizButton.Height / 2));

            // Set the mouse cursor position to the center of the button
            Cursor.Position = center;

            // Store old position
            Point oldPosition = new Point(Cursor.Position.X, Cursor.Position.Y);
            Cursor.Current = currentCursor;
            Cursor.Position = new Point(Cursor.Position.X - 225, Cursor.Position.Y - 90);
            utes.DoMouseClick();
            Cursor.Position = oldPosition;

            Cursor.Current = currentCursor;
            Cursor.Show();
        }

        private void vizButton_Click(object sender, EventArgs e)
        {
            NextVisualization();
        }
    }
}