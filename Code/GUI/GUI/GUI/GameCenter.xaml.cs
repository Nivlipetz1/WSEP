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
        MainWindow main;
        UserMainPage umP;
        public GameCenter(MainWindow main, UserMainPage umP)
        {
            this.umP = umP;
            this.main = main;
            InitializeComponent();
        }

        private void CreateGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateGame(main));
        }

        private void Search_games_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SearchGames(main,umP,this));
        }

        private void Replay_games_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReplayGames());
        }
    }
}
