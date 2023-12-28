using Bomberman.Abstractions;
using Bomberman.Classes;

namespace ServerBomberman
{
    public class Session
    {
        public Session()
        {
            IsGameEnded = false;
            Player1 = new Player(0,0);
            Player2 = new Player(0,1);
        }

        public int Milliseconds { get; set; }
        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }
        public int ID { get; set; }


        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        private bool IsGameEnded { get; set; }
        public Entity[,] GameState { get; set; }
        public DateTime StartTime { get; set; }

        public void SessionTickController()
        {
            if (Milliseconds >= TicksPerSecond)
            {
                return;
            }

            Thread.Sleep(1);
            Milliseconds++;
        }

        public async Task AsyncSessionLoop()
        {
            Task time = new(SessionTickController);

            while (!IsGameEnded)
            {
                time.Start();

                //TODO Логика сервера (парс команд + их обработка)

                await time;
                Milliseconds = 0;
            }


        }

    }
}
