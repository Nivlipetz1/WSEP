using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using GUI.Models;
using System.Diagnostics;


namespace PerformanceTest
{
    class Program
    {
        static string username;
        const string password = "123456";
        static void Main(string[] args)
        {
            username = RandomString(4);
            testAuthHub(100);
            GameCenterHubTest(100);
            Console.Read();
        }
        public static void testAuthHub(int N)
        {
            SemaphoreSlim sem = new SemaphoreSlim(0, 1);
            long[] times = new long[N];
            long[] regs = new long[N];
            long[] logins = new long[N];
            long[] logouts = new long[N];
            Console.WriteLine("AuthHubTest with " + N + " users");
            int SuccesfullRegisteration = 0;
            int SuccesfullLogins = 0;
            int SuccesfullLogouts = 0;
            int done = 0;
            Task[] resps = new Task[N];
            for (int i = 0; i < N; i++)
            {
               // var hubConnection = new HubConnection("http://localhost:51509");
                var hubConnection = new HubConnection("http://52.29.58.18:80/");
                Stopwatch s = new Stopwatch();
                s.Start();
                int k = i;
                IHubProxy proxy = hubConnection.CreateHubProxy("AuthHub");
                hubConnection.Start().ContinueWith(response =>
                {
                    
                    s.Stop();
                    Console.WriteLine(k+" Connected");
                    times[k] = s.ElapsedMilliseconds;
                    Thread.Sleep(500);
                    s.Restart();
                    Task res=proxy.Invoke<bool>("Register", username + k, password).ContinueWith(res1 =>
                     {
                         s.Stop();
                         regs[k] = s.ElapsedMilliseconds;
                         if (res1.Result)
                             SuccesfullRegisteration++;
                     });
                    res.Wait();
                    s.Restart();
                    res=proxy.Invoke<bool>("Login", username + k, password).ContinueWith(res1 =>
                    {
                        s.Stop();
                        logins[k] = s.ElapsedMilliseconds;
                        if (res1.Result)
                        {
                            SuccesfullLogins++;
                        }
                        else
                        {
                            Console.WriteLine(username + k + " failed to login") ;
                        }
                    });
                    res.Wait();
                    s.Restart();
                    resps[k]=proxy.Invoke<bool>("logout").ContinueWith(res1 =>
                    {
                        s.Stop();
                        logouts[k] = s.ElapsedMilliseconds;
                        if (res1.Result)
                            SuccesfullLogouts++;
                        done++;
                        if (done == N)
                            sem.Release();
                    });
                });

                Thread.Sleep(300);
            }

                sem.Wait();
                Console.WriteLine("Average connection Time: " + averageTime(times)+" ms");

                Console.WriteLine("Succesfull Registerations " + SuccesfullRegisteration);
                double registersTime = averageTime(regs);
                Console.WriteLine("Average Response Time :" + registersTime + "ms");
                Console.WriteLine("Succesfull Logins " + SuccesfullLogins);
                double loginsTime = averageTime(logins);
                Console.WriteLine("Average Response Time :" + loginsTime + "ms");
                Console.WriteLine("Succesfull Logouts " + SuccesfullLogouts);
                double logoutsTime = averageTime(logouts);
                Console.WriteLine("Average Response Time :" + logoutsTime + "ms");
                Console.WriteLine(" ==Summary==");
                Console.WriteLine("Average " + (registersTime + loginsTime+logoutsTime) / 3 + " ms");
                
                // Console.Read();
            
        }

