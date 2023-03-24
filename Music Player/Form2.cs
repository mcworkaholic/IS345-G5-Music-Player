using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;
using TextBox = System.Windows.Forms.TextBox;

namespace Music_Player
{
    public partial class loginForm : Form
    {
        Form1 form1 = new Form1();
        public loginForm()
        {
            InitializeComponent();
        }
        string connectionString;
        bool createButtonClicked = false;
        List<TextBox> TextboxList = new List<TextBox>();
        List<PictureBox> errorBoxList = new List<PictureBox>();
        int passViewClickCount = 0;
        bool passViewClicked = false;
        int confirmViewClickCount = 0;
        bool confirmViewClicked = false;

        private void ResetState()
        {
            this.Controls.Clear();
            InitializeComponent();
            createButtonClicked = false;

            // add to list of textboxes for iteration later
            TextboxList.Add(usertextBox);
            TextboxList.Add(passwordtextBox);
            TextboxList.Add(confirmtextBox);

            // Set to no text.
            passwordtextBox.Text = "";
            confirmtextBox.Text = "";

            // The password character is an asterisk.
            passwordtextBox.PasswordChar = '•';
            confirmtextBox.PasswordChar = '•';

            // The control will allow no more than 50 characters.
            passwordtextBox.MaxLength = 50;

            // set path to provided database
            string workingDirectory = Environment.CurrentDirectory;
            string dbPath = Directory.GetParent(workingDirectory).Parent.FullName + "\\Data\\users.db";
            connectionString = $@"Data Source={dbPath};";
            buttonPanel.Location = new System.Drawing.Point(92, 303);

            // re-add event handlers
            createButton.Click += createButton_Click;
            loginButton.Click += loginButton_Click;
            backButton.Click += backButton_Click;
        }

