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
        MainWindow main;
        public CreateGame(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
/*
            int maxP = Int32.Parse(Max_Players.Text);
            int minP = Int32.Parse(Min_Players.Text);
            int sB = Int32.Parse(Small_Blind.Text);
            int bB = Int32.Parse(Big_Blind.Text);
            int cP = Int32.Parse(Chip_Policy.Text);
            int bIP = Int32.Parse(Buy_In_Policy.Text);
            int tP = 0;
            bool aS = Allow_Spec.IsChecked.Value;
            Models.GamePreferences pref = new Models.GamePreferences(maxP, minP, sB, bB, tP, bIP, cP, aS);
*/
            Models.ClientGame newGame =  Communication.GameCenterFunctions.Instance.createGame(pref);
            if (newGame==null)
            {
                MessageBox.Show("Something went wrong!", "Oh Oh!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("New game created successfuly!", "New Game Created!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
                }
            }

            ///FOR DEBUG
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
            NavigationService.Navigate(new GameFrame(main,game));
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