        private static void GameCenterHubTest(int N)
        {
            SemaphoreSlim sem = new SemaphoreSlim(0, 1);
            long[] times = new long[N];
            long[] creates = new long[(N/3)+1];
            long[] joins = new long[N];
            long[] remove = new long[(N/5)+1];
            Console.WriteLine("GameCenterHubTest with " + N + " users");
            int[] gameId = new int[(N /3)+1];
            int Succesfullcreates = 0, Succesfulljoins = 0;
            int done = N;
            for (int i=0;i< N;i++)
            {
              //  var hubConnection = new HubConnection("http://localhost:51509");
                var hubConnection = new HubConnection("http://52.29.58.18:80/");
                Stopwatch s = new Stopwatch();
                s.Start();
                int k = i;
                IHubProxy proxy = hubConnection.CreateHubProxy("AuthHub");
                IHubProxy proxy2 = hubConnection.CreateHubProxy("GameCenterHub");
                IHubProxy proxy3 = hubConnection.CreateHubProxy("GameHub");
                hubConnection.Start().ContinueWith(response =>
                {
                    s.Stop();
                    times[k] = s.ElapsedMilliseconds;
                    Console.WriteLine("User " + k + " connected to server in " + times[k] + "ms");
                    proxy.Invoke("Login", username + k, password).ContinueWith(res =>
                    {
                    
                    if (k%3 == 0)
                    {
                            s.Restart();
                            proxy2.Invoke<ClientGame>("createGame", new GamePreferences(8, 2, 5, 10, 1, 2, 3, true)).ContinueWith(game1 =>
                            {
                                s.Stop();
                                creates[k/3] = s.ElapsedMilliseconds;
                                gameId[k / 3] = game1.Result.id;
                                Console.WriteLine("Game " + gameId[k / 3] + " created");
                                Succesfullcreates++;
                            });
                    }
                    else
                        Thread.Sleep(500);
                        s.Restart();
                        proxy2.Invoke<ClientGame>("joinGame", gameId[k/3], 50).ContinueWith(resp =>
                        {
                            s.Stop();
                            joins[k/3] = s.ElapsedMilliseconds;
                            if (resp.IsFaulted)
                            {
                                Console.WriteLine(resp.Exception.GetBaseException());
                              //  throw (resp.Exception.GetBaseException());   
                            }

                            else if (resp.Result != null)
                            {
                                Console.WriteLine("User " + k + " has join to Game" + gameId[k/3]);
                                Succesfulljoins++;
                            }
                            else
                            {
                                Console.WriteLine("User " + k + " cannot join to Game " + gameId[k / 3]);
                            }
                            done--;
                            if (done == 0)
                                sem.Release();
                               
                        });
                  });
                });
            }
            sem.Wait();
            Console.WriteLine("Average connection Time: " + averageTime(times) + " ms");

            double createTime = averageTime(creates);
            Console.WriteLine("Succesfull CreateGame Operations " + Succesfullcreates);
            Console.WriteLine("Average Response Time :" + createTime + "ms");
            double joinTime = averageTime(joins);
            Console.WriteLine("Succesfull joinGame Operations " + Succesfulljoins);
            Console.WriteLine("Average Response Time :" + joinTime + "ms");
           
            Console.WriteLine(" ==Summary==");
            Console.WriteLine("Average " + (createTime + joinTime) /2 + " ms");
        }

