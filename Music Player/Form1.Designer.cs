namespace Music_Player
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.songslistBox = new System.Windows.Forms.ListBox();
            this.selectSongsButton = new System.Windows.Forms.Button();
            this.WindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.previousBox = new System.Windows.Forms.PictureBox();
            this.nextBox = new System.Windows.Forms.PictureBox();
            this.shuffleButton = new System.Windows.Forms.Button();
            this.queueButton = new System.Windows.Forms.Button();
            this.albumArtBox = new System.Windows.Forms.PictureBox();
            this.durationLabel = new System.Windows.Forms.Label();
            this.vizButton = new System.Windows.Forms.Button();
            this.queueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).BeginInit();
            this.SuspendLayout();
            // 
            // songslistBox
            // 
            this.songslistBox.FormattingEnabled = true;
            this.songslistBox.Location = new System.Drawing.Point(531, 26);
            this.songslistBox.Name = "songslistBox";
            this.songslistBox.Size = new System.Drawing.Size(202, 316);
            this.songslistBox.TabIndex = 1;
            this.songslistBox.SelectedIndexChanged += new System.EventHandler(this.songslistBox_SelectedIndexChanged);
            this.songslistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.songslistBox_MouseDoubleClick);
            // 
            // selectSongsButton
            // 
            this.selectSongsButton.Location = new System.Drawing.Point(531, 349);
            this.selectSongsButton.Name = "selectSongsButton";
            this.selectSongsButton.Size = new System.Drawing.Size(202, 37);
            this.selectSongsButton.TabIndex = 2;
            this.selectSongsButton.Text = "Select Songs";
            this.selectSongsButton.UseVisualStyleBackColor = true;
            this.selectSongsButton.Click += new System.EventHandler(this.selectSongsButton_Click);
            // 
            // WindowsMediaPlayer
            // 
            this.WindowsMediaPlayer.Enabled = true;
            this.WindowsMediaPlayer.Location = new System.Drawing.Point(0, 26);
            this.WindowsMediaPlayer.Name = "WindowsMediaPlayer";
            this.WindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WindowsMediaPlayer.OcxState")));
            this.WindowsMediaPlayer.Size = new System.Drawing.Size(525, 360);
            this.WindowsMediaPlayer.TabIndex = 0;
            this.WindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.WindowsMediaPlayer_PlayStateChange);
            this.WindowsMediaPlayer.MediaChange += new AxWMPLib._WMPOCXEvents_MediaChangeEventHandler(this.WindowsMediaPlayer_MediaChange);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 417);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(743, 33);
            this.bottomPanel.TabIndex = 4;
            // 
            // playTimer
            // 
            this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
            // 
            // previousBox
            // 
            this.previousBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("previousBox.BackgroundImage")));
            this.previousBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.previousBox.Location = new System.Drawing.Point(64, 360);
            this.previousBox.Name = "previousBox";
            this.previousBox.Size = new System.Drawing.Size(15, 17);
            this.previousBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previousBox.TabIndex = 5;
            this.previousBox.TabStop = false;
            this.previousBox.Click += new System.EventHandler(this.previousBox_Click);
            // 
            // nextBox
            // 
            this.nextBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextBox.BackgroundImage")));
            this.nextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextBox.Location = new System.Drawing.Point(87, 360);
            this.nextBox.Name = "nextBox";
            this.nextBox.Size = new System.Drawing.Size(15, 17);
            this.nextBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.nextBox.TabIndex = 6;
            this.nextBox.TabStop = false;
            this.nextBox.Click += new System.EventHandler(this.nextBox_Click);
            // 
            // shuffleButton
            // 
            this.shuffleButton.BackColor = System.Drawing.Color.Transparent;
            this.shuffleButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shuffleButton.Location = new System.Drawing.Point(384, 356);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(59, 23);
            this.shuffleButton.TabIndex = 7;
            this.shuffleButton.Text = "🔀 Shuffle";
            this.shuffleButton.UseVisualStyleBackColor = false;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            // 
            // queueButton
            // 
            this.queueButton.BackColor = System.Drawing.Color.Transparent;
            this.queueButton.Location = new System.Drawing.Point(447, 356);
            this.queueButton.Name = "queueButton";
            this.queueButton.Size = new System.Drawing.Size(65, 23);
            this.queueButton.TabIndex = 8;
            this.queueButton.Text = " ➕ Queue";
            this.queueButton.UseVisualStyleBackColor = false;
            this.queueButton.Click += new System.EventHandler(this.queueButton_Click);
            // 
            // albumArtBox
            // 
            this.albumArtBox.BackColor = System.Drawing.Color.Black;
            this.albumArtBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.albumArtBox.Image = ((System.Drawing.Image)(resources.GetObject("albumArtBox.Image")));
            this.albumArtBox.Location = new System.Drawing.Point(0, 26);
            this.albumArtBox.Name = "albumArtBox";
            this.albumArtBox.Size = new System.Drawing.Size(65, 64);
            this.albumArtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.albumArtBox.TabIndex = 10;
            this.albumArtBox.TabStop = false;
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.durationLabel.Image = ((System.Drawing.Image)(resources.GetObject("durationLabel.Image")));
            this.durationLabel.Location = new System.Drawing.Point(236, 361);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(46, 13);
            this.durationLabel.TabIndex = 11;
            this.durationLabel.Text = "Length: ";
            this.durationLabel.Visible = false;
            // 
            // vizButton
            // 
            this.vizButton.Location = new System.Drawing.Point(321, 356);
            this.vizButton.Name = "vizButton";
            this.vizButton.Size = new System.Drawing.Size(57, 23);
            this.vizButton.TabIndex = 12;
            this.vizButton.Text = "Next Viz";
            this.vizButton.UseVisualStyleBackColor = true;
            this.vizButton.Click += new System.EventHandler(this.vizButton_Click);
            // 
            // queueLabel
            // 
            this.queueLabel.AutoSize = true;
            this.queueLabel.Location = new System.Drawing.Point(528, 10);
            this.queueLabel.Name = "queueLabel";
            this.queueLabel.Size = new System.Drawing.Size(86, 13);
            this.queueLabel.TabIndex = 13;
            this.queueLabel.Text = "Songs in Queue:";
            this.queueLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 450);
            this.Controls.Add(this.queueLabel);
            this.Controls.Add(this.vizButton);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.albumArtBox);
            this.Controls.Add(this.queueButton);
            this.Controls.Add(this.shuffleButton);
            this.Controls.Add(this.nextBox);
            this.Controls.Add(this.previousBox);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.selectSongsButton);
            this.Controls.Add(this.songslistBox);
            this.Controls.Add(this.WindowsMediaPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🎵 Music Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer WindowsMediaPlayer;
        private System.Windows.Forms.ListBox songslistBox;
        private System.Windows.Forms.Button selectSongsButton;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Timer playTimer;
        private System.Windows.Forms.PictureBox previousBox;
        private System.Windows.Forms.PictureBox nextBox;
        private System.Windows.Forms.Button shuffleButton;
        private System.Windows.Forms.Button queueButton;
        private System.Windows.Forms.PictureBox albumArtBox;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Button vizButton;
        private System.Windows.Forms.Label queueLabel;
    }
}

