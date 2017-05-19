using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for GamePM.xaml
    /// </summary>
    public partial class GamePM : Page
    {
        GUIManager manager;
        int gameID;
        private IDictionary<string, string> messageList;
        public GamePM(GUIManager manager, int gameID)
        {
            this.manager = manager;
            this.gameID = gameID;
            InitializeComponent();
            messageList = new Dictionary<string,string>();
            foreach(Models.ClientUserProfile prof in manager.GetPlayers(gameID))
            {
                users.Items.Add(prof.username);
                messageList.Add(prof.username, "");
            }
           /* foreach (Models.ClientUserProfile prof in game.Spectators)
            {
                users.Items.Add(prof.Username);
                messageList.Add(prof.Username, "");
            }*/
        }

        public void AddPlayer(Models.ClientUserProfile prof)
        {
            users.Items.Add(prof.username);
            messageList.Add(prof.username, "");
        }

        public void RemovePlayer(Models.ClientUserProfile prof)
        {
            users.Items.Remove(prof.username);
            messageList.Remove(prof.username);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!message.Text.Equals(""))
            {
                if(manager.SendPMMessage(users.SelectedValue.ToString(),message.Text,gameID)){

                messages.AppendText(message.Text + "\n");
                messageList[users.Text] += message.Text + "\n";
                message.Text = "";
                messages.Focus();
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
                message.Focus();
                }
            }
        }

        private void users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                messages.Text = messageList[users.SelectedValue.ToString()];
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
        }

        public void PushMessage(string sender, string message)
        {
            messageList[sender] += sender+ ": "+message + "\n";
            MessageBox.Show("New Personal Message From: "+sender, "Got New Message!", MessageBoxButton.OK, MessageBoxImage.Information);
            if (users.SelectedValue.ToString().Equals(sender))
            {
                messages.Text = messageList[users.SelectedValue.ToString()];
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
            }
        }
    }
}