        private static void GameHubTest(int N)  //TODO
        {
            SemaphoreSlim sem = new SemaphoreSlim(0, 1);
            SemaphoreSlim gameOn = new SemaphoreSlim(0, 5);
            long[] times = new long[N];
            long[] creates = new long[(N / 3) + 1];
            long[] joins = new long[N];
            long[] remove = new long[(N / 5) + 1];
            Console.WriteLine("GameCenterHubTest with " + N + " users");
            int[] gameId = new int[(N / 3) + 1];
            int Succesfullcreates = 0, Succesfulljoins = 0;
            int done = N;
            for (int i = 0; i < N; i++)
            {
                //  var hubConnection = new HubConnection("http://localhost:51509");
                var hubConnection = new HubConnection("http://52.29.58.18:80/");
                Stopwatch s = new Stopwatch();
                s.Start();
                int k = i;
                IHubProxy proxy = hubConnection.CreateHubProxy("AuthHub");
                IHubProxy proxy2 = hubConnection.CreateHubProxy("GameCenterHub");
                IHubProxy proxy3 = hubConnection.CreateHubProxy("GameHub");
                hubConnection.Start().ContinueWith(response =>
                {
                    s.Stop();
                    times[k] = s.ElapsedMilliseconds;
                    Console.WriteLine("User " + k + " connected to server in " + times[k] + "ms");
                    proxy.Invoke("Login", username + k, password).ContinueWith(res =>
                    {

                        if (k % 3 == 0)
                        {
                            s.Restart();
                            proxy2.Invoke<ClientGame>("createGame", new GamePreferences(8, 2, 5, 10, 1, 2, 3, true)).ContinueWith(game1 =>
                            {
                                s.Stop();
                                creates[k] = s.ElapsedMilliseconds;
                                gameId[k / 3] = game1.Result.id;
                                if (k / 3 == 2)
                                    gameOn.Release(5);
                                Console.WriteLine("Game " + gameId[k / 3] + " created");
                                Succesfullcreates++;
                            });
                        }
                        else
                            Thread.Sleep(500);
                        s.Restart();
                        proxy2.Invoke<ClientGame>("joinGame", gameId[k / 3], 50).ContinueWith(resp =>
                        {
                            s.Stop();
                            joins[k] = s.ElapsedMilliseconds;
                            if (resp.IsFaulted)
                            {
                                Console.WriteLine(resp.Exception.GetBaseException());
                                //  throw (resp.Exception.GetBaseException());   
                            }

                            else if (resp.Result != null)
                            {
                                Console.WriteLine("User " + k + " has join to Game" + gameId[k / 3]);
                                Succesfulljoins++;
                            }
                            else
                            {
                                Console.WriteLine("User " + k + " cannot join to Game " + gameId[k / 3]);
                            }
                            /* ToCopy+++++++    done--;
                                 if (done == 0)
                                     sem.Release();
                                     */
                        });
                        if (k % 5 == 0)
                        {
                            s.Restart();
                            proxy3.Invoke<bool>("removePlayer", gameId[k % 3]).ContinueWith(flag =>
                            {
                                s.Stop();
                                remove[k] = s.ElapsedMilliseconds;
                                Console.WriteLine("User " + k + " has left the Game " + gameId[k / 3]);
                                done--;
                                if (done == 0)
                                    sem.Release();
                            });
                        }
                        else
                        {
                            done--;
                            if (done == 0)
                                sem.Release();

                        }

                    });
                });
            }
            for (int i = 0; i < 5; i++)
            {
                var hubConnection = new HubConnection("http://52.29.58.18:80/");
                Stopwatch s = new Stopwatch();
                s.Start();
                int k = i;
                IHubProxy proxy = hubConnection.CreateHubProxy("AuthHub");
                IHubProxy proxy2 = hubConnection.CreateHubProxy("GameCenterHub");
                IHubProxy proxy3 = hubConnection.CreateHubProxy("GameHub");
                hubConnection.Start().ContinueWith(response =>
                {
                    proxy.Invoke("register", username + "bet" + k, password).Wait();
                    proxy.Invoke("login", username + "bet" + k, password).Wait();
                    gameOn.Wait();
                    proxy2.Invoke("joinGame", gameId[2], 50).Wait();
                    proxy3.Invoke("postMessage", "Hi everybody", gameId[2]);
                    proxy3.Invoke("bet", gameId[2], 12);
                });

            }
            sem.Wait();
            Console.WriteLine("Average connection Time: " + averageTime(times) + " ms");

            double createTime = averageTime(creates);
            Console.WriteLine("Succesfull CreateGame Operations " + Succesfullcreates);
            Console.WriteLine("Average Response Time :" + createTime + "ms");
            double joinTime = averageTime(joins);
            Console.WriteLine("Succesfull joinGame Operations " + Succesfulljoins);
            Console.WriteLine("Average Response Time :" + joinTime + "ms");

            Console.WriteLine(" ==Summary==");
            Console.WriteLine("Average " + (createTime + joinTime) / 2 + " ms");
        }

        
        private static double averageTime(long[] time)
        {
            double sum = 0;
            for(int i=0;i<time.Length;i++)
            {
                sum += time[i];
            }
            return sum / time.Length;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
      
    }
}
