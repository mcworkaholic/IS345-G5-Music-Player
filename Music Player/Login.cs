using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;
using TextBox = System.Windows.Forms.TextBox;
using System.Drawing.Imaging;

namespace Music_Player
{
    public partial class loginForm : Form
    {
        //rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        MusicPlayer form1 = new MusicPlayer();
        public loginForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        private DBUtils dbUtils = new DBUtils();

        // for moving borderless form
        int movX, movY;
        bool isMoving;

        List<TextBox> TextboxList = new List<TextBox>();
        List<PictureBox> errorBoxList = new List<PictureBox>();

        // Click counters that functions vary on odd or even clicks
        int passViewClickCount = 0;
        int confirmViewClickCount = 0;

        // Initial state flags
        bool passViewClicked = false;
        bool confirmViewClicked = false;
        bool createButtonClicked = false;

        private void ResetState()
        {
            this.Controls.Clear();
            InitializeComponent();
            createButtonClicked = false;

            // Set to no text.
            passwordtextBox.Text = "";
            confirmtextBox.Text = "";

            // The password character is an asterisk.
            passwordtextBox.PasswordChar = '•';
            confirmtextBox.PasswordChar = '•';

            // The control will allow no more than 50 characters.
            passwordtextBox.MaxLength = 50;

            buttonPanel.Location = new System.Drawing.Point(34, 75);

            SetFocus(usertextBox);

            // re-add event handlers
            exitBox.Click += exitBox_Click;
            miniBox.Click += miniBox_Click;
            createButton.Click += createButton_Click;
            loginButton.Click += loginButton_Click;
            backButton.Click += backButton_Click;
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            dbUtils.GetConnection();

            SetFocus(usertextBox);

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

            // Initial panel location
            buttonPanel.Location = new System.Drawing.Point(34, 75);
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            SetFocus(usertextBox);

            if (!createButtonClicked)
            {
                buttonPanel.Location = new System.Drawing.Point(35, 99);
                loginButton.Visible = false;
                confirmPanel.Location = new System.Drawing.Point(3, 69);
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
                            dbUtils.InsertUser(usertextBox.Text, passwordtextBox.Text);
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
            string storedHash = dbUtils.GetHash(usertextBox.Text);
            if (passwordtextBox.Text.Length > 0 && usertextBox.Text.Length > 0)
            {
                try
                {
                    if (BC.Verify(passwordtextBox.Text, storedHash))

                    {
                        Program.OpenForm1OnClose = true;
                        this.Close();
                        this.Dispose();
                    }
                    else
                    {
                        // Passwords don't match, so deny login
                        MessageBox.Show("Invalid username or password.", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usrerrBox.Visible = true;
                        pwderrBox.Visible = true;
                    }
                }
                catch (System.ArgumentException)
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
                    passView.Location = new System.Drawing.Point(218, 4);
                }
                else
                {
                    passView.Location = new System.Drawing.Point(246, 5);
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
                    cnfmView.Location = new System.Drawing.Point(218, 4);
                }
                else
                {
                    cnfmView.Location = new System.Drawing.Point(246, 4);
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
        }


        // Next 8 events handle the moving of the form, opening, closing, & general behavior
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            movX = e.X;
            movY = e.Y;
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving) // based on bool flag
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void miniBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void exitBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private new void MouseHover(object sender, EventArgs e)
        {
            // hover on = 👆
            this.Cursor = Cursors.Hand;
        }
        private new void MouseLeave(object sender, EventArgs e)
        {
            // Change cursor to default when hovering away
            this.Cursor = Cursors.Default;
        }
        private void loginForm_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }


        private void codelinkBox_Click(object sender, EventArgs e)
        {
            form1.OpenLink("codelinkBox");
        }

        // method to circumvent the wonky tab-order
        private void SetFocus(Control control)
        {
            ActiveControl = control;
            control.Select();
            control.Focus();
        }
    }
}
