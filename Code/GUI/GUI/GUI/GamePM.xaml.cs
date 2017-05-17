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
        private GameFrame gameFrame;
        private IDictionary<string, string> messageList;
        public GamePM(GameFrame gf)
        {
            gameFrame = gf;
            InitializeComponent();
            Models.ClientGame game = gameFrame.getGame();
            messageList = new Dictionary<string,string>();
            foreach(Models.ClientUserProfile prof in game.Players)
            {
                users.Items.Add(prof.Username);
                messageList.Add(prof.Username,"");
            }
           /* foreach (Models.ClientUserProfile prof in game.Spectators)
            {
                users.Items.Add(prof.Username);
                messageList.Add(prof.Username, "");
            }*/
        }

        public void AddPlayer(Models.ClientUserProfile prof)
        {
            users.Items.Add(prof.Username);
            messageList.Add(prof.Username, "");
        }

        public void RemovePlayer(Models.ClientUserProfile prof)
        {
            users.Items.Remove(prof.Username);
            messageList.Remove(prof.Username);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!message.Text.Equals(""))
            {
                /*if(Communication.GameFunctions.Instance.postWhisperMessage()){
                


                messages.AppendText(message.Text + "\n");
                messageList[users.Text] += message.Text + "\n";
                message.Text = "";
                messages.Focus();
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
                message.Focus();
                }*/
                messages.AppendText("me: " + message.Text + "\n");
                messageList[users.Text] += "me: "+ message.Text + "\n";
                message.Text = "";
                messages.Focus();
                messages.CaretIndex = messages.Text.Length;
                messages.ScrollToEnd();
                message.Focus();
                PushMessage("omer", "yo sup");
                PushMessage("omer", "asdasdsaaaaaaa");
                PushMessage("niv", "dssdas");
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
