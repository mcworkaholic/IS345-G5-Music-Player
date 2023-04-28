using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
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
        private DBUtils dbUtils = new DBUtils();
        MusicPlayer form1 = (MusicPlayer)ActiveForm;
        private string connectionString;

        // can be null, 0, or 1, where 0 or null = false, 1 = true
        public string encryptAfterExit;

        // form movement
        bool isMoving;
        int movX;
        int movY;

        // indicators for whether values have been changed or clicked
        bool checkChanged = false;
        bool libChanged = false;
        bool passChanged = false;
        bool passClicked = false;
        bool refreshClicked = false;

        // counter for odd, even clicks
        int passclickCount;

        // Database values
        string username;
        string password;
        string defaultStartupFolder;
        string encryptOnExit;

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
                username = userConfig[0];
                password = userConfig[1];
                defaultStartupFolder = userConfig[2];
                encryptAfterExit = userConfig[3];

                userBox.Text = username;
                passBox.Text = password;
                libraryBox.Text = defaultStartupFolder;

                switch (encryptAfterExit)
                {
                    case "null":
                        encryptcheckBox.Checked = false; 
                        break;
                    case "False":
                        encryptcheckBox.Checked = false;
                        break;
                    case "True":
                        encryptcheckBox.Checked = true;
                        break;
                }

            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
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
            if (checkChanged)
            {
                dbUtils.UpdateChanges("user", "encrypt_on_exit", encryptAfterExit.ToString());
            }
            if (libChanged)
            {
                dbUtils.UpdateChanges("user", "default_startup_folder", libraryBox.Text);
                form1.LoadLibrary(dbUtils.GetStartUpFolder());
            }
            refreshClicked = true;
            this.Refresh();
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
            if (!refreshClicked)
            {
                if (passChanged || libChanged || checkChanged)
                {
                    PromptForSave();
                }
            }
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
                form1.OpenLink(linkBox.SelectedItem.ToString());
            }
        }

        private void Config_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void locateButton_Click(object sender, EventArgs e)
        {
            filefinderPanel.Visible = true;
        }
        private void SearchDirectories(string rootDirectory)
        {
            searchingLabel.Visible = true;
            searchingLabel.BringToFront();

            string workingDirectory = Environment.CurrentDirectory;
            string exeDirectory = Directory.GetParent(workingDirectory).Parent.FullName + "\\Utilities";
            string exeFile = "music_dir_finder.exe";
            string scriptPath = Path.Combine(exeDirectory, exeFile);

            ProcessStartInfo startInfo = new ProcessStartInfo(scriptPath);
            startInfo.Arguments = rootDirectory;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            HashSet<string> directories = new HashSet<string>();

            Process process = new Process();
            process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data) && directories.Add(e.Data))
                {
                    Invoke(new Action(() =>
                    {
                        if (!directorieslistBox.Items.Contains(e.Data))
                        {
                            searchingLabel.Visible = false;
                        }
                        directorieslistBox.Items.Add(e.Data);
                    }));
                }
            });

            process.Start();
            process.BeginOutputReadLine();
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
                    searchingLabel.Visible = true;
                    SearchDirectories(rootDirectory);
                }

                // Behaviors
                directorieslistBox.Items.Clear();
                directorieslistBox.Visible = true;
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

        //rounded corners
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
            foreach (Control control in configPanel.Controls)
            {
                if(control is TextBox)
                {
                    control.Enabled = true;
                }
            }
        }

        private void libraryBox_MouseClick(object sender, MouseEventArgs e)
        {
            searchButton.Visible = true;
        }

        private void encryptcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshButton.Enabled = true;
            checkChanged = true;

            switch (encryptcheckBox.CheckState)
            {
                case CheckState.Checked: encryptAfterExit = "True"; break;
                case CheckState.Unchecked: encryptAfterExit = "False"; break;
            }
        }

        private void libraryBox_TextChanged(object sender, EventArgs e)
        {
            refreshButton.Enabled = true;
        }

        private void passBox_Click(object sender, EventArgs e)
        {
            passBox.SelectionLength = 0;
            if (passBox.Enabled == true)
            {

                passclickCount++;
                passClicked = (passclickCount % 2 == 1);
                if (passclickCount % 2 == 1)
                {
                    oldpassLabel.Visible = true;
                    newpassLabel.Visible = true;
                    oldpassBox.Visible = true;
                    newpassBox.Visible = true;
                }
                else
                {
                    oldpassLabel.Visible = false;
                    newpassLabel.Visible = false;
                    oldpassBox.Visible = false;
                    newpassBox.Visible = false;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string storedHash = dbUtils.GetHash(Program.username);

            if (!BC.Verify(oldpassBox.Text, storedHash))
            {
                MessageBox.Show("Old password doesn't match your records. Please try again.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newpassBox.Text != null)
            {
                dbUtils.UpdateChanges("user", "password", dbUtils.HashPassword(newpassBox.Text));

                passBox.Text = dbUtils.HashPassword(newpassBox.Text);
                passChanged = true;

                // could use states here instead
                oldpassLabel.Visible = false;
                newpassLabel.Visible = false;
                oldpassBox.Visible = false;
                newpassBox.Visible = false;
                saveButton.Visible = false;
                passBox.Enabled = false;
            }
            else
            {
                MessageBox.Show("New password must not be empty. Please try again.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newpassBox_TextChanged(object sender, EventArgs e)
        {
            saveButton.Visible = true;
        }

        private void configPanel_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void passBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            passBox.SelectionLength = 0;
        }
    }
}
