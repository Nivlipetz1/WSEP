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
                if (!prof.username.Equals(manager.GetProfile().username))
                {
                    messageList.Add(prof.username, "");
                }
            }
            foreach (Models.ClientUserProfile prof in manager.GetSpectators(gameID))
            {
                if (!prof.username.Equals(manager.GetProfile().username))
                {
                    messageList.Add(prof.username, "");
                }
            }
            users.ItemsSource = messageList.Keys;
        }

        public void AddPlayer(Models.ClientUserProfile prof)
        {
            messageList.Add(prof.username, "");
            users.ItemsSource = messageList.Keys;
        }

        public void RemovePlayer(string username)
        {
            messageList.Remove(username);
            users.ItemsSource = messageList.Keys;
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!message.Text.Equals(""))
            {
                if(await manager.SendPMMessage(users.SelectedItem as string,message.Text,gameID)){

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
