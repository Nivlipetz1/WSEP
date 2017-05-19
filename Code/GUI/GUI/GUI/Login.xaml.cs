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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        GUIManager manager;
        public Login(GUIManager manager)
        {
            this.manager = manager;
            InitializeComponent();
            username.Focus();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!username.Text.Equals(""))
            {
                await manager.Login(username.Text,password.Password);
            }
            else
            {
                MessageBox.Show("Bad Input", "    WARNING    ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register(manager));
        }
    }
}
