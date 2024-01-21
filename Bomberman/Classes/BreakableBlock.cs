using Bomberman.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Classes
{
    public class BreakableBlock : Entity, IDestroyable
    {
        public IBuff Buff { get; private set; }
        public BreakableBlock(int startX, int startY) : base(startX, startY, 5)
        {
            //TODO Add buff with certain drop chance
        }

        //TODO Add buff drop in Destroy() using State enum
        public void Destroy()
        {
            
        }

        public override void Spawn()
        {

        }

        public override void Hide()
        {
            base.Hide();

        }
    }
}
