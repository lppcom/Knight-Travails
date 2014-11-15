using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Chessboard
    {
        private int startRow, startCol, endCol, endRow; // start and end tile coordinates
        private List<Move> knightMoves = new List<Move>(); // list of moves a knight can make
        private List<Tile> pathList = new List<Tile>(); // list of steps in the shortest path
        private Tile[,] tiles = new Tile[MAX_COLUMNS, MAX_ROWS]; // each tile on the chessboard itself
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

        // Store a list of all possible moves for a knight
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

        // Build the empty chessboard
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
        public void setTargetTiles(int startCol, int startRow, int endCol, int endRow)
        {
            this.startCol = startCol;
            this.startRow = startRow;
            this.endCol = endCol;
            this.endRow = endRow;

            tiles[startCol, startRow].IsStart = true;
            tiles[startCol, startRow].DistanceFromStart = 0;
            tiles[endCol, endRow].IsEnd = true;
        }

        // Find a valid path from the start to end tile
        public void findPath()
        {
            // First set the distances between the start and end tiles
            setTileDistancesFromStart(); 
            // Check the distance from start to end, and remove one to begin traversing backwards through the path
            int distance = tiles[endCol, endRow].DistanceFromStart - 1;
            // The last tile is added to the pathList to be displayed to the user
            pathList.Add(tiles[endCol, endRow]);
            // nextTiles stores the next set of tiles within one move from the current tile
            List<Tile> nextTiles = getTilesWithinOneMove(tiles[endCol, endRow]);
            // a distance of 0 means that the start tile has been found
            while (distance > 0)
            {
                // Check each tile within one move - if it has distance-1 from the current tile,
                // it is part of a valid move back from the start, so restart the search with it as the reference tile. 
                foreach (Tile tile in nextTiles)
                {
                    if (tile.DistanceFromStart == distance)
                    {
                        distance--;
                        pathList.Add(tile);
                        nextTiles = getTilesWithinOneMove(tile);
                    }
                }
            }              
        }

        // Returns a list of tiles that can be moved to from the given tile
        private List<Tile> getTilesWithinOneMove(Tile tile)
        {
            List<Tile> validTiles = new List<Tile>();
            List<Move> moves = validMovesFrom(tile);
            foreach (Move move in moves)
            {
                Tile nextTile = tiles[move.Xdirection + tile.Column, move.Ydirection + tile.Row];

                validTiles.Add(nextTile);
            }
            return validTiles;
        }

        // Sets the distance of each tile from the start tile, until it finds the end tile
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
                // Check if the tile has been landed on before
                if (tiles[col+move.Xdirection, move.Ydirection + row].DistanceFromStart == -1)
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
                    validMoves.Add(move);
                } 
            }

            return validMoves;
        }

        // Returns a list of tiles in a valid knight path
        public String GetTurnList()
        {
            String output = "";

            foreach (Tile tile in pathList)
            {
                output = tile.ToString() + " " + output;
            }
            
            return output;
        }

        public void PrintBoard()
        {
            System.Console.WriteLine();

            for (int column = 7; column >= 0; column--)
            {
                for (int row = 0; row < MAX_ROWS; row++)
                {
                    System.Console.Write(tiles[row,column] + " " + tiles[row,column].DistanceFromStart);
                    foreach(Tile tile in pathList)
                    {
                        if (tiles[row, column].Matches(tile))
                            System.Console.Write("**");
                        else
                            System.Console.Write("  ");
                    }
                    if (tiles[row, column].DistanceFromStart == -1)
                        System.Console.Write(" | ");
                    else
                        System.Console.Write("  | ");
                }
                System.Console.WriteLine();
                System.Console.WriteLine("-------------------------------------------------");
                System.Console.WriteLine();
            }
        }
    }
}
