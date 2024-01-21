using Bomberman.Abstractions;
using Bomberman.Enums;

namespace Bomberman.Classes
{
    public class Bomb : Entity, IMovable
    {
        public DateTime BombTimer { get; set; }

        public bool IsPlaced { get; set; } = false;
        public Bomb(int startX, int startY) : base(startX, startY, 3)
        {

        }

        public Bomb() : base(0,0,3)
        {

        }


        public bool IsTimerElapsed()
        {
            if (DateTime.Now - BombTimer >= TimeSpan.FromMilliseconds(3000))
            {
                return true;

            }
            return false;
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
