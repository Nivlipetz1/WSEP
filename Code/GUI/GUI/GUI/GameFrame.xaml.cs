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
    /// Interaction logic for GameFrame.xaml
    /// </summary>
    public partial class GameFrame : Page
    {
        private Models.ClientGame game;
        public GameFrame(Models.ClientGame game)
        {
            this.game = game;
            InitializeComponent();
            gameFrame.NavigationService.Navigate(new Game(this));
            chatFrame.NavigationService.Navigate(new GameChat(this));
            pmFrame.NavigationService.Navigate(new GamePM(this));
        }

        public Models.ClientGame getGame()
        {
            return game;
        }
    }
}