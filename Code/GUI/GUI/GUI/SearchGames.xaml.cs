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

        private async void Find_game_Click(object sender, RoutedEventArgs e)
        {
            //send to manager to get results and then
            //show them using showResults
            string criterion = "";
            object param = null;
            if (!player_name.Text.Equals(""))
            {
                criterion = "playerName";
                param = player_name.Text;
            }
            else if(!pot_size.Text.Equals(""))
            {
                criterion = "potsize";
                param = pot_size.Text;
            }
            else if (gamePrefFieldsEdited())
            {
                int sB = -1;
                int bI = -1;
                int tP = -1;
                int cP = -1;
                Int32.TryParse(small_blind.Text, out sB);
                Int32.TryParse(buy_in.Text, out bI);
                Int32.TryParse(Type_Policy.Text, out tP);
                Int32.TryParse(chip_policy.Text, out cP);
                bool aS = Allow_Spec.IsChecked.Value;
                param = new Models.GamePreferences(-1, -1, sB, -1, tP, bI, cP, aS);
                criterion = "gamepreference";
            }

            List<Models.ClientGame> gameList = await manager.SearchGames(criterion, param);
            showResults(gameList);
        }

        private bool gamePrefFieldsEdited()
        {
            return !small_blind.Text.Equals("") ||
                    !buy_in.Text.Equals("") ||
                    !Type_Policy.Text.Equals("") ||
                    !chip_policy.Text.Equals("");
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
                                                      MinPlayers = prefs.minPlayers,
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
            GameDataGrid gdg = (GameDataGrid)Display_game_results.SelectedItem;
            manager.JoinGameAsSpectator(gdg.ID);
        }

        private async void Spec_Click(object sender, RoutedEventArgs e)
        {
            List<Models.ClientGame> gameList = await manager.SearchGamesToSpectate();
            showResults(gameList);
        }
    }
}
