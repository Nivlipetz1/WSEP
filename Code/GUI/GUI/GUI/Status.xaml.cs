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
        GUIManager manager;
        public Status(GUIManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        public void RefreshGameList()
        {
            GameList.ItemsSource = manager.GetGameFrameList();
        }

        public void RefreshStatus()
        {
            Models.ClientUserProfile prof = manager.GetProfile();
            this.credit.Content = "Credit: $" + prof.credit;
            this.username.Content = "Hello " + prof.username;
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            manager.NavigateToGameFrame(GameList.SelectedIndex);
        }
    }
}
