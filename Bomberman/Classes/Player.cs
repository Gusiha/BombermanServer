using Bomberman.Abstractions;
using Bomberman.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Classes
{
    internal class Player : Entity, IMovable, IDestroyable
    {
        protected Player(int startX, int startY) : base(startX, startY)
        {

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
