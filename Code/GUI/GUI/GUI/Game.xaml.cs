using System;
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
        private List<Label> playerLabels;
        private List<Image> playersCards;
        private int revealCard = 0;
        private int minimumBet = 0;

        public static SoundPlayer snd = new SoundPlayer(Properties.Resources.cardsdealt1);
        public static SoundPlayer snd2 = new SoundPlayer(Properties.Resources.cardsdealt2);
        public Game(GUIManager manager, int gameID)
        {
            InitializeComponent();
            this.manager = manager;
            this.gameID = gameID;
            playerLabels = new List<Label>();
            playersCards = new List<Image>();
            playerLabels.Add(player3);
            playerLabels.Add(player4);
            playerLabels.Add(player5);
            playerLabels.Add(player7);
            playerLabels.Add(player6);
            playerLabels.Add(player1);
            playerLabels.Add(player2);
            playersCards.Add(Card1);
            playersCards.Add(Card2);
            playersCards.Add(Card3);
            playersCards.Add(Card4);
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

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {

            PushGameStartMove(new Models.GameStartMove());
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
            //            MoveCard(FlopCard1, 210, 40);
            //            MoveCard(FlopCard2, 140, 40);
            //            MoveCard(FlopCard3, 70, 40);
            //            MoveCard(RiverCard, 0, 40);
            //            MoveCard(TurnCard, -70, 40);
            MoveCard(UserCard1, 0, 220);
            MoveCard(UserCard2, 30, 220);

            Models.BetMove bet = new Models.BetMove();
            bet.setPlayer("niv");
            bet.setAmt(100);
            PushBetMove(bet);
            Models.BetMove bet2 = new Models.BetMove();
            bet2.setPlayer("omer");
            bet2.setAmt(200);
            PushBetMove(bet2);

            Models.NewCardMove nm = new Models.NewCardMove();
            Models.Card c1 = new Models.Card(10, Models.Card.Suit.SPADE);
            Models.Card c2 = new Models.Card(10, Models.Card.Suit.SPADE);
            Models.Card c3 = new Models.Card(1, Models.Card.Suit.SPADE);
            Models.Card[] cArray = { c1, c2, c3 };

            nm.Cards = cArray;
            //NewCardMove(nm);

            Models.FoldMove fm = new Models.FoldMove();
            fm.SetFoldPlayer("naor");
            PushFoldMove(fm);

        }

        public void NewCardMove(Models.NewCardMove move)
        {
            Models.Card[] cards = move.Cards;
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
                    MoveCard(RiverCard, 0, 40);
                    revealCard++;
                    break;
                case 2:
                    RiverCard.Source = new BitmapImage(new Uri(@"Images\Cards\" + cards[4].toImage(), UriKind.Relative));
                    RiverCard.Visibility = Visibility.Visible;
                    snd2.Play();
                    MoveCard(TurnCard, -70, 40);
                    revealCard++;
                    break;
            }
        }



        public void DealCards(Models.PlayerHand hand)
        {
            int index = 0;
            int cardIndex = 0;
            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                Label lbl = playerLabels.ElementAt(index);
                int bet = 0;
                lbl.Content = prof.Username + " $" + bet;
                lbl.Visibility = Visibility.Visible;
                playersCards.ElementAt(cardIndex).Visibility = Visibility.Visible;
                playersCards.ElementAt(cardIndex + 1).Visibility = Visibility.Visible;
                index++;
                cardIndex += 2;
            }
            UserCard1.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
            UserCard2.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
            UserCard1.Visibility = Visibility.Visible;
            UserCard2.Visibility = Visibility.Visible;
        }

        public void PushBetMove(Models.BetMove move)
        {
            int bet = move.GetAmount();
            int index = 0;
            int cardIndex = 0;

            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                if (prof.Username.Equals(move.GetBettingPlayer()))
                {
                    Label lbl = playerLabels.ElementAt(index);
                    lbl.Content = prof.Username + " $" + bet;
                    break;
                }

                index++;
                cardIndex += 2;
            }
        }


        public void PushFoldMove(Models.FoldMove move)
        {
            int index = 0;
            int cardIndex = 0;

            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                if (prof.Username.Equals(move.GetFoldingPlayer()))
                {
                    Label lbl = playerLabels.ElementAt(index);
                    lbl.Visibility = Visibility.Hidden;
                    playersCards.ElementAt(cardIndex).Visibility = Visibility.Hidden;
                    playersCards.ElementAt(cardIndex + 1).Visibility = Visibility.Hidden;
                    break;
                }

                index++;
                cardIndex += 2;
            }
        }

        public void HideBetElements()
        {
            BetAmount.Visibility = Visibility.Hidden;
            Bet_Button.Visibility = Visibility.Hidden;
            Fold_Button.Visibility = Visibility.Hidden;
        }

        public void ShowBetElements()
        {
            BetAmount.Visibility = Visibility.Visible;
            Bet_Button.Visibility = Visibility.Visible;
            Fold_Button.Visibility = Visibility.Visible;
        }

        public void MyTurn(int minimumBet)
        {
            this.minimumBet = minimumBet;
            ShowBetElements();
        }

        public void PushGameStartMove(Models.GameStartMove move)
        {
            MessageBox.Show("Game Started!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            betted.Content = "$0";
            betted.Visibility = Visibility.Visible;
        }


        //didn't check this..
        public void PushEndGameMove(Models.EndGameMove move)
        {
            int index = 0;
            int cardIndex = 0;

            foreach (string username in move.GetPlayerHands().Keys)
            {
                //check self in the winners usernames
                if (!(username.Equals(manager.GetProfile().Username)))
                {
                    Label lbl = playerLabels.ElementAt(index);
                    int dollarIndex = lbl.Content.ToString().IndexOf('$') - 1; //-1 in order to get rid of "space" before dollar sign
                    int size = lbl.Content.ToString().Length - (lbl.Content.ToString().Length - dollarIndex);
                    string lblPlayerName = lbl.Content.ToString().Substring(0, size);

                    if (username.Equals(lblPlayerName))
                    {
                        Models.PlayerHand hand = move.GetPlayerHands()[username];
                        lbl.Content = lbl.Content.ToString() + " with hand: " + hand.toString();
                        //FLIP THE CARDS:
                        //playersCards[cardIndex]= new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
                        //playersCards[cardIndex+1] = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
                    }
                }

                index++;
                cardIndex += 2;
            }

        }

        public void EndGameMove(Models.EndGameMove move)
        {
            IDictionary<string, Models.PlayerHand> hands = move.GetPlayerHands();
            int cardIndex = 0;
            foreach (Models.ClientUserProfile prof in RemoveSelfFromPlayersList(manager.GetPlayers(gameID)))
            {
                if (hands.ContainsKey(prof.Username))
                {
                    Models.PlayerHand hand = hands[prof.Username];
                    Image card1 = playersCards.ElementAt(cardIndex);
                    Image card2 = playersCards.ElementAt(cardIndex + 1);
                    FlopCard1.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
                    FlopCard1.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
                    cardIndex += 2;
                }
            }
        }


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
            manager.Bet(gameID, Int32.Parse(BetAmount.Text), minimumBet, this);
        }

        private void Fold_Button_Click(object sender, RoutedEventArgs e)
        {
            manager.Fold(gameID, this);

        }

        private void BackToGC_Click(object sender, RoutedEventArgs e)
        {
            manager.GoToGameCenter();
        }

        private IEnumerable<Models.ClientUserProfile> RemoveSelfFromPlayersList(IEnumerable<Models.ClientUserProfile> players)
        {
            //create new list everytime, to not override the list in other clients.. (i think thats how it works)
            List<Models.ClientUserProfile> newPlayers = players.ToList();

            if (newPlayers.Contains(manager.GetProfile()))
            {
                newPlayers.Remove(manager.GetProfile());
            }

            return newPlayers;
        }
    }
}
