namespace Music_Player
{
    partial class MusicPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicPlayer));
            this.librarylistBox = new System.Windows.Forms.ListBox();
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.shuffleButton = new System.Windows.Forms.Button();
            this.queueButton = new System.Windows.Forms.Button();
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.githublinkBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.eqButton = new System.Windows.Forms.Button();
            this.deviceBox = new System.Windows.Forms.ComboBox();
            this.prevToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.previousBox = new System.Windows.Forms.PictureBox();
            this.nextToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.nextBox = new System.Windows.Forms.PictureBox();
            this.minimizeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.closeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.backBox = new System.Windows.Forms.PictureBox();
            this.playPauseButton = new System.Windows.Forms.PictureBox();
            this.volumeIconBox = new System.Windows.Forms.PictureBox();
            this.settingsBox = new System.Windows.Forms.PictureBox();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.audioControllerPanel = new System.Windows.Forms.Panel();
            this.audioPosTrackBar = new System.Windows.Forms.TrackBar();
            this.volumeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.albumPanel = new System.Windows.Forms.Panel();
            this.libraryAlbumArtBox = new System.Windows.Forms.PictureBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.noLibraryLabel = new System.Windows.Forms.Label();
            this.albumArtBox = new System.Windows.Forms.PictureBox();
            this.clearBox = new System.Windows.Forms.PictureBox();
            this.WindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.configureLabel = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.githublinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playPauseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeIconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.audioControllerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.audioPosTrackBar)).BeginInit();
            this.albumPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.libraryAlbumArtBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // librarylistBox
            // 
            this.librarylistBox.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.librarylistBox.FormattingEnabled = true;
            this.librarylistBox.HorizontalExtent = 1000;
            this.librarylistBox.HorizontalScrollbar = true;
            this.librarylistBox.Location = new System.Drawing.Point(531, 49);
            this.librarylistBox.Name = "librarylistBox";
            this.librarylistBox.Size = new System.Drawing.Size(264, 316);
            this.librarylistBox.TabIndex = 0;
            this.librarylistBox.SelectedIndexChanged += new System.EventHandler(this.songslistBox_SelectedIndexChanged);
            this.librarylistBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.songslistBox_KeyDown);
            this.librarylistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.songslistBox_MouseDoubleClick);
            // 
            // playTimer
            // 
            this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
            // 
            // shuffleButton
            // 
            this.shuffleButton.BackColor = System.Drawing.Color.Transparent;
            this.shuffleButton.Enabled = false;
            this.shuffleButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shuffleButton.Location = new System.Drawing.Point(389, 19);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(59, 23);
            this.shuffleButton.TabIndex = 7;
            this.shuffleButton.TabStop = false;
            this.shuffleButton.Text = "Shuffle";
            this.shuffleButton.UseVisualStyleBackColor = false;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            this.shuffleButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.shuffleButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // queueButton
            // 
            this.queueButton.BackColor = System.Drawing.Color.Transparent;
            this.queueButton.Enabled = false;
            this.queueButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.queueButton.Location = new System.Drawing.Point(454, 19);
            this.queueButton.Name = "queueButton";
            this.queueButton.Size = new System.Drawing.Size(64, 23);
            this.queueButton.TabIndex = 8;
            this.queueButton.TabStop = false;
            this.queueButton.Text = " ➕ Queue";
            this.queueButton.UseVisualStyleBackColor = false;
            this.queueButton.Click += new System.EventHandler(this.queueButton_Click);
            this.queueButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.queueButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.durationLabel.Location = new System.Drawing.Point(220, 23);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(49, 13);
            this.durationLabel.TabIndex = 100;
            this.durationLabel.Text = "Length: ";
            this.durationLabel.Visible = false;
            // 
            // vizButton
            // 
            this.vizButton.Enabled = false;
            this.vizButton.Location = new System.Drawing.Point(326, 19);
            this.vizButton.Name = "vizButton";
            this.vizButton.Size = new System.Drawing.Size(57, 23);
            this.vizButton.TabIndex = 12;
            this.vizButton.TabStop = false;
            this.vizButton.Text = "Next Viz";
            this.vizButton.UseVisualStyleBackColor = true;
            this.vizButton.Click += new System.EventHandler(this.vizButton_Click);
            this.vizButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.vizButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // queueLabel
            // 
            this.queueLabel.AutoSize = true;
            this.queueLabel.Location = new System.Drawing.Point(528, 32);
            this.queueLabel.Name = "queueLabel";
            this.queueLabel.Size = new System.Drawing.Size(92, 13);
            this.queueLabel.TabIndex = 13;
            this.queueLabel.Text = "Songs in Queue:";
            this.queueLabel.Visible = false;
            // 
            // playlistBox
            // 
            this.playlistBox.FormattingEnabled = true;
            this.playlistBox.Location = new System.Drawing.Point(582, 373);
            this.playlistBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.playlistBox.Name = "playlistBox";
            this.playlistBox.Size = new System.Drawing.Size(132, 21);
            this.playlistBox.TabIndex = 2;
            this.playlistBox.SelectedIndexChanged += new System.EventHandler(this.playlistBox_SelectedIndexChanged);
            this.playlistBox.TextChanged += new System.EventHandler(this.playlistBox_TextChanged);
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.Location = new System.Drawing.Point(582, 397);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(534, 400);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 16;
            this.searchLabel.Text = "Search:";
            // 
            // playlistLabel
            // 
            this.playlistLabel.AutoSize = true;
            this.playlistLabel.Location = new System.Drawing.Point(528, 376);
            this.playlistLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.playlistLabel.Name = "playlistLabel";
            this.playlistLabel.Size = new System.Drawing.Size(50, 13);
            this.playlistLabel.TabIndex = 17;
            this.playlistLabel.Text = "Playlists:";
            // 
            // newButton
            // 
            this.newButton.Enabled = false;
            this.newButton.Location = new System.Drawing.Point(720, 371);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 18;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // addtoplaylistButton
            // 
            this.addtoplaylistButton.Enabled = false;
            this.addtoplaylistButton.Location = new System.Drawing.Point(602, 425);
            this.addtoplaylistButton.Name = "addtoplaylistButton";
            this.addtoplaylistButton.Size = new System.Drawing.Size(100, 23);
            this.addtoplaylistButton.TabIndex = 19;
            this.addtoplaylistButton.TabStop = false;
            this.addtoplaylistButton.Text = "Add to Playlist";
            this.addtoplaylistButton.UseVisualStyleBackColor = true;
            this.addtoplaylistButton.Click += new System.EventHandler(this.addtoplaylistButton_Click);
            this.addtoplaylistButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.addtoplaylistButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // openplayButton
            // 
            this.openplayButton.Enabled = false;
            this.openplayButton.Location = new System.Drawing.Point(720, 396);
            this.openplayButton.Name = "openplayButton";
            this.openplayButton.Size = new System.Drawing.Size(75, 23);
            this.openplayButton.TabIndex = 20;
            this.openplayButton.TabStop = false;
            this.openplayButton.Text = "Open/Play";
            this.openplayButton.UseVisualStyleBackColor = true;
            this.openplayButton.Click += new System.EventHandler(this.openplayButton_Click);
            this.openplayButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.openplayButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // clearQueueButton
            // 
            this.clearQueueButton.Location = new System.Drawing.Point(706, 27);
            this.clearQueueButton.Name = "clearQueueButton";
            this.clearQueueButton.Size = new System.Drawing.Size(89, 22);
            this.clearQueueButton.TabIndex = 21;
            this.clearQueueButton.TabStop = false;
            this.clearQueueButton.Text = "Clear Queue";
            this.clearQueueButton.UseVisualStyleBackColor = true;
            this.clearQueueButton.Visible = false;
            this.clearQueueButton.Click += new System.EventHandler(this.clearQueueButton_Click);
            this.clearQueueButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.clearQueueButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.pictureBox1);
            this.topPanel.Controls.Add(this.githublinkBox);
            this.topPanel.Controls.Add(this.closeBox);
            this.topPanel.Controls.Add(this.minimizeBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(796, 26);
            this.topPanel.TabIndex = 26;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            this.topPanel.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.topPanel.MouseHover += new System.EventHandler(this.MouseHover);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Music_Player.Properties.Resources.login_logo;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // githublinkBox
            // 
            this.githublinkBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.githublinkBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.githublinkBox.ErrorImage = null;
            this.githublinkBox.Image = global::Music_Player.Properties.Resources.codelink;
            this.githublinkBox.InitialImage = null;
            this.githublinkBox.Location = new System.Drawing.Point(709, 1);
            this.githublinkBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.githublinkBox.Name = "githublinkBox";
            this.githublinkBox.Size = new System.Drawing.Size(23, 22);
            this.githublinkBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.githublinkBox.TabIndex = 2;
            this.githublinkBox.TabStop = false;
            this.minimizeToolTip.SetToolTip(this.githublinkBox, "https://github.com/mcworkaholic/IS345-G5-Music-Player");
            this.githublinkBox.Click += new System.EventHandler(this.githublinkBox_Click);
            this.githublinkBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.githublinkBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // closeBox
            // 
            this.closeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeBox.ErrorImage = null;
            this.closeBox.Image = global::Music_Player.Properties.Resources.close;
            this.closeBox.InitialImage = null;
            this.closeBox.Location = new System.Drawing.Point(766, 1);
            this.closeBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(26, 22);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeBox.TabIndex = 1;
            this.closeBox.TabStop = false;
            this.closeToolTip.SetToolTip(this.closeBox, "Close ");
            this.closeBox.Click += new System.EventHandler(this.closeBox_Click);
            this.closeBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.closeBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // minimizeBox
            // 
            this.minimizeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimizeBox.ErrorImage = null;
            this.minimizeBox.Image = global::Music_Player.Properties.Resources.minimize;
            this.minimizeBox.InitialImage = null;
            this.minimizeBox.Location = new System.Drawing.Point(735, 1);
            this.minimizeBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.minimizeBox.Name = "minimizeBox";
            this.minimizeBox.Size = new System.Drawing.Size(31, 22);
            this.minimizeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizeBox.TabIndex = 0;
            this.minimizeBox.TabStop = false;
            this.minimizeToolTip.SetToolTip(this.minimizeBox, "Minimize");
            this.minimizeBox.Click += new System.EventHandler(this.minimizeBox_Click);
            this.minimizeBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.minimizeBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // clearButton
            // 
            this.clearButton.Enabled = false;
            this.clearButton.Location = new System.Drawing.Point(721, 371);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 27;
            this.clearButton.TabStop = false;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Visible = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            this.clearButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.clearButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // eqButton
            // 
            this.eqButton.Enabled = false;
            this.eqButton.Location = new System.Drawing.Point(6, 421);
            this.eqButton.Name = "eqButton";
            this.eqButton.Size = new System.Drawing.Size(75, 23);
            this.eqButton.TabIndex = 28;
            this.eqButton.TabStop = false;
            this.eqButton.Text = "Equalizer";
            this.eqButton.UseVisualStyleBackColor = true;
            this.eqButton.Click += new System.EventHandler(this.eqButton_Click);
            this.eqButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.eqButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // deviceBox
            // 
            this.deviceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceBox.Enabled = false;
            this.deviceBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceBox.FormattingEnabled = true;
            this.deviceBox.Location = new System.Drawing.Point(85, 422);
            this.deviceBox.Name = "deviceBox";
            this.deviceBox.Size = new System.Drawing.Size(168, 21);
            this.deviceBox.Sorted = true;
            this.deviceBox.TabIndex = 3;
            this.deviceBox.SelectedIndexChanged += new System.EventHandler(this.deviceBox_SelectedIndexChanged);
            this.deviceBox.Click += new System.EventHandler(this.deviceBox_Click);
            // 
            // previousBox
            // 
            this.previousBox.BackgroundImage = global::Music_Player.Properties.Resources.previous;
            this.previousBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.previousBox.Enabled = false;
            this.previousBox.ErrorImage = null;
            this.previousBox.InitialImage = null;
            this.previousBox.Location = new System.Drawing.Point(49, 22);
            this.previousBox.Name = "previousBox";
            this.previousBox.Size = new System.Drawing.Size(15, 17);
            this.previousBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previousBox.TabIndex = 5;
            this.previousBox.TabStop = false;
            this.prevToolTip.SetToolTip(this.previousBox, "Previous");
            this.previousBox.Click += new System.EventHandler(this.previousBox_Click);
            this.previousBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.previousBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // nextBox
            // 
            this.nextBox.BackgroundImage = global::Music_Player.Properties.Resources.next;
            this.nextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextBox.Enabled = false;
            this.nextBox.ErrorImage = null;
            this.nextBox.InitialImage = null;
            this.nextBox.Location = new System.Drawing.Point(74, 22);
            this.nextBox.Name = "nextBox";
            this.nextBox.Size = new System.Drawing.Size(15, 17);
            this.nextBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.nextBox.TabIndex = 6;
            this.nextBox.TabStop = false;
            this.nextToolTip.SetToolTip(this.nextBox, "Next");
            this.nextBox.Click += new System.EventHandler(this.nextBox_Click);
            this.nextBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.nextBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // backBox
            // 
            this.backBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backBox.ErrorImage = null;
            this.backBox.Image = global::Music_Player.Properties.Resources.backbutton;
            this.backBox.InitialImage = null;
            this.backBox.Location = new System.Drawing.Point(238, -1);
            this.backBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.backBox.Name = "backBox";
            this.backBox.Size = new System.Drawing.Size(25, 22);
            this.backBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backBox.TabIndex = 2;
            this.backBox.TabStop = false;
            this.closeToolTip.SetToolTip(this.backBox, "To Library");
            this.backBox.Visible = false;
            this.backBox.Click += new System.EventHandler(this.backBox_Click);
            this.backBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.backBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // playPauseButton
            // 
            this.playPauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playPauseButton.Enabled = false;
            this.playPauseButton.ErrorImage = null;
            this.playPauseButton.Image = ((System.Drawing.Image)(resources.GetObject("playPauseButton.Image")));
            this.playPauseButton.InitialImage = null;
            this.playPauseButton.Location = new System.Drawing.Point(6, 16);
            this.playPauseButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(33, 27);
            this.playPauseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playPauseButton.TabIndex = 201;
            this.playPauseButton.TabStop = false;
            this.closeToolTip.SetToolTip(this.playPauseButton, "Play/Pause");
            this.playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            this.playPauseButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.playPauseButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // volumeIconBox
            // 
            this.volumeIconBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.volumeIconBox.Enabled = false;
            this.volumeIconBox.ErrorImage = null;
            this.volumeIconBox.Image = ((System.Drawing.Image)(resources.GetObject("volumeIconBox.Image")));
            this.volumeIconBox.InitialImage = null;
            this.volumeIconBox.Location = new System.Drawing.Point(102, 21);
            this.volumeIconBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.volumeIconBox.Name = "volumeIconBox";
            this.volumeIconBox.Size = new System.Drawing.Size(21, 19);
            this.volumeIconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.volumeIconBox.TabIndex = 202;
            this.volumeIconBox.TabStop = false;
            this.closeToolTip.SetToolTip(this.volumeIconBox, " Mute");
            this.volumeIconBox.Click += new System.EventHandler(this.volumeIconBox_Click);
            this.volumeIconBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.volumeIconBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // settingsBox
            // 
            this.settingsBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingsBox.ErrorImage = null;
            this.settingsBox.Image = global::Music_Player.Properties.Resources.settings;
            this.settingsBox.InitialImage = null;
            this.settingsBox.Location = new System.Drawing.Point(256, 422);
            this.settingsBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.settingsBox.Name = "settingsBox";
            this.settingsBox.Size = new System.Drawing.Size(14, 20);
            this.settingsBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.settingsBox.TabIndex = 3;
            this.settingsBox.TabStop = false;
            this.closeToolTip.SetToolTip(this.settingsBox, "More Settings");
            this.settingsBox.Click += new System.EventHandler(this.settingsBox_Click);
            this.settingsBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.settingsBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // volumeBar
            // 
            this.volumeBar.AutoSize = false;
            this.volumeBar.Enabled = false;
            this.volumeBar.Location = new System.Drawing.Point(129, 24);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(81, 17);
            this.volumeBar.TabIndex = 203;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeToolTip.SetToolTip(this.volumeBar, "Adjust Volume");
            this.volumeBar.Value = 90;
            this.volumeBar.ValueChanged += new System.EventHandler(this.volumeBar_ValueChanged);
            this.volumeBar.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.volumeBar.MouseHover += new System.EventHandler(this.MouseHover);
            this.volumeBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.volumeBar_MouseUp);
            // 
            // audioControllerPanel
            // 
            this.audioControllerPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.audioControllerPanel.Controls.Add(this.audioPosTrackBar);
            this.audioControllerPanel.Controls.Add(this.playPauseButton);
            this.audioControllerPanel.Controls.Add(this.volumeBar);
            this.audioControllerPanel.Controls.Add(this.previousBox);
            this.audioControllerPanel.Controls.Add(this.volumeIconBox);
            this.audioControllerPanel.Controls.Add(this.nextBox);
            this.audioControllerPanel.Controls.Add(this.durationLabel);
            this.audioControllerPanel.Controls.Add(this.vizButton);
            this.audioControllerPanel.Controls.Add(this.shuffleButton);
            this.audioControllerPanel.Controls.Add(this.queueButton);
            this.audioControllerPanel.Location = new System.Drawing.Point(0, 365);
            this.audioControllerPanel.Name = "audioControllerPanel";
            this.audioControllerPanel.Size = new System.Drawing.Size(525, 47);
            this.audioControllerPanel.TabIndex = 204;
            // 
            // audioPosTrackBar
            // 
            this.audioPosTrackBar.AutoSize = false;
            this.audioPosTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.audioPosTrackBar.Enabled = false;
            this.audioPosTrackBar.Location = new System.Drawing.Point(0, 0);
            this.audioPosTrackBar.Maximum = 100;
            this.audioPosTrackBar.Name = "audioPosTrackBar";
            this.audioPosTrackBar.Size = new System.Drawing.Size(525, 16);
            this.audioPosTrackBar.TabIndex = 204;
            this.audioPosTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeToolTip.SetToolTip(this.audioPosTrackBar, "Seek");
            this.audioPosTrackBar.ValueChanged += new System.EventHandler(this.audioPosTrackBar_ValueChanged);
            this.audioPosTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.audioPosTrackBar_MouseDown);
            this.audioPosTrackBar.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.audioPosTrackBar.MouseHover += new System.EventHandler(this.MouseHover);
            this.audioPosTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.audioPosTrackBar_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // albumPanel
            // 
            this.albumPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.albumPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.albumPanel.Controls.Add(this.libraryAlbumArtBox);
            this.albumPanel.Controls.Add(this.backBox);
            this.albumPanel.Controls.Add(this.treeView);
            this.albumPanel.Location = new System.Drawing.Point(532, 47);
            this.albumPanel.Name = "albumPanel";
            this.albumPanel.Size = new System.Drawing.Size(264, 318);
            this.albumPanel.TabIndex = 205;
            this.albumPanel.Visible = false;
            // 
            // libraryAlbumArtBox
            // 
            this.libraryAlbumArtBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.libraryAlbumArtBox.Location = new System.Drawing.Point(82, 17);
            this.libraryAlbumArtBox.Name = "libraryAlbumArtBox";
            this.libraryAlbumArtBox.Size = new System.Drawing.Size(100, 95);
            this.libraryAlbumArtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.libraryAlbumArtBox.TabIndex = 3;
            this.libraryAlbumArtBox.TabStop = false;
            this.libraryAlbumArtBox.Visible = false;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Location = new System.Drawing.Point(0, 136);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(260, 177);
            this.treeView.TabIndex = 0;
            this.treeView.Visible = false;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // noLibraryLabel
            // 
            this.noLibraryLabel.AutoSize = true;
            this.noLibraryLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.noLibraryLabel.Location = new System.Drawing.Point(605, 58);
            this.noLibraryLabel.Name = "noLibraryLabel";
            this.noLibraryLabel.Size = new System.Drawing.Size(118, 13);
            this.noLibraryLabel.TabIndex = 206;
            this.noLibraryLabel.Text = "No Library Connected";
            this.noLibraryLabel.Visible = false;
            // 
            // albumArtBox
            // 
            this.albumArtBox.BackColor = System.Drawing.Color.Black;
            this.albumArtBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.albumArtBox.ErrorImage = null;
            this.albumArtBox.InitialImage = null;
            this.albumArtBox.Location = new System.Drawing.Point(0, 49);
            this.albumArtBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.albumArtBox.Name = "albumArtBox";
            this.albumArtBox.Size = new System.Drawing.Size(73, 73);
            this.albumArtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumArtBox.TabIndex = 10;
            this.albumArtBox.TabStop = false;
            this.albumArtBox.Click += new System.EventHandler(this.albumArtBox_Click);
            this.albumArtBox.DoubleClick += new System.EventHandler(this.albumArtBox_DoubleClick);
            // 
            // clearBox
            // 
            this.clearBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clearBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clearBox.ErrorImage = null;
            this.clearBox.Image = global::Music_Player.Properties.Resources.clear;
            this.clearBox.InitialImage = null;
            this.clearBox.Location = new System.Drawing.Point(695, 398);
            this.clearBox.Name = "clearBox";
            this.clearBox.Size = new System.Drawing.Size(19, 19);
            this.clearBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.clearBox.TabIndex = 30;
            this.clearBox.TabStop = false;
            this.clearBox.Visible = false;
            this.clearBox.Click += new System.EventHandler(this.clearBox_Click);
            this.clearBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.clearBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // WindowsMediaPlayer
            // 
            this.WindowsMediaPlayer.Enabled = true;
            this.WindowsMediaPlayer.Location = new System.Drawing.Point(0, 49);
            this.WindowsMediaPlayer.Name = "WindowsMediaPlayer";
            this.WindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WindowsMediaPlayer.OcxState")));
            this.WindowsMediaPlayer.Size = new System.Drawing.Size(525, 363);
            this.WindowsMediaPlayer.TabIndex = 200;
            this.WindowsMediaPlayer.TabStop = false;
            this.WindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.WindowsMediaPlayer_PlayStateChange);
            this.WindowsMediaPlayer.MediaChange += new AxWMPLib._WMPOCXEvents_MediaChangeEventHandler(this.WindowsMediaPlayer_MediaChange);
            this.WindowsMediaPlayer.KeyUpEvent += new AxWMPLib._WMPOCXEvents_KeyUpEventHandler(this.VideoContainer_KeyUp);
            // 
            // configureLabel
            // 
            this.configureLabel.AutoSize = true;
            this.configureLabel.Location = new System.Drawing.Point(273, 425);
            this.configureLabel.Name = "configureLabel";
            this.configureLabel.Size = new System.Drawing.Size(74, 13);
            this.configureLabel.TabIndex = 207;
            this.configureLabel.Text = "👈 Configure";
            this.configureLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(796, 454);
            this.Controls.Add(this.configureLabel);
            this.Controls.Add(this.noLibraryLabel);
            this.Controls.Add(this.albumPanel);
            this.Controls.Add(this.audioControllerPanel);
            this.Controls.Add(this.settingsBox);
            this.Controls.Add(this.clearBox);
            this.Controls.Add(this.deviceBox);
            this.Controls.Add(this.eqButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.clearQueueButton);
            this.Controls.Add(this.openplayButton);
            this.Controls.Add(this.addtoplaylistButton);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.playlistLabel);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.playlistBox);
            this.Controls.Add(this.queueLabel);
            this.Controls.Add(this.albumArtBox);
            this.Controls.Add(this.librarylistBox);
            this.Controls.Add(this.WindowsMediaPlayer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.githublinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playPauseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeIconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.audioControllerPanel.ResumeLayout(false);
            this.audioControllerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.audioPosTrackBar)).EndInit();
            this.albumPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.libraryAlbumArtBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox librarylistBox;
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
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button eqButton;
        private System.Windows.Forms.ComboBox deviceBox;
        private System.Windows.Forms.PictureBox closeBox;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.ToolTip prevToolTip;
        private System.Windows.Forms.ToolTip nextToolTip;
        private System.Windows.Forms.ToolTip closeToolTip;
        private System.Windows.Forms.ToolTip minimizeToolTip;
        private System.Windows.Forms.PictureBox clearBox;
        private System.Windows.Forms.PictureBox githublinkBox;
        private System.Windows.Forms.PictureBox settingsBox;
        public AxWMPLib.AxWindowsMediaPlayer WindowsMediaPlayer;
        private System.Windows.Forms.PictureBox playPauseButton;
        private System.Windows.Forms.PictureBox volumeIconBox;
        private System.Windows.Forms.Panel audioControllerPanel;
        private System.Windows.Forms.ToolTip volumeToolTip;
        public System.Windows.Forms.TrackBar audioPosTrackBar;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Panel albumPanel;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PictureBox backBox;
        private System.Windows.Forms.PictureBox libraryAlbumArtBox;
        private System.Windows.Forms.Label noLibraryLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label configureLabel;
    }
}

