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
            chessBoard = new Chessboard();
            //chessBoard.testRun();
            System.Console.WriteLine("Enter start coordinates: ");
            int startCol = int.Parse(System.Console.ReadLine());
            int startRow = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Enter end coordinates: ");
            int endCol = int.Parse(System.Console.ReadLine());
            int endRow = int.Parse(System.Console.ReadLine());

            chessBoard.test(startCol, startRow, endCol, endRow);
            System.Console.ReadLine();
        }

        

        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }
}
