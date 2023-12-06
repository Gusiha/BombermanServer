using Bomberman.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Abstractions
{
    internal interface IMovable
    {
        void Move(Directions direction, int stepX, int stepY);
    }
}
