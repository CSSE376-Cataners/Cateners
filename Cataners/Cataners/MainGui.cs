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
using WaveEngineGame;
using System.Diagnostics.CodeAnalysis;

namespace Cataners
{
    public partial class MainGui : Form
    {
        public static MainGui INSTANCE;
        public MainGui()
        {
            InitializeComponent();
            INSTANCE = this;
            //this.FormClosing += closing;

            CommunicationClient client = new  CommunicationClient();
            client.Start();
            Console.WriteLine("connected to the server");
        }

        [ExcludeFromCodeCoverage]
        private void closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        [ExcludeFromCodeCoverage]
        private void mainQuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [ExcludeFromCodeCoverage]
        private void loginButton_Click(object sender, EventArgs e)
        {

            bool userNameGood=false;
            bool passwordGood = false;

            String username = usernameTextbox.Text;
            userNameGood = Verification.verifyUsername(username);

            String password = passwordTextbox.Text;
            passwordGood = Verification.verifyPassword(password);

            if (userNameGood && passwordGood)
            {

                CatanersShared.Message msg = new CatanersShared.Message(new Login(username, password).toJson(),CatanersShared.Translation.TYPE.Login);
                CommunicationClient.Instance.sendToServer(msg.toJson());

                Console.WriteLine("sending " + username + " and " + password + " to the server");
                LoggingInForm logging = new LoggingInForm();
                logging.ShowDialog();
                Object newString = null;
                CommunicationClient.Instance.queues[Translation.TYPE.Login].TryTake(out newString,3000);
                
                if (newString != null && !newString.Equals("-1"))
                {
                    MessageBox.Show("You have successfully logged in!");
                    joinGameButton.Visible = true;
                    createGameButton.Visible = true;
                    signUpButton.Visible = false;
                    loginButton.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter an registered username/password combination");
                }
            }
            else
            {
                if (!userNameGood && passwordGood)
                {
                    MessageBox.Show("Your username does not meet the requirements");
                }
                else if (userNameGood && !passwordGood)
                {
                    MessageBox.Show("Your password does not meet the requirements");
                }
                else
                {
                    MessageBox.Show("Neither your username nor your password meet the requirements");
                }
                
                
            }

        }

        [ExcludeFromCodeCoverage]
        private void signUpButton_Click(object sender, EventArgs e)
        {
            SignUpForm signup = new SignUpForm();
            signup.ShowDialog();
        }

        [ExcludeFromCodeCoverage]
        private void createGameButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateGameForm createGame = new CreateGameForm(this);
            createGame.ShowDialog();

        }

        [ExcludeFromCodeCoverage]
        private void joinGameButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            JoinGameForm joinform = new JoinGameForm();
            joinform.ShowDialog();
        }

        [ExcludeFromCodeCoverage]
        private void startWaveTestButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.Main();
        }

    }
}
