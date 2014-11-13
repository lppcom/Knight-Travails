using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    class Chessboard
    {
        private bool turnsRemaining = true; // valid squares left?
        private int startRow, startCol, endRow, endCol, turnCount;
        private List<Move> moves = new List<Move>();
        private List<Tile> turnList = new List<Tile>(); // store a list of tiles for each iteration
        private Tile[,] tiles;
        private Tile fromTile, toTile; // start and end targets
        bool pathComplete = false; // signify end of path, end target found
        private const int MAX_COLUMNS = 8;
        private const int MAX_ROWS = 8;

        public Chessboard()
        {
            //initialiseTiles();
            initialiseMoves();
        }

        private void initialiseMoves()
        {
            moves.Add(new Move(1, 2));
            moves.Add(new Move(1, -2));
            moves.Add(new Move(-1, 2));
            moves.Add(new Move(-1, -2));
            moves.Add(new Move(2, 1));
            moves.Add(new Move(2, -1));
            moves.Add(new Move(-2, 1));
            moves.Add(new Move(-2, -1));
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

        private bool isValidMove(Tile nextPosition)
        {
            bool newPosition = true;

            foreach (Tile tile in turnList)
            {
                if (tile.Matches(nextPosition)) // already been there before! 
                {
                    newPosition = false;
                    break;
                }
            }
            return (newPosition && nextPosition.Column >= 0 && nextPosition.Column < MAX_COLUMNS && nextPosition.Row >= 0 && nextPosition.Row < MAX_ROWS);
        }


        public bool FindARoute(int startCol, int startRow, int endCol, int endRow)
        {
            setTargetTiles(startCol,startRow,endCol,endRow);
                        
            do
            {
                takeTurn(fromTile);
            } while (turnsRemaining);

            return (pathComplete);
        }

        private void setTargetTiles(int startCol, int startRow, int endCol, int endRow)
        {
            fromTile = new Tile(startCol, startRow);
            toTile = new Tile(endCol, endRow);
        }

        private void takeTurn(Tile tileToTry)
        {
            if (tileMatchesEnd())
            {
                pathComplete = true;
            }
            else
            {
                // try each move from current tile
                foreach (Move move in moves)
                {
                    int nextXStep = move.xDirection + fromTile.Column;
                    int nextYStep = move.yDirection + fromTile.Row;

                    Tile nextTile = new Tile(nextXStep, nextYStep);
                    
                    if (isValidMove(nextTile))
                    {
                        fromTile = nextTile;
                        turnList.Add(fromTile);
                        System.Console.WriteLine(fromTile);
                        if (tileMatchesEnd())
                        {
                            turnsRemaining = false;
                            pathComplete = true;
                            break;
                        }
                            
                        //break;
                        //takeTurn(fromTile);
                    }
                    //else
                    //{
                      // System.Console.WriteLine("Stuck!!");
                       //System.Console.ReadLine();
                       //turnsRemaining = false;
                    //}
                }
            }
        }

        //private void takeTurn()
        //{
        //    if (startMatchesEnd())
        //    {
        //        travailsComplete = true;
        //    }
        //    else
        //    {
        //        if (fromTile.Row + 2 < 8)
        //        {
        //            fromTile.Row += 2;

        //            if (fromTile.Column + 1 < 8)
        //            {
        //                fromTile.Column += 1;
        //                turnList.Add(fromTile);

        //            }
        //            else if (fromTile.Column - 1 > 0)
        //            {
        //                fromTile.Column -= 1;
        //                turnList.Add(fromTile);
        //            }

        //        }
        //        else if (fromTile.Row - 2 > 0)
        //        {
        //            fromTile.Row -= 2;

        //            if (fromTile.Column + 1 < 8)
        //            {
        //                fromTile.Column += 1;
        //                turnList.Add(fromTile);

        //            }
        //            else if (fromTile.Column - 1 > 0)
        //            {
        //                fromTile.Column -= 1;
        //                turnList.Add(fromTile);

        //            }

        //        }
        //        else if (fromTile.Column + 2 < 8)
        //        {
        //            fromTile.Column += 2;

        //            if (fromTile.Row + 1 < 8)
        //            {
        //                fromTile.Row += 1;
        //                turnList.Add(fromTile);

        //            }
        //            else if (fromTile.Row - 1 > 0)
        //            {
        //                fromTile.Row -= 1;
        //                turnList.Add(fromTile);

        //            }

        //        }
        //        else if (fromTile.Column - 2 > 0)
        //        {
        //            fromTile.Column -= 2;

        //            if (fromTile.Row + 1 < 8)
        //            {
        //                fromTile.Row += 1;
        //                turnList.Add(fromTile);

        //            }
        //            else if (fromTile.Row - 1 > 0)
        //            {
        //                fromTile.Row -= 1;
        //                turnList.Add(fromTile);

        //            }

        //        }
        //    }
        //}

        public String GetTurnList()
        {
            String output = "";
            foreach (Tile tile in turnList)
            {
                output += tile.ToString() + " ";
            }
            
            return output;
        }
        
        private bool tileMatchesEnd()
        {
            return (fromTile.Row == toTile.Row && fromTile.Column == toTile.Column);
        }

        //public void testRun()
        //{
        //    setTile(0, 0);
        //    System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

        //    setTile(2, 3);
        //    System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

        //    setTile(4, 5);
        //    System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

        //    setTile(7, 1);
        //    System.Console.WriteLine("Tile @ " + col + "," + row + ": " + tiles[col, row].ToString());

        //    System.Console.ReadLine();
        //}

        //private void setTile(int col, int row)
        //{
        //    this.col = col;
        //    this.row = row;
        //}
    }
}
