using Bomberman.Abstractions;
using Bomberman.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Classes
{
    public class Bomb : Entity, IMovable
    {
        protected Bomb(int startX, int startY) : base(startX, startY)
        {

        }

        public int Range { get; set; }

        public void Explode()
        {

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
