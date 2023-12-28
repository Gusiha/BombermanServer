using Bomberman.Abstractions;
using Bomberman.Classes;
using System.Reflection.Metadata.Ecma335;

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

        public bool Move(Player player, int deltaX, int deltaY)
        {

            if (player.X + deltaX >= GameState.GetLength(1) || player.X + deltaX <= 0 
                || player.Y + deltaY >= GameState.GetLength(0) || player.Y + deltaY <= 0)
            {
                return false;
            }

            GameState[player.Y, player.X] = new Emptiness(player.X, player.Y);

            player.X += deltaX;
            player.Y += deltaY;

            GameState[player.Y, player.X] = player;

            return true;

        }

        public Player FindPlayerById(Guid id)
        {
            return id == Player1.ID ? Player1 : Player2;
        }

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
