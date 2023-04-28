namespace Music_Player
{
    partial class Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.directorytextBox = new System.Windows.Forms.TextBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.formiconBox = new System.Windows.Forms.PictureBox();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.minibox = new System.Windows.Forms.PictureBox();
            this.codelinkBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.goLabel = new System.Windows.Forms.Label();
            this.linkBox = new System.Windows.Forms.ComboBox();
            this.locateButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.filefinderPanel = new System.Windows.Forms.Panel();
            this.hintLabel = new System.Windows.Forms.Label();
            this.directorieslistBox = new System.Windows.Forms.ListBox();
            this.backBox = new System.Windows.Forms.PictureBox();
            this.searchingLabel = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.MinimizetoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ClosetoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.configPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.newpassBox = new System.Windows.Forms.TextBox();
            this.newpassLabel = new System.Windows.Forms.Label();
            this.oldpassBox = new System.Windows.Forms.TextBox();
            this.oldpassLabel = new System.Windows.Forms.Label();
            this.passBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.encryptcheckBox = new System.Windows.Forms.CheckBox();
            this.libraryBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.encryptionLabel = new System.Windows.Forms.Label();
            this.startupfolderLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formiconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            this.filefinderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).BeginInit();
            this.configPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // directorytextBox
            // 
            this.directorytextBox.Location = new System.Drawing.Point(70, 8);
            this.directorytextBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.directorytextBox.Name = "directorytextBox";
            this.directorytextBox.Size = new System.Drawing.Size(100, 22);
            this.directorytextBox.TabIndex = 1;
            this.toolTip1.SetToolTip(this.directorytextBox, "Directory hint");
            this.directorytextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.directorytextBox_KeyPress);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.formiconBox);
            this.topPanel.Controls.Add(this.exitBox);
            this.topPanel.Controls.Add(this.minibox);
            this.topPanel.Controls.Add(this.codelinkBox);
            this.topPanel.Controls.Add(this.closeBox);
            this.topPanel.Controls.Add(this.minimizeBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(576, 26);
            this.topPanel.TabIndex = 27;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp);
            // 
            // formiconBox
            // 
            this.formiconBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.formiconBox.ErrorImage = null;
            this.formiconBox.Image = global::Music_Player.Properties.Resources.settings;
            this.formiconBox.InitialImage = null;
            this.formiconBox.Location = new System.Drawing.Point(3, 4);
            this.formiconBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.formiconBox.Name = "formiconBox";
            this.formiconBox.Size = new System.Drawing.Size(13, 19);
            this.formiconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.formiconBox.TabIndex = 5;
            this.formiconBox.TabStop = false;
            this.MinimizetoolTip.SetToolTip(this.formiconBox, "Minimize");
            // 
            // exitBox
            // 
            this.exitBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exitBox.ErrorImage = null;
            this.exitBox.Image = global::Music_Player.Properties.Resources.close;
            this.exitBox.InitialImage = null;
            this.exitBox.Location = new System.Drawing.Point(547, 1);
            this.exitBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.exitBox.Name = "exitBox";
            this.exitBox.Size = new System.Drawing.Size(26, 22);
            this.exitBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exitBox.TabIndex = 4;
            this.exitBox.TabStop = false;
            this.ClosetoolTip.SetToolTip(this.exitBox, "Close");
            this.exitBox.Click += new System.EventHandler(this.exitBox_Click);
            this.exitBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.exitBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // minibox
            // 
            this.minibox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minibox.ErrorImage = null;
            this.minibox.Image = global::Music_Player.Properties.Resources.minimize;
            this.minibox.InitialImage = null;
            this.minibox.Location = new System.Drawing.Point(516, 1);
            this.minibox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.minibox.Name = "minibox";
            this.minibox.Size = new System.Drawing.Size(31, 22);
            this.minibox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minibox.TabIndex = 3;
            this.minibox.TabStop = false;
            this.MinimizetoolTip.SetToolTip(this.minibox, "Minimize");
            this.minibox.Click += new System.EventHandler(this.minibox_Click);
            this.minibox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.minibox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // codelinkBox
            // 
            this.codelinkBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.codelinkBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.codelinkBox.ErrorImage = null;
            this.codelinkBox.Image = ((System.Drawing.Image)(resources.GetObject("codelinkBox.Image")));
            this.codelinkBox.InitialImage = null;
            this.codelinkBox.Location = new System.Drawing.Point(709, 1);
            this.codelinkBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.codelinkBox.Name = "codelinkBox";
            this.codelinkBox.Size = new System.Drawing.Size(23, 22);
            this.codelinkBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.codelinkBox.TabIndex = 2;
            this.codelinkBox.TabStop = false;
            // 
            // closeBox
            // 
            this.closeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeBox.ErrorImage = null;
            this.closeBox.Image = ((System.Drawing.Image)(resources.GetObject("closeBox.Image")));
            this.closeBox.InitialImage = null;
            this.closeBox.Location = new System.Drawing.Point(766, 1);
            this.closeBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(26, 22);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeBox.TabIndex = 1;
            this.closeBox.TabStop = false;
            // 
            // minimizeBox
            // 
            this.minimizeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimizeBox.ErrorImage = null;
            this.minimizeBox.Image = ((System.Drawing.Image)(resources.GetObject("minimizeBox.Image")));
            this.minimizeBox.InitialImage = null;
            this.minimizeBox.Location = new System.Drawing.Point(735, 1);
            this.minimizeBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.minimizeBox.Name = "minimizeBox";
            this.minimizeBox.Size = new System.Drawing.Size(31, 22);
            this.minimizeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizeBox.TabIndex = 0;
            this.minimizeBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 10);
            this.panel1.TabIndex = 32;
            // 
            // goLabel
            // 
            this.goLabel.AutoSize = true;
            this.goLabel.Location = new System.Drawing.Point(25, 275);
            this.goLabel.Name = "goLabel";
            this.goLabel.Size = new System.Drawing.Size(40, 13);
            this.goLabel.TabIndex = 39;
            this.goLabel.Text = "Go To:";
            // 
            // linkBox
            // 
            this.linkBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.linkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.linkBox.FormattingEnabled = true;
            this.linkBox.Items.AddRange(new object[] {
            "SoundCloud",
            "Spotify",
            "Youtube"});
            this.linkBox.Location = new System.Drawing.Point(71, 271);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(95, 21);
            this.linkBox.Sorted = true;
            this.linkBox.TabIndex = 40;
            this.linkBox.TabStop = false;
            this.linkBox.SelectedIndexChanged += new System.EventHandler(this.linkBox_SelectedIndexChanged);
            // 
            // locateButton
            // 
            this.locateButton.Location = new System.Drawing.Point(230, 271);
            this.locateButton.Name = "locateButton";
            this.locateButton.Size = new System.Drawing.Size(114, 22);
            this.locateButton.TabIndex = 42;
            this.locateButton.Text = "Locate Audio Files";
            this.locateButton.UseVisualStyleBackColor = true;
            this.locateButton.Click += new System.EventHandler(this.locateButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Enabled = false;
            this.refreshButton.Location = new System.Drawing.Point(478, 271);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(84, 22);
            this.refreshButton.TabIndex = 43;
            this.refreshButton.TabStop = false;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            this.refreshButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.refreshButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(404, 271);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(68, 22);
            this.searchButton.TabIndex = 44;
            this.searchButton.TabStop = false;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Visible = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            this.searchButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.searchButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // filefinderPanel
            // 
            this.filefinderPanel.Controls.Add(this.directorytextBox);
            this.filefinderPanel.Controls.Add(this.hintLabel);
            this.filefinderPanel.Location = new System.Drawing.Point(198, 264);
            this.filefinderPanel.Name = "filefinderPanel";
            this.filefinderPanel.Size = new System.Drawing.Size(182, 32);
            this.filefinderPanel.TabIndex = 45;
            this.filefinderPanel.Visible = false;
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Location = new System.Drawing.Point(14, 12);
            this.hintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(56, 13);
            this.hintLabel.TabIndex = 0;
            this.hintLabel.Text = " C:\\Users\\";
            // 
            // directorieslistBox
            // 
            this.directorieslistBox.FormattingEnabled = true;
            this.directorieslistBox.Location = new System.Drawing.Point(7, 96);
            this.directorieslistBox.Name = "directorieslistBox";
            this.directorieslistBox.ScrollAlwaysVisible = true;
            this.directorieslistBox.Size = new System.Drawing.Size(555, 212);
            this.directorieslistBox.TabIndex = 46;
            this.directorieslistBox.Visible = false;
            this.directorieslistBox.DoubleClick += new System.EventHandler(this.directorieslistBox_DoubleClick);
            // 
            // backBox
            // 
            this.backBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.backBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backBox.ErrorImage = null;
            this.backBox.Image = global::Music_Player.Properties.Resources.backbutton;
            this.backBox.InitialImage = null;
            this.backBox.Location = new System.Drawing.Point(509, 9);
            this.backBox.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.backBox.Name = "backBox";
            this.backBox.Size = new System.Drawing.Size(25, 22);
            this.backBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backBox.TabIndex = 47;
            this.backBox.TabStop = false;
            this.toolTip2.SetToolTip(this.backBox, "To Config");
            this.backBox.Visible = false;
            this.backBox.Click += new System.EventHandler(this.backBox_Click);
            this.backBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.backBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // searchingLabel
            // 
            this.searchingLabel.AutoSize = true;
            this.searchingLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchingLabel.Location = new System.Drawing.Point(231, 96);
            this.searchingLabel.Name = "searchingLabel";
            this.searchingLabel.Size = new System.Drawing.Size(67, 13);
            this.searchingLabel.TabIndex = 49;
            this.searchingLabel.Text = "Searching...";
            this.searchingLabel.Visible = false;
            // 
            // configPanel
            // 
            this.configPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.configPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configPanel.Controls.Add(this.saveButton);
            this.configPanel.Controls.Add(this.directorieslistBox);
            this.configPanel.Controls.Add(this.newpassBox);
            this.configPanel.Controls.Add(this.newpassLabel);
            this.configPanel.Controls.Add(this.oldpassBox);
            this.configPanel.Controls.Add(this.oldpassLabel);
            this.configPanel.Controls.Add(this.passBox);
            this.configPanel.Controls.Add(this.passwordLabel);
            this.configPanel.Controls.Add(this.encryptcheckBox);
            this.configPanel.Controls.Add(this.libraryBox);
            this.configPanel.Controls.Add(this.userBox);
            this.configPanel.Controls.Add(this.encryptionLabel);
            this.configPanel.Controls.Add(this.startupfolderLabel);
            this.configPanel.Controls.Add(this.usernameLabel);
            this.configPanel.Controls.Add(this.searchingLabel);
            this.configPanel.Controls.Add(this.backBox);
            this.configPanel.Location = new System.Drawing.Point(10, 55);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(552, 203);
            this.configPanel.TabIndex = 50;
            this.configPanel.Click += new System.EventHandler(this.configPanel_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(422, 126);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(55, 23);
            this.saveButton.TabIndex = 62;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // newpassBox
            // 
            this.newpassBox.Location = new System.Drawing.Point(316, 126);
            this.newpassBox.Name = "newpassBox";
            this.newpassBox.PasswordChar = '•';
            this.newpassBox.Size = new System.Drawing.Size(100, 22);
            this.newpassBox.TabIndex = 61;
            this.newpassBox.Visible = false;
            this.newpassBox.TextChanged += new System.EventHandler(this.newpassBox_TextChanged);
            // 
            // newpassLabel
            // 
            this.newpassLabel.AutoSize = true;
            this.newpassLabel.Location = new System.Drawing.Point(225, 129);
            this.newpassLabel.Name = "newpassLabel";
            this.newpassLabel.Size = new System.Drawing.Size(85, 13);
            this.newpassLabel.TabIndex = 60;
            this.newpassLabel.Text = "New Password:";
            this.newpassLabel.Visible = false;
            // 
            // oldpassBox
            // 
            this.oldpassBox.Location = new System.Drawing.Point(102, 126);
            this.oldpassBox.Name = "oldpassBox";
            this.oldpassBox.PasswordChar = '•';
            this.oldpassBox.Size = new System.Drawing.Size(100, 22);
            this.oldpassBox.TabIndex = 59;
            this.oldpassBox.Visible = false;
            // 
            // oldpassLabel
            // 
            this.oldpassLabel.AutoSize = true;
            this.oldpassLabel.Location = new System.Drawing.Point(15, 129);
            this.oldpassLabel.Name = "oldpassLabel";
            this.oldpassLabel.Size = new System.Drawing.Size(81, 13);
            this.oldpassLabel.TabIndex = 58;
            this.oldpassLabel.Text = "Old Password:";
            this.oldpassLabel.Visible = false;
            // 
            // passBox
            // 
            this.passBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.passBox.Enabled = false;
            this.passBox.Location = new System.Drawing.Point(127, 40);
            this.passBox.Name = "passBox";
            this.passBox.ReadOnly = true;
            this.passBox.Size = new System.Drawing.Size(420, 22);
            this.passBox.TabIndex = 57;
            this.passBox.Click += new System.EventHandler(this.passBox_Click);
            this.passBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.passBox_MouseDoubleClick);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(14, 43);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(107, 13);
            this.passwordLabel.TabIndex = 56;
            this.passwordLabel.Text = "Password (Hashed):";
            // 
            // encryptcheckBox
            // 
            this.encryptcheckBox.AutoSize = true;
            this.encryptcheckBox.Location = new System.Drawing.Point(102, 86);
            this.encryptcheckBox.Name = "encryptcheckBox";
            this.encryptcheckBox.Size = new System.Drawing.Size(15, 14);
            this.encryptcheckBox.TabIndex = 55;
            this.encryptcheckBox.UseVisualStyleBackColor = true;
            this.encryptcheckBox.CheckedChanged += new System.EventHandler(this.encryptcheckBox_CheckedChanged);
            // 
            // libraryBox
            // 
            this.libraryBox.Enabled = false;
            this.libraryBox.Location = new System.Drawing.Point(138, 62);
            this.libraryBox.Name = "libraryBox";
            this.libraryBox.Size = new System.Drawing.Size(409, 22);
            this.libraryBox.TabIndex = 54;
            this.libraryBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.libraryBox_MouseClick);
            this.libraryBox.TextChanged += new System.EventHandler(this.libraryBox_TextChanged);
            // 
            // userBox
            // 
            this.userBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.userBox.Enabled = false;
            this.userBox.Location = new System.Drawing.Point(81, 18);
            this.userBox.Name = "userBox";
            this.userBox.ReadOnly = true;
            this.userBox.Size = new System.Drawing.Size(410, 22);
            this.userBox.TabIndex = 53;
            // 
            // encryptionLabel
            // 
            this.encryptionLabel.AutoSize = true;
            this.encryptionLabel.Location = new System.Drawing.Point(14, 85);
            this.encryptionLabel.Name = "encryptionLabel";
            this.encryptionLabel.Size = new System.Drawing.Size(86, 13);
            this.encryptionLabel.TabIndex = 52;
            this.encryptionLabel.Text = "Encrypt on exit:";
            // 
            // startupfolderLabel
            // 
            this.startupfolderLabel.AutoSize = true;
            this.startupfolderLabel.Location = new System.Drawing.Point(14, 65);
            this.startupfolderLabel.Name = "startupfolderLabel";
            this.startupfolderLabel.Size = new System.Drawing.Size(118, 13);
            this.startupfolderLabel.TabIndex = 51;
            this.startupfolderLabel.Text = "Default Music Library:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(14, 21);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 50;
            this.usernameLabel.Text = "Username:";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(10, 32);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(47, 23);
            this.editButton.TabIndex = 0;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(447, 32);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(117, 23);
            this.downloadButton.TabIndex = 52;
            this.downloadButton.TabStop = false;
            this.downloadButton.Text = "Download Queue";
            this.downloadButton.UseVisualStyleBackColor = true;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 312);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.filefinderPanel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.locateButton);
            this.Controls.Add(this.linkBox);
            this.Controls.Add(this.goLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            this.Click += new System.EventHandler(this.Config_Click);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formiconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            this.filefinderPanel.ResumeLayout(false);
            this.filefinderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).EndInit();
            this.configPanel.ResumeLayout(false);
            this.configPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox codelinkBox;
        private System.Windows.Forms.PictureBox closeBox;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.PictureBox exitBox;
        private System.Windows.Forms.PictureBox minibox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label goLabel;
        private System.Windows.Forms.ComboBox linkBox;
        private System.Windows.Forms.Button locateButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel filefinderPanel;
        private System.Windows.Forms.TextBox directorytextBox;
        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.ListBox directorieslistBox;
        private System.Windows.Forms.PictureBox backBox;
        private System.Windows.Forms.Label searchingLabel;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip ClosetoolTip;
        private System.Windows.Forms.ToolTip MinimizetoolTip;
        private System.Windows.Forms.PictureBox formiconBox;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.Label startupfolderLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.CheckBox encryptcheckBox;
        private System.Windows.Forms.TextBox libraryBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label encryptionLabel;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.TextBox newpassBox;
        private System.Windows.Forms.Label newpassLabel;
        private System.Windows.Forms.TextBox oldpassBox;
        private System.Windows.Forms.Label oldpassLabel;
        private System.Windows.Forms.Button saveButton;
    }
}