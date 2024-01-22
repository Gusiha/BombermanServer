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

            GameState[0, 3] = new BreakableBlock(0, 3);
            GameState[0, 4] = new BreakableBlock(0, 4);
            GameState[0, 5] = new BreakableBlock(0, 5);
            GameState[0, 6] = new BreakableBlock(0, 6);
            GameState[0, 7] = new BreakableBlock(0, 7);
            GameState[0, 9] = new BreakableBlock(0, 9);
            GameState[0, 10] = new BreakableBlock(0, 10);

            GameState[1, 4] = new BreakableBlock(1, 4);
            GameState[1, 6] = new BreakableBlock(1, 6);
            GameState[1, 8] = new BreakableBlock(1, 8);
            GameState[1, 10] = new BreakableBlock(1, 10);

            GameState[2, 0] = new BreakableBlock(2, 0);
            GameState[2, 2] = new BreakableBlock(2, 2);
            GameState[2, 6] = new BreakableBlock(2, 6);
            GameState[2, 7] = new BreakableBlock(2, 7);
            GameState[2, 8] = new BreakableBlock(2, 8);
            GameState[2, 9] = new BreakableBlock(2, 9);

            GameState[3, 0] = new BreakableBlock(3, 0);
            GameState[3, 2] = new BreakableBlock(3, 2);
            GameState[3, 4] = new BreakableBlock(3, 4);
            GameState[3, 10] = new BreakableBlock(3, 10);

            GameState[4, 0] = new BreakableBlock(4, 0);
            GameState[4, 1] = new BreakableBlock(4, 1);
            GameState[4, 2] = new BreakableBlock(4, 2);
            GameState[4, 3] = new BreakableBlock(4, 3);
            GameState[4, 5] = new BreakableBlock(4, 5);
            GameState[4, 6] = new BreakableBlock(4, 6);
            GameState[4, 8] = new BreakableBlock(4, 8);
            GameState[4, 9] = new BreakableBlock(4, 9);

            GameState[5, 0] = new BreakableBlock(5, 0);
            GameState[5, 2] = new BreakableBlock(5, 2);
            GameState[5, 4] = new BreakableBlock(5, 4);
            GameState[5, 6] = new BreakableBlock(5, 6);
            GameState[5, 8] = new BreakableBlock(5, 8);

            GameState[6, 0] = new BreakableBlock(6, 0);
            GameState[6, 1] = new BreakableBlock(6, 1);
            GameState[6, 2] = new BreakableBlock(6, 2);
            GameState[6, 4] = new BreakableBlock(6, 4);
            GameState[6, 5] = new BreakableBlock(6, 5);
            GameState[6, 6] = new BreakableBlock(6, 6);
            GameState[6, 7] = new BreakableBlock(6, 7);
            GameState[6, 8] = new BreakableBlock(6, 8);
            GameState[6, 10] = new BreakableBlock(6, 10);

            GameState[7, 0] = new BreakableBlock(7, 0);
            GameState[7, 2] = new BreakableBlock(7, 2);
            GameState[7, 4] = new BreakableBlock(7, 4);
            GameState[7, 6] = new BreakableBlock(7, 6);
            GameState[7, 8] = new BreakableBlock(7, 8);


            GameState[8, 2] = new BreakableBlock(8, 2);
            GameState[8, 3] = new BreakableBlock(8, 3);
            GameState[8, 4] = new BreakableBlock(8, 4);
            GameState[8, 5] = new BreakableBlock(8, 5);
            GameState[8, 6] = new BreakableBlock(8, 6);
            GameState[8, 7] = new BreakableBlock(8, 7);
            GameState[8, 8] = new BreakableBlock(8, 8);
            GameState[8, 10] = new BreakableBlock(8, 10);

            GameState[9, 0] = new BreakableBlock(9, 0);
            GameState[9, 2] = new BreakableBlock(9, 2);
            GameState[9, 4] = new BreakableBlock(9, 4);
            GameState[9, 6] = new BreakableBlock(9, 6);
            GameState[9, 10] = new BreakableBlock(9, 10);

            GameState[10, 0] = new BreakableBlock(10, 0);
            GameState[10, 2] = new BreakableBlock(10, 2);
            GameState[10, 3] = new BreakableBlock(10, 3);
            GameState[10, 4] = new BreakableBlock(10, 4);
            GameState[10, 5] = new BreakableBlock(10, 5);
            GameState[10, 6] = new BreakableBlock(10, 6);
            GameState[10, 8] = new BreakableBlock(10, 8);
            GameState[10, 10] = new BreakableBlock(10, 10);

            GameState[11, 0] = new BreakableBlock(11, 0);
            GameState[11, 2] = new BreakableBlock(11, 2);
            GameState[11, 4] = new BreakableBlock(11, 4);
            GameState[11, 8] = new BreakableBlock(11, 8);

            GameState[12, 0] = new BreakableBlock(12, 0);
            GameState[12, 1] = new BreakableBlock(12, 1);
            GameState[12, 2] = new BreakableBlock(12, 2);
            GameState[12, 3] = new BreakableBlock(12, 3);
            GameState[12, 4] = new BreakableBlock(12, 4);
            GameState[12, 5] = new BreakableBlock(12, 5);
            GameState[12, 6] = new BreakableBlock(12, 6);
            GameState[12, 7] = new BreakableBlock(12, 7);

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


        public bool PlaceBomb(Player player)
        {
            if (player.BombAmount == 0 || player == null || GameState[player.X, player.Y] is not Emptiness)
            {
                return false;
            }

            foreach (var item in player.Bombs)
            {
                if (item.IsPlaced)
                {
                    continue;
                }

                GameState[player.X, player.Y] = item;
                item.X = player.X;
                item.Y = player.Y;
                item.IsPlaced = true;
                item.BombTimer = DateTime.Now;
                player.BombAmount--;
                return true;
            }

            return false;
        }


        public void ActivateBombs()
        {

            if (Player1 != null)
            {
                foreach (var item in Player1.Bombs)
                {
                    if (item.IsTimerElapsed() && item.IsPlaced)
                    {
                        item.IsPlaced = false;
                        GameState[item.X, item.Y] = new Emptiness(item.X, item.Y);

                        for (int i = 1; i <= Player1.BombRange; i++)
                        {
                            ExplodeCell(item.X, item.Y + i);
                            ExplodeCell(item.X + i, item.Y);
                            ExplodeCell(item.X, item.Y - i);
                            ExplodeCell(item.X - i, item.Y);
                        }

                        Player1.BombAmount++;
                    }
                } 
            }

            if (Player2 != null)
            {
                foreach (var item in Player2.Bombs)
                {
                    if (item.IsTimerElapsed() && item.IsPlaced)
                    {
                        item.IsPlaced = false;
                        GameState[item.X, item.Y] = new Emptiness(item.X, item.Y);

                        for (int i = 1; i <= Player2.BombRange; i++)
                        {
                            ExplodeCell(item.X, item.Y + i);
                            ExplodeCell(item.X + i, item.Y);
                            ExplodeCell(item.X, item.Y - i);
                            ExplodeCell(item.X - i, item.Y);
                        }

                        Player2.BombAmount++;
                    }
                } 
            }
        }

        private void ExplodeCell(int x, int y)
        {
            IDestroyable? destroyableEntity = null;

            if (x < 0 || x >= GameState.GetLength(0) || y < 0 || y >= GameState.GetLength(1))
            {
                return;
            }

            if (Player1?.X == x && Player1?.Y == y)
            {
                Player1.Destroy();
            }

            if (Player2?.X == x && Player2?.Y == y)
            {
                Player2.Destroy();
            }

            if (GameState[x, y] is IDestroyable)
            {
                destroyableEntity = GameState[x, y] as IDestroyable;
            }

            if (destroyableEntity != null)
            {
                destroyableEntity.Destroy();
                GameState[x, y] = new Emptiness(x, y);
            }

        }


        public bool Connect(Player player)
        {
            //to be able to launch the game with one player
            IsGameStarted = true;
            switch (PlayerAmount)
            {
                case 0:
                    Player1 = player;
                    Player1.X = 0;
                    Player1.Y = 0;
                    PlayerAmount++;
                    return true;

                case 1:
                    Player2 = player;
                    Player2.X = 12;
                    Player2.Y = 10;
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
