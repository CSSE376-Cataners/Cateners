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

namespace Cataners
{
    public partial class MainGui : Form
    {
        public MainGui()
        {
            InitializeComponent();
            CommunicationClient client = new  CommunicationClient();
            client.Start();
            Console.WriteLine("connected to the server");
        }

        private void mainQuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            bool userNameGood=false;
            bool passwordGood = false;

            String username = usernameTextbox.Text;
            userNameGood = Verification.verifyInputString(username);

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
                MessageBox.Show("Your username and password do not meet the requirements");
            }

        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            SignUpForm signup = new SignUpForm();
            signup.ShowDialog();
        }

        private void createGameButton_Click(object sender, EventArgs e)
        {
            CreateGameForm createGame = new CreateGameForm(this);
            createGame.ShowDialog();

        }

        private void joinGameButton_Click(object sender, EventArgs e)
        {
            JoinGameForm joinform = new JoinGameForm();
            joinform.ShowDialog();
        }

        private void startWaveTestButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.Main();
        }

    }
}
