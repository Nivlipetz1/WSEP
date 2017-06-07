using System;
using System.Collections.Generic;
using System.IO;
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
        GUIManager manager;
        
        public UserMainPage(GUIManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            ShowAvatar();


        }

        public void ShowAvatar()
        {
            Models.ClientUserProfile prof = manager.GetProfile();

            BitmapImage image = manager.getAvatar();
            if (image != null)
            {
                user_avatar.Source = image;
            }
            username.Content = prof.username;
            credit.Content = "Credit: $" + prof.credit;

        }




        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfile(manager,this));
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                await Communication.AuthFunctions.Instance.logout();
                manager.ClearStatusFrame();
                manager.isLoggedIn = false;
                NavigationService.Navigate(new Login(manager));
            }
        }

        private void GameCenter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameCenter(manager));
        }

    }
}
