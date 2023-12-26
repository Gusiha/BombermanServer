using ClientBomberman;
using System.Data;
using System.Net;
using System.Text;

Client client = new(IPAddress.Parse("192.168.0.102"), 65535);
client.StartMessageLoop();
await client.SendTo(Encoding.UTF8.GetBytes(Console.ReadLine()));
