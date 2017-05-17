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
    /// Interaction logic for SearchGames.xaml
    /// </summary>
    public partial class SearchGames : Page
    {
        public SearchGames()
        {
            InitializeComponent();
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Find_game_Click(object sender, RoutedEventArgs e)
        {

        }

        private void showResults(List<Models.ClientGame> gameList)
        {
            List<GameDataGrid> displayList = new List<GameDataGrid>();
            foreach(Models.ClientGame game in gameList)
            {
                Models.GamePreferences prefs = game.GamePref;
                GameDataGrid gdg = new GameDataGrid { ID = prefs.GameID,
                                                      PlayersInGame = game.Players.Count,
                                                      MaxPlayers = prefs.MaxPlayers,
                                                      BigBlind = prefs.BigBlind,
                                                      SmallBlind = prefs.SmallBlind,
                                                      SpectatingAllowed = prefs.AllowSpectators
                                                                                    };
                displayList.Add(gdg);
            }
            Display_game_results.ItemsSource = displayList;
            Display_game_results.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            
            GameDataGrid gdg = (GameDataGrid)Display_game_results.SelectedItem;
            InputDialog inputDialog = new InputDialog("Please enter your name:", "John Doe");
            int credit = 0;
            if (inputDialog.ShowDialog() == true)
                 credit = Int32.Parse(inputDialog.Answer);
            //Join game function
            //Communication.GameCenterFunctions.Instance.joinGame(gdg.ID, credit);
        }

        private void spectateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
