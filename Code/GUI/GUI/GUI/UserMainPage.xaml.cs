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

            byte [] byte_avatar = manager.GetProfile().avatar;

            if (byte_avatar != null)
            {
                var stream = new MemoryStream(byte_avatar);
                stream.Seek(0, SeekOrigin.Begin);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                ImageSourceConverter c = new ImageSourceConverter();
                user_avatar = (Image)c.ConvertFrom(image);

            }
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfile(manager));
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                await Communication.AuthFunctions.Instance.logout();
                manager.ClearStatusFrame();
                NavigationService.Navigate(new Login(manager));
            }
        }

        private void GameCenter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameCenter(manager));
        }

    }
}
