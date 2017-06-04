using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GUI
{
    class PlayerAtTable
    {
        public Label Label { get; set; }
        public Image FirstCard { get; set; }
        public Image SecondCard { get; set; }
        public string Username { get; set; }
        public int BetAmount { get; set; }
        public int Credit { get; set; }

        public PlayerAtTable(Label lbl,Image firstCard,Image secondCard)
        {
            Label = lbl;
            FirstCard = firstCard;
            SecondCard = secondCard;
            Username = "";
            BetAmount = 0;
        }

        public void Bet(int betAmount)
        {
            BetAmount += betAmount;
            Credit -= betAmount;
            RefreshLabel();
        }

        public void RefreshLabel()
        {
            Label.Content = Username + " $" + BetAmount +"\nCredit: $"+Credit;
        }
        public void Fold()
        {
            BetAmount = 0;
            Label.Content = Username + " Folded";
            FirstCard.Visibility = Visibility.Hidden;
            SecondCard.Visibility = Visibility.Hidden;
        }

        internal void ShowLabels(string username)
        {
            Username = username;
            RefreshLabel();
            Label.Visibility = Visibility.Visible;
            FirstCard.Visibility = Visibility.Visible;
            SecondCard.Visibility = Visibility.Visible;
        }

        public void SetCards(Models.PlayerHand hand)
        {
            FirstCard.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.First.toImage(), UriKind.Relative));
            SecondCard.Source = new BitmapImage(new Uri(@"Images\Cards\" + hand.Second.toImage(), UriKind.Relative));
        }

        public void ReSetCards()
        {
            FirstCard.Source = new BitmapImage(new Uri(@"Images\Cards\back.png", UriKind.Relative));
            SecondCard.Source = new BitmapImage(new Uri(@"Images\Cards\back.png", UriKind.Relative));
        }

        internal void Remove()
        {
            Fold();
            ReSetCards();
            Label.Visibility = Visibility.Hidden;
            Username = "";
        }
    }
}
