using AxWMPLib;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // setting flags and initial states
        int queuedSongs = 0;
        int lastPlayingIndex;
        int currentSelectedIndex;
        bool shuffleButtonClicked = false;
        bool listBoxDoubleClick = false;
        int shuffleButtonClickCount = 0;
        //Create Global Variables of String Type Array to save the titles or name of the //Tracks and path of the track 
        String[] paths, files;
        List<int> queueList = new List<int>();

        private void songslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDoubleClick)
            {
                //play music 
                //only plays when double clicked, a single click only changes the listbox so the user can add the selected song to a queue
                WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                listBoxDoubleClick = false; // reset the flag
            }
        }
        public void SetCurrentEffectPreset(int value)
        {
            // requires registry permission. need to configure for users who do not have registry editing permissions
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            string path = string.Format(@"{0}\Software\Microsoft\MediaPlayer\Preferences", identity.User.Value);
            RegistryKey key = Registry.Users.OpenSubKey(path, true);
            if (key == null)
                throw new Exception("Registry key not found!");
            key.SetValue("SaveSettingsOnExit", value, RegistryValueKind.DWord);
        }
        private void WindowsMediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
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
                queuedSongs--;
                queueList.RemoveAt(0);
                queueLabel.Text = $"Queued Songs: {queuedSongs}";
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
            if (shuffleButtonClicked == false)
            {
                if (songslistBox.SelectedIndex < files.Length - 1)
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
                // while loop ensures that the same song that is currentl playing is not shuffled to next
                while (Randomize() != Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL))
                {
                    songslistBox.SelectedIndex = Randomize();
                    WindowsMediaPlayer.URL = paths[songslistBox.SelectedIndex];
                    break;
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
                //set color/background
            }
            else
            {
                //set color/background
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
            queuedSongs++;
            queueLabel.Visible = true;
            queueLabel.Text = $"Queued Songs: {queuedSongs}";
            currentSelectedIndex = songslistBox.SelectedIndex;
            lastPlayingIndex = Array.IndexOf(paths, WindowsMediaPlayer.currentMedia.sourceURL);
            queueList.Add(currentSelectedIndex);
        }

        private void WindowsMediaPlayer_MediaChange(object sender, _WMPOCXEvents_MediaChangeEvent e)
        {
            durationLabel.Visible = true;

            // Get the duration of the media file being played
            double duration = WindowsMediaPlayer.currentMedia.duration;

            //  display it in durationLabel
            durationLabel.Text = $"Length: {TimeSpan.FromSeconds(duration).ToString(@"mm\:ss")}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add a delegate for the MediaChange event.
            WindowsMediaPlayer.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(WindowsMediaPlayer_MediaChange);
            // set visualization preset from windows media player
            SetCurrentEffectPreset(4);
        }

        private void vizButton_Click(object sender, EventArgs e)
        {
            // not working, would like to be able to change the visualization live while the application is running
            //int start = 0;
            //SetCurrentEffectPreset();
        }

        private void selectSongsButton_Click(object sender, EventArgs e)
        {
            //Select songs from file dialog
            OpenFileDialog ofd = new OpenFileDialog();

            //Code to select multiple files w/click and shift
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = ofd.SafeFileNames; //Save the names of the track in files array 
                paths = ofd.FileNames; //Save the paths of the tracks in path array 

                //Display the music titles in listbox 
                for (int i = 0; i < files.Length; i++)
                {
                    // removing "03." and ".mp3" from filename for display purposes ("03. Tears.mp3" -> "Tears") 
                    int firstPeriodIndex = files[i].IndexOf('.');
                    int secondPeriodIndex = files[i].IndexOf('.', firstPeriodIndex + 1);
                    string title = files[i].Substring(firstPeriodIndex + 1, secondPeriodIndex - firstPeriodIndex - 1);
                    songslistBox.Items.Add(title); // Display song titles in Listbox 
                }
            }
        }
    }
    class TreeNode : IEnumerable<TreeNode>
    {
        private readonly Dictionary<string, TreeNode> _children = new Dictionary<string, TreeNode>();

        public readonly string ID;
        public TreeNode Parent { get; private set; }

        public TreeNode(string id)
        {
            this.ID = id;
        }

        public TreeNode GetChild(string id)
        {
            return this._children[id];
        }

        public void Add(TreeNode item)
        {
            if (item.Parent != null)
            {
                item.Parent._children.Remove(item.ID);
            }

            item.Parent = this;
            this._children.Add(item.ID, item);
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return this._children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get { return this._children.Count; }
        }
    }
}
