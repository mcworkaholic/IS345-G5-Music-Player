namespace Music_Player
{
    partial class loginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginForm));
            this.usertextBox = new System.Windows.Forms.TextBox();
            this.passwordtextBox = new System.Windows.Forms.TextBox();
            this.confirmtextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.confirmLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.usrerrBox = new System.Windows.Forms.PictureBox();
            this.cfrmpwderrBox = new System.Windows.Forms.PictureBox();
            this.confirmPanel = new System.Windows.Forms.Panel();
            this.cnfmView = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.pwderrBox = new System.Windows.Forms.PictureBox();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.passView = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.codelinkBox = new System.Windows.Forms.PictureBox();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.miniBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.passwordPanel = new System.Windows.Forms.Panel();
            this.usernamePanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfrmpwderrBox)).BeginInit();
            this.confirmPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniBox)).BeginInit();
            this.passwordPanel.SuspendLayout();
            this.usernamePanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // usertextBox
            // 
            this.usertextBox.Location = new System.Drawing.Point(114, 3);
            this.usertextBox.Name = "usertextBox";
            this.usertextBox.Size = new System.Drawing.Size(100, 22);
            this.usertextBox.TabIndex = 0;
            // 
            // passwordtextBox
            // 
            this.passwordtextBox.Location = new System.Drawing.Point(114, 4);
            this.passwordtextBox.Name = "passwordtextBox";
            this.passwordtextBox.Size = new System.Drawing.Size(100, 22);
            this.passwordtextBox.TabIndex = 1;
            this.passwordtextBox.TextChanged += new System.EventHandler(this.passwordtextBox_TextChanged);
            // 
            // confirmtextBox
            // 
            this.confirmtextBox.Location = new System.Drawing.Point(114, 3);
            this.confirmtextBox.Name = "confirmtextBox";
            this.confirmtextBox.Size = new System.Drawing.Size(100, 22);
            this.confirmtextBox.TabIndex = 2;
            this.confirmtextBox.TextChanged += new System.EventHandler(this.confirmtextBox_TextChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(50, 6);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(52, 7);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password:";
            // 
            // confirmLabel
            // 
            this.confirmLabel.AutoSize = true;
            this.confirmLabel.Location = new System.Drawing.Point(8, 8);
            this.confirmLabel.Name = "confirmLabel";
            this.confirmLabel.Size = new System.Drawing.Size(103, 13);
            this.confirmLabel.TabIndex = 5;
            this.confirmLabel.Text = "Confirm Password:";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(128, 1);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(87, 23);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "Create Profile";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            this.createButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.createButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(47, 1);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            this.loginButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.loginButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // usrerrBox
            // 
            this.usrerrBox.ErrorImage = null;
            this.usrerrBox.Image = ((System.Drawing.Image)(resources.GetObject("usrerrBox.Image")));
            this.usrerrBox.Location = new System.Drawing.Point(220, 5);
            this.usrerrBox.Name = "usrerrBox";
            this.usrerrBox.Size = new System.Drawing.Size(19, 20);
            this.usrerrBox.TabIndex = 9;
            this.usrerrBox.TabStop = false;
            this.usrerrBox.Visible = false;
            // 
            // cfrmpwderrBox
            // 
            this.cfrmpwderrBox.Image = ((System.Drawing.Image)(resources.GetObject("cfrmpwderrBox.Image")));
            this.cfrmpwderrBox.Location = new System.Drawing.Point(220, 5);
            this.cfrmpwderrBox.Name = "cfrmpwderrBox";
            this.cfrmpwderrBox.Size = new System.Drawing.Size(19, 20);
            this.cfrmpwderrBox.TabIndex = 10;
            this.cfrmpwderrBox.TabStop = false;
            this.cfrmpwderrBox.Visible = false;
            // 
            // confirmPanel
            // 
            this.confirmPanel.Controls.Add(this.cnfmView);
            this.confirmPanel.Controls.Add(this.confirmLabel);
            this.confirmPanel.Controls.Add(this.cfrmpwderrBox);
            this.confirmPanel.Controls.Add(this.confirmtextBox);
            this.confirmPanel.Location = new System.Drawing.Point(3, 69);
            this.confirmPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.confirmPanel.Name = "confirmPanel";
            this.confirmPanel.Size = new System.Drawing.Size(270, 29);
            this.confirmPanel.TabIndex = 2;
            this.confirmPanel.Visible = false;
            // 
            // cnfmView
            // 
            this.cnfmView.Location = new System.Drawing.Point(216, 4);
            this.cnfmView.Name = "cnfmView";
            this.cnfmView.Size = new System.Drawing.Size(23, 21);
            this.cnfmView.TabIndex = 16;
            this.cnfmView.TabStop = false;
            this.cnfmView.Text = "👁️‍🗨️";
            this.cnfmView.UseVisualStyleBackColor = true;
            this.cnfmView.Visible = false;
            this.cnfmView.Click += new System.EventHandler(this.cnfmView_Click);
            this.cnfmView.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.cnfmView.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.backButton);
            this.buttonPanel.Controls.Add(this.loginButton);
            this.buttonPanel.Controls.Add(this.createButton);
            this.buttonPanel.Location = new System.Drawing.Point(34, 100);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(243, 27);
            this.buttonPanel.TabIndex = 3;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(66, 1);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(56, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "👈 Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Visible = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.backButton.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.backButton.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // pwderrBox
            // 
            this.pwderrBox.ErrorImage = null;
            this.pwderrBox.Image = ((System.Drawing.Image)(resources.GetObject("pwderrBox.Image")));
            this.pwderrBox.Location = new System.Drawing.Point(220, 6);
            this.pwderrBox.Name = "pwderrBox";
            this.pwderrBox.Size = new System.Drawing.Size(19, 20);
            this.pwderrBox.TabIndex = 13;
            this.pwderrBox.TabStop = false;
            this.pwderrBox.Visible = false;
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.Color.Transparent;
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.Location = new System.Drawing.Point(164, 51);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(144, 156);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 14;
            this.logoBox.TabStop = false;
            // 
            // passView
            // 
            this.passView.Location = new System.Drawing.Point(216, 4);
            this.passView.Name = "passView";
            this.passView.Size = new System.Drawing.Size(23, 21);
            this.passView.TabIndex = 15;
            this.passView.TabStop = false;
            this.passView.Text = "👁️‍🗨️";
            this.passView.UseVisualStyleBackColor = true;
            this.passView.Visible = false;
            this.passView.Click += new System.EventHandler(this.passView_Click);
            this.passView.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.passView.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.codelinkBox);
            this.topPanel.Controls.Add(this.exitBox);
            this.topPanel.Controls.Add(this.minimizeBox);
            this.topPanel.Controls.Add(this.miniBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(460, 26);
            this.topPanel.TabIndex = 27;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp);
            // 
            // codelinkBox
            // 
            this.codelinkBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.codelinkBox.ErrorImage = null;
            this.codelinkBox.Image = ((System.Drawing.Image)(resources.GetObject("codelinkBox.Image")));
            this.codelinkBox.InitialImage = null;
            this.codelinkBox.Location = new System.Drawing.Point(368, 4);
            this.codelinkBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.codelinkBox.Name = "codelinkBox";
            this.codelinkBox.Size = new System.Drawing.Size(23, 22);
            this.codelinkBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.codelinkBox.TabIndex = 4;
            this.codelinkBox.TabStop = false;
            this.toolTip3.SetToolTip(this.codelinkBox, "https://github.com/mcworkaholic/IS345-G5-Music-Player");
            this.codelinkBox.Click += new System.EventHandler(this.codelinkBox_Click);
            this.codelinkBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.codelinkBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // exitBox
            // 
            this.exitBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exitBox.ErrorImage = null;
            this.exitBox.Image = ((System.Drawing.Image)(resources.GetObject("exitBox.Image")));
            this.exitBox.InitialImage = null;
            this.exitBox.Location = new System.Drawing.Point(427, 3);
            this.exitBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.exitBox.Name = "exitBox";
            this.exitBox.Size = new System.Drawing.Size(30, 23);
            this.exitBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exitBox.TabIndex = 3;
            this.exitBox.TabStop = false;
            this.toolTip2.SetToolTip(this.exitBox, "Close");
            this.exitBox.Click += new System.EventHandler(this.exitBox_Click);
            this.exitBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.exitBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // minimizeBox
            // 
            this.minimizeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimizeBox.ErrorImage = null;
            this.minimizeBox.Image = ((System.Drawing.Image)(resources.GetObject("minimizeBox.Image")));
            this.minimizeBox.InitialImage = null;
            this.minimizeBox.Location = new System.Drawing.Point(743, 4);
            this.minimizeBox.Name = "minimizeBox";
            this.minimizeBox.Size = new System.Drawing.Size(17, 17);
            this.minimizeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizeBox.TabIndex = 0;
            this.minimizeBox.TabStop = false;
            // 
            // miniBox
            // 
            this.miniBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.miniBox.ErrorImage = null;
            this.miniBox.Image = ((System.Drawing.Image)(resources.GetObject("miniBox.Image")));
            this.miniBox.InitialImage = null;
            this.miniBox.Location = new System.Drawing.Point(394, 3);
            this.miniBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.miniBox.Name = "miniBox";
            this.miniBox.Size = new System.Drawing.Size(33, 23);
            this.miniBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.miniBox.TabIndex = 2;
            this.miniBox.TabStop = false;
            this.toolTip1.SetToolTip(this.miniBox, "Minimize");
            this.miniBox.Click += new System.EventHandler(this.miniBox_Click);
            this.miniBox.MouseLeave += new System.EventHandler(this.MouseLeave);
            this.miniBox.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // passwordPanel
            // 
            this.passwordPanel.Controls.Add(this.passwordtextBox);
            this.passwordPanel.Controls.Add(this.passwordLabel);
            this.passwordPanel.Controls.Add(this.passView);
            this.passwordPanel.Controls.Add(this.pwderrBox);
            this.passwordPanel.Location = new System.Drawing.Point(3, 40);
            this.passwordPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.passwordPanel.Name = "passwordPanel";
            this.passwordPanel.Size = new System.Drawing.Size(270, 29);
            this.passwordPanel.TabIndex = 1;
            // 
            // usernamePanel
            // 
            this.usernamePanel.Controls.Add(this.usertextBox);
            this.usernamePanel.Controls.Add(this.usernameLabel);
            this.usernamePanel.Controls.Add(this.usrerrBox);
            this.usernamePanel.Location = new System.Drawing.Point(3, 11);
            this.usernamePanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.usernamePanel.Name = "usernamePanel";
            this.usernamePanel.Size = new System.Drawing.Size(270, 29);
            this.usernamePanel.TabIndex = 0;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.usernamePanel);
            this.controlPanel.Controls.Add(this.passwordPanel);
            this.controlPanel.Controls.Add(this.confirmPanel);
            this.controlPanel.Controls.Add(this.buttonPanel);
            this.controlPanel.Location = new System.Drawing.Point(68, 227);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(284, 139);
            this.controlPanel.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 10);
            this.panel1.TabIndex = 31;
            // 
            // loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(460, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.logoBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "loginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfrmpwderrBox)).EndInit();
            this.confirmPanel.ResumeLayout(false);
            this.confirmPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.codelinkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniBox)).EndInit();
            this.passwordPanel.ResumeLayout(false);
            this.passwordPanel.PerformLayout();
            this.usernamePanel.ResumeLayout(false);
            this.usernamePanel.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox usertextBox;
        private System.Windows.Forms.TextBox passwordtextBox;
        private System.Windows.Forms.TextBox confirmtextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label confirmLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.PictureBox usrerrBox;
        private System.Windows.Forms.PictureBox cfrmpwderrBox;
        private System.Windows.Forms.Panel confirmPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.PictureBox pwderrBox;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button passView;
        private System.Windows.Forms.Button cnfmView;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel passwordPanel;
        private System.Windows.Forms.Panel usernamePanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.PictureBox exitBox;
        private System.Windows.Forms.PictureBox miniBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox codelinkBox;
        private System.Windows.Forms.ToolTip toolTip3;
    }
}