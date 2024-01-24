using Bomberman.Abstractions;
using Bomberman.Enums;
using System.Net;

namespace Bomberman.Classes
{
    public class Player : Entity, IMovable, IDestroyable
    {
        public DateTime MoveTimer { get; set; }
        public DateTime DeathTime { get; private set; }
        public DateTime LastConnectionUpdateTime { get; set; }

        public EndPoint EndPoint { get; private set; }

        public List<Bomb> Bombs { get; set; } = new List<Bomb>();

        public int BombAmount { get; set; } = 2;

        public Guid ID { get; private set; }

        public int BombRange { get; set; } = 1;

        public Player(int startX, int startY, EndPoint endPoint) : base(startX, startY, 2)
        {
            EndPoint = endPoint;
            ID = Guid.NewGuid();
            MoveTimer = DateTime.Now;
            LastConnectionUpdateTime = DateTime.Now;
            for (int i = 0; i < BombAmount; i++)
            {
                Bombs.Add(new Bomb());
            }
        }


        public void Destroy()
        {
            State = States.Destroyed;
            DeathTime = DateTime.Now;
        }


        public void Move(Directions direction, int stepX, int stepY)
        {


            throw new NotImplementedException();
        }

        public override void Spawn()
        {
            throw new NotImplementedException();
        }

        public bool IsPlayerConnected()
        {
            if (DateTime.Now - LastConnectionUpdateTime > TimeSpan.FromMilliseconds(5000))
                return false;
            return true;
        }


    }
}
