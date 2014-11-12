using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Chessboard
    {
        private Tile[,] tiles;

        private int row, col; //for testing
        
        public Chessboard(int columns, int rows)
        {
            tiles = new Tile[columns, rows];

            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    tiles[column, row] = new Tile(column, row);
                }
            }
        }

        public void testRun()
        {
            setTile(0, 0);
            System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

            setTile(2, 3);
            System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

            setTile(4, 5);
            System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

            setTile(7, 1);
            System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

            setTile(10, 1);
            System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

            System.Console.ReadLine();
        }

        private void setTile(int col, int row)
        {
            this.col = col;
            this.row = row;
        }
    }
}
