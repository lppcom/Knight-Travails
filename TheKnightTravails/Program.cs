using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Program
    {
        private Chessboard chessBoard;

        private void calculateKnightTravails()
        {
            chessBoard = new Chessboard();
            getTargetTiles();
            chessBoard.findPath();
            System.Console.WriteLine("Shortest path: " + chessBoard.GetTurnList());
            System.Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            Program knightTravails = new Program();
            System.Console.WriteLine(knightTravails.startUpMessage());
            while (true)
            {
                knightTravails.calculateKnightTravails();
            }
        }

        private string startUpMessage()
        {
            return "This program allows you to calculate the" + Environment.NewLine + "shortest route that a knight piece can" + Environment.NewLine + "take between two tiles on an 8x8 chessboard." + Environment.NewLine + "To leave the program, simply type quit." + Environment.NewLine;
        }

        private String[] getUserInput()
        {
            System.Console.Write("Start and end tiles: ");
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
                System.Console.WriteLine("Enter start and end positions, separated by a space.");
                System.Console.WriteLine();
                String[] input = getUserInput();
                if (input.Length != 2) // Invalid format received
                {
                    inputCheck = false;
                    System.Console.WriteLine("Invalid input detected.");
                    System.Console.WriteLine("Please enter two tiles in chessboard notation, separated by a space - e.g A1 B8");
                    System.Console.WriteLine("");
                }
                else
                {
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

        private int[] getTargetTiles(char[] coords)
        {
            int[] numberCoords = new int[2];

            // Offsets of 65 and 49 applied to make ASCII chars of A-H and 1-8 between 0 & 7 
            numberCoords[0] = (coords[0] - 65);
            numberCoords[1] = (coords[1] - 49);
           
            return numberCoords;
        }

        private bool isValidInput(int input)
        {
            return (input >= 0 && input < 8);  
        }
    }
}
