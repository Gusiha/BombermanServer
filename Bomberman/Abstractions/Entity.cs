using Bomberman.Enums;

namespace Bomberman.Abstractions
{
    public abstract class Entity
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public bool isTakeable { get; protected set; }

        public States State { get; protected set; }


        public Entity(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public void Hide()
        {
            State = States.Hidden;
        }

        public abstract void Spawn();

    }
}
