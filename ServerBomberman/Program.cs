using ServerBomberman;
using System.Net;



GamingServer server = new(60, IPAddress.Parse("192.168.0.102"), 65535);
////server.StartMessageLoop();
Console.WriteLine("Server is listening...");
server.GameLoopAsync();

