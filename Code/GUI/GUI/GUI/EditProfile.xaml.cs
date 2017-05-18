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
using Microsoft.Win32;

namespace GUI
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {
        MainWindow main;
        UserMainPage umP;
        public EditProfile(MainWindow main, UserMainPage umP)
        {
            this.umP = umP;
            this.main = main;
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //inserted correct avatar file
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (!Password.Text.Equals(""))
            {
                if (Communication.AuthFunctions.Instance.editPassword(Password.Text))
                {
                    MessageBox.Show("Password Changed!");
                    changed = true;
                }
            }
            if (!Username.Text.Equals(""))
            {
                if (Communication.AuthFunctions.Instance.editPassword(Password.Text))
                {
                    MessageBox.Show("Username Changed!");
                    changed = true;
                }
            }
            if (changed)
            {
                main.RefreshProfile();
                umP.statusFrame.RefreshStatus();
                NavigationService.GoBack();
            }
        }
    }
}
