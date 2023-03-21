using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

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

        // Hash a password using Bcrypt
        public static string HashPassword(string password)
        {
            string hashedPassword = BC.HashPassword(password);
            return (hashedPassword);
        }
        private void loginForm_Load(object sender, EventArgs e)
        {
            // Set to no text.
            passwordtextBox.Text = "";
            // The password character is an asterisk.
            passwordtextBox.PasswordChar = '*';
            // The control will allow no more than 50 characters.
            passwordtextBox.MaxLength = 50;
            // set path to provided database
            string workingDirectory = Environment.CurrentDirectory;
            string dbPath = Directory.GetParent(workingDirectory).Parent.FullName + "\\Data\\users.db";
            connectionString = $@"Data Source={dbPath};";
            buttonPanel.Location = new System.Drawing.Point(72, 119);
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!createButtonClicked)
            {
                buttonPanel.Location = new System.Drawing.Point(32, 148);
                loginButton.Visible = false;
                confirmPanel.Location = new System.Drawing.Point(48, 116);
                confirmPanel.Visible = true;
                createButton.Text = "Create";
                createButtonClicked = true;
            }
            else
            {
                if (passwordtextBox.Text.Length > 50 || confirmtextBox.Text.Length > 50)
                {
                    MessageBox.Show("Password must be less than 50 characters.");
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
                                MessageBox.Show("Username already exists. Please choose a different username.");
                            }
                        }
                    }
                    else
                    {
                        cfrmpwderrBox.Visible = true;
                        pwderrBox.Visible = true;
                        MessageBox.Show("Passwords must match. Please try again.");
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
                string sql = "SELECT password FROM user WHERE username=@username";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader.GetString(0);
                        }
                    }
                }
            }
            if (BC.Verify(passwordtextBox.Text, storedHash))
            {
                Program.OpenForm1OnClose = true;
                this.Close();
            }
            else
            {
                // Passwords don't match, so deny login
                MessageBox.Show("Invalid username or password.");
                usrerrBox.Visible = true;
                cfrmpwderrBox.Visible = true;
            }
        }
    }
}
