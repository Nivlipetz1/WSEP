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
using System.Threading;
using System.Windows.Input;

namespace GUI
{
    public class GUIManager : ServerToClientFunctions
    {
        Models.ClientUserProfile profile = null;
        Status status;
        public bool isLoggedIn = false;
        private MainWindow mainWindow;
        Status statusWindow;
        public GUIManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            gamesList = new List<ClientGame>();
            this.status = new Status(this);
            gameList = new List<GameFrame>();
            
            statusWindow = new Status(this);
            Communication.GameFunctions.Instance.serverToClient = this;
            Communication.GameCenterFunctions.Instance.serverToClient = this;
            Communication.AuthFunctions.Instance.serverToClient = this;
        }

        public List<GameFrame> gameList { get; set; }
        public List<ClientGame> gamesList { get; set; }

        public void AddGame(ClientGame game)
        {
            game.waitingList = new List<ClientUserProfile>();
            game.waitingListSpec = new List<ClientUserProfile>();
            gamesList.Add(game);
        }

        public void RemoveGame(ClientGame game)
        {
            gamesList.Remove(game);
        }

        public void AddGameFrame(GameFrame gameFrame)
        {
            gameList.Add(gameFrame);
            status.AddGameToList(gameFrame.gameID);
        }

        public List<int> GetGameIDList()
        {
            List<int> list = new List<int>();
            foreach(ClientGame game in gamesList)
            {
                list.Add(game.id);
            }
            return list;
        }


        public void RemoveGameFrame(GameFrame gf)
        {
            if (gameList.Contains(gf))
            {
                gameList.Remove(gf);
                status.AddGameToList(gf.gameID);
            }
        }

