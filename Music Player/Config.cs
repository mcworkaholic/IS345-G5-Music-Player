﻿using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Config : Form
    {
        private DBUtils dbUtils = new DBUtils();
        MusicPlayer form1 = (MusicPlayer)ActiveForm;
        private BindingSource bindingSource = new BindingSource();
        private string connectionString;
        readonly string default_startup_folder = "default_startup_folder";
        readonly string scld_client_id = "scld_client_id";
        readonly string scld_auth_token = "scld_auth_token";
        readonly string soundcloud_details = "soundcloud_details";
        readonly string user = "user";
        bool isMoving;
        int movX;
        int movY;
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
            bindingSource.DataSource = dbUtils.GetUserConfig();
            dataGridView1.DataSource = bindingSource;
            dataGridView1.Columns[0].Width = 160;
            dataGridView1.Columns[1].Width = 210;
            dataGridView1.Columns[2].Width = 210;
            dataGridView1.Columns[3].Width = 210;
            dataGridView1.Columns[0].ReadOnly = true;
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                dataGridView1.Rows[0].Cells[1].Value = dialog.FileName;
                refreshButton.Enabled = true;
                this.Focus();
                refreshButton.Focus();
            }
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            refreshButton.Enabled = true;
            // Check if the changed cell is the desired cell
            if (e.ColumnIndex == 1 && e.RowIndex == 0)
            {
                // Show the button
                searchButton.Visible = true;
            }
            else
            {
                // Hide the button
                searchButton.Visible = false;
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Commit all current values, couldn't get sql adapter to work properly
            dbUtils.UpdateChanges(user, default_startup_folder, dataGridView1.Rows[0].Cells[1].Value.ToString());
            dbUtils.UpdateChanges(soundcloud_details, scld_client_id, dataGridView1.Rows[0].Cells[2].Value.ToString());
            dbUtils.UpdateChanges(soundcloud_details, scld_auth_token, dataGridView1.Rows[0].Cells[3].Value.ToString());
            this.Refresh();
            GetData(connectionString);
            form1.LoadLibrary(dbUtils.GetStartUpFolder());
        }

        private void exitBox_Click(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            refreshButton.Enabled = true;
        }

        private void linkBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            goButton.Visible = true;
        }

        private void goButton_Click(object sender, EventArgs e)
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
            string exeDirectory = Environment.CurrentDirectory;
            string exeFile = "music_dir_finder.exe";
            string folderName = "Utilities";

            string scriptPath = Path.Combine(exeDirectory, folderName, exeFile);

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
                            searchingLabel.Visible = false;
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
                usageLabel.Text = "Directory(s):";
                directorieslistBox.HorizontalScrollbar = true;
                e.Handled = true;
            }
        }
        private void backBox_Click(object sender, EventArgs e)
        {
            directorieslistBox.Visible = false;
            usageLabel.Text = "Click in the grid to start editing";
            backBox.Visible = false;
        }


        private void directorieslistBox_DoubleClick(object sender, EventArgs e)
        {   // Really Slow
            // Get the selected directory path from the list box
            string selectedDirectory = directorieslistBox.SelectedItem.ToString();

            // Open the selected directory in File Explorer
            Process.Start("explorer.exe", selectedDirectory);
        }

        //private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    // Vertical text from column 0, or adjust below, if first column(s) to be skipped
        //    if (e.RowIndex == -1 && e.ColumnIndex >= 0)
        //    {
        //        e.PaintBackground(e.CellBounds, true);
        //        e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
        //        e.Graphics.RotateTransform(270);
        //        e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
        //        e.Graphics.ResetTransform();
        //        e.Handled = true;
        //    }
        //}
    }
}
