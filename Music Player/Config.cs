using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace Music_Player
{
    public partial class Config : Form
    {
        // Class Instantiation
        private DBUtils dbUtils = new DBUtils();
        private RhythmRanger _musicPlayer = (RhythmRanger)ActiveForm;
        private Utes utes = new Utes();

        private string connectionString;

        // can be null, true, or false
        public string encryptAfterExit;
        public string showArtAlbum;

        // form movement
        bool isMoving;
        int movX;
        int movY;

        // indicators for whether values have been changed or clicked
        bool encryptChanged = false;
        bool artChanged = false;
        bool libChanged = false;
        bool passChanged = false;
        bool passClicked = false;
        bool newpassViewClicked = false;
        bool refreshClicked = false;

        // load values from DB and set
        CheckState initialEncryptState;
        CheckState initialArtState;

        // counter for odd, even clicks
        int passclickCount;
        int newpassViewClickCount;

        // Database values
        string username;
        string password;
        string defaultStartupFolder;
        string showArt;

        public Config()
        {
            InitializeComponent();
        }
        public string GlobalConnectionString
        {
            set { connectionString = value; }
        }
        private void Config_Load(object sender, EventArgs e)
        {
            linkBox.SelectedIndex = -1;
            dbUtils.GetConnection();
            GetData(connectionString);
        }
        private void GetData(string connectionString)
        {
            List<string> userConfig = dbUtils.GetUserConfig();
            if (userConfig != null)
            {
                // Grab all values
                username = userConfig[0];
                password = userConfig[1];
                defaultStartupFolder = userConfig[2];
                encryptAfterExit = userConfig[3];
                showArt = userConfig[4];

                // Map values to states
                if (encryptAfterExit == "False" || encryptAfterExit == null || encryptAfterExit == "NULL")
                {
                    initialEncryptState = CheckState.Unchecked;
                    encryptcheckBox.Checked = false;
                }
                else
                {
                    initialEncryptState = CheckState.Checked;
                    encryptcheckBox.Checked = true;
                }
                if (showArt == "False" || showArt == null || showArt == "NULL")
                {
                    initialArtState = CheckState.Unchecked;
                    artcheckBox.Checked = false;
                }
                else
                {
                    initialArtState = CheckState.Checked;
                    artcheckBox.Checked = true;
                }

                // Set other values
                userBox.Text = username;
                passBox.Text = password;
                libraryBox.Text = defaultStartupFolder;
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            // Allow user to search for a folder containing music, and setting it as their default startup folder

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                libraryBox.Text = dialog.FileName;
                refreshButton.Enabled = true;
                libChanged = true;
                this.Focus();
                refreshButton.Focus();
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Keeping track of the currently selected song, so that it is still in focus if the user wishes to refresh the settings
            string currentlyPlaying;
            if (_musicPlayer.songslistView.SelectedItems.Count > 0)
            {
                currentlyPlaying = _musicPlayer.songslistView.SelectedItems[0].Text;
            }
            else
            {
                currentlyPlaying = null;
            }

            

            // Check whether initial and current states match up to indicate a change

            if (encryptcheckBox.CheckState != initialEncryptState)
            {
                encryptChanged = true;
            }

            if (artcheckBox.CheckState != initialArtState)
            {
                artChanged = true;
            }

            if (passChanged)
            {
                // Update the password
                string storedHash = dbUtils.GetHash(Program.username);

                // Check user's records with Bcrypt
                if (!BC.Verify(oldpassBox.Text, storedHash))
                {
                    MessageBox.Show("Old password doesn't match your records. Please try again.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (newpassBox.Text != null)
                {
                    string newHash = dbUtils.HashPassword(newpassBox.Text);
                    dbUtils.UpdateChanges("user", "password", newHash);
                    passBox.Text = newHash;

                    // could use states here instead
                    oldpassLabel.Visible = false;
                    newpassLabel.Visible = false;
                    oldpassBox.Visible = false;
                    newpassBox.Visible = false;
                    passBox.Enabled = false;
                }
                else
                {
                    MessageBox.Show("New password must not be empty. Please try again.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (encryptChanged)
            {
                dbUtils.UpdateChanges("user", "encrypt_on_exit", encryptAfterExit);
            }
            if (artChanged)
            {
                dbUtils.UpdateChanges("user", "show_art", showArtAlbum);
                _musicPlayer.albumVisible = showArtAlbum;
            }
            if (libChanged)
            {
                dbUtils.UpdateChanges("user", "default_startup_folder", libraryBox.Text);
                // Get the new library
                if (_musicPlayer.configPanel.Visible == true)
                {
                    _musicPlayer.configPanel.Visible = false;
                    _musicPlayer.songslistView.Visible = true;
                }
                _musicPlayer.songslistView.Items.Clear();
                RhythmRanger.paths.Clear();
                _musicPlayer.LoadLibrary(dbUtils.GetStartUpFolder());
                _musicPlayer.AddSearchSource();
                _musicPlayer.GetPlaylists("Load");
                foreach (Control control in _musicPlayer.Controls)
                {
                    control.Enabled = true;
                }
            }
            newpassView.Visible = false;
            saveBox.Visible = false;
            refreshClicked = true;
            if (userBox.Enabled == true)
            {
                // Set the textboxes to not enabled to simulate a reset 
                editButton.PerformClick();
            }
            
            // Refresh w/changes as well as the musicplayer's library if changed
            this.Refresh();

            // Reset the selected index to the original song that was playing
            if (currentlyPlaying != null)
            {
                int index = -1;
                for (int i = 0; i < _musicPlayer.songslistView.Items.Count; i++)
                {
                    if (_musicPlayer.songslistView.Items[i].Text == currentlyPlaying)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    _musicPlayer.songslistView.Items[index].Selected = true;
                    _musicPlayer.songslistView.Items[index].EnsureVisible();
                }
            }
        }
        private void PromptForSave()
        {
            // Prompt for saving changes
            DialogResult dialogResult = MessageBox.Show("Changes were detected. Would you like to save them before closing?", "Save Changes", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //Commit changes, then close
                refreshButton.PerformClick();
            }
        }
        private void exitBox_Click(object sender, EventArgs e)
        {
            // Checking whether states have changed once again

            if (encryptcheckBox.CheckState != initialEncryptState)
            {
                encryptChanged = true;
            }

            if (artcheckBox.CheckState != initialArtState)
            {
                artChanged = true;
            }

            if (libraryBox.Text != defaultStartupFolder)
            {
                libChanged = true;
            }

            if (!refreshClicked)
            {
                if (passChanged || libChanged || encryptChanged || artChanged)
                {
                    // Indicate to the user that chnages have been made but they have not yet refreshed, yes to save on exit
                    PromptForSave();
                }
            }
            else
            {
                if (encryptChanged)
                {
                    dbUtils.UpdateChanges("user", "encrypt_on_exit", encryptAfterExit);
                }
                if (artChanged)
                {
                    dbUtils.UpdateChanges("user", "show_art", showArtAlbum);
                }
            }
            _musicPlayer.albumVisible = showArtAlbum;
            this.Close();
            this.Dispose();
        }

        private void minibox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MouseHover(object sender, EventArgs e)
        {
            // hover on
            this.Cursor = Cursors.Hand;
        }
        private void MouseLeave(object sender, EventArgs e)
        {
            // Change cursor to default when hovering away
            this.Cursor = Cursors.Default;
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // For custom form movement
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

        private void linkBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkBox.SelectedIndex > -1)
            {
                utes.OpenLink(linkBox.SelectedItem.ToString());
            }
        }

        private void Config_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void locateButton_Click(object sender, EventArgs e)
        {
            filefinderPanel.Visible = true;
            directorytextBox.Clear();
        }
        private void SearchDirectories(string rootDirectory)
        {
            directorytextBox.Text = "Searching...";
            if (directorieslistBox.Items.Count > 0)
            {
                directorieslistBox.Items.Clear();
            }
            List<string> audioDirectories = utes.SearchForAudioDirectories(rootDirectory);
            directorieslistBox.BringToFront();
            directorieslistBox.Visible = true;
            directorieslistBox.Items.Add($"Found {audioDirectories.Count} directories containing audio in: {rootDirectory}");
            string line = new string('-', directorieslistBox.Width);
            directorieslistBox.Items.Add(line);

            if (audioDirectories.Count > 0)
            {
                foreach (string file in audioDirectories)
                {
                    directorieslistBox.Items.Add(file);
                }
            }
            filefinderPanel.Visible = false;
        }
        private void directorytextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Enter key is pressed, search the directory
                string rootDirectory = Path.Combine("C:\\Users\\", directorytextBox.Text);

                // Error handling
                if (!Directory.Exists(rootDirectory))
                {
                    MessageBox.Show("Please enter a valid directory hint.", "Invalid Directory Hint", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SearchDirectories(rootDirectory);
                }
                backBox.Visible = true;
                backBox.BringToFront();
                directorieslistBox.HorizontalScrollbar = true;
                e.Handled = true;
            }
        }
        private void backBox_Click(object sender, EventArgs e)
        {
            directorieslistBox.Visible = false;
            backBox.Visible = false;
        }


        private void directorieslistBox_DoubleClick(object sender, EventArgs e)
        {   // Really Slow
            // Get the selected directory path from the list box
            string selectedDirectory = directorieslistBox.SelectedItem.ToString();

            // Open the selected directory in File Explorer
            Process.Start("explorer.exe", selectedDirectory);
        }

        //Rounded Corners
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

        private void editButton_Click(object sender, EventArgs e)
        {
            // Toggle between enabled mode or not for each textbox
            if (userBox.Enabled == false)
            {
                foreach (Control control in configPanel.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (Control control in configPanel.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Enabled = false;
                    }
                }

                // could use states here instead
                searchButton.Enabled = false;
                newpassView.Visible = false;
                oldpassLabel.Visible = false;
                newpassLabel.Visible = false;
                oldpassBox.Visible = false;
                newpassBox.Visible = false;
                passBox.Enabled = false;
            }

        }

        private void libraryBox_MouseClick(object sender, MouseEventArgs e)
        {
            searchButton.Enabled = true;
        }

        private void encryptcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshButton.Enabled = true;

            if(encryptcheckBox.CheckState == CheckState.Checked)
            {
                encryptAfterExit = "True";
            }
            else
            {
                encryptAfterExit = "False";
            }
        }
        private void artcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshButton.Enabled = true;

            if(artcheckBox.CheckState == CheckState.Checked)
            {
                showArtAlbum = "True";
            }
            else
            {
                showArtAlbum = "False";
            }
        }

        private void libraryBox_TextChanged(object sender, EventArgs e)
        {
            refreshButton.Enabled = true;
        }

        private void passBox_Click(object sender, EventArgs e)
        {
            // Handle visibilty for the password change area

            passBox.SelectionLength = 0;
            if (passBox.Enabled == true)
            {
                passClicked = utes.ToggleState(passClicked, ref passclickCount);
                if (passClicked)
                {
                    oldpassLabel.Visible = true;
                    newpassLabel.Visible = true;
                    oldpassBox.Visible = true;
                    newpassBox.Visible = true;
                    newpassView.Visible = true;
                }
                else
                {
                    oldpassBox.Clear();
                    newpassBox.Clear();
                    oldpassLabel.Visible = false;
                    newpassLabel.Visible = false;
                    oldpassBox.Visible = false;
                    newpassBox.Visible = false;
                    newpassView.Visible = false;
                }
            }
        }
        private void newpassBox_TextChanged(object sender, EventArgs e)
        {
            if (oldpassBox.Text.Length == newpassBox.Text.Length && oldpassBox.Text != newpassBox.Text)
            {
                saveBox.Visible = true;
            }
        }

        private void configPanel_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void passBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            passBox.SelectionLength = 0;
        }

        private void newpassView_Click(object sender, EventArgs e)
        {
            // Toggle between '•' and what the character is for the password changing area

            newpassViewClicked = utes.ToggleState(newpassViewClicked, ref newpassViewClickCount);
            if (newpassViewClicked)
            {
                if (newpassBox.PasswordChar == '•')
                {
                    newpassBox.PasswordChar = '\0';
                    oldpassBox.PasswordChar = '\0';
                }
                else
                {
                    newpassBox.PasswordChar = '•';
                    oldpassBox.PasswordChar = '•';
                }
            }
            else
            {
                newpassBox.PasswordChar = '•';
                oldpassBox.PasswordChar = '•';
            }
        }

        private void saveBox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveBox.CheckState == CheckState.Checked)
            {
                passChanged = true;
            }
            else { passChanged = false; }
        }
    }
}
