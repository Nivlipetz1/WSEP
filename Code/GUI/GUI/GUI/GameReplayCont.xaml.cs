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
    /// Interaction logic for GameReplayCont.xaml
    /// </summary>
    public partial class GameReplayCont : Page
    {
        public GameFrame gameFrame {get; set;}
        private int numOfMoves;
        private int moveCounter = 0;
        public GameReplayCont(GameFrame gf,int numOfMoves)
        {
            InitializeComponent();
            this.numOfMoves = numOfMoves;
            RefreshLabel();
            this.gameFrame = gf;

        }

        private void NextMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.PushMove();
            moveCounter++;
            RefreshLabel();
        }

        private void RefreshLabel()
        {
            moveCountLbl.Content = "Move #" + moveCounter + " From #" + numOfMoves;
        }

        internal void DisablePlayButton()
        {
            NextMoveBtn.IsEnabled = false;
        }
    }
}
