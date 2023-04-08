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
            this.refreshButton = new System.Windows.Forms.Button();
            this.usageLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.minibox = new System.Windows.Forms.PictureBox();
            this.codelinkBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.soundcloudButton = new System.Windows.Forms.Button();
            this.spotifyButton = new System.Windows.Forms.Button();
            this.youtubeButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Enabled = false;
            this.refreshButton.Location = new System.Drawing.Point(489, 268);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.toolTip1.SetToolTip(this.refreshButton, "Refresh Changes ");
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            this.refreshButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.refreshButton.MouseHover += new System.EventHandler(this.MouseHover);
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
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
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
            // soundcloudButton
            // 
            this.soundcloudButton.Location = new System.Drawing.Point(10, 268);
            this.soundcloudButton.Name = "soundcloudButton";
            this.soundcloudButton.Size = new System.Drawing.Size(87, 23);
            this.soundcloudButton.TabIndex = 34;
            this.soundcloudButton.Text = "Soundcloud";
            this.soundcloudButton.UseVisualStyleBackColor = true;
            this.soundcloudButton.Click += new System.EventHandler(this.soundcloudButton_Click);
            // 
            // spotifyButton
            // 
            this.spotifyButton.Location = new System.Drawing.Point(103, 268);
            this.spotifyButton.Name = "spotifyButton";
            this.spotifyButton.Size = new System.Drawing.Size(87, 23);
            this.spotifyButton.TabIndex = 35;
            this.spotifyButton.Text = "Spotify";
            this.spotifyButton.UseVisualStyleBackColor = true;
            this.spotifyButton.Click += new System.EventHandler(this.spotifyButton_Click);
            // 
            // youtubeButton
            // 
            this.youtubeButton.Location = new System.Drawing.Point(196, 268);
            this.youtubeButton.Name = "youtubeButton";
            this.youtubeButton.Size = new System.Drawing.Size(75, 23);
            this.youtubeButton.TabIndex = 36;
            this.youtubeButton.Text = "YouTube";
            this.youtubeButton.UseVisualStyleBackColor = true;
            this.youtubeButton.Click += new System.EventHandler(this.youtubeButton_Click);
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
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(408, 268);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 38;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Visible = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 312);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.youtubeButton);
            this.Controls.Add(this.spotifyButton);
            this.Controls.Add(this.soundcloudButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.usageLabel);
            this.Controls.Add(this.refreshButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label usageLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox codelinkBox;
        private System.Windows.Forms.PictureBox closeBox;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.PictureBox exitBox;
        private System.Windows.Forms.PictureBox minibox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button soundcloudButton;
        private System.Windows.Forms.Button spotifyButton;
        private System.Windows.Forms.Button youtubeButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchButton;
    }
}