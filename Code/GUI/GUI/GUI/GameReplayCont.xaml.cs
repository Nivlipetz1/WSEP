using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Object l;
        public GameFrame gameFrame {get; set;}
        private int numOfMoves;
        private int moveCounter = 0;
        private bool run;
        private Thread t1;
        public GameReplayCont(GameFrame gf,int numOfMoves)
        {
            InitializeComponent();
            this.numOfMoves = numOfMoves;
            RefreshLabel();
            this.gameFrame = gf;
            l = new Object();
            t1 = new Thread(new ThreadStart(this.replayAll));
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
        public void replayAll()
        {

            while (moveCounter < numOfMoves)
            {
                lock (l)
                {
                    if (!run)
                        Monitor.Wait(l);
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    moveCounter++;
                    RefreshLabel();
                    gameFrame.PushMove();
                    
                });
                Thread.Sleep(2000);

            }     
            moveCounter = 0;
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                RefreshLabel();
                startBtn.Content = "Start";
                run = false;
            });
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if(run)
            {
                startBtn.Content = "Start"; //Pressed Pause
                run = false;

                return;
            }
            startBtn.Content = "Pause"; //Pressed Start
            run = true;
            if (!t1.IsAlive)
            {
                t1 = new Thread(new ThreadStart(this.replayAll));
                gameFrame.reset();
                t1.Start();
            }
            else
                lock (l)
                {
                    Monitor.Pulse(l);
                }
        }

    }
}
