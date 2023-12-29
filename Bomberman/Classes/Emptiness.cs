using Bomberman.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Classes
{
    public class Emptiness : Entity
    {
        public Emptiness(int startX, int startY) : base(startX, startY, 0)
        {
            State = Enums.States.Empty;
        }

        public override void Spawn()
        {
            
        }
    }
}
