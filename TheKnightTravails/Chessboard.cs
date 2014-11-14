using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Chessboard
    {
        private int startRow, startCol, endCol, endRow; // get rid of these
        private List<Move> knightMoves = new List<Move>();
        private List<Tile> turnList = new List<Tile>(); // store a list of tiles for each iteration
        private Tile[,] tiles = new Tile[MAX_COLUMNS, MAX_ROWS];
        private Tile fromTile, toTile; // start and end targets
        bool pathComplete = false; // signify end of path, end target found
        private const int MAX_COLUMNS = 8;
        private const int MAX_ROWS = 8;
        private bool endTileFound = false;

        public Chessboard()
        {
            initialiseTiles();
            initialiseMoves();
        }

        public void test(int startCol, int startRow, int endCol, int endRow)
        {
            setTargetTiles(startCol, startRow, endCol, endRow);
            setTileDistancesFromStart();

            foreach (Tile tile in tiles)
            {
                String distance = " -- Distance from start: " + tile.DistanceFromStart.ToString();
                System.Console.WriteLine(tile.ToString() + distance);
            }
        }

        private void initialiseMoves()
        {
            knightMoves.Add(new Move(1, 2));
            knightMoves.Add(new Move(1, -2));
            knightMoves.Add(new Move(-1, 2));
            knightMoves.Add(new Move(-1, -2));
            knightMoves.Add(new Move(2, 1));
            knightMoves.Add(new Move(2, -1));
            knightMoves.Add(new Move(-2, 1));
            knightMoves.Add(new Move(-2, -1));
        }

        private void initialiseTiles()
        {
            for (int column = 0; column < MAX_COLUMNS; column++)
            {
                for (int row = 0; row < MAX_ROWS; row++)
                {
                    tiles[column,row] = new Tile(column, row);
                }
            }
        }

        // Initialise start and end tiles in tiles multi-dimensional array
        private void setTargetTiles(int startCol, int startRow, int endCol, int endRow)
        {
            this.startCol = startCol;
            this.startRow = startRow;
            this.endCol = endCol;
            this.endRow = endRow;

            tiles[startCol, startRow].IsStart = true;
            tiles[startCol, startRow].DistanceFromStart = 0;
            tiles[endCol, endRow].IsEnd = true;
        }

        private void findPath()
        {
            // Check the distance from start to end
            int distance = tiles[endCol, endRow].DistanceFromStart
        }

        private void setTileDistancesFromStart()
        {
            int distance = 1;
            setTileDistance(startCol, startRow, distance);

            while (!endTileFound)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile.DistanceFromStart == distance)
                    {
                        setTileDistance(tile.Column, tile.Row, distance + 1);
                    }
                }
                distance++;
            }  
        }

        private void setTileDistance(int col, int row, int distance)
        {
            // start with tiles[startcol, startRow] -> get all valid moves
            List<Move> nextMoves = validMovesFrom(tiles[col, row]);
            foreach (Move move in nextMoves)
            {
                tiles[move.Xdirection + col, move.Ydirection + row].DistanceFromStart = distance;
                
                if (tiles[move.Xdirection + col, move.Ydirection + row].IsEnd)
                {
                    // end tile has been found
                    endTileFound = true;
                    break;
                }
            }
        }

        // Check all valid moves from a particular tile
        private List<Move> validMovesFrom(Tile tile)
        {
            List<Move> validMoves = new List<Move>();

            // Check each possible knight move
            foreach (Move move in knightMoves)
            {
                int nextXStep = move.Xdirection + tile.Column;
                int nextYStep = move.Ydirection + tile.Row;

                // Check if the proposed move is within the chessboard limits
                if (nextXStep < MAX_COLUMNS && nextXStep >= 0 && nextYStep < MAX_ROWS && nextYStep >= 0)
                {
                    // Check if the tile has been landed on before
                    if (tiles[nextXStep, nextYStep].DistanceFromStart == -1)
                    {
                        validMoves.Add(move);
                    }
                } 
            }

            return validMoves;
        }

        public String GetTurnList()
        {
            String output = "";
            foreach (Tile tile in turnList)
            {
                output += tile.ToString() + " ";
            }
            
            return output;
        }
    }
}
