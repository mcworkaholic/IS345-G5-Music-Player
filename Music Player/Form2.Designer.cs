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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.confirmLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.usrerrBox = new System.Windows.Forms.PictureBox();
            this.pwderrBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(186, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(186, 89);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(186, 115);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 2;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(122, 65);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(124, 92);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password:";
            // 
            // confirmLabel
            // 
            this.confirmLabel.AutoSize = true;
            this.confirmLabel.Location = new System.Drawing.Point(80, 118);
            this.confirmLabel.Name = "confirmLabel";
            this.confirmLabel.Size = new System.Drawing.Size(103, 13);
            this.confirmLabel.TabIndex = 5;
            this.confirmLabel.Text = "Confirm Password:";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(186, 141);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 23);
            this.createButton.TabIndex = 7;
            this.createButton.Text = "Create Profile";
            this.createButton.UseVisualStyleBackColor = true;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(106, 141);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 8;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            // 
            // usrerrBox
            // 
            this.usrerrBox.ErrorImage = null;
            this.usrerrBox.Image = ((System.Drawing.Image)(resources.GetObject("usrerrBox.Image")));
            this.usrerrBox.Location = new System.Drawing.Point(292, 64);
            this.usrerrBox.Name = "usrerrBox";
            this.usrerrBox.Size = new System.Drawing.Size(19, 20);
            this.usrerrBox.TabIndex = 9;
            this.usrerrBox.TabStop = false;
            // 
            // pwderrBox
            // 
            this.pwderrBox.Image = ((System.Drawing.Image)(resources.GetObject("pwderrBox.Image")));
            this.pwderrBox.Location = new System.Drawing.Point(292, 116);
            this.pwderrBox.Name = "pwderrBox";
            this.pwderrBox.Size = new System.Drawing.Size(19, 20);
            this.pwderrBox.TabIndex = 10;
            this.pwderrBox.TabStop = false;
            // 
            // loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 208);
            this.Controls.Add(this.pwderrBox);
            this.Controls.Add(this.usrerrBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.confirmLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "loginForm";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.usrerrBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pwderrBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label confirmLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.PictureBox usrerrBox;
        private System.Windows.Forms.PictureBox pwderrBox;
    }
}