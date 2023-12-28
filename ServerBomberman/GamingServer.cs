using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerBomberman
{
    public class GamingServer
    {
        private Socket _socket { get; set; }
        private IPEndPoint _endPoint;
        private byte[] _buffer;
        private ArraySegment<byte> _bufferSegment { get; set; }



        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }


        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }


        public List<Session> Sessions { get; set; }


        public GamingServer(int tickRate, IPAddress iPAddress, int port)
        {

            TickRate = tickRate;
            TicksPerSecond = 1000 / tickRate;

            Initialize(iPAddress, port);
        }


        public void Initialize(IPAddress iPAddress, int port)
        {
            _buffer = new byte[2048];
            _bufferSegment = new(_buffer);
            IPAddress = iPAddress;
            Port = port;
            _endPoint = new IPEndPoint(IPAddress.Any, Port);

            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress, Port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(serverEndpoint);
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

            Stopwatch timer = Stopwatch.StartNew();

            //while (Sessions.Count > 0)
            while (true)
            {
                //Inside [ServerTickController] you can place methods, which have to be done with certain tickrate
                ServerTickController(StartMessageLoop);
            }

        }

        public void StartMessageLoop()
        {
            _ = Task.Run(async () =>
            {
                SocketReceiveFromResult result;
                //while (true)
                //{
                result = await _socket.ReceiveFromAsync(_bufferSegment, SocketFlags.None, _endPoint);
                var message = Encoding.UTF8.GetString(_buffer, 0, result.ReceivedBytes);
                Console.WriteLine($"Recieved : {message} from {result.RemoteEndPoint}");
                
                //Парсинг сообщения
                //Ответ на сообщение (КОД сообщения + возможно тело и тд)
                //await SendTo(result.RemoteEndPoint, Encoding.UTF8.GetBytes("Hello from Server"));
                //}

            });
        }

        public async Task SendTo(EndPoint recipient, byte[] data)
        {
            var messageToSend = new ArraySegment<byte>(data);
            await _socket.SendToAsync(messageToSend, recipient);
        }



    }
}

