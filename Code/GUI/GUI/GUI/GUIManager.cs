using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GUI.Models;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;

namespace GUI
{
    public class GUIManager : ServerToClientFunctions
    {
        Models.ClientUserProfile profile = null;
        Status status;
        private MainWindow mainWindow;
        Status statusWindow;

        public GUIManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.status = new Status(this);
            gameList = new List<GameFrame>();
            statusWindow = new Status(this);
            Communication.GameFunctions.Instance.serverToClient = this;
            Communication.GameCenterFunctions.Instance.serverToClient = this;
            Communication.AuthFunctions.Instance.serverToClient = this;
        }

        public List<GameFrame> gameList { get; set; }

        public void AddGameFrame(GameFrame gameFrame)
        {
            gameList.Add(gameFrame);
            status.RefreshGameList();
        }


        public void RemoveGameFrame(GameFrame gf)
        {
            if (gameList.Contains(gf))
            {
                gameList.Remove(gf);
                status.RefreshGameList();
            }
        }

        internal void ConnectToServer()
        {
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
                    mainWindow.Close();
                }
            }
            else
            {

                mainWindow.mainFrame.NavigationService.Navigate(new Login(this));

                profile = new Models.ClientUserProfile();
            }
        }

        internal IEnumerable<ClientUserProfile> GetSpectators(int gameID)
        {
            return findGame(gameID).getGame().spectators;
        }

        public void RemovePlayer(int gameID,string username)
        {
            GameFrame gameFrame = findGame(gameID);
            gameFrame.RemovePlayer(username);

        }

        internal async Task<List<ClientGame>> SearchGames(string criterion,object parameter)
        {
            return await Communication.GameCenterFunctions.Instance.getActiveGames(criterion,parameter);
        }

        internal async Task<List<ClientGame>> SearchGamesToSpectate()
        {
            return await Communication.GameCenterFunctions.Instance.getAllSpectatingGames();
        }

        internal void disconnectFromServer()
        {
            Communication.Server.Instance.disconnect();
        }

        internal async void EditProfile(string username, string password, BitmapImage avatar,UserMainPage mainPage)
        {
            bool changed = false;
            if (!password.Equals(""))
            {
                if (await Communication.AuthFunctions.Instance.editPassword(password))
                {
                    MessageBox.Show("Password Changed!");
                    changed = true;
                }
            }
            if (!username.Equals(""))
            {
                if (await Communication.AuthFunctions.Instance.editUserName(username))
                {
                    MessageBox.Show("Username Changed!");
                    changed = true;
                }
            }
            if (avatar != null)
            {
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(avatar));
                using(MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                if (data.Length > 32000)
                {
                    MessageBox.Show("Avatar size too big!");
                }
                else
                {
                    if (await Communication.AuthFunctions.Instance.editAvatar(data))
                    {
                        mainPage.ShowAvatar();
                        MessageBox.Show("Avatar Changed!");
                        changed = true;

                    }
                }

            }

            if (changed)
            {
                await RefreshProfile();
                mainWindow.mainFrame.NavigationService.GoBack();
            }
        }

        internal async void Register(string username, string password)
        {
            if (await Communication.AuthFunctions.Instance.register(username, password))
            {
                MessageBox.Show("You can now login with you credentials.", "Registration Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.mainFrame.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Bad Input, Please try again with different username.", "Registration Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void ClearStatusFrame()
        {
            mainWindow.statusFrame.Content = null;
        }

        internal ClientUserProfile GetProfile()
        {
            return profile;
        }

        internal async Task<bool> PostChatMessage(string message, int gameID)
        {
            return await Communication.GameFunctions.Instance.postMessage(message, gameID);
        }

        internal IEnumerable<ClientUserProfile> GetPlayers(int gameID)
        {
            return findGame(gameID).getGame().players;
        }

        private GameFrame findGame(int gameID)
        {
            GameFrame wantedFrame = null;
            foreach (GameFrame gf in gameList)
            {
                if (gf.getGame().id == gameID)
                {
                    wantedFrame = gf;
                }
            }
            return wantedFrame;
        }

        internal async void CreateGame(GamePreferences pref)
        {
            Models.ClientGame newGame = await Communication.GameCenterFunctions.Instance.createGame(pref);
            if (newGame == null)
            {
                MessageBox.Show("Something went wrong!", "Oh Oh!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("New game created successfuly!", "New Game Created!", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.mainFrame.NavigationService.GoBack();

            }
        }

        internal IEnumerable<GameFrame> GetGameFrameList()
        {
            return gameList;
        }

        public async Task RefreshProfile()
        {
            profile = await Communication.AuthFunctions.Instance.getClientUser();
            statusWindow.RefreshStatus();
        }

        public async Task Login(string username, string password)
        {
            //main.mainFrame.NavigationService.Navigate(new UserMainPage(main));
            //main.statusFrame.NavigationService.Navigate(new Status(main));
            if (await Communication.AuthFunctions.Instance.login(username, password))
            {
                await RefreshProfile();
                Status status = statusWindow;
                UserMainPage umP = new UserMainPage(this);
                mainWindow.statusFrame.NavigationService.Navigate(status);
                mainWindow.mainFrame.NavigationService.Navigate(umP);
            }
            else
            {
                MessageBox.Show("Bad Input", "    WARNING    ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<bool> SendPMMessage(string to, string message, int gameID)
        {
            return await Communication.GameFunctions.Instance.postWhisperMessage(to, message, gameID);
        }

        public void NavigateToGameFrame(int selectedIndex)
        {
            NavigateToGameFrame(gameList[selectedIndex]);
        }

        public void NotifyTurn(int minimumBet, int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            wantedFrame.GameWindow.MyTurn(minimumBet);
        }

        internal async void JoinGame(int gameID, int credit)
        {
            Models.ClientGame game = await Communication.GameCenterFunctions.Instance.joinGame(gameID, credit);
            if (game != null)
            {
                await RefreshProfile();
                GameFrame gameFrame = new GameFrame(this, game);
                AddGameFrame(gameFrame);
                gameFrame.Init();
                NavigateToGameFrame(gameFrame);
            }
            else
            {
                MessageBox.Show("something went wrong:(");
            }
        }

        private void NavigateToGameFrame(GameFrame gameFrame)
        {
            mainWindow.mainFrame.NavigationService.Navigate(gameFrame);
        }

        public void PushHand(Models.PlayerHand hand, int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            wantedFrame.GameWindow.DealCards(hand);
        }

        public void Notify(string message)
        {
            MessageBox.Show("System Message:\nmessage");
        }

        public void PushMoveToGame(Models.Move move, int gameID)
        {
            GameFrame wantedFrame = findGame(gameID);
            if (move is Models.BetMove)
            {
                wantedFrame.GameWindow.PushBetMove((Models.BetMove)move);
            }
            else if (move is Models.FoldMove)
            {
                wantedFrame.GameWindow.PushFoldMove((Models.FoldMove)move);
            }
            else if (move is Models.GameStartMove)
            {
                wantedFrame.GameWindow.PushGameStartMove((Models.GameStartMove)move);
            }
            else if (move is Models.NewCardMove)
            {
                wantedFrame.GameWindow.NewCardMove((Models.NewCardMove)move);
            }
            else if (move is Models.EndGameMove)
            {
                wantedFrame.GameWindow.PushEndGameMove((Models.EndGameMove)move);
            }
        }

        internal async void QuitGame(int gameID)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                if (await Communication.GameFunctions.Instance.removePlayer(gameID))
                    mainWindow.mainFrame.NavigationService.GoBack();
                else
                    MessageBox.Show("Something went wrong", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal async void Bet(int gameID, int amount, int minimumBet, Game gameWindow)
        {
            if (amount >= minimumBet)
            {
                if (await Communication.GameFunctions.Instance.bet(gameID, amount.ToString()))
                {
                    gameWindow.HideBetElements();
                    MessageBox.Show("Bet Accepted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Minimum bet is " + minimumBet + "! please try again.", "Too Low!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal async void Fold(int gameID, Game gameWindow)
        {
            if (await Communication.GameFunctions.Instance.bet(gameID, "Fold"))
            {
                gameWindow.HideBetElements();
                MessageBox.Show("You have folded", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        internal void GoToGameCenter()
        {
            mainWindow.mainFrame.NavigationService.Navigate(new GameCenter(this));
        }

        private Status GetStatusFrame()
        {
            return status;
        }

        /*public void PlayerJoinedGame(int gameID,string username)
        {
            GameFrame gameFrame = findGame(gameID);
            gameFrame.
        }*/

        public void PushPMMessage(int gameId, string sender, string message)
        {
            GameFrame gameFrame = findGame(gameId);
            gameFrame.GamePM.PushMessage(sender, message);
        }

        public void PushChatMessage(int gameId, string sender, string message)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGame(gameId);
                gameFrame.GameChat.PushMessage(sender, message);
            });
        }
    }
}
