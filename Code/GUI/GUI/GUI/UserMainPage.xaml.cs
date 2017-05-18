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
    /// Interaction logic for UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Page
    {
        MainWindow main;
        public Status statusFrame { get; set; }
        public List<GameFrame> gameList { get; set; }
        public UserMainPage(MainWindow main, Status statusFrame)
        {
            InitializeComponent();
            gameList = new List<GameFrame>();
            this.main = main;
            this.statusFrame = statusFrame;
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfile(main,this));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                main.statusFrame.Content = null;
                NavigationService.Navigate(new Login(main));
            }
        }

        private void GameCenter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameCenter(main,this));
        }

        public void AddGame(GameFrame gf)
        {
            gameList.Add(gf);
            statusFrame.RefreshGameList();
        }

        public void RemoveGame(GameFrame gf)
        {
            if (gameList.Contains(gf))
            {
                gameList.Remove(gf);
                statusFrame.RefreshGameList();
            }
        }
    }
}
