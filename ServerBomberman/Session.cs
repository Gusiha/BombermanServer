using Bomberman.Abstractions;
using Bomberman.Classes;

namespace ServerBomberman
{
    public class Session
    {
        public Session()
        {
            IsGameEnded = false;
            GameState = new Entity[13, 11];
            for (int i = 0; i < GameState.GetLength(0); i++)
            {
                for (int j = 0; j < GameState.GetLength(1); j++)
                {
                    GameState[i, j] = new Emptiness(i, j);
                }
            }

            GameState[1, 1] = new SolidBlock(1, 1);
            GameState[1, 3] = new SolidBlock(1, 3);
            GameState[1, 5] = new SolidBlock(1, 5);
            GameState[1, 7] = new SolidBlock(1, 7);
            GameState[1, 9] = new SolidBlock(1, 9);

            GameState[3, 1] = new SolidBlock(3, 1);
            GameState[3, 3] = new SolidBlock(3, 3);
            GameState[3, 5] = new SolidBlock(3, 5);
            GameState[3, 7] = new SolidBlock(3, 7);
            GameState[3, 9] = new SolidBlock(3, 9);

            GameState[5, 1] = new SolidBlock(5, 1);
            GameState[5, 3] = new SolidBlock(5, 3);
            GameState[5, 5] = new SolidBlock(5, 5);
            GameState[5, 7] = new SolidBlock(5, 7);
            GameState[5, 9] = new SolidBlock(5, 9);

            GameState[7, 1] = new SolidBlock(7, 1);
            GameState[7, 3] = new SolidBlock(7, 3);
            GameState[7, 5] = new SolidBlock(7, 5);
            GameState[7, 7] = new SolidBlock(7, 7);
            GameState[7, 9] = new SolidBlock(7, 9);

            GameState[9, 1] = new SolidBlock(9, 1);
            GameState[9, 3] = new SolidBlock(9, 3);
            GameState[9, 5] = new SolidBlock(9, 5);
            GameState[9, 7] = new SolidBlock(9, 7);
            GameState[9, 9] = new SolidBlock(9, 9);

            GameState[11, 1] = new SolidBlock(11, 1);
            GameState[11, 3] = new SolidBlock(11, 3);
            GameState[11, 5] = new SolidBlock(11, 5);
            GameState[11, 7] = new SolidBlock(11, 7);
            GameState[11, 9] = new SolidBlock(11, 9);

        }

        public int Milliseconds { get; set; }
        public int TickRate { get; set; }
        public double TicksPerSecond { get; set; }
        public Guid Id { get; set; }
        public int PlayerAmount { get; private set; }

        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }

        public bool IsGameStarted { get; set; }
        public bool IsGameEnded { get; set; }
        public Entity[,] GameState { get; set; }
        public DateTime StartTime { get; set; }

        public bool Move(Player player, int deltaX, int deltaY)
        {

            if (player.X + deltaX >= GameState.GetLength(0) || player.X + deltaX < 0
                    || player.Y + deltaY >= GameState.GetLength(1) || player.Y + deltaY < 0)
            {
                return false;
            }


            if (GameState[player.X + deltaX, player.Y + deltaY] is not Emptiness)
                return false;


            if (Player2 != null && Player1 != null)
            {
                if (player.ID == Player1.ID)
                {
                    if (GameState[player.X + deltaX, player.Y + deltaY] == GameState[Player2.X, Player2.Y])
                    {
                        return false;
                    }
                }


                if (player.ID == Player2.ID)
                {
                    if (GameState[player.X + deltaX, player.Y + deltaY] == GameState[Player1.X, Player1.Y])
                    {
                        return false;
                    }
                }

            }


            if (DateTime.Now - player.MoveTimer > TimeSpan.FromMilliseconds(150))
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
            //to be able to launch the game with one player
            IsGameStarted = true;
            switch (PlayerAmount)
            {
                case 0:
                    Player1 = player;
                    Player1.X = 3;
                    Player1.Y = 5;
                    PlayerAmount++;
                    return true;

                case 1:
                    Player2 = player;
                    Player2.X = 10;
                    Player2.Y = 5;
                    //IsGameStarted = true;
                    return true;

                default:
                    return false;
            }


        }

        public bool Disconnect(Player player)
        {
            Player1 = null;
            Player2 = null;
            IsGameEnded = true;

            return true;
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

        public override string ToString()
        {
            string gameState = string.Empty;

            foreach (var item in GameState)
            {
                gameState += item.EntityID.ToString();
                gameState += " ";

            }

            return gameState.TrimEnd();
        }
    }
}
