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
        GUIManager manager;
        public CreateGame(GUIManager manager)
        {
            this.manager = manager;
            InitializeComponent();
            for(int i = 2; i <= 8; i++)
            {
                minPlayers.Items.Add(i);
            }
            minPlayers.SelectedItem = 2;
        }

        private void SetMaxPlayerCombo()
        {
            int selectedItem = 8;
            if(maxPlayers.SelectedItem != null)
                selectedItem = (int)maxPlayers.SelectedItem;
            maxPlayers.Items.Clear();
            for (int i = (int)minPlayers.SelectedItem; i <= 8; i++)
            {
                maxPlayers.Items.Add(i);
            }
            if(selectedItem < (int)minPlayers.SelectedItem)
            {
                maxPlayers.SelectedItem = minPlayers.SelectedItem;
            }
            else
            {
                maxPlayers.SelectedItem = selectedItem;
            }
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {

            int maxP = (int)minPlayers.SelectedItem;
            int minP = (int)maxPlayers.SelectedItem;
            int sB = Int32.Parse(Small_Blind.Text);
            int bB = Int32.Parse(Big_Blind.Text);
            int cP = Int32.Parse(Chip_Policy.Text);
            int bIP = Int32.Parse(Buy_In_Policy.Text);
            int tP = 0;
            bool aS = Allow_Spec.IsChecked.Value;
            Models.GamePreferences pref = new Models.GamePreferences(maxP, minP, sB, bB, tP, bIP, cP, aS);

            manager.CreateGame(pref);
/*
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
            */
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            manager.GoToGameCenter();
        }

        private void minPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetMaxPlayerCombo();
        }

        private void minPlayers_DropDownOpened(object sender, EventArgs e)
        {
            SetMaxPlayerCombo();
        }
    }
}
