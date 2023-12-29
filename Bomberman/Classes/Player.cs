using Bomberman.Abstractions;
using Bomberman.Enums;

namespace Bomberman.Classes
{
    public class Player : Entity, IMovable, IDestroyable
    {
        public DateTime BombTimer { get; set; }
        public DateTime MoveTimer { get; set; }


        public Guid ID { get; private set; }


        public Player(int startX, int startY) : base(startX, startY)
        {
            ID = Guid.NewGuid();
            BombTimer = DateTime.Now;
            MoveTimer = DateTime.Now;
        }

        public void Destroy()
        {


            throw new NotImplementedException();
        }


        public void Move(Directions direction, int stepX, int stepY)
        {


            throw new NotImplementedException();
        }

        public override void Spawn()
        {
            throw new NotImplementedException();
        }
    }
}
