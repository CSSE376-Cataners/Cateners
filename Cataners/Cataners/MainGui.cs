﻿using System;
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
    public partial class MainGui : Form
    {
        public MainGui()
        {
            InitializeComponent();
        }

        private void mainQuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainJoinGameButton_Click(object sender, EventArgs e)
        {
        }

        private void MainGui_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            bool userNameGood=false;
            bool passwordGood = false;

            String username = usernameTextbox.Text;
            userNameGood = Verification.verifyInputString(username);

            String password = passwordTextbox.Text;
            passwordGood = Verification.verifyPassword(password);

        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            SignUpForm signup = new SignUpForm();
            signup.ShowDialog();
        }

    }
}
