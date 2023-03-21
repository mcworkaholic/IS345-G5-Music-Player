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
            this.pwderrBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfrmpwderrBox)).BeginInit();
            this.confirmPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).BeginInit();
            this.SuspendLayout();
            // 
            // usertextBox
            // 
            this.usertextBox.Location = new System.Drawing.Point(162, 63);
            this.usertextBox.Name = "usertextBox";
            this.usertextBox.Size = new System.Drawing.Size(100, 22);
            this.usertextBox.TabIndex = 0;
            // 
            // passwordtextBox
            // 
            this.passwordtextBox.Location = new System.Drawing.Point(162, 91);
            this.passwordtextBox.Name = "passwordtextBox";
            this.passwordtextBox.Size = new System.Drawing.Size(100, 22);
            this.passwordtextBox.TabIndex = 1;
            // 
            // confirmtextBox
            // 
            this.confirmtextBox.Location = new System.Drawing.Point(114, 3);
            this.confirmtextBox.Name = "confirmtextBox";
            this.confirmtextBox.Size = new System.Drawing.Size(100, 22);
            this.confirmtextBox.TabIndex = 2;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(98, 66);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(100, 94);
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
            this.usrerrBox.Location = new System.Drawing.Point(268, 65);
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
            this.confirmPanel.Controls.Add(this.confirmLabel);
            this.confirmPanel.Controls.Add(this.cfrmpwderrBox);
            this.confirmPanel.Controls.Add(this.confirmtextBox);
            this.confirmPanel.Location = new System.Drawing.Point(48, 116);
            this.confirmPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.confirmPanel.Name = "confirmPanel";
            this.confirmPanel.Size = new System.Drawing.Size(250, 29);
            this.confirmPanel.TabIndex = 11;
            this.confirmPanel.Visible = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.loginButton);
            this.buttonPanel.Controls.Add(this.createButton);
            this.buttonPanel.Location = new System.Drawing.Point(32, 148);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(243, 27);
            this.buttonPanel.TabIndex = 12;
            // 
            // pwderrBox
            // 
            this.pwderrBox.ErrorImage = null;
            this.pwderrBox.Image = ((System.Drawing.Image)(resources.GetObject("pwderrBox.Image")));
            this.pwderrBox.Location = new System.Drawing.Point(268, 93);
            this.pwderrBox.Name = "pwderrBox";
            this.pwderrBox.Size = new System.Drawing.Size(19, 20);
            this.pwderrBox.TabIndex = 13;
            this.pwderrBox.TabStop = false;
            this.pwderrBox.Visible = false;
            // 
            // loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 208);
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
    }
}