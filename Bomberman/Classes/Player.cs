using Bomberman.Abstractions;
using Bomberman.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Classes
{
    public class Player : Entity, IMovable, IDestroyable
    {
        public Guid ID { get; private set; }

        public Player(int startX, int startY) : base(startX, startY)
        {
            ID = Guid.NewGuid();

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
