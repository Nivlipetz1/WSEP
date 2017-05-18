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
    /// Interaction logic for Status.xaml
    /// </summary>
    public partial class Status : Page
    {
        MainWindow main;
        public UserMainPage umPage { get; set; }
        public Status(MainWindow main, UserMainPage umPage)
        {
            this.main = main;
            Models.ClientUserProfile prof = main.getProfile();
            InitializeComponent();
            this.credit.Content = "Credit: $"+prof.Credit;
            this.username.Content = "Hello " + prof.Username;
        }

        public void RefreshGameList()
        {
            GameList.Items.Clear();
            foreach (GameFrame gf in umPage.gameList)
                GameList.Items.Add(gf);
        }

        public void RefreshStatus()
        {
            Models.ClientUserProfile prof = main.getProfile();
            this.credit.Content = "Credit: $" + prof.Credit;
            this.username.Content = "Hello " + prof.Username;
        }

        private void GameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.mainFrame.NavigationService.Navigate(GameList.SelectedItem);
        }
    }
}
