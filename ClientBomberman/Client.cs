using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientBomberman
{
    internal class Client
    {

        private Socket _socket { get; set; }
        private IPEndPoint _endPoint;
        private byte[] _buffer;
        private ArraySegment<byte> _bufferSegment { get; set; }

        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }



        public Client(IPAddress iPAddress, int port)
        {
            Initialize(iPAddress, port);
        }



        public void Initialize(IPAddress iPAddress, int port)
        {
            _buffer = new byte[2048];
            _bufferSegment = new(_buffer);
            IPAddress = Dns.GetHostAddresses(Dns.GetHostName()).First();
            Port = port;
            _endPoint = new IPEndPoint(iPAddress, Port);

            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress, Port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(clientEndPoint);
            _socket.Listen();
        }


        public void StartMessageLoop()
        {
            _ = Task.Run(async () =>
            {
                SocketReceiveFromResult result;
                while (true)
                {
                    result = await _socket.ReceiveFromAsync(_bufferSegment, SocketFlags.None, _endPoint);
                    var message = Encoding.UTF8.GetString(_buffer, 0, result.ReceivedBytes);
                    Console.WriteLine($"Recieved : {message} from {result.RemoteEndPoint}");
                }

            });
        }

        public async Task SendTo(byte[] data)
        {
            var messageToSend = new ArraySegment<byte>(data);
            await _socket.SendToAsync(messageToSend, _endPoint);
        }



    }
}