        // Hash a password using Bcrypt
        public static string HashPassword(string password)
        {
            string hashedPassword = BC.HashPassword(password);
            return (hashedPassword);
        }
        private void loginForm_Load(object sender, EventArgs e)
        {
            // add to list of textboxes for iteration later
            TextboxList.Add(usertextBox);
            TextboxList.Add(passwordtextBox);
            TextboxList.Add(confirmtextBox);

            //add to list of error boxes for iteration later
            errorBoxList.Add(usrerrBox);
            errorBoxList.Add(pwderrBox);
            errorBoxList.Add(cfrmpwderrBox);

            // Set to no text.
            passwordtextBox.Text = "";
            confirmtextBox.Text = "";
            // The password character is an asterisk.
            passwordtextBox.PasswordChar = '•';
            confirmtextBox.PasswordChar = '•';
            // The control will allow no more than 50 characters.
            passwordtextBox.MaxLength = 50;
            // set path to provided database
            string workingDirectory = Environment.CurrentDirectory;
            string dbPath = Directory.GetParent(workingDirectory).Parent.FullName + "\\Data\\users.db";
            connectionString = $@"Data Source={dbPath};";
            buttonPanel.Location = new System.Drawing.Point(92, 303);
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!createButtonClicked)
            {
                buttonPanel.Location = new System.Drawing.Point(49, 331);
                loginButton.Visible = false;
                confirmPanel.Location = new System.Drawing.Point(65, 300);
                confirmPanel.Visible = true;
                createButton.Text = "Create";
                createButtonClicked = true;
                backButton.Visible = true;
            }
            else
            {
                createButtonClicked = true;
                foreach (TextBox textbox in TextboxList)
                {
                    if (textbox.Text == string.Empty)
                    {
                        backButton.Visible = true;
                        if (textbox == usertextBox)
                        {
                            usrerrBox.Visible = true;
                        }
                        else if (textbox == passwordtextBox)
                        {
                            pwderrBox.Visible = true;
                        }
                        else
                        {
                            cfrmpwderrBox.Visible = true;
                        }
                    }
                }
                if (passwordtextBox.Text.Length > 50 || confirmtextBox.Text.Length > 50)
                {
                    MessageBox.Show("Password must be less than 50 characters.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cfrmpwderrBox.Visible = true;
                    pwderrBox.Visible = true;
                }
                else if (usertextBox.Text.Length > 0 && passwordtextBox.Text.Length > 0)
                {
                    if (passwordtextBox.Text == confirmtextBox.Text)
                    {
                        try
                        {
                            // Second click: perform insert and close form
                            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                            {
                                connection.Open();
                                string sql = "INSERT INTO user (username, password) VALUES (@username, @password)";
                                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                                {
                                    string hash = HashPassword(passwordtextBox.Text);
                                    command.Parameters.AddWithValue("@username", usertextBox.Text);
                                    command.Parameters.AddWithValue("@password", hash);
                                    command.ExecuteNonQuery();
                                }
                            }
                            Program.OpenForm1OnClose = true;
                            this.Close();
                        }
                        catch (SQLiteException ex)
                        {
                            if (ex.Message.Contains("UNIQUE constraint failed: user.username"))
                            {
                                usrerrBox.Visible = true;
                                backButton.Visible = true;
                                usertextBox.Text = String.Empty;
                                passwordtextBox.Text = String.Empty;
                                confirmtextBox.Text = String.Empty;
                                passView.Visible = false;
                                cnfmView.Visible = false;
                                MessageBox.Show("Username already exists. Please choose a different username.", "Username In Use", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        cfrmpwderrBox.Visible = true;
                        pwderrBox.Visible = true;
                        if (confirmtextBox.Visible == true)
                        {
                            if (confirmtextBox.Text.Length > 0)
                            {
                                MessageBox.Show("Passwords must match. Please try again.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usertextBox.Text;

            // Get the stored hash 
            string storedHash = "";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT password, user_id FROM user WHERE username=@username";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader.GetString(0);
                            Program.user_id = reader.GetInt32(1);
                        }
                    }
                }
            }
            if (passwordtextBox.Text.Length > 0 && usertextBox.Text.Length > 0)
            {
                try
                {
                    if (BC.Verify(passwordtextBox.Text, storedHash))

                    {
                        Program.OpenForm1OnClose = true;
                        this.Close();
                    }
                    else
                    {
                        // Passwords don't match, so deny login
                        MessageBox.Show("Invalid username or password.", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usrerrBox.Visible = true;
                        pwderrBox.Visible = true;
                    }
                }
                catch(System.ArgumentException)
                {
                    // username not found, so deny login
                    MessageBox.Show("Invalid username, please create an account.", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usrerrBox.Visible = true;
                    pwderrBox.Visible = true;
                }
                
            }
            else
            {
                foreach (TextBox textbox in TextboxList)
                {
                    if (textbox.Text == string.Empty)
                    {
                        if (textbox == usertextBox)
                        {
                            usrerrBox.Visible = true;
                        }
                        else if (textbox == passwordtextBox)
                        {
                            pwderrBox.Visible = true;
                        }
                    }
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ResetState();
        }

        private void passwordtextBox_TextChanged(object sender, EventArgs e)
        {
            if (passwordtextBox.Text.Length > 0)
            {
                if (pwderrBox.Visible == false)
                {
                    passView.Location = new System.Drawing.Point(281, 276);
                }
                else
                {
                    passView.Location = new System.Drawing.Point(310, 276);
                }
                passView.Visible = true;
            }
            else
            {
                passView.Visible = false;
            }
        }

        private void confirmtextBox_TextChanged(object sender, EventArgs e)
        {
            if (confirmtextBox.Text.Length > 0)
            {
                if (cfrmpwderrBox.Visible == false)
                {
                    cnfmView.Location = new System.Drawing.Point(216, 3);
                }
                else
                {
                    cnfmView.Location = new System.Drawing.Point(245, 3);
                }
                cnfmView.Visible = true;
            }
            else
            {
                cnfmView.Visible = false;
            }
        }

        private void passView_Click(object sender, EventArgs e)
        {
            passViewClickCount++;
            passViewClicked = (passViewClickCount % 2 == 1);
            if (passViewClickCount % 2 == 1)
            {
                if (passwordtextBox.PasswordChar == '•')
                {
                    passwordtextBox.PasswordChar = '\0';
                }
                else
                {
                    passwordtextBox.PasswordChar = '•';
                }
            }
            else
            {
                passwordtextBox.PasswordChar = '•';
            }
            passView.Text = "👁";
        }

        private void cnfmView_Click(object sender, EventArgs e)
        {
            confirmViewClickCount++;
            confirmViewClicked = (confirmViewClickCount % 2 == 1);
            if (confirmViewClickCount % 2 == 1)
            {
                if (confirmtextBox.PasswordChar == '•')
                {
                    confirmtextBox.PasswordChar = '\0';
                }
                else
                {
                    confirmtextBox.PasswordChar = '•';
                }
            }
            else
            {
                confirmtextBox.PasswordChar = '•';
            }
            cnfmView.Text = "👁";
        }
    }
}
