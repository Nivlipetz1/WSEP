using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using ServiceLayer.Models;
using Gaming;


namespace PerformanceTest
{
    class Program
    {
        static int gameId = 0;
        static void Main(string[] args)
        {
            
                test1(3);
            
        }
        public static void test1(int i)
        {
            var hubConnection = new HubConnection("http://localhost:51509/");
            IHubProxy proxy = hubConnection.CreateHubProxy("AuthHub"); //Create connection to specific hub
            IHubProxy proxy2 = hubConnection.CreateHubProxy("GameCenterHub");
            IHubProxy proxy3 = hubConnection.CreateHubProxy("GameHub");
            proxy.On<string>("onPlayerJoinRoom", (s) =>     // register to hub events
            {
                Console.WriteLine(s);
                Console.Read();
            });

            try
            {
                hubConnection.Start().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                Console.Read();
                return;
            }
            
            Task<bool> reg = proxy.Invoke<bool>("Register", "ohadd"+i, "123456");
            Task<bool> bla = proxy.Invoke<bool>("login", "ohadd"+i, "123456");

            reg.Wait();
            bla.Wait();
            bool res = reg.Result;
            Console.WriteLine(res);
            Console.WriteLine("ohad"+i+"logged in "+bla.Result);
            
            if(i==0)
            {
                GamePreferences perf=new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
                Task<ClientGame> create = proxy2.Invoke<ClientGame>("createGame",perf);
                create.Wait();
                gameId = create.Result.getID();
                Console.WriteLine("crated game"+ gameId);
            }
            Task<ClientGame> join= proxy2.Invoke<ClientGame>("joinGame", gameId, "ohad" + i);
            join.Wait();
            if(join.Result!=null)
                Console.WriteLine("ohad"+i+" is join to game");
            
            
            Task<List<ClientGame>> games = proxy2.Invoke<List<ClientGame>>("getActiveGames", "none", "ronen");
            games.Wait();
            foreach (ClientGame g in games.Result)
                Console.WriteLine(g.getID());
        }
    }
}
