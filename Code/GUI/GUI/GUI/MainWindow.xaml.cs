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
        public List<GameFrame> list;
        Models.ClientUserProfile prof;
        public MainWindow()
        {
            
            InitializeComponent();
            if (!Communication.Server.Instance.connect())
            {
                MessageBox.Show("not connected");
            }
            mainFrame.NavigationService.Navigate(new Login(this));
            list = new List<GameFrame>();
            prof = new Models.ClientUserProfile();
        }
        public void setProfile(string username)
        {
            prof = Communication.AuthFunctions.Instance.getClientUser(prof.Username);
        }
        public void RefreshProfile()
        {
            prof =  Communication.AuthFunctions.Instance.getClientUser(prof.Username);
        }

        public Models.ClientUserProfile getProfile()
        {
            return prof;
        }
    }
}
