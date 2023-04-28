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
            this.usageLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.directorytextBox = new System.Windows.Forms.TextBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.minibox = new System.Windows.Forms.PictureBox();
            this.codelinkBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.goLabel = new System.Windows.Forms.Label();
            this.linkBox = new System.Windows.Forms.ComboBox();
            this.goButton = new System.Windows.Forms.Button();
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.filefinderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // usageLabel
            // 
            this.usageLabel.AutoSize = true;
            this.usageLabel.Location = new System.Drawing.Point(7, 39);
            this.usageLabel.Name = "usageLabel";
            this.usageLabel.Size = new System.Drawing.Size(168, 13);
            this.usageLabel.TabIndex = 3;
            this.usageLabel.Text = "Click in the grid to start editing";
            // 
            // directorytextBox
            // 
            this.directorytextBox.Location = new System.Drawing.Point(88, 8);
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
            this.topPanel.Controls.Add(this.pictureBox1);
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
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(10, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(554, 207);
            this.dataGridView1.TabIndex = 37;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // goLabel
            // 
            this.goLabel.AutoSize = true;
            this.goLabel.Location = new System.Drawing.Point(12, 279);
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
            this.linkBox.Location = new System.Drawing.Point(58, 275);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(95, 21);
            this.linkBox.Sorted = true;
            this.linkBox.TabIndex = 40;
            this.linkBox.SelectedIndexChanged += new System.EventHandler(this.linkBox_SelectedIndexChanged);
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(159, 275);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(33, 22);
            this.goButton.TabIndex = 41;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Visible = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            this.goButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.goButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // locateButton
            // 
            this.locateButton.Location = new System.Drawing.Point(243, 275);
            this.locateButton.Name = "locateButton";
            this.locateButton.Size = new System.Drawing.Size(114, 22);
            this.locateButton.TabIndex = 42;
            this.locateButton.Text = "Locate Audio Files";
            this.locateButton.UseVisualStyleBackColor = true;
            this.locateButton.Click += new System.EventHandler(this.locateButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(479, 275);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(84, 22);
            this.refreshButton.TabIndex = 43;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            this.refreshButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.refreshButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(405, 275);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(68, 22);
            this.searchButton.TabIndex = 44;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            this.searchButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.searchButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // filefinderPanel
            // 
            this.filefinderPanel.Controls.Add(this.directorytextBox);
            this.filefinderPanel.Controls.Add(this.hintLabel);
            this.filefinderPanel.Location = new System.Drawing.Point(198, 267);
            this.filefinderPanel.Name = "filefinderPanel";
            this.filefinderPanel.Size = new System.Drawing.Size(201, 32);
            this.filefinderPanel.TabIndex = 45;
            this.filefinderPanel.Visible = false;
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Location = new System.Drawing.Point(14, 12);
            this.hintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(74, 13);
            this.hintLabel.TabIndex = 0;
            this.hintLabel.Text = "👉   C:\\Users\\";
            // 
            // directorieslistBox
            // 
            this.directorieslistBox.FormattingEnabled = true;
            this.directorieslistBox.Location = new System.Drawing.Point(9, 55);
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
            this.backBox.Location = new System.Drawing.Point(520, 59);
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
            this.searchingLabel.Location = new System.Drawing.Point(250, 152);
            this.searchingLabel.Name = "searchingLabel";
            this.searchingLabel.Size = new System.Drawing.Size(67, 13);
            this.searchingLabel.TabIndex = 49;
            this.searchingLabel.Text = "Searching...";
            this.searchingLabel.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Music_Player.Properties.Resources.settings;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(13, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.MinimizetoolTip.SetToolTip(this.pictureBox1, "Minimize");
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 312);
            this.Controls.Add(this.searchingLabel);
            this.Controls.Add(this.backBox);
            this.Controls.Add(this.directorieslistBox);
            this.Controls.Add(this.filefinderPanel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.locateButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.linkBox);
            this.Controls.Add(this.goLabel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.usageLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            this.Click += new System.EventHandler(this.Config_Click);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.filefinderPanel.ResumeLayout(false);
            this.filefinderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label usageLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox codelinkBox;
        private System.Windows.Forms.PictureBox closeBox;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.PictureBox exitBox;
        private System.Windows.Forms.PictureBox minibox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label goLabel;
        private System.Windows.Forms.ComboBox linkBox;
        private System.Windows.Forms.Button goButton;
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
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}