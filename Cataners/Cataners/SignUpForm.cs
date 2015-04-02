using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatanersShared;

namespace Cataners
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {


            String username = usernameTextbox.Text;
            String password = passwordTextBox.Text;
            String confirm = confirmPasswordTextBox.Text;

            if (Verification.verifyInputString(username) && Verification.verifyPassword(password))
            {
                Login newUser = new Login(username, password, true);

            }

        }

        public void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            String username = usernameTextbox.Text;
            String password = passwordTextBox.Text;
            String confirm = confirmPasswordTextBox.Text;

            if (password.Equals(confirm) && confirm.Length > 3 && username.Length > 5 && username.Length < 16)
            {
                registerButton.Enabled = true;
                notMatchingLabel.Visible = false;
            }
            else
            {
                registerButton.Enabled = false;
                if (confirm.Length > 3) {
                    notMatchingLabel.Visible = true;
                }
                
            }

        }

        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
            String username = usernameTextbox.Text;
            String password = passwordTextBox.Text;
            String confirm = confirmPasswordTextBox.Text;

            if (password.Equals(confirm) && confirm.Length > 3 && username.Length > 5 && username.Length < 16)
            {
                registerButton.Enabled = true;
            }
            else
            {
                registerButton.Enabled = false;

            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            String username = usernameTextbox.Text;
            String password = passwordTextBox.Text;
            String confirm = confirmPasswordTextBox.Text;

            if (password.Equals(confirm) && confirm.Length > 3 && username.Length > 5 && username.Length < 16)
            {
                registerButton.Enabled = true;
                notMatchingLabel.Visible = false;
            }
            else
            {
                registerButton.Enabled = false;
                if (confirm.Length > 3)
                {
                    notMatchingLabel.Visible = true;
                }

            }
        }
    }
}
