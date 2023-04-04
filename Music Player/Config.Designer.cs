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
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.editButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.usageLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.codelinkBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.minibox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).BeginInit();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(10, 55);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(554, 207);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Option";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 700;
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(408, 268);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            this.editButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.editButton.MouseHover += new System.EventHandler(this.MouseHover);
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
            this.usageLabel.Size = new System.Drawing.Size(277, 13);
            this.usageLabel.TabIndex = 3;
            this.usageLabel.Text = "Double click on any of the options to enable editing";
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
            // exitBox
            // 
            this.exitBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exitBox.ErrorImage = null;
            this.exitBox.Image = ((System.Drawing.Image)(resources.GetObject("exitBox.Image")));
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
            this.minibox.Image = ((System.Drawing.Image)(resources.GetObject("minibox.Image")));
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 10);
            this.panel1.TabIndex = 32;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 312);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.usageLabel);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.listView);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minibox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button editButton;
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
    }
}