
// Author: Weston Evans/mcworkaholic on Github
// 
// Application purpose:  This application was intended to become a decently
// functioning music player with a mix of old and new technologies.
// In the future I plan on adding archiving functionality with a few Python scripts, with the ability to sync with services such as Soundcloud, Spotify, and Youtube Music.

// The design is pretty intuitive, all you have to do is first create an account(this is what keeps track of each profile's playlists)
// and then double click on any song in the listbox to get started.
// In the future I'm going to allow the user to define what folder is opened up at the start.  ✔️

// Each icon that does not have text, has a tooltip describing what the action does. 
// The search textbox is autopopulated with every node contained in the tree besides the root from FileSystemTreeNode.cs,
// to allow the user to make fast searches and either play a song or open an album.

// The audio is controlled from MusicPlayer.cs, and the audio/video is synced with the visuals from the WindowsMediaPlayer component(hacky I know).

// You will notice that some functionalities do not work, and that some refactoring can be done. I plan on fixing that for the final project. 

using AxWMPLib;
using CSCore.CoreAudioAPI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using Cursors = System.Windows.Forms.Cursors;

namespace Music_Player
{
    public partial class MusicPlayer : Form
    {
        private MusicPlayerClass MusicPlayerClass = new MusicPlayerClass(); //создание объекта музыкального плеера

        private GlobalKeyboardHook _globalKeyboardHook; // initiate

        private DBUtils dbUtils = new DBUtils();

        EQ equalizerWindow;

        // tracking for queuing
        int currentSelectedIndex;

        // for moving borderless form
        int movX, movY;
        bool isMoving;

        // initial states
        bool shuffleButtonClicked = false;
        bool listBoxDoubleClick = false;
        bool stopUpdate;
        bool playlistViewing = false;
        int shuffleButtonClickCount = 0;
        int playlistIndexChanged = 0;
        int deviceIndexChanged = 0;
        int lastVolume = 85;

        //Create Global Variables of String Type Array to save the path of the track 
        List<string> paths;

        List<int> trackIndexes = new List<int>();
        int atIndex = 0;

        // variables that are set on formload event
        string connectionString;
        (Dictionary<string, List<string>>, string) devices;

        // Modified throughout use
        List<int> queueList = new List<int>();

        // Create a dictionary to cache album artwork URLs
        //Dictionary<string, string> albumArtCache = new Dictionary<string, string>();

        // for storing initial root node
        FileSystemTreeNode rootNode;

        string songObj = "(Song)";
        string albumObj = "(Album)";
        string artistObj = "(Artist)";


        // Fullscreen capabilities
        private bool _isFullscreenToggle = false;
        public bool IsFullscreen
        {
            get { return _isFullscreenToggle; }

            set { _isFullscreenToggle = value; }
        }
        private Size _previousVideoContainerSize = new Size();

        public MusicPlayer()
        {
            InitializeComponent();
        }

