﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        int gameID;
        GUIManager manager;
        //private List<Label> playerLabels;
        private List<Image> playersCards;
        private List<PlayerAtTable> players;
        private int revealCard = 0;
        private int minimumBet = 0;
        private int potSizeInt = 0;
        private int playerCredit = 0;
        private int playerBet = 0;

        public static SoundPlayer snd = new SoundPlayer(Properties.Resources.cardsdealt1);
        public static SoundPlayer snd2 = new SoundPlayer(Properties.Resources.cardsdealt2);
        public static SoundPlayer bet_sound = new SoundPlayer(Properties.Resources.chaching);
        public static SoundPlayer startGameSound = new SoundPlayer(Properties.Resources.TableOpenForBetting);
        public static SoundPlayer WinnerSound = new SoundPlayer(Properties.Resources.WeveGotAWinner);
        public static SoundPlayer LoserSound = new SoundPlayer(Properties.Resources.LoserSound);
        public Game(GUIManager manager, int gameID, bool SpecMode)
        {
            InitializeComponent();
            this.manager = manager;
            this.gameID = gameID;
            players = new List<PlayerAtTable>();
            players.Add(new PlayerAtTable(player3, Card3, Card4));
            players.Add(new PlayerAtTable(player4, Card1, Card2));
            players.Add(new PlayerAtTable(player5, Card5, Card6));
            players.Add(new PlayerAtTable(player7, Card7, Card8));
            players.Add(new PlayerAtTable(player6, Card9, Card10));
            players.Add(new PlayerAtTable(player1, Card11, Card12));
            players.Add(new PlayerAtTable(player2, Card13, Card14));
            if(SpecMode)
                players.Add(new PlayerAtTable(betted, UserCard1, UserCard2));
            //playerLabels = new List<Label>();
            playersCards = new List<Image>();
            playersCards.Add(Card3);
            playersCards.Add(Card4);
            playersCards.Add(Card1);
            playersCards.Add(Card2);
            playersCards.Add(Card5);
            playersCards.Add(Card6);
            playersCards.Add(Card7);
            playersCards.Add(Card8);
            playersCards.Add(Card9);
            playersCards.Add(Card10);
            playersCards.Add(Card11);
            playersCards.Add(Card12);
            playersCards.Add(Card13);
            playersCards.Add(Card14);
            if (SpecMode)
            {
                playersCards.Add(UserCard1);
                playersCards.Add(UserCard2);
            }

            if (SpecMode)
            {
                SpecLbl.Visibility = Visibility.Visible;
            }

        }

        private void RepositionCards()
        {
            foreach(Image card in playersCards)
            {
                Canvas.SetLeft(card, 241);
                Canvas.SetTop(card, 152);
                card.Source = new BitmapImage(new Uri(@"Images\Cards\back.png", UriKind.Relative));
                //card.SetValue(Canvas.LeftProperty,241);
                //card.SetValue(Canvas.TopProperty,152);
            }
            Canvas.SetLeft(FlopCard1, 241);
            Canvas.SetTop(FlopCard1, 152);
            Canvas.SetLeft(FlopCard2, 241);
            Canvas.SetTop(FlopCard2, 152);
            Canvas.SetLeft(FlopCard3, 241);
            Canvas.SetTop(FlopCard3, 152);
            Canvas.SetLeft(RiverCard, 241);
            Canvas.SetTop(RiverCard, 152);
            Canvas.SetLeft(TurnCard, 241);
            Canvas.SetTop(TurnCard, 152);
            FlopCard1.Visibility = Visibility.Hidden;
            FlopCard2.Visibility = Visibility.Hidden;
            FlopCard3.Visibility = Visibility.Hidden;
            RiverCard.Visibility = Visibility.Hidden;
            TurnCard.Visibility = Visibility.Hidden;
        }


        public void NewCardMove(Models.NewCardMove move)
        {
            Models.Card[] cards = move.cards;
            switch (revealCard)
            {
                case 0:
                    FlopCard1.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[0].toImage(), UriKind.Relative));
                    FlopCard2.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[1].toImage(), UriKind.Relative));
                    FlopCard3.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[2].toImage(), UriKind.Relative));
                    FlopCard1.Visibility = Visibility.Visible;
                    FlopCard2.Visibility = Visibility.Visible;
                    FlopCard3.Visibility = Visibility.Visible;
                    snd2.Play();
                    snd2.Play();
                    snd2.Play();
                    MoveCard(FlopCard1, 210, 40);
                    MoveCard(FlopCard2, 140, 40);
                    MoveCard(FlopCard3, 70, 40);
                    revealCard++;
                    break;
                case 1:
                    TurnCard.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[3].toImage(), UriKind.Relative));
                    TurnCard.Visibility = Visibility.Visible;
                    snd2.Play();
                    MoveCard(TurnCard, 0, 40);
                    revealCard++;
                    break;
                case 2:
                    RiverCard.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[4].toImage(), UriKind.Relative));
                    RiverCard.Visibility = Visibility.Visible;
                    snd2.Play();
                    MoveCard(RiverCard, -70, 40);
                    revealCard++;
                    break;
            }
        }



        public void DealCards(Models.PlayerHand hand)
        {

            UserCard1.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
            UserCard2.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
            UserCard1.Visibility = Visibility.Visible;
            UserCard2.Visibility = Visibility.Visible;
            snd.Play();
            MoveCard(Card1, 55, -150);
            MoveCard(Card2, 70, -150);
            MoveCard(Card3, -130, -150);
            MoveCard(Card4, -145, -150);
            MoveCard(Card5, 200, -150);
            MoveCard(Card6, 215, -150);
            MoveCard(Card7, 290, 20);
            MoveCard(Card8, 305, 20);
            MoveCard(Card9, 290, 170);
            MoveCard(Card10, 305, 170);
            MoveCard(Card11, -190, 170);
            MoveCard(Card12, -205, 170);
            MoveCard(Card13, -190, 20);
            MoveCard(Card14, -205, 20);
            MoveCard(UserCard1, 0, 220);
            MoveCard(UserCard2, 30, 220);
        }

        public void PushBetMove(Models.BetMove move)
        {
            bet_sound.Play();
            int bet = move.GetAmount();
            potSizeInt += bet;
            PotSizeLbl.Content = "Pot Size: $" + potSizeInt;

            if (move.GetBettingPlayer().Equals(manager.GetProfile().username))
            {
                playerCredit -= bet;
                playerBet += bet;
                credit.Content = "Credit: $" + playerCredit;
                betted.Content = "$" + playerBet;
                return;
            }

            foreach (PlayerAtTable player in players)
            {
                if (player.Username.Equals(move.GetBettingPlayer()))
                {

                    player.Bet(move.GetAmount());
                    return;
                }
            }
        }


        public void PushFoldMove(Models.FoldMove move)
        {

            if (move.foldingPlayer.Equals(manager.GetProfile().username))
            {
                betted.Content = "Folded";
                UserCard1.Visibility = Visibility.Hidden;
                UserCard2.Visibility = Visibility.Hidden;
                return;
            }

            foreach (PlayerAtTable player in players)
            {
                if (player.Username.Equals(move.GetFoldingPlayer()))
                {
                    player.Fold();
                    break;
                }
            }
        }

        public void removePlayer(string username)
        {

            foreach (PlayerAtTable player in players)
            {
                if (player.Username.Equals(username))
                {
                    player.Remove();
                    break;
                }
            }
        }

        public void HideBetElements()
        {
            BetAmount.Visibility = Visibility.Hidden;
            Bet_Button.Visibility = Visibility.Hidden;
            Fold_Button.Visibility = Visibility.Hidden;
            MinimumBetLabel.Visibility = Visibility.Hidden;
            CheckBtn.Visibility = Visibility.Hidden;
        }

        public void ShowBetElements()
        {
            BetAmount.Visibility = Visibility.Visible;
            Bet_Button.Visibility = Visibility.Visible;
            Fold_Button.Visibility = Visibility.Visible;
            MinimumBetLabel.Visibility = Visibility.Visible;
            if (minimumBet == 0)
            {
                CheckBtn.IsEnabled = true;
            }
            else
            {
                CheckBtn.IsEnabled = false;
            }
            CheckBtn.Visibility = Visibility.Visible;
        }

        public void MyTurn(int minimumBet)
        {
            Bet_Button.IsEnabled = true;
            Fold_Button.IsEnabled = true;
            this.minimumBet = minimumBet;
            MinimumBetLabel.Content = "Minimum Bet: $" + minimumBet;
            ShowBetElements();
        }

        public void PushGameStartMove(Models.GameStartMove move)
        {
            foreach(PlayerAtTable player in players)
            {
                player.Remove();
            }
            manager.UpdatePlayerList(gameID,move);
            playerBet = 0;
            playerCredit = move.playerBets[manager.GetProfile().username];
            RepositionCards();
            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                foreach (PlayerAtTable player in players)
                {
                    if (player.Username.Equals(""))
                    {
                        player.Credit = move.playerBets[prof.username];
                        player.ShowLabels(prof.username);
                        break;
                    }
                }
            }
            potSizeInt = 0;
            revealCard = 0;

            BitmapImage image = manager.getAvatar();
            if (image != null)
            {
                playerAvatar.Source = image;
            }
            playerAvatar.Visibility = Visibility.Visible;
            credit.Content = "Credit: $" + playerCredit;
            credit.Visibility = Visibility.Visible;
            betted.Content = "$0";
            betted.Visibility = Visibility.Visible;
            PotSizeLbl.Content = "Pot Size: $" + potSizeInt;
            PotSizeLbl.Visibility = Visibility.Visible;
            startGameSound.PlaySync();

        }

        public void PushWinners(List<string> winners)
        {
            if (winners.Contains(manager.GetProfile().username))
                WinnerSound.Play();
            else
                LoserSound.Play();
            string winnersStr = "";
            foreach(string winner in winners)
            {
                winnersStr += "\n"+winner;
            }
            MessageBox.Show("The Winners Are: "+winnersStr+"\nThey each get "+potSizeInt/winners.Count, "Winners", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        //didn't check this..
        public void PushEndGameMove(Models.EndGameMove move)
        {
            foreach (PlayerAtTable player in players)
            {
                foreach (string username in move.handRanks.Keys)
                {
                    if (username.Equals(player.Username))
                    {
                        Models.PlayerHand hand = move.playerHands[username];
                        //lbl.Content = lbl.Content.ToString() + " with hand: " + hand.toString();
                        //FLIP THE CARDS:
                        player.SetCards(hand);
                        break;
                    }
                }
            }
        }


        /*
        public void EndGameMove(Models.EndGameMove move)
        {
            IDictionary<string, Models.PlayerHand> hands = move.playerHands;
            int cardIndex = 0;
            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                if (hands.ContainsKey(prof.username))
                {
                    Models.PlayerHand hand = hands[prof.username];
                    Image card1 = playersCards.ElementAt(cardIndex);
                    Image card2 = playersCards.ElementAt(cardIndex + 1);
                    card1.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
                    card2.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
                    cardIndex += 2;
                }
            }
            
        }*/


        private void MoveCard(Image card, int x, int y)
        {

            //card.Visibility = Visibility.Visible;
            TranslateTransform trans = new TranslateTransform();
            card.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, x, TimeSpan.FromSeconds(5));
            DoubleAnimation anim2 = new DoubleAnimation(0, y, TimeSpan.FromSeconds(5));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            manager.QuitGame(gameID);
        }

        private void Bet_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!BetAmount.Text.Equals(""))
            manager.Bet(gameID, BetAmount.Text, minimumBet, this);
        }

        private void Fold_Button_Click(object sender, RoutedEventArgs e)
        {
            Bet_Button.IsEnabled = false;
            Fold_Button.IsEnabled = false;
            manager.Fold(gameID, this);

        }

        private void BackToGC_Click(object sender, RoutedEventArgs e)
        {
            manager.GoToGameCenter();
        }

        private IEnumerable<Models.ClientUserProfile> RemoveSelfFromPlayersList(IEnumerable<Models.ClientUserProfile> players)
        {
            //create new list everytime, to not override the list in other clients.. (i think thats how it works)
            List<Models.ClientUserProfile> newPlayers = new List<Models.ClientUserProfile>();
            string userName = manager.GetProfile().username;
            foreach(Models.ClientUserProfile prof in players)
            {
                if (!prof.username.Equals(userName))
                    newPlayers.Add(prof);

            }

            return newPlayers;
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            manager.Bet(gameID, "0", minimumBet, this);
        }
    }
}
