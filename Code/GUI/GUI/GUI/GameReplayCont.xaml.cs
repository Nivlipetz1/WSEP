﻿using System;
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
        public GameReplayCont(GameFrame gf)
        {
            InitializeComponent();
            this.gameFrame = gf;
        }

        private void NextMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.PushMove();
        }

        internal void DisablePlayButton()
        {
            NextMoveBtn.IsEnabled = false;
        }
    }
}
