using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheKnightTravails
{
    class Tile
    {
        public int Row { get; set;}
        public int Column { get; set; }

        public int DistanceFromStart { get; set; }

        public bool IsEnd { get; set; }

        public Tile(int column, int row)
        {
            Column = column;
            Row = row;
            DistanceFromStart = -1; // -1 signifies that tile has not been "stepped on" yet
        }

        public bool Matches(Tile tile)
        {
            return tile.Column == this.Column && tile.Row == this.Row;
        }

        public override String ToString()
        {
            String columnString = StaticTileLocations.GetColumnLetter(Column);
            String rowString = StaticTileLocations.GetRowNumber(Row);

            return columnString + rowString;
        }
    }
}
