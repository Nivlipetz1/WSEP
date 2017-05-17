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
        public List<GameFrame> list;
        Models.ClientUserProfile prof;
        public MainWindow()
        {
            InitializeComponent();
            if (!(Communication.Server.Instance.connect()))
            {
                MessageBox.Show("Not Connected");
            }
            
            mainFrame.NavigationService.Navigate(new Login(this));
            list = new List<GameFrame>();
            prof = new Models.ClientUserProfile();
        }
        public void setProfile(string username)
        {
            prof = Communication.AuthFunctions.Instance.getClientUser(username);
        }
        public void RefreshProfile()
        {
            prof =  Communication.AuthFunctions.Instance.getClientUser(prof.Username);
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
    }
}
