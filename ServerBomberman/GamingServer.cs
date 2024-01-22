using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerBomberman
{
    public class GamingServer
    {
        private Socket socket { get; set; }
        private IPEndPoint _endPoint;
        private byte[] _buffer;
        private ArraySegment<byte> bufferSegment { get; set; }


        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }


        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }


        public List<Session> Sessions { get; set; }


        public GamingServer(int tickRate, IPAddress iPAddress, int port)
        {
            Sessions = new List<Session>();

            for (int i = 0; i < 5; i++)
            {
                Sessions.Add(new Session());
            }

            TickRate = tickRate;
            TicksPerSecond = 1000 / tickRate;

            Initialize(iPAddress, port);
        }


        public void Initialize(IPAddress iPAddress, int port)
        {
            _buffer = new byte[2048];
            bufferSegment = new(_buffer);
            IPAddress = iPAddress;
            Port = port;
            _endPoint = new IPEndPoint(IPAddress.Any, Port);

            IPEndPoint serverEndpoint = new(IPAddress, Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(serverEndpoint);
        }

        //public void ServerTickController()
        //{
        //    if (Milliseconds >= TicksPerSecond)
        //    {
        //        return;
        //    }

        //    Thread.Sleep(1);
        //    Milliseconds++;
        //}

        public void ServerTickController(params Action[] actions)
        {
            Stopwatch timer = Stopwatch.StartNew();

            //while (Sessions.Count > 0)

            timer.Restart();
            foreach (var action in actions)
            {
                action.Invoke();
            }

            timer.Stop();

            if (timer.ElapsedMilliseconds <= TicksPerSecond)
            {
                Thread.Sleep((int)(TicksPerSecond - timer.ElapsedMilliseconds));
            }

        }

        public void GameLoopAsync()
        {

            //while (Sessions.Count > 0)
            while (true)
            {
                //Inside [ServerTickController] you can put methods, which have to be invoked with certain tickrate
                ServerTickController(StartMessageLoop, Update);

            }
        }

        private Session? FindSessionByPlayerID(Guid id)
        {
            foreach (var item in Sessions)
            {
                if (item.FindPlayerById(id) != null)
                {
                    return item;
                }


            }
            return null;

        }

        public void Update()
        {

            _ = Task.Run(async () =>
            {
                foreach (var item in Sessions)
                {
                    if (item.IsGameStarted)
                    {
                        item.ActivateBombs();
                        string gameState = item.ToString();

                        if (item.Player2 == null)
                        {
                            await SendTo(item.Player1.EndPoint, Encoding.UTF8.GetBytes($"202 {gameState} {item.Player1.X} {item.Player1.Y} {10} {5}"));
                        }

                        else
                        {
                            await SendTo(item.Player1.EndPoint, Encoding.UTF8.GetBytes($"202 {gameState} {item.Player1.X} {item.Player1.Y} {item.Player2.X} {item.Player2.Y}"));
                            await SendTo(item.Player2.EndPoint, Encoding.UTF8.GetBytes($"202 {gameState} {item.Player2.X} {item.Player2.Y} {item.Player1.X} {item.Player1.Y}"));
                        }

                    }
                }
            });
        }
        public void StartMessageLoop()
        {

            _ = Task.Run(async () =>
            {
                GameInterpreter gameInterpreter = new GameInterpreter();
                SocketReceiveFromResult result;
                //while (true)
                //{
                result = await socket.ReceiveFromAsync(bufferSegment, SocketFlags.None, _endPoint);
                var message = Encoding.UTF8.GetString(_buffer, 0, result.ReceivedBytes);
                Console.WriteLine($"<- {message} [{result.RemoteEndPoint}]");



                int[] response = gameInterpreter.Parse(message);
                switch (response[2])
                {
                    //Move
                    case 0:
                        {
                            Session? foundSession = FindSessionByPlayerID(gameInterpreter.PlayerID);

                            if (foundSession is null)
                            {
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"400 Failed to move. Session not found"));
                                break;
                            }


                            int messageCode = gameInterpreter.DoAction(response, foundSession);

                            switch (messageCode)
                            {
                                case 200:
                                    {
                                        await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} {foundSession.FindPlayerById(gameInterpreter.PlayerID)?.X} {foundSession.FindPlayerById(gameInterpreter.PlayerID)?.Y} "));
                                        break;
                                    }

                                case 201:
                                    {
                                        await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} You can't move in this way according to the rules"));
                                        break;
                                    }

                                default:
                                    break;
                            }

                            break;
                        }

                    //Connect
                    case 1:
                        {
                            bool success = false;

                            for (int i = 0; i < Sessions.Count; i++)
                            {

                                if (gameInterpreter.ConnectPlayer(Sessions[i], result.RemoteEndPoint))
                                {
                                    success = true;
                                    break;
                                }

                            }

                            if (!success)
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"400 Failed to join session. Sessions are fulled"));

                            else
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"203 {gameInterpreter.PlayerID}"));

                            break;
                        }

                    //Disconnect
                    case 2:
                        {
                            Session? session = FindSessionByPlayerID(gameInterpreter.PlayerID);

                            if (session is null)
                            {
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"400 Failed to disconnect from session"));
                                break;
                            }

                            else
                            {
                                int messageCode = gameInterpreter.DoAction(response, session);

                                switch (messageCode)
                                {
                                    case 200:
                                        {
                                            await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} You have disconnected successfully"));
                                            break;
                                        }




                                    default:
                                        break;
                                }
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($" Failed to disconnect from session"));

                                break;
                            }
                        }
                    //PlaceBomb
                    case 3:
                        {

                            Session? session = FindSessionByPlayerID(gameInterpreter.PlayerID);

                            if (session is null)
                            {
                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"400 Failed to place the bomb (session not found)"));
                                break;
                            }

                            else
                            {
                                int messageCode = gameInterpreter.DoAction(response, session);

                                switch (messageCode)
                                {
                                    case 200:
                                        {
                                            if (gameInterpreter.PlayerID == session.Player1.ID)
                                            {
                                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} Bomb deployed by {gameInterpreter.PlayerID} at [{session.Player1.X}][{session.Player1.Y}]"));
                                            }

                                            else
                                            {
                                                await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} Bomb deployed by {gameInterpreter.PlayerID} at [{session.Player2.X}][{session.Player2.Y}]"));
                                            }

                                            break;
                                        }

                                    case 201:
                                        {
                                            await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"{messageCode} {gameInterpreter.PlayerID} couldn't place the bomb"));
                                            break;
                                        }

                                    default:
                                        break;
                                }

                                break;
                            }
                        }


                    case 400:
                        {
                            //TODO Отравить сообщение об ошибке
                            await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes($"400 {response[1]}"));
                            break;
                        }

                    case 402:
                        {

                            break;
                        }

                    default:
                        break;
                }

                //Парсинг сообщения
                //Ответ на сообщение (КОД сообщения + возможно тело и тд)
                //await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes("Hello from Server"));
                //}

            });

        }



        public async Task SendTo(EndPoint recipient, byte[] data)
        {
            var messageToSend = new ArraySegment<byte>(data);
            await socket.SendToAsync(messageToSend, recipient);
            Console.WriteLine($"-> {Encoding.UTF8.GetString(messageToSend)} [{recipient}]");
        }



    }
}