        public BitmapImage getAvatar()
        {
            byte[] byte_avatar = GetProfile().avatar;
            BitmapImage image = null;
            if (byte_avatar != null)
            {
                image = LoadImage(byte_avatar);
            }
            return image;
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        internal bool isStringAPlayer(string str, int gameID)
        {
            Models.ClientGame game = findGame(gameID);
            bool value = false;
            foreach (Models.ClientUserProfile prof in game.players)
            {
                if (prof.username.Equals(str))
                {
                    value = true;
                    break;
                }
            }
            return value;
        }

        internal string GetMessages(int gameID,string v)
        {
            Models.ClientGame game = findGame(gameID);
            return game.GetMessages(v);
        }

        internal List<string> GetUsersForPM(int gameID)
        {
            Models.ClientGame game = findGame(gameID);
            return new List<string>(game.messageList.Keys);
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
            return findGame(gameID).spectators;
        }

        public void RemovePlayer(int gameID,string username)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameID);
                gameFrame.RemovePlayer(username);
                ClientGame game = findGame(gameID);
                foreach (ClientUserProfile prof in game.players)
                    if (prof.username.Equals(username))
                        game.players.Remove(prof);
            });
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
            string changedString = "";
            string notChanged = "";
            bool changed = false;

            if (password.Equals("") && username.Equals("") && avatar==null)
                notChanged = "Nothing to Change";

            if (!password.Equals(""))
            {
                if (await Communication.AuthFunctions.Instance.editPassword(password))
                {
                    changedString += "Password Changed!\n";
                    changed = true;
                }
                else
                {
                    notChanged += "Password Not Changed!\n";
                }
            }
            if (!username.Equals("")) //edit email
            {
                if (await Communication.AuthFunctions.Instance.editUserName(username))
                {
                    changedString += "Username Changed!\n";
                    changed = true;
                }
                else
                {
                    notChanged += "Username Not Changed!\n";
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
                        
                        changedString += "Avatar Changed!\n";
                        changed = true;

                    }
                    else
                    {
                        notChanged += "Avatar Not Changed!\n";
                    }
                }

            }

            if (changed)
            {
                MessageBox.Show(changedString+"\n"+notChanged,"Profile Updated",MessageBoxButton.OK,MessageBoxImage.Information);
                await RefreshProfile();
                mainPage.ShowAvatar();
                mainWindow.mainFrame.NavigationService.GoBack();
                return;
            }

                MessageBox.Show(notChanged, "Profile failed to update", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.mainFrame.NavigationService.GoBack();
            
        }

        internal async Task<List<int>> getAllAvailableReplayes()
        {
            return await Communication.GameCenterFunctions.Instance.getAllAvailableReplayes();
        }

        internal async void JoinGameAsSpectator(int gameID)
        {
           Models.ClientGame game = await Communication.GameCenterFunctions.Instance.spectateGame(gameID);
            if (game != null)
            {
                game.InitMessageList(profile.username);
                AddGame(game);
                await RefreshProfile();
                GameFrame gameFrame = new GameFrame(this, game);
                AddGameFrame(gameFrame);
                gameFrame.Init(true);
                NavigateToGameFrame(gameFrame);
            }
            else
            {
                MessageBox.Show("something went wrong:(");
            }
        }

        internal async void Register(string username, string password)
        {
            if (!username.Trim().Equals("") && !password.Trim().Equals("") && await Communication.AuthFunctions.Instance.register(username, password))
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

        internal async void GetReplay(int gameID)
        {
            List<Move> moves = await Communication.GameCenterFunctions.Instance.getReplayByGameId(gameID);
            GameFrame gf = new GameFrame(this, gameID,moves);
            AddGameFrame(gf);
            gf.InitReplay();
            NavigateToGameFrame(gf);
        }

        internal IEnumerable<ClientUserProfile> GetPlayers(int gameID)
        {
            return findGame(gameID).players;
        }

        private ClientGame findGame(int gameID)
        {
            ClientGame game = null;
            //while (gameList.Count == 0) ;
            foreach (ClientGame g in gamesList)
            {
                if (g.id == gameID)
                {
                    game = g;
                }
            }
            return game;
        }

        public GameFrame findGameFrame(int gameID)
        {
            GameFrame gameFrame = null;
            //while (gameList.Count == 0) ;
            foreach (GameFrame g in gameList)
            {
                if (g.getGameID() == gameID)
                {
                    gameFrame = g;
                }
            }
            return gameFrame;
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
                isLoggedIn = true;
            }
            else
            {
                MessageBox.Show("Bad Input", "    WARNING    ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<bool> SendPMMessage(string to, string message, int gameID)
        {
            if(await Communication.GameFunctions.Instance.postWhisperMessage(to, message, gameID))
            {
                ClientGame game = findGame(gameID);
                game.AddMyMessage(to, message);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NavigateToGameFrame(int selectedIndex)
        {
            GameFrame wantedFrame = null;
            foreach(GameFrame frame in gameList)
            {
                if (frame.gameID == selectedIndex)
                {
                    wantedFrame = frame;
                    break;
                }
            }
            if(wantedFrame!=null)
                NavigateToGameFrame(wantedFrame);
        }

        public void NotifyTurn(int minimumBet, int gameID)
        {
           // while (mutexLocks[gameID]) ;
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame wantedFrame = findGameFrame(gameID);
                wantedFrame.GameWindow.MyTurn(minimumBet);
            });
        }

        internal BitmapImage GetAvatar(int gameID, string username)
        {
            ClientGame game = findGame(gameID);
            return LoadImage(game.GetAvatar(username));
        }

        internal async void JoinGame(int gameID, int credit)
        {
                Models.ClientGame game = await Communication.GameCenterFunctions.Instance.joinGame(gameID, credit);
                if (game != null)
                {
                    game.InitMessageList(profile.username);
                    AddGame(game);
                    await RefreshProfile();
                    GameFrame gameFrame = new GameFrame(this, game);
                    AddGameFrame(gameFrame);
                    gameFrame.Init(false);
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
           // while (mutexLocks[gameID]) ;
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame wantedFrame = findGameFrame(gameID);
                    wantedFrame.GameWindow.DealCards(hand);
            });
        }

        public void PushWinners(List<string> winners,int gameID)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameID);
                
                    gameFrame.GameWindow.PushWinners(winners);
            });
        }

        public void Notify(string message)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {

                MessageBox.Show("System Message:\n"+message);
            });
        }

        public void PushMoveToGame(Models.Move move, int gameID)
        {
            //while (mutexLocks[gameID]) ;
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame wantedFrame = findGameFrame(gameID);
                if (wantedFrame != null)
                {
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
        });
        }

        internal async void QuitGame(int gameID, bool specMode)
        {
            MessageBoxResult rs = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (rs == MessageBoxResult.Yes)
            {
                if (specMode)
                {
                    if (await Communication.GameFunctions.Instance.removeSpectator(gameID))
                    {
                        RemoveGame(findGame(gameID));
                        RemoveGameFrame(findGameFrame(gameID));
                        await RefreshProfile();
                        GoToGameCenter();
                        //mainWindow.mainFrame.NavigationService.GoBack();
                    }
                    else
                        MessageBox.Show("Something went wrong", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (await Communication.GameFunctions.Instance.removePlayer(gameID))
                    {
                        RemoveGame(findGame(gameID));
                        RemoveGameFrame(findGameFrame(gameID));
                        await RefreshProfile();
                        GoToGameCenter();
                        //mainWindow.mainFrame.NavigationService.GoBack();
                    }
                    else
                        MessageBox.Show("Something went wrong", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        internal async void Bet(int gameID, string amount, int minimumBet, Game gameWindow)
        {
            int betAmount = 0;
            if (Int32.TryParse(amount,out betAmount) && betAmount >= minimumBet)
            {
                if (await Communication.GameFunctions.Instance.bet(gameID, betAmount.ToString()))
                {
                    gameWindow.HideBetElements();
                    //MessageBox.Show("Bet Accepted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bet is over your credit! \nplease try again.", "Too Low!", MessageBoxButton.OK, MessageBoxImage.Error);
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

        internal void GoToUserMainPage()
        {
            mainWindow.mainFrame.NavigationService.Navigate(new UserMainPage(this));
        }

        internal void StopReplay(int gameID)
        {
            findGameFrame(gameID).quitReplay();
        }

        private Status GetStatusFrame()
        {
            return status;
        }

        public void PlayerJoinedGame(int gameID,Models.ClientUserProfile prof)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameID);
                    if (gameFrame != null)
                    {
                        gameFrame.getGame().AddPlayerToWaitingList(prof);
                    }
        });
        }

        public void SpecJoinedGame(int gameID, Models.ClientUserProfile prof)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameID);
                if (gameFrame != null)
                {
                    gameFrame.getGame().AddSpecToWaitingList(prof);

                }
            });
        }

        public void PushPMMessage(int gameId, string sender, string message)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                ClientGame game = findGame(gameId);
                game.AddMessage(sender, message);
                GameFrame gameFrame = findGameFrame(gameId);
                gameFrame.GamePM.PushMessage(sender);
            });
        }

        public void PushChatMessage(int gameId, string sender, string message)
        {
            //while (mutexLocks[gameId]) ;
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameId);
                    gameFrame.GameChat.PushMessage(sender, message);
            });
        }

        internal void UpdatePlayerList(int gameID, GameStartMove move)
        {
            Models.ClientGame game = findGame(gameID);
            game.UpdatePlayerListFromWaitingList();
            foreach (Models.ClientUserProfile prof in game.players)
            {
                prof.credit = move.playerBets[prof.username];
            }
        }

        public void SpecQuitGame(string player, int gameId)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                GameFrame gameFrame = findGameFrame(gameId);
                if (gameFrame != null)
                {
                    ClientGame cg = findGame(gameId);
                    gameFrame.RemoveSpec(player);
                    cg.RemoveSpec(player);
                    gameFrame.GameWindow.updateSpec();
                }
            });
        }

        public void PlayerQuitGame(string player, int gameId)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(async() =>
            {
                GameFrame gameFrame = findGameFrame(gameId);
                if (gameFrame != null)
                {
                    ClientGame cg = findGame(gameId);
                    gameFrame.RemovePlayer(player);
                    cg.RemovePlayer(player);

                    if (profile.username.Equals(player))
                    {
                        RemoveGame(findGame(gameId));
                        RemoveGameFrame(findGameFrame(gameId));
                        await RefreshProfile();
                        GoToGameCenter();
                    }
                }
            });
        }

        public void dbDown()
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                MessageBox.Show("DB is down\nWill be back shortly.. please hold on.", "System Message", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = Cursors.Wait;
                lock (this)
                {
                    Monitor.Wait(this);
                }
            });
        }

        public void dbUp()
        {
            lock (this)
            {
                Monitor.PulseAll(this);
            }
            Application.Current.Dispatcher.InvokeAsync(() => { 
                        Mouse.OverrideCursor = null;
            });
        }
    }
}
