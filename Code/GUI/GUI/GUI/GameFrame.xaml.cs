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
        GUIManager manager;
        public Game GameWindow { get; set; }
        public GameChat GameChat { get; set; }
        public GamePM GamePM { get; set; }
        public int gameID { get; set; }
        public GameFrame(GUIManager manager, Models.ClientGame game)
        {
            this.manager = manager;
            this.game = game;
            this.gameID = gameID;

        }

        public void Init()
        {
            InitializeComponent();
            gameFrame.NavigationService.Navigate(GameWindow = new Game(manager, gameID));
            chatFrame.NavigationService.Navigate(GameChat = new GameChat(manager, gameID));
            pmFrame.NavigationService.Navigate(GamePM = new GamePM(manager, gameID));
        }

        public Models.ClientGame getGame()
        {
            return game;
            
        }

        public override string ToString()
        {
            return "Game "+game.id;
        }

        internal void RemovePlayer(string username)
        {
            GamePM.RemovePlayer(username);
            GameWindow.removePlayer(username);
            
        }
    }
}
