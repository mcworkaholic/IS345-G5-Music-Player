using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Downloads : Form
    {
        Utes utes = new Utes();

        // for moving borderless form
        private int movX, movY;
        private bool isMoving;

        private bool isDragging = false;


        public Downloads()
        {
            InitializeComponent();
        }

        private void closeBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
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

        private void downloadslistBox_DragEnter(object sender, DragEventArgs e)
        {
            // Paste the URL
            downloadslistBox.Items.Add(e.Data.GetData("System.String", true).ToString());            
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<string> deletedItems = new List<string>();
            foreach (var item in downloadslistBox.SelectedItems)
            {
                deletedItems.Add(item.ToString());
            }

            foreach (var item in deletedItems)
            {
                downloadslistBox.Items.Remove(item);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetData("System.String") != null)
            {
                downloadslistBox.Items.Add(Clipboard.GetData("System.String"));
            }
        }

        private void downloadslistBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pastemenuStrip.Width = 100;
                pastemenuStrip.Show(downloadslistBox, new Point(e.X, e.Y));
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }

        private void topPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isMoving = false;
        }
    }
}
