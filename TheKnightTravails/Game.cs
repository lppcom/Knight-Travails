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
            while(true)
            {
                chessBoard = new Chessboard();
                getTargetTiles();
                chessBoard.findPath();
                System.Console.WriteLine(chessBoard.GetTurnList());
                chessBoard.PrintBoard();
                System.Console.ReadLine();
            }
            
        }

        private String[] getUserInput()
        {
            String rawInput = System.Console.ReadLine().ToUpper();
            if (rawInput.Equals("QUIT"))
            {
                System.Environment.Exit(0);
            }
            return rawInput.Split(' ');
        }
        
        private void getTargetTiles()
        {
            bool inputCheck;
            do
            {
                System.Console.WriteLine("Enter start and end positions please.");
                System.Console.WriteLine();
                String[] input = getUserInput();

                int[] startCoords = getTargetTiles(input[0].ToCharArray());
                int[] endCoords = getTargetTiles(input[1].ToCharArray());

                inputCheck = checkCoords(startCoords) && checkCoords(endCoords);

                if (inputCheck)
                {
                    chessBoard.setTargetTiles(startCoords[0], startCoords[1], endCoords[0], endCoords[1]);
                }
                else
                {
                    System.Console.WriteLine("Invalid input detected. Columns must be between A-H, Rows between 1-8.");
                    System.Console.WriteLine("Please try again.");
                    System.Console.WriteLine("-------------------------------------");
                }       
            } while (!inputCheck);
            
        }

        private bool checkCoords(int[] coords)
        {
            bool coordCheck = true;
            foreach (int coord in coords)
            {
                coordCheck = coordCheck && isValidInput(coord);
            }
            return coordCheck;
        }

        private void printList(int[] list)
        {
            foreach (int item in list)
            {
                System.Console.Write(item + " ");
            }
        }

        private int[] getTargetTiles(char[] coords)
        {
            int[] numberCoords = new int[2];

            // Offsets of 65 and 49 applied to make inputs between 0 & 8
            numberCoords[0] = (coords[0] - 65);
            numberCoords[1] = (coords[1] - 49);
           
            return numberCoords;
        }

        private bool isValidInput(int input)
        {
            return (input >= 0 && input < 8);  
        }

        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }
}
