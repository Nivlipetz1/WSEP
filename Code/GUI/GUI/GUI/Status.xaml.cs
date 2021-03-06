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
    /// Interaction logic for Status.xaml
    /// </summary>
    public partial class Status : Page
    {
        GUIManager manager;
        private List<string> gameIDList;
        public Status(GUIManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            gameIDList = new List<string>();
            RefreshGamesList();
        }

        public void AddGameToList(int gameID)
        {
            gameIDList.Add(string.Format("Game {0}",gameID));
            //GameList.ItemsSource = gameIDList;
        }

        public void RemoveGameFromList(int gameID)
        {
            gameIDList.Remove(string.Format("Game {0}", gameID));
            //GameList.ItemsSource = gameIDList;
        }

        public void RefreshStatus()
        {
            Models.ClientUserProfile prof = manager.GetProfile();
            this.credit.Content = "Credit: $" + prof.credit;
            this.username.Content = "Hello " + prof.username;
        }



        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!GameList.Text.Equals(""))
               manager.NavigateToGameFrame(Int32.Parse(GameList.Text.Substring(4)));
        }

        private void GameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshGamesList();
        }

        private void RefreshGamesList(){
            
            GameList.Items.Clear();
            foreach (int gameID in manager.GetGameIDList())
                    GameList.Items.Add("Game "+gameID);
        }

        private void GameList_DropDownOpened(object sender, EventArgs e)
        {
            RefreshGamesList();
        }
    }
}
