﻿using System;
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
        UserMainPage umP;
        MainWindow main;
        GameCenter gCenter;
        public SearchGames(MainWindow main,UserMainPage umP, GameCenter gCenter)
        {
            this.gCenter = gCenter;
            this.main = main;
            this.umP = umP;
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
                GameDataGrid gdg = new GameDataGrid { ID = prefs.gameID,
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
            InputDialog inputDialog = new InputDialog("How much credit would you like to use?", "0");
            int credit = 0;
            if (inputDialog.ShowDialog() == true)
                 credit = Int32.Parse(inputDialog.Answer);
            //Join game function
            Models.ClientGame game =  Communication.GameCenterFunctions.Instance.joinGame(gdg.ID, credit);
            if (game != null)
            {
                main.RefreshProfile();
                umP.statusFrame.RefreshStatus();
                GameFrame gameFrame = new GameFrame(main, game,gCenter);
                umP.AddGame(gameFrame);
                NavigationService.Navigate(gameFrame);
            }
            else
            {
                MessageBox.Show("something went wrong:(");
            }
            
        }

        private void spectateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
