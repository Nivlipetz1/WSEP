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
    /// Interaction logic for UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Page
    {
        MainWindow main;
        public Status statusFrame { get; set; }
        public List<GameFrame> gameList { get; set; }
        public UserMainPage(MainWindow main, Status statusFrame)
        {
            InitializeComponent();
            gameList = new List<GameFrame>();
            this.main = main;
            this.statusFrame = statusFrame;
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfile(main,this));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                main.statusFrame.Content = null;
                NavigationService.Navigate(new Login(main));
            }
        }

        private void GameCenter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameCenter(main,this));
        }

        public void AddGame(GameFrame gf)
        {
            gameList.Add(gf);
            statusFrame.RefreshGameList();
        }

        public void RemoveGame(GameFrame gf)
        {
            if (gameList.Contains(gf))
            {
                gameList.Remove(gf);
                statusFrame.RefreshGameList();
            }
        }

        private GameFrame findGame(int gameID)
        {
            GameFrame wantedFrame = null;
            foreach (GameFrame gf in gameList)
            {
                if (gf.getGame().GamePref.gameID == gameID)
                {
                    wantedFrame = gf;
                }
            }
            return wantedFrame;
        }

        public void NotifyTurn(int minimumBet,int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            wantedFrame.gameWindow.MyTurn(minimumBet);
        }

        public void PushHand(Models.PlayerHand hand, int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            wantedFrame.gameWindow.DealCards(hand);
        }

        public void PushMoveToGame(Models.Move move, int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            if (move is Models.BetMove)
            {
                wantedFrame.gameWindow.PushBetMove((Models.BetMove)move);
            }
            else if (move is Models.FoldMove)
            {
                wantedFrame.gameWindow.PushFoldMove((Models.FoldMove)move);
            }
            else if (move is Models.GameStartMove)
            {
                wantedFrame.gameWindow.PushGameStartMove((Models.GameStartMove)move);
            }
            else if (move is Models.NewCardMove)
            {
                wantedFrame.gameWindow.NewCardMove((Models.NewCardMove)move);
            }
        }
    }
}
