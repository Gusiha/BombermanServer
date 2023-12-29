using Bomberman.Abstractions;
using Bomberman.Classes;

namespace ServerBomberman
{
    public class Session
    {
        public Session()
        {
            IsGameEnded = false;
        }

        public int Milliseconds { get; set; }
        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }
        public Guid Id { get; set; }
        public int PlayerAmount { get; private set; }

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

            if (GameState[player.Y + deltaY, player.X + deltaX] is not Emptiness)
                return false;

            if (DateTime.Now - player.MoveTimer > TimeSpan.FromMilliseconds(250))
            {
                player.X += deltaX;
                player.Y += deltaY;
                player.MoveTimer = DateTime.Now;
                return true;
            }


            return false;
        }

        public bool Connect(Player player)
        {
            switch (PlayerAmount)
            {
                case 0:
                    Player1 = player;
                    //TODO Координаты игрока
                    return true;

                case 1:
                    Player2 = player;
                    //TODO Координаты игрока
                    return true;

                default:
                    return false;
            }
        }

        public Player? FindPlayerById(Guid id)
        {
            if (id != Player1.ID && id != Player2.ID)
                return null;
            else
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
