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
    /// Interaction logic for GameFrame.xaml
    /// </summary>
    public partial class GameFrame : Page
    {
        public GameFrame()
        {
            InitializeComponent();
            gameFrame.NavigationService.Navigate(new Game());
            chatFrame.NavigationService.Navigate(new GameChat());
            pmFrame.NavigationService.Navigate(new GamePM());
        }
    }
}
