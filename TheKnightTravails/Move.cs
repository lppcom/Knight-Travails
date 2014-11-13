using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Move
    {
        public int xDirection {get; set;}
        public int yDirection {get; set;}

        public Move(int x, int y)
        {
            xDirection = x;
            yDirection = y;
        }
    }
}
