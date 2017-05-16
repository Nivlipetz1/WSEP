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
    /// Interaction logic for CreateGame.xaml
    /// </summary>
    public partial class CreateGame : Page
    {
        public CreateGame()
        {
            InitializeComponent();
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            Models.ClientGame game = new Models.ClientGame();
            List<Models.ClientUserProfile> players = new List<Models.ClientUserProfile>();
            Models.ClientUserProfile niv = new Models.ClientUserProfile();
            niv.Username = "niv";
            Models.ClientUserProfile omer = new Models.ClientUserProfile();
            omer.Username = "omer";
            Models.ClientUserProfile naor = new Models.ClientUserProfile();
            naor.Username = "naor";
            Models.ClientUserProfile rick = new Models.ClientUserProfile();
            rick.Username = "Rick Sanchez";
            Models.ClientUserProfile rick2 = new Models.ClientUserProfile();
            rick2.Username = "Motry";
            Models.ClientUserProfile rick3 = new Models.ClientUserProfile();
            rick3.Username = "AAA";


            players.Add(niv);
            players.Add(omer);
            players.Add(naor);
            players.Add(rick);
            players.Add(rick2);
            players.Add(rick3);
            game.Players = players;
            NavigationService.Navigate(new GameFrame(game));
        }
    }
}
