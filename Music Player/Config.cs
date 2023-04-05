using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;


namespace Music_Player
{
    public partial class Config : Form
    {
        private dbUtils dbUtils = new dbUtils();
        Form1 form1 = (Form1)ActiveForm;
        ListViewItem musicFolderItem;
        private string connectionString;
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
            string startupFolder = dbUtils.GetStartUpFolder(connectionString);
            musicFolderItem = new ListViewItem("Music Directory");
            if (startupFolder == string.Empty)
            {
                listView.Scrollable = false;
                musicFolderItem.SubItems.Add("NULL");
            }
            else
            {
                listView.Scrollable = true;
                musicFolderItem.SubItems.Add(startupFolder);
            }
            listView.Items.Add(musicFolderItem);
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            editButton.Enabled = true;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem.Text == "Music Directory")
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = "C:\\Users";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    musicFolderItem.SubItems.RemoveAt(1);
                    musicFolderItem.SubItems.Add(dialog.FileName);
                    dbUtils.UpdateStartUpFolder(connectionString, dialog.FileName);
                    refreshButton.Enabled = true;
                    this.Focus();
                    refreshButton.Focus();
                }
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            form1.Refresh();
            this.Close();
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
    }
}
