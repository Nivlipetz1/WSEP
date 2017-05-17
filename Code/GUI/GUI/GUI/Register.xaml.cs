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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {

            //if (Communication.AuthFunctions.Instance.register(Username.Text, Password.Password))
            if(true)
            {
                MessageBox.Show("You can now login with you credentials.", "Registration Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Bad Input, Please try again with different username.", "Registration Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
             
        }
    }
}
