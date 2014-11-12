using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Game
    {
        private Chessboard chessBoard;
        public Game()
        {
            chessBoard = new Chessboard(8, 8);
            chessBoard.testRun();

        }

        

        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }
}
