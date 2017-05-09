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
        public Game()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startgameBtn.Visibility = Visibility.Hidden;
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
            MoveCard(FlopCard1, 210, 40);
            MoveCard(FlopCard2, 140, 40);
            MoveCard(FlopCard3, 70, 40);
            MoveCard(RiverCard, 0, 40);
            MoveCard(TurnCard, -70, 40);
            MoveCard(UserCard1, 0,220);
            MoveCard(UserCard2, 30, 220);
            BetAmount.Visibility = Visibility.Visible;
            Bet_Button.Visibility = Visibility.Visible;
            Fold_Button.Visibility = Visibility.Visible;


        }
        private void MoveCard(Image card, int x, int y)
        {
            card.Visibility = Visibility.Visible;
            TranslateTransform trans = new TranslateTransform();
            card.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, x, TimeSpan.FromSeconds(1));
            DoubleAnimation anim2 = new DoubleAnimation(0, y, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }
    }
}
