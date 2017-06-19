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
        public GameReplayCont GameReplayCont { get; set; }
        public int gameID { get; set; }
        private bool initialized = false;
        private List<Models.Move> moves;
        private int movesIndex = 0;
        public GameFrame(GUIManager manager, Models.ClientGame game)
        {
            this.manager = manager;
            this.game = game;
            this.gameID = game.id;

        }

        public GameFrame(GUIManager manager, int gameID, List<Models.Move> moves)//USED ONLY FOR REPLAYS
        {
            this.manager = manager;
            this.game = null;
            this.gameID = gameID;
            this.moves = moves;

        }

        public void Init(bool SpecMode)
        {
            if (!initialized)
            {
                initialized = true;
                InitializeComponent();
                gameFrame.NavigationService.Navigate(GameWindow = new Game(manager, gameID, SpecMode, false));
                chatFrame.NavigationService.Navigate(GameChat = new GameChat(manager, gameID));
                pmFrame.NavigationService.Navigate(GamePM = new GamePM(manager, gameID));
            }
        }

        public void InitReplay()
        {
            if (!initialized)
            {
                initialized = true;
                InitializeComponent();
                gameFrame.NavigationService.Navigate(GameWindow = new Game(manager, gameID, true,true));
                chatFrame.NavigationService.Navigate(GameReplayCont = new GameReplayCont(this,moves.Count));
                pmFrame.Foreground = new SolidColorBrush(Colors.DarkSlateGray);
            }
           
        }

        public Models.ClientGame getGame()
        {
            return game;
            
        }

        public int getGameID()
        {
            return gameID;

        }

        internal void PushMove()
        {
            manager.PushMoveToGame(moves[movesIndex], gameID);
            movesIndex++;
            if(movesIndex == moves.Count)
            {
                GameReplayCont.DisablePlayButton();
            }
        }

        public override string ToString()
        {
            return "Game "+game.id;
        }

        internal void RemovePlayer(string username)
        {
            GamePM.RefreshSelectionList();
            GameWindow.removePlayer(username);
            game.RemovePlayer(username);
        }



        /*public void AddPlayer(Models.ClientUserProfile profile)
        {
            game.AddPlayer(profile);
            GamePM.RefreshSelectionList();
        }*/
    }
}
