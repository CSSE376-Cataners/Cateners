using CatanersShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    public delegate void shower();
    public partial class ChatBox : Form
    {
        public static ChatBox INSTANCE;

        public ChatBox()
        {
            InitializeComponent();
            INSTANCE = this;
            this.FormClosing += OnClosing;
            this.textEntryBox.KeyDown += OnKeyDownHandlerForEnter;
        }

        public void invokedShow()
        {
            this.Invoke(new shower(show));
        }

        private void show()
        {
            this.Show();
        }

        private void OnClosing(object sender, FormClosingEventArgs args)
        {
            /*if (INSTANCE == this)
                INSTANCE = null;*/
            this.Hide();
            args.Cancel = true;

        }

        private void OnKeyDownHandlerForEnter(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Return)
                send_Click(null, null);
            args.Handled = true;
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (Data.username == null || Data.username.Equals(String.Empty))
            {
                MessageBox.Show("Please Login before trying to send Messages.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String text = textEntryBox.Text.Trim();
            if (text.Length > 0)
            {
                Chat chat = new Chat(text, Chat.TYPE.Normal, Data.username);
                CatanersShared.Message msg = new CatanersShared.Message(chat.toJson(), Translation.TYPE.Chat);
                CommunicationClient.Instance.sendToServer(msg.toJson());
                addChat(chat);
                textEntryBox.Text = "";
            }
        }

        public void addChat(Chat chat)
        {
            richTextBox.Text += String.Format("[{0}] {1}: {2}", DateTime.Now.ToString("T"), chat.Special, chat.Message) + "\n";
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }
    }
}
