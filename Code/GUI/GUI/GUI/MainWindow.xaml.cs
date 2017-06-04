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
        GUIManager manager;
        public MainWindow()
        {
            InitializeComponent();
            manager = new GUIManager(this);
            manager.ConnectToServer();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*           if (loggedIn)
                       {
                           Communication.AuthFunctions.Instance.logout(prof.Username);
                       }
                       loggedIn = false;
                       */
           manager.disconnectFromServer();
        }


    }
}
