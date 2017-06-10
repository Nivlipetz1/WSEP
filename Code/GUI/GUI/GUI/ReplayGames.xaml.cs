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
    /// Interaction logic for ReplayGames.xaml
    /// </summary>
    public partial class ReplayGames : Page
    {
        private GUIManager manager;
        public ReplayGames(GUIManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReplayGamePlayback());
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            manager.GetReplay(int.Parse(GameIDTxt.Text));
        }
    }
}
