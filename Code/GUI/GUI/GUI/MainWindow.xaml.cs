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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool loggedIn = false;

        Models.ClientUserProfile prof;
        public MainWindow()
        {
            InitializeComponent();
        TRY_AGAIN:
            if (!(Communication.Server.Instance.connect()))
            {
                MessageBoxResult rs = MessageBox.Show("Could not connect.\nClick Yes to try again or No to quit", "No Connection", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No);
                if (rs == MessageBoxResult.Yes)
                {
                    goto TRY_AGAIN;
                }
                else
                {
                    Close();
                }
            }
            else
            {

                mainFrame.NavigationService.Navigate(new Login(this));
                
                prof = new Models.ClientUserProfile();
            }
        }
        public void setProfile()
        {
            prof = Communication.AuthFunctions.Instance.getClientUser();
        }
        public void RefreshProfile()
        {
            prof =  Communication.AuthFunctions.Instance.getClientUser();
        }

        public Models.ClientUserProfile getProfile()
        {
            return prof;
        }

        public bool getLoggedIn()
        {
            return loggedIn;
        }

        public void setLoggedIn(bool set)
        {
            loggedIn = set;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (loggedIn)
            {
                Communication.AuthFunctions.Instance.logout(prof.Username);
            }
            loggedIn = false;
        }

        public void notify(string message)
        {
            MessageBox.Show("System Message:\nmessage");
        }
    }
}
