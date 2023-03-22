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
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.pwderrBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.passView = new System.Windows.Forms.Button();
            this.cnfmView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfrmpwderrBox)).BeginInit();
            this.confirmPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // usertextBox
            // 
            this.usertextBox.Location = new System.Drawing.Point(179, 247);
            this.usertextBox.Name = "usertextBox";
            this.usertextBox.Size = new System.Drawing.Size(100, 22);
            this.usertextBox.TabIndex = 0;
            // 
            // passwordtextBox
            // 
            this.passwordtextBox.Location = new System.Drawing.Point(179, 275);
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
            this.usernameLabel.Location = new System.Drawing.Point(115, 250);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(117, 278);
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
            this.createButton.Size = new System.Drawing.Size(105, 23);
            this.createButton.TabIndex = 7;
            this.createButton.Text = "Create Profile";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(47, 1);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 8;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // usrerrBox
            // 
            this.usrerrBox.ErrorImage = null;
            this.usrerrBox.Image = ((System.Drawing.Image)(resources.GetObject("usrerrBox.Image")));
            this.usrerrBox.Location = new System.Drawing.Point(285, 249);
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
            this.confirmPanel.Location = new System.Drawing.Point(65, 300);
            this.confirmPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.confirmPanel.Name = "confirmPanel";
            this.confirmPanel.Size = new System.Drawing.Size(270, 29);
            this.confirmPanel.TabIndex = 11;
            this.confirmPanel.Visible = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.backButton);
            this.buttonPanel.Controls.Add(this.loginButton);
            this.buttonPanel.Controls.Add(this.createButton);
            this.buttonPanel.Location = new System.Drawing.Point(92, 332);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(243, 27);
            this.buttonPanel.TabIndex = 12;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(66, 1);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(56, 23);
            this.backButton.TabIndex = 9;
            this.backButton.Text = "👈 Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Visible = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // pwderrBox
            // 
            this.pwderrBox.ErrorImage = null;
            this.pwderrBox.Image = ((System.Drawing.Image)(resources.GetObject("pwderrBox.Image")));
            this.pwderrBox.Location = new System.Drawing.Point(285, 277);
            this.pwderrBox.Name = "pwderrBox";
            this.pwderrBox.Size = new System.Drawing.Size(19, 20);
            this.pwderrBox.TabIndex = 13;
            this.pwderrBox.TabStop = false;
            this.pwderrBox.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(92, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // passView
            // 
            this.passView.Location = new System.Drawing.Point(281, 276);
            this.passView.Name = "passView";
            this.passView.Size = new System.Drawing.Size(23, 21);
            this.passView.TabIndex = 15;
            this.passView.Text = "👁️‍🗨️";
            this.passView.UseVisualStyleBackColor = true;
            this.passView.Visible = false;
            this.passView.Click += new System.EventHandler(this.passView_Click);
            // 
            // cnfmView
            // 
            this.cnfmView.Location = new System.Drawing.Point(216, 3);
            this.cnfmView.Name = "cnfmView";
            this.cnfmView.Size = new System.Drawing.Size(23, 21);
            this.cnfmView.TabIndex = 16;
            this.cnfmView.Text = "👁️‍🗨️";
            this.cnfmView.UseVisualStyleBackColor = true;
            this.cnfmView.Visible = false;
            this.cnfmView.Click += new System.EventHandler(this.cnfmView_Click);
            // 
            // loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(460, 408);
            this.Controls.Add(this.passView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pwderrBox);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.confirmPanel);
            this.Controls.Add(this.usrerrBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordtextBox);
            this.Controls.Add(this.usertextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "loginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.loginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfrmpwderrBox)).EndInit();
            this.confirmPanel.ResumeLayout(false);
            this.confirmPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button passView;
        private System.Windows.Forms.Button cnfmView;
    }
}