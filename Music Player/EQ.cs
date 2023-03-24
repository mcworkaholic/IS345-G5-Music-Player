using CSCore.Streams.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Music_Player
{
    public partial class EQ : Form
    {
        private Equalizer _equalizer;
        EqualizerFilter filter;
        public EQ()
        {
            InitializeComponent();
        }

        // for moving borderless form
        int movX, movY;
        bool isMoving;
        float lastValue;
        float currentValue;
        bool valueChanged;
        List<Label> labelList = new List<Label>();

        private void EQ_Load(object sender, EventArgs e)
        {
            
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

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            var trackbar = sender as TrackBar;
            lastValue = trackbar.Value;

        }
        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            var trackbar = sender as TrackBar;
            currentValue = trackbar.Value;

            if (currentValue != lastValue)
            {
                valueChanged = true;
            }
            else
            {
                valueChanged = false;
            }
            if (valueChanged)
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
        private void buttonReset_Click(object sender, EventArgs e)
        {
            foreach (Control contr in mainPanel.Controls)
            {
                if (contr is TrackBar) //If the equalizer is not empty, i.e. at least 1 track was launched and the control is tracKbar
                {
                    var trackBar = contr as TrackBar; //assume control is trackBar
                    trackBar.Value = 0; //get the decibel value that was added to the specific filter
                }
                if (contr is System.Windows.Forms.Label && contr.Tag != null) //if the control is text and the control had a tag (each required label is tied to the trackBar via a tag)
                {
                    foreach (Control contr1 in mainPanel.Controls)//search for a trackbar with the same tag
                    {
                        if (contr1 is TrackBar)
                        {
                            var trackbar = contr1 as TrackBar;
                            if (trackbar.Tag == contr.Tag)
                            {
                                contr.Text = trackbar.Value.ToString() + "dB";//write value from trackbar to label
                            }
                        }
                    }
                }
            }
        }
        // form movement and closing
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            movX = e.X;
            movY = e.Y;
        }

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
