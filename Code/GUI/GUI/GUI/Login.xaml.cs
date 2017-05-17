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
        MainWindow main;
        public Login(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            username.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!username.Text.Equals(""))
            {
                
                //main.mainFrame.NavigationService.Navigate(new UserMainPage(main));
                //main.statusFrame.NavigationService.Navigate(new Status(main));
                if (Communication.AuthFunctions.Instance.login(username.Text, password.Password)) {
                    main.statusFrame.NavigationService.Navigate(new Status(main));
                    main.mainFrame.NavigationService.Navigate(new UserMainPage(main));
                }
                else
                    MessageBox.Show("Bad Input", "    WARNING    ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Bad Input", "    WARNING    ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }
    }
}
