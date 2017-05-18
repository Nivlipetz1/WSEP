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
        private Models.ClientGame game;
        MainWindow main;
        GameCenter gCenter;
        public Game gameWindow { get; set; }
        public GameFrame(MainWindow main, Models.ClientGame game, GameCenter gCenter)
        {
            this.gCenter = gCenter;
            this.main = main;
            this.game = game;
            InitializeComponent();
            gameWindow = new Game(this, gCenter);
            gameFrame.NavigationService.Navigate(gameWindow);
            chatFrame.NavigationService.Navigate(new GameChat(this));
            pmFrame.NavigationService.Navigate(new GamePM(this));
        }

        public Models.ClientGame getGame()
        {
            return game;
            
        }

        public override string ToString()
        {
            return "Game "+game.GamePref.GameID;
        }
    }
}
