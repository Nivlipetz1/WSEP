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
    /// Interaction logic for GameCenter.xaml
    /// </summary>
    public partial class GameCenter : Page
    {
        GUIManager manager;
        public GameCenter(GUIManager manager)
        {
            this.manager = manager;
            InitializeComponent();
        }

        private void CreateGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateGame(manager));
        }

        private void Search_games_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SearchGames(manager));
        }

        private void Replay_games_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReplayGames(manager));
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            manager.GoToUserMainPage();
        }
    }
}
