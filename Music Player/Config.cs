using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Config : Form
    {
        private dbUtils dbUtils = new dbUtils();
        Form1 form1 = (Form1)ActiveForm;
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
            form1.Refresh();
        }

        private void exitBox_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void soundcloudButton_Click(object sender, EventArgs e)
        {
            form1.OpenLink(soundcloudButton);
        }

        private void spotifyButton_Click(object sender, EventArgs e)
        {
            form1.OpenLink(spotifyButton);
        }

        private void youtubeButton_Click(object sender, EventArgs e)
        {
            form1.OpenLink(youtubeButton);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            refreshButton.Enabled = true;
        }
    }
}
