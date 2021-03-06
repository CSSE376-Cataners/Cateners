﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatanersShared;
using System.Diagnostics.CodeAnalysis;

namespace Cataners
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        [ExcludeFromCodeCoverage]
        private void registerButton_Click(object sender, EventArgs e)
        {


            String username = usernameTextbox.Text;
            String password = passwordTextBox.Text;
            String confirm = confirmPasswordTextBox.Text;

            if (Verification.verifyUsername(username) && Verification.verifyPassword(password))
            {
                CatanersShared.Message msg = new CatanersShared.Message(new Login(username, password, true).toJson(), CatanersShared.Translation.TYPE.Register);
                CommunicationClient.Instance.sendToServer(msg.toJson());
                Console.WriteLine("sending " + username + " and " + password + " to the server");
                Object newString = null;
                CommunicationClient.Instance.queues[Translation.TYPE.Register].TryTake(out newString,3000);
                //MessageBox.Show(newString);
                if(newString != null && !newString.Equals("-1")){
                    MessageBox.Show("You have successfully signed up!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This username is already taken. Please try again.");
                }
                
            }

        }

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
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
