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
            this.playlistBox = new System.Windows.Forms.ComboBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.playlistLabel = new System.Windows.Forms.Label();
            this.newButton = new System.Windows.Forms.Button();
            this.addtoplaylistButton = new System.Windows.Forms.Button();
            this.openplayButton = new System.Windows.Forms.Button();
            this.clearQueueButton = new System.Windows.Forms.Button();
            this.saveArtButton = new System.Windows.Forms.Button();
            this.upperStopBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperStopBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // songslistBox
            // 
            this.songslistBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.songslistBox.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songslistBox.FormattingEnabled = true;
            this.songslistBox.Location = new System.Drawing.Point(531, 26);
            this.songslistBox.Name = "songslistBox";
            this.songslistBox.Size = new System.Drawing.Size(292, 316);
            this.songslistBox.TabIndex = 1;
            this.songslistBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.songslistBox_DrawItem);
            this.songslistBox.SelectedIndexChanged += new System.EventHandler(this.songslistBox_SelectedIndexChanged);
            this.songslistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.songslistBox_MouseDoubleClick);
            // 
            // WindowsMediaPlayer
            // 
            this.WindowsMediaPlayer.Enabled = true;
            this.WindowsMediaPlayer.Location = new System.Drawing.Point(0, 26);
            this.WindowsMediaPlayer.Name = "WindowsMediaPlayer";
            this.WindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WindowsMediaPlayer.OcxState")));
            this.WindowsMediaPlayer.Size = new System.Drawing.Size(525, 363);
            this.WindowsMediaPlayer.TabIndex = 0;
            this.WindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.WindowsMediaPlayer_PlayStateChange);
            this.WindowsMediaPlayer.MediaChange += new AxWMPLib._WMPOCXEvents_MediaChangeEventHandler(this.WindowsMediaPlayer_MediaChange);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 424);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(823, 26);
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
            this.previousBox.Enabled = false;
            this.previousBox.Location = new System.Drawing.Point(64, 362);
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
            this.nextBox.Enabled = false;
            this.nextBox.Location = new System.Drawing.Point(87, 362);
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
            this.shuffleButton.Enabled = false;
            this.shuffleButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shuffleButton.Location = new System.Drawing.Point(384, 356);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(59, 23);
            this.shuffleButton.TabIndex = 7;
            this.shuffleButton.Text = "Shuffle";
            this.shuffleButton.UseVisualStyleBackColor = false;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            // 
            // queueButton
            // 
            this.queueButton.BackColor = System.Drawing.Color.Transparent;
            this.queueButton.Enabled = false;
            this.queueButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.queueButton.Location = new System.Drawing.Point(447, 356);
            this.queueButton.Name = "queueButton";
            this.queueButton.Size = new System.Drawing.Size(64, 23);
            this.queueButton.TabIndex = 8;
            this.queueButton.Text = " ➕ Queue";
            this.queueButton.UseVisualStyleBackColor = false;
            this.queueButton.Click += new System.EventHandler(this.queueButton_Click);
            // 
            // albumArtBox
            // 
            this.albumArtBox.BackColor = System.Drawing.Color.Black;
            this.albumArtBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.albumArtBox.Location = new System.Drawing.Point(0, 26);
            this.albumArtBox.Name = "albumArtBox";
            this.albumArtBox.Size = new System.Drawing.Size(79, 73);
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
            this.durationLabel.Size = new System.Drawing.Size(49, 13);
            this.durationLabel.TabIndex = 11;
            this.durationLabel.Text = "Length: ";
            this.durationLabel.Visible = false;
            // 
            // vizButton
            // 
            this.vizButton.Enabled = false;
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
            this.queueLabel.Location = new System.Drawing.Point(528, 9);
            this.queueLabel.Name = "queueLabel";
            this.queueLabel.Size = new System.Drawing.Size(92, 13);
            this.queueLabel.TabIndex = 13;
            this.queueLabel.Text = "Songs in Queue:";
            this.queueLabel.Visible = false;
            // 
            // playlistBox
            // 
            this.playlistBox.FormattingEnabled = true;
            this.playlistBox.Location = new System.Drawing.Point(618, 348);
            this.playlistBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.playlistBox.Name = "playlistBox";
            this.playlistBox.Size = new System.Drawing.Size(100, 21);
            this.playlistBox.TabIndex = 14;
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.Location = new System.Drawing.Point(618, 372);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(100, 22);
            this.searchTextBox.TabIndex = 15;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(570, 375);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 16;
            this.searchLabel.Text = "Search:";
            // 
            // playlistLabel
            // 
            this.playlistLabel.AutoSize = true;
            this.playlistLabel.Location = new System.Drawing.Point(564, 351);
            this.playlistLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.playlistLabel.Name = "playlistLabel";
            this.playlistLabel.Size = new System.Drawing.Size(50, 13);
            this.playlistLabel.TabIndex = 17;
            this.playlistLabel.Text = "Playlists:";
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(720, 347);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 18;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // addtoplaylistButton
            // 
            this.addtoplaylistButton.Location = new System.Drawing.Point(618, 398);
            this.addtoplaylistButton.Name = "addtoplaylistButton";
            this.addtoplaylistButton.Size = new System.Drawing.Size(100, 23);
            this.addtoplaylistButton.TabIndex = 19;
            this.addtoplaylistButton.Text = "Add to Playlist";
            this.addtoplaylistButton.UseVisualStyleBackColor = true;
            // 
            // openplayButton
            // 
            this.openplayButton.Enabled = false;
            this.openplayButton.Location = new System.Drawing.Point(720, 371);
            this.openplayButton.Name = "openplayButton";
            this.openplayButton.Size = new System.Drawing.Size(75, 23);
            this.openplayButton.TabIndex = 20;
            this.openplayButton.Text = "Open/Play";
            this.openplayButton.UseVisualStyleBackColor = true;
            this.openplayButton.Click += new System.EventHandler(this.openplayButton_Click);
            // 
            // clearQueueButton
            // 
            this.clearQueueButton.Location = new System.Drawing.Point(734, 4);
            this.clearQueueButton.Name = "clearQueueButton";
            this.clearQueueButton.Size = new System.Drawing.Size(89, 22);
            this.clearQueueButton.TabIndex = 21;
            this.clearQueueButton.Text = "Clear Queue";
            this.clearQueueButton.UseVisualStyleBackColor = true;
            this.clearQueueButton.Visible = false;
            this.clearQueueButton.Click += new System.EventHandler(this.clearQueueButton_Click);
            // 
            // saveArtButton
            // 
            this.saveArtButton.Location = new System.Drawing.Point(414, 395);
            this.saveArtButton.Name = "saveArtButton";
            this.saveArtButton.Size = new System.Drawing.Size(111, 23);
            this.saveArtButton.TabIndex = 22;
            this.saveArtButton.Text = "Save Artwork";
            this.saveArtButton.UseVisualStyleBackColor = true;
            this.saveArtButton.Visible = false;
            this.saveArtButton.Click += new System.EventHandler(this.saveArtButton_Click);
            // 
            // upperStopBox
            // 
            this.upperStopBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("upperStopBox.BackgroundImage")));
            this.upperStopBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.upperStopBox.Image = ((System.Drawing.Image)(resources.GetObject("upperStopBox.Image")));
            this.upperStopBox.Location = new System.Drawing.Point(33, 361);
            this.upperStopBox.Name = "upperStopBox";
            this.upperStopBox.Size = new System.Drawing.Size(27, 12);
            this.upperStopBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.upperStopBox.TabIndex = 23;
            this.upperStopBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 372);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 11);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(823, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.upperStopBox);
            this.Controls.Add(this.saveArtButton);
            this.Controls.Add(this.clearQueueButton);
            this.Controls.Add(this.openplayButton);
            this.Controls.Add(this.addtoplaylistButton);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.playlistLabel);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.playlistBox);
            this.Controls.Add(this.queueLabel);
            this.Controls.Add(this.vizButton);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.albumArtBox);
            this.Controls.Add(this.queueButton);
            this.Controls.Add(this.shuffleButton);
            this.Controls.Add(this.nextBox);
            this.Controls.Add(this.previousBox);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.songslistBox);
            this.Controls.Add(this.WindowsMediaPlayer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🎵 Music Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperStopBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer WindowsMediaPlayer;
        private System.Windows.Forms.ListBox songslistBox;
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
        private System.Windows.Forms.ComboBox playlistBox;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label playlistLabel;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button addtoplaylistButton;
        private System.Windows.Forms.Button openplayButton;
        private System.Windows.Forms.Button clearQueueButton;
        private System.Windows.Forms.Button saveArtButton;
        private System.Windows.Forms.PictureBox upperStopBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

