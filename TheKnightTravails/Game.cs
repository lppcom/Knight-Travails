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

            if (chessBoard.FindARoute(startCol,startRow,endCol,endRow))
            {
                System.Console.WriteLine("success");
                System.Console.WriteLine(chessBoard.GetTurnList());
                System.Console.ReadLine();
            }
            else
            {
                System.Console.WriteLine("No route found...");
                System.Console.ReadLine();
            }
        }

        

        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }
}
