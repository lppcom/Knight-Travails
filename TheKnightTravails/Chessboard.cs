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
        private bool startTileFound = false;

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

        public void findPath()
        {
            setTileDistancesFromStart(); 
            // Check the distance from start to end
            int distance = tiles[endCol, endRow].DistanceFromStart - 1;
            turnList.Add(tiles[endCol, endRow]);

            List<Tile> nextTiles = getTilesWithinOneMove(tiles[endCol, endRow]);

            while (distance > 0)
            {
                foreach (Tile tile in nextTiles)
                {
                    if (tile.DistanceFromStart == distance)
                    {
                        distance--;
                        turnList.Add(tile);
                        //System.Console.WriteLine(tile);
                        nextTiles = getTilesWithinOneMove(tile);
                    }
                }
            }              
        }

        public List<Tile> findPath(Tile tile, int distance)
        {
            List<Tile> nextTiles = new List<Tile>();
            List<Tile> possibleTiles = getTilesWithinOneMove(tile);

            foreach (Tile tileToTry in possibleTiles)
            {
                if (tileToTry.DistanceFromStart == distance)
                {
                    nextTiles.Add(tileToTry);
                }
            }

            return nextTiles;
        }

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

        public String GetTurnList()
        {
            String output = "";

            foreach (Tile tile in turnList)
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
                    foreach(Tile tile in turnList)
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
