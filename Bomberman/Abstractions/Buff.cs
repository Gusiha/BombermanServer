using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bomberman.Classes;

namespace Bomberman.Abstractions
{
    public abstract class Buff : Entity, IBuff
    {
        private Bomb _buffedEntity;

        protected Buff(int startX, int startY) : base(startX, startY, 4)
        {
        }

        public abstract void Activate();
        
    }
}
