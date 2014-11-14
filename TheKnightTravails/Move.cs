using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Move
    {
        public int Xdirection {get; set;}
        public int Ydirection {get; set;}

        public Move(int x, int y)
        {
            Xdirection = x;
            Ydirection = y;
        }
    }
}