        public string GlobalConnectionString
        {
            get { return connectionString; }
        }
        private void buttonHook_Click(object sender, EventArgs e)
        {
            // Hooks only into specified Keys (here "Space").
            _globalKeyboardHook = new GlobalKeyboardHook(new Keys[] { Keys.Space });

            // Hooks into all keys.
            //_globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            // EDT: No need to filter for VkSnapshot anymore. This now gets handled
            // through the constructor of GlobalKeyboardHook(...).
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                // Now you can access both, the key and virtual code
                Keys loggedKey = e.KeyboardData.Key;
                int loggedVkCode = e.KeyboardData.VirtualCode;

                this.ActiveControl = null;
                MusicPlayerClass.Volume = 0;
                volumeBar.Value = 0;
            }
            else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                volumeBar.Value = lastVolume;
                MusicPlayerClass.Volume = volumeBar.Value;
            }
        }

        private void songslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDoubleClick)
            {
                //play music 
                //only plays when double clicked, a single click only changes the listbox so the user can add the selected song to a queue or playlist
                WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                Play(WindowsMediaPlayer.URL);

                // reset the flag
                listBoxDoubleClick = false;
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
        private void songslistBox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int currentIndex;
            if (e.Button == MouseButtons.Left)
            //play music at the selected index
            {
                if (librarylistBox.Items.Count > 0)
                {
                    if (playlistViewing == false)
                    {
                        WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    }
                    else
                    {
                        currentIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(librarylistBox.SelectedItem.ToString(), songObj).FullPath));
                        WindowsMediaPlayer.URL = paths[currentIndex];
                    }
                    listBoxDoubleClick = true;
                    Play(WindowsMediaPlayer.URL);
                    // reset the flag
                    listBoxDoubleClick = false;
                }
            }
            else
            {
                MessageBox.Show("No device IDs found for selected device name.");
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
                    if (librarylistBox.SelectedIndex < librarylistBox.Items.Count - 1)
                    {
                        // sets the next consecutive song to be played next
                        librarylistBox.SelectedIndex++;
                    }
                    else
                    {
                        // if at the end of the selected list, jump to the first song (index 0 of current list of songs)
                        librarylistBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    if (atIndex == trackIndexes.Count - 1)
                    {
                        trackIndexes.Clear();
                        shuffle();
                        atIndex = 0;
                    }

                    atIndex++;
                    librarylistBox.SelectedIndex = trackIndexes[atIndex];
                    WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    Play(WindowsMediaPlayer.URL);
                }
            }
            playTimer.Enabled = false;
        }

        private void previousBox_Click(object sender, EventArgs e)
        {
            if (shuffleButtonClicked == false)
            {
                if (librarylistBox.SelectedIndex > 0)
                {
                    // Go back 1 song from the current index of the library
                    int currentIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(librarylistBox.SelectedItem.ToString(), songObj).FullPath));
                    librarylistBox.SelectedIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                    Play(paths[currentIndex - 1]);
                    librarylistBox.SelectedIndex--;
                }
                else if (librarylistBox.SelectedIndex == 0)
                {
                    // Go back to the start/top of the library
                    librarylistBox.SelectedIndex = librarylistBox.Items.Count - 1;
                    Play(paths[librarylistBox.SelectedIndex]);
                }
            }
            else if (shuffleButtonClicked == true)
            {

                if (atIndex == trackIndexes.Count - 1)
                {
                    trackIndexes.Clear();
                    shuffle();
                    atIndex = 0;
                }

                atIndex++;
                librarylistBox.SelectedIndex = trackIndexes[atIndex];
                WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                Play(WindowsMediaPlayer.URL);

                //// while loop ensures that the same song that is currently playing is not shuffled to next
                //while (MusicPlayerClass.Shuffle(librarylistBox) != paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL)))
                //{
                //    librarylistBox.SelectedIndex = MusicPlayerClass.Shuffle(librarylistBox);
                //    Play(paths[librarylistBox.SelectedIndex]);
                //    break;
                //}
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
                    if (librarylistBox.SelectedIndex == -1)
                    {
                        int currentIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                        if (paths.Count - 1 == currentIndex)
                        {
                            librarylistBox.SelectedIndex = 0;
                        }
                        else
                        {
                            librarylistBox.SelectedIndex = currentIndex + 1;
                        }
                        WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    }
                    else if (librarylistBox.SelectedIndex < librarylistBox.Items.Count - 1)
                    {
                        int currentIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                        WindowsMediaPlayer.URL = paths[currentIndex + 1];
                        librarylistBox.SelectedIndex = paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL));
                    }
                    else if (librarylistBox.SelectedIndex == librarylistBox.Items.Count - 1)
                    {
                        librarylistBox.SelectedIndex = 0;
                        WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    }
                    Play(WindowsMediaPlayer.URL);
                }
                else if (shuffleButtonClicked == true)
                {
                    if (atIndex == trackIndexes.Count - 1)
                    {
                        trackIndexes.Clear();
                        shuffle();
                        atIndex = 0;
                    }

                    atIndex++;
                    librarylistBox.SelectedIndex = trackIndexes[atIndex];
                    WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    Play(WindowsMediaPlayer.URL);

                    //// while loop ensures that the same song that is currently playing is not shuffled to next
                    //while (MusicPlayerClass.Shuffle(librarylistBox) != paths.FindIndex(path => path.Contains(WindowsMediaPlayer.URL)))
                    //{
                    //    librarylistBox.SelectedIndex = MusicPlayerClass.Shuffle(librarylistBox);
                    //    WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                    //    Play(WindowsMediaPlayer.URL);
                    //    break;
                    //}
                }
            }
        }
        private void shuffle()
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < librarylistBox.Items.Count; i++)
            {
                indexList.Add(i);
            }
            indexList.Remove(librarylistBox.SelectedIndex);
            trackIndexes = MusicPlayerClass.Shuffle(indexList);
        }
        private void shuffleButton_Click(object sender, EventArgs e)
        {
            // each button click increments the global counter
            // this is  a method of turning off and on shuffle mode, odd clicks turn it on, even clicks turn it off.
            // if the current click count divided by 2 yields a remainder of 1,
            // shuffleButtonClickCount == odd and shuffle mode == off
            shuffleButtonClickCount++;
            shuffleButtonClicked = (shuffleButtonClickCount % 2 == 1);
            if (shuffleButtonClickCount % 2 == 1)
            {
                //set button text as indicator
                // shuffle mode ON
                shuffleButton.Text = "🔀 Shuffle";
                shuffle();
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
            if (treeView.Visible == false)
            {
                currentSelectedIndex = librarylistBox.SelectedIndex;
            }
            else
            {
                if (librarylistBox.Visible == true)
                {
                    currentSelectedIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(librarylistBox.SelectedItem.ToString(), songObj).FullPath));
                }
                else
                {
                    currentSelectedIndex = paths.FindIndex(path => path.Contains(rootNode.FindNodeByDisplayName(treeView.SelectedNode.Text, songObj).FullPath));
                }
            }
            queueList.Add(currentSelectedIndex);
            queueLabel.Text = $"Queued Songs: {queueList.Count}";
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

            string currentSongPath = WindowsMediaPlayer.currentMedia.sourceURL;
            var currentNode = rootNode.FindNodeByFullPath(currentSongPath, songObj);
            try
            {
                TagLib.File file_TAG = TagLib.File.Create(currentNode.FullPath);
                if (file_TAG.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file_TAG.Tag.Pictures[0].Data.Data);
                    Image image = Image.FromStream(new MemoryStream(bin));
                    albumArtBox.Image = image;
                }
            }
            catch (Exception ex) { MessageBox.Show("An unexpected error has occurred: " + ex.Message); }
        }
        public void LoadLibrary(string path)
        {
            // designate initial root for tree traversal
            rootNode = FileSystemTreeNode.BuildTree(path);

            if (librarylistBox.Items.Count > 0)
            {
                // clear list, listbox and reset visibilities
                librarylistBox.Items.Clear();
                paths.Clear();
                librarylistBox.Visible = true;
                albumPanel.Visible = false;
            }
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
                            // Construct the song name and artist name string
                            string songName = greatGrandChild.DisplayName;
                            string albumName = greatGrandChild.Parent.DisplayName;
                            string artistName = greatGrandChild.Parent.Parent.DisplayName;
                            string itemText = songName; //{objectTypeSeparator}{artistName}";// +{objectTypeSeparator}{albumName}{objectTypeSeparator}";

                            // Add the string to the ListBox control
                            librarylistBox.Items.Add(itemText);
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

            // Populate Device dropdown with the system's devices, play through default
            LoadDevices();

            // Remove setup labels
            noLibraryLabel.Visible = false;
            configureLabel.Visible = false;
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
            WindowsMediaPlayer.settings.volume = 0;
            playPauseButton.Image = Properties.Resources.PausePlay;
            // Add a delegate for the MediaChange event.
            WindowsMediaPlayer.MediaChange += new AxWMPLib._WMPOCXEvents_MediaChangeEventHandler(WindowsMediaPlayer_MediaChange);
            // set visualization preset from windows media player
            SetCurrentEffectPreset(4);
            connectionString = dbUtils.connectionString;
            string startupPath = dbUtils.GetStartUpFolder();

            if (startupPath != string.Empty)
            {
                LoadLibrary(startupPath);
                AddSearchSource();
                LoadDevices();

                // Add Playlists
                GetPlaylists("Load");
                librarylistBox.Visible = true;
                albumPanel.Visible = false;
                librarylistBox.HorizontalScrollbar = true;
                searchTextBox.Enabled = true;
                playlistBox.Enabled = true;
                noLibraryLabel.Visible = false;
                configureLabel.Visible = false;
            }
            else
            {
                librarylistBox.HorizontalScrollbar = false;
                searchTextBox.Enabled = false;
                playlistBox.Enabled = false;
                noLibraryLabel.Visible = true;
                configureLabel.Visible = true;
            }
        }
        private void GetPlaylists(string action)
        {
            int selectedIndex = playlistBox.SelectedIndex; // Store the selected index
            playlistBox.DataSource = dbUtils.GetPlaylists();
            playlistBox.SelectedIndex = selectedIndex; // Set the selected index again

            if (action == "Add")
            {
                playlistBox.SelectedIndex = playlistBox.Items.Count - 1;
            }
        }
        private void AddSearchSource()
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
                librarylistBox.Visible = false;
                albumPanel.Visible = true;
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

                            //searchTextBox.Clear();
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
                    librarylistBox.Items.Clear();
                    paths.Clear();

                    //hide panel
                    albumPanel.Visible = false;

                    List<string> songs = dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString());
                    foreach (string song in songs)
                    {
                        // now add to both items that were just cleared
                        librarylistBox.Items.Add(song);
                        paths.Add(rootNode.FindNodeByDisplayName(song, songObj).FullPath);
                    }
                    librarylistBox.Visible = true;
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
        private void Form1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
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
            if (librarylistBox.Visible == true)
            {
                if (librarylistBox.SelectedIndex == -1)
                {
                    MessageBox.Show("No song was highlighted in the listbox", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (playlistBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a playlist to add to from the playlist dropdown.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //insert song into playlist from library
                    if (!dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Contains(librarylistBox.SelectedItem))
                        dbUtils.InsertSong(rootNode.FindNodeByDisplayName((string)librarylistBox.SelectedItem, songObj), playlistBox.SelectedItem.ToString());
                    else
                        MessageBox.Show("Song is already in the selected playlist");
                }
            }
            else
            {
                //insert song into playlist from album
                if (!dbUtils.GetPlaylistSongs(Program.user_id, playlistBox.SelectedItem.ToString()).Contains(treeView.SelectedNode.Text))
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
            if (searchTextBox.Text.Length == 0 && playlistBox.SelectedIndex == -1)
            {
                this.ActiveControl = null;
            }
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

        private void songslistBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Play the selected song while listbox is active control
                WindowsMediaPlayer.URL = paths[librarylistBox.SelectedIndex];
                Play(WindowsMediaPlayer.URL);
            }
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
                            this.ActiveControl = null;
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
            this.ActiveControl = null;
        }
        public void OpenLink(string link)
        {
            string target;
            switch (link)
            {
                case "githublinkBox":
                    target = "https://github.com/mcworkaholic/IS345-G5-Music-Player";
                    break;
                case "SoundCloud":
                    target = "https://soundcloud.com/";
                    break;
                case "Spotify":
                    target = "https://open.spotify.com/";
                    break;
                default:
                    target = "https://music.youtube.com/";
                    break;
            }
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }

        // Opens GitHub link with default web browser
        private void githublinkBox_Click(object sender, EventArgs e)
        {
            OpenLink("githublinkBox");
        }

        private void VideoContainer_KeyUp(object sender, _WMPOCXEvents_KeyUpEvent e)
        {
            if (IsFullscreen && e.nKeyCode == (short)Keys.Escape)
            {
                //FullscreenToggle();
            }
        }

        //private void FullscreenToggle()
        //{
        //    this.IsFullscreen = !this.IsFullscreen;
        //    if (this.IsFullscreen)
        //    {
        //        _previousVideoContainerSize = new Size(WindowsMediaPlayer.Width, WindowsMediaPlayer.Height);
        //        Screen screen = Screen.PrimaryScreen;
        //        Rectangle area = screen.Bounds;
        //        WindowsMediaPlayer.Width = Screen.PrimaryScreen.Bounds.Width;
        //        WindowsMediaPlayer.Height = Screen.PrimaryScreen.Bounds.Height;
        //        this.WindowState = FormWindowState.Maximized;
        //    }
        //    else
        //    {
        //        WindowsMediaPlayer.Width = _previousVideoContainerSize.Width;
        //        WindowsMediaPlayer.Height = _previousVideoContainerSize.Height;
        //    }
        //}


        private void maximizeBox_Click(object sender, EventArgs e)
        {
            // To be implemented
            // FullscreenToggle();
        }

        private void settingsBox_Click(object sender, EventArgs e)
        {
            Config config = new Config();
            config.GlobalConnectionString = this.GlobalConnectionString;
            config.Show();
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
            librarylistBox.Visible = true;
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

        private void albumArtBox_Click(object sender, EventArgs e)
        {
            ViewAlbum(rootNode.FindNodeByFullPath(WindowsMediaPlayer.URL, songObj).Parent);
        }

        private void albumArtBox_DoubleClick(object sender, EventArgs e)
        {
            albumArtBox.Visible = false;
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

        private void vizButton_Click(object sender, EventArgs e)
        {
            // To be implemented 
            // not working, would like to be able to change the visualization live while the application is running
            //int start = 0;
            //SetCurrentEffectPreset();
        }
    }
}
