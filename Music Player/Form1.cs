
// Author: Weston Evans/mcworkaholic on Github
// 
// Application purpose: [ This application was intended to become a decently
// functioning music player with a mix of old and new technologies.
// In the future I plan on adding archiving functionality with a few Python scripts, with the ability to sync with services such as Soundcloud, Spotify, and Youtube Music.

// The design is pretty intuitive, all you have to do is first create an account(this is what keeps track of each profile's playlists)
// and then double click on any song in the listbox to get started. In the future I'm going to allow the user to define what folder is opened up at the start.

// Each icon that does not have text, has a tooltip describing what the action does. 
// The search textbox is autopopulated with every node contained in the tree besides the root from TreeNode.cs,
// to allow the user to make fast searches and either play a song or open an album.

// The audio is controlled from MusicPlayer.cs, and the audio/video is synced with the visuals from the WindowsMediaPlayer component(hacky I know).

// You will notice that some functionalities do not work, and that some refactoring can be done. I plan on fixing that for the next project. ]

using AxWMPLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Form1 : Form
    {
        private MusicPlayer MusicPlayer = new MusicPlayer();//создание объекта музыкального плеера
        private dbUtils dbUtils = new dbUtils();
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
        int shuffleButtonClickCount = 0;
        int playlistIndexChanged = 0;
        int deviceIndexChanged = 0;
        int lastVolume = 85;

        //Create Global Variables of String Type Array to save the titles or name of the //Tracks and path of the track 
        string[] files, paths;

        // variables that are set on formload event
        string connectionString;
        (Dictionary<string, List<string>>, string) devices;

        // Separates song from artist with a unique identifier on form load for display purposes
        Guid objectTypeSeparator;

        // Modified throughout use
        List<int> queueList = new List<int>();

        // Create a dictionary to cache album artwork URLs
        Dictionary<string, string> albumArtCache = new Dictionary<string, string>();

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

        public Form1()
        {
            InitializeComponent();

            // key events for pressing "ENTER" key while in the songslistbox, while it is the active control
            songslistBox.PreviewKeyDown += songsListBox_PreviewKeyDown;
            songslistBox.KeyDown += new KeyEventHandler(songslistBox_KeyDown);
        }

        public string GlobalConnectionString
        {
            get { return connectionString; }
        }
        public override void Refresh()
        {
            if (MusicPlayer.PlaybackState == CSCore.SoundOut.PlaybackState.Playing)
            {
                MusicPlayer.Pause();
            }
            MusicPlayer.Dispose();
            this.Controls.Clear();
            this.InitializeComponent();
            this.form_Load();
        }
        private void songslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDoubleClick)
            {
                //play music 
                //only plays when double clicked, a single click only changes the listbox so the user can add the selected song to a queue or playlist
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
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
        private void songslistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            //play music at the selected index
            {
                if (songslistBox.Items.Count > 0)
                {
                    listBoxDoubleClick = true;
                    WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
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
                    if (songslistBox.SelectedIndex < files.Length - 1)
                    {
                        // sets the next consecutive song to be played next
                        songslistBox.SelectedIndex++;
                    }
                    else
                    {
                        // if at the end of the selected list, jump to the first song (index 0 of current list of songs)
                        songslistBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    // while loop ensures that the same song that is currently playing is not shuffled to next
                    int randomizedIndex;
                    do
                    {
                        randomizedIndex = MusicPlayer.Shuffle(songslistBox);
                    } while (randomizedIndex == songslistBox.SelectedIndex);

                    songslistBox.SelectedIndex = randomizedIndex;
                }
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                Play(WindowsMediaPlayer.URL);
            }
            playTimer.Enabled = false;
        }

        private void previousBox_Click(object sender, EventArgs e)
        {
            if (shuffleButtonClicked == false)
            {
                if (songslistBox.SelectedIndex > 0)
                {
                    // Go back 1 song from the current index of the library
                    int currentIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
                    songslistBox.SelectedIndex = Array.IndexOf(paths, WindowsMediaPlayer.URL);
                    Play(paths[currentIndex - 1]);
                    songslistBox.SelectedIndex--;
                }
                else if (songslistBox.SelectedIndex == 0)
                {
                    // Go back to the start/top of the library
                    songslistBox.SelectedIndex = songslistBox.Items.Count - 1;
                    Play(paths[songslistBox.SelectedIndex]);
                }
            }
            else if (shuffleButtonClicked == true)
            {
                // while loop ensures that the same song that is currently playing is not shuffled to next
                while (MusicPlayer.Shuffle(songslistBox) != Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL))
                {
                    songslistBox.SelectedIndex = MusicPlayer.Shuffle(songslistBox);
                    Play(paths[songslistBox.SelectedIndex]);
                    break;
                }
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
                    var currentDevice = MusicPlayer.GetSoundDevice(deviceId.First());
                    WindowsMediaPlayer.URL = songPath;
                    MusicPlayer.Open(songPath, currentDevice);
                    MusicPlayer.Play();
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
                    if (songslistBox.SelectedIndex == -1)
                    {
                        int currentIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
                        if (paths.GetUpperBound(0) == currentIndex)
                        {
                            songslistBox.SelectedIndex = 0;
                        }
                        else
                        {
                            songslistBox.SelectedIndex = currentIndex + 1;
                        }
                        WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                    }
                    else if (songslistBox.SelectedIndex < files.Length - 1)
                    {
                        int currentIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
                        WindowsMediaPlayer.URL = paths[currentIndex + 1];
                        songslistBox.SelectedIndex = Array.IndexOf(paths, WindowsMediaPlayer.URL);
                    }
                    else if (songslistBox.SelectedIndex == files.Length - 1)
                    {
                        songslistBox.SelectedIndex = 0;
                        WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                    }
                    Play(WindowsMediaPlayer.URL);
                }
                else if (shuffleButtonClicked == true)
                {
                    // while loop ensures that the same song that is currently playing is not shuffled to next
                    while (MusicPlayer.Shuffle(songslistBox) != Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL))
                    {
                        songslistBox.SelectedIndex = MusicPlayer.Shuffle(songslistBox);
                        WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                        Play(WindowsMediaPlayer.URL);
                        break;
                    }
                }
            }
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
            }
            else
            {
                //set button text as indicator
                // shuffle mode OFF
                shuffleButton.Text = "Shuffle";
            }
        }
        private void queueButton_Click(object sender, EventArgs e)
        {
            queueLabel.Visible = true;
            clearQueueButton.Visible = true;
            if (treeView.Visible == false)
            {
                currentSelectedIndex = songslistBox.SelectedIndex;
            }
            else
            {
                currentSelectedIndex = Array.IndexOf(paths, rootNode.FindNodeByDisplayName(treeView.SelectedNode.Text, songObj).FullPath);
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
            MusicPlayer.Volume = 0;
            MusicPlayer.Volume = volumeBar.Value;

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
        private void LoadMusic(string path)
        {
            objectTypeSeparator = Guid.NewGuid();
            rootNode = FileSystemTreeNode.BuildTree(path);

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
                            string itemText = $"{songName}{objectTypeSeparator}{artistName}";// +{objectTypeSeparator}{albumName}{objectTypeSeparator}";

                            // Add the string to the ListBox control
                            songslistBox.Items.Add(itemText);
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
            // Needed to convert from list to array to restore original functionality
            paths = filePaths.ToArray();
            files = fileNames.ToArray();
        }
        private (Dictionary<string, List<string>>, string) GetAudioDevices()
        {
            var devices = new Dictionary<string, List<string>>();

            // Retrieve all active audio devices
            foreach (var dev in MusicPlayer.EnumerateWasapiDevices())
            {
                if (!devices.ContainsKey(dev.FriendlyName))
                {
                    devices.Add(dev.FriendlyName, new List<string>());
                }
                // Dictionary holds device's friendly names as keys and IDs as values
                devices[dev.FriendlyName].Add(dev.DeviceID.ToString());
            }

            // get the default audio endpoint
            string defaultDeviceName = MusicPlayer.GetDefaultSoundDevice().FriendlyName;
            return (devices, defaultDeviceName);
        }
        public void form_Load()
        {
            // made public to be called for refresh

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
                LoadMusic(startupPath);
                AddSearchSource();

                // Add available audio devices
                devices = GetAudioDevices();
                foreach (var device in devices.Item1)
                {
                    deviceBox.Items.Add(device.Key);
                }

                // Set the initial selected index based on the default audio endpoint
                deviceBox.SelectedIndex = deviceBox.FindStringExact(devices.Item2);

                // Add Playlists
                GetPlaylists("Load");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form_Load();
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
                songslistBox.Visible = false;
                libraryPanel.Visible = true;
                libraryAlbumArtBox.Visible = true;
                treeView.Visible = true;
                backBox.Visible = true;
            }
        }

        private void OpenorPlay(string searchText)
        {
            List<string> exceptions = new List<string> { "(Artist)", "(Album)", "(Song)" };
            // setting initial flag
            bool resultsFound = true;
            string nodeType;
            if (searchText.Contains(exceptions[2]))
            {
                nodeType = "(Song)";
            }
            else if (searchText.Contains(exceptions[1]))
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
                searchText = searchText.Replace(exception, "").Trim();
            }
            foreach (FileSystemTreeNode node in rootNode.GetAllNodesExceptRoot())
            {
                // song name, not filename
                if (node.DisplayName == searchText && node.ObjectType == nodeType)
                {
                    if (node.ObjectType == "(Song)")
                    {
                        // Play the song associated with the node
                        string songPath = node.FullPath;
                        WindowsMediaPlayer.URL = songPath;
                        Play(songPath);

                        searchTextBox.Clear();
                        openplayButton.Enabled = false;
                        resultsFound = true;
                        break; // Stop searching
                    }
                    else if (node.ObjectType == "(Album)")
                    {
                        ViewAlbum(rootNode.FindNodeByDisplayName(searchText, albumObj));
                        resultsFound = true;
                        break;
                    }
                    else if (node.ObjectType == "(Artist)")
                    {
                        //Implement
                    }
                }

                else { resultsFound = false; }
            }
            if (resultsFound == false)
            {
                MessageBox.Show("No Results Found.");
                clearBox.Visible = true;
                searchTextBox.Focus();
            }
        }

        private void openplayButton_Click(object sender, EventArgs e)
        {
            string selectedPlaylist;
            if (playlistBox.SelectedIndex == -1)
            {
                selectedPlaylist = string.Empty;
            }
            else
            {
                selectedPlaylist = playlistBox.SelectedItem.ToString();
            }

            if (searchTextBox.Text != null && searchTextBox.Text != string.Empty && selectedPlaylist != null && selectedPlaylist != string.Empty)
            {
                MessageBox.Show("It's one or the other pal, either clear the playlist in the combobox or delete the text in the search box and press me again.");
            }
            else
            {
                if (searchTextBox.Text != string.Empty && searchTextBox.Text != null)
                {
                    OpenorPlay(searchTextBox.Text);
                }
                else if (selectedPlaylist != string.Empty && selectedPlaylist != null)
                {
                    OpenorPlay(selectedPlaylist);
                }
            }
        }
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
            {
                openplayButton.Enabled = true;
            }
            else if (searchTextBox.Text.Length <= 0 && playlistBox.SelectedIndex != -1)
            {
                openplayButton.Enabled = false;
            }
            else
            {
                openplayButton.Enabled = false;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length == 0 && playlistBox.SelectedIndex == -1)
            {
                this.ActiveControl = null;
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
                MessageBox.Show($"Playlist {playlistBox.Text} Added");
                playlistBox.Text = string.Empty;
                GetPlaylists("Add");
            }
        }
        private void songslistBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (songslistBox.Items.Count > 0)
            {
                string itemText = songslistBox.Items[e.Index].ToString();

                int separatorIndex = itemText.LastIndexOf(objectTypeSeparator.ToString());

                // Get the song name artist, album from the ListBox item
                string songName = itemText.Substring(0, separatorIndex).Trim().Replace(objectTypeSeparator.ToString(), "");
                string artistName = itemText.Substring(separatorIndex).Trim().Replace(objectTypeSeparator.ToString(), "");
                string albumName = itemText.Substring(separatorIndex).Trim().Replace(objectTypeSeparator.ToString(), "");

                // Draw the song name aligned to the left edge of the ListBox
                using (var songNameFont = new Font(songslistBox.Font, System.Drawing.FontStyle.Bold))
                {
                    e.Graphics.DrawString(songName, songNameFont, Brushes.Black, e.Bounds);
                }
                // Draw the artist name aligned to the right edge of the ListBox
                using (var artistNameFont = new Font(songslistBox.Font, System.Drawing.FontStyle.Bold))
                {
                    SizeF artistNameSize = e.Graphics.MeasureString(artistName, artistNameFont);
                    float artistNameX = e.Bounds.Right - artistNameSize.Width;
                    PointF artistNameLocation = new PointF(artistNameX, e.Bounds.Y);
                    e.Graphics.DrawString(artistName, artistNameFont, Brushes.Black, artistNameLocation);
                }
                using (var albumNameFont = new Font(songslistBox.Font, System.Drawing.FontStyle.Bold))
                {
                    SizeF albumNameSize = e.Graphics.MeasureString(albumName, albumNameFont);
                    float albumNameX = e.Bounds.Right / 3 - albumNameSize.Width;
                    PointF albumNameLocation = new PointF(albumNameX, e.Bounds.Y);
                    e.Graphics.DrawString(albumName, albumNameFont, Brushes.Black, albumNameLocation);
                }
                e.DrawFocusRectangle();
            }
            else
            {
                songslistBox.HorizontalScrollbar = false;
                searchTextBox.Enabled = false;
                playlistBox.Enabled = false;
                noLibraryLabel.Visible = true;
                configureLabel.Visible = true;
            }
        }
        private void addtoplaylistButton_Click(object sender, EventArgs e)
        {

            if (songslistBox.SelectedIndex == -1)
            {
                MessageBox.Show("No song was highlighted in the listbox", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (playlistBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a playlist to add to from the playlist dropdown.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //insert song into playlist

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

        private void clearButton_Click(object sender, EventArgs e)
        {
            playlistBox.Text = string.Empty;
            playlistBox.SelectedIndex = -1;
            addtoplaylistButton.Enabled = false;
            openplayButton.Enabled = false;
            if (searchTextBox.Text.Length == 0 && playlistBox.SelectedIndex == -1)
            {
                this.ActiveControl = null;
            }
            clearButton.Visible = false;

        }

        // Next 5 events handle the moving of the form + opening and closing ////
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            movX = e.X;
            movY = e.Y;
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving) // based on bool flag
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
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
            MusicPlayer.Dispose();

        }


        //// Next 2 events are for controlling song navigation with the left & right arrows, if "ENTER" key, plays song ///


        // PreviewKeyDown is where you preview the key.
        // Do not put any logic here, instead use the
        // KeyDown event after setting IsInputKey to true.
        private void songsListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }

        private void songslistBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Play the selected song while listbox is active control
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
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
            equalizerWindow = new EQ(MusicPlayer.GetEqualizer());
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
                        var newDefaultDevice = MusicPlayer.GetSoundDevice(deviceId.First());

                        // Get the current position in milliseconds from WindowsMediaPlayer
                        double currentPositionMs = WindowsMediaPlayer.Ctlcontrols.currentPosition;

                        // find out by what percentage the carriage has moved
                        if (WindowsMediaPlayer.currentMedia != null)
                        {
                            double percent = currentPositionMs / TimeSpan.FromMilliseconds(WindowsMediaPlayer.currentMedia.duration).TotalMilliseconds;
                            // find out the current position of the track by multiplying the percentage by the total duration of the track
                            TimeSpan position = TimeSpan.FromMilliseconds(MusicPlayer.Length.TotalMilliseconds * percent);

                            MusicPlayer.Open(WindowsMediaPlayer.URL, newDefaultDevice);
                            MusicPlayer.Position = position;
                            MusicPlayer.Volume = volumeBar.Value;
                            WindowsMediaPlayer.Ctlcontrols.play();
                            MusicPlayer.Play();
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
        public void OpenLink(Control control)
        {
            string target;
            switch (control.Name.ToString())
            {
                case "githublinkBox":
                    target = "https://github.com/mcworkaholic/IS345-G5-Music-Player";
                    break;
                case "soundcloudButton":
                    target = "https://soundcloud.com/";
                    break;
                case "spotifyButton":
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
            OpenLink((Control)sender as PictureBox);
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
                MusicPlayer.Pause();
                timer1.Stop();
            }
            else
            {
                WindowsMediaPlayer.Ctlcontrols.play();
                MusicPlayer.Play();
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
            MusicPlayer.Volume = volumeBar.Value;
        }

        private void volumeIconBox_Click(object sender, EventArgs e)
        {
            if (MusicPlayer.Volume == 0)
            {
                MusicPlayer.Volume = lastVolume;
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
                TimeSpan position = TimeSpan.FromMilliseconds(MusicPlayer.Length.TotalMilliseconds * percent);
                MusicPlayer.Position = position;
                WindowsMediaPlayer.Ctlcontrols.currentPosition = position.TotalSeconds;
            }
        }
        private void audioPosTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                stopUpdate = false;
                MusicPlayer.Volume = volumeBar.Value;
                MusicPlayer.Play();
                WindowsMediaPlayer.Ctlcontrols.play();
            }
        }

        private void audioPosTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                stopUpdate = true;
                MusicPlayer.Volume = 0;
                WindowsMediaPlayer.Ctlcontrols.pause();
                MusicPlayer.Pause();
            }
        }

        // Stores last known volume 
        private void volumeBar_MouseUp(object sender, MouseEventArgs e)
        {
            lastVolume = volumeBar.Value;
        }

        private void backBox_Click(object sender, EventArgs e)
        {
            libraryPanel.Visible = false;
            treeView.Visible = false;
            backBox.Visible = false;
            songslistBox.Visible = true;
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                var clickedNode = rootNode.FindNodeByDisplayName(e.Node.Text, songObj);
                if (clickedNode.ObjectType == "(Song)")
                {
                    MusicPlayer.Pause();
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
            MusicPlayer.Pause();
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
