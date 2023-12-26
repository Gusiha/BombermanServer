using Bomberman.Abstractions;

namespace ServerBomberman
{
    public class Session
    {
        public Session(int tickRate)
        {
            Milliseconds = 0;
            TickRate = tickRate;
            TicksPerSecond = 1000 / tickRate;
            IsGameEnded = false;
        }

        public int Milliseconds { get; set; }
        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }
        public int ID { get; set; }
        public int Player1ID { get; set; }
        public int Player2ID { get; set; }
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
