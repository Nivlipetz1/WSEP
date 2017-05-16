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
        private GameFrame gameFrame;
        private List<Label> playerLabels;
        private List<Image> playersCards;
        private int revealCard = 0;

        public static SoundPlayer snd = new SoundPlayer(Properties.Resources.cardsdealt1);
        public static SoundPlayer snd2 = new SoundPlayer(Properties.Resources.cardsdealt2);
        public Game(GameFrame gameFrame)
        {
            InitializeComponent();

            this.gameFrame = gameFrame;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startgameBtn.Visibility = Visibility.Hidden;

            StartGame(new Models.GameStartMove());
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
            MoveCard(UserCard1, 0,220);
            MoveCard(UserCard2, 30, 220);

            Models.BetMove bet = new Models.BetMove();
            bet.setPlayer("niv");
            bet.setAmt(100);
            PushBetMove(bet);
            Models.BetMove bet2 = new Models.BetMove();
            bet2.setPlayer("omer");
            bet2.setAmt(200);
            PushBetMove(bet2);

        }

        public void EndGameMove(Models.EndGameMove move)
        {
            IDictionary<string, Models.PlayerHand> hands = move.GetPlayerHands();
            Models.ClientGame game = gameFrame.getGame();
            int cardIndex = 0;
            foreach (Models.ClientUserProfile prof in game.Players)
            {
                if (hands.ContainsKey(prof.Username))
                {
                    Models.PlayerHand hand = hands[prof.Username];
                    Image card1 = playersCards.ElementAt(cardIndex);
                    Image card2 = playersCards.ElementAt(cardIndex + 1);
                    FlopCard1.Source = new BitmapImage(new Uri(@"./Images/Cards/" + hand.getFirst().toImage()));
                    FlopCard1.Source = new BitmapImage(new Uri(@"./Images/Cards/" + hand.getSecond().toImage()));
                    cardIndex += 2;
                }
            }
        }

        public void NewCardMove(Models.NewCardMove move)
        {
            Models.Card[] cards = move.Cards;
            switch (revealCard)
            {
                case 0:
                    FlopCard1.Source = new BitmapImage(new Uri(@"./Images/Cards/" + cards[0].toImage()));
                    FlopCard2.Source = new BitmapImage(new Uri(@"./Images/Cards/" + cards[1].toImage()));
                    FlopCard3.Source = new BitmapImage(new Uri(@"./Images/Cards/" + cards[2].toImage()));
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
                    TurnCard.Source = new BitmapImage(new Uri(@"./Images/Cards/" + cards[0].toImage()));
                    TurnCard.Visibility = Visibility.Visible;
                    snd2.Play();
                    MoveCard(RiverCard, 0, 40);
                    revealCard++;
                    break;
                case 2:
                    RiverCard.Source = new BitmapImage(new Uri(@"./Images/Cards/" + cards[0].toImage()));
                    RiverCard.Visibility = Visibility.Visible;
                    snd2.Play();
                    MoveCard(TurnCard, -70, 40);
                    revealCard++;
                    break;
            }
        }

        private void StartGame(Models.GameStartMove move)
        {
            betted.Content = "$0";
            betted.Visibility = Visibility.Visible;
            Models.ClientGame game = gameFrame.getGame();
            int index = 0;
            int cardIndex = 0;
            foreach(Models.ClientUserProfile prof in game.Players)
            {
                Label lbl = playerLabels.ElementAt(index);
                int bet = 0;
                lbl.Content = prof.Username + " $"+bet;
                lbl.Visibility = Visibility.Visible;
                playersCards.ElementAt(cardIndex).Visibility = Visibility.Visible;
                playersCards.ElementAt(cardIndex+1).Visibility = Visibility.Visible;
                index++;
                cardIndex += 2;
            }
            UserCard1.Visibility = Visibility.Visible;
            UserCard2.Visibility = Visibility.Visible;
            BetAmount.Visibility = Visibility.Visible;
            Bet_Button.Visibility = Visibility.Visible;
            Fold_Button.Visibility = Visibility.Visible;
        }

        public void PushBetMove(Models.BetMove move)
        {
            Models.ClientGame game = gameFrame.getGame();
            int bet = move.GetAmount();
            int index = 0;
            int cardIndex = 0;

            foreach (Models.ClientUserProfile prof in game.Players)
            {
                if (prof.Username.Equals(move.GetBettingPlayer()))
                {
                    Label lbl = playerLabels.ElementAt(index);
                    lbl.Content = prof.Username + " $" + bet;
                    lbl.Visibility = Visibility.Visible;
                    playersCards.ElementAt(cardIndex).Visibility = Visibility.Visible;
                    playersCards.ElementAt(cardIndex + 1).Visibility = Visibility.Visible;
                    break;
                }

                index++;
                cardIndex += 2;
            }
        }


        public void PushGameStartMove(Models.GameStartMove move)
        {
            MessageBox.Show("Game Started!");
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
    }
}
