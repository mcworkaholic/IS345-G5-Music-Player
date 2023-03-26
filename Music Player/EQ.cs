using CSCore.Streams.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Music_Player
{
    public partial class EQ : Form
    {
        EqualizerFilter filter;
        public EQ()
        {
            InitializeComponent();
        }

        // for moving borderless form
        int movX, movY;
        bool isMoving;

        List<Label> labelList = new List<Label>();

        private void EQ_Load(object sender, EventArgs e)
        {
            // Add event delegates to handle mousehover and leave
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Button)
                {
                    control.MouseHover += new EventHandler(MouseHover);
                    control.MouseLeave += new EventHandler(MouseLeave);
                }
                else if (control is TrackBar trackbar)
                {
                    control.MouseHover += new EventHandler(MouseHover);
                    control.MouseLeave += new EventHandler(MouseLeave);
                }
                else if (control is PictureBox pictureBox)
                {
                    control.MouseHover += new EventHandler(MouseHover);
                    control.MouseLeave += new EventHandler(MouseLeave);
                }
            }
            // Create and add labels to the list
            for (int i = 0; i <= 10; i++)
            {
                Label label = this.Controls.Find($"trackBar{i}Label", true).FirstOrDefault() as Label;
                if (label != null)
                {
                    labelList.Add(label);
                }
            }
        }
        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control control in mainPanel.Controls)
            {
                if (control is System.Windows.Forms.TrackBar trackbar)
                {
                    string labelName = trackbar.Name + "Label";
                    foreach (Label label in labelList)
                    {
                        if (label.Name == labelName)
                        {
                            label.Text = trackbar.Value.ToString() + "dB";
                        }
                    }
                }
            }
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            foreach (Control control in mainPanel.Controls)
            {
                if (control is System.Windows.Forms.TrackBar trackbar)
                {
                    trackbar.Value = 0;
                    string labelName = trackbar.Name + "Label";
                    foreach (Label label in labelList)
                    {
                        if (label.Name == labelName)
                        {
                            label.Text = trackbar.Value.ToString() + "dB";
                        }
                    }
                }
            }
        }
        // cursors
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

        // form movement and closing
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            movX = e.X;
            movY = e.Y;
        }

        // For borderless forms, utilizes the top panel
        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }



        private void closeBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
