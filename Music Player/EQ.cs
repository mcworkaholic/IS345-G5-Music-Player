using CSCore.Streams.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Music_Player
{
    public partial class EQ : Form
    {
        private Equalizer _equalizer;
        public EQ(Equalizer equalizer)
        {
            InitializeComponent();
            _equalizer = equalizer;
        }

        // for moving borderless form
        int movX, movY;
        bool isMoving;

        List<Label> labelList = new List<Label>();

        private void EQ_Load(object sender, EventArgs e)
        {
            EqualizerFilter filter;
            // Add event delegates to handle mousehover and leave
            foreach (Control control in mainPanel.Controls)
            {
                if (control is System.Windows.Forms.TrackBar) //Если эквалайзер не пуст, т.е. хотя бы 1 трек запускался и контрол это tracKbar
                {
                    control.MouseHover += new EventHandler(MouseHover);
                    control.MouseLeave += new EventHandler(MouseLeave);
                    if (_equalizer != null)
                    {
                        var trackBar = control as System.Windows.Forms.TrackBar; //считаем, что контрол это trackBar
                        filter = _equalizer.SampleFilters[Int32.Parse((string)trackBar.Tag)]; //получаем значения фильтра эквалайзера по тегу трекбара
                        trackBar.Value = (int)filter.AverageGainDB; //получаем значение децибел, которое было прибавлено в конкретный фильтр
                    }
                }
                else if (control is System.Windows.Forms.Button)
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
            var TrackBar = sender as System.Windows.Forms.TrackBar;
            if (_equalizer != null && TrackBar != null)
            {
                //double perc = (trackbar.Value / (double)trackbar.Maximum);
                var value = (float)(TrackBar.Value);

                //the tag of the trackbar contains the index of the filter
                int filterIndex = Int32.Parse((string)TrackBar.Tag);
                EqualizerFilter filter = _equalizer.SampleFilters[filterIndex];
                filter.AverageGainDB = value;
            }
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
            this.Dispose();
        }
    }
}
