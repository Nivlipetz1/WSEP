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
        GUIManager manager;
        public SearchGames(GUIManager manager)
        {
            this.manager = manager;
            InitializeComponent();
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Find_game_Click(object sender, RoutedEventArgs e)
        {
            //send to manager to get results and then
            //show them using showResults
        }

        private void showResults(List<Models.ClientGame> gameList)
        {
            List<GameDataGrid> displayList = new List<GameDataGrid>();
            foreach(Models.ClientGame game in gameList)
            {
                Models.GamePreferences prefs = game.gamePref;
                GameDataGrid gdg = new GameDataGrid { ID = game.id,
                                                      PlayersInGame = game.players.Count,
                                                      MaxPlayers = prefs.maxPlayers,
                                                      BigBlind = prefs.bigBlind,
                                                      SmallBlind = prefs.smallBlind,
                                                      SpectatingAllowed = prefs.allowSpectators
                                                                                    };
                displayList.Add(gdg);
            }
            Display_game_results.ItemsSource = displayList;
            Display_game_results.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            
            GameDataGrid gdg = (GameDataGrid)Display_game_results.SelectedItem;
            InputDialog inputDialog = new InputDialog("How much credit would you like to use?", "0");
            int credit = 0;
            if (inputDialog.ShowDialog() == true)
                 credit = Int32.Parse(inputDialog.Answer);
            //Join game function
            manager.JoinGame(gdg.ID,credit);
        }

        private void spectateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
