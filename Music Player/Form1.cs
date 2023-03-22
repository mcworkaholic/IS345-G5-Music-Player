using AxWMPLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Brushes = System.Drawing.Brushes;

namespace Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // setting flags and initial states
        int lastPlayingIndex;
        int currentSelectedIndex;
        bool shuffleButtonClicked = false;
        bool listBoxDoubleClick = false;
        int shuffleButtonClickCount = 0;
        //Create Global Variables of String Type Array to save the titles or name of the //Tracks and path of the track 
        string[] files, paths;
        string workingDirectory;
        string musicFolderPath;
        string dbPath;
        string connectionString;
        List<int> queueList = new List<int>();

        // Create a dictionary to cache album artwork URLs
        Dictionary<string, string> albumArtCache = new Dictionary<string, string>();

        private void songslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDoubleClick)
            {
                //play music 
                //only plays when double clicked, a single click only changes the listbox so the user can add the selected song to a queue or playlist
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                listBoxDoubleClick = false; // reset the flag
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
            queueButton.Enabled = true;
            shuffleButton.Enabled = true;
            vizButton.Enabled = true;
            nextBox.Enabled = true;
            previousBox.Enabled = true;
            if (WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                // start a timer to initiate autoplay of songs with the current play state (shuffle or next)
                playTimer.Interval = 100;
                playTimer.Enabled = true;
            }
        }
        private int Randomize()
        {
            // shuffle functionality generates a random number within an array of indexes from the listbox
            // and changes the currently selected index
            var random = new Random();
            int[] indexes = new int[songslistBox.Items.Count];
            for (var i = 0; i < indexes.Length; i += 1)
                indexes[i] = i;
            List<int> list = indexes.ToList();
            int newIndex = random.Next(list.Count);
            return newIndex;
        }
        private void playTimer_Tick(object sender, EventArgs e)
        {
            if (queueList.Count == 0)
            {
                queueLabel.Visible = false;
            }
            else
            {
                WindowsMediaPlayer.URL = paths[queueList[0]];
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
                        randomizedIndex = Randomize();
                    } while (randomizedIndex == songslistBox.SelectedIndex);

                    songslistBox.SelectedIndex = randomizedIndex;
                }
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
            }
            playTimer.Enabled = false;
        }

        private void previousBox_Click(object sender, EventArgs e)
        {
            if (shuffleButtonClicked == false)
            {
                if (songslistBox.SelectedIndex == -1)
                {
                    int currentIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
                    if (paths.GetLowerBound(0) == currentIndex)
                    {
                        songslistBox.SelectedIndex = songslistBox.Items.Count - 1;
                    }
                    else
                    {
                        songslistBox.SelectedIndex = currentIndex + 1;
                    }
                    WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                }
                if (songslistBox.SelectedIndex > 0)
                {
                    int currentIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
                    WindowsMediaPlayer.URL = paths[currentIndex - 1];
                    songslistBox.SelectedIndex = Array.IndexOf(paths, WindowsMediaPlayer.URL);
                }
                else if (songslistBox.SelectedIndex == 0)
                {
                    songslistBox.SelectedIndex = songslistBox.Items.Count - 1;
                    WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                }
            }
            else if (shuffleButtonClicked == true)
            {
                // while loop ensures that the same song that is currently playing is not shuffled to next
                while (Randomize() != Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL))
                {
                    songslistBox.SelectedIndex = Randomize();
                    WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                    break;
                }
            }
        }

        private void nextBox_Click(object sender, EventArgs e)
        {  
            if (queueList.Count > 0)
            {
                WindowsMediaPlayer.URL = paths[queueList[0]];
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
                }
                else if (shuffleButtonClicked == true)
                {
                    // while loop ensures that the same song that is currently playing is not shuffled to next
                    while (Randomize() != Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL))
                    {
                        songslistBox.SelectedIndex = Randomize();
                        WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
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

        private void songslistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                listBoxDoubleClick = true;
                //play music 
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                listBoxDoubleClick = false; // reset the flag
            }
        }

        private void queueButton_Click(object sender, EventArgs e)
        {
            queueLabel.Visible = true;
            clearQueueButton.Visible = true;
            currentSelectedIndex = songslistBox.SelectedIndex;
            lastPlayingIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
            queueList.Add(currentSelectedIndex);
            queueLabel.Text = $"Queued Songs: {queueList.Count}";
        }
        private void WindowsMediaPlayer_MediaChange(object sender, _WMPOCXEvents_MediaChangeEvent e)
        {
            // Get the duration of the media file being played
            double duration = WindowsMediaPlayer.currentMedia.duration;

            //  display it in durationLabel
            durationLabel.Visible = true;
            durationLabel.Text = $"Length: {TimeSpan.FromSeconds(duration):mm\\:ss}";
            var root = TreeNode.BuildTree(musicFolderPath);
            string currentSongPath = WindowsMediaPlayer.currentMedia.sourceURL;
            var currentNode = root.FindNodeByFullPath(currentSongPath);
            string fileName = currentNode.FileName;
            string albumName = currentNode.GetParent().ToString();
            string albumArtPath = currentSongPath.Replace(fileName, "Album Art");

            // Check if the album artwork URL is already in the cache
            if (albumArtCache.ContainsKey(albumName))
            {
                // Set the image source to the cached artwork URL
                albumArtBox.ImageLocation = albumArtCache[albumName];
            }
            else
            {
                // Search for the album artwork URL and add it to the cache if needed
                ApplySearch(albumName, albumArtPath);
            }
        }

        private async Task SearchForAlbumCover(string album)
        {
            try
            {
                // Build the iTunes API URL
                string url = $"https://itunes.apple.com/search?term=" + album + "&country=us" + "&entity=album";
                // Make a request to the API
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic data = serializer.Deserialize<dynamic>(responseBody);

                    // Get the first result
                    dynamic result = data["results"][0];

                    // Get the artwork URL
                    string artworkUrl = result["artworkUrl100"].ToString().Replace("100x100bb.jpg", "100000x100000-999.jpg");

                    // Add the artwork URL to the cache for this album
                    albumArtCache[album] = artworkUrl;

                    // Set the image source to the artwork URL
                    albumArtBox.Load(artworkUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void ApplySearch(string album, string albumArtPath)
        {
            // Check if any artwork files exist in the Album Art folder
            string[] artFiles = Directory.GetFiles(albumArtPath, "*.jpg");
            if (artFiles.Length > 0)
            {
                // Set the image source to the first artwork file found
                albumArtBox.ImageLocation = artFiles[0];
            }
            else
            {
                if (saveArtButton.Visible == false)
                    saveArtButton.Visible = true;
                // Search for the album artwork URL and add it to the cache
                await SearchForAlbumCover(album);
            }
        }

        private void LoadMusic(string path)
        {
            // Point to the directory you want to build a tree of

            // Build the tree structure of the directory
            TreeNode rootNode = TreeNode.BuildTree(path);

            // Get the third level children of the root node
            List<TreeNode> thirdLevelChildren = new List<TreeNode>();
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
                            string artistName = greatGrandChild.GetParent().Parent.DisplayName;
                            string itemText = $"{songName} {artistName}";

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
        private void Form1_Load(object sender, EventArgs e)
        {
            // Add a delegate for the MediaChange event.
            WindowsMediaPlayer.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(WindowsMediaPlayer_MediaChange);
            // set visualization preset from windows media player
            SetCurrentEffectPreset(4);
            workingDirectory = Environment.CurrentDirectory;
            musicFolderPath = Directory.GetParent(workingDirectory).Parent.FullName + "\\Music";
            dbPath = Directory.GetParent(workingDirectory).Parent.FullName + "\\Data\\users.db";
            connectionString = $@"Data Source={dbPath};";
            LoadMusic(musicFolderPath);
            AddSearchSource(musicFolderPath);
        }
        private void AddSearchSource(string path)
        {
            var rootNode = TreeNode.BuildTree(path);
            var allNodesExceptRoot = rootNode.GetAllNodesExceptRoot();
            var autoCompleteSource = allNodesExceptRoot.Select(node => node.DisplayName).ToList();
            autoCompleteSource.RemoveAll(node => node.Equals("Album Art")); // remove node and its children if node is album art
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

        private void openplayButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;
            string pattern = searchText.Trim().Replace(" ", ".*");
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var rootNode = TreeNode.BuildTree(musicFolderPath);
            foreach (TreeNode node in rootNode.GetAllNodesExceptRoot())
            {
                if (regex.IsMatch(node.FileName))
                {
                    // Found a matching node
                    if (node.NodeType == NodeType.File)
                    {
                        // Play the song associated with the node
                        string songPath = node.FullPath;
                        WindowsMediaPlayer.URL = songPath;
                        break; // Stop searching
                    }
                    else if (node.NodeType == NodeType.Folder)
                    {
                        // set songslListBox.Visible = false;
                        // open treeview of folder and its descendants
                    }
                }
            }
            searchTextBox.Clear();
            openplayButton.Enabled = false;
        }

        private void saveArtButton_Click(object sender, EventArgs e)
        {
            saveArtButton.Visible = false;
            // save artwork from cache to their respective folders
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
                openplayButton.Enabled = true;
            else openplayButton.Enabled = false;
        }

        private void songslistBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            // Get the song name and artist from the ListBox item
            string itemText = songslistBox.Items[e.Index].ToString();
            int separatorIndex = itemText.LastIndexOf(' ');

            string songName = itemText.Substring(0, separatorIndex).Trim();
            string artistName = itemText.Substring(separatorIndex + 1).Trim();

            // Draw the song name aligned to the left edge of the ListBox
            using (var songNameFont = new Font(songslistBox.Font, FontStyle.Bold))
            {
                e.Graphics.DrawString(songName, songNameFont, Brushes.Black, e.Bounds);
            }
            // Draw the artist name aligned to the right edge of the ListBox
            using (var artistNameFont = new Font(songslistBox.Font, FontStyle.Bold))
            {
                SizeF artistNameSize = e.Graphics.MeasureString(artistName, artistNameFont);
                float artistNameX = e.Bounds.Right - artistNameSize.Width;
                PointF artistNameLocation = new PointF(artistNameX, e.Bounds.Y);
                e.Graphics.DrawString(artistName, artistNameFont, Brushes.Black, artistNameLocation);
            }
            e.DrawFocusRectangle();
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
                MessageBox.Show("Please enter a unique name in the playlists textbox to create a new playlist.");
            }
            else
            {
                //using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                //{
                //    connection.Open();
                //    string sql = "INSERT INTO playlist (SELECT user_id FROM user WHERE user_id = @user_id , playlist_name) VALUES (@user_id, @playlist_name)";
                //    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                //    {
                //        command.Parameters.AddWithValue("@playlist_name", playlistBox.Text);
                //        command.Parameters.AddWithValue("@user_id", user_id);
                //        command.ExecuteNonQuery();
                //    }
                //}
            }
        }

        private void vizButton_Click(object sender, EventArgs e)
        {
            // not working, would like to be able to change the visualization live while the application is running
            //int start = 0;
            //SetCurrentEffectPreset();
        }

        // ORIGINAL SONG LOAD FUNCTION
        //private void selectSongsButton_Click(object sender, EventArgs e)
        //{
        //    //Select songs from file dialog
        //    OpenFileDialog ofd = new OpenFileDialog();

        //    //Code to select multiple files w/click and shift
        //    ofd.Multiselect = true;

        //    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        files = ofd.SafeFileNames; //Save the names of the track in files array 
        //        paths = ofd.FileNames; //Save the paths of the tracks in path array 

        //        //Display the music titles in listbox 
        //        for (int i = 0; i < files.Length; i++)
        //        {

        //        }
        //    }
        //}
    }
    public class TreeNode
    {
        private static TreeNode _cachedTree;

        private readonly List<TreeNode> _children = new List<TreeNode>();
        public string DisplayName { get; set; } // Displays the song title w/out file extension and track number
        public string FullPath { get; set; } //  property to store file/folder path
        public string FileName { get; set; } // property to store the name of the song file, i.e "07. A Street I Know.mp3"
        public IEnumerable<TreeNode> Children => _children;
        public TreeNode Parent { get; set; }
        public NodeType NodeType { get; set; } // whether node is a file or folder
        public TreeNode FindNodeByFileName(string fileName)
        {
            // retrieves corresponding node by its filename 
            if (this.FileName == fileName)
            {
                return this;
            }
            foreach (var child in Children)
            {
                var foundNode = child.FindNodeByFileName(fileName);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
        public TreeNode FindNodeByFullPath(string path)
        {
            // retrieves corresponding node by its path
            if (this.FullPath == path)
            {
                return this;
            }
            foreach (var child in Children)
            {
                var foundNode = child.FindNodeByFullPath(path);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
        public IEnumerable<TreeNode> GetAllNodesExceptRoot()
        {
            var allNodes = new List<TreeNode>();
            foreach (var childNode in Children)
            {
                allNodes.Add(childNode);
                allNodes.AddRange(childNode.GetAllNodesExceptRoot());
            }
            return allNodes;
        }
        public TreeNode GetParent()
        {
            return Parent;
        }
        public void AddChild(TreeNode child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void DeleteChild(TreeNode child)
        {
            _children.Remove(child);
        }
        public static TreeNode BuildTree(string directoryPath)
        {
            //if (_cachedTree != null)
            //{
            //    return _cachedTree;
            //}

            var rootNode = new TreeNode
            {
                FullPath = directoryPath,
                FileName = Path.GetFileName(directoryPath),
                DisplayName = Path.GetFileName(directoryPath),
                NodeType = NodeType.Folder
            };

            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                int firstCharIndex = fileName.IndexOf("-");
                string title;
                if (firstCharIndex == -1)
                {
                    title = fileName.Replace(".mp3", "").Substring(fileName.IndexOf('.') + 1).Trim();
                }
                else
                {
                    string[] parts = fileName.Split('-');
                    title = parts[1].Trim().Replace(".mp3", "");
                }
                var node = new TreeNode
                {
                    FullPath = filePath,
                    FileName = Path.GetFileName(filePath),
                    DisplayName = title,
                    NodeType = NodeType.File
                };
                rootNode.AddChild(node);
            }

            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                var node = BuildTree(subDirectoryPath);
                rootNode.AddChild(node);
            }
            //_cachedTree = rootNode;
            return rootNode;
        }
    }

    public enum NodeType
    {
        File,
        Folder
    }


}
