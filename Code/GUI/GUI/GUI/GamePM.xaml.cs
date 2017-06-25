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
        public GamePM(GUIManager manager, int gameID)
        {
            this.manager = manager;
            this.gameID = gameID;
            InitializeComponent();
            RefreshSelectionList();
        }

        public GamePM()
        {
            InitializeComponent();
            message.Visibility = Visibility.Hidden;
            messages.Visibility = Visibility.Hidden;
            users.Visibility = Visibility.Hidden;
            SendMessage.Visibility = Visibility.Hidden;
            pmLbl.Visibility = Visibility.Hidden;
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!message.Text.Equals("") && (users.SelectedValue != null))
            {
                if (await manager.SendPMMessage(users.SelectedItem as string, message.Text, gameID)) {

                    message.Text = "";
                    messages.Text = manager.GetMessages(gameID, users.SelectedValue.ToString());
                    messages.Focus();
                    messages.CaretIndex = messages.Text.Length;
                    messages.ScrollToEnd();
                    message.Focus();
                }
            }
        }

        private void users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string messagesString = manager.GetMessages(gameID, users.SelectedValue.ToString());
            messages.Text = messagesString;
            messages.CaretIndex = messages.Text.Length;
            messages.ScrollToEnd();
        } 

        public void PushMessage(string sender)
        {
            RefreshSelectionList();
            MessageBox.Show("New Personal Message From: "+sender+"\nAt Game: "+gameID, "Got New Message!", MessageBoxButton.OK, MessageBoxImage.Information);
            users.Text = sender;
            messages.Text = manager.GetMessages(gameID, sender);
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
        }

        public void RefreshSelectionList()
        {
            users.ItemsSource = manager.GetUsersForPM(gameID);
        }

        private void users_DropDownOpened(object sender, EventArgs e)
        {
            RefreshSelectionList();
        }
    }
}
